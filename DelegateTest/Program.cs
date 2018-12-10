using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace DelegateTest
{
    class Program
    {
        public delegate void MyDelegate();

        delegate string TestDelegate(string name);

        public static void Test()
        {
            Console.WriteLine("test test test.");
        }
        static void Main(string[] args)
        {
            ThreadMessage("Main thread");
            TestDelegate hello = new TestDelegate(Hello);
            //异步调用委托，获取计算结果
            IAsyncResult result = hello.BeginInvoke("erha", null, null);
            for(int i = 0; i < 10; i++)
            {
                ThreadMessage(i.ToString());
            }
            while (!result.IsCompleted)
            {
                Thread.Sleep(200);      //虚拟操作
                Console.WriteLine("Main thead do work!");
            }
            //等待异步方法完成，调用EndInvoke(IAsyncResult)获取运行结果
            string data = hello.EndInvoke(result);
            Console.WriteLine(data);
            Console.ReadKey();
            MyDelegate delegate1 = new MyDelegate(Test);
            //显示委托类的几个方法成员     
            var methods = delegate1.GetType().GetMethods();
            if (methods != null)
                foreach (MethodInfo info in methods)
                    Console.WriteLine(info.Name);
            Console.ReadKey();
        }


        static string Hello(string name)
        {
            ThreadMessage("Async Thread");
            Thread.Sleep(2000);            //虚拟异步工作
            return "Hello " + name;
        }

        //显示当前线程
        static void ThreadMessage(string data)
        {
            string message = string.Format("{0}\n  ThreadId is:{1}",
                   data, Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(message);
        }

    }
}
