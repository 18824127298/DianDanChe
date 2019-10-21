using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.App.Net.IBXService.Message;

namespace Sigbit.App.Net.IBXService.VCodeBreak.Message
{
    public class IBMVCodeBreakResultFetchRESP : IBMResponseBase
    {
        private string _breakResultText = "";
        /// <summary>
        /// 破解的文字结果
        /// </summary>
        public string BreakResultText
        {
            get { return _breakResultText; }
            set { _breakResultText = value; }
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            this.AddAStringValue("break_result.break_text", this.BreakResultText);
        }

        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.BreakResultText = GetAStringValue("break_result.break_text");
        }

        public override string GetMessageDescription()
        {
            string sRet = base.GetMessageDescription();

            sRet += "破解的结果文字:" + this.BreakResultText + ";";

            return sRet;
        }
    }
}
