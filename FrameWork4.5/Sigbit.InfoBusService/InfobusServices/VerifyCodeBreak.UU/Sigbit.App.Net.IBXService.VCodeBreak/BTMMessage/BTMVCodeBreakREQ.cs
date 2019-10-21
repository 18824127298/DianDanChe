using System;
using System.Collections.Generic;
using System.Text;
using Sigbit.Common;

namespace Sigbit.App.Net.IBXService.VCodeBreak.BTMMessage
{
    public class BTMVCodeBreakREQ : SRMRequestBase
    {
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

        private string _imageData = "";
        /// <summary>
        /// 图像数据
        /// </summary>
        public string ImageData
        {
            get { return _imageData; }
            set { _imageData = value; }
        }

        private string _imageFileName = "";
        /// <summary>
        /// 验证码的图像文件名
        /// </summary>
        public string ImageFileName
        {
            get { return _imageFileName; }
            set { _imageFileName = value; }
        }

        private string _requestThirdID = "";
        /// <summary>
        /// 第三方编码
        /// </summary>
        public string RequestThirdID
        {
            get { return _requestThirdID; }
            set { _requestThirdID = value; }
        }

        public BTMVCodeBreakREQ()
        {
            this.CommandId = "vcode_break_request";
            this.CommandIdChs = "验证码识别接口";
        }

        /// <summary>
        /// 从属性同步
        /// </summary>
        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            AddAStringValue("authen_user_name", this.AuthenUserName);
            AddAStringValue("authen_password", this.AuthenPassword);
            AddAStringValue("vcode_usage_id", this.VCodeUsageID);
            AddAStringValue("image_data", this.ImageData);
            AddAStringValue("image_file_name", this.ImageFileName);
            AddAStringValue("request_third_id", this.RequestThirdID);
        }

        /// <summary>
        /// 同步到属性
        /// </summary>
        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.AuthenUserName = GetAStringValue("authen_user_name");
            this.AuthenPassword = GetAStringValue("authen_password");
            this.VCodeUsageID = GetAStringValue("vcode_usage_id");
            this.ImageData = GetAStringValue("image_data");
            this.ImageFileName = GetAStringValue("image_file_name");
            this.RequestThirdID = GetAStringValue("request_third_id");
        }

        public void LoadImageDataFromFile(string sFileName)
        {
            byte[] bsFileContent = FileUtil.ReadBytesFromFile(sFileName);
            string sImageData = Convert.ToBase64String(bsFileContent, 0, bsFileContent.Length);
            this.ImageData = sImageData;
        }

        public void SaveImageDataToFile(string sFileName)
        {
            byte[] bsFileContent = System.Convert.FromBase64String(this.ImageData);
            FileUtil.WriteBytesToFile(sFileName, bsFileContent);
        }

        /// <summary>
        /// 得到消息的描述
        /// </summary>
        /// <returns>消息的描述</returns>
        public override string GetMessageDescription()
        {
            string sRet = base.GetMessageDescription();
            sRet += "授权用户:" + this.AuthenUserName + "(" + this.AuthenPassword + ");";
            sRet += "使用场景:" + this.VCodeUsageID + ";";
            sRet += "图像文件：" + this.ImageFileName + "(64Size:" + this.ImageData.Length.ToString() + ");";
            sRet += "第三方编码：" + this.RequestThirdID + ";";

            return sRet;
        }
    }
}
