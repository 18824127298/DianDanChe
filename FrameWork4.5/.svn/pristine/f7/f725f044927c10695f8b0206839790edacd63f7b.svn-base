using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.WeiXinService.Message
{

    public class TWXVoiceMessageResp : TWXMessageBaseResp
    {
        public TWXVoiceMessageResp()
        {
            this.MsgType = MsgType.Voice;
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

            TWXNode nodeRoot = new TWXNode();
            nodeRoot.Key = "Voice";

            nodeRoot.AppendNode("MediaId", this.MediaId);

            AddNode(nodeRoot);

        }


        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            TWXNode nodeRoot = base.GetNode("Voice");

            this.MediaId = nodeRoot.ChildNodes.GetValueOfKey("MediaId");

        }
    }
}
