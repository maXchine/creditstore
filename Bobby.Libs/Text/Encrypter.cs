namespace Bobby.Libs.Text
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// DES加密
    /// </summary>
    public static class Encrypter
    {
        #region + custom

        /// <summary>
        /// Static ctor.
        /// </summary>
        static Encrypter() { }

        // 自定义序列 键 与 向量
        private static readonly byte[] CustomKey = { 0xf8, 0xd9, 0xba, 0x9b, 0x7c, 0x5d, 0x3e, 0x1f };
        private static readonly byte[] CustomIv = { 0x0, 0xde, 0xbc, 0x9a, 0x78, 0x56, 0x34, 0x12 };

        #endregion

        #region + member

        /*
        /// <summary>
        /// 准备键值
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        private static byte[] PrepareKeys(string keys)
        {
            var keyBytes = CustomIv;
            keys.GetUTF8Bytes().Take(8).ToArray().Copyto(keyBytes, 0);

            for (var index = 0; index < CustomKey.Length; index++)
            {
                keyBytes[index] = (byte)(0xff - CustomKey[index]);
            }
            return keyBytes;
        }
        */

        /// <summary>
        /// 混淆
        /// </summary>
        /// <param name="bytes"></param>
        private static void Confuse(IList<byte> bytes)
        {
            for (var index = 0; index < bytes.Count / 2; index++)
            {
                var temp = bytes[index];
                bytes[index] = bytes[bytes.Count - 1 - index];
                bytes[bytes.Count - 1 - index] = temp;
            }
        }

        #endregion

        #region + encrypt

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="input">加密字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(this string input)
        {
            var provider = new DESCryptoServiceProvider
            {
                IV = CustomIv,
                Key = CustomKey
            };
            var bytes = Encoding.Unicode.GetBytes(input);

            Confuse(bytes);

            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, provider.CreateEncryptor(), CryptoStreamMode.Write);

            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();

            var buffer = new StringBuilder();
            foreach (var b in memoryStream.ToArray())
            {
                buffer.AppendFormat("{0:X2}", b);
            }
            return buffer.ToString();
        }

        #endregion

        #region + decrypt

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="input">解密字符串</param>
        /// <returns>解密后的原始字符串</returns>
        public static string Decrypt(this string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                return "";

            var provider = new DESCryptoServiceProvider
            {
                IV = CustomIv,
                Key = CustomKey
            };

            var len = input.Length / 2;
            var bytes = new byte[len];

            for (var index = 0; index < len; index++)
            {
                byte oneByte;
                if (byte.TryParse(input.Substring(index * 2, 2), out oneByte))
                    bytes[index] = oneByte;

                bytes[index] = (byte) Convert.ToInt32(input.Substring(index * 2, 2), 16);
            }

            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, provider.CreateDecryptor(), CryptoStreamMode.Write);

            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();

            bytes = memoryStream.ToArray();
            Confuse(bytes);

            return Encoding.Unicode.GetString(bytes);
        }

        #endregion
    }
}
