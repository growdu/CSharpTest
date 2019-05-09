using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "";
            DapperTest.Connect.Select(s);
        }
    }
}
