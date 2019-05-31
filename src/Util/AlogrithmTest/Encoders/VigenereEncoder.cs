﻿using System;
using System.Text;

namespace Algorithms.Encoders
{
    /// <summary>
    /// Encodes using vigenere cypher--》维吉尼亚密码
    /// </summary>
    public class VigenereEncoder : IEncoder<string>
    {
        readonly CaesarEncoder caesarEncoder = new CaesarEncoder();

        /// <summary>
        /// Encodes text using specified key
        /// </summary>
        /// <param name="text">Text to be encoded</param>
        /// <param name="key">Key that will be used to encode the text</param>
        /// <returns>Encoded text</returns>
        public string Encode(string text, string key) => Cipher(text, key, caesarEncoder.Encode);

        /// <summary>
        /// Decodes text that was encoded using specified key
        /// </summary>
        /// <param name="text">Text to be decoded</param>
        /// <param name="key">Key that was used to encode the text</param>
        /// <returns>Decoded text</returns>
        public string Decode(string text, string key) => Cipher(text, key, caesarEncoder.Decode);

        private string Cipher(string text, string key, Func<string, int, string> symbolCipher)
        {
            key = AppendKey(key, text.Length);
            var encodedTextBuilder = new StringBuilder(text.Length);
            for (var i = 0; i < text.Length; i++)
            {
                if (!char.IsLetter(text[i]))
                {
                    encodedTextBuilder.Append(text[i]);
                    continue;
                }

                var letterZ = char.IsUpper(key[i]) ? 'Z' : 'z';
                var encodedSymbol = symbolCipher(text[i].ToString(), letterZ - key[i]);
                encodedTextBuilder.Append(encodedSymbol);
            }

            return encodedTextBuilder.ToString();
        }

        private string AppendKey(string key, int length)
        {
            var keyBuilder = new StringBuilder(key, length);
            while (keyBuilder.Length < length)
            {
                keyBuilder.Append(key);
            }
            return keyBuilder.ToString();
        }
    }
}
