using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheDaiBaoWeChatService.Service;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoCommonService.Expansion;
using CheDaiBaoCommonController;

namespace CheDaiBaoWeChatController.Interface
{
    public class QiyebaoSms
    {
        public bool SendSms(string sPhone, string sContent)
        {
            SendHttp sendhttp = new SendHttp();
            String str = sendhttp.sendMsg(sPhone, sContent, "true", "", "");
            string sRespcode = Expansions.GetJsonValue(str, "respcode");
            SendMessageHistoryService sendMessageHistoryService = new SendMessageHistoryService();
            if (sRespcode == "0")
            {
                sendMessageHistoryService.Insert(new SendMessageHistory()
                {
                    Phone = sPhone,
                    SendStatus = sRespcode,
                    SendTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    SmsContent = sContent,
                    Remarks = "短信发送成功"
                });
                return true;
            }
            else
            {
                sendMessageHistoryService.Insert(new SendMessageHistory()
                {
                    Phone = sPhone,
                    SendStatus = sRespcode,
                    SendTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    SmsContent = sContent,
                    Remarks = "短信发送失败",
                    SendFailMsg = Expansions.GetJsonValue(str, "respdesc")
                });
                return false;
            }
        }
    }
}
