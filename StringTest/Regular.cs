using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringTest
{
    class Regular
    {
        public string RemoveNum(string text)
        {
            Regex number = new Regex("[0-9]{4,12}");// ([^年]|[^月]|[^日]|[^个]|[^天])");
            var keys = new Regex("[0-9]{4,12}").Matches(text);
            foreach (var key in keys)
            {
                char c = text[text.IndexOf(key.ToString()) + key.ToString().Length];
                if (c == '-' || c == '_' || c == '-'||key.ToString().Length>=5)
                {
                    text = text.Replace(key.ToString(), "");
                }
            }
            bool ok = number.IsMatch(text);
                string t = number.Match(text).ToString();
            return t;
        }

        public static string GetTitle(string text)
        {
            string title = "";
            string[] keys = new string[] { "公告", "摘要", "报告", "说明书", "(更新)", "（更新）", "招募书" };
            foreach (string key in keys)
            {
                if (!text.Contains(key))
                    continue;


                title = text.Substring(0, text.IndexOf(key))+key;
            }
            return title;
        }

    }
}
