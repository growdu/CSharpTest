using FundExtract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinFormTest
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll", EntryPoint = "GetSystemDefaultLCID")]
        public static extern int GetSystemDefaultLCID();

        [DllImport("kernel32.dll", EntryPoint = "SetLocaleInfoA")]
        public static extern int SetLocaleInfo(int Locale, int LCType, string lpLCData);

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (true)
                    return;
            }
            catch
            {

            }
            finally
            {
                MessageBox.Show("finally.");
            }

            DateTime time = DateTime.Now;
            string s = time.ToString("yyyy年MM日dd日dddd");
            //s = time.DayOfWeek.ToString();
            string date = "2018-11-26";
            DateTime dt = Convert.ToDateTime(date);
            Type t = dt.GetType();
            dt=dt.AddDays(1);
            Application.Run();
            MessageBox.Show(t.ToString());
            string path = @"C:\Users\duanys\Desktop\officework\代码提取\基金\测试案例\需匹配内容提及代码\
中融基金管理有限公司关于旗下部分基金增加财通证券股份有限公司为销售机构并开展费率优惠活动的公告.pdf";
            try
            {
                FundData fund = FundData.Extract(path);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString()+ex.StackTrace);
            }
            Console.WriteLine("Hello World!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x = GetSystemDefaultLCID();
            MessageBox.Show(x.ToString());
            string name = Thread.CurrentThread.ToString();
            Thread thread = Thread.CurrentThread;
            Context context = Thread.CurrentContext;
            Thread.Sleep(10);
            if (thread.IsAlive)
            {
                ;
            }
            if (thread.IsBackground)
            {
                ;
            }
            if (thread.IsThreadPoolThread)
            {
                ;
            }
            thread.Abort();
            thread.Start();
            thread.Join();
            thread.Priority = ThreadPriority.Normal;
            Mutex mutex = new Mutex();
        }
    }
}
