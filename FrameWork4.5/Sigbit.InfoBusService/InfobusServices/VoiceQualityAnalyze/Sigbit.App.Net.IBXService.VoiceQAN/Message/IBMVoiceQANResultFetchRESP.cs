using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.App.Net.IBXService.Message;
using Sigbit.Common;

namespace Sigbit.App.Net.IBXService.VoiceQAN.Message
{
    public class IBMVoiceQANResultFetchRESP : IBMResponseBase
    {
        private string _uploadReceipt = "";
        /// <summary>
        /// 上传回执
        /// </summary>
        public string UploadReceipt
        {
            get { return _uploadReceipt; }
            set { _uploadReceipt = value; }
        }

        private string _requestThirdID = "";
        /// <summary>
        /// 第三方标识
        /// </summary>
        public string RequestThirdID
        {
            get { return _requestThirdID; }
            set { _requestThirdID = value; }
        }

        private double _qualityValue01 = 0;
        /// <summary>
        /// 杂音
        /// </summary>
        public double QualityValue01
        {
            get { return _qualityValue01; }
            set { _qualityValue01 = value; }
        }

        private double _qualityValue02 = 0;
        /// <summary>
        /// 音量
        /// </summary>
        public double QualityValue02
        {
            get { return _qualityValue02; }
            set { _qualityValue02 = value; }
        }

        private double _qualityValue03 = 0;
        /// <summary>
        /// 连贯性
        /// </summary>
        public double QualityValue03
        {
            get { return _qualityValue03; }
            set { _qualityValue03 = value; }
        }

        private double _qualityValue04 = 0;
        /// <summary>
        /// 其它（保留）
        /// </summary>
        public double QualityValue04
        {
            get { return _qualityValue04; }
            set { _qualityValue04 = value; }
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            this.AddAStringValue("req.upload_receipt", this.UploadReceipt);
            this.AddAStringValue("req.request_third_id", this.RequestThirdID);
            this.AddAStringValue("vqan.quality_value_01", this.QualityValue01.ToString("0.000"));
            this.AddAStringValue("vqan.quality_value_02", this.QualityValue02.ToString("0.000"));
            this.AddAStringValue("vqan.quality_value_03", this.QualityValue03.ToString("0.000"));
            this.AddAStringValue("vqan.quality_value_04", this.QualityValue04.ToString("0.000"));
        }

        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.UploadReceipt = GetAStringValue("req.upload_receipt");
            this.RequestThirdID = GetAStringValue("req.request_third_id");
            this.QualityValue01 = ConvertUtil.ToFloat(GetAStringValue("vqan.quality_value_01"));
            this.QualityValue02 = ConvertUtil.ToFloat(GetAStringValue("vqan.quality_value_02"));
            this.QualityValue03 = ConvertUtil.ToFloat(GetAStringValue("vqan.quality_value_03"));
            this.QualityValue04 = ConvertUtil.ToFloat(GetAStringValue("vqan.quality_value_04"));
        }

        public override string GetMessageDescription()
        {
            string sRet = base.GetMessageDescription();

            sRet += "上传回执：" + this.UploadReceipt + ";";
            sRet += "第三方编码:" + this.RequestThirdID + ";";
            sRet += "杂音:" + this.QualityValue01.ToString("0.000");
            sRet += "音量:" + this.QualityValue02.ToString("0.000");
            sRet += "连贯性:" + this.QualityValue03.ToString("0.000");
            sRet += "其它:" + this.QualityValue04.ToString("0.000");

            return sRet;
        }
    }
}
