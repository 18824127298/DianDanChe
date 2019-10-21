using CheDaiBaoCommonController.Model;
using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CheDaiBaoCommonController.Controllers
{
    public class BaseCommonController:Controller
    {
        protected virtual void AddNotification(MessageResultModels MessageResultModels, bool persistForTheNextRequest)
        {
            string dataKey = string.Format("Message.{0}", typeof(MessageResultModels).Name);
            if (persistForTheNextRequest)
            {
                if (TempData[dataKey] == null)
                    TempData[dataKey] = new List<MessageResultModels>();
                ((List<MessageResultModels>)TempData[dataKey]).Add(MessageResultModels);
            }
            else
            {
                if (ViewData[dataKey] == null)
                    ViewData[dataKey] = new List<MessageResultModels>();
                ((List<MessageResultModels>)ViewData[dataKey]).Add(MessageResultModels);
            }
        }

        protected virtual void Notification(MessageResultModels MessageResultModels, bool persistForTheNextRequest = true)
        {
            AddNotification(MessageResultModels, persistForTheNextRequest);
        }

        protected virtual void Prompt(MessageResultModels MessageResultModels, bool persistForTheNextRequest = true)
        {
            string dataKey = "Message";
            if (persistForTheNextRequest)
            {
                if (TempData[dataKey] == null)
                    TempData[dataKey] = new List<MessageResultModels>();
                ((List<MessageResultModels>)TempData[dataKey]).Add(MessageResultModels);
            }
            else
            {
                if (ViewData[dataKey] == null)
                    ViewData[dataKey] = new List<MessageResultModels>();
                ((List<MessageResultModels>)ViewData[dataKey]).Add(MessageResultModels);
            }
        }


        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
            {

                //记录错误日志
                OperaService operaService = new OperaService();
                operaService.Insert(new Opera()
                {
                    BorrowerId = 0,
                    OperaType = OperaType.网站异常,
                    RelationId = 0,
                    Remark = filterContext.Exception.ToString() + "Url:" + Request.Url
                });
            }
            base.OnException(filterContext);
        }


        /// <summary>
        /// 收集错误并显示
        /// </summary>
        protected virtual void ShowValidateError()
        {
            foreach (var key in ModelState.Keys.ToList())
            {
                foreach (var error in ModelState[key].Errors)
                {
                    Notification(new MessageResultModels(key + ":" + error.ErrorMessage, NotifyEnum.Error));
                    return;
                }
            }
        }


        protected virtual JsonResult ShowJsonValidateError()
        {
            List<string> errList = new List<string>();
            foreach (var key in ModelState.Keys.ToList())
            {
                foreach (var error in ModelState[key].Errors)
                {
                    errList.Add(key+":"+error.ErrorMessage);
                }
            }
            return Json(new { Result = false, Messages = string.Join(" ", errList)});
        }


    }
}
