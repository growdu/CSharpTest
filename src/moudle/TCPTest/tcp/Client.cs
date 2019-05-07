using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPTest.tcp
{
    class Client
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client isrunning ...");
            TcpClient client = new TcpClient();
            try
            {
                client.Connect(IPAddress.Parse("10.1.53.128"), 25000);
            }
            catch (Exception ex)
            {

            }
            ConsoleKey key;
            // 打印连接到的服务端信息
            Console.WriteLine("Server Connected! Local:{0} -->Server:{1}",
            client.Client.LocalEndPoint, client.Client.RemoteEndPoint);
            string msg = "Hello, readers!";
            NetworkStream streamToServer = client.GetStream();
            byte[] buffer = Encoding.Unicode.GetBytes(msg); // 获得缓存
            streamToServer.Write(buffer, 0, buffer.Length); // 发往服务器
            Console.WriteLine("Sent: {0}", msg);
            do
            {
                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Q);
        }

    }
}
