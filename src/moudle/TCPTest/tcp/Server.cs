using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPTest.tcp
{
    class Server
    {
        static int BufferSize = 8192;
        static void Main(string[] args)
        {
            IPAddress ip = new IPAddress(new byte[] { 10, 1, 53, 128});
            // 获得IPAddress对象的另外几种常用方法：
           // IPAddress ip = IPAddress.Parse("192.168.0.100");
           // IPAddress ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
            TcpListener listen = new TcpListener(ip,25000);
            listen.Start(); // 开始侦听
            Console.WriteLine("Start Listening ...");
            // 获取一个连接，中断方法
            //TcpClient remoteClient = listen.AcceptTcpClient();

            while (true)
            {
                // 获取一个连接，中断方法
                TcpClient remoteClient = listen.AcceptTcpClient();
                // 打印连接到的客户端信息
                Console.WriteLine("Client Connected! Local:{0} <-- Client:{1}",
                remoteClient.Client.LocalEndPoint, remoteClient.Client.RemoteEndPoint);
                // 获得流，并写入buffer中
                NetworkStream streamToClient = remoteClient.GetStream();
                byte[] buffer = new byte[BufferSize];
                int bytesRead;
                MemoryStream ms = new MemoryStream();
                do
                {
                    bytesRead = streamToClient.Read(buffer, 0, BufferSize);
                    ms.Write(buffer,0,bytesRead);
                } while (bytesRead>0);
                buffer = ms.GetBuffer();
                string msg = Encoding.Unicode.GetString(buffer);
                Console.WriteLine(msg);
                // 获得请求的字符串
                Console.WriteLine("\n\n输入\"Q\"键退出。");
                ConsoleKey key;
                do
                {
                    key = Console.ReadKey(true).Key;
                } while (key != ConsoleKey.Q);
            }

           
        }
    }
}
