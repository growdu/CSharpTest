using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools.DataHelper;

namespace StringTest
{
    class StringCompare
    {
        static void Main()
        {
            var s =new  StringCompare();
            s.Test();
        }

        public void Test()
        {
            var ts = new List<string>() { "SEQUENCE_DATA_001_20190517092634.csv", "SEQUENCE_DATA_20190527191452.csv",
            "SEQUENCE_DATA_20190517095747.csv","SEQUENCE_DATA_004_20190517092634.csv"};
            ts.Sort(Compare);
        }

        public  int Compare(string s1,string s2)
        {
            int n = 0;
           
            try
            {
                s1 = s1.Substring(0, s1.LastIndexOf("."));
                s2 = s2.Substring(0, s2.LastIndexOf("."));
                string[] t1 = s1.Split('_');
                t1 = t1.Reverse().ToArray();
                string[] t2 = s2.Split('_');
                t2 = t2.Reverse().ToArray();
                n = Compare1(t1[0],t2[0]);
                if (n == 0)
                    return Compare1(t1[1],t2[1]);

                return n;
            }
            catch(Exception ex)
            {
                ;
            }
          
            return 0;
        }

        public int Compare1(string s1, string s2)
        {
            if (double.Parse(s1) > double.Parse(s2))
                return 1;

            else if (double.Parse(s1) < double.Parse(s2))
                return -1;

            return 0;
        }

    }
}
