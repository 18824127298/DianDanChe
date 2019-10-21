using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;

namespace Sigbit.Net.BIPPacket
{
    /// <summary>
    /// 结果集
    /// </summary>
    public class BIPDataSet
    {
        #region 赋值
        /// <summary>
        /// 2012赋值
        /// </summary>
        /// <param name="dsSrc">源数据集</param>
        public void AssignBy(BIPDataSet dsSrc)
        {
            //=========== 1. 清空当前的数据集 =============
            this.Clear();

            //========= 2. 结果集名称 ============
            this.DataSetName = dsSrc.DataSetName;

            //============ 3. 字段 =============
            for (int i = 0; i < dsSrc.GetFieldCount(); i++)
            {
                string sFieldName = dsSrc.GetFieldName(i);
                BIPFieldType ftFieldType = dsSrc.GetFieldType(i);
                int nFieldLength = dsSrc.GetFieldLength(i);
                int nFieldPrecision = dsSrc.GetFieldPrecision(i);

                this.AddField(sFieldName, ftFieldType, nFieldLength, nFieldPrecision);
            }

            //=========== 4. 数据 ===============
            for (int rec = 1; rec <= dsSrc.GetRecordCount(); rec++)
            {
                for (int fld = 0; fld < dsSrc.GetFieldCount(); fld++)
                {
                    string sItemValue = dsSrc.GetItemString(rec, fld);
                    this.SetItemString(rec, fld, sItemValue);
                }
            }
        }
        #endregion

        #region 属性
        string _dataSetName = "";
        /// <summary>
        /// 结果集名称
        /// </summary>
        public string DataSetName
        {
            get { return _dataSetName; }
            set { _dataSetName = value; }
        }

        int _dataSetSeq = 0;
        /// <summary>
        /// 结果集的顺序号
        /// </summary>
        public int DataSetSeq
        {
            get { return _dataSetSeq; }
            set { _dataSetSeq = value; }
        }

        BIPFieldList _bipFieldList = new BIPFieldList();
        BIPRecordList _bipRecordList = new BIPRecordList();
        #endregion 属性

        #region 构造及支持函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public BIPDataSet()
        {
            _dataSetSeq = 0;
        }

        /// <summary>
        /// 清除DataSet中的所有字段及值
        /// </summary>
        public void Clear()
        {
            _bipFieldList.Clear();
            _bipRecordList.Clear();
        }

        /// <summary>
        /// 得到记录数
        /// </summary>
        /// <returns>记录数</returns>
        public int GetRecordCount()
        {
            return _bipRecordList.Count;
        }

        /// <summary>
        /// 忽略项
        /// </summary>
        public bool IgnoreCase
        {
            get
            { return _bipFieldList.IgnoreCase; }
            set
            { _bipFieldList.IgnoreCase = value; }
        }
        #endregion 构造及支持函数

        #region 字段
        /// <summary>
        /// 得到字段数
        /// </summary>
        /// <returns>字段数</returns>
        public int GetFieldCount()
        {
            return _bipFieldList.Count;
        }

        /// <summary>
        /// 增加字段
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <param name="fieldType">字段类型</param>
        /// <param name="nFieldLength">字段长度</param>
        /// <param name="nFieldPrecision">字段精度</param>
        /// <remarks>
        /// 缺省情况下，fieldType为字符型，字段长度不定义。如果字段名称
        /// 已经存在，则抛出例外。指定精度的情况下，如果fieldType不是数
        /// 字类型，则抛出例外。
        /// </remarks>
        public void AddField(string sFieldName, BIPFieldType fieldType,
                int nFieldLength, int nFieldPrecision)
        {
            if (sFieldName != "")
            {
                if (_bipFieldList.LocateFieldName(sFieldName) != -1)
                    throw new BIPCallException("BIPDataSet::AddField() : "
                            + "Field already exists - " + sFieldName);
            }

            BIPField bipField = new BIPField();
            bipField.FieldName = sFieldName;
            bipField.FieldType = fieldType;
            bipField.FieldLength = nFieldLength;
            bipField.FieldPrecision = nFieldPrecision;

            if (nFieldPrecision != 0 && fieldType != BIPFieldType.Number)
                throw new BIPCallException("BIPDataSet::AddField() : "
                        + "field_precision != 0 of not number field_type - "
                        + sFieldName);

            _bipFieldList.AddField(bipField);
        }

        /// <summary>
        /// 增加字段
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <param name="fieldType">字段类型</param>
        /// <param name="nFieldLength">字段长度</param>
        public void AddField(string sFieldName, BIPFieldType fieldType,
                int nFieldLength)
        {
            AddField(sFieldName, fieldType, nFieldLength, 0);
        }

        /// <summary>
        /// 增加字段
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <param name="fieldType">字段类型</param>
        public void AddField(string sFieldName, BIPFieldType fieldType)
        {
            AddField(sFieldName, fieldType, 0, 0);
        }

        /// <summary>
        /// 增加字段
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        public void AddField(string sFieldName)
        {
            AddField(sFieldName, BIPFieldType.Char, 0, 0);
        }

        /// <summary>
        /// 以空名称的方式增加指定数量的字段
        /// </summary>
        /// <param name="nFieldCount">字段数量</param>
        public void AddFields(int nFieldCount)
        {
            for (int i = 0; i < nFieldCount; i++)
            {
                AddField("");
            }
        }

        /// <summary>
        /// 得到指定字段的名称
        /// </summary>
        /// <param name="nSeq">字段序号</param>
        /// <returns>字段的名称</returns>
        public string GetFieldName(int nSeq)
        {
            if (nSeq < 0 || nSeq >= GetFieldCount())
                throw new BIPCallException("BIPDataSet::GetFieldName() : "
                        + "Invalid FieldSeq - " + nSeq.ToString());

            return _bipFieldList.GetField(nSeq).FieldName;
        }

        /// <summary>
        /// 得到指定字段的类型
        /// </summary>
        /// <param name="nSeq">字段序号</param>
        /// <returns>字段类型</returns>
        public BIPFieldType GetFieldType(int nSeq)
        {
            if (nSeq < 0 || nSeq >= GetFieldCount())
                throw new BIPCallException("BIPDataSet::GetFieldType() : "
                        + "Invalid FieldSeq - " + nSeq.ToString());

            return _bipFieldList.GetField(nSeq).FieldType;
        }

        /// <summary>
        /// 得到指定字段的长度
        /// </summary>
        /// <param name="nSeq">字段序号</param>
        /// <returns>字段长度</returns>
        public int GetFieldLength(int nSeq)
        {
            if (nSeq < 0 || nSeq >= GetFieldCount())
                throw new BIPCallException("BIPDataSet::GetFieldLength() : "
                        + "Invalid FieldSeq - " + nSeq.ToString());

            return _bipFieldList.GetField(nSeq).FieldLength;
        }

        /// <summary>
        /// 得到指定字段的精度
        /// </summary>
        /// <param name="nSeq">字段序号</param>
        /// <returns>字段精度</returns>
        public int GetFieldPrecision(int nSeq)
        {
            if (nSeq < 0 || nSeq >= GetFieldCount())
                throw new BIPCallException("BIPDataSet::GetFieldPrecision() : "
                        + "Invalid FieldSeq - " + nSeq.ToString());

            return _bipFieldList.GetField(nSeq).FieldPrecision;
        }

        /// <summary>
        /// 得到指定名称的字段序号
        /// </summary>
        /// <param name="sFieldName">字段名称</param>
        /// <returns>字段序号</returns>
        public int GetFieldSeq(string sFieldName)
        {
            int nFieldSeq;
            nFieldSeq = _bipFieldList.LocateFieldName(sFieldName);

            if (nFieldSeq == -1)
                throw new BIPCallException("BIPDataSet::GetFieldSeq() : "
                        + "Cannot locate the field name - " + sFieldName);

            return nFieldSeq;
        }
        #endregion 字段

        #region 获取数据
        /// <summary>
        /// 按指定记录序号，字段序号来获取字段字符串值
        /// </summary>
        /// <param name="nRecordNum">记录序号</param>
        /// <param name="nFieldSeq">字段序号</param>
        /// <returns>字段字符串值</returns>
        public string GetItemString(int nRecordNum, int nFieldSeq)
        {
            if (nFieldSeq >= GetFieldCount())
                throw new BIPCallException("TCBIPDataSet::GetItemString() : "
                        + "field_seq out of range - " + nFieldSeq.ToString());

            if (nRecordNum > GetRecordCount() + 1)
                throw new BIPCallException("TCBIPDataSet::GetItemString() : "
                        + "record_num out of range - " + nRecordNum.ToString());

            return _bipRecordList.GetItemString(nRecordNum, nFieldSeq);
        }

        /// <summary>
        /// 按指定记录序号，字段名来获取字段字符串值
        /// </summary>
        /// <param name="nRecordNum">记录序号</param>
        /// <param name="sFieldName">字段名</param>
        /// <returns>字段字符串值</returns>
        public string GetItemString(int nRecordNum, string sFieldName)
        {
            return GetItemString(nRecordNum, GetFieldSeq(sFieldName));
        }

        /// <summary>
        /// 按指定记录序号，字段序号来获取字段整型值
        /// </summary>
        /// <param name="nRecordNum">记录序号</param>
        /// <param name="nFieldSeq">字段序号</param>
        /// <returns>字段整型值</returns>
        public int GetItemInteger(int nRecordNum, int nFieldSeq)
        {
            return Convert.ToInt32(GetItemString(nRecordNum, nFieldSeq));
        }

        /// <summary>
        /// 按指定记录序号，字段名来获取字段整型值
        /// </summary>
        /// <param name="nRecordNum">记录序号</param>
        /// <param name="sFieldName">字段名</param>
        /// <returns>字段整型值</returns>
        public int GetItemInteger(int nRecordNum, string sFieldName)
        {
            return Convert.ToInt32(GetItemString(nRecordNum, sFieldName));
        }

        /// <summary>
        /// 按指定记录序号，字段序号来获取字段双精度值
        /// </summary>
        /// <param name="nRecordNum">记录序号</param>
        /// <param name="nFieldSeq">字段序号</param>
        /// <returns>字段双精度值</returns>
        public double GetItemDouble(int nRecordNum, int nFieldSeq)
        {
            return Convert.ToDouble(GetItemString(nRecordNum, nFieldSeq));
        }

        /// <summary>
        /// 按指定记录序号，字段名来获取字段双精度值
        /// </summary>
        /// <param name="nRecordNum">记录序号</param>
        /// <param name="sFieldName">字段名</param>
        /// <returns>字段双精度值</returns>
        public double GetItemDouble(int nRecordNum, string sFieldName)
        {
            return Convert.ToDouble(GetItemString(nRecordNum, sFieldName));
        }

        /// <summary>
        /// 得到第一条记录的字段字符串值
        /// </summary>
        /// <param name="nFieldSeq">字段序号</param>
        /// <returns>字段字符串值</returns>
        public string GetAStringValue(int nFieldSeq)
        {
            return GetItemString(1, nFieldSeq);
        }

        /// <summary>
        /// 得到第一条记录的字段字符串值
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <returns>字段字符串值</returns>
        public string GetAStringValue(string sFieldName)
        {
            return GetItemString(1, sFieldName);
        }

        /// <summary>
        /// 得到第一条记录的字段整型值
        /// </summary>
        /// <param name="nFieldSeq">字段序号</param>
        /// <returns>字段整型值</returns>
        public int GetAIntegerValue(int nFieldSeq)
        {
            return GetItemInteger(1, nFieldSeq);
        }

        /// <summary>
        /// 得到第一条记录的字段整型值
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <returns>字段整型值</returns>
        public int GetAIntegerValue(string sFieldName)
        {
            return GetItemInteger(1, sFieldName);
        }

        /// <summary>
        /// 得到第一条记录的字段双精度值
        /// </summary>
        /// <param name="nFieldSeq">字段序号</param>
        /// <returns>字段双精度值</returns>
        public double GetADoubleValue(int nFieldSeq)
        {
            return GetItemDouble(1, nFieldSeq);
        }

        /// <summary>
        /// 得到第一条记录的字段双精度值
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <returns>字段双精度值</returns>
        public double GetADoubleValue(string sFieldName)
        {
            return GetItemDouble(1, sFieldName);
        }
        #endregion 获取数据

        #region 设置数据
        /// <summary>
        /// 按指定记录序号，字段序号来设置字段值
        /// </summary>
        /// <param name="nRecordNum">记录序号</param>
        /// <param name="nFieldSeq">字段序号</param>
        /// <param name="sItemValue">字段值</param>
        /// <remarks>
        /// 按指定记录序号，字段序号来设置字段值。如果字段序号越界，则
        /// 抛出例外。如果记录号超过记录总数，则自动增加记录；但如果指
        /// 定的记录号大于(记录总数+1)，则抛出例外。
        /// </remarks>
        public void SetItemString(int nRecordNum, int nFieldSeq, string sItemValue)
        {
            if (nFieldSeq >= GetFieldCount())
                throw new BIPCallException("BIPDataSet::SetItemString() : "
                        + "field_seq out of range - " + nFieldSeq.ToString());

            if (nRecordNum > GetRecordCount() + 1)
                throw new BIPCallException("BIPDataSet::SetItemString() : "
                        + "record_num out of range - " + nRecordNum.ToString());

            if (nRecordNum == GetRecordCount() + 1)
                _bipRecordList.AddRecord();

            _bipRecordList.SetItemString(nRecordNum, nFieldSeq, sItemValue);
        }

        /// <summary>
        /// 按指定记录序号，字段名来设置字段值
        /// </summary>
        /// <param name="nRecordNum">记录序号</param>
        /// <param name="sFieldName">字段名</param>
        /// <param name="sItemValue">字段值</param>
        public void SetItemString(int nRecordNum, string sFieldName, string sItemValue)
        {
            SetItemString(nRecordNum, GetFieldSeq(sFieldName), sItemValue);
        }

        /// <summary>
        /// 按字段序号设置第一条记录的字段值
        /// </summary>
        /// <param name="nFieldSeq">字段序号</param>
        /// <param name="sItemValue">字段值</param>
        /// <remarks>
        /// 如果字段序号越界，则增加字段。该方法仅能用于一条记录的情况，
        /// 如果记录数不足一条，则增加之，如果记录数大于一条，则抛出例外。
        /// </remarks>
        public void SetAStringValue(int nFieldSeq, string sItemValue)
        {
            if (GetRecordCount() > 1)
                throw new BIPCallException("BIPDataSet.SetAStringValue() : "
                        + "This function can only called with one record dataset");

            if (GetRecordCount() == 0)
                _bipRecordList.AddRecord();

            if (nFieldSeq >= GetFieldCount())
                AddFields(nFieldSeq + 1 - GetFieldCount());

            SetItemString(1, nFieldSeq, sItemValue);
        }

        /// <summary>
        /// 按字段名设置第一条记录的字段值
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <param name="sItemValue">字段值</param>
        /// <remarks>如果字段名不存在，则增加一个为指定字段名的字段。</remarks>
        public void SetAStringValue(string sFieldName, string sItemValue)
        {
            if (GetRecordCount() > 1)
                throw new BIPCallException("TCBIPDataSet::SetAStringValue() : "
                        + "This function can only called with one record dataset");

            if (GetRecordCount() == 0)
                _bipRecordList.AddRecord();

            if (_bipFieldList.LocateFieldName(sFieldName) == -1)
                AddField(sFieldName);

            SetItemString(1, sFieldName, sItemValue);
        }

        /// <summary>
        /// 增加字段值
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <param name="sItemValue">字段值</param>
        /// 说明 : 在最后增加一个字段，并记录该字段的值。如果字段名称与
        /// 已经存在的相重，则抛出例外。该方法仅能用于一条记录的情况，
        /// 如果记录数不足一条，则增加之，如果记录数大于一条，则抛出例外。
        public void AddAStringValue(string sFieldName, string sItemValue)
        {
            if (GetRecordCount() > 1)
                throw new BIPCallException("TCBIPDataSet::AddAStringValue() : "
                        + "This function can only called with one record dataset");

            if (GetRecordCount() == 0)
                _bipRecordList.AddRecord();

            if (_bipFieldList.LocateFieldName(sFieldName) != -1)
                throw new BIPCallException("TCBIPDataSet::AddAStringValue() : "
                        + "field_name already exists.");

            AddField(sFieldName);
            SetItemString(1, sFieldName, sItemValue);
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="nRecordNum">记录序号</param>
        void DeleteRecord(int nRecordNum)
        {
            if (nRecordNum < 1 || nRecordNum > GetRecordCount())
                throw new BIPCallException("TCBIPDataSet::DeleteRecord() : "
                        + "record_num out of range - " + nRecordNum.ToString());

            _bipRecordList.RemoveAt(nRecordNum - 1);
        }
        #endregion 设置数据

        #region 结构及数据显示
        /// <summary>
        /// 得到显示结构的字符串
        /// </summary>
        /// <returns>显示结构的字符串</returns>
        public string GetDisplayStruct()
        {
            string sRet = "";
            string sLine;

            for (int i = 0; i < GetFieldCount(); i++)
            {
                sLine = GetFieldName(i).PadRight(16) + " ";
                sLine += (char)GetFieldType(i) + "    ";
                sLine += GetFieldLength(i).ToString().PadRight(6) + " ";
                sLine += GetFieldPrecision(i).ToString().PadRight(6);

                sRet += sLine;
            }

            return sRet;
        }

        /// <summary>
        /// 获取显示的文本内容
        /// </summary>
        /// <returns></returns>
        public string GetDisplayContentText()
        {
            string sContent = "";
            string sLine;

            //======== 0. 显示标识 ============
            sLine = "<<DATASET>> NAME=" + DataSetName;
            sLine += "   SEQ=" + _dataSetSeq.ToString();
            sContent += sLine + "\r\n";

            //======== 1. 显示字段 ==========
            int i;
            int nFieldSeq;

            sLine = "";
            for (i = 0; i < GetFieldCount(); i++)
            {
                if (i != 0)
                    sLine += "\t";
                sLine += GetFieldName(i);
            }
            sContent += sLine + "\r\n";

            //========= 2. 显示分隔 =========
            sLine = StringUtil.RepeatChar('-', 70);
            sContent += sLine + "\r\n";

            //======== 3. 显示内容 =========
            for (i = 1; i <= GetRecordCount(); i++)
            {
                sLine = "";
                for (nFieldSeq = 0; nFieldSeq < GetFieldCount(); nFieldSeq++)
                {
                    if (nFieldSeq != 0)
                        sLine += "\t";
                    sLine += GetItemString(i, nFieldSeq);
                }
                sContent += sLine + "\r\n";
            }

            return sContent;
        }
        #endregion 结构及数据显示

        #region 解码数据
        /// <summary>
        /// 从字节数组中读取DataSet
        /// </summary>
        /// <param name="bsDataSet">字节数组</param>
        /// <param name="nPos">起始位置</param>
        public void ReadFrom(byte[] bsDataSet, ref int nPos)
        {
            Clear();

            //========== 1. 数据块标识	N(1)	固定为"BE" ==========
            byte cDataSetHeaderID;
            cDataSetHeaderID = BIPUtil.RXNByte(bsDataSet, ref nPos);
            if (cDataSetHeaderID != 0xBE)
                throw new BIPFormatException("TCBIPDataSet::ReadFrom() : "
                        + "DataSetHeaderID Error.");

            //======== 2. 数据块长度	N(4)	============
            int nPureDataSetLength = BIPUtil.RXNLongNumber(bsDataSet, ref nPos);

            if (bsDataSet.Length < nPureDataSetLength + 1 + 4)
                throw new BIPFormatException("TCBIPDataSet::ReadFrom() : "
                        + "data_set_length exceed - " + nPureDataSetLength.ToString());

            //======== 3. 分析数据区 ==============
            byte[] bsPureData = BIPUtil.RXNByLength(bsDataSet, ref nPos, nPureDataSetLength);

            RXNUnpackData(bsPureData);
        }

        /// <summary>
        /// 读取去掉头尾标记的字符串到DataSet
        /// </summary>
        /// <param name="bsPureData">去掉头尾标记的字符串</param>
        private void RXNUnpackData(byte[] bsPureData)
        {
            int nPos = 0;

            //========= 1. 数据块序号	N(1) ===========
            _dataSetSeq = BIPUtil.RXNByteNumber(bsPureData, ref nPos);

            //======= 2. 数据块名称	X(16) ===========
            _dataSetName = BIPUtil.RXNStringByLength(bsPureData, ref nPos, 16);
            _dataSetName = _dataSetName.TrimEnd();

            //======= 3. 字段数	N(2) ==========
            int nFieldCount = BIPUtil.RXNShortNumber(bsPureData, ref nPos);

            //======= 4. 字段区长度	N(4) ===========
            int nFieldSectionLength = BIPUtil.RXNLongNumber(bsPureData, ref nPos);

            //========= 5. 记录数	N(4) ==========
            int nRecordCount = BIPUtil.RXNLongNumber(bsPureData, ref nPos);

            //======= 6. 记录区长度	N(4) =========
            int nRecordSectionLength = BIPUtil.RXNLongNumber(bsPureData, ref nPos);
            if (nPos + nFieldSectionLength + nRecordSectionLength
                    != bsPureData.Length)
                throw new BIPFormatException("TCBIPDataSet::RXNUnpackData() : "
                        + "two section length not match - " + nFieldSectionLength.ToString()
                        + " - " + nRecordSectionLength.ToString());

            //=========== 7. 字段区、记录区 ==========
            byte[] bsFieldSectionData
                    = BIPUtil.RXNByLength(bsPureData, ref nPos, nFieldSectionLength);
            byte[] bsRecordSectionData
                    = BIPUtil.RXNByLength(bsPureData, ref nPos, nRecordSectionLength);

            RXNFieldSection(bsFieldSectionData, nFieldCount);
            RXNRecordSection(bsRecordSectionData, nRecordCount);
        }

        /// <summary>
        /// 读取字段区
        /// </summary>
        /// <param name="bsFieldSectionData">字段区</param>
        /// <param name="nFieldCount">字段数量</param>
        private void RXNFieldSection(byte[] bsFieldSectionData, int nFieldCount)
        {
            int nPos = 0;
            long nFieldNum;
            string sFieldName;

            for (int i = 0; i < nFieldCount; i++)
            {
                //======== 1. 字段序号 N(2) =========
                nFieldNum = BIPUtil.RXNShortNumber(bsFieldSectionData, ref nPos);
                if (nFieldNum != i + 1)
                    throw new BIPFormatException("TCBIPDataSet::RXNFieldSection() : "
                            + "Field No not match.");

                //======= 2. 字段名 STR ==========
                sFieldName = BIPUtil.RXNString(bsFieldSectionData, ref nPos);

                //======== 3. 字段类型 X(1) =======
                BIPFieldType fieldType
                        = (BIPFieldType)(BIPUtil.RXNByte(bsFieldSectionData, ref nPos));

                if (fieldType != BIPFieldType.Number && fieldType != BIPFieldType.Char)
                    throw new BIPFormatException("TCBIPDataSet::RXNFieldSection() : "
                            + "unknow field_type");

                //======= 4. 字段定义 Z(1) ==========
                int nFieldLength;
                int nFieldPrecision;

                int nFieldProperty = BIPUtil.RXNByteNumber(bsFieldSectionData, ref nPos);

                if (nFieldProperty == 0)
                {
                    nFieldLength = 0;
                    nFieldPrecision = 0;
                }
                else
                {
                    if (nFieldProperty != 1)
                        throw new BIPFormatException("TCBIPDataSet::RXNFieldSection() : "
                                + "Unknow field_property.");

                    //========= 5. 字段长度（Option）	N(4) =========
                    if (fieldType == BIPFieldType.Char)
                    {
                        nFieldLength = BIPUtil.RXNLongNumber(bsFieldSectionData, ref nPos);
                        nFieldPrecision = 0;
                    }
                    else
                    {
                        nFieldLength = BIPUtil.RXNShortNumber(bsFieldSectionData, ref nPos);
                        nFieldPrecision = BIPUtil.RXNShortNumber(bsFieldSectionData, ref nPos);
                    }
                }

                AddField(sFieldName, fieldType, nFieldLength, nFieldPrecision);
            }
        }

        /// <summary>
        /// 读取记录区
        /// </summary>
        /// <param name="bsRecordSectionData">记录区</param>
        /// <param name="nRecordCount">记录数量</param>
        private void RXNRecordSection(byte[] bsRecordSectionData, int nRecordCount)
        {
            int nPos = 0;

            for (int i = 1; i <= nRecordCount; i++)
            {
                RXNRecordSection__OneRecord(bsRecordSectionData, ref nPos, i);
            }

            if (nPos != bsRecordSectionData.Length)
                throw new BIPFormatException("TCBIPDataSet::RXNRecordSection() : "
                        + "unexpected data left");
        }

        /// <summary>
        /// 读取记录区的一条记录
        /// </summary>
        /// <param name="bsRecordSectionData">记录区</param>
        /// <param name="nPos">当前位置</param>
        /// <param name="nRecordNum">记录序号</param>
        private void RXNRecordSection__OneRecord(byte[] bsRecordSectionData,
                ref int nPos, int nRecordNum)
        {
            //======== 1. 记录序号 ========
            int nFetchedRecordNum = BIPUtil.RXNLongNumber(bsRecordSectionData, ref nPos);
            if (nFetchedRecordNum != nRecordNum)
                throw new BIPFormatException("TCBIPDataSet::RXNRecordSection__OneRecord() : "
                        + "record_num mismatched.");

            //====== 2. 记录长度 ===========
            long nRecordLength;
            long nValueBeginPos;

            nRecordLength = BIPUtil.RXNLongNumber(bsRecordSectionData, ref nPos);
            nValueBeginPos = nPos;

            //========= 3. 字段数据 ========
            string sValue;

            for (int i = 0; i < GetFieldCount(); i++)
            {
                sValue = BIPUtil.RXNString(bsRecordSectionData, ref nPos);

                SetItemString(nRecordNum, i, sValue);
            }

            if (nValueBeginPos + nRecordLength != nPos)
                throw new BIPFormatException("TCBIPDataSet::RXNRecordSection__OneRecord() : "
                        + "record_length is not correct.");
        }
        #endregion 解码数据

        /// <summary>
        /// 得到数据集的字节数组
        /// </summary>
        /// <returns>数据集的字节数组</returns>
        public byte[] ToBytes()
        {
            BIPBytesBuilder bb = new BIPBytesBuilder();

            byte[] bsData = WXNData();

            ////========== 1. 数据块标识	N(1)	固定为"BE" ==========
            bb.AddByte(0xBE);

            //======== 2. 数据块长度	N(4)	============
            bb.AddLongNumber(bsData.Length);

            //======== 3. 数据 ===========
            bb.Add(bsData);

            return bb.ToBytes();
        }

        /// <summary>
        /// 得到数据集中的数据内容
        /// </summary>
        /// <returns>数据集数据的字节数组</returns>
        private byte[] WXNData()
        {
            BIPBytesBuilder bb = new BIPBytesBuilder();

            //========= 1. 数据块序号	N(1) ===========
            bb.AddByteNumber(_dataSetSeq);

            //======= 2. 数据块名称	X(16) ===========
            if (_dataSetName.Length > 16)
                throw new BIPCallException("TCBIPDataSet::WXNDataString() : "
                        + "DataSetName is too long - " + _dataSetName);
            bb.AddPureString(_dataSetName.PadRight(16));

            //======= 3. 字段数	N(2) ==========
            bb.AddShortNumber(GetFieldCount());

            //======= 4. 字段区长度	N(4) ===========
            byte[] bsFieldData = WXNFieldData();
            bb.AddLongNumber(bsFieldData.Length);

            //========= 5. 记录数	N(4) ==========
            bb.AddLongNumber(GetRecordCount());

            //======= 6. 记录区长度	N(4) =========
            byte[] bsRecordData = WXNRecordData();
            bb.AddLongNumber(bsRecordData.Length);

            //======= 7. 字段区 ==========
            bb.Add(bsFieldData);

            //======= 8. 记录区 ========
            bb.Add(bsRecordData);

            return bb.ToBytes();
        }

        /// <summary>
        /// 得到字段区数据
        /// </summary>
        /// <returns>字段区数据</returns>
        private byte[] WXNFieldData()
        {
            BIPBytesBuilder bb = new BIPBytesBuilder();
            int nFieldLength;

            for (int i = 0; i < GetFieldCount(); i++)
            {
                //======== 1. 字段序号	N(2) ========
                bb.AddShortNumber(i + 1);

                //======= 2. 字段名 STR ==========
                bb.AddString(GetFieldName(i));

                //======== 3. 字段类型 X(1) =======
                bb.AddByte((byte)GetFieldType(i));

                //======= 4. 字段定义 Z(1) ==========
                nFieldLength = GetFieldLength(i);
                if (nFieldLength == 0)
                {
                    bb.AddByteNumber(0);
                }
                else
                {
                    bb.AddByteNumber(1);

                    //========= 5. 字段长度（Option）	N(4) =========
                    if (GetFieldType(i) == BIPFieldType.Number)
                    {
                        bb.AddShortNumber(nFieldLength);
                        bb.AddShortNumber(GetFieldPrecision(i));
                    }
                    else
                        bb.AddLongNumber(nFieldLength);
                }
            } // end of for (i = 0; i < GetFieldCount(); i++)

            return bb.ToBytes();
        }

        /// <summary>
        /// 得到记录区数据
        /// </summary>
        /// <returns>记录区数据</returns>
        private byte[] WXNRecordData()
        {
            BIPBytesBuilder bb = new BIPBytesBuilder();

            for (int i = 1; i <= GetRecordCount(); i++)
                bb.Add(WXNRecordData_OneRecord(i));

            return bb.ToBytes();
        }

        /// <summary>
        /// 得到一条记录的BIP数据表示
        /// </summary>
        /// <param name="nRecordNum">记录序号</param>
        /// <returns>一条记录的BIP数据表示</returns>
        private byte[] WXNRecordData_OneRecord(int nRecordNum)
        {
            BIPBytesBuilder bb = new BIPBytesBuilder();

            //======== 1. 记录序号	N(4) ==========
            bb.AddLongNumber(nRecordNum);

            //========= 2. 记录长度	N(4) =========
            int nTotalLength = 0;
            for (int i = 0; i < GetFieldCount(); i++)
            {
                string sItem = GetItemString(nRecordNum, i);
                nTotalLength += BIPUtil.BIPStringBytes(sItem).Length;
            }
            bb.AddLongNumber(nTotalLength);

            //======= 3. 字段1数据	STR ==========
            for (int i = 0; i < GetFieldCount(); i++)
            {
                string sItem = GetItemString(nRecordNum, i);
                bb.AddString(sItem);
            }

            return bb.ToBytes();
        }
    }
}
