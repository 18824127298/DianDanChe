using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.App.Net.IBXService.Message;

namespace Sigbit.App.Net.IBXService.VoiceQAN.Message
{
    public class IBMVoiceQANResultFetchREQ : IBMRequestBase
    {
        public IBMVoiceQANResultFetchREQ()
        {
            this.TransCode = "voice_qan_result_fetch";
            this.TransCodeChs = "提取语音分析结果";
        }

    }
}
