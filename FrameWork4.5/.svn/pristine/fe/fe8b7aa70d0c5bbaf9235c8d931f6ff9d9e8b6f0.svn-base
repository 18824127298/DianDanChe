using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.WeiXinService.Event
{
    /// <summary>
    /// 用户已关注时的事件推送
    /// </summary>
    public class TWXScanEvent : TWXSubscribeEvent
    {
        public TWXScanEvent()
        {
            this.Event = TWXEvent.Scan;
        }

        private string _eventKey = "";
        /// <summary>
        /// 事件KEY值，qrscene_为前缀，后面为二维码的参数值
        /// </summary>
        public string EventKey
        {
            get { return _eventKey; }
            set { _eventKey = value; }
        }

        private string _ticket = "";
        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket
        {
            get { return _ticket; }
            set { _ticket = value; }
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            AddAStringValue("EventKey", this.EventKey, true);
            AddAStringValue("Ticket", this.Ticket, true);
        }

        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();
            this.EventKey = GetAStringValue("EventKey");
            this.Ticket = GetAStringValue("Ticket");
        }

    }
}
