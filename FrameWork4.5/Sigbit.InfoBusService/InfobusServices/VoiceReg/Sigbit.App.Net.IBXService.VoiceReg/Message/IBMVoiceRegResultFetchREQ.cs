using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.App.Net.IBXService.Message;

namespace Sigbit.App.Net.IBXService.VoiceReg.Message
{
    public class IBMVoiceRegResultFetchREQ : IBMRequestBase
    {
        public IBMVoiceRegResultFetchREQ()
        {
            this.TransCode = "voice_reg_result_fetch";
            this.TransCodeChs = "提取新的语音识别结果";
        }
    }
}
