using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.IO;

namespace Sigbit.Data.FoxDBF
{
    /// <summary>
    /// 创建DBF文件
    /// </summary>
    public class DBFCreate
    {
        private int _fieldCount = 0;
        /// <summary>
        /// 字段数
        /// </summary>
        public int FieldCount
        {
            get { return _fieldCount; }
            set { _fieldCount = value; }
        }

        private int _fieldCurrentSeq = -1;
        /// <summary>
        /// 当前的字段序号
        /// </summary>
        public int FieldCurrentSeq
        {
            get { return _fieldCurrentSeq; }
            set { _fieldCurrentSeq = value; }
        }

        private string _DBFName = "";
        /// <summary>
        /// DBF文件名
        /// </summary>
        public string DBFName
        {
            get { return _DBFName; }
            set { _DBFName = value; }
        }

        /// <summary>
        /// DBF文件的字段定义
        /// </summary>
        private FoxFieldDef[] _arrFDFieldsDef;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sDBFName">DBF文件的名称</param>
        /// <param name="nFieldCount">字段数</param>
        public DBFCreate(string sDBFName, int nFieldCount)
        {
            Debug.Assert(nFieldCount > 0);

            _fieldCount = nFieldCount;

            _arrFDFieldsDef = new FoxFieldDef[nFieldCount];
            for (int i = 0; i < nFieldCount; i++)
            {
                _arrFDFieldsDef[i] = new FoxFieldDef();
            }

            _fieldCurrentSeq = -1;

            _DBFName = sDBFName;
        }

        public void AddField(string sFieldName, DBFFieldType fieldType)
        {
            int nFieldLen = 0;

            switch (fieldType)
            {
                case DBFFieldType.Date:
                    nFieldLen = 8;
                    break;
                case DBFFieldType.Char:
                    nFieldLen = 1;
                    break;
                case DBFFieldType.Number:
                    nFieldLen = 9;
                    break;
                case DBFFieldType.Logic:
                    nFieldLen = 1;
                    break;
                default:
                    throw new Exception("AddField() : 不知道字段的长度。");
            }

            AddField(sFieldName, fieldType, nFieldLen, 0);
        }

        public void AddField(string sFieldName, DBFFieldType fieldType, int nFieldLen)
        {
            AddField(sFieldName, fieldType, nFieldLen, 0);
        }

        /// <summary>
        /// 加入一个字段
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <param name="fieldType">字段类型</param>
        /// <param name="nFieldLen">字段长度</param>
        /// <param name="nFieldPoint">字段小数点后的长度</param>
        public void AddField(string sFieldName, DBFFieldType fieldType, int nFieldLen, int nFieldPoint)
        {
            this.FieldCurrentSeq++;

            if (this.FieldCurrentSeq >= this.FieldCount)
                throw new Exception("TCDBFCreate.AddField() Error.  FieldSeq Exceeded. - " + this.DBFName);

            Debug.Assert(fieldType == DBFFieldType.Number || nFieldPoint == 0);
            Debug.Assert(nFieldPoint + 1 <= nFieldLen);
            Debug.Assert(nFieldLen > 0);

            Debug.Assert(fieldType != DBFFieldType.Logic || nFieldLen == 1);
            Debug.Assert(fieldType != DBFFieldType.Date || nFieldLen == 8);

            _arrFDFieldsDef[this.FieldCurrentSeq].FieldName = sFieldName;
            _arrFDFieldsDef[this.FieldCurrentSeq].FieldType = fieldType;
            _arrFDFieldsDef[this.FieldCurrentSeq].FieldLength = nFieldLen;
            _arrFDFieldsDef[this.FieldCurrentSeq].FieldPoint = nFieldPoint;
        }

        /// <summary>
        /// 根据填入的文件名及字段定义创建DBF文件
        /// </summary>
        public void CreateDBF()
        {
            Debug.Assert(this.FieldCurrentSeq == this.FieldCount - 1);

            //======== 1. 填充FOX文件头 ===========
            FOXHEAD fxhFoxHead = new FOXHEAD();

            fxhFoxHead.Year = DateTime.Now.Year % 100;
            fxhFoxHead.Month = DateTime.Now.Month;
            fxhFoxHead.Day = DateTime.Now.Day;

            fxhFoxHead.RecNum = 0;
            fxhFoxHead.RecAddr = FOXHEAD.SIZE_OF_FOXHEAD + 1;
            fxhFoxHead.RecLen = 1;

            for (int i = 0; i < this.FieldCount; i++)
            {
                fxhFoxHead.RecAddr += FOXFIELD.SIZE_OF_FOXFIELD;
                fxhFoxHead.RecLen += _arrFDFieldsDef[i].FieldLength;
            }

            //======= 2. 打开文件，并写入文件头 ============
            FileStream fsDestDBF = File.Create(this.DBFName);

            byte[] bsFoxHead = fxhFoxHead.ToBytes();
            fsDestDBF.Write(bsFoxHead, 0, bsFoxHead.Length);

            //====== 3. 准备字段定义部分 =============
            int nStartOffset = 1;

            for (int i = 0; i < this.FieldCount; i++)
            {
                FOXFIELD foxField = new FOXFIELD();
                foxField.FieldName = _arrFDFieldsDef[i].FieldName;
                foxField.FieldType = _arrFDFieldsDef[i].FieldType;
                foxField.FieldLength = _arrFDFieldsDef[i].FieldLength;
                foxField.FieldPoint = _arrFDFieldsDef[i].FieldPoint;

                foxField.StartOffset = nStartOffset;
                nStartOffset += foxField.FieldLength;

                //=========== 4. 写入文件 ==============
                byte[] bsField = foxField.ToBytes();
                fsDestDBF.Write(bsField, 0, bsField.Length);
            }

            // ========= 5. 写入尾标记，关闭文件 ========
            fsDestDBF.WriteByte(0x0D);

            fsDestDBF.Flush();
            fsDestDBF.Close();
        }
    }
}
