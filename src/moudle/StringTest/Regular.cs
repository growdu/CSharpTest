using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringTest
{
    /***********************************************************************
     * 文 件 名：Regular.cs
     * CopyRight(C) 2019-2029 https://www.github.com/growdu
     * 创 建 人：growdu
     * 创建日期：2019-04-08
     * 修 改 人：
     * 修改日期：
     * 描    述：正则表达式使用用例
     ***********************************************************************/
    class Regular
    {
        /// <summary>
        /// 匹配数字并移除
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string RemoveNum(string text)
        {
            string test = "中a3 ";
            char[] tests = test.ToCharArray();
            foreach (var c in tests)
            {
                string isnot = IsNormalCode(c) ? "is" : "not";
                Console.WriteLine(c + isnot + "normal code.");
            }
            
            Regex number = new Regex("[0-9]{4,12}");// ([^年]|[^月]|[^日]|[^个]|[^天])");
            var keys = new Regex("[0-9]{4,12}").Matches(text);
            foreach (var key in keys)
            {
                int i = text.IndexOf(key.ToString());
                char c = text[text.IndexOf(key.ToString()) + key.ToString().Length];
                if (c == '-' || c == '_' || c == '-'||key.ToString().Length>=5)
                {
                    text = text.Replace(key.ToString(), "");
                }
            }
            //bool ok = number.IsMatch(text);
            //    string t = number.Match(text).ToString();
            return text;
        }

        /// <summary>
        /// 根据关键字匹配句子
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 判断某一字符是否是中英文数字编码
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsNormalCode(char c)
        {
            Regex normal = new Regex("[A-Za-z0-9\u4e00-\u9fa5]");
            return normal.IsMatch(c.ToString());
        }
    }
}
