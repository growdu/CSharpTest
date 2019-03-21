using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;

namespace ThreadTest
{
    class ProcessTest
    {
        static void Main(string []args)
        {
            foreach (Process p in Process.GetProcesses())
            {
                Console.Write(p.ProcessName);
                Console.Write("----");
                Console.WriteLine(GetProcessUserName(p.Id));
                
            }
            Console.ReadKey();
            Process[] pp = Process.GetProcessesByName("acrobat");
            pp[0].Kill();
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

    }
}
