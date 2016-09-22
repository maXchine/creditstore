
namespace Bobby.Libs {

    using System.Reflection;

    /// <summary>
    ///     简易属性信息
    /// </summary>
    public class ExportPropertyInfo {

        /// <summary>
        ///     显示名称
        /// </summary>
        public string DisplayName { get; set; }


        /// <summary>
        ///     是否转换数据
        /// </summary>
        public bool NeedConvert { get; set; }


        /// <summary>
        ///     属性信息
        /// </summary>
        public PropertyInfo ProperyInfo { get; set; }


        /// <summary>
        ///     转换方法
        /// </summary>
        public MethodInfo TheCall { get; set; }

        /// <summary>
        ///     是否忽略此属性
        /// </summary>
        public bool IgnoreItem { get; set; }

    }
}
