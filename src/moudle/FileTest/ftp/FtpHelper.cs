using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace FileTest.ftp
{
    class FtpHelper
    {
        private static string ftpPath;
        private static string userName;
        private static string password;

        static void Main(string[] args)
        {
            var t = GetFileList("/");
        }

        static FtpHelper()
        {
            ftpPath= ConfigurationManager.AppSettings["ftpPath"];
            userName= ConfigurationManager.AppSettings["userName"];
            password= ConfigurationManager.AppSettings["password"];
        }

        /// <summary>
        /// 获取ftp请求
        /// </summary>
        /// <param name="method">ftp协议方法类型</param>
        /// <returns></returns>
        public static FtpWebRequest GetRequest(string method)
        {
            var request = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpPath));
            request.UseBinary = true;
            request.Credentials = new NetworkCredential(userName, password);//设置用户名和密码
            request.Method = method;
            return request;
        }

        /// <summary>
        /// 获取ftp目录文件夹下的文件和目录名
        /// </summary>
        /// <param name="dir">ftp目录（"/"为根目录）</param>
        /// <returns></returns>
        public static List<string> GetFileList(string dir)
        {
            var files = new List<string>();
            try
            {
                var request = GetRequest(WebRequestMethods.Ftp.ListDirectory);

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string line = reader.ReadLine();
                        while (line != null)
                        {
                            files.Add(line);
                            Console.WriteLine(line);
                            line = reader.ReadLine();
                        }
                        response.Close();
                    }
                }
                return files;
            }
            catch (Exception ex)
            {
                Log.Log.Error("获取ftp上面的文件和文件夹：" + ex.Message);
               return null;
            }
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="file">ip服务器下的相对路径</param>
        /// <returns>文件大小</returns>
        public static int GetFileSize(string file)
        {
            try
            {
                var request = GetRequest(WebRequestMethods.Ftp.GetFileSize);
                return (int)request.GetResponse().ContentLength;
            }
            catch (Exception ex)
            {
                Log.Log.Error("获取文件大小出错：" + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="filePath">原路径（绝对路径）包括文件名</param>
        /// <param name="objPath">目标文件夹：服务器下的相对路径 不填为根目录</param>
        public static void FileUpLoad(string srcPath, string targetPath = "")
        {
            string url = ftpPath;
            if (!targetPath.IsEmpty())
            {
                url += targetPath + "/";
            }
            FileInfo fileInfo = new FileInfo(srcPath);
            using (FileStream fs = fileInfo.OpenRead())
            {
                var request = GetRequest(WebRequestMethods.Ftp.UploadFile);
                request.KeepAlive = false;
                using (Stream stream = request.GetRequestStream())
                {
                    //设置缓冲大小
                    int BufferLength = 5120;
                    byte[] b = new byte[BufferLength];
                    int i;
                    while ((i = fs.Read(b, 0, BufferLength)) > 0)
                    {
                        stream.Write(b, 0, i);
                    }
                    Log.Log.Info("上传文件成功");
                }
            }
        }

        public static void Download(string from,string to)
        {
            try
            {
                var request = GetRequest(WebRequestMethods.Ftp.DownloadFile);
                request.UsePassive = false;
                using (var fs = new FileStream(to, FileMode.Create))
                {
                    using (var response= (FtpWebResponse)request.GetResponse())
                    {
                        using (Stream ftpStream = response.GetResponseStream())
                        {
                            long cl = response.ContentLength;
                            int bufferSize = 2048;
                            int readCount;
                            byte[] buffer = new byte[bufferSize];
                            readCount = ftpStream.Read(buffer, 0, bufferSize);
                            while (readCount > 0)
                            {
                                fs.Write(buffer, 0, readCount);
                                readCount = ftpStream.Read(buffer, 0, bufferSize);
                            }
                        }
                    }
                    fs.Flush();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Log.Error(ex.ToString());
            }
        }


    }
}
