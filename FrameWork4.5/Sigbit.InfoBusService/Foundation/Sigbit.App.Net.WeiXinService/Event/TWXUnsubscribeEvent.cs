using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.WeiXinService.Event
{
    public class TWXUnsubscribeEvent : TWXEventBaseReq
    {
        public TWXUnsubscribeEvent()
        {
            this.Event = TWXEvent.Unsubscribe;
        }
    }
}
