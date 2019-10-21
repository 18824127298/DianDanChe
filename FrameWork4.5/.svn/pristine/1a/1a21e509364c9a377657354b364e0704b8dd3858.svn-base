using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.WeiXinService.Event
{
    /// <summary>
    /// 关注事件
    /// </summary>
    public class TWXSubscribeEvent : TWXEventBaseReq
    {
        protected bool _isQRScene = false;
        /// <summary>
        /// 是否为二维码带参数扫描关注
        /// </summary>
        public bool IsQRScene
        {
            get { return _isQRScene; }
        }

        public TWXSubscribeEvent()
        {
            this.Event = TWXEvent.Subscribe;
            _isQRScene = false;
        }



    }

    /// <summary>
    /// 用户未关注扫描二维码事件
    /// </summary>
    public class TWXQRsceneSubscribeEvent : TWXSubscribeEvent
    {

        public TWXQRsceneSubscribeEvent()
        {
            this.Event = TWXEvent.Subscribe;
            _isQRScene = true;
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
