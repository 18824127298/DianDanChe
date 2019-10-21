using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework
{
    public class TbLogUserLoginFLogoutCause
    {
        public const string Logout = "logout";
        public const string Exit = "exit";
        public const string WinClose = "win_close";
        public const string HeartbeatLost = "heartbeat_lost";
    }

    /// <summary>
    /// 系统登录日志表(sbt_log_user_login)的表操作用户类
    /// </summary>
    public class TbLogUserLogin : TbLogUserLoginBase
    {
        #region 用户可编辑区域

        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)
        /// 

        #endregion
    }


    /// <summary>
    /// 系统登录日志表(sbt_log_user_login)的字段名类
    /// </summary>
    public class TbLogUserLoginF
    {
        public const string TableName = "sbt_log_user_login";
        public const string LoginLogUid = "login_log_uid";
        public const string ApplicationName = "application_name";
        public const string UserUid = "user_uid";
        public const string UserName = "user_name";
        public const string LoginTime = "login_time";
        public const string LoginIpAddress = "login_ip_address";
        public const string IsLoginSuccess = "is_login_success";
        public const string LoginFailType = "login_fail_type";
        public const string LoginFailDesc = "login_fail_desc";
        public const string LockOperation = "lock_operation";
        public const string LastHeartbeatTime = "last_heartbeat_time";
        public const string HasLogout = "has_logout";
        public const string LogoutTime = "logout_time";
        public const string LogoutCause = "logout_cause";
        public const string InSystemDuration = "in_system_duration";
        public const string Remarks = "remarks";
    }


    /// <summary>
    /// 系统登录日志表(sbt_log_user_login)的表操作基类
    /// </summary>
    public class TbLogUserLoginBase : Sigbit.Data.TableBase
    {
        #region 私有变量定义
        protected string _loginLogUid = "";
        protected string _applicationName = "";
        protected string _userUid = "";
        protected string _userName = "";
        protected string _loginTime = "";
        protected string _loginIpAddress = "";
        protected string _isLoginSuccess = "";
        protected string _loginFailType = "";
        protected string _loginFailDesc = "";
        protected string _lockOperation = "";
        protected string _lastHeartbeatTime = "";
        protected string _hasLogout = "";
        protected string _logoutTime = "";
        protected string _logoutCause = "";
        protected int _inSystemDuration;
        protected string _remarks = "";
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public TbLogUserLoginBase()
        {
            ResetData();
        }

        #region 属性定义
        /// <summary>
        /// 登录日志标识，主键
        /// </summary>
        public string LoginLogUid
        {
            get
            {
                return _loginLogUid;
            }
            set
            {
                _loginLogUid = value;
            }
        }

        /// <summary>
        /// 应用程序名
        /// </summary>
        public string ApplicationName
        {
            get
            {
                return _applicationName;
            }
            set
            {
                _applicationName = value;
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
        /// 用户名
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
        /// 登录时间
        /// </summary>
        public string LoginTime
        {
            get
            {
                return _loginTime;
            }
            set
            {
                _loginTime = value;
            }
        }

        /// <summary>
        /// IP地址
        /// </summary>
        public string LoginIpAddress
        {
            get
            {
                return _loginIpAddress;
            }
            set
            {
                _loginIpAddress = value;
            }
        }

        /// <summary>
        /// 是否登录成功
        /// </summary>
        public string IsLoginSuccess
        {
            get
            {
                return _isLoginSuccess;
            }
            set
            {
                _isLoginSuccess = value;
            }
        }

        /// <summary>
        /// 登录失败的类型
        /// </summary>
        public string LoginFailType
        {
            get
            {
                return _loginFailType;
            }
            set
            {
                _loginFailType = value;
            }
        }

        /// <summary>
        /// 登录失败的描述
        /// </summary>
        public string LoginFailDesc
        {
            get
            {
                return _loginFailDesc;
            }
            set
            {
                _loginFailDesc = value;
            }
        }

        /// <summary>
        /// 用户帐号锁定操作
        /// </summary>
        public string LockOperation
        {
            get
            {
                return _lockOperation;
            }
            set
            {
                _lockOperation = value;
            }
        }

        /// <summary>
        /// 最后一次心跳的时间
        /// </summary>
        public string LastHeartbeatTime
        {
            get
            {
                return _lastHeartbeatTime;
            }
            set
            {
                _lastHeartbeatTime = value;
            }
        }

        /// <summary>
        /// 是否已退出
        /// </summary>
        public string HasLogout
        {
            get
            {
                return _hasLogout;
            }
            set
            {
                _hasLogout = value;
            }
        }

        /// <summary>
        /// 退出时间
        /// </summary>
        public string LogoutTime
        {
            get
            {
                return _logoutTime;
            }
            set
            {
                _logoutTime = value;
            }
        }

        /// <summary>
        /// 退出原因
        /// </summary>
        public string LogoutCause
        {
            get
            {
                return _logoutCause;
            }
            set
            {
                _logoutCause = value;
            }
        }

        /// <summary>
        /// 使用系统的时间（秒）
        /// </summary>
        public int InSystemDuration
        {
            get
            {
                return _inSystemDuration;
            }
            set
            {
                _inSystemDuration = value;
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
            sb.Append("LoginLogUid: " + this._loginLogUid + "<br>");
            sb.Append("ApplicationName: " + this._applicationName + "<br>");
            sb.Append("UserUid: " + this._userUid + "<br>");
            sb.Append("UserName: " + this._userName + "<br>");
            sb.Append("LoginTime: " + this._loginTime + "<br>");
            sb.Append("LoginIpAddress: " + this._loginIpAddress + "<br>");
            sb.Append("IsLoginSuccess: " + this._isLoginSuccess + "<br>");
            sb.Append("LoginFailType: " + this._loginFailType + "<br>");
            sb.Append("LoginFailDesc: " + this._loginFailDesc + "<br>");
            sb.Append("LockOperation: " + this._lockOperation + "<br>");
            sb.Append("LastHeartbeatTime: " + this._lastHeartbeatTime + "<br>");
            sb.Append("HasLogout: " + this._hasLogout + "<br>");
            sb.Append("LogoutTime: " + this._logoutTime + "<br>");
            sb.Append("LogoutCause: " + this._logoutCause + "<br>");
            sb.Append("InSystemDuration: " + this._inSystemDuration + "<br>");
            sb.Append("Remarks: " + this._remarks + "<br>");
            return sb.ToString();
        }

        /// <summary>
        /// 清零本记录的数据
        /// </summary>
        public override void ResetData()
        {
            _loginLogUid = "";
            _applicationName = "";
            _userUid = "";
            _userName = "";
            _loginTime = "";
            _loginIpAddress = "";
            _isLoginSuccess = "";
            _loginFailType = "";
            _loginFailDesc = "";
            _lockOperation = "";
            _lastHeartbeatTime = "";
            _hasLogout = "";
            _logoutTime = "";
            _logoutCause = "";
            _inSystemDuration = 0;
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
            sSQL = "select application_name,    user_uid,            user_name,            \n";
            sSQL += "       login_time,          login_ip_address,    is_login_success,     \n";
            sSQL += "       login_fail_type,     login_fail_desc,     lock_operation,       \n";
            sSQL += "       last_heartbeat_time, has_logout,          logout_time,          \n";
            sSQL += "       logout_cause,        in_system_duration,  remarks              \n";
            sSQL += "  from sbt_log_user_login    \n";
            sSQL += "  where login_log_uid = " + Quote(_loginLogUid) + "\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbLogUserLogin.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _applicationName = DbToString(row["application_name"]);
            _userUid = DbToString(row["user_uid"]);
            _userName = DbToString(row["user_name"]);
            _loginTime = DbToString(row["login_time"]);
            _loginIpAddress = DbToString(row["login_ip_address"]);
            _isLoginSuccess = DbToString(row["is_login_success"]);
            _loginFailType = DbToString(row["login_fail_type"]);
            _loginFailDesc = DbToString(row["login_fail_desc"]);
            _lockOperation = DbToString(row["lock_operation"]);
            _lastHeartbeatTime = DbToString(row["last_heartbeat_time"]);
            _hasLogout = DbToString(row["has_logout"]);
            _logoutTime = DbToString(row["logout_time"]);
            _logoutCause = DbToString(row["logout_cause"]);
            _inSystemDuration = DbToInt(row["in_system_duration"]);
            _remarks = DbToString(row["remarks"]);
            return true;
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        public override void Insert()
        {
            string sSQL;

            sSQL = "insert into sbt_log_user_login \n";
            sSQL += "( login_log_uid,    application_name, \n";
            sSQL += "  user_uid,         user_name,        \n";
            sSQL += "  login_time,       login_ip_address, \n";
            sSQL += "  is_login_success, login_fail_type,  \n";
            sSQL += "  login_fail_desc,  lock_operation,   \n";
            sSQL += "  last_heartbeat_time, has_logout,       \n";
            sSQL += "  logout_time,      logout_cause,     \n";
            sSQL += "  in_system_duration, remarks           \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_loginLogUid) + "," + Quote(_applicationName) + ",\n";
            sSQL += Quote(_userUid) + "," + Quote(_userName) + ",\n";
            sSQL += Quote(_loginTime) + "," + Quote(_loginIpAddress) + ",\n";
            sSQL += Quote(_isLoginSuccess) + "," + Quote(_loginFailType) + ",\n";
            sSQL += Quote(_loginFailDesc) + "," + Quote(_lockOperation) + ",\n";
            sSQL += Quote(_lastHeartbeatTime) + "," + Quote(_hasLogout) + ",\n";
            sSQL += Quote(_logoutTime) + "," + Quote(_logoutCause) + ",\n";
            sSQL += _inSystemDuration.ToString() + "," + Quote(_remarks) + ")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 按主键删除一条数据
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL = "delete from sbt_log_user_login \n";
            sSQL += "  where login_log_uid = " + Quote(_loginLogUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbLogUserLogin.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL = "update sbt_log_user_login set \n";
            sSQL += " login_log_uid = " + Quote(_loginLogUid) + ",\n";
            sSQL += " application_name = " + Quote(_applicationName) + ",\n";
            sSQL += " user_uid = " + Quote(_userUid) + ",\n";
            sSQL += " user_name = " + Quote(_userName) + ",\n";
            sSQL += " login_time = " + Quote(_loginTime) + ",\n";
            sSQL += " login_ip_address = " + Quote(_loginIpAddress) + ",\n";
            sSQL += " is_login_success = " + Quote(_isLoginSuccess) + ",\n";
            sSQL += " login_fail_type = " + Quote(_loginFailType) + ",\n";
            sSQL += " login_fail_desc = " + Quote(_loginFailDesc) + ",\n";
            sSQL += " lock_operation = " + Quote(_lockOperation) + ",\n";
            sSQL += " last_heartbeat_time = " + Quote(_lastHeartbeatTime) + ",\n";
            sSQL += " has_logout = " + Quote(_hasLogout) + ",\n";
            sSQL += " logout_time = " + Quote(_logoutTime) + ",\n";
            sSQL += " logout_cause = " + Quote(_logoutCause) + ",\n";
            sSQL += " in_system_duration = " + _inSystemDuration.ToString() + ",\n";
            sSQL += " remarks = " + Quote(_remarks) + "\n";
            sSQL += "  where login_log_uid = " + Quote(_loginLogUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbLogUserLogin.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from sbt_log_user_login \n";
            sSQL += "  where login_log_uid = " + Quote(_loginLogUid) + "\n";

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
        public void FetchByE(string loginLogUid)
        {
            bool hasData;
            hasData = FetchBy(loginLogUid);
            if (!hasData)
                throw new Exception("TbLogUserLogin.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string loginLogUid)
        {
            _loginLogUid = loginLogUid;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbLogUserLogin CreateBy(string loginLogUid)
        {
            TbLogUserLogin tbl;
            bool hasData;

            tbl = new TbLogUserLogin();
            hasData = tbl.FetchBy(loginLogUid);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string loginLogUid)
        {
            TbLogUserLogin tbl;
            tbl = new TbLogUserLogin();

            tbl.LoginLogUid = loginLogUid;

            tbl.Delete();
        }

        #endregion

        #region 文件和访问组件相关的支持函数
        /// <summary>
        /// 通过DataRow进行赋值
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _loginLogUid = DbToString(row["login_log_uid"]);
            _applicationName = DbToString(row["application_name"]);
            _userUid = DbToString(row["user_uid"]);
            _userName = DbToString(row["user_name"]);
            _loginTime = DbToString(row["login_time"]);
            _loginIpAddress = DbToString(row["login_ip_address"]);
            _isLoginSuccess = DbToString(row["is_login_success"]);
            _loginFailType = DbToString(row["login_fail_type"]);
            _loginFailDesc = DbToString(row["login_fail_desc"]);
            _lockOperation = DbToString(row["lock_operation"]);
            _lastHeartbeatTime = DbToString(row["last_heartbeat_time"]);
            _hasLogout = DbToString(row["has_logout"]);
            _logoutTime = DbToString(row["logout_time"]);
            _logoutCause = DbToString(row["logout_cause"]);
            _inSystemDuration = DbToInt(row["in_system_duration"]);
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
            sLine = "login_log_uid\x9" + _loginLogUid;
            writer.WriteLine(sLine);

            sLine = "application_name\x9" + _applicationName;
            writer.WriteLine(sLine);

            sLine = "user_uid\x9" + _userUid;
            writer.WriteLine(sLine);

            sLine = "user_name\x9" + _userName;
            writer.WriteLine(sLine);

            sLine = "login_time\x9" + _loginTime;
            writer.WriteLine(sLine);

            sLine = "login_ip_address\x9" + _loginIpAddress;
            writer.WriteLine(sLine);

            sLine = "is_login_success\x9" + _isLoginSuccess;
            writer.WriteLine(sLine);

            sLine = "login_fail_type\x9" + _loginFailType;
            writer.WriteLine(sLine);

            sLine = "login_fail_desc\x9" + _loginFailDesc;
            writer.WriteLine(sLine);

            sLine = "lock_operation\x9" + _lockOperation;
            writer.WriteLine(sLine);

            sLine = "last_heartbeat_time\x9" + _lastHeartbeatTime;
            writer.WriteLine(sLine);

            sLine = "has_logout\x9" + _hasLogout;
            writer.WriteLine(sLine);

            sLine = "logout_time\x9" + _logoutTime;
            writer.WriteLine(sLine);

            sLine = "logout_cause\x9" + _logoutCause;
            writer.WriteLine(sLine);

            sLine = "in_system_duration\x9" + _inSystemDuration.ToString();
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
            sLine = "login_log_uid\tapplication_name\tuser_uid\t";
            sLine += "user_name\tlogin_time\tlogin_ip_address\t";
            sLine += "is_login_success\tlogin_fail_type\tlogin_fail_desc\t";
            sLine += "lock_operation\tlast_heartbeat_time\thas_logout\t";
            sLine += "logout_time\tlogout_cause\tin_system_duration\t";
            sLine += "remarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL = "select login_log_uid,   application_name,user_uid,        \n";
            sSQL += "       user_name,       login_time,      login_ip_address,\n";
            sSQL += "       is_login_success,login_fail_type, login_fail_desc, \n";
            sSQL += "       lock_operation,  last_heartbeat_time,has_logout,      \n";
            sSQL += "       logout_time,     logout_cause,    in_system_duration,\n";
            sSQL += "       remarks         \n";
            sSQL += "  from sbt_log_user_login";

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
                case "login_log_uid":
                    return "varchar";
                case "application_name":
                    return "varchar";
                case "user_uid":
                    return "varchar";
                case "user_name":
                    return "varchar";
                case "login_time":
                    return "varchar";
                case "login_ip_address":
                    return "varchar";
                case "is_login_success":
                    return "varchar";
                case "login_fail_type":
                    return "varchar";
                case "login_fail_desc":
                    return "varchar";
                case "lock_operation":
                    return "varchar";
                case "last_heartbeat_time":
                    return "varchar";
                case "has_logout":
                    return "varchar";
                case "logout_time":
                    return "varchar";
                case "logout_cause":
                    return "varchar";
                case "in_system_duration":
                    return "int";
                case "remarks":
                    return "varchar";
                default:
                    throw new Exception("TbLogUserLoginBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 获取一个字段的CSharp类型
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "login_log_uid":
                    return "string";
                case "application_name":
                    return "string";
                case "user_uid":
                    return "string";
                case "user_name":
                    return "string";
                case "login_time":
                    return "string";
                case "login_ip_address":
                    return "string";
                case "is_login_success":
                    return "string";
                case "login_fail_type":
                    return "string";
                case "login_fail_desc":
                    return "string";
                case "lock_operation":
                    return "string";
                case "last_heartbeat_time":
                    return "string";
                case "has_logout":
                    return "string";
                case "logout_time":
                    return "string";
                case "logout_cause":
                    return "string";
                case "in_system_duration":
                    return "int";
                case "remarks":
                    return "string";
                default:
                    throw new Exception("TbLogUserLoginBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL = "select " + toFieldName + " from sbt_log_user_login \n";
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
