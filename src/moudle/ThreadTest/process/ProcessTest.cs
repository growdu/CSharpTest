using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
    class ProcessTest
    {
        static void Main(string []args)
        {
            ParallerTest();
            //ConcurrentDictionaryTest();
            //foreach (Process p in Process.GetProcesses())
            //{
            //    Console.Write(p.ProcessName);
            //    Console.Write("----");
            //    Console.WriteLine(GetProcessUserName(p.Id));
                
            //}
            //Console.ReadKey();
            //Process[] pp = Process.GetProcessesByName("acrobat");
            //pp[0].Kill();
        }

        static void ParallerTest()
        {
            Console.WriteLine("This is main thread.");
            Parallel.Invoke(()=>Hello1(),()=>Hello2());
            Console.WriteLine("main thread over.");
            Console.ReadKey();
        }

        static void Hello1()
        {
            Console.WriteLine("This is  thread1.");
            var lists = new List<int>();
            for (int i=0;i<10;i++)
            {
                lists.Add(i);
               
            }
            Parallel.ForEach(lists, (item, p) =>
            {
                Console.WriteLine("hello1 " + item);
                Thread.Sleep(145);
            });
            Console.WriteLine("thread1 over.");
        }

        static void Hello2()
        {
            Console.WriteLine("This is  thread2.");
            var lists = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                lists.Add(i);

            }
            lists.ForEach(a =>
            {
                Console.WriteLine("hello2 " + a);
                Thread.Sleep(145);
            });
            Console.WriteLine("thread2 over.");
        }

        private static string GetProcessUserName(int pID)
        {
            string text1 = null;

            SelectQuery query1 = new SelectQuery("Select * from Win32_Process WHERE processID=" + pID);
            ManagementObjectSearcher searcher1 = new ManagementObjectSearcher(query1);

            try
            {
                foreach (ManagementObject disk in searcher1.Get())
                {
                    ManagementBaseObject inPar = null;
                    ManagementBaseObject outPar = null;

                    inPar = disk.GetMethodParameters("GetOwner");

                    outPar = disk.InvokeMethod("GetOwner", inPar, null);

                    text1 = outPar["User"].ToString();
                    break;
                }
            }
            catch
            {
                text1 = "SYSTEM";
            }

            return text1;
        }

        static void ConcurrentDictionaryTest()
        {
            ConcurrentDictionary<string, string> task = new ConcurrentDictionary<string, string>();
            task.TryAdd("0","nice");
            task.TryAdd("1","good");
            task.TryAdd("2", "beautiful");
            task.TryAdd("3", "wonderful");
            task.TryAdd("4", "excelent");
            task.TryAdd("5", "great");
            Task.Factory.StartNew(() =>
            {
                task.TryAdd("6", "bad");
                task.TryAdd("7", "terrible");
            });
            foreach (var key in task.Keys)
            {
                Console.WriteLine(key+"--------"+task[key]);
                string val = task[key];
                task.TryRemove(key,out val);
                Console.WriteLine(task.Keys.Count);
            }
        }

    }
}
