using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.App.Net.IBXService.Message;

namespace Sigbit.App.Net.IBXService.VoiceQAN.Message
{
    public class IBMVoiceQANREQ : IBMRequestBase
    {
        public IBMVoiceQANREQ()
        {
            this.TransCode = "voice_qan";
            this.TransCodeChs = "语音分析";
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

        private string _voiceFileName = "";
        /// <summary>
        /// 语音文件名
        /// </summary>
        public string VoiceFileName
        {
            get { return _voiceFileName; }
            set { _voiceFileName = value; }
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
        /// 是否同步调用（同步调用目前只发生在语音已匹配的情况下）
        /// </summary>
        public bool SyncCall
        {
            get { return _syncCall; }
            set { _syncCall = value; }
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            this.AddAStringValue("vqan.upload_receipt", this.UploadReceipt);
            this.AddAStringValue("vqan.voice_file_name", this.VoiceFileName);
            this.AddAStringValue("vqan.third_id", this.RequestThirdId);

            if (this.SyncCall)
                this.AddAStringValue("call.sync", "Y");
            else
                this.AddAStringValue("call.sync", "N");
        }

        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.UploadReceipt = GetAStringValue("vqan.upload_receipt");
            this.VoiceFileName = GetAStringValue("vqan.voice_file_name");
            this.RequestThirdId = GetAStringValue("vqan.third_id");

            if (GetAStringValue("call.sync") == "N")
                this.SyncCall = false;
            else
                this.SyncCall = true;
        }

        public override string GetMessageDescription()
        {
            string sRet = base.GetMessageDescription();

            sRet += "上传回执：" + this.UploadReceipt + ";";
            sRet += "语音文件名：" + this.VoiceFileName + ";";
            sRet += "第三方编码：" + this.RequestThirdId + ";";

            if (this.SyncCall)
                sRet += "同步调用";
            else
                sRet += "【异步调用】";

            return sRet;
        }
    }
}
