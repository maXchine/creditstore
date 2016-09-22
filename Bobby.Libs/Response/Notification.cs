namespace Bobby.Libs.Response {


    /// <summary>
    ///     消息实体
    /// </summary>
    public class Notification {

        /// <summary>
        ///     消息內容
        /// </summary>
        public string Message { get; set; }


        /// <summary>
        ///     成功标志
        /// </summary>
        public bool Result { get; set; }


        /// <summary>
        ///     操作标志
        /// </summary>
        public HandleResult Flag { get; set; }
    }
}
