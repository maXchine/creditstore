using Bobby.Libs.Config;

namespace Bobby.Libs.Web
{

    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Web.Mvc;

    using Response;

    /// <summary>
    /// 基础控制器
    /// </summary>
    public class BobbyController : Controller
    {



        /// <summary>
        /// 请求表单
        /// </summary>
        protected NameValueCollection Forms
        {
            get { return Request.Form; }
        }


        /// <summary>
        /// 请求查询字符串
        /// </summary>
        protected NameValueCollection QueryString
        {
            get { return Request.QueryString; }
        }


        /// <summary>
        /// 通知页
        /// </summary>
        /// <returns></returns>
        protected ActionResult Notification(Notification notification, string actionName = "Message", string controllerName = "Default", object routeValue = null)
        {

            TempData[Constants.Message] = notification;
            return RedirectToAction(actionName, controllerName, routeValue);
        }


        /// <summary>
        /// 通知页
        /// </summary>
        /// <returns></returns>
        protected ActionResult Notification(string message = "您的操作有误", HandleResult result = HandleResult.Error)
        {
            return Notification(new Notification
            {
                Message = message,
                Flag = result
            }, routeValue: new { area = "Home" });
        }


        /// <summary>
        /// 表单单键多值分割
        /// </summary>
        /// <param name="name">键</param>
        /// <returns></returns>
        protected string[] FormArray(string name)
        {
            return Forms[name].Split(',');
        }


        /// <summary>
        /// 登录超时
        /// </summary>
        /// <returns></returns>
        protected JsonResult ProcessSignTimeout(string message = "登录超时，请重新登录", JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            return ProcessResult(HandleResult.Timeout, message, behavior: behavior);
        }


        /// <summary>
        /// 处理成功
        /// </summary>
        /// <param name="msg">返回消息</param>
        /// <param name="data">返回数据</param>
        /// <param name="behavior">指定是否允许来自客户端的 HTTP GET 请求。</param>
        /// <returns></returns>
        protected JsonResult ProcessCompleted(string msg = null, object data = null, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            if (msg == null)
                msg = Configs.SystemTips.OperateSuccess;

            return ProcessResult(HandleResult.Completed, msg, data, behavior);
        }


        /// <summary>
        /// 处理失败
        /// </summary>
        /// <param name="msg">返回消息</param>
        /// <param name="data">返回数据</param>
        /// <param name="behavior">指定是否允许来自客户端的 HTTP GET 请求。</param>
        /// <returns></returns>
        protected JsonResult ProcessFaild(string msg = null, object data = null, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            if (msg == null)
                msg = Configs.SystemTips.OperateFailed;

            return ProcessResult(HandleResult.Failed, msg, data, behavior);
        }


        /// <summary>
        /// 处理失败
        /// </summary>
        /// <param name="msg">返回消息</param>
        /// <param name="data">返回数据</param>
        /// <param name="behavior">指定是否允许来自客户端的 HTTP GET 请求。</param>
        /// <returns></returns>
        protected JsonResult ProcessErrored(string msg = null, object data = null, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            if (msg == null)
                msg = Configs.SystemTips.ServerError;

            return ProcessResult(HandleResult.Error, msg, data, behavior);
        }


        /// <summary>
        /// 处理结果
        /// </summary>
        /// <param name="handleResult"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
        protected JsonResult ProcessResult(HandleResult handleResult, string message, object data = null, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            return Json(new { status = handleResult, message, data }, behavior);
        }


        /// <summary>
        /// 到处数据到简易Excel表格
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="fileDownloadName"></param>
        /// <returns></returns>
        protected FileResult Export<T>(List<T> source, string fileDownloadName)
        {

            // 文件格式检查
            if (!fileDownloadName.ToLower().EndsWith(".csv"))
                fileDownloadName += ".csv";

            return File(Exporter.Target(source), "text/plain", fileDownloadName);
        }
    }
}
