using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoCommonController.Model
{
    public class MessageResultModels
    {
        public MessageResultModels(string context, NotifyEnum NotifyEnum = NotifyEnum.Success)
        {
            this.Context = context;
            this.NotifyEnums = NotifyEnum;
        }

        public NotifyEnum NotifyEnums { get; set; }
        public string Context { get; set; }
        public string Title { get; set; }
    }

    public enum NotifyEnum
    {
        Info,
        Success,
        Warning,
        Error,
        Loading
    }
}
