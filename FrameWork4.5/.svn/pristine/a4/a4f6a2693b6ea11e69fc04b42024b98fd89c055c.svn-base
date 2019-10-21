using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;
using Sigbit.App.Net.WeiXinService.Message;

namespace Sigbit.App.Net.WeiXinService.Event
{
    public enum TWXEvent
    {
        None,
        [SbtEnumDescString("订阅")]
        Subscribe,
        [SbtEnumDescString("取消订阅")]
        Unsubscribe,
        [SbtEnumDescString("用户已关注")]
        Scan,
        [SbtEnumDescString("上报地理位置")]
        Location,
        [SbtEnumDescString("点击事件")]
        Click,
        [SbtEnumDescString("菜单链接")]
        View
    }


    public class TWXEventBaseReq : TWXMessageBaseReq
    {
        public TWXEventBaseReq()
        {
            this.MsgType = MsgType.Event;
        }


        private TWXEvent _event = TWXEvent.None;
        /// <summary>
        /// 事件类型
        /// </summary>
        public TWXEvent Event
        {
            get { return _event; }
            set { _event = value; }
        }


        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            string sEventValue = "";

            switch (this.Event)
            {
                case TWXEvent.Subscribe:
                    sEventValue = this.Event.ToCodeString();
                    break;
                case TWXEvent.Unsubscribe:
                    sEventValue = this.Event.ToCodeString();
                    break;
                default:
                    sEventValue = this.Event.ToCodeString().ToUpper();
                    break;
            }

            AddAStringValue("Event", sEventValue, true);
        }


        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            string sEventValue = GetAStringValue("Event");

            this.Event = (TWXEvent)EnumExUtil.CodeToEnum(TWXEvent.None, sEventValue.ToLower());

        }
    }
}
