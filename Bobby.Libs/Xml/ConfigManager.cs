using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bobby.Libs.Text;

namespace Bobby.Libs.Xml
{
    public static class ConfigManager
    {

        /// <summary>
        ///     读取配置文件配置
        /// </summary>
        /// <param name="configTyp">配置类型</param>
        /// <param name="elementName">查询配置的节点名称</param>
        /// <param name="keyAttrName">查询节点的键对应的属性名称</param>
        /// <param name="valueAttrName">查询节点的值对应的属性名称</param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, string>> GetConfigXml(ConfigTypes configTyp, string elementName = "option", string keyAttrName = "key", string valueAttrName = "value")
        {

            var result = new List<KeyValuePair<string, string>>();
            var path = ConfigPaths.Global[configTyp];

            var xml = new XmlHelper(path);
            if (xml.IsInitError)
                return result;

            xml.ToDescendant(elementName);

            var key = xml.GetNowAttr(keyAttrName);
            var val = xml.GetNowAttr(valueAttrName);

            while (val.NonEmpty())
            {
                result.Add(new KeyValuePair<string, string>(key, val));
                key = xml.GetAttr(elementName, keyAttrName);
                val = xml.GetNowAttr(valueAttrName);
            }

            xml.Close();
            return result;
        }
    }
}
