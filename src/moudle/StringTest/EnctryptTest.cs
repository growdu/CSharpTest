using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace StringTest
{
    class EnctryptTest
    {
        public static void Encrypt(string src="")
        {
            if (src == null || src == "")
            {
                src = "Hello,reader.";
            }
            
            // 初始化对象
            // 也可以：
            //SHA1Managed alg = new SHA1Managed();
            //MD5、SHA、SHA1、SHA256（或SHA-256）、SHA384（或SHA-384）、
            //SHA512（或SHA - 512）
            HashAlgorithm ha = HashAlgorithm.Create("SHA1");
            // 将字符串转化为字节数组
            byte[] plainData = Encoding.Default.GetBytes(src);
            // 获得摘要
            byte[] hashData = ha.ComputeHash(plainData);

            string key = "secret key";//使用秘钥加密
            byte[] keyData = Encoding.Default.GetBytes(key);
            KeyedHashAlgorithm alg = new HMACSHA1(keyData);
            byte[] hashKeyData = ha.ComputeHash(plainData);

            // 输出结果
            foreach (byte b in hashData)
            {
                Console.Write("{0:X2}\t", b);
            }
        }
        }

        public class HashAlgorithmType
        {
            public const string SHA1 = "SHA1";
            public const string SHA256 = "SHA256";
            public const string SHA384 = "SHA384";
            public const string SHA512 = "SHA512";
            public const string MD5 = "MD5";
        }
}
