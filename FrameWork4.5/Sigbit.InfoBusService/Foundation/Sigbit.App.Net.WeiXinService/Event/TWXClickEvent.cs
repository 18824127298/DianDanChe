using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.WeiXinService.Event
{
    public class TWXClickEvent : TWXEventBaseReq
    {
        public TWXClickEvent()
        {
            this.Event = TWXEvent.Click;
        }

        private string _eventKey = "";
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
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
