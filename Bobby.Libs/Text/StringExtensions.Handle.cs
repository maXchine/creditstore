using System.Text;
using System.Text.RegularExpressions;

namespace Bobby.Libs.Text {
    public static partial class StringExtensions {

        public static string RemoveHtmlTags(this string htmlString) {
            //\<\w+ *(\w+=\"[^"]*\" *)*\>|\</\w+\>|\<\w+/\>
            //<font color="#FF0000"><strong>活动说明 该项目为投资换Iphone6S活动项目。本活动起始时间为9月25日，每位用户在活动期限内投资有“Iphone专享”标志的活动融资包，一次性投资3万元即可获得Iphone6S/16G手机一部。详见<a href="https://www.cnsmefs.com">www.cnsmefs.com</a>，了解具体活动详情。</strong></font><br/> 产品介绍： 益智投E是中益信推出的对全额本息保障的借款项目进行自动投资及定时自动转让的理财产品，锁定期为24个月，年化利率为6%。其采用智能投标的方式，提高资金的流动率和利用率，从而增加投资人的实际收益。投资人通过益智投E理财产品将资金出借给中益信平台上的借款客户，出借的本金及利息将在锁定期限届满后一次性返还至其汇付天下账户，投资人一旦申请该理财产品后，将无法进行取消，申请期限结束后，系统将自动匹配投资项目。<br/> 投资范围：由合作机构提供全额本息担保的借款项目，包括信用、担保、抵质押等类型。<br/> 产品优势：（1）收益佳：预期年化收益率6 %；（2）期限灵活：锁定期为24个月；（3）申请门槛：起投金额为30,000元；（4）全托管：一站式理财服务，自动投标。<br/>
            var regex = new Regex("\\<\\w+ *(\\w+=\\\"[^\"]*\\\" *)*\\>|\\</\\w+\\>|\\<\\w+/\\>");
            return regex.Replace(htmlString, "");
        }

        public static string HiddenPart(this string s, int showLen = 1, string replaceString = "**", string defaultDisplay = "----") {

            if (s.IsEmpty())
                return defaultDisplay;

            if (s.Length == 2)
                return s[0] + replaceString;

            if (s.Length < showLen)
                return string.Concat(s, replaceString, replaceString);

            var builder = new StringBuilder();
            for (var i = 0; i < s.Length; i++)
                if (i <= showLen - 1)
                    builder.Append(s[i]);
                else
                    builder.Append(replaceString);

            return builder.ToString();
        }


        public static string HiddenLeft(this string s, int showLeftLen = 4, string replaceString = "*", string defaultDisplay = "----") {

            if (s.IsEmpty())
                return defaultDisplay;

            if (s.Length == 2)
                return s[0] + replaceString;

            if (s.Length < showLeftLen)
                return string.Concat(s, replaceString, replaceString);

            var builder = new StringBuilder();
            for (var i = 0; i < s.Length; i++)
                if (i >= s.Length - showLeftLen)
                    builder.Append(s[i]);
                else
                    builder.Append(replaceString);

            return builder.ToString();
        }


        public static string ShowRight(this string s, int showLen = 1, char hideChar = '*') {
            if (s.IsEmpty())
                return s;

            if (s.Length <= 1)
                return Regex.Replace(s, @".", hideChar.ToString());

            var hideLen = s.Length - showLen;
            var showPart = s.Substring(hideLen, showLen);

            return string.Concat(hideChar.RepeateChar(hideLen), showPart);
        }


        public static string HiddenMobileNumber(this string s, byte hideLen = 4, char hideChar = '*') {

            if (s.IsEmpty() || hideLen <= 0)
                return s;

            if (hideLen > 10)
                hideLen = 10;

            return string.Concat(s.Substring(0, 3), '*'.RepeateChar(hideLen), s.Substring(hideLen + 2, 8 - hideLen));
        }


        private static string RepeateChar(this char c, int repeateTimes) {
            var builder = new StringBuilder();
            for (var i = 0; i < repeateTimes; i++) {
                builder.Append(c);
            }
            return builder.ToString();
        }


        public static string BankLeft4(this string s, string tip = "****") {
            if (s.IsEmpty())
                return "";
            return "{1}{0}".FormatArgs(s.Substring(s.Length - 4), tip);
        }
    }
}
