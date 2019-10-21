using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.WeiXinService.Message
{
    public class TWXVoiceMessageReq : TWXMessageBaseReq
    {
        public TWXVoiceMessageReq()
        {
            this.MsgType = MsgType.Voice;
        }


        private string _mediaId = "";
        /// <summary>
        /// 语音消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId
        {
            get { return _mediaId; }
            set { _mediaId = value; }
        }

        private string _format = "";
        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format
        {
            get { return _format; }
            set { _format = value; }
        }

       
        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            AddAStringValue("MediaId", this.MediaId, true);
            AddAStringValue("Format", this.Format, true);
            AddAStringValue("MsgId", this.MsgId, false);
        }


        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.MediaId = GetAStringValue("MediaId");
            this.Format = GetAStringValue("Format");
            this.MsgId = GetAStringValue("MsgId");
        }
    }
}
