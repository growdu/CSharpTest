using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Timers;

namespace TimeTest
{
    class Program
    {
        /// <summary>
        /// 定时任务，如指定每天某个时间节点执行某个任务
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now.Minute);
            DataTable dt = new DataTable();
            dt.Columns.Add("test");
            DataRow dr = dt.NewRow();
            dr["test"] = null;
             Console.Title = "定时服务测试";
            Console.WriteLine("服务启动中.......");
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Interval = 6000;
            //timer.Interval = 3600000;//执行间隔时间,单位为毫秒;此时时间间隔为1小时
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Send);
            Console.ReadKey();
        }

        /// <summary>
        /// 当设置的间隔时间达到时，就会执行
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        static void Send(object source, ElapsedEventArgs e)
        {
            if (DateTime.Now.Minute == 0)
            {
                Console.WriteLine(DateTime.Now);
            }
            try
            {
                if (DateTime.Now.Hour == 15)
                {
                   
                }
                else if (DateTime.Now.Day == 15 || DateTime.Now.Day == 27)
                {
                    
                }
            }
            catch
            {
                
            }
        }

    }
}
