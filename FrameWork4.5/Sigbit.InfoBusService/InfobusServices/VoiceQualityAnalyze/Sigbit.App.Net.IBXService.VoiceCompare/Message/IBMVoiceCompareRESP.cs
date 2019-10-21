using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;
using Sigbit.App.Net.IBXService.Message;


namespace Sigbit.App.Net.IBXService.VoiceCOMP.Message
{

    public enum IBMVoiceCompareMatchResult
    {
        None,
        [SbtEnumDescString("成功")]
        Succ,
        [SbtEnumDescString("失败")]
        Fail,
        [SbtEnumDescString("无基准音")]
        NoStandVoice,
        [SbtEnumDescString("无比较音")]
        NoMatchVoice
    }

    public class IBMVoiceCompareRESP : IBMResponseBase
    {

        private IBMVoiceCompareMatchResult _matchResult = IBMVoiceCompareMatchResult.None;

        public IBMVoiceCompareMatchResult MatchResult
        {
            get { return _matchResult; }
            set { _matchResult = value; }
        }


        private double _matchRate = 0.0;
        /// <summary>
        /// 匹配率
        /// </summary>
        public double MatchRate
        {
            get { return _matchRate; }
            set { _matchRate = value; }
        }


        private double _playDelay = 0.0;
        /// <summary>
        /// 放音时延
        /// </summary>
        public double PlayDelay
        {
            get { return _playDelay; }
            set { _playDelay = value; }
        }


        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            this.AddAStringValue("vcomp.match_result", EnumExUtil.ToCodeString(this.MatchResult));
            this.AddAStringValue("vcomp.match_rate", this.MatchRate.ToString("0.0000"));
            this.AddAStringValue("vcomp.play_delay", this.PlayDelay.ToString("0.00"));

        }

        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();
            this.MatchResult = (IBMVoiceCompareMatchResult)EnumExUtil.CodeToEnum(IBMVoiceCompareMatchResult.None, GetAStringValue("vcomp.match_result"));
            this.MatchRate = ConvertUtil.ToFloat(GetAStringValue("vcomp.match_rate"));
            this.PlayDelay = ConvertUtil.ToFloat(GetAStringValue("vcomp.play_delay"));

        }

        public override string GetMessageDescription()
        {
            string sRet = base.GetMessageDescription();

            sRet += "匹配率:" + this.MatchRate.ToString("%") + ";";
            sRet += "放音时延:" + this.PlayDelay.ToString("0.00");

            return sRet;
        }

    }
}
