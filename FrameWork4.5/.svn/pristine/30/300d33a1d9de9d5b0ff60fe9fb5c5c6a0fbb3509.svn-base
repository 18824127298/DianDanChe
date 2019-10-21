using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Script.Serialization;
using Sigbit.App.Net.WeiXinService;

namespace Sigbit.App.Net.WeiXinService.JSON
{
    public class TWJRespBase
    {
        private int _errcode = 0;
        /// <summary>
        /// 错误码
        /// </summary>
        public int ErrCode
        {
            get { return _errcode; }
            set { _errcode = value; }
        }

        private string _errmsg = "";
        /// <summary>
        /// 错误说明
        /// </summary>
        public string ErrMsg
        {
            get { return _errmsg; }
            set { _errmsg = value; }
        }

        public string ErrMsgDesc
        {
            get
            {
                WeiXinReturnCode eReturnCode = (WeiXinReturnCode)this.ErrCode;

                return eReturnCode.ToString();
            }
        }


        private string _resultInfo = "";

        public string ResultInfo
        {
            get { return _resultInfo; }
        }


        public virtual void ParseJsonResult(string sJsonResultString)
        {
            _resultInfo = sJsonResultString;

            if (sJsonResultString.StartsWith("{\"errcode\":"))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();

                TWJRespBase result = js.Deserialize<TWJRespBase>(sJsonResultString);

                this.ErrCode = result.ErrCode;
                this.ErrMsg = result.ErrMsg;

            }
        }

        public override string ToString()
        {
            if (this.ErrCode == 0)
                return "";

            string sRet = this.ErrMsgDesc + "(" + this.ErrCode + ")";

            return sRet;
            
        }


    }
}
