using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Data.FoxDBF
{
    /// <summary>
    /// 字段定义
    /// </summary>
    class FoxFieldDef
    {
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
        /// 字段精度
        /// </summary>
        public int FieldPoint
        {
            get { return _fieldPoint; }
            set { _fieldPoint = value; }
        }
    }
}
