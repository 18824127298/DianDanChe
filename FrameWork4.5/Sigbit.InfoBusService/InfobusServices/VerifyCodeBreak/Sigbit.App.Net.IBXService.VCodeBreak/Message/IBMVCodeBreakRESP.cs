using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.App.Net.IBXService.Message;

namespace Sigbit.App.Net.IBXService.VCodeBreak.Message
{
    public class IBMVCodeBreakRESP : IBMResponseBase
    {
        private string _breakResult = "";
        /// <summary>
        /// 破解结果
        /// </summary>
        public string BreakResult
        {
            get { return _breakResult; }
            set { _breakResult = value; }
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            this.AddAStringValue("vcbreak.break_result", this.BreakResult);
        }

        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.BreakResult = GetAStringValue("vcbreak.break_result");
        }

        public override string GetMessageDescription()
        {
            string sRet = base.GetMessageDescription();

            sRet += "破解结果:" + this.BreakResult + ";";

            return sRet;
        }
    }
}
