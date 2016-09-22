namespace Bobby.Libs.Text
{
    using System;
    using System.Text;

    partial class StringExtensions
    {

        /// <summary>
        ///     获取字符串字节长度
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int GetGb2312BytesLength(this string s)
        {
            return s.GetBytes().Length;
        }

        /// <summary>
        ///     获取字符串字节长度
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="name">解码名称</param>
        /// <exception cref="System.ArgumentException">传入name不为内置解码，则异常。</exception>
        /// <returns></returns>
        public static byte[] GetBytes(this string s, string name = "gb2312")
        {
            return Encoding.GetEncoding(name).GetBytes(s);
        }

        public static string ToBase64(this string s)
        {
            return Convert.ToBase64String(s.GetBytes());
        }

        public static string FromBase64(this string s)
        {
            return Encoding.GetEncoding("gb2312").GetString(Convert.FromBase64String(s));
        }

    }

}