using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringTest
{
    /// <summary>
    /// 这个类主要是关于字符串的取值改变的测试
    /// </summary>
    class StringAndChar
    {
        public static void ToUp()
        {
            string test = "我是一个a";
            Console.WriteLine("改变前：" + test);
            test = test.ToUpper();
            Console.WriteLine("改变后："+test);
        }

    }
}
