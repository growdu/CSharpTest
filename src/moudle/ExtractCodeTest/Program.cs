using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ExtractCodeTest
{
    class Program
    {
        public class Test {

            public string Date{get;set;}
        }
        static void Main(string[] args)
        {
            string st = "666第1期";
            Regex a = new Regex("第.期");
            if (a.IsMatch(st))
            {
                string tt = a.Match(st).ToString();
                Console.WriteLine(st);
            }
            Regex alphabet = new Regex("[a-zA-Z]");
            char c = '即';
            if (char.IsLower(c)||char.IsUpper(c))
                Console.WriteLine(c);

            string te = @"基金a代码";
            char t = te[te.Length-1];
            if (alphabet.IsMatch(te))
                Console.WriteLine(te);
            Dictionary<char, string> dic = new Dictionary<char, string>();
            dic.Add('q', "last");
            dic.Add('a', "do");
            dic.Add('h', "do");
            dic.Add('b',"list");
            var dicSort = from objDic in dic orderby objDic.Key  select objDic;
            foreach (KeyValuePair<char, string> kvp in dicSort) {
                Console.Write(kvp.Key + "：" + kvp.Value + "\n");
            }
            string title = @"小米关于宇宙的思考";
            if (title.Contains("关于")&&title.IndexOf("关于") + 2<title.Length)
            {
                title = title.Substring(title.IndexOf("关于")+2, title.Length - title.IndexOf("关于")-2);
            }


            string path = @"2018\201811\20181130\基金公告\2018\11\30\";
            string date = GetDate(path);
            string s = ConvertDate(date);
            DateTime dateTime = Convert.ToDateTime(ConvertDate(date));
            Test test = new Test();
            if (test.Date.Length == 1)
                return;

            Console.WriteLine(date);
            Console.WriteLine(dateTime.ToString("yyyy-MM-dd"));
            Console.ReadKey();

        }

        private static string GetDate(string path)
        {
            Regex number = new Regex("[0-9]{7,8}");
            string[] dates = path.Split('\\');
            foreach (string date in dates)
            {
                if (!number.IsMatch(date))
                    continue;

                return date;
            }
            return "";
        }

        private static string ConvertDate(string date)
        {
            return date.Substring(0,4)+"-"+date.Substring(4,2)+"-"+date.Substring(6,2);
        }
    }
}
