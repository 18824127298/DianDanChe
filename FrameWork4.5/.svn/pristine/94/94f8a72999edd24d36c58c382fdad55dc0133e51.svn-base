using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Sigbit.App.Net.WeiXinService.Client;

namespace Sigbit.App.Net.WeiXinService.JSON
{

    class WeiXinCallbackIPList
    {
        public List<string> ip_list = new List<string>();
    }

    /// <summary>
    /// 获取服务器IP
    /// </summary>
    public class TWJGetCallbackIpResp : TWJRespBase
    {
        //https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token=ACCESS_TOKEN

        public TWJGetCallbackIpResp()
        {

        }

        public override void ParseJsonResult(string sJsonResultString)
        {
            base.ParseJsonResult(sJsonResultString);


            if (base.ErrCode == 0)
            {

                WeiXinCallbackIPList lstIpResult = new WeiXinCallbackIPList();

                JavaScriptSerializer js = new JavaScriptSerializer();

                lstIpResult = js.Deserialize<WeiXinCallbackIPList>(sJsonResultString);

                IpList.Clear();

                foreach (string sIp in lstIpResult.ip_list)
                {
                    IpList.Add(sIp);
                }

            }

        }

        public List<string> IpList = new List<string>();

    }
}
