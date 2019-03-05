using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceTest
{
    public partial class MyService : ServiceBase
    {
        public MyService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Task.Factory.StartNew(Handle);
        }

        protected override void OnStop()
        {
        }

        void Handle()
        {
            while (true)
            {
                try
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + "service.log";
                    string context = "MyWindowsService: Service Stoped " + DateTime.Now + "\n";
                    WriteLogs(path, context);
                    Thread.Sleep(600);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void WriteLogs(string path, string context)
        {
            using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using(StreamWriter sw=new StreamWriter(fs))
                {
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(context);
                    sw.Flush();
                }
            } 
        }
    }
}
