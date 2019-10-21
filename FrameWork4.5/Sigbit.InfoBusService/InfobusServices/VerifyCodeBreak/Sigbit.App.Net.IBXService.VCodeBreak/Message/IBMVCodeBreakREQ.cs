using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.App.Net.IBXService.Message;

namespace Sigbit.App.Net.IBXService.VCodeBreak.Message
{
    public class IBMVCodeBreakREQ : IBMRequestBase
    {
        public IBMVCodeBreakREQ()
        {
            this.TransCode = "vcbreak_break";
            this.TransCodeChs = "验证码破解";
        }

        private string _vcodeId = "";
        /// <summary>
        /// 验证码的标识（类型）
        /// </summary>
        public string VcodeId
        {
            get { return _vcodeId; }
            set { _vcodeId = value; }
        }

        private string _uploadReceipt = "";
        /// <summary>
        /// 上传回执
        /// </summary>
        public string UploadReceipt
        {
            get { return _uploadReceipt; }
            set { _uploadReceipt = value; }
        }

        private string _imageFileName = "";
        /// <summary>
        /// 图像文件
        /// </summary>
        public string ImageFileName
        {
            get { return _imageFileName; }
            set { _imageFileName = value; }
        }

        private string _requestThirdId = "";
        /// <summary>
        /// 第三方编码
        /// </summary>
        public string RequestThirdId
        {
            get { return _requestThirdId; }
            set { _requestThirdId = value; }
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            this.AddAStringValue("vcbreak.vcode_id", this.VcodeId);
            this.AddAStringValue("vcbreak.upload_receipt", this.UploadReceipt);
            this.AddAStringValue("vcbreak.image_file_name", this.ImageFileName);
            this.AddAStringValue("vcbreak.third_id", this.RequestThirdId);
        }

        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.VcodeId = GetAStringValue("vcbreak.vcode_id");
            this.UploadReceipt = GetAStringValue("vcbreak.upload_receipt");
            this.ImageFileName = GetAStringValue("vcbreak.image_file_name");
            this.RequestThirdId = GetAStringValue("vcbreak.third_id");
        }

        public override string GetMessageDescription()
        {
            string sRet = base.GetMessageDescription();

            sRet += "验证码类型（标识）:" + this.VcodeId + ";";
            sRet += "上传回执：" + this.UploadReceipt + ";";
            sRet += "图像文件名：" + this.ImageFileName + ";";
            sRet += "第三方编码：" + this.RequestThirdId + ";";

            return sRet;
        }
    }
}
