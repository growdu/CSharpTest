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
            string file = Directory.GetParent("..").FullName;
            file += @"\examples\1.pdf";
            PdfParse pdf = new PdfParse(file);
            Console.WriteLine(pdf.Content);
            //创建xml
            XmlTest.CreateXml();
            string name = @".\页脚未去除.pdf";
            //测试访问目录
            DirTest.TravelDir();
        }
    }
}
