using System.Web.Script.Serialization;

namespace Bobby.Libs.Text
{
    public partial class StringExtensions
    {

        public static T JsonParse<T>(this string input, T ins)
        {
            return input.IsEmpty() ? ins : ToObj<T>(input);
        }


        /// <summary>
        /// Json字符串转对象
        /// </summary>
        public static T ToObj<T>(string str)
        {

            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
                return default(T);

            try
            {
                return new JavaScriptSerializer().Deserialize<T>(str);
            }
            catch
            {
                return default(T);
            }
        }

    }
}
