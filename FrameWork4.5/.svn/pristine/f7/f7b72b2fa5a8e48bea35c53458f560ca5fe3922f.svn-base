using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.App.Net.IBXService.Message;

namespace Sigbit.App.Net.IBXService.Upload.Message
{
    public class IBMUploadReceiptRESP : IBMResponseBase
    {
        private string _receipt = "";
        /// <summary>
        /// 回执
        /// </summary>
        public string Receipt
        {
            get { return _receipt; }
            set { _receipt = value; }
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            this.AddAStringValue("receipt.receipt", this.Receipt);
        }

        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.Receipt = GetAStringValue("receipt.receipt");
        }

        public override string GetMessageDescription()
        {
            string sRet = base.GetMessageDescription();

            sRet += "回执:" + this.Receipt + ";";

            return sRet;
        }
    }
}
