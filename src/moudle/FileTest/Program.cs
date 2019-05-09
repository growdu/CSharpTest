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
            var t = ftp.FtpHelper.GetFileList("/");
            ReadFile.CopyFile(@"C:\Users\duanys\Desktop\officework\数据对接\超对称数据\e-commerce_data_daily.csv", @"C:\Users\duanys\Desktop\officework\数据对接\超对称数据\test.txt");
            string begin = "";
            string end = "";
            if (args.Length != 2)
            {
                Console.WriteLine("param error");
                return;
            }
            else
            {
                begin = args[0];
                end = args[1];
                Console.WriteLine("Extract data from" + begin + " to " + end);
                Console.ReadKey();
            }
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
