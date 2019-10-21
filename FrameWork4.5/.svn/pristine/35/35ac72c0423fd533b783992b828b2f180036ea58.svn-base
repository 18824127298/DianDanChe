using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.WeiXinService.Message
{
    public class TWXVideoMessageReq : TWXMessageBaseReq
    {
        public TWXVideoMessageReq()
        {
            this.MsgType = MsgType.Video;
        }


        private string _mediaId = "";
        /// <summary>
        /// 视频消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId
        {
            get { return _mediaId; }
            set { _mediaId = value; }
        }

        private string _thumbMediaId = "";
        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string ThumbMediaId
        {
            get { return _thumbMediaId; }
            set { _thumbMediaId = value; }
        }

       
        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            AddAStringValue("MediaId", this.MediaId, true);
            AddAStringValue("ThumbMediaId", this.ThumbMediaId, true);
            AddAStringValue("MsgId", this.MsgId, false);
        }


        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.MediaId = GetAStringValue("MediaId");
            this.ThumbMediaId = GetAStringValue("ThumbMediaId");
            this.MsgId = GetAStringValue("MsgId");
        }
    }

    public class TWXShortvideoMessageReq : TWXVideoMessageReq
    {
        public TWXShortvideoMessageReq()
        {
            this.MsgType = Message.MsgType.Shortvideo;
        }
    }

}
