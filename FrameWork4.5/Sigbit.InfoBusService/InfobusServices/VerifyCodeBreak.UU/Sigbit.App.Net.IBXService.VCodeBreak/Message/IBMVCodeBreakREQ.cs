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

        private string _authenUserName = "";
        /// <summary>
        /// 授权使用者的用户名
        /// </summary>
        public string AuthenUserName
        {
            get { return _authenUserName; }
            set { _authenUserName = value; }
        }

        private string _authenPassword = "";
        /// <summary>
        /// 授权使用者的密码
        /// </summary>
        public string AuthenPassword
        {
            get { return _authenPassword; }
            set { _authenPassword = value; }
        }

        private string _VCodeUsageID = "";
        /// <summary>
        /// 验证码的使用场景标识
        /// </summary>
        public string VCodeUsageID
        {
            get { return _VCodeUsageID; }
            set { _VCodeUsageID = value; }
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

        private bool _syncCall = true;
        /// <summary>
        /// 是否同步调用
        /// </summary>
        public bool SyncCall
        {
            get { return _syncCall; }
            set { _syncCall = value; }
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            this.AddAStringValue("authen.user_name", this.AuthenUserName);
            this.AddAStringValue("authen.password", this.AuthenPassword);
            
            this.AddAStringValue("vcbreak.vcode_usage_id", this.VCodeUsageID);
            this.AddAStringValue("vcbreak.upload_receipt", this.UploadReceipt);
            this.AddAStringValue("vcbreak.image_file_name", this.ImageFileName);
            this.AddAStringValue("vcbreak.third_id", this.RequestThirdId);

            if (this.SyncCall)
                this.AddAStringValue("call.sync", "Y");
            else
                this.AddAStringValue("call.sync", "N");
        }

        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.AuthenUserName = GetAStringValue("authen.user_name");
            this.AuthenPassword = GetAStringValue("authen.password");

            this.VCodeUsageID = GetAStringValue("vcbreak.vcode_usage_id");
            this.UploadReceipt = GetAStringValue("vcbreak.upload_receipt");
            this.ImageFileName = GetAStringValue("vcbreak.image_file_name");
            this.RequestThirdId = GetAStringValue("vcbreak.third_id");

            if (GetAStringValue("call.sync") == "N")
                this.SyncCall = false;
            else
                this.SyncCall = true;
        }

        public override string GetMessageDescription()
        {
            string sRet = base.GetMessageDescription();

            sRet += "授权用户:" + this.AuthenUserName + "(" + this.AuthenPassword + ");";

            sRet += "使用场景:" + this.VCodeUsageID + ";";
            sRet += "上传回执：" + this.UploadReceipt + ";";
            sRet += "图像文件名：" + this.ImageFileName + ";";
            sRet += "第三方编码：" + this.RequestThirdId + ";";

            if (this.SyncCall)
                sRet += "同步调用";
            else
                sRet += "【异步调用】";

            return sRet;
        }
    }
}
