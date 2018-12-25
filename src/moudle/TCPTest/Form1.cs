using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace TCPTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //连接服务端
            TcpClient tcpClient = new TcpClient("127.0.0.1", 500);

            //发送信息
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] sendMessage = Encoding.UTF8.GetBytes("Client request connection!");
            networkStream.Write(sendMessage, 0, sendMessage.Length);
            networkStream.Flush();

            //接收信息
            byte[] receiveMessage = new byte[1024];
            int count = networkStream.Read(receiveMessage, 0, 1024);
            Console.WriteLine(Encoding.UTF8.GetString(receiveMessage));
            Console.ReadKey();
        }
    }
}
