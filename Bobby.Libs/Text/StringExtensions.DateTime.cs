namespace Bobby.Libs.Text
{
    using System;

    public static partial class StringExtensions
    {

        public static DateTime? PerseDate(this string s)
        {
            DateTime dateTime;
            return DateTime.TryParse(s, out dateTime) ? (DateTime?) dateTime : null;
        }
    }
}
