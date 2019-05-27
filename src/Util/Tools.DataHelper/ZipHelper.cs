using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tools.DataHelper
{
    public class ZipHelper
    {

        /// <summary>   
        /// 解压功能(解压压缩文件到指定目录)   
        /// </summary>   
        /// <param name="fileToUnZip">待解压的文件</param>   
        /// <param name="zipedFolder">指定解压目标目录</param>   
        /// <param name="password">密码</param>   
        /// <returns>解压结果</returns>   
        public static bool UnZip(string fileToUnZip, string zipedFolder, string password)
        {

            if (!File.Exists(fileToUnZip))
                return false;

            if (!Directory.Exists(zipedFolder))
                Directory.CreateDirectory(zipedFolder);

            using (ZipInputStream zipStream = new ZipInputStream(File.OpenRead(fileToUnZip)))
            {
                if (!password.IsEmpty())
                {
                    zipStream.Password = password;
                }
                ZipEntry ent = null;
                while ((ent = zipStream.GetNextEntry()) != null)
                {
                    if (!ent.Name.IsEmpty())
                    {
                        var fileName = Path.Combine(zipedFolder, ent.Name);
                        fileName = fileName.Replace('/', '\\');//change by Mr.HopeGi   

                        if (fileName.EndsWith("\\")&&!Directory.Exists(fileName))
                        {
                            Directory.CreateDirectory(fileName);
                            continue;
                        }
                        using (FileStream fs = new FileStream(fileName, FileMode.Create))
                        {
                            int size = 2048;
                            byte[] buffer = new byte[size];
                            int bytesRead = 0;
                            do
                            {
                                bytesRead = zipStream.Read(buffer, 0, size);
                                fs.Write(buffer, 0, bytesRead);
                            } while (bytesRead > 0);
                        }
                    }
                }
            }
            return true;
    }

        /// <summary>   
        /// 解压功能(解压压缩文件到指定目录)   
        /// </summary>   
        /// <param name="fileToUnZip">待解压的文件</param>   
        /// <param name="zipedFolder">指定解压目标目录</param>   
        /// <returns>解压结果</returns>   
        public static bool UnZip(string fileToUnZip, string zipedFolder)
        {
            return UnZip(fileToUnZip, zipedFolder, null);
        }

    }
}
