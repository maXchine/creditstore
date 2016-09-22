namespace Bobby.Libs.Xml
{
    using System.Configuration;
    using Text;

    /// <summary>
    /// 程序配置相关
    /// </summary>
    public static class AppSettings
    {

        /// <summary>
        /// 读取程序配置的文本,当没有配置相关键时以默认值返回
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetSettings(string key, string defaultValue)
        {
            var val = ConfigurationManager.AppSettings[key];
            return val.IsEmpty() ? defaultValue : val;
        }

        /// <summary>
        /// 读取程序配置的布尔值,当没有配置相关键时以默认值返回
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static bool GetSettings(string key, bool defaultValue)
        {
            var val = ConfigurationManager.AppSettings[key];
            bool result;
            return val.IsEmpty() && bool.TryParse(val, out result) ? result : defaultValue;
        }
        
        /// <summary>
        /// 读取程序配置的整型数值,当没有配置相关键时以默认值返回
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetSettings(string key, int defaultValue)
        {
            var val = ConfigurationManager.AppSettings[key];
            int outer;
            return int.TryParse(val, out outer) ? outer : defaultValue;
        }

    }
}
