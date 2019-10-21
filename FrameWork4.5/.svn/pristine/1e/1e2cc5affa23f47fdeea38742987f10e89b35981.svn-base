using System;
using System.Collections.Generic;
using System.Text;
using Sigbit.Common;

namespace Sigbit.App.Net.WeiXinService.Event
{
    public class TWXLocationEvent : TWXEventBaseReq
    {
        public TWXLocationEvent()
        {
            this.Event = TWXEvent.Location;
        }

        private double _latitude = 0.0;
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        private double _longitude = 0;
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }


        private double _precision = 0;
        /// <summary>
        /// 地理位置精度
        /// </summary>
        public double Precision
        {
            get { return _precision; }
            set { _precision = value; }
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            AddAStringValue("Latitude", this.Latitude.ToString("0.000000"), false);
            AddAStringValue("Longitude", this.Longitude.ToString("0.000000"), false);
            AddAStringValue("Precision", this.Precision.ToString("0.000000"), false);
        }


        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.Latitude = ConvertUtil.ToFloat(GetAStringValue("Latitude"));
            this.Longitude = ConvertUtil.ToFloat(GetAStringValue("Longitude"));
            this.Precision = ConvertUtil.ToFloat(GetAStringValue("Precision"));
        }

    }
}
