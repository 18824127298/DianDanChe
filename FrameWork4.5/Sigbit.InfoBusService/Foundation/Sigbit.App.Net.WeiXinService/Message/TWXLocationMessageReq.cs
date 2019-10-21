using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.WeiXinService.Message
{
    public class TWXLocationMessageReq : TWXMessageBaseReq
    {
        public TWXLocationMessageReq()
        {
            this.MsgType = MsgType.Location;
        }

        private string _location_X = "";
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Location_X
        {
            get { return _location_X; }
            set { _location_X = value; }
        }

        private string _location_Y = "";
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Location_Y
        {
            get { return _location_Y; }
            set { _location_Y = value; }
        }

        private int _scale = 0;
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public int Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        private string _label = "";
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label
        {
            get { return _label; }
            set { _label = value; }
        }


        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            AddAStringValue("Label", this.Label, true);
            AddAStringValue("MsgId", this.MsgId, false);
        }


        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.Location_X = GetAStringValue("Location_X");
            this.Location_Y = GetAStringValue("Location_X");
            this.Scale = GetAIntValue("Scale");
            this.Label = GetAStringValue("Label");
            this.MsgId = GetAStringValue("MsgId");
        }
    }
}
