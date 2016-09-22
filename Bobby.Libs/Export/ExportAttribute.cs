namespace Bobby.Libs {

    using System;


    /// <summary>
    ///     导出属性设置
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ExportSettingsAttribute : Attribute {

        /// <summary>
        ///     ctor.
        /// </summary>
        /// <param name="displayName">显示名称</param>
        public ExportSettingsAttribute(string displayName) {
            DisplayName = displayName;
        }


        /// <summary>
        ///     显示名称
        /// </summary>
        public string DisplayName { get; set; }


        /// <summary>
        ///     是否转换数据
        /// </summary>
        public bool NeedConvert { get; set; }


        /// <summary>
        ///     调用方法（命名空间+静态方法名称 eg: System.String.Concat）
        /// </summary>
        public string InvokeFullName { get; set; }

    }
}
