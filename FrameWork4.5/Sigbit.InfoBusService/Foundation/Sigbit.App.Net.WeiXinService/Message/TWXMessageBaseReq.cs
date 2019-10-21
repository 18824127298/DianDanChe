using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.WeiXinService.Message
{
    public class TWXMessageBaseReq : TWXMessageBase
    {
        private string _msgId = "";
        /// <summary>
        /// 消息Id
        /// </summary>
        public string MsgId
        {
            get { return _msgId; }
            set { _msgId = value; }
        }


    }
}
