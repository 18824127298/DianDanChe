using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

using Sigbit.Common;

namespace Sigbit.Data.FoxDBF
{
    /// <summary>
    /// 记录的数据类型
    /// </summary>
    enum MFXRecordDataType
    {
        None,
        String,
        Int,
        Double,
        Bool,
        DataTime,
        Char
    }

    /// <summary>
    /// 缓冲一个字段的内容
    /// </summary>
    class MFXRecordDataListItem
    {
        MFXRecordDataType _dataType = MFXRecordDataType.None;
        /// <summary>
        /// 数据类型
        /// </summary>
        public MFXRecordDataType DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }

        private string _dataString = "";
        /// <summary>
        /// 字符串数据
        /// </summary>
        public string DataString
        {
            get { return _dataString; }
            set { _dataString = value; }
        }

        private int _dataInt = 0;
        /// <summary>
        /// 整型数据
        /// </summary>
        public int DataInt
        {
            get { return _dataInt; }
            set { _dataInt = value; }
        }

        private double _dataDouble = 0;
        /// <summary>
        /// 浮点数据
        /// </summary>
        public double DataDouble
        {
            get { return _dataDouble; }
            set { _dataDouble = value; }
        }

        private bool _dataBool = false;
        /// <summary>
        /// 布尔数据
        /// </summary>
        public bool DataBool
        {
            get { return _dataBool; }
            set { _dataBool = value; }
        }

        private DateTime _dataDateTime;
        /// <summary>
        /// 时间数据
        /// </summary>
        public DateTime DataDateTime
        {
            get { return _dataDateTime; }
            set { _dataDateTime = value; }
        }

        private char _dataChar;
        /// <summary>
        /// 字符数据
        /// </summary>
        public char DataChar
        {
            get { return _dataChar; }
            set { _dataChar = value; }
        }

        /// <summary>
        /// 得到记录的字节数组表示
        /// </summary>
        /// <param name="foxField">字段定义</param>
        /// <returns>字节数组</returns>
        public byte[] ToBytesOfField(FOXFIELD foxField)
        {
            if (foxField.FieldType == DBFFieldType.Char)
                return ToBytesOfField__Char(foxField);
            else if (foxField.FieldType == DBFFieldType.Date)
                return ToBytesOfField__Date(foxField);
            else if (foxField.FieldType == DBFFieldType.Logic)
                return ToBytesOfField__Logic(foxField);
            else if (foxField.FieldType == DBFFieldType.Number)
                return ToBytesOfField__Number(foxField);

            throw new Exception("字段类型不是字符、日期、数字、逻辑中的一种");
        }

        public byte[] ToBytesOfField__Char(FOXFIELD foxField)
        {
            if (this.DataType == MFXRecordDataType.Char)
            {
                byte[] bsRet = FOXUtil.FOXStringBytes("", foxField.FieldLength, ' ');
                bsRet[0] = (byte)this.DataChar;
                return bsRet;
            }
            else if (this.DataType == MFXRecordDataType.String)
            {
                byte[] bsRet = FOXUtil.FOXStringBytes(this.DataString, foxField.FieldLength, ' ');
                return bsRet;
            }
            else if (this.DataType == MFXRecordDataType.None)
            {
                byte[] bsRet = FOXUtil.FOXStringBytes("", foxField.FieldLength, ' ');
                return bsRet;
            }
            else
                throw new Exception("字符串类型，必须提供字符或字符串的数据");
        }

        public byte[] ToBytesOfField__Number(FOXFIELD foxField)
        {
            double fThisNumber;
            if (this.DataType == MFXRecordDataType.Int)
                fThisNumber = (double)this.DataInt;
            else if (this.DataType == MFXRecordDataType.Double)
                fThisNumber = this.DataDouble;
            else if (this.DataType == MFXRecordDataType.String)
            {
                if (foxField.FieldPoint == 0)
                    fThisNumber = ConvertUtil.ToInt(this.DataString.Trim());
                else
                    fThisNumber = ConvertUtil.ToFloat(this.DataString.Trim());
            }
            else if (this.DataType == MFXRecordDataType.None)
                fThisNumber = 0;
            else
                throw new Exception("数字类型，必须提供字符或字符串的数据");

            string sFormatString = "{0:f" + foxField.FieldPoint.ToString() + "}";
            string sRet = string.Format(sFormatString, fThisNumber);

            if (sRet.Length > foxField.FieldLength)
                throw new Exception("数字类型，装不下那么大的数字");

            for (int i = sRet.Length + 1; i <= foxField.FieldLength; i++)
                sRet = " " + sRet;

            byte[] bsRet = FOXUtil.FOXStringBytes(sRet);
            
            return bsRet;
        }

        public byte[] ToBytesOfField__Date(FOXFIELD foxField)
        {
            if (this.DataType == MFXRecordDataType.DataTime)
            {
                DateTime dt = this.DataDateTime;
                string sDTString = dt.Year.ToString("0000") + dt.Month.ToString("00") + dt.Day.ToString("00");
                byte[] bsRet = FOXUtil.FOXStringBytes(sDTString, foxField.FieldLength);
                return bsRet;
            }
            else if (this.DataType == MFXRecordDataType.String)
            {
                byte[] bsRet = FOXUtil.FOXStringBytes(this.DataString, foxField.FieldLength, ' ');
                return bsRet;
            }
            else if (this.DataType == MFXRecordDataType.None)
            {
                byte[] bsRet = FOXUtil.FOXStringBytes("", foxField.FieldLength, ' ');
                return bsRet;
            }
            else
                throw new Exception("日期类型，必须提供日期的数据");
        }

        public byte[] ToBytesOfField__Logic(FOXFIELD foxField)
        {
            byte[] bsRet = new byte[1];
            bsRet[0] = (byte)' ';
            if (this.DataType == MFXRecordDataType.Bool)
            {
                if (this.DataBool == true)
                    bsRet[0] = (byte)'T';
            }
            else if (this.DataType == MFXRecordDataType.String)
            {
                if (this.DataString.Length >= 1)
                {
                    if (this.DataString[0] == 'T')
                        bsRet[0] = (byte)'T';
                }
            }
            else if (this.DataType == MFXRecordDataType.None)
            {
            }
            else
                throw new Exception("逻辑类型，必须提供布尔的数据");

            return bsRet;
        }
    }

    /// <summary>
    /// 缓冲一条记录的内容
    /// </summary>
    class MFXRecordDataList
    {
        /// <summary>
        /// 各字段的数组
        /// </summary>
        ArrayList _arrDataItems;

        private bool _isDeletedRecord = false;
        /// <summary>
        /// 是否已删除记录
        /// </summary>
        public bool IsDeletedRecord
        {
            get { return _isDeletedRecord; }
            set { _isDeletedRecord = value; }
        }

        /// <summary>
        /// 得到一个字段数据
        /// </summary>
        /// <param name="nIndex">下标</param>
        /// <returns>字段数据</returns>
        public MFXRecordDataListItem GetItem(int nIndex)
        {
            return (MFXRecordDataListItem)_arrDataItems[nIndex];
        }

        private int _fieldCount = 0;
        /// <summary>
        /// 字段数
        /// </summary>
        public int FieldCount
        {
            get { return _fieldCount; }
            set { _fieldCount = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nFieldCount">字段数</param>
        public MFXRecordDataList(int nFieldCount)
        {
            _fieldCount = nFieldCount;

            Reset();
        }

        /// <summary>
        /// 清空缓冲内容
        /// </summary>
        /// <remarks>
        /// 在数据读取、更新前后调用
        /// </remarks>
        public void Reset()
        {
            _arrDataItems = new ArrayList();

            for (int i = 0; i < _fieldCount; i++)
            {
                MFXRecordDataListItem item = new MFXRecordDataListItem();
                _arrDataItems.Add(item);
            }
        }

        #region SetRecordData
        public void SetRecordData(int nFieldSeq, string sData)
        {
            MFXRecordDataListItem item = GetItem(nFieldSeq);
            item.DataType = MFXRecordDataType.String;
            item.DataString = sData;
        }

        public void SetRecordData(int nFieldSeq, int nData)
        {
            MFXRecordDataListItem item = GetItem(nFieldSeq);
            item.DataType = MFXRecordDataType.Int;
            item.DataInt = nData;
        }

        public void SetRecordData(int nFieldSeq, double fData)
        {
            MFXRecordDataListItem item = GetItem(nFieldSeq);
            item.DataType = MFXRecordDataType.Double;
            item.DataDouble = fData;
        }

        public void SetRecordData(int nFieldSeq, bool bData)
        {
            MFXRecordDataListItem item = GetItem(nFieldSeq);
            item.DataType = MFXRecordDataType.Bool;
            item.DataBool = bData;
        }

        public void SetRecordData(int nFieldSeq, DateTime dtData)
        {
            MFXRecordDataListItem item = GetItem(nFieldSeq);
            item.DataType = MFXRecordDataType.DataTime;
            item.DataDateTime = dtData;
        }

        public void SetRecordData(int nFieldSeq, char cData)
        {
            MFXRecordDataListItem item = GetItem(nFieldSeq);
            item.DataType = MFXRecordDataType.Char;
            item.DataChar = cData;
        }
        #endregion

        #region GetRecordData
        public string GetRecordString_WithoutTrim(int nFieldSeq)
        {
            MFXRecordDataListItem item = GetItem(nFieldSeq);
            if (item.DataType != MFXRecordDataType.String)
                return "";
            return item.DataString;
        }

        public string GetRecordString(int nFieldSeq)
        {
            MFXRecordDataListItem item = GetItem(nFieldSeq);
            if (item.DataType != MFXRecordDataType.String)
                return "";
            return item.DataString.TrimEnd(' ');
        }

        public int GetRecordInt(int nFieldSeq)
        {
            MFXRecordDataListItem item = GetItem(nFieldSeq);
            if (item.DataType != MFXRecordDataType.String)
                return 0;
            return ConvertUtil.ToInt(item.DataString.Trim());
        }

        public double GetRecordDouble(int nFieldSeq)
        {
            MFXRecordDataListItem item = GetItem(nFieldSeq);
            if (item.DataType != MFXRecordDataType.String)
                return 0;
            return ConvertUtil.ToFloat(item.DataString.Trim());
        }

        public bool GetRecordBool(int nFieldSeq)
        {
            MFXRecordDataListItem item = GetItem(nFieldSeq);
            if (item.DataType != MFXRecordDataType.String)
                return false;
            if (item.DataString == "T")
                return true;
            else
                return false;
        }

        public DateTime GetRecordDateTime(int nFieldSeq)
        {
            MFXRecordDataListItem item = GetItem(nFieldSeq);

            string sDT8;
            if (item.DataType != MFXRecordDataType.String)
                sDT8 = "20000101";
            else
                sDT8 = item.DataString;

            string sDT10 = sDT8.Substring(0, 4) + "-" + sDT8.Substring(4, 2)
                    + "-" + sDT8.Substring(6, 2);
            return DateTime.Parse(sDT10);
        }

        public char GetRecordChar(int nFieldSeq)
        {
            MFXRecordDataListItem item = GetItem(nFieldSeq);
            if (item.DataType != MFXRecordDataType.String)
                return ' ';
            return item.DataString[0];
        }
        #endregion

        /// <summary>
        /// 转换到字节数组
        /// </summary>
        /// <param name="fieldList">字段定义信息</param>
        /// <returns>字节数组</returns>
        public byte[] ToBytesOfFieldList(FOXFIELDList fieldList)
        {
            FOXBytesBuilder bytesBuilder = new FOXBytesBuilder();

            if (this.IsDeletedRecord)
                bytesBuilder.AddByte(0x2A);
            else
                bytesBuilder.AddChar(' ');

            for (int i = 0; i < this.FieldCount; i++)
            {
                MFXRecordDataListItem item = this.GetItem(i);
                FOXFIELD field = fieldList.GetField(i);

                byte[] bsFieldData = item.ToBytesOfField(field);
                bytesBuilder.AddBytes(bsFieldData);
            }

            return bytesBuilder.ToBytes();
        }

        /// <summary>
        /// 由字节数组构建
        /// </summary>
        /// <param name="bsRecordBuffer">字节数组</param>
        /// <param name="fieldList">字段定义信息</param>
        public void FromBytesOfFieldList(byte[] bsRecordBuffer, FOXFIELDList fieldList)
        {
            int nPos = 0;

            byte byDelSign = FOXUtil.RXNByte(bsRecordBuffer, ref nPos);
            if (byDelSign == 0x2A)
                this.IsDeletedRecord = true;
            else
                this.IsDeletedRecord = false;

            for (int i = 0; i < this.FieldCount; i++)
            {
                MFXRecordDataListItem item = this.GetItem(i);
                FOXFIELD field = fieldList.GetField(i);

                item.DataType = MFXRecordDataType.String;

                item.DataString = FOXUtil.RXNStringByLength(bsRecordBuffer, ref nPos, field.FieldLength);
            }
        }
    }
}
