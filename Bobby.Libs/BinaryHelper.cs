namespace Bobby.Libs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;


    public static class Exporter
    {

        // 获取值显示的调用方法
        private static MethodInfo GetCall(ExportSettingsAttribute customInfo)
        {
            if (!customInfo.NeedConvert)
                return null;

            var invokeFullName = customInfo.InvokeFullName;

            var splitIndex = invokeFullName.LastIndexOf('.');
            var iNamespace = invokeFullName.Substring(0, splitIndex);
            var methodName = invokeFullName.Substring(splitIndex + 1);

            var assembly = Assembly.Load("ZYX.P2P.Web.Core");
            var typeInfo = assembly.GetType(iNamespace);
            var methodInfo = typeInfo.GetMethod(methodName);

            return methodInfo;
        }




        // 获取属性值
        private static IEnumerable<object> GetData(object instance, IEnumerable<ExportPropertyInfo> propertyInfo)
        {

            foreach (var epi in propertyInfo)
            {
                var val = epi.ProperyInfo.GetValue(instance);

                if (epi.NeedConvert && epi.TheCall != null)
                    val = epi.TheCall.Invoke(null, new[] { val });

                if (val is decimal)
                    val = ((decimal) val).ToString("#0.00");

                if (val is DateTime)
                    val = ((DateTime) val).ToString("yyyy-MM-dd HH:mm:ss");

                yield return string.Concat(val, "\t");
            }
        }


        // 反射并记录属性自定义信息
        private static ExportPropertyInfo GetPropertyExportInfo(PropertyInfo propertyInfo)
        {
            var matchType = typeof(ExportSettingsAttribute);
            return
                propertyInfo.GetCustomAttributes(matchType)
                    .OfType<ExportSettingsAttribute>()
                    .Select(customInfo => new ExportPropertyInfo
                    {
                        DisplayName = customInfo.DisplayName,
                        NeedConvert = customInfo.NeedConvert,
                        TheCall = GetCall(customInfo),
                        ProperyInfo = propertyInfo
                    }).FirstOrDefault();
        }

        /// <summary>
        ///     转换属性值为 Excel 字符串格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instanceList"></param>
        /// <returns></returns>
        public static byte[] Target<T>(List<T> instanceList)
        {
            var builder = new StringBuilder();

            var type = typeof(T);
            var properties = type.GetProperties();

            var propertyCustomInfos = properties.Select(GetPropertyExportInfo).Where(m => m != null).ToList();
            var columeName = propertyCustomInfos.Select(t => t.DisplayName).ToList();

            builder.AppendLine(string.Join(",", columeName));

            var count = instanceList.Count;
            for (var i = 0; i < count; i++)
            {
                var rowData = GetData(instanceList[i], propertyCustomInfos);
                builder.AppendLine(string.Join(",", rowData));
            }

            return Encoding.GetEncoding("gb2312").GetBytes(builder.ToString());
        }
    }
}
