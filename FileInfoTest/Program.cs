using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileInfoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                FileInfo fi = new FileInfo(@"1.pdf");
                long length = fi.Length;
                string lasttime = fi.LastWriteTime.ToLongDateString();
                Console.WriteLine(length +"\n"+ lasttime);
                Console.ReadKey();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace+ex.ToString());
                Console.ReadKey();
            }
            
        }
    }
}
