using CheDaiBaoWeChatModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CheDaiBaoCommonController.ControllersEnumerates
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class GetEffectiveIPAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            string effectiveIp=Configs.GetEffectiveIP();
            if (!string.IsNullOrEmpty(effectiveIp))
            {
                string ip = filterContext.RequestContext.HttpContext.Request.UserHostAddress;
                if (!ip.Equals(effectiveIp))
                {
                    HandleUnauthorizedRequest(filterContext, "非法IP，无法访问！");
                }
            }
        }

        private void HandleUnauthorizedRequest(AuthorizationContext filterContext, string message)
        {
            ContentResult Content = new ContentResult();
            Content.Content = string.Format("<a href='javascript:history.go(-1);'>{0}</a>", message);
            filterContext.Result = Content;
        }
    }
}
