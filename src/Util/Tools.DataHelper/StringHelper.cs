using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Tools.DataHelper
{
    public static  partial class StringHelper
    {
        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string input)
        {
            return (input == null || input.Length == 0 || input.Trim() == "" || string.IsNullOrEmpty(input) || input == "　" || input == " ");
        }

        /// <summary>
        /// 字符串转全角
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 转半角(DBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        public static string ToDBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 用正则表达式匹配字符串中的字符，匹配到返回true，否则返回true
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsMatch(this string input, string pattern)
        {
            if (input == null||pattern==null)
                return false;

            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// 匹配该字符串中符合正则表达式的第一个子串
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        public static string Match(this string input, string pattern)
        {
            if (input == null || pattern == null)
                return "";

            return Regex.Match(input, pattern).Value;
        }

        /// <summary>
        /// 判断字符串是否是整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsInt(this string input)
        {
            int i;
            return int.TryParse(input, out i);
        }

        /// <summary>
        /// 将字符转换为整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int ToInt(this string input)
        {
            return int.Parse(input);
        }

        /// <summary>
        /// 返回字符串中的文件名（不包含路径）
        /// </summary>
        /// <param name="input">文件路径名</param>
        /// <param name="hasSuffix">是否包含文件后缀名</param>
        /// <returns></returns>
        public static string GetFileName(this string input,bool hasSuffix=true)
        {
            int index = input.LastIndexOf("\\");
            if (index == -1||index<2)
                return input;

            if(hasSuffix)
                return input.Substring(index + 1,input.Length-index-1) ;

            return input.Substring(index + 1, input.LastIndexOf('.')- index - 1);
        }

        /// <summary>
        /// 转换为decimal
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToDecimal(this string input)
        {
            decimal dData = 0.0M;
            if (input.Contains("E"))
            {
                dData = Convert.ToDecimal(Decimal.Parse(input.ToString(), System.Globalization.NumberStyles.Float));
            }
            return dData.ToString();
        }

    }
}
