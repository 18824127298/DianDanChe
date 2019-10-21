using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Sigbit.App.Net.WeiXinService.JSON
{

    class WeiXinAccessToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }

    public class TWJAccessTokenResp : TWJRespBase
    {
        private string _accessToken = "";

        public string AccessToken
        {
            get { return _accessToken; }
            set { _accessToken = value; }
        }

        private int _expires_in = 0;
        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public int ExpiresIn
        {
            get { return _expires_in; }
            set { _expires_in = value; }
        }


        public override void ParseJsonResult(string sResultString)
        {
            base.ParseJsonResult(sResultString);

            if (ErrCode == 0)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();

                WeiXinAccessToken result = js.Deserialize<WeiXinAccessToken>(sResultString);

                this.AccessToken = result.access_token;
                this.ExpiresIn = result.expires_in;
            }

        }

    }
}
