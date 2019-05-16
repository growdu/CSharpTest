using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Service1Client client = new Service1Client();

            // 使用 "client" 变量在服务上调用操作。
            Console.WriteLine(client.GetData(100));
            Console.ReadKey();
            // 始终关闭客户端。

            client.Close();
        }
    }
}
