using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.IO;

namespace Sigbit.Data.FoxDBF
{
    /// <summary>
    /// ����DBF�ļ�
    /// </summary>
    public class DBFCreate
    {
        private int _fieldCount = 0;
        /// <summary>
        /// �ֶ���
        /// </summary>
        public int FieldCount
        {
            get { return _fieldCount; }
            set { _fieldCount = value; }
        }

        private int _fieldCurrentSeq = -1;
        /// <summary>
        /// ��ǰ���ֶ����
        /// </summary>
        public int FieldCurrentSeq
        {
            get { return _fieldCurrentSeq; }
            set { _fieldCurrentSeq = value; }
        }

        private string _DBFName = "";
        /// <summary>
        /// DBF�ļ���
        /// </summary>
        public string DBFName
        {
            get { return _DBFName; }
            set { _DBFName = value; }
        }

        /// <summary>
        /// DBF�ļ����ֶζ���
        /// </summary>
        private FoxFieldDef[] _arrFDFieldsDef;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="sDBFName">DBF�ļ�������</param>
        /// <param name="nFieldCount">�ֶ���</param>
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
                    throw new Exception("AddField() : ��֪���ֶεĳ��ȡ�");
            }

            AddField(sFieldName, fieldType, nFieldLen, 0);
        }

        public void AddField(string sFieldName, DBFFieldType fieldType, int nFieldLen)
        {
            AddField(sFieldName, fieldType, nFieldLen, 0);
        }

        /// <summary>
        /// ����һ���ֶ�
        /// </summary>
        /// <param name="sFieldName">�ֶ���</param>
        /// <param name="fieldType">�ֶ�����</param>
        /// <param name="nFieldLen">�ֶγ���</param>
        /// <param name="nFieldPoint">�ֶ�С�����ĳ���</param>
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
        /// ����������ļ������ֶζ��崴��DBF�ļ�
        /// </summary>
        public void CreateDBF()
        {
            Debug.Assert(this.FieldCurrentSeq == this.FieldCount - 1);

            //======== 1. ���FOX�ļ�ͷ ===========
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

            //======= 2. ���ļ�����д���ļ�ͷ ============
            FileStream fsDestDBF = File.Create(this.DBFName);

            byte[] bsFoxHead = fxhFoxHead.ToBytes();
            fsDestDBF.Write(bsFoxHead, 0, bsFoxHead.Length);

            //====== 3. ׼���ֶζ��岿�� =============
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

                //=========== 4. д���ļ� ==============
                byte[] bsField = foxField.ToBytes();
                fsDestDBF.Write(bsField, 0, bsField.Length);
            }

            // ========= 5. д��β��ǣ��ر��ļ� ========
            fsDestDBF.WriteByte(0x0D);

            fsDestDBF.Flush();
            fsDestDBF.Close();
        }
    }
}
