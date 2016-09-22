namespace Bobby.Libs.Response {

    /// <summary>
    ///     处理结果
    /// </summary>
    public enum HandleResult {

        /// <summary>
        ///     失败
        /// </summary>
        Failed = 0,

        /// <summary>
        ///     成功
        /// </summary>
        Completed = 1,

        /// <summary>
        ///     服务器内部错误
        /// </summary>
        Error = 2,

        /// <summary>
        ///     不可访问、无授权访问、登录超时
        /// </summary>
        Timeout = 3,

        /// <summary>
        ///     执行脚本
        /// </summary>
        Script = 4

    }
}
