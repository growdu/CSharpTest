using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{

    public class Person
    {
        public string Name
        {
            get;
            set;
        }
        public int Age
        {
            get;
            set;
        }
    }


    public class Message
    {
        public Message()
        {

        }
        public void ShowMessage()
        {
            string message = string.Format("Async threadId is :{0}",
                                             Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(message);
            for(int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("The number is {0}", i);
            }
        }

        public void ShowMessage(Object person)
        {
            Person _person = null;
            if (person != null)
            {
                _person = (Person)person;
                string message = string.Format("\n{0}'s age is {1}!\nAsync another threadId is:{2}",
                    _person.Name, _person.Age, Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine(message);
            }
            for (int n = 0; n < 10; n++)
            {
                Thread.Sleep(300);
                Console.WriteLine("The number is:" +_person.Name+n.ToString());
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var mess = string.Format("  CurrentThreadID is:{0}\tAppDomainID is:{1}",
                Thread.CurrentThread.ManagedThreadId, AppDomain.CurrentDomain.Id);
            Console.WriteLine(mess);
            Console.Read();
            AppDomain domain =AppDomain.CurrentDomain;
            int id = domain.Id;
            string path = domain.BaseDirectory;
            var processList = Process.GetProcesses().OrderBy(x => x.Id).Take(10);
            foreach(var pro in processList)
            {
                Console.WriteLine("Process ID is:{0}\t process name is: {1}"
                    ,pro.Id,pro.ProcessName);
            }
            Process process = Process.Start("notepad",@"C:\users\duanys\desktop\test.cs");

            var mouldeList = Process.GetCurrentProcess().Modules;
            foreach(ProcessModule moulde in mouldeList)
            {
                Console.WriteLine("Process mouldeName is:{0}\t process fileName is: {1}\t version:{2}"
                  , moulde.ModuleName, moulde.FileName,moulde.FileVersionInfo.FileVersion);
            }
            Console.WriteLine("The main thread is :{0}",Thread.CurrentThread.ManagedThreadId);
            Message message = new Message();
            Thread thread = new Thread(new ThreadStart(message.ShowMessage));
            thread.Start();
            Console.WriteLine("Do something......");
            Console.WriteLine("Main thread is completed.");
            while (thread.IsAlive)
            {
                ;
            }
            Thread thread1 = new Thread(new ParameterizedThreadStart(message.ShowMessage));
            Person person = new Person();
            person.Age = 10;
            person.Name = "alan";
            thread1.Start(person);
            Console.WriteLine("Do another thing......");
            Console.WriteLine("Main thread is completed.");
            Person person1 = new Person();
            person1.Age = 100;
            person1.Name = "erha";
            Task.Factory.StartNew(()=> {
                message.ShowMessage(person1);
            });
            Console.ReadKey();
            try
            {
                process.Kill();
            }
            catch
            {

            }
        }
    }
}
