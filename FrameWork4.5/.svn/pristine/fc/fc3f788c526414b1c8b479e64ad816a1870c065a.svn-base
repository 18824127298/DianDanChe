using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.WeiXinService.Message
{
    public class TWXTextMessageResp : TWXMessageBaseResp
    {
        public TWXTextMessageResp()
        {
            this.MsgType = MsgType.Text;
        }


        private string _content = "";
        /// <summary>
        /// 回复的消息内容（换行：在content中能够换行，微信客户端就支持换行显示）
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
        }


        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.Content = GetAStringValue("Content");
        }
    }
}
