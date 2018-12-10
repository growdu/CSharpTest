using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TCPTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.SetMaxThreads(1000, 1000);
            IPAddress address = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(address,50000);
            listener.Start();
            while (true)
            {
                ChatClient chatClient =new ChatClient( listener.AcceptTcpClient());
            }
        }

        public class ChatClient
        {
            static TcpClient tcpClient;
            static byte[] byteMessage;
            static string clientEndPoint;

            public ChatClient(TcpClient client)
            {
                tcpClient = client;
                byteMessage = new byte[tcpClient.ReceiveBufferSize];

                //显示客户端信息
                clientEndPoint = tcpClient.Client.RemoteEndPoint.ToString();
                Console.WriteLine("Client's endpoint is "+clientEndPoint);

                //使用NetworkStream.BeginRed异步读取信息
                NetworkStream networkStream = tcpClient.GetStream();
                networkStream.BeginRead(byteMessage,0,tcpClient.ReceiveBufferSize,
                    new AsyncCallback(ReceiveAsyncCallback),null);
            }

            public void ReceiveAsyncCallback(IAsyncResult iAsyncResult)
            {
                //显示CLR线程池状态
                Thread.Sleep(100);
                ThreadPoolMessage("\nMessage is receiving");

                //使用NetworkStream.EndRead结束异步读取
                NetworkStream networkStreamRead = tcpClient.GetStream();
                int length = networkStreamRead.EndRead(iAsyncResult);

                //如果接收到的数据长度少于1则抛出异常
                if (length < 1)
                {
                    tcpClient.GetStream().Close();
                    throw new Exception("Disconnection!");
                }

                //显示接收信息
                string message = Encoding.UTF8.GetString(byteMessage, 0, length);
                Console.WriteLine("Message:" + message);

                //使用NetworkStream.BeginWrite异步发送信息
                byte[] sendMessage = Encoding.UTF8.GetBytes("Message is received!");
                NetworkStream networkStreamWrite = tcpClient.GetStream();
                networkStreamWrite.BeginWrite(sendMessage, 0, sendMessage.Length,
                                                new AsyncCallback(SendAsyncCallback), null);
            }

            //把信息转换成二进制数据，然后发送到客户端
            public void SendAsyncCallback(IAsyncResult iAsyncResult)
            {
                //显示CLR线程池状态
                Thread.Sleep(100);
                ThreadPoolMessage("\nMessage is sending");

                //使用NetworkStream.EndWrite结束异步发送
                tcpClient.GetStream().EndWrite(iAsyncResult);

                //重新监听
                tcpClient.GetStream().BeginRead(byteMessage, 0, tcpClient.ReceiveBufferSize,
                                                   new AsyncCallback(ReceiveAsyncCallback), null);
            }

            //显示线程池现状
            static void ThreadPoolMessage(string data)
            {
                int a, b;
                ThreadPool.GetAvailableThreads(out a, out b);
                string message = string.Format("{0}\n  CurrentThreadId is {1}\n  " +
                      "WorkerThreads is:{2}  CompletionPortThreads is :{3}\n",
                      data, Thread.CurrentThread.ManagedThreadId, a.ToString(), b.ToString());

                Console.WriteLine(message);
            }

        }
    }
}
