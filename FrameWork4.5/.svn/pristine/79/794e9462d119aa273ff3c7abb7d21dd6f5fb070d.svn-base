using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

namespace Sigbit.Net.BIPPacket
{
    /// <summary>
    /// 该类封装一个数据包。实现数据包的读、写、更新操作。
    /// </summary>
    /// <remarks>
    /// 数据包一般包括两个DataSet，序号为0的存放单条记录，序号为1的存放
    /// 结果集。
    /// </remarks>
    public class BIPCustomPacket
    {
        #region 属性
        /// <summary>
        /// BIP数据集列表
        /// </summary>
        protected BIPDataSetList _bipDataSetList = new BIPDataSetList();
        /// <summary>
        /// 数据集总数
        /// </summary>
        public int DataSetCount
        {
            get
            {
                return _bipDataSetList.Count;
            }
        }

        private BIPPacketFormat _packetFormat;
        /// <summary>
        /// 包格式
        /// </summary>
        public BIPPacketFormat PacketFormat
        {
            get { return _packetFormat; }
            set { _packetFormat = value; }
        }

        private BIPPacketType _packetType;
        /// <summary>
        /// 包类型
        /// </summary>
        public BIPPacketType PacketType
        {
            get { return _packetType; }
            set { _packetType = value; }
        }

        private string _transCode;
        /// <summary>
        /// 完整的交易码
        /// </summary>
        public string TransCode
        {
            get { return _transCode; }
            set { _transCode = value; }
        }

        /// <summary>
        /// 得到交易码双字节组 
        /// </summary>
        /// <param name="nSeq">双字节组序号（从0开始）</param>
        /// <returns>交易码双字节组</returns>
        public string GetSubTransCode(int nSeq)
        {
            if (nSeq < 0 || nSeq > 3)
                throw new BIPCallException("TCBIPCustomPacket::GetSubTransCode()");

            if (nSeq * 2 >= _transCode.Length)
                return "";

            return _transCode.Substring(nSeq * 2, 2);
        }

        /// <summary>
        /// 按序号设置交易码双字节组
        /// </summary>
        /// <param name="nSeq">序号</param>
        /// <param name="sSubTransCode">双字节组</param>
        public void SetSubTransCode(int nSeq, string sSubTransCode)
        {
            if (nSeq < 0 || nSeq > 3 || sSubTransCode.Length != 2)
                throw new BIPCallException("TCBIPCustomPacket::SetSubTransCode()");

            string sResult;

            int nNeedLength;
            nNeedLength = nSeq * 2;

            if (nNeedLength > _transCode.Length)
                sResult = _transCode.PadRight(nNeedLength);
            else
                sResult = _transCode;

            sResult = sResult.Substring(0, nNeedLength) + sSubTransCode
                    + sResult.Substring(nNeedLength + 2);

            _transCode = sResult;
        }

        private string _shortPacketData;
        /// <summary>
        /// 短包数据(字符串)
        /// </summary>
        public string ShortPacketData
        {
            get 
            {
                if (_shortPacketBuffer != null)
                    return Encoding.Default.GetString(_shortPacketBuffer);

                return _shortPacketData; 
            }
            set { _shortPacketData = value; }
        }

        private byte[] _shortPacketBuffer = null;
        /// <summary>
        /// 短包数据(字节数组)
        /// </summary>
        public byte[] ShortPacketBuffer
        {
            get { return _shortPacketBuffer; }
            set { _shortPacketBuffer = value; }
        }

        private int _packetId;
        /// <summary>
        /// 包标识
        /// </summary>
        public int PacketId
        {
            get { return _packetId; }
            set { _packetId = value; }
        }

        private string _packetStatus;
        /// <summary>
        /// 包状态
        /// </summary>
        public string PacketStatus
        {
            get { return _packetStatus; }
            set { _packetStatus = value; }
        }

        private int _packetResendCount;
        /// <summary>
        /// 包重发次数
        /// </summary>
        public int PacketResendCount
        {
            get { return _packetResendCount; }
            set { _packetResendCount = value; }
        }

        private BIPPacketSyncMeth _packetSyncMeth;
        /// <summary>
        /// 同步方式
        /// </summary>
        public BIPPacketSyncMeth PacketSyncMeth
        {
            get { return _packetSyncMeth; }
            set { _packetSyncMeth = value; }
        }

        private bool _ignoreCase;
        /// <summary>
        /// 忽略大小写
        /// </summary>
        public bool IgnoreCase
        {
            get 
            { 
                return _ignoreCase; 
            }
            set 
            { 
                _ignoreCase = value;
                for (int i = 0; i < DataSetCount; i++)
                    _bipDataSetList.GetDataSet(i).IgnoreCase = value;
            }
        }
        #endregion 属性

        #region 初始化及构造函数
        /// <summary>
        /// 初始化私有变量，在构造函数中调用
        /// </summary>
        private void Init()
        {
            _packetFormat = BIPPacketFormat.LongFormat;
            _packetType = BIPPacketType.Request;
            _packetId = 0;
            _packetResendCount = 0;
            _packetSyncMeth = BIPPacketSyncMeth.DestDone;
            _transCode = "";
            _packetStatus = "";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <remarks>
        ///  缺省创建两个DataSet
        /// </remarks>
        public BIPCustomPacket()
        {
            Init();
            _bipDataSetList.AddDataSet();
            _bipDataSetList.AddDataSet();
            _ignoreCase = false;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nDataSetInitCount">初始创建的DataSet的个数</param>
        public BIPCustomPacket(int nDataSetInitCount)
        {
            Init();

            int i;

            for (i = 0; i < nDataSetInitCount; i++)
                _bipDataSetList.AddDataSet();
        }
        #endregion 初始化及构造函数

        #region 数据集
        /// <summary>
        /// 创建新的DataSet
        /// </summary>
        /// <param name="sDataSetName">DataSet的名称（缺省时为""）</param>
        /// <param name="nSeq">序号（缺省时加到DataSet列表的最后）</param>
        /// <returns>新创建的DataSet</returns>
        public BIPDataSet NewDataSet(string sDataSetName, int nSeq)
        {
            if (nSeq != -1 && nSeq > _bipDataSetList.Count)
                    throw new BIPCallException("BIPCustomPacket::NewDataSet() : "
                            + "dataset seq out of range.");

            BIPDataSet dataSet = new BIPDataSet();

            if (sDataSetName != "")
                dataSet.DataSetName = sDataSetName;

            dataSet.IgnoreCase = _ignoreCase;

            if (nSeq == -1)
                _bipDataSetList.Add(dataSet);
            else
                _bipDataSetList.Insert(nSeq, dataSet);

            return dataSet;
        }

        /// <summary>
        /// 创建新的DataSet
        /// </summary>
        /// <param name="sDataSetName">DataSet的名称（缺省时为""）</param>
        /// <returns>新创建的DataSet</returns>
        public BIPDataSet NewDataSet(string sDataSetName)
        {
            return NewDataSet(sDataSetName, -1);
        }

        /// <summary>
        /// 创建新的DataSet
        /// </summary>
        /// <returns>新创建的DataSet</returns>
        public BIPDataSet NewDataSet()
        {
            return NewDataSet("", -1);
        }

        /// <summary>
        /// 返回序号为1的DataSet
        /// </summary>
        /// <returns>DataSet</returns>
        public BIPDataSet GetDataSet()
        {
            Debug.Assert(DataSetCount >= 2);
            return _bipDataSetList.GetDataSet(1);
        }

        /// <summary>
        /// 返回指定序号的DataSet
        /// </summary>
        /// <param name="nSeq">DataSet序号</param>
        /// <returns>DataSet</returns>
        public BIPDataSet GetDataSet(int nSeq)
        {
            Debug.Assert(DataSetCount > nSeq);
            return _bipDataSetList.GetDataSet(nSeq);
        }

        /// <summary>
        /// 返回指定名称的DataSet
        /// </summary>
        /// <param name="sDataSetName">DataSet名称</param>
        /// <returns>DataSet</returns>
        public BIPDataSet GetDataSet(string sDataSetName)
        {
            for (int i = 0; i < DataSetCount; i++)
            {
                if (_bipDataSetList.GetDataSet(i).DataSetName == sDataSetName)
                    return _bipDataSetList.GetDataSet(i);
            }

            throw new BIPCallException("TCBIPCustomPacket::GetDataSet() : "
                    + "cannot locate the dataset_name - " + sDataSetName);
        }

        /// <summary>
        /// 删除指定序号的DataSet
        /// </summary>
        /// <param name="nSeq">DataSet序号</param>
        public void DeleteDataSet(int nSeq)
        {
            if (nSeq >= _bipDataSetList.Count)
                throw new BIPCallException("TCBIPCustomPacket::DeleteDataSet() : "
                        + "dataset seq out of range.");
            _bipDataSetList.RemoveAt(nSeq);
        }

        /// <summary>
        /// 删除所有的序号大于1的DataSet（保留前两个DataSet）, 并清除前两
        /// 个DataSet的内容。
        /// </summary>
        public void DeleteAllDataSet()
        {
            int i;
            for (i = DataSetCount - 1; i > 1; i--)
                DeleteDataSet(i);

            for (i = DataSetCount; i < 2; i++)
                NewDataSet();

            for (i = 0; i < DataSetCount; i++)
                _bipDataSetList.GetDataSet(i).Clear();
        }
        #endregion 数据集

        #region 数据操作
        /// <summary>
        /// 获得结果集中的一项数据
        /// </summary>
        /// <param name="nRecordNum">记录序号</param>
        /// <param name="nFieldSeq">字段序号</param>
        /// <returns>数据的字符串值</returns>
        public string GetItemString(int nRecordNum, int nFieldSeq)
        { 
            return GetDataSet().GetItemString(nRecordNum, nFieldSeq); 
        }

        /// <summary>
        /// 根据域字段序号获取指定项的字符串返回值，无值则返回默认值
        /// </summary>
        /// <param name="nRecordNum"></param>
        /// <param name="nFieldSeq"></param>
        /// <param name="sDefault"></param>
        /// <returns></returns>
        public string GetItemStringDefault(int nRecordNum, int nFieldSeq, string sDefault)
        {
            try
            {
                return GetItemString(nRecordNum, nFieldSeq);
            }
            catch
            {
                return sDefault;
            }
        }

        /// <summary>
        /// 获得结果集中的一项数据
        /// </summary>
        /// <param name="nRecordNum">记录序号</param>
        /// <param name="sFieldName">字段序号</param>
        /// <returns>数据的字符串值</returns>
        public string GetItemString(int nRecordNum, string sFieldName)
        { 
            return GetDataSet().GetItemString(nRecordNum, sFieldName); 
        }

        /// <summary>
        /// 根据域名获取指定项的字符串返回值，无值则返回默认值
        /// </summary>
        /// <param name="nRecordNum"></param>
        /// <param name="sFieldName"></param>
        /// <param name="sDefault"></param>
        /// <returns></returns>
        public string GetItemStringDefault(int nRecordNum, string sFieldName, string sDefault)
        {
            try
            {
                return GetItemString(nRecordNum, sFieldName);
            }
            catch
            {
                return sDefault;
            }
        }

        /// <summary>
        /// 得到一项离散数据
        /// </summary>
        /// <param name="nFieldSeq">字段序号</param>
        /// <returns>字符串值</returns>
        public string GetAStringValue(int nFieldSeq)
        {
            return GetDataSet(0).GetAStringValue(nFieldSeq); 
        }

        /// <summary>
        /// 按照域序号获取字符串返回值，无则返回默认值
        /// </summary>
        /// <param name="nFieldSeq"></param>
        /// <param name="sDefault"></param>
        /// <returns></returns>
        public string GetAStringValueDefault(int nFieldSeq, string sDefault)
        {
            try
            {
                return GetAStringValue(nFieldSeq);
            }
            catch
            {
                return sDefault;
            }
        }

        /// <summary>
        /// 得到一项离散数据
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <returns>字符串值</returns>
        public string GetAStringValue(string sFieldName)
        {
            return GetDataSet(0).GetAStringValue(sFieldName); 
        }

        /// <summary>
        /// 按域名获取返回值，无值返回默认值
        /// </summary>
        /// <param name="sFieldName"></param>
        /// <param name="sDefault"></param>
        /// <returns></returns>
        public string GetAStringValueDefault(string sFieldName, string sDefault)
        {
            try
            {
                return GetAStringValue(sFieldName);
            }
            catch
            {
                return sDefault;
            }
        }

        /// <summary>
        /// 设置结果集中的数据
        /// </summary>
        /// <param name="nRecordNum">记录序号</param>
        /// <param name="nFieldSeq">字段序号</param>
        /// <param name="sItemValue">数据值</param>
        public void SetItemString(int nRecordNum, int nFieldSeq, string sItemValue)
        { 
            GetDataSet().SetItemString(nRecordNum, nFieldSeq, sItemValue); 
        }

        /// <summary>
        /// 设置结果集中的数据
        /// </summary>
        /// <param name="nRecordNum">记录序号</param>
        /// <param name="sFieldName">字段名</param>
        /// <param name="sItemValue">数据值</param>
        public void SetItemString(int nRecordNum, string sFieldName, string sItemValue)
        { 
            GetDataSet().SetItemString(nRecordNum, sFieldName, sItemValue); 
        }

        /// <summary>
        /// 设置一项离散数据
        /// </summary>
        /// <param name="nFieldSeq">字段序号</param>
        /// <param name="sItemValue">数据值</param>
        public void SetAStringValue(int nFieldSeq, string sItemValue)
        {
            GetDataSet(0).SetAStringValue(nFieldSeq, sItemValue); 
        }

        /// <summary>
        /// 设置一项离散数据
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <param name="sItemValue">数据值</param>
        public void SetAStringValue(string sFieldName, string sItemValue)
        {
            GetDataSet(0).SetAStringValue(sFieldName, sItemValue); 
        }

        /// <summary>
        /// 增加一项离散数据
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <param name="sItemValue">数据值</param>
        public void AddAStringValue(string sFieldName, string sItemValue)
        {
            GetDataSet(0).AddAStringValue(sFieldName, sItemValue); 
        }

        /// <summary>
        /// 得到字段数
        /// </summary>
        /// <returns>字段数</returns>
        public int GetFieldCount()
        {
            return GetDataSet().GetFieldCount(); 
        }

        /// <summary>
        /// 得到记录数
        /// </summary>
        /// <returns>记录数</returns>
        public int GetRecordCount()
        { 
            return GetDataSet().GetRecordCount(); 
        }

        /// <summary>
        /// 增加一个字段
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <param name="fieldType">字段类型</param>
        /// <param name="nFieldLength">字段长度</param>
        /// <param name="nFieldPrecision">字段精度</param>
        public void AddField(string sFieldName, BIPFieldType fieldType,
                int nFieldLength, int nFieldPrecision)
        {
            GetDataSet().AddField(sFieldName, fieldType, nFieldLength, nFieldPrecision);
        }

        /// <summary>
        /// 增加一个字段
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <param name="fieldType">字段类型</param>
        /// <param name="nFieldLength">字段长度</param>
        public void AddField(string sFieldName, BIPFieldType fieldType,
                int nFieldLength)
        {
            GetDataSet().AddField(sFieldName, fieldType, nFieldLength);
        }

        /// <summary>
        /// 增加一个字段
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <param name="fieldType">字段类型</param>
        public void AddField(string sFieldName, BIPFieldType fieldType)
        {
            GetDataSet().AddField(sFieldName, fieldType);
        }

        /// <summary>
        /// 增加一个字段
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        public void AddField(string sFieldName)
        {
            GetDataSet().AddField(sFieldName);
        }

        /// <summary>
        /// 增加指定数据的字段
        /// </summary>
        /// <param name="nFieldCount"></param>
        public void AddFields(int nFieldCount)
        { 
            GetDataSet().AddFields(nFieldCount);
        }
        #endregion 数据操作
    }
}
