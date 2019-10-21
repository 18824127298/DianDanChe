using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.WeiXinService.Event
{
    public class TWXViewEvent : TWXEventBaseReq
    {
        public TWXViewEvent()
        {
            this.Event = TWXEvent.View;
        }

        private string _eventKey = "";
        /// <summary>
        /// 事件KEY值，设置的跳转URL
        /// </summary>
        public string EventKey
        {
            get { return _eventKey; }
            set { _eventKey = value; }
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();
            AddAStringValue("EventKey", this.EventKey, true);
        }

        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();
            this.EventKey = GetAStringValue("EventKey");
        }
    }
}
