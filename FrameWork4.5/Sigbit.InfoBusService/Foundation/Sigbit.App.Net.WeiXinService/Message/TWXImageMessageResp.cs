using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.WeiXinService.Message
{

    //public class TWXImageMessageResp_DataItem : TWXElementPacket
    //{
    //    private string _mediaId = "";
    //    /// <summary>
    //    /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
    //    /// </summary>
    //    public string MediaId
    //    {
    //        get { return _mediaId; }
    //        set { _mediaId = value; }
    //    }


    //    public override void SynchronizeFromProperties()
    //    {
    //        base.SynchronizeFromProperties();

    //        AddAStringValue("MediaId", this.MediaId, true);
    //    }


    //    public override void SynchronizeToProperties()
    //    {
    //        base.SynchronizeToProperties();

    //        this.MediaId = GetAStringValue("MediaId", true);

    //    }



    //}


    public class TWXImageMessageResp : TWXMessageBaseResp
    {
        public TWXImageMessageResp()
        {
            this.MsgType = MsgType.Image;
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

            TWXNode nodeImage = new TWXNode();
            nodeImage.Key = "Image";

            TWXNode nodeMediaId = new TWXNode();
            nodeMediaId.Key = "MediaId";
            nodeMediaId.Value = this.MediaId;
            nodeMediaId.IsCDATA = true;
            nodeMediaId.AllowNull = false;

            nodeImage.AppendNode(nodeMediaId);

            base.AddNode(nodeImage);

        }


        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            

        }
    }
}
