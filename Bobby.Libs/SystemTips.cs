using Bobby.Libs.Config;

namespace Bobby.Libs
{

    using Text;
    using System.Configuration;


    /// <summary>
    /// 系统提示信息
    /// </summary>
    public class SystemTips : IConfigs
    {
        public static SystemTips BaseConfig { get; private set; }
        public void SetValue()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 当用户操作有误时的提醒信息
        /// </summary>
        public string ErrorOption
        {
            get { return _errorOption.IsEmpty() ? "错误的操作" : _errorOption; }
        }

        private string _errorOption;

        public string ServerError
        {
            get { return _serverError.IsEmpty() ? "系统错误" : _serverError; }
        }

        private string _serverError = "";

        /// <summary>
        /// 操作失败
        /// </summary>
        public string OperateFailed
        {
            get { return _operateFailed.IsEmpty() ? "操作失败" : _operateFailed; }
            private set { _operateFailed = value; }
        }
        private string _operateFailed = "";


        public string OperateSuccess
        {
            get { return _operateSuccess.IsEmpty() ? "操作成功" : _operateSuccess; }
        }
        private string _operateSuccess = "";
    }
}
