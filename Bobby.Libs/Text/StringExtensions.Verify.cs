namespace Bobby.Libs.Text
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    partial class StringExtensions
    {

        /// <summary>
        ///     判断一个字符串是否为null，空白字符或者空格字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static bool IsEmpty(this string s)
        {
            return string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        ///     判断一个字符串是否不为null，空白字符或者空格字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static bool NonEmpty(this string s)
        {
            return !s.IsEmpty();
        }

        private static readonly Regex Reg15 = new Regex(Len15IdNo);
        private static readonly Regex Reg18 = new Regex(Len18IdNo);
        private static readonly Regex RegEmail = new Regex(EmailParttern);

        private const string Len15IdNo = @"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$";
        private const string Len18IdNo = @"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$";
        private const string EmailParttern = @"^[\d\w]+[-+.]?[\d\w]+@[\w\d]+(\.\w+)+$";

        public static bool IsIdNumber(this string s)
        {
            return !s.IsEmpty() && (Reg15.IsMatch(s) || Reg18.IsMatch(s));
        }

        public static bool NonIdNumber(this string s)
        {
            return !s.IsIdNumber();
        }

        public static bool IsIpV4(this string s)
        {
            return new Regex(@"^((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))").IsMatch(s);
        }

        public static bool IsBoolean(this string s)
        {
            bool b;
            return bool.TryParse(s, out b);
        }

        public static bool ToBoolean(this string s)
        {
            bool b;
            bool.TryParse(s, out b);
            return b;
        }

        public static bool IsBoy(this string idNo)
        {
            if (idNo.NonIdNumber())
                throw new ArgumentException("idNo 非身份证号码", "idNo");

            switch (idNo.Length)
            {
                case 15:
                    return int.Parse(idNo[14].ToString()) % 2 == 1;

                case 18:
                    return int.Parse(idNo[16].ToString()) % 2 == 1;
            }
            return false;
        }


        public static bool IsGirl(this string s)
        {
            return !s.IsBoy();
        }


        public static bool NonIpV4(this string s)
        {
            return !s.IsIpV4();
        }


        public static bool IsEmail(this string s)
        {
            return RegEmail.IsMatch(s) || SimpleEmailValidate(s);
        }


        public static bool IsCnName(this string s)
        {
            return new Regex(@"^[\u4e00-\u9fa5]{2,3}$").IsMatch(s);
        }


        public static bool NonCnName(this string s)
        {
            return !s.IsCnName();
        }

        public static bool SimpleEmailValidate(this string s)
        {
            var otherChar = new[] { "@", "." };
            int outer;
            return s.Count(c => c == '@') == 1 && !otherChar.Any(c => s.StartsWith(c) || s.EndsWith(c)) && s.Any(c => c == '.') && !int.TryParse(s.Last().ToString(), out outer);
        }

        public static bool NonEmail(this string s)
        {
            return !s.IsEmail();
        }


        public static bool IsPhone(this string s)
        {
            return s.NonEmpty() && (s.Trim().Length != 11 || new Regex(@"^1\d{10}$").IsMatch(s.Trim()));
        }


        public static bool NonPhone(this string s)
        {
            return !s.IsPhone();
        }

    }

}