using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bobby.Libs.Xml
{
    /// <summary>
    /// 配置路径
    /// </summary>
    public class ConfigPaths
    {

        private static ConfigPaths _singleInstance;

        private static readonly IDictionary<Enum, string> DictXml;

        private static readonly object CreateInstance = new object();


        /// <summary>
        /// 初始化（请在此处添加键值关系）
        /// </summary>
        static ConfigPaths()
        {
            DictXml = new Dictionary<Enum, string>();
        }


        /// <summary>
        /// 创建全局实例
        /// </summary>
        public static ConfigPaths Global
        {
            get
            {
                // 如果已经实例化
                if (_singleInstance != null)
                    return _singleInstance;


                // 实例化锁
                lock (CreateInstance)
                {
                    // 安全判断，防止重复创建
                    if (_singleInstance == null)


                        // ReSharper disable once PossibleMultipleWriteAccessInDoubleCheckLocking
                        _singleInstance = new ConfigPaths();
                }

                return _singleInstance;
            }
        }


        /// <summary>
        /// 获取指定配置类型的配置文件地址
        /// </summary>
        /// <param name="configType">配置类型</param>
        /// <returns></returns>
        public string this[Enum configType]
        {

            get
            {
                string result;
                if (DictXml.TryGetValue(configType, out result))
                    return result;

                result = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "Config\\", configType.ToString(), ".xml").Replace("/", "\\");
                DictXml.Add(configType, result);

                return result;
            }
        }

    }
}
