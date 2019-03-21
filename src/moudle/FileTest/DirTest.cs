using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileTest
{
    /// <summary>
    /// 目录测试
    /// </summary>
    public class DirTest
    {
        /// <summary>
        /// 遍历目录
        /// </summary>
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

        /// <summary>
        /// 在path目录下按时间创建目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string CreateDir(string path)
        {
            string[] date = new string[] {
                 DateTime.Now.Year.ToString(),
                  DateTime.Now.Month.ToString(),
                   DateTime.Now.Day.ToString()
            };
            foreach(string d in date)
            {
                path = Path.Combine(path,d);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            return path;
        }

    }
}
