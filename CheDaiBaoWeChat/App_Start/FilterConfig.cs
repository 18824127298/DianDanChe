using CheDaiBaoCommonController.ControllersEnumerates;
using System.Web;
using System.Web.Mvc;

namespace CheDaiBaoWeChat
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new WeiXinAuthorizeAttribute());
        }
    }
}