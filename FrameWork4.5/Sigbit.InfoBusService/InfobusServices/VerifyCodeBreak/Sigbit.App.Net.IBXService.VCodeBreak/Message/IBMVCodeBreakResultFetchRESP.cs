using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.App.Net.IBXService.Message;

namespace Sigbit.App.Net.IBXService.VCodeBreak.Message
{
    public class IBMVCodeBreakResultFetchRESP : IBMResponseBase
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

        private string _breakResultText = "";
        /// <summary>
        /// 破解的文字结果
        /// </summary>
        public string BreakResultText
        {
            get { return _breakResultText; }
            set { _breakResultText = value; }
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            this.AddAStringValue("break_result.receipt", this.UploadReceipt);
            this.AddAStringValue("break_result.third_id", this.RequestThirdId);
            this.AddAStringValue("break_result.break_text", this.BreakResultText);
        }

        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.UploadReceipt = GetAStringValue("break_result.receipt");
            this.RequestThirdId = GetAStringValue("break_result.third_id");
            this.BreakResultText = GetAStringValue("break_result.break_text");
        }

        public override string GetMessageDescription()
        {
            string sRet = base.GetMessageDescription();

            sRet += "回执:" + this.UploadReceipt + ";";
            sRet += "第三方标识:" + this.RequestThirdId + ";";
            sRet += "破解的结果文字:" + this.BreakResultText + ";";

            return sRet;
        }
    }
}
