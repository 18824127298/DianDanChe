using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Common
{
    /// <summary>
    /// 实时的文本消息
    /// </summary>
    public class RealTimeMessageBase
    {
        class MessageLineBuilder
        {
            private const int MAX_LINE_COUNT_LIMIT = 400;

            private StringBuilder _sbMessage = new StringBuilder();

            private int _lineCount = 0;

            public void Append(string sString)
            {
                _sbMessage.Append(sString);
            }

            public void AppendLine(string sLine)
            {
                _sbMessage.AppendLine(sLine);
                _lineCount++;

                if (_lineCount > MAX_LINE_COUNT_LIMIT)
                {
                    _sbMessage = new StringBuilder();
                    _lineCount = 0;
                }
            }

            public void AppendLine()
            {
                AppendLine("");
            }

            public int LineCount
            {
                get
                {
                    return _lineCount;
                }
            }

            public override string ToString()
            {
                return _sbMessage.ToString();
            }
        }

        #region logMessage

        private MessageLineBuilder _mbDisplayMessage = new MessageLineBuilder();

        /// <summary>
        /// 用于显示的文本信息
        /// </summary>
        public string DisplayMessageText
        {
            get { return _mbDisplayMessage.ToString(); }
            set
            {
                _mbDisplayMessage = new MessageLineBuilder();
                _mbDisplayMessage.Append(value);
            }
        }

        /// <summary>
        /// 增加一行
        /// </summary>
        /// <param name="sLine">一行文本</param>
        public void AddMessageLine(string sLine)
        {
            _mbDisplayMessage.AppendLine(sLine);
        }

        /// <summary>
        /// 增加一个空行
        /// </summary>
        public void AddMessageLine()
        {
            _mbDisplayMessage.AppendLine();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sUser"></param>
        /// <param name="sMessage"></param>
        public void AddUserMessage(string sUser, string sMessage)
        {
            string sUserLine = sUser + " (" + DateTimeUtil.Now.Substring(5) + "):";
            _mbDisplayMessage.AppendLine(sUserLine);
            _mbDisplayMessage.AppendLine(sMessage.Trim());
        }

        /// <summary>
        /// 提取显示的文本，并清除
        /// </summary>
        /// <returns>显示的文本</returns>
        public string FetchAndClear()
        {
            string sRet = _mbDisplayMessage.ToString();
            _mbDisplayMessage = new MessageLineBuilder();
            return sRet;
        }

        /// <summary>
        /// 消息行数
        /// </summary>
        public int MessageLineCount
        {
            get
            {
                return _mbDisplayMessage.LineCount;
            }
        }
        #endregion logMessage
    }
}
