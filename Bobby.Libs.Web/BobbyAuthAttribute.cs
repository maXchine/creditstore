using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Bobby.Libs.Text;

namespace Bobby.Libs.Web
{
    public class BobbyAuthAttribute : AuthorizeAttribute
    {

        /// <summary>
        /// 忽略验证的action eg： Index, VerifyCode, SignIn
        /// </summary>
        public string IgnoreActions;


        /// <summary>
        /// 用户角色
        /// </summary>
        private new object Roles { get; set; }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }

        protected virtual object GetRoles(string controllerName, string actionName)
        {
            return null;
        }

        protected virtual string Domain { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;

            var cacheKey = string.Concat(Domain, '|', controllerName, '|', actionName);

            // 如果请求已被设置为忽略的action,则不再进一步验证
            if (IgnoreActions.NonEmpty() && IgnoreActions.Split(',').Any(m => string.Equals(m.Trim(), actionName, StringComparison.CurrentCultureIgnoreCase)))
                return;

            var caches = HttpRuntime.Cache[cacheKey];

            if (caches != null)
            {
                Roles = caches;
                return;
            }

            Roles = GetRoles(controllerName, actionName);
            if (Roles != null)
                HttpRuntime.Cache.Add(cacheKey, Roles, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0), CacheItemPriority.Default, null);

            base.OnAuthorization(filterContext);
        }
    }
}
