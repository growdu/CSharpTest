using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TCPTest
{
    
    class Program
    {
        static string dataDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;
        static void Main(string[] args)
        {
            string cookie = GetCookie();
            var ids=GetSocket(cookie);
            foreach(var id in ids)
            {
                Console.WriteLine(GetData(cookie,id));
            }
            ThreadPool.SetMaxThreads(1000, 1000);
            IPAddress address = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(address,50000);
            listener.Start();
            while (true)
            {
                ChatClient chatClient =new ChatClient( listener.AcceptTcpClient());
            }
        }


        static string GetCookie()
        {
            string cookie = "";
            string user = "";
            string password = "";
            string url = "https://127.0.0.1";
            JObject json = new JObject();
            JProperty jp = new JProperty("username",user);
            json.Add(jp);
            jp = new JProperty("password", password);
            json.Add(jp);
            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            //生成文件流
            byte[] buffer = Encoding.UTF8.GetBytes(json.ToString());
            //向流中写字符串
            StreamWriter writer = null;
            //根据url创建请求对象
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            try
            {
                writer = new StreamWriter(request.GetRequestStream());
                writer.Write(json);
            }
            catch (Exception)
            {

            }
            finally
            {
                writer.Close();
            }
            HttpWebResponse objresponse = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(objresponse.GetResponseStream()))
            {
                cookie= sr.ReadToEnd();
                sr.Close();
            }
            JObject js = (JObject)JsonConvert.DeserializeObject(cookie);
            cookie = "user-" + js["data"]["id"] + "-" + js["data"]["token"];
            using(FileStream fs=new FileStream("data.txt",FileMode.OpenOrCreate))
            {
                StreamWriter wr = new StreamWriter(fs);
                wr.Write(cookie);
                wr.Flush();
                wr.Close();
            }
            return cookie;
        }

        static List<string> GetSocket(string cookie)
        {
            string url = "https://127.0.0.1";
            string result = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Headers.Add("Authorization", cookie);
            HttpWebResponse objresponse = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(objresponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                sr.Close();
            }
            JObject json= (JObject)JsonConvert.DeserializeObject(result);
            JToken data = json["data"];
            List<string> ids = new List<string>();
            for(int i = 0; i < data.Count(); i++)
            {
                ids.Add(data[i]["id"].ToString());
            }
            return ids;
        }

        static string GetData(string cookie,string id)
        {
            string url = "https://127.0.0.1";
            string result = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Headers.Add("Authorization", cookie);
            request.ContentType = "application/json";
            JObject json = new JObject(new JProperty("id",id),new JProperty("startDate","2019-04-01"), new JProperty("endDate", "2019-04-02"));
            var writer = new StreamWriter(request.GetRequestStream());
            writer.Write(json);
            writer.Close();
            HttpWebResponse objresponse = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(objresponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;
        }

        static string VisitWeb(string url,string method,string body)
        {
            string result= "";
            return result;
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
