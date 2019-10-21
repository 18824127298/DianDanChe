using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;
using Sigbit.Net.XmlPacket;
using Sigbit.Net.BIPPacket;

namespace Sigbit.Data.RomitSQL
{
    /// <summary>
    /// ��Ϣ�Ļ���
    /// </summary>
    public abstract class ROMMBase : XmlPacket
    {
        private ROMXMessageID _messageID = ROMXMessageID.None;
        /// <summary>
        /// ��Ϣ��ʶ
        /// </summary>
        public ROMXMessageID MessageID
        {
            get { return _messageID; }
            set { _messageID = value; }
        }

        /// <summary>
        /// �õ����ĺ�������������Ϣ
        /// </summary>
        /// <returns>���ĺ�������������Ϣ</returns>
        /// <remarks>
        /// ��BIPPacket�е�ʵ�������ж�ȡ��������ReadFromBytes()��ToBytes()֮�����
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
        /// �õ�һ���������������Ϣ
        /// </summary>
        /// <param name="ds">�����</param>
        /// <returns>������Ϣ</returns>
        private string GetBIPPacketDataDescription__DataSet(BIPDataSet ds)
        {
            StringBuilder sbRet = new StringBuilder();

            //=========== 1. ���ֻ��һ����¼���Ͳ���key=value����ʽ ===========
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
            //======== 2. ���򣬲��ý������չ����ʽ =============
            else
            {
                int nFieldCount = ds.GetFieldCount();
                sbRet.Append("   ");
                //======== 2.1 ת���ֶ� ===========
                for (int i = 0; i < nFieldCount; i++)
                {
                    string sFieldName = ds.GetFieldName(i);
                    sbRet.Append("|");
                    sbRet.Append(sFieldName);
                }
                sbRet.AppendLine();

                //========= 2. ת������ ============
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
