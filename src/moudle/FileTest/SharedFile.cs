using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FileTest
{
    /// <summary>
    /// 访问共享目录
    /// </summary>
    public class SharedFile:IDisposable
    {
        // obtains user token     
        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool LogonUser(string pszUsername, string pszDomain, string pszPassword,
          int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        // closes open handes returned by LogonUser     
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        extern static bool CloseHandle(IntPtr handle);

        [DllImport("Advapi32.DLL")]
        static extern bool ImpersonateLoggedOnUser(IntPtr hToken);

        [DllImport("Advapi32.DLL")]
        static extern bool RevertToSelf();
        const int LOGON32_PROVIDER_DEFAULT = 0;
        const int LOGON32_LOGON_NEWCREDENTIALS = 9;//域控中的需要用:Interactive = 2     
        private bool disposed;

        static void Main(string[] args)
        {
            string selectPath = @"\\10.3.2.19\指数数据备份\天相指数\";
            SharedFile.CheckSharedFile("", "", selectPath);
            var dicInfo = new DirectoryInfo(selectPath);//选择的目录信息  

            DirectoryInfo[] dic = dicInfo.GetDirectories("*.*", SearchOption.TopDirectoryOnly);
            foreach (DirectoryInfo temp in dic)
            {
                Console.WriteLine(temp.FullName);
            }

            Console.WriteLine("---------------------------");
            FileInfo[] textFiles = dicInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly);//获取所有目录包含子目录下的文件  
            foreach (FileInfo temp in textFiles)
            {
                Console.WriteLine(temp.Name);
            }
            Console.ReadKey();
        }



        public  SharedFile(string username, string password, string ip)
        {
            // initialize tokens     
            IntPtr pExistingTokenHandle = new IntPtr(0);
            IntPtr pDuplicateTokenHandle = new IntPtr(0);

            try
            {
                // get handle to token     
                bool bImpersonated = LogonUser(username, ip, password,
                  LOGON32_LOGON_NEWCREDENTIALS, LOGON32_PROVIDER_DEFAULT, ref pExistingTokenHandle);

                if (bImpersonated)
                {
                    if (!ImpersonateLoggedOnUser(pExistingTokenHandle))
                    {
                        int nErrorCode = Marshal.GetLastWin32Error();
                        throw new Exception("ImpersonateLoggedOnUser error;Code=" + nErrorCode);
                    }
                }
                else
                {
                    int nErrorCode = Marshal.GetLastWin32Error();
                    throw new Exception("LogonUser error;Code=" + nErrorCode);
                }
            }
            finally
            {
                // close handle(s)     
                if (pExistingTokenHandle != IntPtr.Zero)
                    CloseHandle(pExistingTokenHandle);
                if (pDuplicateTokenHandle != IntPtr.Zero)
                    CloseHandle(pDuplicateTokenHandle);
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                RevertToSelf();
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public static void CheckSharedFile(string userName, string password, string selectPath)
        {
            string error = "";
            try
            {
                SharedFile _dir = new SharedFile(userName, password, selectPath);
                var dirs = new DirectoryInfo(selectPath);//选择的目录信息    
                DirectoryInfo[] ds = dirs.GetDirectories("*.*", SearchOption.TopDirectoryOnly);
                //error = (Utils.IsEmpty(_dir.Error)) ? "" : _dir.Error;
                if (ds.Length == 0)
                {
                    error = selectPath + "，共享目录无法访问.";
                }
                //Utils.WriteTxtFile(selectPath + @"pdf\A股\a.txt", "检测共享目录：" + selectPath, true);
            }
            catch (Exception ex)
            {
                error = "检测共享目录异常：" + ex.Message + ex.StackTrace + "\n" + selectPath + "\n" + userName + "\n" + password;
            }
        }

    }
}
