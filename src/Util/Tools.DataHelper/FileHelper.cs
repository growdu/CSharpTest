using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tools.DataHelper
{
   public class FileHelper
    {
        /// <summary>
        /// 递归获取目录下的所有文件
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns></returns>
        public static List<string> GetAllFiles(string path)
        {
            if (File.Exists(path))
                return new List<string>() { path };

            if (!Directory.Exists(path))
            {
                Log.Error("目录不存在。");
                return null;
            }

            List<string> files = new List<string>();
            string[] dirs = null;
            dirs = Directory.GetDirectories(path);
            if (dirs != null && dirs.Length > 0)
            {
                foreach (var dir in dirs)
                {
                    files.AddRange(GetAllFiles(dir));
                }
            }
            var fs = Directory.GetFiles(path);
            if (fs != null && fs.Length > 0)
            {
                files.AddRange(fs);
            }
            return files;
        }

        /// <summary>
        /// 统计文件行数
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>返回文件行数</returns>
        public static int CountFileLine(string path)
        {
            int currentLine = 0;
            int limit = 1000;
            int count = 0;
            var lines = FileHelper.ReadMultiLines(path, currentLine, limit);
            while (lines != null&&lines.Count()>0)
            {
                currentLine += limit;
                count += lines.Count();
                lines = ReadMultiLines(path, currentLine, limit);
            }
            return count;
        }

        /// <summary>
        /// 按行读取文件
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="currentLine">读取的起始位置</param>
        /// <param name="limit">一次读取的行数</param>
        /// <returns></returns>
        public static IEnumerable<string> ReadMultiLines(string file, int currentLine, int limit = 500)
        {
            try
            {
                var lines = File.ReadLines(file, Encoding.UTF8).Skip(currentLine).Take(limit);
                return lines;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
