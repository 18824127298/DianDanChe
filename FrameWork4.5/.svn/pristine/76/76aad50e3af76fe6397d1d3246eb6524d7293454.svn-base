using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

namespace Sigbit.Data.FoxDBF
{
    class FOXFIELD
    {
        public const int SIZE_OF_FOXFIELD = 32;

        private string _fieldName = "";
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        private DBFFieldType _fieldType;
        /// <summary>
        /// 字段类型
        /// </summary>
        public DBFFieldType FieldType
        {
            get { return _fieldType; }
            set { _fieldType = value; }
        }

        private int _fieldLength = 0;
        /// <summary>
        /// 字段长度
        /// </summary>
        public int FieldLength
        {
            get { return _fieldLength; }
            set { _fieldLength = value; }
        }

        private int _fieldPoint = 0;
        /// <summary>
        /// 小数点后的位数
        /// </summary>
        public int FieldPoint
        {
            get { return _fieldPoint; }
            set { _fieldPoint = value; }
        }

        private int _startOffset = 0;
        /// <summary>
        /// 开始的偏移量
        /// </summary>
        public int StartOffset
        {
            get { return _startOffset; }
            set { _startOffset = value; }
        }

        public override string ToString()
        {
            string sRet = this.FieldName + " " + (char)this.FieldType;

            if (this.FieldType == DBFFieldType.Char)
                sRet += "(" + this.FieldLength + ")";
            else if (this.FieldType == DBFFieldType.Number)
            {
                sRet += "(" + this.FieldLength;
                if (this.FieldPoint != 0)
                    sRet += "." + this.FieldPoint.ToString();
                sRet += ")";
            }

            return sRet;
        }

        public byte[] ToBytes()
        {
            FOXBytesBuilder bbField = new FOXBytesBuilder();

            //========== 1. 字段名 ===========
            bbField.AddString(this.FieldName.ToUpper(), 10, 0);

            //======== 2. 保留 ==========
            bbField.AddByte(0);

            //========== 3. 字段类型 =========
            bbField.AddChar((char)this.FieldType);

            //=========== 4. 保留 =======
            bbField.AddLongNumber(this.StartOffset);

            //========= 5. 长度 =========
            bbField.AddByteNumber(this.FieldLength);

            //=========== 6. 小数点后的位数 ======
            bbField.AddByteNumber(this.FieldPoint);

            //============ 7. 保留 ===========
            bbField.AddBytes(0, 14);

            byte[] bsRet = bbField.ToBytes();
            Debug.Assert(bsRet.Length == SIZE_OF_FOXFIELD);

            return bsRet;
        }

        public void FromBytes(byte[] bsFieldInfo)
        {
            int nPos = 0;

            //========== 1. 字段名 ===========
            this.FieldName = FOXUtil.RXNStringByLength(bsFieldInfo, ref nPos, 10, 0);

            //======== 2. 保留 ==========
            FOXUtil.RXNByte(bsFieldInfo, ref nPos);

            //========== 3. 字段类型 =========
            this.FieldType = (DBFFieldType)FOXUtil.RXNChar(bsFieldInfo, ref nPos);

            //======== 4. 保留 =========
            this.StartOffset = FOXUtil.RXNLongNumber(bsFieldInfo, ref nPos);

            //========= 5. 长度 =========
            this.FieldLength = FOXUtil.RXNByteNumber(bsFieldInfo, ref nPos);

            //=========== 6. 小数点后的位数 ======
            this.FieldPoint = FOXUtil.RXNByteNumber(bsFieldInfo, ref nPos);
        }
    }
}
