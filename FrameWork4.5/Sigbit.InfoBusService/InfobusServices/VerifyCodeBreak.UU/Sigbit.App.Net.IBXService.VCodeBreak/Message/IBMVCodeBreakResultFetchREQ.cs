using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.App.Net.IBXService.Message;

namespace Sigbit.App.Net.IBXService.VCodeBreak.Message
{
    public class IBMVCodeBreakResultFetchREQ : IBMRequestBase
    {
        public IBMVCodeBreakResultFetchREQ()
        {
            this.TransCode = "vcode_break_result_fetch";
            this.TransCodeChs = "提取验证码破解结果";
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

        private string _uploadReceipt = "";
        /// <summary>
        /// 上传回执
        /// </summary>
        public string UploadReceipt
        {
            get { return _uploadReceipt; }
            set { _uploadReceipt = value; }
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            this.AddAStringValue("authen.user_name", this.AuthenUserName);
            this.AddAStringValue("authen.password", this.AuthenPassword);

            this.AddAStringValue("vcbreak.upload_receipt", this.UploadReceipt);
        }

        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.AuthenUserName = GetAStringValue("authen.user_name");
            this.AuthenPassword = GetAStringValue("authen.password");

            this.UploadReceipt = GetAStringValue("vcbreak.upload_receipt");
        }

        public override string GetMessageDescription()
        {
            string sRet = base.GetMessageDescription();

            sRet += "授权用户:" + this.AuthenUserName + "(" + this.AuthenPassword + ");";

            sRet += "上传回执：" + this.UploadReceipt + ";";

            return sRet;
        }
    }
}
