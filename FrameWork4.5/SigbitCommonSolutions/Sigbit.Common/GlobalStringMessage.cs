using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Common
{
    /// <summary>
    /// 全局应用字符串消息类
    /// </summary>
    public class GlobalStringMessage
    {
        /// <summary>
        /// 消息实例名称
        /// </summary>
        private string _messageInstanceName;

        /// <summary>
        /// 缺省的实例名
        /// </summary>
        private const string DEFAULT_INSTANCE_NAME = "instanceDefault";

        private static Hashtable _htbMessagePool = new Hashtable();

        private static GlobalStringMessage _instance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static GlobalStringMessage Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GlobalStringMessage();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public GlobalStringMessage()
        {
            _messageInstanceName = DEFAULT_INSTANCE_NAME;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sMessageInstanceName">消息实例名称</param>
        public GlobalStringMessage(string sMessageInstanceName)
        {
            _messageInstanceName = sMessageInstanceName;
        }

        /// <summary>
        /// 消息池实例名称
        /// </summary>
        public string MessageInstanceName
        {
            get
            {
                return _messageInstanceName;
            }
            set
            {
                _messageInstanceName = value;
                if (_messageInstanceName == null || _messageInstanceName == "")
                    _messageInstanceName = DEFAULT_INSTANCE_NAME;
            }
        }
        /// <summary>
        /// 消息委托
        /// </summary>
        /// <param name="sMessage">传入消息字符串</param>
        public delegate void MessageDelegate(string sMessage);
        /// <summary>
        /// 新消息事件
        /// </summary>
        public event MessageDelegate NewMessage;

        /// <summary>
        /// 加入消息
        /// </summary>
        /// <param name="sMessage">消息</param>
        public void PushMessage(string sMessage)
        {
            PushMessage(sMessage, false);
        }

        /// <summary>
        /// 加入消息
        /// </summary>
        /// <param name="sMessage">消息</param>
        /// <param name="bGenEvent">是否生成事件</param>
        public void PushMessage(string sMessage, bool bGenEvent)
        {
            //===============1.累加消息========================
            string sPoolMsg = ConvertUtil.ToString(_htbMessagePool[_messageInstanceName], "");
            sPoolMsg += sMessage + "\r\n";
            _htbMessagePool[_messageInstanceName] = sPoolMsg;

            if (!bGenEvent)
            {
                return;
            }

            //===============2.触发新消息事件==================
            if (NewMessage != null)
            {
                NewMessage(sMessage);
            }
        }

        /// <summary>
        /// 获得消息
        /// </summary>
        /// <returns></returns>
        public string GetMessage()
        {
            return ConvertUtil.ToString(_htbMessagePool[_messageInstanceName], "");
        }

        /// <summary>
        /// 清除消息
        /// </summary>
        public void ClearMessage()
        {
            _htbMessagePool[_messageInstanceName] = "";
        }

    }
}
