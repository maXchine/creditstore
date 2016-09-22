namespace Bobby.Libs.Text
{

    public static partial class StringExtensions
    {

        /// <summary>
        /// 字符是否为十进制类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsDecimal(this string s)
        {
            decimal result;
            return decimal.TryParse(s, out result);
        }


        /// <summary>
        /// 字符是否不为十进制类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool NonDecimal(this string s)
        {
            return !s.IsDecimal();
        }

        /// <summary>
        /// 将字符串转换为十进制类型
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="defaultValue">转换失败后以此值作为返回值</param>
        /// <returns>成功转换字符串后的数值或者失败时调用制定的返回值</returns>
        public static decimal ParseDecimal(this string s, decimal defaultValue)
        {
            decimal outer;
            return decimal.TryParse(s, out outer) ? outer : defaultValue;
        }

        public static decimal ToDecimal(this string s)
        {
            decimal d;
            decimal.TryParse(s, out d);
            return d;
        }

        /// <summary>
        /// 尝试将字符串转换为十进制类型
        /// </summary>
        /// <param name="s"></param>
        /// <param name="outer"></param>
        /// <returns></returns>
        public static bool TryParseDecimal(this string s, out decimal outer)
        {
            return decimal.TryParse(s, out outer);
        }

        /// <summary>
        /// 将一个对象转换成十进制类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal? AsDecimal(this string value)
        {
            decimal decimalValue;
            if (value != null && decimal.TryParse(value, out decimalValue))
            {
                return decimalValue;
            }
            return null;
        }


        /// <summary>
        /// 将一个对象转换成2位小数的十进制数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal AsDecimal2(this decimal value)
        {
            return decimal.Parse(value.ToString("#0.00"));
        }

    }

}