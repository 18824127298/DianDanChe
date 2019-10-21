using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.WeiXinService.Message
{
    public class TWXImageMessageReq : TWXMessageBaseReq
    {
        public TWXImageMessageReq()
        {
            this.MsgType = MsgType.Image;
        }


        private string _picUrl = "";
        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl
        {
            get { return _picUrl; }
            set { _picUrl = value; }
        }

        private string _mediaId = "";
        /// <summary>
        /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId
        {
            get { return _mediaId; }
            set { _mediaId = value; }
        }


        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            AddAStringValue("PicUrl", this.PicUrl, true);
            AddAStringValue("MediaId", this.MediaId, true);
            AddAStringValue("MsgId", this.MsgId, false);
        }


        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.PicUrl = GetAStringValue("PicUrl");
            this.MediaId = GetAStringValue("MediaId");
            this.MsgId = GetAStringValue("MsgId");
        }
    }
}
