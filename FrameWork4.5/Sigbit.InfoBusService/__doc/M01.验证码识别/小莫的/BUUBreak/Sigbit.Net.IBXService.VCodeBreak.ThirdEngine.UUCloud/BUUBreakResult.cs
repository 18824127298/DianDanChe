using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Net.IBXService.VCodeBreak.ThirdEngine.UUCloud
{
    public class BUUBreakResult
    {
        public BUUBreakResult() 
        { }

        public BUUBreakResult(string sBreakResult, string sErrCode)
        {
 
        }

        private string _breakResult;
        /// <summary>
        /// 破解结果
        /// </summary>
        public string BreakResult
        {
            get
            {
                return _breakResult;
            }
            set
            {
                _breakResult = value;
            }
        }

        private string _errorCode;
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
                try
                {
                    int nTemp = int.Parse(value);
                    _errorCode = value;
                }
                catch
                {
                    _errorCode ="0";
                }
            }
        }

        private string _errorString;
        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrorString
        {
            get
            {
                if (_errorString!=null && _errorString.Trim() != "")
                {
                    return _errorString.Trim();
                }

                if (_errorCode.Trim() == "")
                    return "";
                try
                {
                    int nErrorCode = int.Parse(_errorCode);
                    if (nErrorCode < 5000)
                    {
                        return "错误：" + _errorCode;
                    }
                    return "成功";
                }
                catch
                {
                    return "未知：" + _errorCode;
                }
            }
            set
            {
                _errorString = value;
            }
        }

    }
}
