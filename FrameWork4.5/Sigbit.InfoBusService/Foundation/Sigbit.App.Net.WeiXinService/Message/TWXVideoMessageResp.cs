using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.WeiXinService.Message
{
    public class TWXVideoMessageResp : TWXMessageBaseResp
    {
        public TWXVideoMessageResp()
        {
            this.MsgType = MsgType.Video;
        }


        private string _mediaId = "";
        /// <summary>
        /// 通过素材管理接口上传多媒体文件，得到的id
        /// </summary>
        public string MediaId
        {
            get { return _mediaId; }
            set { _mediaId = value; }
        }

        private string _title = "";
        /// <summary>
        /// 视频消息的标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }


        private string _description = "";
        /// <summary>
        /// 视频消息的描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            TWXNode nodeRoot = new TWXNode();
            nodeRoot.Key = "Video";

            nodeRoot.AppendNode("MediaId", this.MediaId);
            nodeRoot.AppendNode("Title", this.Title);
            nodeRoot.AppendNode("Description", this.Description);

            AddNode(nodeRoot);
        }


        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            TWXNode nodeRoot = base.GetNode("Video");

            this.MediaId = nodeRoot.ChildNodes.GetValueOfKey("MediaId");
            this.Title = nodeRoot.ChildNodes.GetValueOfKey("Title");
            this.Description = nodeRoot.ChildNodes.GetValueOfKey("Description");

        }
    }
}
