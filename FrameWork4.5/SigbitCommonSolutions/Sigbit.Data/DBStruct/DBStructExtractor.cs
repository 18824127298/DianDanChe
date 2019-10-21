using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Data.DBStruct
{
    public class DBStructExtractor
    {
        private string _instanceName = "";
        private SigbitDBType eDBType;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sInstanceName">数据库实例名称</param>
        public DBStructExtractor(string sDBInstanceName)
        {

            if (sDBInstanceName == "")
            {
                sDBInstanceName = "instanceDefault";
            }
            _instanceName = sDBInstanceName;
            eDBType = DataHelperConfig.Instance.GetDBType(_instanceName);

        }

        /// <summary>
        /// 提取所有的表名称
        /// </summary>
        /// <returns></returns>
        public string[] ExtractAllTablesNames()
        {
            string sSQL = "";
            switch (eDBType)
            {
                ///SQLServer数据库
                case SigbitDBType.dbMsSql:
                    sSQL = "select name from sysobjects where type='u'";
                    break;
                ///Mysql数据库
                case SigbitDBType.dbMySql:
                    sSQL = "show tables from " + GetMysqlDBName();
                    break;
                ///Oracle数据库
                case SigbitDBType.dbOracle:
                    sSQL = "select * from tabs";
                    break;
                default:
                    throw new Exception("暂未实现对" + eDBType + "类型的数据库处理");
            }

            DataSet ds = DataHelper.GetInstance(_instanceName).ExecuteDataSet(sSQL);
            string[] arrTableNames = new string[ds.Tables[0].Rows.Count];
            for (int i = 0; i < arrTableNames.Length; i++)
            {
                arrTableNames[i] = ds.Tables[0].Rows[i][0].ToString();
            }
            return arrTableNames;

        }

        /// <summary>
        /// 提取表结构的定义
        /// </summary>
        /// <param name="sTableName">表名</param>
        /// <returns></returns>
        public TableDefine ExtractTableDefine(string sTableName)
        {

            switch (eDBType)
            {
                case SigbitDBType.dbMsSql:
                    return ExtractMSSQLTableDefine(sTableName);
                case SigbitDBType.dbMySql:
                    return ExtractMysqlTableDefine(sTableName);
                default:
                    throw new Exception("ExtractTableDefine:未实现对" + eDBType + "的处理");
            }
        }


        #region Mysql相关处理
        /// <summary>
        /// 取出Mysql表定义
        /// </summary>
        /// <param name="sTableName"></param>
        /// <returns></returns>
        private TableDefine ExtractMysqlTableDefine(string sTableName)
        {
            TableDefine tableDefine = new TableDefine();
            tableDefine.TableName = sTableName;
            string sDBName = GetMysqlDBName();

            //======================1.获得所有列信息=========================
            string sSQL = "desc " + sDBName + "." + sTableName;
            DataSet ds = DataHelper.GetInstance(_instanceName).ExecuteDataSet(sSQL);
            foreach (DataRow dRow in ds.Tables[0].Rows)
            {
                ColumnDefine cluDefine = new ColumnDefine();
                if (dRow["Null"].ToString() == "YES")
                {
                    cluDefine.CanBeNull = Bool3State.True;
                }
                else
                {
                    cluDefine.CanBeNull = Bool3State.False;
                }
                cluDefine.ColumnName = dRow["field"].ToString();
                ParseMysqlDataType(ref cluDefine, dRow["Type"].ToString());
                cluDefine.DefaultValue = dRow["Default"].ToString();
                tableDefine.Columns.AddColumnDefine(cluDefine);
            }

            //=================2.获得主键及索引信息===========================

            sSQL = "show index from " + sDBName + "." + sTableName;
            ds = DataHelper.GetInstance(_instanceName).ExecuteDataSet(sSQL);
            Hashtable htIndex = new Hashtable();
            foreach (DataRow dRow in ds.Tables[0].Rows)
            {
                string sIndexName = dRow["Key_name"].ToString();
                string sColumnName = dRow["Column_Name"].ToString();
                string sUnique = dRow["Non_unique"].ToString();
                bool bIsUnique = false;
                if (sUnique == "0")
                {
                    bIsUnique = true;
                }
                if (htIndex.Contains(sIndexName))
                {
                    ArrayList arrIndexColumns = (ArrayList)htIndex[sIndexName];
                    TableIndexDefineItem indexItem = new TableIndexDefineItem();
                    indexItem.ColumnName = sColumnName;
                    indexItem.IsUniqueIndex = bIsUnique;
                    indexItem.KeyName = sIndexName;
                    arrIndexColumns.Add(indexItem);
                }
                else
                {
                    ArrayList arrIndexColumns = new ArrayList();
                    TableIndexDefineItem indexItem = new TableIndexDefineItem();
                    indexItem.ColumnName = sColumnName;
                    indexItem.IsUniqueIndex = bIsUnique;
                    indexItem.KeyName = sIndexName;
                    arrIndexColumns.Add(indexItem);
                    htIndex[sIndexName] = arrIndexColumns;
                }
            }


            //================3.设置索引===========================

            ArrayList arrTableIndex = new ArrayList();
            foreach (object obj in htIndex.Keys)
            {
                TableIndexDefine indexDefine = new TableIndexDefine();
                indexDefine.IndexName = obj.ToString();
                ArrayList arrIndexColumns = (ArrayList)htIndex[obj];
                ArrayList arrColumns = new ArrayList();
                foreach (object objColumn in arrIndexColumns)
                {
                    TableIndexDefineItem item = (TableIndexDefineItem)objColumn;
                    arrColumns.Add(item.ColumnName);
                    indexDefine.IsUniqueIndex = item.IsUniqueIndex;
                }
                indexDefine.ColumnNames = (string[])arrColumns.ToArray("".GetType());
                arrTableIndex.Add(indexDefine);
                if (indexDefine.IndexName == "PRIMARY")
                {
                    ///设置主键索引
                    tableDefine.PrimaryKey = indexDefine;
                }
            }
            tableDefine.Indexes = (TableIndexDefine[])arrTableIndex.ToArray(new TableIndexDefine().GetType());

            return tableDefine;
        }


        /// <summary>
        /// 获取Mysql数据库名
        /// </summary>
        /// <returns></returns>
        private string GetMysqlDBName()
        {
            string sConectString = DataHelperConfig.Instance.GetConnectString(_instanceName);
            string sReg = "Data Source=(.*?);";
            string sDBName = Regex.Match(sConectString, sReg).Groups[1].Value;
            return sDBName;
        }


        /// <summary>
        /// 解析Mysql数据类型及长度
        /// </summary>
        /// <param name="cluDefine"></param>
        /// <param name="sDataTypeString"></param>
        private void ParseMysqlDataType(ref ColumnDefine cluDefine, string sDataTypeString)
        {
            //示例：varchar(255)
            string sDataType = "";
            int nTypeLength = 0;
            int nPrecLength = 0;
            if (sDataTypeString.Contains("("))
            {
                int nIndexLeft = sDataTypeString.IndexOf('(');
                int nIndexRigth = sDataTypeString.IndexOf(')');
                sDataType = sDataTypeString.Substring(0, nIndexLeft);
                string sTypeLengthString = sDataTypeString.Substring(nIndexLeft + 1, nIndexRigth - nIndexLeft - 1);
                if (sTypeLengthString.Contains(","))
                {
                    //示例:numeric(18,3)
                    nPrecLength = ConvertUtil.ToInt(sTypeLengthString.Split(',')[1]);
                }
                else
                {
                    nTypeLength = ConvertUtil.ToInt(sTypeLengthString);
                }
            }
            else
            {
                sDataType = sDataTypeString;
            }

            cluDefine.DataTypeName = sDataType;
            switch (sDataType)
            {
                case "char":
                    cluDefine.DataType = DataTypeDefine.Char;
                    cluDefine.Length = nTypeLength;
                    break;
                case "varchar":
                    cluDefine.DataType = DataTypeDefine.Varchar;
                    cluDefine.Length = nTypeLength;
                    break;
                case "int":
                    cluDefine.DataType = DataTypeDefine.Int;
                    cluDefine.Length = nTypeLength;
                    break;
                case "decimal":
                    cluDefine.DataType = DataTypeDefine.Numeric;
                    cluDefine.LengthPrecision = nPrecLength;
                    break;
                case "text":
                    cluDefine.DataType = DataTypeDefine.Text;
                    break;
                default:
                    cluDefine.DataType = DataTypeDefine.Other;
                    break;
            }

        }
        #endregion

        #region MSSQL相关处理
        /// <summary>
        /// 取出SQLServer表定义
        /// </summary>
        /// <param name="sTableName"></param>
        /// <returns></returns>
        private TableDefine ExtractMSSQLTableDefine(string sTableName)
        {
            TableDefine tableDefine = new TableDefine();
            tableDefine.TableName = sTableName;

            ///获取列类型映射
            Hashtable htTypes = new Hashtable();
            string sSQL = "select distinct xtype,name from systypes where name!='sysname'";
            DataSet ds = DataHelper.GetInstance(_instanceName).ExecuteDataSet(sSQL);
            foreach (DataRow dRow in ds.Tables[0].Rows)
            {
                object oId = dRow[0];
                string sType = dRow[1].ToString();
                htTypes.Add(oId, sType);
            }

            ///获取默认值映射
            Hashtable htDefaultValue = new Hashtable();
            sSQL = "select id,text from syscomments where number=0";
            ds = DataHelper.GetInstance(_instanceName).ExecuteDataSet(sSQL);
            foreach (DataRow dRow in ds.Tables[0].Rows)
            {
                string sId = dRow[0].ToString();
                string sDefaultValue = dRow[1].ToString();
                htDefaultValue.Add(sId, sDefaultValue);
            }

            //获得所有列信息
            sSQL = "select name,xtype,length,xscale,isnullable,cdefault from syscolumns where id in(select id from sysobjects where name='"
                + sTableName + "')";

            ds = DataHelper.GetInstance(_instanceName).ExecuteDataSet(sSQL);
            foreach (DataRow dRow in ds.Tables[0].Rows)
            {
                ColumnDefine cluDefine = new ColumnDefine();

                string sColumnName = dRow["name"].ToString();
                string sDataType = htTypes[dRow["xtype"]].ToString();
                int nLength = ConvertUtil.ToInt(dRow["length"]);
                int nScale = ConvertUtil.ToInt(dRow["xscale"]);
                string sDefaultValueID = dRow["cdefault"].ToString();

                cluDefine.ColumnName = sColumnName;
                cluDefine.DataTypeName = sDataType;
                string sDefaultValue = null;
                if (htDefaultValue.Contains(sDefaultValueID))
                {
                    sDefaultValue = htDefaultValue[sDefaultValueID].ToString();
                    cluDefine.DefaultValue = sDefaultValue.TrimStart('(').TrimEnd(')');
                }
                else
                {
                    cluDefine.DefaultValue = null;
                }

                switch (sDataType)
                {
                    case "char":
                        cluDefine.DataType = DataTypeDefine.Char;
                        cluDefine.Length = nLength;
                        break;
                    case "varchar":
                        cluDefine.DataType = DataTypeDefine.Varchar;
                        cluDefine.Length = nLength;
                        break;
                    case "int":
                        cluDefine.DataType = DataTypeDefine.Int;
                        cluDefine.Length = nLength;
                        break;
                    case "numeric":
                        cluDefine.DataType = DataTypeDefine.Numeric;
                        cluDefine.LengthPrecision = nScale;
                        break;
                    case "text":
                        cluDefine.DataType = DataTypeDefine.Text;
                        break;
                    default:
                        cluDefine.DataType = DataTypeDefine.Other;
                        break;
                }

                tableDefine.Columns.AddColumnDefine(cluDefine);
            }


            ///获得主键及索引信息
            ///Note:目前未考虑SQLServer的聚焦索引
            sSQL = "sp_helpindex " + sTableName;
            ds = DataHelper.GetInstance(_instanceName).ExecuteDataSet(sSQL);
            TableIndexDefine[] arrIndex = new TableIndexDefine[ds.Tables[0].Rows.Count];

            for (int i = 0; i < arrIndex.Length; i++)
            {
                DataRow dRow = ds.Tables[0].Rows[i];
                string sIndexName = dRow[0].ToString();
                string sIndexDesc = dRow[1].ToString();
                string[] arrIndexColumns = dRow[2].ToString().Split(',');
                for (int j = 0; j < arrIndexColumns.Length; j++)
                {
                    arrIndexColumns[j] = arrIndexColumns[j].Trim();
                }
                TableIndexDefine indexDefine = new TableIndexDefine();
                indexDefine.IndexName = sIndexName;
                ///描述示例:clustered, unique, primary key located on PRIMARY
                if (sIndexDesc.Contains("unique"))
                {
                    indexDefine.IsUniqueIndex = true;
                }
                indexDefine.ColumnNames = arrIndexColumns;

                if (sIndexDesc.Contains("primary key"))
                {
                    tableDefine.PrimaryKey = indexDefine;
                }

                arrIndex[i] = indexDefine;
            }
            tableDefine.Indexes = arrIndex;

            return tableDefine;
        }




        #endregion

        #region Oracle相关处理

        #endregion

    }
}
