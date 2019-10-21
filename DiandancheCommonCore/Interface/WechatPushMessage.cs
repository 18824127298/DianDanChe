using CheDaiBaoCommonService.Service;
using Sigbit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiandancheCommonCore.Interface
{
    public class WechatPushMessage
    {
        public string PushOverdueMessage()
        {
            string apptoken = WeChatBaseRequestService.getApptoken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", apptoken);
            string sContent = @"{
'touser':'oAh7kw24ZCfQCZyuHia7IOQ0Nd9A',
'template_id':'LFieeIO8MCi3IPIAKnXoumKhJ9BTFlfG2LwUSZJJ1F0',
'url':'http://weixin.qq.com/download',
'topcolor':'#FF0000',
'data':{
'keyword1': {
'value':'黄先生',
'color':'#173177'}}}";
            string userinfo = WeChatBaseRequestService.PostUrl(url, sContent);
            DebugLogger.LogDebugMessage(userinfo);
            return userinfo;
        }
    }
}
