using CheDaiBaoCommonController.Controllers;
using CheDaiBaoWeChatService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CheDaiBaoWeChatController.Controllers
{
    public class CdbIndexController : BaseCommonController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TodayOilPrice()
        {
            return View();
        }

        public ActionResult AddCar()
        {
            return View();
        }
    }
}
