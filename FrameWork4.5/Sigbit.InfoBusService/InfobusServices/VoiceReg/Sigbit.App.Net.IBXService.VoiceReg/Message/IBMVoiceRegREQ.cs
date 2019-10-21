using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.App.Net.IBXService.Message;

namespace Sigbit.App.Net.IBXService.VoiceReg.Message
{
    public class IBMVoiceRegREQ : IBMRequestBase
    {
        public IBMVoiceRegREQ()
        {
            this.TransCode = "voice_reg";
            this.TransCodeChs = "语音识别";
        }

        private string _grammerId = "";
        /// <summary>
        /// 语法标识
        /// </summary>
        public string GrammarId
        {
            get { return _grammerId; }
            set { _grammerId = value; }
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
        /// 语音文件
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

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            this.AddAStringValue("vreg.grammar_id", this.GrammarId);
            this.AddAStringValue("vreg.upload_receipt", this.UploadReceipt);
            this.AddAStringValue("vreg.voice_file_name", this.VoiceFileName);
            this.AddAStringValue("vreg.third_id", this.RequestThirdId);
        }

        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.GrammarId = GetAStringValue("vreg.grammar_id");
            this.UploadReceipt = GetAStringValue("vreg.upload_receipt");
            this.VoiceFileName = GetAStringValue("vreg.voice_file_name");
            this.RequestThirdId = GetAStringValue("vreg.third_id");
        }

        public override string GetMessageDescription()
        {
            string sRet = base.GetMessageDescription();

            sRet += "语法标识:" + this.GrammarId + ";";
            sRet += "上传回执：" + this.UploadReceipt + ";";
            sRet += "语音文件名：" + this.VoiceFileName + ";";
            sRet += "第三方编码：" + this.RequestThirdId + ";";

            return sRet;
        }
    }
}
