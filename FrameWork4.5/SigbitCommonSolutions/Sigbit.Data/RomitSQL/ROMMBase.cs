using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;
using Sigbit.Net.XmlPacket;
using Sigbit.Net.BIPPacket;

namespace Sigbit.Data.RomitSQL
{
    /// <summary>
    /// 消息的基类
    /// </summary>
    public abstract class ROMMBase : XmlPacket
    {
        private ROMXMessageID _messageID = ROMXMessageID.None;
        /// <summary>
        /// 消息标识
        /// </summary>
        public ROMXMessageID MessageID
        {
            get { return _messageID; }
            set { _messageID = value; }
        }

        /// <summary>
        /// 得到包的汉字数据描述信息
        /// </summary>
        /// <returns>包的汉字数据描述信息</returns>
        /// <remarks>
        /// 从BIPPacket中的实际数据中读取，建议在ReadFromBytes()或ToBytes()之后调用
        /// </remarks>
        public string GetBIPPacketDataDescription()
        {
            string sRet = "";
            for (int i = 0; i < this.DataSetCount; i++)
            {
                if (i != 0)
                    sRet += "\r\n";

                BIPDataSet ds = this.GetDataSet(i);
                sRet += GetBIPPacketDataDescription__DataSet(ds);
            }
            return sRet;
        }

        /// <summary>
        /// 得到一个结果集的描述信息
        /// </summary>
        /// <param name="ds">结果集</param>
        /// <returns>描述信息</returns>
        private string GetBIPPacketDataDescription__DataSet(BIPDataSet ds)
        {
            StringBuilder sbRet = new StringBuilder();

            //=========== 1. 如果只有一条记录，就采用key=value的形式 ===========
            if (ds.GetRecordCount() == 1)
            {
                for (int i = 0; i < ds.GetFieldCount(); i++)
                {
                    string sFieldName = ds.GetFieldName(i);
                    string sFieldValue = ds.GetItemString(1, i);

                    string sLine = sFieldName + "=" + sFieldValue;
                    sbRet.AppendLine(sLine);
                }
            }
            //======== 2. 否则，采用结果集的展现形式 =============
            else
            {
                int nFieldCount = ds.GetFieldCount();
                sbRet.Append("   ");
                //======== 2.1 转换字段 ===========
                for (int i = 0; i < nFieldCount; i++)
                {
                    string sFieldName = ds.GetFieldName(i);
                    sbRet.Append("|");
                    sbRet.Append(sFieldName);
                }
                sbRet.AppendLine();

                //========= 2. 转换数据 ============
                int nRecordCount = ds.GetRecordCount();

                for (int nRecNo = 1; nRecNo <= nRecordCount; nRecNo++)
                {
                    sbRet.Append(nRecNo.ToString() + ":");
                    for (int nFieldSeq = 0; nFieldSeq < nFieldCount; nFieldSeq++)
                    {
                        string sFieldValue = ds.GetItemString(nRecNo, nFieldSeq);
                        sbRet.Append("|");
                        sbRet.Append(sFieldValue);
                    }
                    sbRet.AppendLine();
                }
            }

            return sbRet.ToString();
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            AddAStringValue("message_id", ConvertUtil.EnumToString(this.MessageID));
        }

        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.MessageID = (ROMXMessageID)ConvertUtil.StringToEnum(GetAStringValue("message_id"), ROMXMessageID.None);
        }
    }
}
