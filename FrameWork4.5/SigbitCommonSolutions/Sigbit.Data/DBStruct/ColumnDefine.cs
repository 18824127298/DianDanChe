using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Data.DBStruct
{
    /// <summary>
    /// 字段类型枚举
    /// </summary>
    public enum DataTypeDefine
    {
        /// <summary>
        /// 定长字符字段
        /// </summary>
        Char,
        /// <summary>
        /// 变长字符字段
        /// </summary>
        Varchar,
        /// <summary>
        /// 整形字段
        /// </summary>
        Int,
        /// <summary>
        /// 文本字段
        /// </summary>
        Text,
        /// <summary>
        /// 小数字段
        /// </summary>
        Numeric,
        /// <summary>
        /// 其它数据类型
        /// </summary>
        Other
    }

    public class ColumnDefine
    {
        private string _columnName = "";
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }

        private string _columnNameChs = "";
        /// <summary>
        /// 列中文名称
        /// </summary>
        public string ColumnNameChs
        {
            get { return _columnNameChs; }
            set { _columnNameChs = value; }
        }

        private DataTypeDefine _dataType = DataTypeDefine.Other;
        /// <summary>
        /// 数据类型
        /// </summary>
        public DataTypeDefine DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }

        private string _dataTypeName = "";
        /// <summary>
        /// 具体的数据类型名
        /// </summary>
        public string DataTypeName
        {
            get { return _dataTypeName; }
            set { _dataTypeName = value; }
        }

        private int _length = -1;
        /// <summary>
        /// 数据长度
        /// </summary>
        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }

        private int _lengthPrecision = 0;
        /// <summary>
        /// 小数部分的长度
        /// </summary>
        public int LengthPrecision
        {
            get { return _lengthPrecision; }
            set { _lengthPrecision = value; }
        }


        private Bool3State _canBeNull = Bool3State.Undefine;
        /// <summary>
        /// 是否允许为空
        /// </summary>
        public Bool3State CanBeNull
        {
            get { return _canBeNull; }
            set { _canBeNull = value; }
        }

        private string _defaultValue = "";
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

    }
}
