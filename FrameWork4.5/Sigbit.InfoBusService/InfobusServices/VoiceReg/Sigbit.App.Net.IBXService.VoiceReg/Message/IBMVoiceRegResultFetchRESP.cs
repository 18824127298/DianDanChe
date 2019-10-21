using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.App.Net.IBXService.Message;

namespace Sigbit.App.Net.IBXService.VoiceReg.Message
{
    public class IBMVoiceRegResultFetchRESP : IBMResponseBase
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

        private string _requestThirdId = "";
        /// <summary>
        /// 第三方标识信息
        /// </summary>
        public string RequestThirdId
        {
            get { return _requestThirdId; }
            set { _requestThirdId = value; }
        }

        private string _regResultText = "";
        /// <summary>
        /// 识别的文字结果
        /// </summary>
        public string RegResultText
        {
            get { return _regResultText; }
            set { _regResultText = value; }
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            this.AddAStringValue("isr_result.receipt", this.UploadReceipt);
            this.AddAStringValue("isr_result.third_id", this.RequestThirdId);
            this.AddAStringValue("isr_result.isr_text", this.RegResultText);
        }

        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.UploadReceipt = GetAStringValue("isr_result.receipt");
            this.RequestThirdId = GetAStringValue("isr_result.third_id");
            this.RegResultText = GetAStringValue("isr_result.isr_text");
        }

        public override string GetMessageDescription()
        {
            string sRet = base.GetMessageDescription();

            sRet += "回执:" + this.UploadReceipt + ";";
            sRet += "第三方标识:" + this.RequestThirdId + ";";
            sRet += "识别的结果文字:" + this.RegResultText + ";";

            return sRet;
        }
    }
}
