using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;
using Sigbit.Net.BIPPacket;

namespace Sigbit.Net.XmlPacket
{
    /// <summary>
    /// 轻量级的XMLPacket，从XmlPacket继承，实现文本文件的读写。
    /// </summary>
    public class XmlPacketLight : XmlPacket
    {
        #region 读取数据包

        /// <summary>
        /// 从字节数组读取
        /// </summary>
        /// <param name="bsPacket">字节数组</param>
        public override void ReadFrom(byte[] bsPacket)
        {
            //======= 1. 判断是否XML格式 =============
            bool bIsXMLFormat = true;
            if (Array.IndexOf(bsPacket, '<') == -1)
                bIsXMLFormat = false;

            //======== 2. XML格式读取 ===========
            if (bIsXMLFormat)
                base.ReadFrom(bsPacket);
            //========= 3. 简约格式读取 ===========
            else
            {
                string sPacketString = Encoding.UTF8.GetString(bsPacket);
                ReadFrom(sPacketString);
            }

            MaySynchronizeToProperties();
        }

        /// <summary>
        /// 从字符串读取
        /// </summary>
        /// <param name="sPacketString">字符串</param>
        public override void ReadFrom(string sPacketString)
        {
            //======= 1. 判断是否XML格式 =============
            bool bIsXMLFormat = true;
            if (sPacketString.IndexOf('<') == -1)
                bIsXMLFormat = false;

            //======== 2. XML格式读取 ===========
            if (bIsXMLFormat)
                base.ReadFrom(sPacketString);
            //========= 3. 简约格式读取 ===========
            else
            {
                ReadFromString__Light(sPacketString);
            }

            MaySynchronizeToProperties();
        }

        private void ReadFromString__Light(string sPacketString)
        {
            //========= 1. 清空所有数据 ============
            this.DeleteAllDataSet();

            //========== 2. 读取每一行，做为Key/Value，直到遇到竖线 ==========
            int nDataSetBeginLineSeq = -1;
            string[] arrLines = sPacketString.Split('\n');

            for (int i = 0; i < arrLines.Length; i++)
            {
                string sLine = arrLines[i];
                if (sLine.Length == 0)
                    continue;

                //=========== 3. 遇到"|"，就表示遇到了DataSet ========
                char cFirstChar = sLine[0];
                if (cFirstChar == '|')
                {
                    nDataSetBeginLineSeq = i;
                    break;
                }

                //======== 4. 分割出关键字和值 =========
                string[] arrItems = sLine.Split('=');
                if (arrItems.Length != 2)
                    continue;

                string sFieldName = arrItems[0];
                string sFieldValue = arrItems[1];

                this.SetAStringValue(sFieldName, sFieldValue);
            }

            //======== 5. DataSet(第二个结果集)的转换 ==============
            if (nDataSetBeginLineSeq != -1)
                ReadFromString__Light__DataSet(arrLines, nDataSetBeginLineSeq);
        }

        /// <summary>
        /// DataSet(第二个结果集)的转换
        /// </summary>
        /// <param name="arrLines">每行的数组</param>
        /// <param name="nDataSetBeginLineSeq">结果集开始的行序号</param>
        private void ReadFromString__Light__DataSet(string[] arrLines, int nDataSetBeginLineSeq)
        {
            if (arrLines.Length < nDataSetBeginLineSeq)
                return;

            //========= 1. 先转换字段定义(第一行) ===========
            string sFirstLine = arrLines[nDataSetBeginLineSeq];
            string[] arrFieldItems = sFirstLine.Split('|');
            int nFieldCount = arrFieldItems.Length - 1;

            for (int i = 0; i < nFieldCount; i++)
            {
                string sFieldName = arrFieldItems[i + 1];
                this.AddField(sFieldName);
            }

            //============ 2. 再转换数据（剩下的行） ============
            for (int nLineSeq = nDataSetBeginLineSeq + 1; nLineSeq < arrLines.Length; nLineSeq++)
            {
                int nRecNo = nLineSeq - nDataSetBeginLineSeq;
                string sLine = arrLines[nLineSeq];
                if (sLine.Length == 0)
                    break;

                string[] arrDataItems = sLine.Split('|');
                if (arrDataItems.Length != nFieldCount + 1)
                    throw new Exception("数据的字段数量和字段的定义数量不一致");

                for (int nFieldSeq = 0; nFieldSeq < nFieldCount; nFieldSeq++)
                {
                    string sDataValue = arrDataItems[nFieldSeq + 1];
                    this.SetItemString(nRecNo, nFieldSeq, sDataValue);
                }
            }
        }

        /// <summary>
        /// 从文件中读取
        /// </summary>
        /// <param name="sFileName">文件名</param>
        public override void ReadFromFile(string sFileName)
        {
            byte[] bsContent = FileUtil.ReadBytesFromFile(sFileName);
            this.ReadFrom(bsContent);

            MaySynchronizeToProperties();
        }

        #endregion 读取数据包

        #region 写出数据包
        /// <summary>
        /// 转为字符串
        /// </summary>
        /// <returns>去掉最前面的那个签名</returns>
        public override string ToString()
        {
            MaySynchronizeFromProperties();

            //======== 1. 转为XML格式 ============
            if (!this.WriteVerify__CanWriteNonXMLFormat())
            {
                string sRet = base.ToString();

                if (sRet.Length > 1)
                    sRet = sRet.Substring(1);

                return sRet;
            }

            //=========== 2. 转换第一个结果集(Key-Value对) ============
            StringBuilder sbRet = new StringBuilder();

            BIPDataSet dataSetOne = GetDataSet(0);
            for (int i = 0; i < dataSetOne.GetFieldCount(); i++)
            {
                string sFieldName = dataSetOne.GetFieldName(i);
                string sFieldValue = dataSetOne.GetItemString(1, i);

                string sLine = sFieldName + "=" + sFieldValue + "\n";
                sbRet.Append(sLine);
            }

            //============ 3. 转换第二个结果集(DataSet) =============
            if (DataSetCount >= 2)
            {
                string sDataSetString = ToString__LightDataSet(GetDataSet(1));
                sbRet.Append(sDataSetString);
            }

            return sbRet.ToString();
        }

        /// <summary>
        /// 将结果集转换成"|"分割的表示
        /// </summary>
        /// <param name="dataSet">结果集</param>
        /// <returns>结果集的简约文本输出</returns>
        private string ToString__LightDataSet(BIPDataSet dataSet)
        {
            int nFieldCount = dataSet.GetFieldCount();
            if (nFieldCount == 0)
                return "";

            StringBuilder sbDataSet = new StringBuilder();

            //======== 1. 转换字段 ===========
            for (int i = 0; i < nFieldCount; i++)
            {
                string sFieldName = dataSet.GetFieldName(i);
                sbDataSet.Append("|");
                sbDataSet.Append(sFieldName);
            }
            sbDataSet.Append("\n");

            //========= 2. 转换数据 ============
            int nRecordCount = dataSet.GetRecordCount();

            for (int nRecNo = 1; nRecNo <= nRecordCount; nRecNo++)
            {
                for (int nFieldSeq = 0; nFieldSeq < nFieldCount; nFieldSeq++)
                {
                    string sFieldValue = dataSet.GetItemString(nRecNo, nFieldSeq);
                    sbDataSet.Append("|");
                    sbDataSet.Append(sFieldValue);
                }
                sbDataSet.Append("\n");
            }

            return sbDataSet.ToString();
        }

        /// <summary>
        /// 写出到文件
        /// </summary>
        /// <param name="sFileName">文件名</param>
        public override void WriteToFile(string sFileName)
        {
            MaySynchronizeFromProperties();

            //======== 1. 转为简约格式 ============
            if (this.WriteVerify__CanWriteNonXMLFormat())
            {
                byte[] bsPacketBytes = this.ToBytes();
                FileUtil.WriteBytesToFile(sFileName, bsPacketBytes);
            }
            //======== 2. 转为XML格式 =============
            else
            {
                base.WriteToFile(sFileName);
            }
        }

        /// <summary>
        /// 写出到字节数组
        /// </summary>
        /// <returns>字节数组</returns>
        public override byte[] ToBytes()
        {
            MaySynchronizeFromProperties();

            //======== 1. 转为简约格式 ============
            if (this.WriteVerify__CanWriteNonXMLFormat())
            {
                string sPacketString = this.ToString();
                byte[] bsRet = Encoding.UTF8.GetBytes(sPacketString);
                return bsRet;
            }
            //======== 2. 转为XML格式 =============
            else
            {
                return base.ToBytes();
            }
        }

        /// <summary>
        /// 校验数据包，判断能否写成非XML的格式，以节省空间
        /// </summary>
        /// <returns>是否能写成非XML格式</returns>
        private bool WriteVerify__CanWriteNonXMLFormat()
        {
            MaySynchronizeFromProperties();

            //========= 1. 判断结果集的情况，不是单KeyValue结果集，就返回false ==========
            //========= 1.1 结果集数量大于2，不能处理 =========
            int nDataSetCount = this.DataSetCount;
            if (nDataSetCount > 2)
                return false;

            //======== 1.2 第1个结果集多于一条记录，不能处理 ==========
            int nD1RecordCount = GetDataSet(0).GetRecordCount();
            if (nD1RecordCount > 1)
                return false;

            //======== 2. 扫描前两个结果集的字段名和数据，不能包含"\n"、"="、"<"、"|" =======
            for (int nDSSeq = 0; nDSSeq < this.DataSetCount; nDSSeq++)
            {
                BIPDataSet currentDS = GetDataSet(nDSSeq);

                //============ 2.1 扫描字段名 ===============
                int nFieldCount = currentDS.GetFieldCount();
                for (int i = 0; i < nFieldCount; i++)
                {
                    string sFieldName = currentDS.GetFieldName(i);
                    if (!WriteVerify__CanWriteNonXMLFormat__AValue(sFieldName))
                        return false;
                }

                //============ 2.2 扫描数据 ===============
                int nRecordCount = currentDS.GetRecordCount();

                for (int nRecNo = 1; nRecNo <= nRecordCount; nRecNo++)
                {
                    for (int nFieldSeq = 0; nFieldSeq < nFieldCount; nFieldSeq++)
                    {
                        string sFieldValue = currentDS.GetItemString(nRecNo, nFieldSeq);
                        if (!WriteVerify__CanWriteNonXMLFormat__AValue(sFieldValue))
                            return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 校验一个值，判断能否写成非XML的格式
        /// </summary>
        /// <param name="sValue">一个值</param>
        /// <returns>能否写成非XML格式</returns>
        private bool WriteVerify__CanWriteNonXMLFormat__AValue(string sValue)
        {
            if (sValue.IndexOf('=') != -1)
                return false;

            if (sValue.IndexOf('\n') != -1)
                return false;

            if (sValue.IndexOf('<') != -1)
                return false;

            if (sValue.IndexOf('|') != -1)
                return false;

            return true;
        }
        #endregion 写出数据包
    }
}
