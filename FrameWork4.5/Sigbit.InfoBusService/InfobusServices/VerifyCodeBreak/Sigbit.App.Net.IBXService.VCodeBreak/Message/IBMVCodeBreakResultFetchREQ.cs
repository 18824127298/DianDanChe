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
            this.TransCodeChs = "提取新的验证码破解结果";
        }
    }
}
