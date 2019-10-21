using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Data.DBStruct
{
    /// <summary>
    /// 建立规则类型
    /// </summary>
    public enum DBStructBuildRuleType
    {
        /// <summary>
        /// 如果原先有表，则删掉并重建这个表。
        /// </summary>
        RawTableDrop = 1,
        /// <summary>
        /// 如果原先有表，则检查原表的结构和待建规则是否一致，如不一致，则抛出例外。
        /// </summary>
        RawTableCheck = 2,
        /// <summary>
        /// 如果原先有表，则直接跳过。
        /// </summary>
        RawTableSkip = 4,
        /// <summary>
        /// 如果原先有同名表，会抛出例外。
        /// </summary>
        Default = 0
    }

    /// <summary>
    /// 建立规则
    /// </summary>
    public class DBStructBuildRule
    {
        private DBStructBuildRuleType _ruleType = DBStructBuildRuleType.Default;

        /// <summary>
        /// 建立规则类型
        /// </summary>
        public DBStructBuildRuleType RuleType
        {
            get { return _ruleType; }
            set { _ruleType = value; }
        }

        private bool _forceLowerTableName = false;
        /// <summary>
        /// 强制所有表名为小写
        /// </summary>
        public bool ForceLowerTableName
        {
            get { return _forceLowerTableName; }
            set { _forceLowerTableName = value; }
        }

        private bool _forceLowerColumnName = false;
        /// <summary>
        /// 强制所有列名为小写
        /// </summary>
        public bool ForceLowerColumnName
        {
            get { return _forceLowerColumnName; }
            set { _forceLowerColumnName = value; }
        }

        private bool _generateCreateTableSQLOnly = false;
        /// <summary>
        /// 仅生成SQL语句
        /// </summary>
        public bool GenerateCreateTableSQLOnly
        {
            get { return _generateCreateTableSQLOnly; }
            set { _generateCreateTableSQLOnly = value; }
        }


        private string _oracleDefaultDataTableSpace = "TBS_CSP_AIC_DAT";
        /// <summary>
        /// Oracle默认的数据表空间
        /// </summary>
        public string OracleDefaultDataTableSpace
        {
            get { return _oracleDefaultDataTableSpace; }
            set { _oracleDefaultDataTableSpace = value; }
        }

        private string _oracleDefaultIndexTableSpace = "TBS_CSP_AIC_IDX";
        /// <summary>
        /// Oracle默认的索引空间
        /// </summary>
        public string OracleDefaultIndexTableSpace
        {
            get { return _oracleDefaultIndexTableSpace; }
            set { _oracleDefaultIndexTableSpace = value; }
        }


    }


    public class DBStructBuilder
    {
        private string _DBInstanceName = "";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sDBInstanceName"></param>
        public DBStructBuilder(string sDBInstanceName)
        {
            if (sDBInstanceName == "")
            {
                _DBInstanceName = "instanceDefault";
            }
            else
            {
                _DBInstanceName = sDBInstanceName;
            }
        }

        /// <summary>
        /// 建立表
        /// </summary>
        /// <param name="tableDefine"></param>
        /// <param name="buildRule"></param>
        public void BuildTableDefine(TableDefine tableDefine, DBStructBuildRule buildRule)
        {
            ///格式化表名
            if (buildRule.ForceLowerTableName)
            {
                tableDefine.TableName = tableDefine.TableName.ToLower();
            }

            ///格式化列名
            if (buildRule.ForceLowerColumnName)
            {
                for (int i = 0; i < tableDefine.Columns.ColumnCount; i++)
                {
                    tableDefine.Columns.GetColumnDefine(i).ColumnName =
                        tableDefine.Columns.GetColumnDefine(i).ColumnName.ToLower();
                }
            }

            SigbitDBType eDBType = DataHelperConfig.Instance.GetDBType(_DBInstanceName);

            switch (eDBType)
            {
                case SigbitDBType.dbMsSql:
                    CreateMSSQLTable(tableDefine, buildRule);
                    break;
                case SigbitDBType.dbMySql:
                    CreateMySQLTable(tableDefine, buildRule);
                    break;
                case SigbitDBType.dbOracle:
                    CreateOracleTable(tableDefine, buildRule);       //##### 增加一行

                    break;
                default:
                    throw new Exception("未实现对数据库类型" + eDBType + "的处理");
            }

        }


        #region MySQL建表
        private void CreateMySQLTable(TableDefine tableDefine, DBStructBuildRule buildRule)
        {
            switch (buildRule.RuleType)
            {
                case DBStructBuildRuleType.Default:
                    ///如果原先有表,抛出例外
                    //if (CheckMySQLTableExists(tableDefine.TableName))
                    //{
                    //    throw new Exception("建表错误:" + tableDefine.TableName + "已经存在");
                    //}
                    //else          //############本段注释掉
                    //{
                    _CreateMySQLTable(tableDefine, buildRule);
                    //}
                    break;
                case DBStructBuildRuleType.RawTableCheck:
                    ///如果原先有表,删掉并重建这个表
                    if (CheckMySQLTableExists(tableDefine.TableName))
                    {
                        DropMySQLTable(tableDefine.TableName);
                        _CreateMySQLTable(tableDefine, buildRule);
                    }
                    break;
                case DBStructBuildRuleType.RawTableDrop:
                    ///如果原先有表，则检查原表的结构和待建规则是否一致，如不一致，则抛出例外。
                    if (CheckMySQLTableExists(tableDefine.TableName))
                    {
                        //提取表定义
                        DBStructExtractor extractor = new DBStructExtractor(_DBInstanceName);
                        TableDefine tdTableDefine = extractor.ExtractTableDefine(tableDefine.TableName);
                        //检查列数、索引数
                        if (tdTableDefine.Columns.ColumnCount != tableDefine.Columns.ColumnCount)
                        {
                            throw new Exception(tableDefine.TableName + "源表与目标表列数不一致");
                        }
                        if (tdTableDefine.Indexes.Length != tableDefine.Indexes.Length)
                        {
                            throw new Exception(tableDefine.TableName + "源表与目标表索引数不一致");
                        }

                        //检查所有列
                        for (int i = 0; i < tdTableDefine.Columns.ColumnCount; i++)
                        {
                            ColumnDefine cluSrc = tableDefine.Columns.GetColumnDefine(i);
                            ColumnDefine cluDest = tdTableDefine.Columns.GetColumnDefine(i);
                            //比较列名
                            if (cluSrc.ColumnName != cluDest.ColumnName)
                            {
                                throw new Exception("表" + tableDefine.TableName + "中列名不一致,源列名:" +
                                    cluSrc.ColumnName + ",目标列名" + cluDest.ColumnName);
                            }
                            //比较数据类型
                            if (cluSrc.DataType != cluDest.DataType)
                            {
                                throw new Exception("表" + tableDefine.TableName + "中数据类型不一致,源类型:" +
                                    cluSrc.DataType + ",目标类型:" + cluDest.DataType);
                            }
                            //比较数据长度
                            if (cluSrc.Length != cluDest.Length)
                            {
                                throw new Exception("表" + tableDefine.TableName + "中列" + cluSrc.ColumnName +
                                    "长度不一致,源长度:" + cluSrc.Length + ",目标长度:" + cluDest.Length);
                            }
                            //比较小数精度
                            if (cluSrc.LengthPrecision != cluDest.LengthPrecision)
                            {
                                throw new Exception("表" + tableDefine.TableName + "中列" + cluSrc.ColumnName +
                                    "小数精度不一致,源精度:" + cluSrc.LengthPrecision + ",目标精度:" + cluDest.LengthPrecision);
                            }
                            //比较默认值
                            if (cluSrc.DefaultValue != cluDest.DefaultValue)
                            {
                                throw new Exception("表" + tableDefine.TableName + "中列" + cluSrc.ColumnName +
                                    "默认值不一致,源默认值:" + cluSrc.DefaultValue + ",目标默认值:" + cluDest.DefaultValue);
                            }
                            //比较是否为空
                            if (cluSrc.CanBeNull != cluDest.CanBeNull)
                            {
                                throw new Exception("表" + tableDefine.TableName + "中列" + cluSrc.ColumnName +
                                    "是否为空不一致,源值:" + cluSrc.CanBeNull + ",目度:" + cluDest.CanBeNull);
                            }
                        }

                        //检查所有索引
                        for (int i = 0; i < tableDefine.Indexes.Length; i++)
                        {
                            TableIndexDefine indexSrc = tableDefine.Indexes[i];
                            TableIndexDefine indexDest = tdTableDefine.Indexes[i];
                            //比较索引名称
                            if (indexSrc.IndexName != indexDest.IndexName)
                            {
                                throw new Exception("表" + tableDefine.TableName + "中索引名称不一致,源索引名:" +
                                    indexSrc.IndexName + ",目标索引名" + indexDest.IndexName);
                            }
                            //比较唯一索引
                            if (indexSrc.IsUniqueIndex != indexDest.IsUniqueIndex)
                            {
                                throw new Exception("表" + tableDefine.TableName + "中索引" + indexSrc.IndexName +
                                    "唯一索引值不一致,源值:" + indexSrc.IsUniqueIndex + ",目标值" + indexDest.IsUniqueIndex);
                            }
                            //比较所有索引列
                            if (indexSrc.ColumnNames.Length != indexDest.ColumnNames.Length)
                            {
                                throw new Exception("表" + tableDefine.TableName + "中索引" + indexSrc.IndexName +
                                     "列数不一致,源列数:" + indexSrc.ColumnNames.Length + ",目标列数" + indexDest.ColumnNames.Length);
                            }

                            for (int j = 0; j < indexSrc.ColumnNames.Length; j++)
                            {
                                string sIndexSrcColumnName = indexSrc.ColumnNames[j];
                                string sIndexDestColumnName = indexDest.ColumnNames[j];
                                if (sIndexSrcColumnName != sIndexDestColumnName)
                                {
                                    throw new Exception("表" + tableDefine.TableName + "中索引" + indexSrc.IndexName +
                                         "列名不一致:" + sIndexSrcColumnName + ",目标列数" + sIndexDestColumnName);
                                }
                            }
                        }
                    }
                    else
                    {
                        _CreateMySQLTable(tableDefine, buildRule);
                    }
                    break;
                case DBStructBuildRuleType.RawTableSkip:
                    ///如果原先有表，则直接跳过。
                    if (!CheckMySQLTableExists(tableDefine.TableName))
                    {
                        _CreateMySQLTable(tableDefine, buildRule);
                    }
                    break;
            }
        }

        /// <summary>
        /// 检查Mysql表是否存在
        /// </summary>
        /// <param name="sTableName"></param>
        /// <returns></returns>
        private bool CheckMySQLTableExists(string sTableName)
        {
            DBStructExtractor extractor = new DBStructExtractor(_DBInstanceName);
            string[] arrTables = extractor.ExtractAllTablesNames();
            foreach (string sName in arrTables)
            {
                if (sName.ToLower() == sTableName.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        private void DropMySQLTable(string sTableName)
        {
            string sSQL = "drop table " + sTableName;
            DataHelper.GetInstance(_DBInstanceName).ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 建立Mysql表
        /// </summary>
        /// <param name="tableDefine"></param>
        /// <param name="buildRule"></param>
        private void _CreateMySQLTable(TableDefine tableDefine, DBStructBuildRule buildRule)
        {

            ///===================1.建表语句头====================
            string sSQL = "# Table: " + tableDefine.TableName + "\r\n";
            sSQL += "create table " + tableDefine.TableName + "(\r\n";

            ///===================2.累加每一字段==================
            for (int i = 0; i < tableDefine.Columns.ColumnCount; i++)
            {
                ColumnDefine cluDefine = tableDefine.Columns.GetColumnDefine(i);
                string sColumnTypeText = "";
                switch (cluDefine.DataType)
                {
                    case DataTypeDefine.Int:
                        sColumnTypeText = "int(" + cluDefine.Length + ")";
                        break;
                    case DataTypeDefine.Numeric:
                        sColumnTypeText = "numeric(18," + cluDefine.LengthPrecision + ")";
                        break;
                    case DataTypeDefine.Text:
                        sColumnTypeText = "text";
                        break;
                    case DataTypeDefine.Char:
                        sColumnTypeText = "char(" + cluDefine.Length + ")";
                        break;
                    case DataTypeDefine.Varchar:
                        sColumnTypeText = "varchar(" + cluDefine.Length + ")";
                        break;
                    default:
                        throw new Exception("未实现对数据类型" + cluDefine.DataTypeName + "的处理");
                }

                string sNotNullText = "";
                if (cluDefine.CanBeNull == Bool3State.False)
                {
                    sNotNullText = "not null";
                }

                string sDefaultValueText = "";
                if (cluDefine.DefaultValue != null && cluDefine.DefaultValue != "")
                {
                    sDefaultValueText = "default '" + cluDefine.DefaultValue + "'";
                }
                sSQL += string.Format(" {0} {1} {2} {3},\r\n", cluDefine.ColumnName, sColumnTypeText, sNotNullText, sDefaultValueText);

            }

            ///===================3.累加主键字段==================
            TableIndexDefine primaryKey = tableDefine.PrimaryKey;

            if (primaryKey.ColumnNames != null)
            {
                sSQL += " PRIMARY KEY(";
                foreach (string sPKColumnName in tableDefine.PrimaryKey.ColumnNames)
                {
                    sSQL += sPKColumnName + ",";
                }
                sSQL = sSQL.TrimEnd(',') + " ),";
            }

            ///===================4.累加索引字段==================
            if (tableDefine.Indexes.Length > 0)
            {
                for (int i = 0; i < tableDefine.Indexes.Length; i++)
                {
                    TableIndexDefine indexDefine = tableDefine.Indexes[i];
                    ///排除主键
                    if (indexDefine.IndexName != "PRIMARY")
                    {
                        ///是唯一索引
                        if (indexDefine.IsUniqueIndex)
                        {
                            sSQL += " UNIQUE KEY " + indexDefine.IndexName + "(";
                            foreach (string sColumnName in indexDefine.ColumnNames)
                            {
                                sSQL += sColumnName + ",";
                            }
                            sSQL = sSQL.TrimEnd(',') + "),";
                        }
                        else
                        {
                            sSQL += " KEY " + indexDefine.IndexName + "(";
                            foreach (string sColumnName in indexDefine.ColumnNames)
                            {
                                sSQL += sColumnName + ",";
                            }
                            sSQL = sSQL.TrimEnd(',') + "),";
                        }
                    }
                }
            }

            ///===================5.添加建表尾语句================
            sSQL = sSQL.TrimEnd(',') + " )";

            DebugLogger.LogDebugMessage(sSQL, "CreateMysqlTable");

            ///===================6.执行SQL语句创建表=============
            if (buildRule.GenerateCreateTableSQLOnly)
                WriteToCreateTableSQLFile(sSQL + ";\r\n\r\n");
            else
                DataHelper.GetInstance(_DBInstanceName).ExecuteNonQuery(sSQL);
        }
        #endregion

        #region MSSQL建表

        private void CreateMSSQLTable(TableDefine tableDefine, DBStructBuildRule buildRule)
        {
            _CreateMSSQLTable(tableDefine, buildRule);
        }

        private void DropMSSQLTable(string sTableName)
        {
            string sSQL = "drop table " + sTableName;
            DataHelper.GetInstance(_DBInstanceName).ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 建立MSSQL表
        /// </summary>
        /// <param name="tableDefine"></param>
        /// <param name="buildRule"></param>
        private void _CreateMSSQLTable(TableDefine tableDefine, DBStructBuildRule buildRule)
        {
            ///===================1.建表语句头====================
            ///*==============================================================*/
            ///* Table: T_RVR_REMIND                                          */
            ///*==============================================================*/
            string sSQL = "/*==============================================================*/\r\n";
            sSQL += "/* Table: " + tableDefine.TableName.PadRight(54) + "*/\r\n";
            sSQL += "/*==============================================================*/\r\n";
            sSQL += "create table " + tableDefine.TableName + "\r\n(\r\n";

            ///===================2.累加每一字段==================
            for (int i = 0; i < tableDefine.Columns.ColumnCount; i++)
            {
                ColumnDefine cluDefine = tableDefine.Columns.GetColumnDefine(i);
                string sColumnTypeText = "";
                switch (cluDefine.DataType)
                {
                    case DataTypeDefine.Int:
                        sColumnTypeText = "int default 0 not null";
                        break;
                    case DataTypeDefine.Numeric:
                        sColumnTypeText = "numeric(18," + cluDefine.LengthPrecision + ") default 0 not null";
                        break;
                    case DataTypeDefine.Text:
                        sColumnTypeText = "text not null default ''";
                        break;
                    case DataTypeDefine.Char:       //########调整了缺省值的处理方法
                        sColumnTypeText = "varchar(" + cluDefine.Length + ") collate Chinese_PRC_CS_AS_WS default '' not null";
                        break;
                    case DataTypeDefine.Varchar:
                        sColumnTypeText = "varchar(" + cluDefine.Length + ") collate Chinese_PRC_CS_AS_WS default '' not null";
                        break;
                    case DataTypeDefine.Other:
                        if (cluDefine.DataTypeName == "double")
                            sColumnTypeText = "numeric(18,4) default 0 not null";
                        else
                            throw new Exception("未实现对数据类型" + cluDefine.DataTypeName + "的处理");
                        break;
                    default:
                        throw new Exception("未实现对数据类型" + cluDefine.DataTypeName + "的处理");
                }

                if (i == tableDefine.Columns.ColumnCount - 1)
                    sSQL += string.Format("  {0} {1}\r\n", cluDefine.ColumnName, sColumnTypeText);
                else
                    sSQL += string.Format("  {0} {1},\r\n", cluDefine.ColumnName, sColumnTypeText);
            }
            sSQL += ")\r\n";

            //===================3. 主键字段==================
            TableIndexDefine primaryKey = tableDefine.PrimaryKey;

            if (primaryKey.ColumnNames != null)
            {
                sSQL += "alter table " + tableDefine.TableName + "\r\n";
                sSQL += "  add constraint pk_" + tableDefine.TableName + " primary key (";
                foreach (string sPKColumnName in tableDefine.PrimaryKey.ColumnNames)
                {
                    sSQL += sPKColumnName + ",";
                }
                sSQL = sSQL.TrimEnd(',') + ")\r\n";
            }

            //===================6. 执行SQL语句创建表=============
            if (buildRule.GenerateCreateTableSQLOnly)
                WriteToCreateTableSQLFile(sSQL);
            else
                DataHelper.GetInstance(_DBInstanceName).ExecuteNonQuery(sSQL);

            //============ 7. 添加索引 ===============
            if (tableDefine.Indexes.Length <= 0)
                return;

            for (int i = 0; i < tableDefine.Indexes.Length; i++)
            {
                TableIndexDefine indexDefine = tableDefine.Indexes[i];

                //======== 7.1 排除主键 ===============
                if (indexDefine.IndexName == "PRIMARY")
                    continue;

                //========= 7.2 创建索引 =============
                string sIndexSQL = "create";
                if (indexDefine.IsUniqueIndex)
                    sIndexSQL += " unique";
                sIndexSQL += " index " + GetMSSQLIndexName(tableDefine.TableName, indexDefine);
                sIndexSQL += " on " + tableDefine.TableName + " (";

                foreach (string sColumnName in indexDefine.ColumnNames)
                {
                    sIndexSQL += sColumnName + ",";
                }
                sIndexSQL = sIndexSQL.TrimEnd(',');
                sIndexSQL += ");\r\n\r\n";

                if (buildRule.GenerateCreateTableSQLOnly)
                    WriteToCreateTableSQLFile(sIndexSQL);
                else
                    DataHelper.GetInstance(_DBInstanceName).ExecuteNonQuery(sIndexSQL);
            }
        }

        private string GetMSSQLIndexName(string sTableName, TableIndexDefine indexDefine)
        {
            //========== 1. 前缀 ==============
            string sIndexName = "ix_";
            if (indexDefine.IsUniqueIndex)
                sIndexName = "uix_";

            //============ 2. 表名 ============
            sIndexName += sTableName;

            //=========== 3. 各字段名 ============
            for (int i = 0; i < indexDefine.ColumnNames.Length; i++)
            {
                string sColumnName = indexDefine.ColumnNames[i];
                sIndexName += "_" + sColumnName;
            }

            //========= 4. 标记出超长的索引名 ==========
            if (sIndexName.Length > 30)
                sIndexName = "[" + sIndexName + "]";

            return sIndexName;
        }

        #endregion

        #region Oracle建表
        private void CreateOracleTable(TableDefine tableDefine, DBStructBuildRule buildRule)
        {
            _CreateOracleTable(tableDefine, buildRule);
        }

        private void DropOracleTable(string sTableName)
        {
            string sSQL = "drop table " + sTableName;
            DataHelper.GetInstance(_DBInstanceName).ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 建立Oracle表
        /// </summary>
        /// <param name="tableDefine"></param>
        /// <param name="buildRule"></param>
        private void _CreateOracleTable(TableDefine tableDefine, DBStructBuildRule buildRule)
        {
            ///===================1.建表语句头====================
            ///*==============================================================*/
            ///* Table: T_RVR_REMIND                                          */
            ///*==============================================================*/

            string sSQL = "/*==============================================================*/\r\n";
            sSQL += "/* Table: " + tableDefine.TableName.PadRight(54) + "*/\r\n";
            sSQL += "/*==============================================================*/\r\n";

            tableDefine.TableName = DBConnOracle.ConvertLongTableName(tableDefine.TableName);

            sSQL += "create table " + tableDefine.TableName + "\r\n(\r\n";

            ///===================2.累加每一字段==================
            for (int i = 0; i < tableDefine.Columns.ColumnCount; i++)
            {
                ColumnDefine cluDefine = tableDefine.Columns.GetColumnDefine(i);
                string sColumnTypeText = "";
                switch (cluDefine.DataType)
                {
                    case DataTypeDefine.Int:
                        sColumnTypeText = "number(9) default 0 not null";
                        break;
                    case DataTypeDefine.Numeric:
                        sColumnTypeText = "number(18," + cluDefine.LengthPrecision + ") default 0 not null";
                        break;
                    case DataTypeDefine.Text:
                        sColumnTypeText = "varchar2(4000) default '#' not null";
                        break;
                    case DataTypeDefine.Char:       //########调整了缺省值的处理方法
                        sColumnTypeText = "varchar2(" + cluDefine.Length + ") default '#' not null";
                        break;
                    case DataTypeDefine.Varchar:
                        sColumnTypeText = "varchar2(" + cluDefine.Length + ") default '#' not null";
                        break;
                    case DataTypeDefine.Other:
                        if (cluDefine.DataTypeName == "double")
                            sColumnTypeText = "number(18,4) default 0 not null";
                        else
                            throw new Exception("未实现对数据类型" + cluDefine.DataTypeName + "的处理");
                        break;
                    default:
                        throw new Exception("未实现对数据类型" + cluDefine.DataTypeName + "的处理");
                }

                if (i == tableDefine.Columns.ColumnCount - 1)
                    sSQL += string.Format("  {0} {1}\r\n", cluDefine.ColumnName, sColumnTypeText);
                else
                    sSQL += string.Format("  {0} {1},\r\n", cluDefine.ColumnName, sColumnTypeText);
            }
            sSQL += ")\r\n";
            sSQL += "tablespace " + buildRule.OracleDefaultDataTableSpace + ";\r\n\r\n";
            //sSQL += "  pctfree 10\r\n";
            //sSQL += "  initrans 1\r\n";
            //sSQL += "  maxtrans 255\r\n";
            //sSQL += "  storage\r\n";
            //sSQL += "  (\r\n";
            //sSQL += "    initial 64K\r\n";
            //sSQL += "    minextents 1\r\n";
            //sSQL += "    maxextents unlimited\r\n";
            //sSQL += "  );\r\n\r\n";

            //============ 2+. 逐个字段加入定义 ================
            for (int i = 0; i < tableDefine.Columns.ColumnCount; i++)
            {
                ColumnDefine cluDefine = tableDefine.Columns.GetColumnDefine(i);
                string sFieldName = cluDefine.ColumnName;
                string sFieldChs = TCXTableCommentPool.Instance.GetFieldComment
                        (tableDefine.TableName, cluDefine.ColumnName);
                if (sFieldChs == "")
                {
                    DebugLogger.LogWarning("表" + tableDefine.TableName + "的字段"
                            + cluDefine.ColumnName + "找不到定义");
                    continue;
                }
                sSQL += "comment on column " + tableDefine.TableName + "." + cluDefine.ColumnName + " is\r\n";
                sSQL += "\'" + sFieldChs + "\';\r\n\r\n";
            }

            //===================3. 主键字段==================
            TableIndexDefine primaryKey = tableDefine.PrimaryKey;

            if (primaryKey.ColumnNames != null)
            {
                sSQL += "alter table " + tableDefine.TableName + "\r\n";

                string sPrimaryKey = "pk_" + tableDefine.TableName;
                if (sPrimaryKey.Length > 29)
                    sPrimaryKey = sPrimaryKey.Substring(0, 24) + "_" + RandUtil.NewString(4, RandStringType.Number);

                sSQL += "  add constraint " + sPrimaryKey + " primary key (";
                foreach (string sPKColumnName in tableDefine.PrimaryKey.ColumnNames)
                {
                    sSQL += sPKColumnName + ",";
                }
                sSQL = sSQL.TrimEnd(',') + ")\r\n";
            }
            sSQL += "  using index\r\n";
            sSQL += "  tablespace " + buildRule.OracleDefaultIndexTableSpace + ";\r\n\r\n";
            //sSQL += "  pctfree 10\r\n";
            //sSQL += "  initrans 2\r\n";
            //sSQL += "  maxtrans 255\r\n";
            //sSQL += "  storage\r\n";
            //sSQL += "  (\r\n";
            //sSQL += "    initial 64K\r\n";
            //sSQL += "    minextents 1\r\n";
            //sSQL += "    maxextents unlimited\r\n";
            //sSQL += "  );\r\n\r\n";

            //===================6. 执行SQL语句创建表=============
            if (buildRule.GenerateCreateTableSQLOnly)
                WriteToCreateTableSQLFile(sSQL);
            else
                DataHelper.GetInstance(_DBInstanceName).ExecuteNonQuery(sSQL);

            //============ 7. 添加索引 ===============
            if (tableDefine.Indexes.Length <= 0)
                return;

            for (int i = 0; i < tableDefine.Indexes.Length; i++)
            {
                TableIndexDefine indexDefine = tableDefine.Indexes[i];

                //======== 7.1 排除主键 ===============
                if (indexDefine.IndexName == "PRIMARY")
                    continue;

                //========= 7.2 创建索引 =============
                string sIndexSQL = "create";
                if (indexDefine.IsUniqueIndex)
                    sIndexSQL += " unique";
                sIndexSQL += " index " + GetOracleIndexName(tableDefine.TableName, indexDefine);
                sIndexSQL += " on " + tableDefine.TableName + " (";

                foreach (string sColumnName in indexDefine.ColumnNames)
                {
                    sIndexSQL += sColumnName + ",";
                }
                sIndexSQL = sIndexSQL.TrimEnd(',');
                sIndexSQL += ");\r\n\r\n";
                //sIndexSQL += "  tablespace TBS_CSP_AIC_IDX\r\n";
                //sIndexSQL += "  pctfree 10\r\n";
                //sIndexSQL += "  initrans 2\r\n";
                //sIndexSQL += "  maxtrans 255\r\n";
                //sIndexSQL += "  storage\r\n";
                //sIndexSQL += "  (\r\n";
                //sIndexSQL += "    initial 64K\r\n";
                //sIndexSQL += "    minextents 1\r\n";
                //sIndexSQL += "    maxextents unlimited\r\n";
                //sIndexSQL += "  );\r\n\r\n";

                if (buildRule.GenerateCreateTableSQLOnly)
                    WriteToCreateTableSQLFile(sIndexSQL);
                else
                    DataHelper.GetInstance(_DBInstanceName).ExecuteNonQuery(sIndexSQL);
            }
        }

        private string GetOracleIndexName(string sTableName, TableIndexDefine indexDefine)
        {
            //========== 1. 前缀 ==============
            string sIndexName = "ix_";
            if (indexDefine.IsUniqueIndex)
                sIndexName = "uix_";

            //============ 2. 表名 ============
            sIndexName += sTableName;

            //=========== 3. 各字段名 ============
            for (int i = 0; i < indexDefine.ColumnNames.Length; i++)
            {
                string sColumnName = indexDefine.ColumnNames[i];
                sIndexName += "_" + sColumnName;
            }

            //========= 4. 标记出超长的索引名(%20111216-改为低于30个字符%) ==========
            if (sIndexName.Length > 29)
                sIndexName = sIndexName.Substring(0, 24) + "_" + RandUtil.NewString(4, RandStringType.Number);
            //sIndexName = "[" + sIndexName + "]";

            return sIndexName;
        }

        #endregion

        private void WriteToCreateTableSQLFile(string sSQL)
        {
            string sFileName = "c:\\temp\\create_tableJBHH.sql";

            //========= 2. 写入文件 =========
            FileStream fs = File.OpenWrite(sFileName);
            fs.Seek(0, SeekOrigin.End);

            byte[] bsLog = Encoding.GetEncoding("gb2312").GetBytes(sSQL);
            fs.Write(bsLog, 0, bsLog.Length);

            fs.Flush();
            fs.Close();
        }
    }


}
