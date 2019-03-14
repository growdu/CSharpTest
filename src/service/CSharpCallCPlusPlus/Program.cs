using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
 
namespace CSharpCallCPlusPlus
{
    class Program
    {
        [DllImport(@"C:\Users\duanys\source\repos\CSharpTest\src\moudle\CallCPlus\test\swap.dll", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public extern static void swap(ref int p,ref int q);

        static void Main(string[] args)
        {
            int a = 3;
            int b = 4;
            Console.WriteLine("{0},{1}",a,b);
            swap(ref a,ref b);
            Console.WriteLine("{0},{1}", a, b);

        }
    }
}
