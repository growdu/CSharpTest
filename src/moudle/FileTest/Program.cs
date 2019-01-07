using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileTest
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlTest.CreateXml();
            string name = @".\页脚未去除.pdf";
            PdfParse.Extract(name);
            string fullName = @"C:博时\20181211临时公告.7z";
            FileInfo f = new FileInfo(fullName);
            string path = f.DirectoryName + "\\";
            if (true)
            {
                string day = DateTime.Now.ToString("yyyyMMdd");
                if (!path.Contains(day))
                {
                    path += day + "\\";
                }
            }
            if (true)
            {
                string[] files = Directory.GetFiles(path);
                DirectoryInfo dir = new DirectoryInfo(path);
                DirectoryInfo[] dirs = dir.GetDirectories();
                if (dirs.Length > 0)
                {
                    files = Directory.GetFiles(path + dirs[0].Name);
                }
            }
        }
    }
}
