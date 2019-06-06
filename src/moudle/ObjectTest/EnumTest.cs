using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectTest
{
    class EnumTest
    {
        public enum Test
        {
            ok=1,
            fail,
            error,
            exit,
        }

        public static void EnumToStringTest()
        {
            Console.WriteLine(Test.ok.ToString());
        }

        public static void EnumEqualIntTest()
        {
            if ((int)Test.fail == 2)
            {
                Console.WriteLine(Test.ok.ToString());
            }
        }

    }
}
