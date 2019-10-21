using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework
{
    /// <summary>
    /// 系统操作审计日志表(sbt_log_operation_audit)的表操作用户类
    /// </summary>
    public class TbLogOperationAudit : TbLogOperationAuditBase
    {
        #region 用户可编辑区域

        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }


    /// <summary>
    /// 系统操作审计日志表(sbt_log_operation_audit)的字段名类
    /// </summary>
    public class TbLogOperationAuditF
    {
        public const string TableName = "sbt_log_operation_audit";
        public const string LogUid = "log_uid";
        public const string UserUid = "user_uid";
        public const string UserName = "user_name";
        public const string ProcTime = "proc_time";
        public const string ProcClassId = "proc_class_id";
        public const string ProcClassName = "proc_class_name";
        public const string ProcSubclassId = "proc_subclass_id";
        public const string ProcSubclassName = "proc_subclass_name";
        public const string ActionCode = "action_code";
        public const string ActionName = "action_name";
        public const string ActionDescription = "action_description";
        public const string IpAddress = "ip_address";
        public const string ProcessData = "process_data";
        public const string SqlStatement = "sql_statement";
        public const string ModifyTime = "modify_time";
        public const string Remarks = "remarks";
    }


    /// <summary>
    /// 系统操作审计日志表(sbt_log_operation_audit)的表操作基类
    /// </summary>
    public class TbLogOperationAuditBase : Sigbit.Data.TableBase
    {
        #region 私有变量定义
        protected string _logUid = "";
        protected string _userUid = "";
        protected string _userName = "";
        protected string _procTime = "";
        protected string _procClassId = "";
        protected string _procClassName = "";
        protected string _procSubclassId = "";
        protected string _procSubclassName = "";
        protected string _actionCode = "";
        protected string _actionName = "";
        protected string _actionDescription = "";
        protected string _ipAddress = "";
        protected string _processData = "";
        protected string _sqlStatement = "";
        protected string _modifyTime = "";
        protected string _remarks = "";
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public TbLogOperationAuditBase()
        {
            ResetData();
        }

        #region 属性定义
        /// <summary>
        /// 日志标识，主键
        /// </summary>
        public string LogUid
        {
            get
            {
                return _logUid;
            }
            set
            {
                _logUid = value;
            }
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserUid
        {
            get
            {
                return _userUid;
            }
            set
            {
                _userUid = value;
            }
        }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        /// <summary>
        /// 处理时间
        /// </summary>
        public string ProcTime
        {
            get
            {
                return _procTime;
            }
            set
            {
                _procTime = value;
            }
        }

        /// <summary>
        /// 处理分类标识
        /// </summary>
        public string ProcClassId
        {
            get
            {
                return _procClassId;
            }
            set
            {
                _procClassId = value;
            }
        }

        /// <summary>
        /// 处理分类名称
        /// </summary>
        public string ProcClassName
        {
            get
            {
                return _procClassName;
            }
            set
            {
                _procClassName = value;
            }
        }

        /// <summary>
        /// 处理子分类标识
        /// </summary>
        public string ProcSubclassId
        {
            get
            {
                return _procSubclassId;
            }
            set
            {
                _procSubclassId = value;
            }
        }

        /// <summary>
        /// 处理子分类名称
        /// </summary>
        public string ProcSubclassName
        {
            get
            {
                return _procSubclassName;
            }
            set
            {
                _procSubclassName = value;
            }
        }

        /// <summary>
        /// 动作代码
        /// </summary>
        public string ActionCode
        {
            get
            {
                return _actionCode;
            }
            set
            {
                _actionCode = value;
            }
        }

        /// <summary>
        /// 动作名称
        /// </summary>
        public string ActionName
        {
            get
            {
                return _actionName;
            }
            set
            {
                _actionName = value;
            }
        }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string ActionDescription
        {
            get
            {
                return _actionDescription;
            }
            set
            {
                _actionDescription = value;
            }
        }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress
        {
            get
            {
                return _ipAddress;
            }
            set
            {
                _ipAddress = value;
            }
        }

        /// <summary>
        /// 操作数据
        /// </summary>
        public string ProcessData
        {
            get
            {
                return _processData;
            }
            set
            {
                _processData = value;
            }
        }

        /// <summary>
        /// 操作SQL
        /// </summary>
        public string SqlStatement
        {
            get
            {
                return _sqlStatement;
            }
            set
            {
                _sqlStatement = value;
            }
        }

        /// <summary>
        /// 该条记录的修改时间
        /// </summary>
        public string ModifyTime
        {
            get
            {
                return _modifyTime;
            }
            set
            {
                _modifyTime = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            get
            {
                return _remarks;
            }
            set
            {
                _remarks = value;
            }
        }
        #endregion

        #region 变量的清零及输出
        /// <summary>
        /// 得到数据的HTML显示文本
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("LogUid: " + this._logUid + "<br>");
            sb.Append("UserUid: " + this._userUid + "<br>");
            sb.Append("UserName: " + this._userName + "<br>");
            sb.Append("ProcTime: " + this._procTime + "<br>");
            sb.Append("ProcClassId: " + this._procClassId + "<br>");
            sb.Append("ProcClassName: " + this._procClassName + "<br>");
            sb.Append("ProcSubclassId: " + this._procSubclassId + "<br>");
            sb.Append("ProcSubclassName: " + this._procSubclassName + "<br>");
            sb.Append("ActionCode: " + this._actionCode + "<br>");
            sb.Append("ActionName: " + this._actionName + "<br>");
            sb.Append("ActionDescription: " + this._actionDescription + "<br>");
            sb.Append("IpAddress: " + this._ipAddress + "<br>");
            sb.Append("ProcessData: " + this._processData + "<br>");
            sb.Append("SqlStatement: " + this._sqlStatement + "<br>");
            sb.Append("ModifyTime: " + this._modifyTime + "<br>");
            sb.Append("Remarks: " + this._remarks + "<br>");
            return sb.ToString();
        }

        /// <summary>
        /// 清零本记录的数据
        /// </summary>
        public override void ResetData()
        {
            _logUid = "";
            _userUid = "";
            _userName = "";
            _procTime = "";
            _procClassId = "";
            _procClassName = "";
            _procSubclassId = "";
            _procSubclassName = "";
            _actionCode = "";
            _actionName = "";
            _actionDescription = "";
            _ipAddress = "";
            _processData = "";
            _sqlStatement = "";
            _modifyTime = "";
            _remarks = "";
        }

        #endregion

        #region 基本的增删改查操作
        /// <summary>
        /// 按主键获取一条数据（如无数据抛例外）
        /// </summary>
        public override void Fetch()
        {
            Fetch(false);
        }

        /// <summary>
        /// 按主键获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public override bool Fetch(bool allowNoData)
        {
            string sSQL;
            int nRecordCount;
            DataSet dataSet;
            DataRow row;

            //======= 1. 得到SQL语句 ==============
            sSQL = "select user_uid,            user_name,           proc_time,            \n";
            sSQL += "       proc_class_id,       proc_class_name,     proc_subclass_id,     \n";
            sSQL += "       proc_subclass_name,  action_code,         action_name,          \n";
            sSQL += "       action_description,  ip_address,          process_data,         \n";
            sSQL += "       sql_statement,       modify_time,         remarks              \n";
            sSQL += "  from sbt_log_operation_audit    \n";
            sSQL += "  where log_uid = " + Quote(_logUid) + "\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbLogOperationAudit.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _userUid = DbToString(row["user_uid"]);
            _userName = DbToString(row["user_name"]);
            _procTime = DbToString(row["proc_time"]);
            _procClassId = DbToString(row["proc_class_id"]);
            _procClassName = DbToString(row["proc_class_name"]);
            _procSubclassId = DbToString(row["proc_subclass_id"]);
            _procSubclassName = DbToString(row["proc_subclass_name"]);
            _actionCode = DbToString(row["action_code"]);
            _actionName = DbToString(row["action_name"]);
            _actionDescription = DbToString(row["action_description"]);
            _ipAddress = DbToString(row["ip_address"]);
            _processData = DbToString(row["process_data"]);
            _sqlStatement = DbToString(row["sql_statement"]);
            _modifyTime = DbToString(row["modify_time"]);
            _remarks = DbToString(row["remarks"]);
            return true;
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        public override void Insert()
        {
            string sSQL;

            sSQL = "insert into sbt_log_operation_audit \n";
            sSQL += "( log_uid,          user_uid,         \n";
            sSQL += "  user_name,        proc_time,        \n";
            sSQL += "  proc_class_id,    proc_class_name,  \n";
            sSQL += "  proc_subclass_id, proc_subclass_name, \n";
            sSQL += "  action_code,      action_name,      \n";
            sSQL += "  action_description, ip_address,       \n";
            sSQL += "  process_data,     sql_statement,    \n";
            sSQL += "  modify_time,      remarks           \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_logUid) + "," + Quote(_userUid) + ",\n";
            sSQL += Quote(_userName) + "," + Quote(_procTime) + ",\n";
            sSQL += Quote(_procClassId) + "," + Quote(_procClassName) + ",\n";
            sSQL += Quote(_procSubclassId) + "," + Quote(_procSubclassName) + ",\n";
            sSQL += Quote(_actionCode) + "," + Quote(_actionName) + ",\n";
            sSQL += Quote(_actionDescription) + "," + Quote(_ipAddress) + ",\n";
            sSQL += Quote(_processData) + "," + Quote(_sqlStatement) + ",\n";
            sSQL += Quote(_modifyTime) + "," + Quote(_remarks) + ")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 按主键删除一条数据
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL = "delete from sbt_log_operation_audit \n";
            sSQL += "  where log_uid = " + Quote(_logUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbLogOperationAudit.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL = "update sbt_log_operation_audit set \n";
            sSQL += " log_uid = " + Quote(_logUid) + ",\n";
            sSQL += " user_uid = " + Quote(_userUid) + ",\n";
            sSQL += " user_name = " + Quote(_userName) + ",\n";
            sSQL += " proc_time = " + Quote(_procTime) + ",\n";
            sSQL += " proc_class_id = " + Quote(_procClassId) + ",\n";
            sSQL += " proc_class_name = " + Quote(_procClassName) + ",\n";
            sSQL += " proc_subclass_id = " + Quote(_procSubclassId) + ",\n";
            sSQL += " proc_subclass_name = " + Quote(_procSubclassName) + ",\n";
            sSQL += " action_code = " + Quote(_actionCode) + ",\n";
            sSQL += " action_name = " + Quote(_actionName) + ",\n";
            sSQL += " action_description = " + Quote(_actionDescription) + ",\n";
            sSQL += " ip_address = " + Quote(_ipAddress) + ",\n";
            sSQL += " process_data = " + Quote(_processData) + ",\n";
            sSQL += " sql_statement = " + Quote(_sqlStatement) + ",\n";
            sSQL += " modify_time = " + Quote(_modifyTime) + ",\n";
            sSQL += " remarks = " + Quote(_remarks) + "\n";
            sSQL += "  where log_uid = " + Quote(_logUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbLogOperationAudit.Update() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键判断记录是否存在
        /// </summary>
        /// <returns>记录是否存在</returns>
        public override bool RecordExists()
        {
            string sSQL;
            int nRecordCount;

            //======= 1. 得到SQL语句 ==============
            sSQL = "select count(*) from sbt_log_operation_audit \n";
            sSQL += "  where log_uid = " + Quote(_logUid) + "\n";

            //======= 2. 运行SQL语句 ========
            nRecordCount = Convert.ToInt32(DataHelper.Instance.ExecuteScalar(sSQL));
            if (nRecordCount == 1)
                return true;
            else
                return false;
        }

        #endregion

        #region 以主键为参数的操作
        /// <summary>
        /// 以主键为参数获取一条数据（如无数据抛例外）
        /// </summary>
        public void FetchByE(string logUid)
        {
            bool hasData;
            hasData = FetchBy(logUid);
            if (!hasData)
                throw new Exception("TbLogOperationAudit.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string logUid)
        {
            _logUid = logUid;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbLogOperationAudit CreateBy(string logUid)
        {
            TbLogOperationAudit tbl;
            bool hasData;

            tbl = new TbLogOperationAudit();
            hasData = tbl.FetchBy(logUid);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string logUid)
        {
            TbLogOperationAudit tbl;
            tbl = new TbLogOperationAudit();

            tbl.LogUid = logUid;

            tbl.Delete();
        }

        #endregion

        #region 文件和访问组件相关的支持函数
        /// <summary>
        /// 通过DataRow进行赋值
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _logUid = DbToString(row["log_uid"]);
            _userUid = DbToString(row["user_uid"]);
            _userName = DbToString(row["user_name"]);
            _procTime = DbToString(row["proc_time"]);
            _procClassId = DbToString(row["proc_class_id"]);
            _procClassName = DbToString(row["proc_class_name"]);
            _procSubclassId = DbToString(row["proc_subclass_id"]);
            _procSubclassName = DbToString(row["proc_subclass_name"]);
            _actionCode = DbToString(row["action_code"]);
            _actionName = DbToString(row["action_name"]);
            _actionDescription = DbToString(row["action_description"]);
            _ipAddress = DbToString(row["ip_address"]);
            _processData = DbToString(row["process_data"]);
            _sqlStatement = DbToString(row["sql_statement"]);
            _modifyTime = DbToString(row["modify_time"]);
            _remarks = DbToString(row["remarks"]);
        }

        /// <summary>
        /// 通过DataSet进行赋值
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <param name="rowNum">行号</param>
        public override void AssignByDataRow(DataSet dataSet, int rowNum)
        {
            DataRow row;
            row = dataSet.Tables[0].Rows[rowNum];

            AssignByDataRow(row);
        }

        /// <summary>
        /// 将当前记录的信息保存到文件
        /// </summary>
        /// </param name="fileName">保存到的文件名</param>
        public override void DumpToFile(string fileName)
        {
            //========= 1. 打开文件 ============
            StreamWriter writer;
            string sLine;
            writer = File.CreateText(fileName);

            //========= 2. 写入文件 ============
            sLine = "log_uid\x9" + _logUid;
            writer.WriteLine(sLine);

            sLine = "user_uid\x9" + _userUid;
            writer.WriteLine(sLine);

            sLine = "user_name\x9" + _userName;
            writer.WriteLine(sLine);

            sLine = "proc_time\x9" + _procTime;
            writer.WriteLine(sLine);

            sLine = "proc_class_id\x9" + _procClassId;
            writer.WriteLine(sLine);

            sLine = "proc_class_name\x9" + _procClassName;
            writer.WriteLine(sLine);

            sLine = "proc_subclass_id\x9" + _procSubclassId;
            writer.WriteLine(sLine);

            sLine = "proc_subclass_name\x9" + _procSubclassName;
            writer.WriteLine(sLine);

            sLine = "action_code\x9" + _actionCode;
            writer.WriteLine(sLine);

            sLine = "action_name\x9" + _actionName;
            writer.WriteLine(sLine);

            sLine = "action_description\x9" + _actionDescription;
            writer.WriteLine(sLine);

            sLine = "ip_address\x9" + _ipAddress;
            writer.WriteLine(sLine);

            sLine = "process_data\x9" + _processData;
            writer.WriteLine(sLine);

            sLine = "sql_statement\x9" + _sqlStatement;
            writer.WriteLine(sLine);

            sLine = "modify_time\x9" + _modifyTime;
            writer.WriteLine(sLine);

            sLine = "remarks\x9" + _remarks;
            writer.WriteLine(sLine);

            //========= 3. 关闭文件 ============
            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// 将表中的所有记录保存到文件
        /// </summary>
        /// </param name="fileName">保存到的文件名</param>
        public override void DumpAllRecordsToFile(string fileName)
        {
            string sSQL;
            int i, nCol, nRecordCount;
            DataSet dataSet;
            DataRow row;
            string sFieldValue, sLine;
            StreamWriter writer;

            //======== 1. 打开文件 ========
            writer = File.CreateText(fileName);

            //======== 2. 写入第一行（标题行） ========
            sLine = "log_uid\tuser_uid\tuser_name\t";
            sLine += "proc_time\tproc_class_id\tproc_class_name\t";
            sLine += "proc_subclass_id\tproc_subclass_name\taction_code\t";
            sLine += "action_name\taction_description\tip_address\t";
            sLine += "process_data\tsql_statement\tmodify_time\t";
            sLine += "remarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL = "select log_uid,         user_uid,        user_name,       \n";
            sSQL += "       proc_time,       proc_class_id,   proc_class_name, \n";
            sSQL += "       proc_subclass_id,proc_subclass_name,action_code,     \n";
            sSQL += "       action_name,     action_description,ip_address,      \n";
            sSQL += "       process_data,    sql_statement,   modify_time,     \n";
            sSQL += "       remarks         \n";
            sSQL += "  from sbt_log_operation_audit";

            //======== 4. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. 处理每一行 ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 处理每一列 ========
                for (nCol = 0; nCol < 16; nCol++)
                {
                    if (row[nCol] is DBNull)
                        sFieldValue = "";
                    else
                        sFieldValue = row[nCol].ToString();

                    if (nCol == 0)
                        sLine += sFieldValue;
                    else
                        sLine += "\t" + sFieldValue;
                }

                //======== 5.2 将一行的值写入文件 ========
                writer.WriteLine(sLine);
            }

            //======== 6. 关闭文件 ========
            writer.Flush();
            writer.Close();
        }

        #endregion

        #region 辅助支持函数
        /// <summary>
        /// 获取一个字段的数据库类型
        /// </summary>
        static public string DBTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "log_uid":
                    return "varchar";
                case "user_uid":
                    return "varchar";
                case "user_name":
                    return "varchar";
                case "proc_time":
                    return "varchar";
                case "proc_class_id":
                    return "varchar";
                case "proc_class_name":
                    return "varchar";
                case "proc_subclass_id":
                    return "varchar";
                case "proc_subclass_name":
                    return "varchar";
                case "action_code":
                    return "varchar";
                case "action_name":
                    return "varchar";
                case "action_description":
                    return "text";
                case "ip_address":
                    return "varchar";
                case "process_data":
                    return "text";
                case "sql_statement":
                    return "text";
                case "modify_time":
                    return "varchar";
                case "remarks":
                    return "varchar";
                default:
                    throw new Exception("TbLogOperationAuditBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 获取一个字段的CSharp类型
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "log_uid":
                    return "string";
                case "user_uid":
                    return "string";
                case "user_name":
                    return "string";
                case "proc_time":
                    return "string";
                case "proc_class_id":
                    return "string";
                case "proc_class_name":
                    return "string";
                case "proc_subclass_id":
                    return "string";
                case "proc_subclass_name":
                    return "string";
                case "action_code":
                    return "string";
                case "action_name":
                    return "string";
                case "action_description":
                    return "string";
                case "ip_address":
                    return "string";
                case "process_data":
                    return "string";
                case "sql_statement":
                    return "string";
                case "modify_time":
                    return "string";
                case "remarks":
                    return "string";
                default:
                    throw new Exception("TbLogOperationAuditBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 通过一个字段的值访问得到另一个字段的值
        /// </summary>
        static public string GetFieldValueBy(string fromFieldName, string fromFieldValue, string toFieldName)
        {
            string sSQL;
            int nRecordCount;
            DataSet dataSet;
            DataRow row;

            //======= 1. 得到SQL语句 ==============
            sSQL = "select " + toFieldName + " from sbt_log_operation_audit \n";
            sSQL += "where " + fromFieldName + " = ";
            if (CSharpTypeOfFieldName(fromFieldName) == "string")
                sSQL += "'" + fromFieldValue + "'";
            else
                sSQL += fromFieldValue;

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount == 0)
                return "";

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            return row[toFieldName].ToString();
        }

        #endregion

    }
}
