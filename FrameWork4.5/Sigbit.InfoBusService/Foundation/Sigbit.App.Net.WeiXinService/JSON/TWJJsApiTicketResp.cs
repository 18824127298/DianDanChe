using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigbit.App.Net.WeiXinService.JSON
{
    public class TWJJsApiTicketResp : TWJRespBase
    {
        public string ticket { get; set; }

        public int expires_in { get; set; }


        public override void ParseJsonResult(string sJsonResultString)
        {
            base.ParseJsonResult(sJsonResultString);
        }

    }
}
