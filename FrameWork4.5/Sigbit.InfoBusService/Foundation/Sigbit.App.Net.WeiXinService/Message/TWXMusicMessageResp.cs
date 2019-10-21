using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.WeiXinService.Message
{


    public class TWXMusicMessageResp : TWXMessageBaseResp
    {
        public TWXMusicMessageResp()
        {
            this.MsgType = MsgType.Music;
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

        private string _musicURL = "";
        /// <summary>
        /// 音乐链接
        /// </summary>
        public string MusicURL
        {
            get { return _musicURL; }
            set { _musicURL = value; }
        }


        private string _hqMusicUrl = "";
        /// <summary>
        /// 高质量音乐链接，WIFI环境优先使用该链接播放音乐
        /// </summary>
        public string HQMusicUrl
        {
            get { return _hqMusicUrl; }
            set { _hqMusicUrl = value; }
        }


        private string _thumbMediaId = "";
        /// <summary>
        /// 缩略图的媒体id，通过素材管理接口上传多媒体文件，得到的id
        /// </summary>
        public string ThumbMediaId
        {
            get { return _thumbMediaId; }
            set { _thumbMediaId = value; }
        }


        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            TWXNode nodeRoot = new TWXNode();
            nodeRoot.Key = "Music";

            nodeRoot.AppendNode("Title", this.Title);
            nodeRoot.AppendNode("Description", this.Description);
            nodeRoot.AppendNode("MusicURL", this.MusicURL);
            nodeRoot.AppendNode("HQMusicUrl", this.HQMusicUrl);
            nodeRoot.AppendNode("ThumbMediaId", this.ThumbMediaId);

            AddNode(nodeRoot);
        }


        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            TWXNode nodeRoot = base.GetNode("Music");

            this.Title = nodeRoot.ChildNodes.GetValueOfKey("Title");
            this.Description = nodeRoot.ChildNodes.GetValueOfKey("Description");
            this.MusicURL = nodeRoot.ChildNodes.GetValueOfKey("MusicURL");
            this.HQMusicUrl = nodeRoot.ChildNodes.GetValueOfKey("HQMusicUrl");
            this.ThumbMediaId = nodeRoot.ChildNodes.GetValueOfKey("ThumbMediaId");


        }
    }
}
