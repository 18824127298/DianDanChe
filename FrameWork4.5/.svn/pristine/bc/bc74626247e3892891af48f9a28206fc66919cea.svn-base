using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Data.DBStruct
{
    public class TableDefine
    {
        private string _tableName = "";
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        private string _tableNameChs = "";
        /// <summary>
        /// 表中文名称
        /// </summary>
        public string TableNameChs
        {
            get { return _tableNameChs; }
            set { _tableNameChs = value; }
        }

        private ColumnDefineList _columns = new ColumnDefineList();
        /// <summary>
        /// 表的所有列定义
        /// </summary>
        public ColumnDefineList Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }

        private TableIndexDefine _primaryKey = new TableIndexDefine();
        /// <summary>
        /// 表的主键
        /// </summary>
        public TableIndexDefine PrimaryKey
        {
            get { return _primaryKey; }
            set { _primaryKey = value; }
        }

        private TableIndexDefine[] _indexes;
        /// <summary>
        /// 表的所有索引
        /// </summary>
        public TableIndexDefine[] Indexes
        {
            get { return _indexes; }
            set { _indexes = value; }
        }
    }
}
