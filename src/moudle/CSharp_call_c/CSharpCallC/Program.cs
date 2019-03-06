using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CSharpCallC
{
    class Program
    {
        [DllImport(@"C:\Users\duanys\source\repos\CSharpTest\src\moudle\CSharp_call_c\test.dll", CharSet = CharSet.Unicode)]
        public unsafe static extern void swap(int* a, int* b);

        static void Main(string[] args)
        {
            int a = 1;
            int b = 2;
            Console.WriteLine($"Before Swap a={a},b={b}");
            unsafe
            {
                swap(&a, &b);
            }
            Console.WriteLine($"After Swap a={a},b={b}");
            Console.WriteLine("\n");
            Console.ReadKey();
        }
    }
}
