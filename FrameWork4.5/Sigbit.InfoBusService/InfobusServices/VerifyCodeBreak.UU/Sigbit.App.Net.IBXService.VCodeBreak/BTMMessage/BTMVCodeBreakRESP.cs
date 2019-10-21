using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.IBXService.VCodeBreak.BTMMessage
{
    public class BTMVCodeBreakRESP : SRMResponseBase
    {
        private string _breakResultText = "";
        /// <summary>
        /// 破解的结果文字
        /// </summary>
        public string BreakResultText
        {
            get { return _breakResultText; }
            set { _breakResultText = value; }
        }

        /// <summary>
        /// 从属性同步
        /// </summary>
        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            AddAStringValue("break_result_text", this.BreakResultText);
        }

        /// <summary>
        /// 同步到属性
        /// </summary>
        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.BreakResultText = GetAStringValue("break_result_text");
        }

        /// <summary>
        /// 得到消息的描述
        /// </summary>
        /// <returns>消息的描述</returns>
        public override string GetMessageDescription()
        {
            string sRet = base.GetMessageDescription();
            sRet += "破解结果:" + this.BreakResultText + ";";

            return sRet;
        }
    }
}
