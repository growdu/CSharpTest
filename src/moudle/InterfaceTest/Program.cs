using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceTest
{
 /***********************************************************************
* 文 件 名：
* CopyRight(C) 2019-2029 https://www.github.com/growdu
* 创 建 人：growdu
* 创建日期：2019-04-24
* 修 改 人：
* 修改日期：
* 描    述：接口测试，测试类和接口互换
***********************************************************************/
    class Program
    {
        static void Main(string[] args)
        {
            Interface1 hello = new Hello();
            hello.Hello();
            Console.ReadKey();
        }
    }

    public class Hello : Interface1
    {
        void Interface1.Hello()
        {
            Console.WriteLine("Hello.");
        }
    }

    public class SortHelper<T> where T : IComparable
    {

    }

    public class SortHelper2
    {
        public void Sort<T>(T[] array) where T : IComparable
        {

        }
    }

}
