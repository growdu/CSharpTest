using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileTest
{
    class DirTest
    {
        public static void TravelDir()
        {
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
