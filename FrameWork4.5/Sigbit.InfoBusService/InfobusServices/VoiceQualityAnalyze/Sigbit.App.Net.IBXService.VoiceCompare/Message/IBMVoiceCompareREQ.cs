using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.App.Net.IBXService.Message;

namespace Sigbit.App.Net.IBXService.VoiceCOMP.Message
{

    public class IBMVoiceCompareREQ : IBMRequestBase
    {
        public IBMVoiceCompareREQ()
        {
            this.TransCode = "voice_compare";
            this.TransCodeChs = "语音对比";
        }

        private string _standVoice = "";
        /// <summary>
        /// 基准音名称
        /// </summary>
        public string StandVoice
        {
            get { return _standVoice; }
            set { _standVoice = value; }
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


        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            this.AddAStringValue("vcomp.stand_voice", this.StandVoice);
            this.AddAStringValue("vcomp.voice_file_name", this.VoiceFileName);
        }

        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.StandVoice = GetAStringValue("vcomp.stand_voice");
            this.VoiceFileName = GetAStringValue("vcomp.voice_file_name");

        }

        public override string GetMessageDescription()
        {
            string sRet = base.GetMessageDescription();

            sRet += "基准音：" + this.StandVoice + ";";
            sRet += "语音文件名：" + this.VoiceFileName + ";";


            return sRet;
        }
    }
}
