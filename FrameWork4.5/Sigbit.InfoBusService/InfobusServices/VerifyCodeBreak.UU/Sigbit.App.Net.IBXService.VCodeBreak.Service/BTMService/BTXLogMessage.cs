using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;

using Sigbit.App.Net.IBXService.VCodeBreak.BTMMessage;
using Sigbit.App.Net.IBXService.DBDefine;

namespace Sigbit.App.Net.IBXService.VCodeBreak.Service.BTMService
{
    class BTXLogMessage
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

        private SRMRequestBase _requestMessage = null;
        /// <summary>
        /// 请求消息
        /// </summary>
        public SRMRequestBase RequestMessage
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

        private SRMResponseBase _responseMessage = null;
        /// <summary>
        /// 响应消息
        /// </summary>
        public SRMResponseBase ResponseMessage
        {
            get { return _responseMessage; }
            set { _responseMessage = value; }
        }

        public void NoteLog()
        {
            //=========== 1. 数据准备 =============
            TbLogMessage tblLog = new TbLogMessage();
            tblLog.RequestTime = DateTimeUtil.ToDateTimeStrWithMilliSeconds(this.RequestTime);

            tblLog.LogUid = Guid.NewGuid().ToString();

            if (this.RequestMessage != null)
            {
                tblLog.TransCodeEng = this.RequestMessage.CommandId;
                tblLog.TransCodeChs = this.RequestMessage.CommandIdChs;
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

                tblLog.FromSystem = "";
                tblLog.FromClientId = "";
                tblLog.FromClientVersion = "";
                tblLog.FromClientDesc = "";
            }

            tblLog.ResponseTime = DateTimeUtil.NowWithMilliSeconds;

            if (this.ResponseMessage != null)
            {
                tblLog.ResponsePacket = this.ResponseMessage.ToString();
                tblLog.ResponseDesc = this.ResponseMessage.GetMessageDescription();

                tblLog.ErrorCode = (int)ConvertUtil.ExtractNumberFromString(this.ResponseMessage.ErrorCode, 0);
                tblLog.ErrorString = this.ResponseMessage.ErrorString;

                if (tblLog.ErrorString != "")
                {
                    tblLog.ErrorString += "(" + this.ResponseMessage.ErrorCode + ")";
                }
            }

            tblLog.Remarks = "";

            //================ 2. 计算响应时长 =============
            tblLog.CallDuration = DateTimeUtil.MilliSecondsAfter(tblLog.RequestTime, tblLog.ResponseTime);

            //============ 3. 插入日志记录 ===========
            tblLog.Insert();
        }
    }
}
