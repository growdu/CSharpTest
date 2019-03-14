using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTest
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            Console.WriteLine("This is debug");
#else
            Console.WriteLine("This is realse");
#endif
            Console.ReadKey();
        }
    }
}
