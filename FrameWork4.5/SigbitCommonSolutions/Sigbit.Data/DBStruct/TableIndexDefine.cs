using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Data.DBStruct
{
    /// <summary>
    /// 索引定义
    /// </summary>
    public class TableIndexDefine
    {
        private string _indexName = "";
        /// <summary>
        /// 索引名称
        /// </summary>
        public string IndexName
        {
            get { return _indexName; }
            set { _indexName = value; }
        }

        private string[] _columnNames;
        /// <summary>
        /// 索引相关的列名列表
        /// </summary>
        public string[] ColumnNames
        {
            get { return _columnNames; }
            set { _columnNames = value; }
        }

        private bool _isUniqueIndex = false;
        /// <summary>
        /// 是否唯一索引
        /// </summary>
        public bool IsUniqueIndex
        {
            get { return _isUniqueIndex; }
            set { _isUniqueIndex = value; }
        }

    }

    public class TableIndexDefineItem
    {
        private string _keyName = "";
        /// <summary>
        /// 键名
        /// </summary>
        public string KeyName
        {
            get { return _keyName; }
            set { _keyName = value; }
        }

        private string _columnName = "";
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }

        private bool _isUniqueIndex = false;
        /// <summary>
        /// 是否唯一索引
        /// </summary>
        public bool IsUniqueIndex
        {
            get { return _isUniqueIndex; }
            set { _isUniqueIndex = value; }
        }
    }

}
