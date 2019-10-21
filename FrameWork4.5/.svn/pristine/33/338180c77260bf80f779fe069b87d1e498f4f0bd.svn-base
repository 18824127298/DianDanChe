using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.WeiXinService.Message
{
    public class TWXTextMessageReq : TWXMessageBaseReq
    {
        public TWXTextMessageReq()
        {
            this.MsgType = MsgType.Text;
        }


        private string _content = "";
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            AddAStringValue("Content", this.Content, true);
            AddAStringValue("MsgId", this.MsgId, false);
        }


        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.Content = GetAStringValue("Content");
            this.MsgId = GetAStringValue("MsgId");
        }
    }
}
