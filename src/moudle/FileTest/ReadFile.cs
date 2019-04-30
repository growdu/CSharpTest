using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileTest
{
 /***********************************************************************
 * 文 件 名：
 * CopyRight(C) 2019-2029 https://www.github.com/growdu
 * 创 建 人：growdu
 * 创建日期：2019-04-24
 * 修 改 人：
 * 修改日期：
 * 描    述：读取大文件
 ***********************************************************************/
    class ReadFile
    {
        /// <summary>
        /// 读取大的txt文件
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <param name="currentLine">从文件的哪一行读取</param>
        /// <param name="limit">每次读取多少行</param>
        /// <returns></returns>
        public static IEnumerable<string> readMultiLines(string file, int currentLine, int limit = 500)
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

        public static void CopyFile(string sourceName,string targetName)
        {
            int bufferSize = 10240;
            using (Stream source = new FileStream(sourceName, FileMode.Open, FileAccess.Read))
            {
                using (Stream target = new FileStream(targetName, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = new byte[bufferSize];
                    int byteRead;
                    do
                    {
                        byteRead = source.Read(buffer, 0, bufferSize);
                        target.Write(buffer, 0, byteRead);
                    } while (byteRead > 0);
                }
            }
        }

    }
}
