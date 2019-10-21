using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.WeiXinService.Message
{
    public class TWXLinkMessageReq : TWXMessageBaseReq
    {
        public TWXLinkMessageReq()
        {
            this.MsgType = MsgType.Link;
        }


        private string _title = "";
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _description = "";
        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _url = "";
        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }


        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            AddAStringValue("Title", this.Title, true);
            AddAStringValue("Description", this.Description, true);
            AddAStringValue("Url", this.Url, true);
            AddAStringValue("MsgId", this.MsgId, false);
        }


        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.Title = GetAStringValue("Title");
            this.Description = GetAStringValue("Description");
            this.Url = GetAStringValue("Url");
            this.MsgId = GetAStringValue("MsgId");
        }
    }
}
