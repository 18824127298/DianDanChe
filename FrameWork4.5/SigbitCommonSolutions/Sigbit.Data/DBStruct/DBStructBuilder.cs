using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Data.DBStruct
{
    /// <summary>
    /// ������������
    /// </summary>
    public enum DBStructBuildRuleType
    {
        /// <summary>
        /// ���ԭ���б���ɾ�����ؽ������
        /// </summary>
        RawTableDrop = 1,
        /// <summary>
        /// ���ԭ���б�����ԭ��Ľṹ�ʹ��������Ƿ�һ�£��粻һ�£����׳����⡣
        /// </summary>
        RawTableCheck = 2,
        /// <summary>
        /// ���ԭ���б���ֱ��������
        /// </summary>
        RawTableSkip = 4,
        /// <summary>
        /// ���ԭ����ͬ�������׳����⡣
        /// </summary>
        Default = 0
    }

    /// <summary>
    /// ��������
    /// </summary>
    public class DBStructBuildRule
    {
        private DBStructBuildRuleType _ruleType = DBStructBuildRuleType.Default;

        /// <summary>
        /// ������������
        /// </summary>
        public DBStructBuildRuleType RuleType
        {
            get { return _ruleType; }
            set { _ruleType = value; }
        }

        private bool _forceLowerTableName = false;
        /// <summary>
        /// ǿ�����б���ΪСд
        /// </summary>
        public bool ForceLowerTableName
        {
            get { return _forceLowerTableName; }
            set { _forceLowerTableName = value; }
        }

        private bool _forceLowerColumnName = false;
        /// <summary>
        /// ǿ����������ΪСд
        /// </summary>
        public bool ForceLowerColumnName
        {
            get { return _forceLowerColumnName; }
            set { _forceLowerColumnName = value; }
        }

        private bool _generateCreateTableSQLOnly = false;
        /// <summary>
        /// ������SQL���
        /// </summary>
        public bool GenerateCreateTableSQLOnly
        {
            get { return _generateCreateTableSQLOnly; }
            set { _generateCreateTableSQLOnly = value; }
        }


        private string _oracleDefaultDataTableSpace = "TBS_CSP_AIC_DAT";
        /// <summary>
        /// OracleĬ�ϵ����ݱ�ռ�
        /// </summary>
        public string OracleDefaultDataTableSpace
        {
            get { return _oracleDefaultDataTableSpace; }
            set { _oracleDefaultDataTableSpace = value; }
        }

        private string _oracleDefaultIndexTableSpace = "TBS_CSP_AIC_IDX";
        /// <summary>
        /// OracleĬ�ϵ������ռ�
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
        /// ���캯��
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
        /// ������
        /// </summary>
        /// <param name="tableDefine"></param>
        /// <param name="buildRule"></param>
        public void BuildTableDefine(TableDefine tableDefine, DBStructBuildRule buildRule)
        {
            ///��ʽ������
            if (buildRule.ForceLowerTableName)
            {
                tableDefine.TableName = tableDefine.TableName.ToLower();
            }

            ///��ʽ������
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
                    CreateOracleTable(tableDefine, buildRule);       //##### ����һ��

                    break;
                default:
                    throw new Exception("δʵ�ֶ����ݿ�����" + eDBType + "�Ĵ���");
            }

        }


        #region MySQL����
        private void CreateMySQLTable(TableDefine tableDefine, DBStructBuildRule buildRule)
        {
            switch (buildRule.RuleType)
            {
                case DBStructBuildRuleType.Default:
                    ///���ԭ���б�,�׳�����
                    //if (CheckMySQLTableExists(tableDefine.TableName))
                    //{
                    //    throw new Exception("�������:" + tableDefine.TableName + "�Ѿ�����");
                    //}
                    //else          //############����ע�͵�
                    //{
                    _CreateMySQLTable(tableDefine, buildRule);
                    //}
                    break;
                case DBStructBuildRuleType.RawTableCheck:
                    ///���ԭ���б�,ɾ�����ؽ������
                    if (CheckMySQLTableExists(tableDefine.TableName))
                    {
                        DropMySQLTable(tableDefine.TableName);
                        _CreateMySQLTable(tableDefine, buildRule);
                    }
                    break;
                case DBStructBuildRuleType.RawTableDrop:
                    ///���ԭ���б�����ԭ��Ľṹ�ʹ��������Ƿ�һ�£��粻һ�£����׳����⡣
                    if (CheckMySQLTableExists(tableDefine.TableName))
                    {
                        //��ȡ����
                        DBStructExtractor extractor = new DBStructExtractor(_DBInstanceName);
                        TableDefine tdTableDefine = extractor.ExtractTableDefine(tableDefine.TableName);
                        //���������������
                        if (tdTableDefine.Columns.ColumnCount != tableDefine.Columns.ColumnCount)
                        {
                            throw new Exception(tableDefine.TableName + "Դ����Ŀ���������һ��");
                        }
                        if (tdTableDefine.Indexes.Length != tableDefine.Indexes.Length)
                        {
                            throw new Exception(tableDefine.TableName + "Դ����Ŀ�����������һ��");
                        }

                        //���������
                        for (int i = 0; i < tdTableDefine.Columns.ColumnCount; i++)
                        {
                            ColumnDefine cluSrc = tableDefine.Columns.GetColumnDefine(i);
                            ColumnDefine cluDest = tdTableDefine.Columns.GetColumnDefine(i);
                            //�Ƚ�����
                            if (cluSrc.ColumnName != cluDest.ColumnName)
                            {
                                throw new Exception("��" + tableDefine.TableName + "��������һ��,Դ����:" +
                                    cluSrc.ColumnName + ",Ŀ������" + cluDest.ColumnName);
                            }
                            //�Ƚ���������
                            if (cluSrc.DataType != cluDest.DataType)
                            {
                                throw new Exception("��" + tableDefine.TableName + "���������Ͳ�һ��,Դ����:" +
                                    cluSrc.DataType + ",Ŀ������:" + cluDest.DataType);
                            }
                            //�Ƚ����ݳ���
                            if (cluSrc.Length != cluDest.Length)
                            {
                                throw new Exception("��" + tableDefine.TableName + "����" + cluSrc.ColumnName +
                                    "���Ȳ�һ��,Դ����:" + cluSrc.Length + ",Ŀ�곤��:" + cluDest.Length);
                            }
                            //�Ƚ�С������
                            if (cluSrc.LengthPrecision != cluDest.LengthPrecision)
                            {
                                throw new Exception("��" + tableDefine.TableName + "����" + cluSrc.ColumnName +
                                    "С�����Ȳ�һ��,Դ����:" + cluSrc.LengthPrecision + ",Ŀ�꾫��:" + cluDest.LengthPrecision);
                            }
                            //�Ƚ�Ĭ��ֵ
                            if (cluSrc.DefaultValue != cluDest.DefaultValue)
                            {
                                throw new Exception("��" + tableDefine.TableName + "����" + cluSrc.ColumnName +
                                    "Ĭ��ֵ��һ��,ԴĬ��ֵ:" + cluSrc.DefaultValue + ",Ŀ��Ĭ��ֵ:" + cluDest.DefaultValue);
                            }
                            //�Ƚ��Ƿ�Ϊ��
                            if (cluSrc.CanBeNull != cluDest.CanBeNull)
                            {
                                throw new Exception("��" + tableDefine.TableName + "����" + cluSrc.ColumnName +
                                    "�Ƿ�Ϊ�ղ�һ��,Դֵ:" + cluSrc.CanBeNull + ",Ŀ��:" + cluDest.CanBeNull);
                            }
                        }

                        //�����������
                        for (int i = 0; i < tableDefine.Indexes.Length; i++)
                        {
                            TableIndexDefine indexSrc = tableDefine.Indexes[i];
                            TableIndexDefine indexDest = tdTableDefine.Indexes[i];
                            //�Ƚ���������
                            if (indexSrc.IndexName != indexDest.IndexName)
                            {
                                throw new Exception("��" + tableDefine.TableName + "���������Ʋ�һ��,Դ������:" +
                                    indexSrc.IndexName + ",Ŀ��������" + indexDest.IndexName);
                            }
                            //�Ƚ�Ψһ����
                            if (indexSrc.IsUniqueIndex != indexDest.IsUniqueIndex)
                            {
                                throw new Exception("��" + tableDefine.TableName + "������" + indexSrc.IndexName +
                                    "Ψһ����ֵ��һ��,Դֵ:" + indexSrc.IsUniqueIndex + ",Ŀ��ֵ" + indexDest.IsUniqueIndex);
                            }
                            //�Ƚ�����������
                            if (indexSrc.ColumnNames.Length != indexDest.ColumnNames.Length)
                            {
                                throw new Exception("��" + tableDefine.TableName + "������" + indexSrc.IndexName +
                                     "������һ��,Դ����:" + indexSrc.ColumnNames.Length + ",Ŀ������" + indexDest.ColumnNames.Length);
                            }

                            for (int j = 0; j < indexSrc.ColumnNames.Length; j++)
                            {
                                string sIndexSrcColumnName = indexSrc.ColumnNames[j];
                                string sIndexDestColumnName = indexDest.ColumnNames[j];
                                if (sIndexSrcColumnName != sIndexDestColumnName)
                                {
                                    throw new Exception("��" + tableDefine.TableName + "������" + indexSrc.IndexName +
                                         "������һ��:" + sIndexSrcColumnName + ",Ŀ������" + sIndexDestColumnName);
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
                    ///���ԭ���б���ֱ��������
                    if (!CheckMySQLTableExists(tableDefine.TableName))
                    {
                        _CreateMySQLTable(tableDefine, buildRule);
                    }
                    break;
            }
        }

        /// <summary>
        /// ���Mysql���Ƿ����
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
        /// ����Mysql��
        /// </summary>
        /// <param name="tableDefine"></param>
        /// <param name="buildRule"></param>
        private void _CreateMySQLTable(TableDefine tableDefine, DBStructBuildRule buildRule)
        {

            ///===================1.�������ͷ====================
            string sSQL = "# Table: " + tableDefine.TableName + "\r\n";
            sSQL += "create table " + tableDefine.TableName + "(\r\n";

            ///===================2.�ۼ�ÿһ�ֶ�==================
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
                        throw new Exception("δʵ�ֶ���������" + cluDefine.DataTypeName + "�Ĵ���");
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

            ///===================3.�ۼ������ֶ�==================
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

            ///===================4.�ۼ������ֶ�==================
            if (tableDefine.Indexes.Length > 0)
            {
                for (int i = 0; i < tableDefine.Indexes.Length; i++)
                {
                    TableIndexDefine indexDefine = tableDefine.Indexes[i];
                    ///�ų�����
                    if (indexDefine.IndexName != "PRIMARY")
                    {
                        ///��Ψһ����
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

            ///===================5.��ӽ���β���================
            sSQL = sSQL.TrimEnd(',') + " )";

            DebugLogger.LogDebugMessage(sSQL, "CreateMysqlTable");

            ///===================6.ִ��SQL��䴴����=============
            if (buildRule.GenerateCreateTableSQLOnly)
                WriteToCreateTableSQLFile(sSQL + ";\r\n\r\n");
            else
                DataHelper.GetInstance(_DBInstanceName).ExecuteNonQuery(sSQL);
        }
        #endregion

        #region MSSQL����

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
        /// ����MSSQL��
        /// </summary>
        /// <param name="tableDefine"></param>
        /// <param name="buildRule"></param>
        private void _CreateMSSQLTable(TableDefine tableDefine, DBStructBuildRule buildRule)
        {
            ///===================1.�������ͷ====================
            ///*==============================================================*/
            ///* Table: T_RVR_REMIND                                          */
            ///*==============================================================*/
            string sSQL = "/*==============================================================*/\r\n";
            sSQL += "/* Table: " + tableDefine.TableName.PadRight(54) + "*/\r\n";
            sSQL += "/*==============================================================*/\r\n";
            sSQL += "create table " + tableDefine.TableName + "\r\n(\r\n";

            ///===================2.�ۼ�ÿһ�ֶ�==================
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
                    case DataTypeDefine.Char:       //########������ȱʡֵ�Ĵ�����
                        sColumnTypeText = "varchar(" + cluDefine.Length + ") collate Chinese_PRC_CS_AS_WS default '' not null";
                        break;
                    case DataTypeDefine.Varchar:
                        sColumnTypeText = "varchar(" + cluDefine.Length + ") collate Chinese_PRC_CS_AS_WS default '' not null";
                        break;
                    case DataTypeDefine.Other:
                        if (cluDefine.DataTypeName == "double")
                            sColumnTypeText = "numeric(18,4) default 0 not null";
                        else
                            throw new Exception("δʵ�ֶ���������" + cluDefine.DataTypeName + "�Ĵ���");
                        break;
                    default:
                        throw new Exception("δʵ�ֶ���������" + cluDefine.DataTypeName + "�Ĵ���");
                }

                if (i == tableDefine.Columns.ColumnCount - 1)
                    sSQL += string.Format("  {0} {1}\r\n", cluDefine.ColumnName, sColumnTypeText);
                else
                    sSQL += string.Format("  {0} {1},\r\n", cluDefine.ColumnName, sColumnTypeText);
            }
            sSQL += ")\r\n";

            //===================3. �����ֶ�==================
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

            //===================6. ִ��SQL��䴴����=============
            if (buildRule.GenerateCreateTableSQLOnly)
                WriteToCreateTableSQLFile(sSQL);
            else
                DataHelper.GetInstance(_DBInstanceName).ExecuteNonQuery(sSQL);

            //============ 7. ������� ===============
            if (tableDefine.Indexes.Length <= 0)
                return;

            for (int i = 0; i < tableDefine.Indexes.Length; i++)
            {
                TableIndexDefine indexDefine = tableDefine.Indexes[i];

                //======== 7.1 �ų����� ===============
                if (indexDefine.IndexName == "PRIMARY")
                    continue;

                //========= 7.2 �������� =============
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
            //========== 1. ǰ׺ ==============
            string sIndexName = "ix_";
            if (indexDefine.IsUniqueIndex)
                sIndexName = "uix_";

            //============ 2. ���� ============
            sIndexName += sTableName;

            //=========== 3. ���ֶ��� ============
            for (int i = 0; i < indexDefine.ColumnNames.Length; i++)
            {
                string sColumnName = indexDefine.ColumnNames[i];
                sIndexName += "_" + sColumnName;
            }

            //========= 4. ��ǳ������������� ==========
            if (sIndexName.Length > 30)
                sIndexName = "[" + sIndexName + "]";

            return sIndexName;
        }

        #endregion

        #region Oracle����
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
        /// ����Oracle��
        /// </summary>
        /// <param name="tableDefine"></param>
        /// <param name="buildRule"></param>
        private void _CreateOracleTable(TableDefine tableDefine, DBStructBuildRule buildRule)
        {
            ///===================1.�������ͷ====================
            ///*==============================================================*/
            ///* Table: T_RVR_REMIND                                          */
            ///*==============================================================*/

            string sSQL = "/*==============================================================*/\r\n";
            sSQL += "/* Table: " + tableDefine.TableName.PadRight(54) + "*/\r\n";
            sSQL += "/*==============================================================*/\r\n";

            tableDefine.TableName = DBConnOracle.ConvertLongTableName(tableDefine.TableName);

            sSQL += "create table " + tableDefine.TableName + "\r\n(\r\n";

            ///===================2.�ۼ�ÿһ�ֶ�==================
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
                    case DataTypeDefine.Char:       //########������ȱʡֵ�Ĵ�����
                        sColumnTypeText = "varchar2(" + cluDefine.Length + ") default '#' not null";
                        break;
                    case DataTypeDefine.Varchar:
                        sColumnTypeText = "varchar2(" + cluDefine.Length + ") default '#' not null";
                        break;
                    case DataTypeDefine.Other:
                        if (cluDefine.DataTypeName == "double")
                            sColumnTypeText = "number(18,4) default 0 not null";
                        else
                            throw new Exception("δʵ�ֶ���������" + cluDefine.DataTypeName + "�Ĵ���");
                        break;
                    default:
                        throw new Exception("δʵ�ֶ���������" + cluDefine.DataTypeName + "�Ĵ���");
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

            //============ 2+. ����ֶμ��붨�� ================
            for (int i = 0; i < tableDefine.Columns.ColumnCount; i++)
            {
                ColumnDefine cluDefine = tableDefine.Columns.GetColumnDefine(i);
                string sFieldName = cluDefine.ColumnName;
                string sFieldChs = TCXTableCommentPool.Instance.GetFieldComment
                        (tableDefine.TableName, cluDefine.ColumnName);
                if (sFieldChs == "")
                {
                    DebugLogger.LogWarning("��" + tableDefine.TableName + "���ֶ�"
                            + cluDefine.ColumnName + "�Ҳ�������");
                    continue;
                }
                sSQL += "comment on column " + tableDefine.TableName + "." + cluDefine.ColumnName + " is\r\n";
                sSQL += "\'" + sFieldChs + "\';\r\n\r\n";
            }

            //===================3. �����ֶ�==================
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

            //===================6. ִ��SQL��䴴����=============
            if (buildRule.GenerateCreateTableSQLOnly)
                WriteToCreateTableSQLFile(sSQL);
            else
                DataHelper.GetInstance(_DBInstanceName).ExecuteNonQuery(sSQL);

            //============ 7. ������� ===============
            if (tableDefine.Indexes.Length <= 0)
                return;

            for (int i = 0; i < tableDefine.Indexes.Length; i++)
            {
                TableIndexDefine indexDefine = tableDefine.Indexes[i];

                //======== 7.1 �ų����� ===============
                if (indexDefine.IndexName == "PRIMARY")
                    continue;

                //========= 7.2 �������� =============
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
            //========== 1. ǰ׺ ==============
            string sIndexName = "ix_";
            if (indexDefine.IsUniqueIndex)
                sIndexName = "uix_";

            //============ 2. ���� ============
            sIndexName += sTableName;

            //=========== 3. ���ֶ��� ============
            for (int i = 0; i < indexDefine.ColumnNames.Length; i++)
            {
                string sColumnName = indexDefine.ColumnNames[i];
                sIndexName += "_" + sColumnName;
            }

            //========= 4. ��ǳ�������������(%20111216-��Ϊ����30���ַ�%) ==========
            if (sIndexName.Length > 29)
                sIndexName = sIndexName.Substring(0, 24) + "_" + RandUtil.NewString(4, RandStringType.Number);
            //sIndexName = "[" + sIndexName + "]";

            return sIndexName;
        }

        #endregion

        private void WriteToCreateTableSQLFile(string sSQL)
        {
            string sFileName = "c:\\temp\\create_tableJBHH.sql";

            //========= 2. д���ļ� =========
            FileStream fs = File.OpenWrite(sFileName);
            fs.Seek(0, SeekOrigin.End);

            byte[] bsLog = Encoding.GetEncoding("gb2312").GetBytes(sSQL);
            fs.Write(bsLog, 0, bsLog.Length);

            fs.Flush();
            fs.Close();
        }
    }


}
