using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Diagnostics;
using Sigbit.Common;
using CheDaiBaoCommonService.Service;


namespace CheDaiBaoCommonController.ControllersEnumerates
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class WeiXinAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            WeiXinAuthenticationService WeiXinBase = new WeiXinAuthenticationService();
            if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName != "Loan" || filterContext.ActionDescriptor.ActionName != "CapriciousLoan")
            {
                if (System.Web.HttpContext.Current.Session["WeiXin"] == null)
                {
                    WeiXinBase.WeiXinRenZheng(filterContext);
                }
            }
        }
    }
}

