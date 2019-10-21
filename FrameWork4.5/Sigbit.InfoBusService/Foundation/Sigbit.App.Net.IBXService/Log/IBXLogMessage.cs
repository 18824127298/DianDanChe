using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;
using Sigbit.App.Net.IBXService.Message;
using Sigbit.App.Net.IBXService.DBDefine;

namespace Sigbit.App.Net.IBXService.Log
{
    public class IBXLogMessage
    {
        private DateTime _requestTime = DateTime.Now;
        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestTime
        {
            get { return _requestTime; }
            set { _requestTime = value; }
        }

        private IBMRequestBase _requestMessage = null;
        /// <summary>
        /// 请求消息
        /// </summary>
        public IBMRequestBase RequestMessage
        {
            get { return _requestMessage; }
            set { _requestMessage = value; }
        }

        private byte[] _requestPacket = null;
        /// <summary>
        /// 请求数据包
        /// </summary>
        public byte[] RequestPacket
        {
            get { return _requestPacket; }
            set { _requestPacket = value; }
        }

        private string _requestPacket__Description = "";
        /// <summary>
        /// 上传数据包的描述。如果上传的数据包是二进制信息（图片、音频），则填写该字段。
        /// </summary>
        public string RequestPacket__Description
        {
            get { return _requestPacket__Description; }
            set { _requestPacket__Description = value; }
        }

        private IBMResponseBase _responseMessage = null;
        /// <summary>
        /// 响应消息
        /// </summary>
        public IBMResponseBase ResponseMessage
        {
            get { return _responseMessage; }
            set { _responseMessage = value; }
        }

        /// <summary>
        /// 上一次清除日志的时间
        /// </summary>
        private static DateTime _lastClearLogTime = DateTime.Now.AddDays(-1);

        public void NoteLog()
        {
            //=========== 1. 数据准备 =============
            TbLogMessage tblLog = new TbLogMessage();
            tblLog.RequestTime = DateTimeUtil.ToDateTimeStrWithMilliSeconds(this.RequestTime);

            tblLog.LogUid = Guid.NewGuid().ToString();

            if (this.RequestMessage != null)
            {
                tblLog.TransCodeEng = this.RequestMessage.TransCode;
                tblLog.TransCodeChs = this.RequestMessage.TransCodeChs;
            }
            tblLog.FromApplication = "client";
            tblLog.ToApplication = "server";

            if (this.RequestPacket != null)
                tblLog.RequestPacket = Encoding.UTF8.GetString(this.RequestPacket);
            else
                tblLog.RequestPacket = this.RequestPacket__Description;

            if (this.RequestMessage != null)
            {
                tblLog.RequestDesc = this.RequestMessage.GetMessageDescription();

                tblLog.FromSystem = this.RequestMessage.FromSystem;
                tblLog.FromClientId = this.RequestMessage.FromClientId;
                tblLog.FromClientVersion = this.RequestMessage.FromClientVersion;
                tblLog.FromClientDesc = this.RequestMessage.FromClientDesc;
            }

            tblLog.ResponseTime = DateTimeUtil.NowWithMilliSeconds;

            if (this.ResponseMessage != null)
            {
                tblLog.ResponsePacket = this.ResponseMessage.ToString();
                tblLog.ResponseDesc = this.ResponseMessage.GetMessageDescription();

                tblLog.ErrorCode = this.ResponseMessage.ErrorCode;
                tblLog.ErrorString = this.ResponseMessage.ErrorString;
            }

            tblLog.Remarks = "";

            //================ 2. 计算响应时长 =============
            tblLog.CallDuration = DateTimeUtil.MilliSecondsAfter(tblLog.RequestTime, tblLog.ResponseTime);

            //============ 3. 插入日志记录 ===========
            if (CTIBXLogMessageConfig.Instance.LogMessageInDB)
                tblLog.Insert();

            //=========== 4. 清除过期的日志，每5分钟清除一次 =========
            TimeSpan tsSeconds = DateTime.Now - _lastClearLogTime;
            if (tsSeconds.TotalSeconds > 60 * 4 + 30)
            {
                _lastClearLogTime = DateTime.Now;

                string sClearLogEndTime = DateTimeUtil.AddSeconds(DateTimeUtil.Now,
                        -1 * CTIBXLogMessageConfig.Instance.LogClearBeforeHours * 3600);

                int nRemoveRecCount = TbLogMessage.ClearLogBeforeTime(sClearLogEndTime);

                if (nRemoveRecCount != 0)
                {
                    DebugLogger.LogDebugMessage("清除总线消息日志，共删除" + nRemoveRecCount.ToString() + "条记录",
                            "IBX_LOG_MESSAGE.NOTE_LOG");
                }
            }


        }
    }
}
