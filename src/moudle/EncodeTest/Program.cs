using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EncodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Code code = new Code();
            code.ToUTF8("将字符编码转换为UTF-8。");
            string src = @"¹É¶«´ó»á³öÏ¯";
            var bytes = Encoding.Unicode.GetBytes(src);
            foreach (var b in bytes)
            {
                Console.WriteLine(String.Format("\\u{0:X2}", b));
            }
            string test = Encoding.Unicode.GetString(bytes);
            Console.WriteLine(test);
            Console.ReadKey();
        }
    }

    public class Code
    {
        public string ToUTF8(string text)
        {
            Encoding utf8 = Encoding.UTF8;
            Encoding strGb2312 = Encoding.GetEncoding("GB2312");
            text = TransferStr(text, strGb2312, utf8);
            Console.WriteLine(text);
            return text;
        }

        /// <summary>
        /// 将字符串转换为Unicode字节
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string String2Unicode(string source)
        {
            var bytes = Encoding.Unicode.GetBytes(source);
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < bytes.Length; i += 2)
            {
                stringBuilder.AppendFormat("\\u{0:x2}{1:x2}", bytes[i + 1], bytes[i]);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 将一种字符串的编码格式转换为另一种
        /// </summary>
        /// <param name="str">需转换的字符串</param>
        /// <param name="originalEncode">原编码格式</param>
        /// <param name="targetEncode">目标编码格式</param>
        /// <returns></returns>
        private string TransferStr(string str, Encoding originalEncode, Encoding targetEncode)
        {
            try
            {
                byte[] unicodeBytes = originalEncode.GetBytes(str);
                byte[] asciiBytes = Encoding.Convert(originalEncode, targetEncode, unicodeBytes);
                char[] asciiChars = new char[targetEncode.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
                targetEncode.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
                string result = new string(asciiChars);
                return result;
            }
            catch
            {
                Console.WriteLine("There is an exception.");
                return "";
            }
        }


        private string CreateToken(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }


    }
}
