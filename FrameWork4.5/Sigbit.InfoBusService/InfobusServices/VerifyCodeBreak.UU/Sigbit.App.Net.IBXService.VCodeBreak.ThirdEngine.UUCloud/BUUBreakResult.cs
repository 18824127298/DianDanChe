using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.IBXService.VCodeBreak.ThirdEngine.UUCloud
{
    public class BUUBreakResult
    {
        private string _breakResultText = "";
        /// <summary>
        /// 破解结果
        /// </summary>
        public string BreakResultText
        {
            get
            {
                return _breakResultText;
            }
            set
            {
                _breakResultText = value;
            }
        }

        private string _errorCode = "";
        /// <summary>
        /// 错误码
        /// </summary>
        public string ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
            }
        }

        private string _errorString = "";
        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrorString
        {
            get
            {
                return _errorString;
            }
            set
            {
                _errorString = value;
            }
        }

    }
}
