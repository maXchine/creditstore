namespace Bobby.Libs.Text
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;


    public static partial class StringExtensions
    {

        public static string FormatArgs(this string s, params object[] parms)
        {
            return string.Format(s, parms);
        }

        // 将字符串转移为ASCII码
        public static string getCnASCII(string cnStr)
        {
            var strBuf = new StringBuilder();
            var bGBK = Encoding.Default.GetBytes(cnStr);
            for (var i = 0; i < bGBK.Length; i++)
            {
                // System.out.println(Integer.toHexString(bGBK[i]&0xff));
                strBuf.Append((bGBK[i] & 0xff).ToString("X"));
            }
            return strBuf.ToString();
        }

        /**
         * 银行卡号码、身份证号码、手机号码、邮箱地址等信息处理
         * 
         * @param cardNo
         * @param prefix
         * @param suffix
         * @return
         */

        public static string getSecurityNum(string cardNo, int prefix, int suffix)
        {
            var cardNoBuffer = new StringBuilder();
            var len = prefix + suffix;
            if (StringUtils.isNotBlank(cardNo) && cardNo.Length > len)
            {
                cardNoBuffer.Append(cardNo.Substring(0, prefix));
                for (var i = 0; i < cardNo.Length - len; i++)
                {
                    cardNoBuffer.Append("*");
                }
                cardNoBuffer.Append(cardNo.Substring(cardNo.Length - suffix, suffix));
            }
            return cardNoBuffer.ToString();
        }

        public static void main(string[] args)
        {
            //System.out.println(getSecurityNum("6216260000009477603", 4, 4));
            //System.out.println(isTelPhone("(025)2861774")); 
            Console.WriteLine(isMobilePhone("18312507671"));
        }

        /**
         * 验证电话号码和手机号
         * 
         * @param phone
         * @return
         */

        public static bool isTelPhone(string phone)
        {
            var p =
                "^([1][3][0-9]{9})|([1][4][0-9]{9})|([1][5][0-9]{9})|([1][8][0-9]{9})|(0[1][3][0-9]{9})|([0-9]{3,4}\\-[0-9]{7,8})|([0-9]{7,8})|(\\([0-9]{3,4}\\)[0-9]{3,8})|(0{0,1}13[0-9]{9})$";
            return Regex.IsMatch(phone, p);
        }

        /**
         * 验证手机号
         * 
         * @param phone
         * @return
         */

        public static bool isMobilePhone(string phone)
        {
            var p = "^([1][3][0-9]{9})|([1][4][0-9]{9})|([1][5][0-9]{9})|([1][8][0-9]{9})$";
            return Regex.IsMatch(phone, p);
        }

        /**
         * 验证身份证号码
         * 
         * @param idCardNo
         * @return
         */

        public static bool isIdCardNo(string idCardNo)
        {
            var p = "^[1-9]\\d{13,16}[a-zA-Z0-9]{1}$";
            return Regex.IsMatch(idCardNo, p);
        }

        /**
         * 验证邮箱
         * 
         * @param idCardNo
         * @return
         */

        public static bool isEmail(string email)
        {
            var p = @"[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})";
            return Regex.IsMatch(email, p);
        }

    }


    public static class StringUtils
    {

        public static bool isNotBlank(string cardNo)
        {
            return !string.IsNullOrEmpty(cardNo) && !string.IsNullOrWhiteSpace(cardNo) && cardNo.Length > 0;
        }

        public static bool isBlank(string cardNo)
        {
            return string.IsNullOrEmpty(cardNo) || string.IsNullOrWhiteSpace(cardNo);
        }

        public static bool equalsIgnoreCase(this string eqStr, string cmpStr)
        {
            return eqStr.Equals(cmpStr, StringComparison.OrdinalIgnoreCase);
        }

        public static bool isNumericSpace(string excitationType)
        {
            return Regex.IsMatch(excitationType, "^[0-9 ]*$");
        }

        public static bool isAlphaNumericWithSpaceAndDot(string excitationType)
        {
            return Regex.IsMatch(excitationType, "^[a-zA-Z0-9 .]*$");
        }

        public static bool isNumberic(this string _string)
        {
            if (string.IsNullOrEmpty(_string))
                return false;
            foreach (var c in _string)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }


        public static string SimpleCardNo(this string cardNo)
        {

            if (cardNo.IsEmpty())
            {
                return "";
            }

            var builder = new StringBuilder();
            for (var i = 0; i < cardNo.Length; i++)
            {
                builder.Append(cardNo[i]);
                if (i % 4 == 3)
                    builder.Append(" ");
            }
            return builder.ToString();
        }
    }

}