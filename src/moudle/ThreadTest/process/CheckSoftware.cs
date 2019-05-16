using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreadTest.process
{
    /***********************************************************************
 * 文 件 名：ConsoleProgress.cs
 * CopyRight(C) 2019-2029 https://www.github.com/growdu
 * 创 建 人：growdu
 * 创建日期：2019-05-06
 * 修 改 人：
 * 修改日期：
 * 描    述：查看某个软件是否已经安装
 ***********************************************************************/
    class CheckSoftware
    {

        static void Main(string[] args)
        {
            string name = "Chrome";
            if (Check(name))
            {
                Console.WriteLine(name+" has installed.");
            }
            else
            {
                Console.WriteLine(name + " has not installed.");
            }
            Console.ReadKey();
        }

        /// <summary>
        /// 检查某个软件是否已经安装
        /// </summary>
        /// <param name="softwareName">软件名称</param>
        /// <returns>已安装返回true，否则返回false</returns>
        public static bool Check(string softwareName)
        {
            Microsoft.Win32.RegistryKey uninstallNode = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (string subKeyName in uninstallNode.GetSubKeyNames())
            {
                Microsoft.Win32.RegistryKey subKey = uninstallNode.OpenSubKey(subKeyName);
                object displayName = subKey.GetValue("DisplayName");
                if (displayName != null)
                {
                    Console.WriteLine(displayName);
                    if (displayName.ToString().Contains(softwareName))
                    {
                        return true; 
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 检查adobe的相关软件是否安装
        /// </summary>
        /// <param name="name">adobe组件名称</param>
        /// <returns></returns>
        public static bool CheckAdobe(string name)
        {
            Microsoft.Win32.RegistryKey uninstallNode = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE");
            foreach (string subKeyName in uninstallNode.GetSubKeyNames())
            {
                Microsoft.Win32.RegistryKey subKey = uninstallNode.OpenSubKey(subKeyName);
                var displayName = subKey.Name;
                //object displayName = subKey.GetValue("DisplayName");
                if (displayName != null&&displayName.Contains("Adobe"))
                {
                    foreach (string keyName in subKey.GetSubKeyNames())
                    {
                        var n = subKey.OpenSubKey(keyName);
                        if (n.Name.Contains(name))
                            return true;
                    }
                }
            }
            return Check(name);
        }

    }
}
