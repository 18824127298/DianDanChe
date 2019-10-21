using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Diagnostics;

namespace Sigbit.Data.FoxDBF
{
    /// <summary>
    /// 提供通用存储结构。力图达到方便高效的效果。
    /// </summary>
    public class FoxDBFile
    {
        #region 私有变量
        /// <summary>
        /// 数据库文件名
        /// </summary>
        private string _sDBFFileName = "";

        /// <summary>
        /// 是否记录条数增加过
        /// </summary>
        private bool _bHasAppended = false;

        /// <summary>
        /// 文件是否已被打开过
        /// </summary>
        private bool _bFileOpened = false;

        /// <summary>
        /// 当前记录
        /// </summary>
        private int _nCurrentRecordSeq = 0;

        /// <summary>
        /// 文件流
        /// </summary>
        private FileStream _fsDBFFile;

        /// <summary>
        /// 数据库文件的文件头
        /// </summary>
        private FOXHEAD _fhFoxHead;

        /// <summary>
        /// 只读方式打开
        /// </summary>
        private bool _bOpenWithReadOnly = false;

        /// <summary>
        /// 字段总数
        /// </summary>
        private int _nFieldAmount = 0;

        /// <summary>
        /// 记录开始地址
        /// </summary>
        private int _nRecordAddress = 0;

        /// <summary>
        /// DBF字段信息
        /// </summary>
        private FOXFIELDList _fflistFoxFields;

        /// <summary>
        /// 记录数据
        /// </summary>
        private MFXRecordDataList _mfxRecordData;

        #endregion

        #region 属性
        /// <summary>
        /// 记录数
        /// </summary>
        public int RecCount
        {
            get
            {
                return _fhFoxHead.RecNum;
            }
        }

        /// <summary>
        /// 记录长度
        /// </summary>
        public int RecordLength
        {
            get
            {
                return _fhFoxHead.RecLen;
            }
        }
        #endregion

        #region 文件的打开和关闭
        /// <summary>
        /// 写入DBF的文件头
        /// </summary>
        private void WriteFoxHeader()
        {
            _fsDBFFile.Seek(0, SeekOrigin.Begin);

            byte[] bsHeadInfo = _fhFoxHead.ToBytes();
            _fsDBFFile.Write(bsHeadInfo, 0, bsHeadInfo.Length);
        }

        /// <summary>
        /// 将一些变化情况写入硬盘
        /// </summary>
        /// <remarks>
        /// 该函数一般在一个阶段操作完成以后调用，以保证数据的完整性。
        /// </remarks>
        public void Flush()
        {
            Debug.Assert(_bFileOpened);

            if (_bHasAppended)
            {
                _bHasAppended = false;

                _fsDBFFile.Seek(_nRecordAddress + this.RecCount * this.RecordLength, SeekOrigin.Begin);
                _fsDBFFile.WriteByte(0x1A);

                WriteFoxHeader();
            }

            _fsDBFFile.Flush();
        }
        
        /// <summary>
        /// 进行关闭文件，释放内存等操作
        /// </summary>
        /// <remarks>
        /// 如果文件已被增加记录，则重写文件头并在尾部写入结束字符。
        /// </remarks>
        public void CloseDBF()
        {
            if (!_bFileOpened)
                return;

            Flush();

            _bFileOpened = false;
            _fsDBFFile.Close();
        }

        /// <summary>
        /// 打开DBF文件，并获取文件信息
        /// </summary>
        /// <param name="sDBFFileName">文件名</param>
        /// <remarks>
        /// 要操作DBF文件前，必须先调用该函数。
        /// </remarks>
        public void AttachFile(string sDBFFileName)
        {
            _sDBFFileName = sDBFFileName;

            //========= 0. 初始化变量 ================
            CloseDBF();

            _bHasAppended = false;
            _bFileOpened = false;

            _nCurrentRecordSeq = -1;

            //========= 1. 打开DBF文件 =========
            if (!File.Exists(sDBFFileName))
            {
                throw new Exception("FoxDBF::AttachFile() File doesn\'t exist - "
                        + "FileName : " + sDBFFileName);
            }

            if (_bOpenWithReadOnly)
                _fsDBFFile = File.Open(sDBFFileName, FileMode.Open, FileAccess.Read);
            else
                _fsDBFFile = File.Open(sDBFFileName, FileMode.Open, FileAccess.ReadWrite);

            _bFileOpened = true;

            //======== 2. 读取DBF文件头 ==============
            //======== DBF文件头中包含有RecCount、RecordLength信息 ========
            byte[] bsFoxHead = new byte[FOXHEAD.SIZE_OF_FOXHEAD];

            int nReaded = _fsDBFFile.Read(bsFoxHead, 0, bsFoxHead.Length);
            if (nReaded != FOXHEAD.SIZE_OF_FOXHEAD)
                throw new Exception("File Header Error FileName - " + _sDBFFileName);

            _fhFoxHead = new FOXHEAD();
            _fhFoxHead.FromBytes(bsFoxHead);

            //=========== 3. 逐条读取DBF文件的字段描述 ===========
            _nFieldAmount = 0;
            _fflistFoxFields = new FOXFIELDList();

            while (true)
            {
                byte[] bsFieldInfo = new byte[FOXFIELD.SIZE_OF_FOXFIELD];
                nReaded = _fsDBFFile.Read(bsFieldInfo, 0, FOXFIELD.SIZE_OF_FOXFIELD);

                if (nReaded != FOXFIELD.SIZE_OF_FOXFIELD && bsFieldInfo[0] != 0x0D)
                    throw new Exception("DBF Field Description Error - "
                            + _sDBFFileName);

                if (bsFieldInfo[0] == 0x0D)
                    break;

                FOXFIELD foxField = new FOXFIELD();
                foxField.FromBytes(bsFieldInfo);

                _fflistFoxFields.AddField(foxField);

                _nFieldAmount ++;
            }

            _nRecordAddress = FOXHEAD.SIZE_OF_FOXHEAD + _nFieldAmount * FOXFIELD.SIZE_OF_FOXFIELD + 1;

            //=========== 4. 给字段分配相应的内存空间 ============
            _mfxRecordData = new MFXRecordDataList(_nFieldAmount);

            //========== 6. 校验完整性，比较文件大小是否与记录数相符 =====
            int nShouldFileSize = _nRecordAddress + this.RecCount * this.RecordLength;
            int nActualFileSize = (int)_fsDBFFile.Length;

            if (nShouldFileSize != nActualFileSize
                    && nShouldFileSize + 1 != nActualFileSize)
            {
                throw new Exception("DBF File Integrity Error - FileName: "
                        + sDBFFileName + "\n"
                        + " Actual Size : " + nActualFileSize.ToString()
                        + "    Need Size : " + nShouldFileSize.ToString() + "\n");
            }
        }
        #endregion

        #region 字段信息
        /// <summary>
        /// 根据字段序号得到字段名
        /// </summary>
        /// <param name="nFieldIndex">字段序号</param>
        /// <returns>字段名</returns>
        public string FieldName(int nFieldIndex)
        {
            return _fflistFoxFields.GetField(nFieldIndex).FieldName;
        }

        /// <summary>
        /// 根据字段名得到字段序号
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <returns>字段序号</returns>
        public int FieldSeq(string sFieldName)
        {
            return _fflistFoxFields.GetFieldSeqOfFieldName(sFieldName);
        }

        public int FieldLength(int nFieldIndex)
        {
            return _fflistFoxFields.GetField(nFieldIndex).FieldLength;
        }

        public int FieldPoint(int nFieldIndex)
        {
            return _fflistFoxFields.GetField(nFieldIndex).FieldPoint;
        }

        public DBFFieldType FieldType(int nFieldIndex)
        {
            return _fflistFoxFields.GetField(nFieldIndex).FieldType;
        }

        public int FieldCount
        {
            get
            {
                return _fflistFoxFields.FieldCount;
            }
        }
        #endregion

        #region 改变记录缓冲
        public void SetRecordData(int nFieldSeq, string sData)
        {
            _mfxRecordData.SetRecordData(nFieldSeq, sData);
        }

        public void SetRecordData(int nFieldSeq, int nData)
        {
            _mfxRecordData.SetRecordData(nFieldSeq, nData);
        }

        public void SetRecordData(int nFieldSeq, double fData)
        {
            _mfxRecordData.SetRecordData(nFieldSeq, fData);
        }

        public void SetRecordData(int nFieldSeq, bool bData)
        {
            _mfxRecordData.SetRecordData(nFieldSeq, bData);
        }

        public void SetRecordData(int nFieldSeq, DateTime dtData)
        {
            _mfxRecordData.SetRecordData(nFieldSeq, dtData);
        }

        public void SetRecordData(int nFieldSeq, char cData)
        {
            _mfxRecordData.SetRecordData(nFieldSeq, cData);
        }

        public void SetRecordData(string sFieldName, string data)
        {
            int nFieldSeq = _fflistFoxFields.GetFieldSeqOfFieldName(sFieldName);
            SetRecordData(nFieldSeq, data);
        }

        public void SetRecordData(string sFieldName, int data)
        {
            int nFieldSeq = _fflistFoxFields.GetFieldSeqOfFieldName(sFieldName);
            SetRecordData(nFieldSeq, data);
        }

        public void SetRecordData(string sFieldName, double data)
        {
            int nFieldSeq = _fflistFoxFields.GetFieldSeqOfFieldName(sFieldName);
            SetRecordData(nFieldSeq, data);
        }

        public void SetRecordData(string sFieldName, bool data)
        {
            int nFieldSeq = _fflistFoxFields.GetFieldSeqOfFieldName(sFieldName);
            SetRecordData(nFieldSeq, data);
        }

        public void SetRecordData(string sFieldName, DateTime data)
        {
            int nFieldSeq = _fflistFoxFields.GetFieldSeqOfFieldName(sFieldName);
            SetRecordData(nFieldSeq, data);
        }

        public void SetRecordData(string sFieldName, char data)
        {
            int nFieldSeq = _fflistFoxFields.GetFieldSeqOfFieldName(sFieldName);
            SetRecordData(nFieldSeq, data);
        }

        #endregion

        #region 读取记录缓冲
        public string GetRecordString_WithoutTrim(int nFieldSeq)
        {
            return _mfxRecordData.GetRecordString_WithoutTrim(nFieldSeq);
        }

        public string GetRecordString(int nFieldSeq)
        {
            return _mfxRecordData.GetRecordString(nFieldSeq);
        }

        public int GetRecordInt(int nFieldSeq)
        {
            return _mfxRecordData.GetRecordInt(nFieldSeq);
        }

        public double GetRecordDouble(int nFieldSeq)
        {
            return _mfxRecordData.GetRecordDouble(nFieldSeq);
        }

        public bool GetRecordBool(int nFieldSeq)
        {
            return _mfxRecordData.GetRecordBool(nFieldSeq);
        }

        public DateTime GetRecordDateTime(int nFieldSeq)
        {
            return _mfxRecordData.GetRecordDateTime(nFieldSeq);
        }

        public char GetRecordChar(int nFieldSeq)
        {
            return _mfxRecordData.GetRecordChar(nFieldSeq);
        }

        public string GetRecordString(string sFieldName)
        {
            int nFieldSeq = _fflistFoxFields.GetFieldSeqOfFieldName(sFieldName);
            return _mfxRecordData.GetRecordString(nFieldSeq);
        }

        public int GetRecordInt(string sFieldName)
        {
            int nFieldSeq = _fflistFoxFields.GetFieldSeqOfFieldName(sFieldName);
            return _mfxRecordData.GetRecordInt(nFieldSeq);
        }

        public double GetRecordDouble(string sFieldName)
        {
            int nFieldSeq = _fflistFoxFields.GetFieldSeqOfFieldName(sFieldName);
            return _mfxRecordData.GetRecordDouble(nFieldSeq);
        }

        public bool GetRecordBool(string sFieldName)
        {
            int nFieldSeq = _fflistFoxFields.GetFieldSeqOfFieldName(sFieldName);
            return _mfxRecordData.GetRecordBool(nFieldSeq);
        }

        public DateTime GetRecordDateTime(string sFieldName)
        {
            int nFieldSeq = _fflistFoxFields.GetFieldSeqOfFieldName(sFieldName);
            return _mfxRecordData.GetRecordDateTime(nFieldSeq);
        }

        public char GetRecordChar(string sFieldName)
        {
            int nFieldSeq = _fflistFoxFields.GetFieldSeqOfFieldName(sFieldName);
            return _mfxRecordData.GetRecordChar(nFieldSeq);
        }

        #endregion

        #region 新增记录、删除记录、更新记录
        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <remarks>
        /// 为增加速度，在进行Append操作时，不刷新文件头及记录文件尾。而是
        /// 在关闭文件时进行记录。所以进行Append操作后，要及时关闭文件或运
        /// 行Flush()函数。
        /// </remarks>
        public void Append()
        {
            this.Deleted = false;

            byte[] bsRecord = _mfxRecordData.ToBytesOfFieldList(_fflistFoxFields);

            _fsDBFFile.Seek(_nRecordAddress + RecCount * RecordLength, SeekOrigin.Begin);
            _fsDBFFile.Write(bsRecord, 0, bsRecord.Length);

            _fhFoxHead.RecNum++;
            _nCurrentRecordSeq = _fhFoxHead.RecNum - 1;

            _bHasAppended = true;

            _mfxRecordData.Reset();
        }

        /// <summary>
        /// 删除当前记录
        /// </summary>
        /// <remarks>
        /// 只对记录做上标记，如果要真正删除还要调用Pack()函数。
        /// </remarks>
        public void Delete()
        {
            if (_nCurrentRecordSeq < 0 || _nCurrentRecordSeq >= this.RecCount)
                return;

            _fsDBFFile.Seek(_nRecordAddress + _nCurrentRecordSeq * this.RecordLength, SeekOrigin.Begin);

            _fsDBFFile.WriteByte(0x2A);
        }

        /// <summary>
        /// 是否已经被删除了
        /// </summary>
        public bool Deleted
        {
            get
            {
                return _mfxRecordData.IsDeletedRecord;
            }
            set
            {
                _mfxRecordData.IsDeletedRecord = value;
            }
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        public void Update()
        {
            if (_nCurrentRecordSeq < 0 || _nCurrentRecordSeq >= this.RecCount)
                throw new Exception("未移到有效记录，无法更新");

            byte[] bsRecordInfo = _mfxRecordData.ToBytesOfFieldList(_fflistFoxFields);

            _fsDBFFile.Seek(_nRecordAddress + _nCurrentRecordSeq * this.RecordLength, SeekOrigin.Begin);
            _fsDBFFile.Write(bsRecordInfo, 0, this.RecordLength);

            _mfxRecordData.Reset();
        }
        /*
void TCFoxDBF::Update()
{
    if (m_nCurrentRecordSeq < 0 || m_nCurrentRecordSeq >= RecCount())
        throw TCException("TCFoxDBF::InsertRecordByStringList() Error - \n"
                " CurrentRecord : " + IntToStr(m_nCurrentRecordSeq)
                + "\n TotalRecord : " + IntToStr(RecCount()));

    m_fDBFFile.Seek(m_nRecordAddress + m_nCurrentRecordSeq * RecordLength());
    m_fDBFFile.Read(m_pRecordBuffer, RecordLength());

    m_pRecordBuffer[0] = ' ';
    PutDBFValue();

    m_fDBFFile.Seek(m_nRecordAddress + m_nCurrentRecordSeq * RecordLength());
    m_fDBFFile.Write(m_pRecordBuffer, RecordLength());
}
         */

        #endregion

        #region 插入记录
        /// <summary>
        /// 根据绑定的内容插入记录
        /// </summary>
        /// <remarks>
        /// 由于插入记录的文件操作过多（插入点之后的文件内容要全部后移），
        /// 故本函数只是多提供一种选择，一般不推荐使用。
        /// </remarks>
        public void Insert()
        {
            this.Deleted = false;

            if (_nCurrentRecordSeq < 0 || _nCurrentRecordSeq >= this.RecCount)
                throw new Exception("Insert() 未移动相应的待插入的记录处");

            Insert__Prepare();

            _fsDBFFile.Seek(_nRecordAddress + _nCurrentRecordSeq * this.RecordLength, SeekOrigin.Begin);

            byte[] bsRecordInfo = _mfxRecordData.ToBytesOfFieldList(_fflistFoxFields);
            _fsDBFFile.Write(bsRecordInfo, 0, bsRecordInfo.Length);

            _fhFoxHead.RecNum++;
            WriteFoxHeader();

            _mfxRecordData.Reset();
        }

        /// <summary>
        /// 将插入点后面的文件内容后移，为插入数据做准备
        /// </summary>
        private void Insert__Prepare()
        {
            //====== 1. 分配两个字节数组，用于缓存数据 ========
            byte[] bsRecordBuffer1 = new byte[this.RecordLength];
            byte[] bsRecordBuffer2 = new byte[this.RecordLength];

            //========== 2. 将当前的记录保存到Buffer1中 ========
            _fsDBFFile.Seek(_nRecordAddress + _nCurrentRecordSeq * this.RecordLength, SeekOrigin.Begin);
            _fsDBFFile.Read(bsRecordBuffer1, 0, this.RecordLength);

            //========= 3. 逐个将记录后移 =============
            for (int i = _nCurrentRecordSeq + 1; i <= this.RecCount; i++)
            {
                //========= 3.1 将本条记录读取到Buffer1中 ==========
                if (i != this.RecCount)
                {
                    _fsDBFFile.Seek(_nRecordAddress + i * this.RecordLength, SeekOrigin.Begin);
                    _fsDBFFile.Read(bsRecordBuffer2, 0, this.RecordLength);
                }

                //========== 3.2 将上一条记录(已存入Buffer1之中)写入到本条记录的位置 ==========
                _fsDBFFile.Seek(_nRecordAddress + i * this.RecordLength, SeekOrigin.Begin);
                _fsDBFFile.Write(bsRecordBuffer1, 0, this.RecordLength);

                //========= 3.3 将Buffer2的内容复制到Buffer1之中 ========
                Array.Copy(bsRecordBuffer2, bsRecordBuffer1, this.RecordLength);
            }

            //============ 4. 文件结束标记 =========
            _fsDBFFile.WriteByte(0x1A);
        }

        #endregion


        #region 移动记录的当前位置
        /// <summary>
        /// 移到指定的记录
        /// </summary>
        /// <param name="nRecNo">记录号</param>
        /// <remarks>
        /// 记录条数从1开始计数
        /// </remarks>
        public void Go(int nRecNo)
        {
            _mfxRecordData.Reset();

            _nCurrentRecordSeq = nRecNo - 1;

            ReadRecordToBuffer();
        }

        /// <summary>
        /// 读取一条记录的内容到缓冲区
        /// </summary>
        private void ReadRecordToBuffer()
        {
            if (_nCurrentRecordSeq >= this.RecCount)
                throw new Exception("当前记录号过大");

            if (_nCurrentRecordSeq < 0)
                throw new Exception("当前记录号过小");

            _fsDBFFile.Seek(_nRecordAddress + _nCurrentRecordSeq * this.RecordLength, SeekOrigin.Begin);

            byte[] bsRecordBuffer = new byte[this.RecordLength];
            int nReaded = _fsDBFFile.Read(bsRecordBuffer, 0, this.RecordLength);

            if (nReaded != this.RecordLength)
                throw new Exception("读取失败。DBF文件也许坏掉了。");

            _mfxRecordData.FromBytesOfFieldList(bsRecordBuffer, _fflistFoxFields);
        }

        #endregion

        #region 清空、压缩
        /// <summary>
        /// 清空DBF文件
        /// </summary>
        public void ZAP()
        {
            _fsDBFFile.SetLength(_nRecordAddress);

            _nCurrentRecordSeq = 0;
            _fhFoxHead.RecNum = 0;

            WriteFoxHeader();
        }

        /// <summary>
        /// 真正地物理删除记录
        /// </summary>
        /// <remarks>
        /// 在做删除记录Delete()时，只是做一下删除标记而并不真正删除该
        /// 记录。当执行本函数时即进行真正地物理删除。本操作能够节省空
        /// 间，但如考虑到速度的影响，不推荐频繁使用。
        /// </remarks>
        public void Pack()
        {
            byte[] bsRecordBuffer = new byte[this.RecordLength];

            int nWriteRow = 0;

            //====== 1. 循环读取每一条记录，如不是删除的记录，则写入 ======
            for (int nReadRow = 0; nReadRow < this.RecCount; nReadRow++)
            {
                _fsDBFFile.Seek(_nRecordAddress + nReadRow * this.RecordLength, SeekOrigin.Begin);
                _fsDBFFile.Read(bsRecordBuffer, 0, this.RecordLength);

                if (bsRecordBuffer[0] == 0x2A)
                    continue;

                _fsDBFFile.Seek(_nRecordAddress + nWriteRow * this.RecordLength, SeekOrigin.Begin);
                _fsDBFFile.Write(bsRecordBuffer, 0, this.RecordLength);

                nWriteRow++;
            }

            //======= 2. 截短文件，写入文件尾标记，更新文件头 ======
            _fsDBFFile.SetLength(_nRecordAddress + nWriteRow * this.RecordLength);
            _fsDBFFile.Seek(_nRecordAddress + nWriteRow * this.RecordLength, SeekOrigin.Begin);
            _fsDBFFile.WriteByte(0x1A);

            _fhFoxHead.RecNum = nWriteRow;
            WriteFoxHeader();
        }
        #endregion

    }
}

