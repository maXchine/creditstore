namespace Bobby.Libs.Config
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web.Script.Serialization;

    using Text;

    /// <summary>
    /// 1,读取配置
    /// 2,缓存配置
    /// 3,系统调用配置数据
    /// 4,写入配置
    /// 5,初始化配置
    /// </summary>
    public abstract class Configs
    {

        public static readonly SystemTips SystemTips = new SystemTips();

        /// <summary>
        /// 读取.json 配置文件
        /// </summary>
        /// <param name="path">文件路径</param>
        public static T ReadToJson<T>(string path) where T : IConfigs
        {
            if (path.IsEmpty())
                throw new ArgumentNullException("path", "Error path");

            var serializer = new JavaScriptSerializer();
            var jsonConfig = ReadJson(path);
            var result = serializer.Deserialize<T>(jsonConfig);
            return result;
        }

        /// <summary>
        /// 保存配置,将配置写入文件.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config">配置对象</param>
        /// <param name="savePath">保存路径</param>
        public static void SaveConfig<T>(T config, string savePath) where T : IConfigs
        {
            if (savePath.IsEmpty())
                savePath = BuildPath(config);

            var bytes = SerializeToBytes(config);
            var stream = File.Exists(savePath) ? File.OpenWrite(savePath) : File.Create(savePath);

            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            stream.Dispose();

        }

        /// <summary>
        /// 构建配置文件保存路径
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config">配置对象</param>
        /// <returns></returns>
        private static string BuildPath<T>(T config) where T : IConfigs
        {
            var typeName = (typeof(T)).FullName.Replace(".", "\\");
            return string.Concat(AppDomain.CurrentDomain.BaseDirectory, "\\Configs\\", typeName);
        }

        /// <summary>
        /// 序列化对象为字节数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <returns></returns>
        private static byte[] SerializeToBytes<T>(T config) where T : IConfigs
        {
            return Encoding.UTF8.GetBytes(SerializeToJson(config));
        }

        private static string SerializeToJson<T>(T config) where T : IConfigs
        {
            return (new JavaScriptSerializer()).Serialize(config);
        }

        /// <summary>
        /// 读取文件数据
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string ReadJson(string path)
        {
            var file = File.OpenRead(path);
            var bytes = file.ReadAllBytes().ToArray();

            file.Close();
            file.Dispose();


            var json = Encoding.UTF8.GetString(bytes);

            return json;
        }
    }
}
