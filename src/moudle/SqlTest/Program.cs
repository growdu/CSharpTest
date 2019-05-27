using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SqlTest
{
    class Program
    {
        static void Main(string[] args)
        {
           DataTable dt= MySql.GetDataTable();
            string s = "";
            DapperTest.Connect.Select(s);
        }
    }
}
