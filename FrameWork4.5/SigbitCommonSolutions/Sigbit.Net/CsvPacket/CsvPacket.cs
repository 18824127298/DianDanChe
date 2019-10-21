using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.IO;

using Sigbit.Common;
using Sigbit.Net.BIPPacket;

namespace Sigbit.Net.CsvPacket
{
    public class CsvPacket : BIPCustomPacket
    {
        private bool _inPlainFormat = false;
        /// <summary>
        /// 是否存为平面格式
        /// </summary>
        public bool InPlainFormat
        {
            get { return _inPlainFormat; }
            set { _inPlainFormat = value; }
        }

        #region 同步属性和结果集
        /// <summary>
        /// 从属性同步到DataSet中
        /// </summary>
        protected virtual void SynchronizeFromProperties()
        {
        }

        /// <summary>
        /// 已从属性同步
        /// </summary>
        private bool _bHasSynchronizedFromProperties = false;

        /// <summary>
        /// 判断从属性同步，并同步
        /// </summary>
        protected void MaySynchronizeFromProperties()
        {
            if (!_bHasSynchronizedFromProperties)
            {
                SynchronizeFromProperties();
                _bHasSynchronizedFromProperties = true;
            }
        }

        /// <summary>
        /// 从DataSet中同步到属性
        /// </summary>
        protected virtual void SynchronizeToProperties()
        {
        }

        /// <summary>
        /// 已同步到属性
        /// </summary>
        private bool _bHasSynchronizedToProperties = false;

        /// <summary>
        /// 判断同步属性，并同步
        /// </summary>
        protected void MaySynchronizeToProperties()
        {
            if (!_bHasSynchronizedToProperties)
            {
                SynchronizeToProperties();
                _bHasSynchronizedToProperties = true;
            }
        }

        #endregion 同步属性和结果集

        #region 内容显示
        /// <summary>
        /// 得到显示Packet内容的文本
        /// </summary>
        /// <returns>Packet内容文本</returns>
        public string GetDisplayContentText()
        {
            string sContent = "";
            string sLine;

            //======== 1. 包格式 ========
            sLine = "<<BIP_PACKET>>    FORMAT:" + PacketFormat;
            sLine += "\tTYPE:" + PacketType;
            sLine += "\tTRANSCODE:" + TransCode;
            sContent += sLine + "\r\n";

            if (PacketFormat == BIPPacketFormat.ShortFormat)
            {
                sLine = "SHORT_DATA:" + ShortPacketData;
                sContent += sLine + "\r\n";
                return sContent;
            }

            //======== 2. 版本号、包标志、数据块数量 ========
            sLine += "\tPACKETID=" + PacketId;
            sLine += "\tDATASET_COUNT=" + DataSetCount;
            sContent += sLine + "\r\n";

            //=========== 3. DataSet ==========
            int i;

            for (i = 0; i < DataSetCount; i++)
                sContent += GetDataSet(i).GetDisplayContentText();

            return sContent;
        }
        #endregion 内容显示

        #region 读取CSV
        /// <summary>
        /// 从文件中产生包的内容（第二个结果集）
        /// </summary>
        /// <param name="sFileName">文件名</param>
        public void ReadFromFile(string sFileName)
        {
            this.DeleteAllDataSet();

            BIPDataSet ds = GetDataSet(1);
            CsvPacket___Util.FileToDataSet(sFileName, ds);

            MaySynchronizeToProperties();
        }

        public void ReadAllDataSetFromPath(string sDirectoryName)
        {
            for (int i = 0; i < DataSetCount; i++)
            {
                BIPDataSet ds = GetDataSet(i);

                if (ds.DataSetName != "")
                {
                    string sFileName = sDirectoryName + "\\" + ds.DataSetName + ".csv";
                    CsvPacket___Util.FileToDataSet(sFileName, ds);
                }
            }
        }

        #endregion

        #region 写入CSV

        /// <summary>
        /// 将包的内容写入文件（第二个结果集）
        /// </summary>
        /// <param name="sFileName">文件名</param>
        public void WriteToFile(string sFileName)
        {
            MaySynchronizeFromProperties();

            BIPDataSet ds = GetDataSet(1);

            CsvPacket___Util.DataSetToFile(ds, sFileName, this.InPlainFormat);
        }

        public string[] WriteAllDataSetToPath(string sDirectoryName)
        {
            ArrayList arrRet = new ArrayList();

            Directory.CreateDirectory(sDirectoryName);

            for (int i = 0; i < DataSetCount; i++)
            {
                BIPDataSet ds = GetDataSet(i);

                if (ds.DataSetName != "")
                {
                    string sFileName = sDirectoryName + "\\" + ds.DataSetName + ".csv";
                    CsvPacket___Util.DataSetToFile(ds, sFileName, this.InPlainFormat);

                    arrRet.Add(sFileName);
                }
            }

            string[] retFileNameList = ArrayUtil.ToStringArray(arrRet);
            return retFileNameList;
        }


        #endregion

        #region 快捷调用手段
        public void AddKeyValueRec(string sKey, string sValue)
        {
            int nNewRecNo = this.GetRecordCount() + 1;

            this.SetItemString(nNewRecNo, "key", sKey);
            this.SetItemString(nNewRecNo, "value", sValue);
        }

        #endregion
    }

    /// <summary>
    /// CSV读写的应用类。在BIPDataSet和CSV文件之间进行转换。
    /// </summary>
    public class CsvPacket___Util
    {
        private static Encoding _fileEncoding = Encoding.Default;
        /// <summary>
        /// 文件编码
        /// </summary>
        public static Encoding FileEncoding
        {
            get { return CsvPacket___Util._fileEncoding; }
            set { CsvPacket___Util._fileEncoding = value; }
        }

        #region 读取
        /// <summary>
        /// 读取文件到BIPDataSet中
        /// </summary>
        /// <param name="sFileName">文件名</param>
        /// <param name="ds">数据集</param>
        public static void FileToDataSet(string sFileName, BIPDataSet ds)
        {
            ds.Clear();

            //============ 1. 读到字符串数组中 =============
            string[] arrLines = FileUtil.ReadStringListFromFile(sFileName, true);
                                         
            //========== 2. 处理每一行 =============
            string sCSVDataLine = "";

            for (int i = 0; i < arrLines.Length; i++)
            {
                string sLine = arrLines[i];

                if (sCSVDataLine == "")
                    sCSVDataLine = sLine;
                else
                    sCSVDataLine += "\r\n" + sLine;

                //======== 3. 如果包含偶数个引号，说明该行数据中结束 =======
                int nQuotaCount = StringUtil.Occurs("\"", sCSVDataLine);
                if (nQuotaCount % 2 == 0)
                {
                    FileToDataSet_AddDataLine(sCSVDataLine, ds);
                    sCSVDataLine = "";
                }
            }
        }

        /// <summary>
        /// 插入CSV的一行到数据集中
        /// </summary>
        /// <param name="sCSVDataLine">CSV的一行</param>
        /// <param name="ds">数据集</param>
        private static void FileToDataSet_AddDataLine(string sCSVDataLine, BIPDataSet ds)
        {
            //======== 1. 拆分为多个数据项 ================
            string[] arrValues = FileToDataSet_SplitToMultiValue(sCSVDataLine);

            //========= 2. 如果未增加字段，则增加字段 ===========
            if (ds.GetFieldCount() == 0)
            {
                for (int i = 0; i < arrValues.Length; i++)
                {
                    string sValue = arrValues[i];

                    ds.AddField(sValue);
                }
            }
            //========== 3. 否则增加数据记录 ============
            else
            {
                int nAddToRecord = ds.GetRecordCount() + 1;

                for (int i = 0; i < arrValues.Length; i++)
                {
                    string sValue = arrValues[i];
                    ds.SetItemString(nAddToRecord, i, sValue);
                }
            }
        }

        /// <summary>
        /// 将CSV的一行分割成若干单元
        /// </summary>
        /// <param name="sCSVDataLine">CSV的行</param>
        /// <returns>若干单元的数组</returns>
        private static string[] FileToDataSet_SplitToMultiValue(string sCSVDataLine)
        {
            ArrayList arrValues = new ArrayList();

            //=========== 1. 用逗号分开一行，得到各项 =============
            string[] arrCommaItems = sCSVDataLine.Split(',');

            //=========== 2. 循环处理每一项 ==========
            string sCellData = "";

            for (int i = 0; i < arrCommaItems.Length; i++)
            {
                string sCommaItem = arrCommaItems[i];

                //=============== 3. 判断之前的引号数量是否偶数 ===========
                if (sCellData == "")
                    sCellData = sCommaItem;
                else
                    sCellData += "," + sCommaItem;

                int nQuotaCount = StringUtil.Occurs("\"", sCellData);

                if (nQuotaCount % 2 == 0)
                {
                    arrValues.Add(FileToDataSet_RegulateCellData(sCellData));
                    sCellData = "";
                }
            }

            //=============== x. 返回 =============
            string[] arrRetValues = ArrayUtil.ToStringArray(arrValues);
            return arrRetValues;
        }

        /// <summary>
        /// 规整单元的数据信息
        /// </summary>
        /// <param name="sCellData">单元的数据信息</param>
        /// <returns>规整后的数据。主要是规整引号的内容。</returns>
        private static string FileToDataSet_RegulateCellData(string sCellData)
        {
            //============ 1. 是否以奇数个引号开始 =============
            int nStartQuotaCount = 0;
            bool bStartWithQuota = false;

            for (int i = 0; i < sCellData.Length; i++)
            {
                char cStart = sCellData[i];
                if (cStart == '\"')
                    nStartQuotaCount++;
                else
                    break;
            }

            if (nStartQuotaCount % 2 == 1)
                bStartWithQuota = true;

            //=========== 2. 是否以引号结尾 ============
            bool bEndWithQuota = false;
            if (sCellData.EndsWith("\""))
                bEndWithQuota = true;

            if (sCellData == "\"\"")
            {
                bStartWithQuota = true;
                bEndWithQuota = true;
            }

            //============ 3. 去掉前后的引号 ===========
            string sRet = sCellData;

            if (sRet.Length >= 2 && bStartWithQuota && bEndWithQuota)
                sRet = sRet.Substring(1, sRet.Length - 2);

            //============ 4. 双引号变为单引号 ============
            sRet = sRet.Replace("\"\"", "\"");

            return sRet;
        }
        #endregion

        #region 写入
        /// <summary>
        /// 将数据集写入文件
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <param name="sFileName">文件名</param>
        public static void DataSetToFile(BIPDataSet ds, string sFileName, bool bInPlainFormat)
        {
            ArrayList arrFileLines = new ArrayList();

            //======== 1. 加入标题 ===========
            string sFieldLine = DataSetToFile_GetFieldLine(ds, bInPlainFormat);
            arrFileLines.Add(sFieldLine);

            //========= 2. 加入数据 ===============
            for (int i = 1; i <= ds.GetRecordCount(); i++)
            {
                string sDataLine = DataSetToFile_GetDataLine(ds, i, bInPlainFormat);
                arrFileLines.Add(sDataLine);
            }

            //============ 3. 写入文件 =============
            FileUtil.WriteStringListToFile(sFileName, arrFileLines);
        }

        /// <summary>
        /// 得到字段定义的一行
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <returns>字段定义行</returns>
        private static string DataSetToFile_GetFieldLine(BIPDataSet ds, bool bInPlainFormat)
        {
            ArrayList arrItems = new ArrayList();

            for (int i = 0; i < ds.GetFieldCount(); i++)
            {
                string sFieldName = ds.GetFieldName(i);
                arrItems.Add(sFieldName);
            }

            string sRet = DataSetToFile_GetLineOfItems(arrItems, bInPlainFormat);
            return sRet;
        }

        /// <summary>
        /// 得到一行数据
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <param name="nRecordNo">记录号</param>
        /// <returns>数据行</returns>
        private static string DataSetToFile_GetDataLine(BIPDataSet ds, int nRecordNo, bool bInPlainFormat)
        {
            ArrayList arrItems = new ArrayList();

            for (int i = 0; i < ds.GetFieldCount(); i++)
            {
                string sDataValue = ds.GetItemString(nRecordNo, i);
                arrItems.Add(sDataValue);
            }

            string sRet = DataSetToFile_GetLineOfItems(arrItems, bInPlainFormat);
            return sRet;
        }

        /// <summary>
        /// 由各个数据项，得到一行数据
        /// </summary>
        /// <param name="arrItems">数据项的数组</param>
        /// <returns>一行数据</returns>
        private static string DataSetToFile_GetLineOfItems(ArrayList arrItems, bool bInPlainFormat)
        {
            string sRet = "";

            for (int i = 0; i < arrItems.Count; i++)
            {
                string sItem = (string)arrItems[i];
                string sRegulateItem = DataSetToFile_RegulateItemValue(sItem, bInPlainFormat);

                if (i != 0)
                    sRet += ",";

                sRet += sRegulateItem;
            }

            return sRet;
        }

        /// <summary>
        /// 规整一个数据项
        /// </summary>
        /// <param name="sItemValue">数据项</param>
        /// <returns>规整结果</returns>
        private static string DataSetToFile_RegulateItemValue(string sItemValue, bool bInPlainFormat)
        {
            if (bInPlainFormat)
                sItemValue = DataSetToFile_RegulateItemValue_PlainText(sItemValue);

            string sRet = sItemValue.Replace("\"", "\"\"");

            if (sItemValue.IndexOf(",") >= 0 || sItemValue.IndexOf("\n") >= 0 || sItemValue.IndexOf("\r") >= 0 
                    || sItemValue.IndexOf("\"") >= 0)
                sRet = "\"" + sRet + "\"";

            return sRet;
        }

        private static string DataSetToFile_RegulateItemValue_PlainText(string sItemValue)
        {
            //=========== 1. 去掉回车换行 ==============
            string sRet = sItemValue.Replace("\r", "");
            sRet = sRet.Replace("\n", "");

            //========== 2. 双引号变单引号============
            sRet = sRet.Replace("\"", "'");

            //========== 3. 半角逗号变汉字的全角逗号 ============
            sRet = sRet.Replace(",", "，");

            return sRet;
        }

        #endregion
    }
}
