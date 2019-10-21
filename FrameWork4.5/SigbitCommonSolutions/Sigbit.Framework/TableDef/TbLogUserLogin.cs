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
    /// ϵͳ��¼��־��(sbt_log_user_login)�ı�����û���
    /// </summary>
    public class TbLogUserLogin : TbLogUserLoginBase
    {
        #region �û��ɱ༭����

        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)
        /// 

        #endregion
    }


    /// <summary>
    /// ϵͳ��¼��־��(sbt_log_user_login)���ֶ�����
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
    /// ϵͳ��¼��־��(sbt_log_user_login)�ı��������
    /// </summary>
    public class TbLogUserLoginBase : Sigbit.Data.TableBase
    {
        #region ˽�б�������
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
        /// ���캯��
        /// </summary>
        public TbLogUserLoginBase()
        {
            ResetData();
        }

        #region ���Զ���
        /// <summary>
        /// ��¼��־��ʶ������
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
        /// Ӧ�ó�����
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
        /// �û�ID
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
        /// �û���
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
        /// ��¼ʱ��
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
        /// IP��ַ
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
        /// �Ƿ��¼�ɹ�
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
        /// ��¼ʧ�ܵ�����
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
        /// ��¼ʧ�ܵ�����
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
        /// �û��ʺ���������
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
        /// ���һ��������ʱ��
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
        /// �Ƿ����˳�
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
        /// �˳�ʱ��
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
        /// �˳�ԭ��
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
        /// ʹ��ϵͳ��ʱ�䣨�룩
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
        /// ��ע
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

        #region ���������㼰���
        /// <summary>
        /// �õ����ݵ�HTML��ʾ�ı�
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
        /// ���㱾��¼������
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

        #region ��������ɾ�Ĳ����
        /// <summary>
        /// ��������ȡһ�����ݣ��������������⣩
        /// </summary>
        public override void Fetch()
        {
            Fetch(false);
        }

        /// <summary>
        /// ��������ȡһ������
        /// </summary>
        /// <returns>�Ƿ���ʵ�����</returns>
        public override bool Fetch(bool allowNoData)
        {
            string sSQL;
            int nRecordCount;
            DataSet dataSet;
            DataRow row;

            //======= 1. �õ�SQL��� ==============
            sSQL = "select application_name,    user_uid,            user_name,            \n";
            sSQL += "       login_time,          login_ip_address,    is_login_success,     \n";
            sSQL += "       login_fail_type,     login_fail_desc,     lock_operation,       \n";
            sSQL += "       last_heartbeat_time, has_logout,          logout_time,          \n";
            sSQL += "       logout_cause,        in_system_duration,  remarks              \n";
            sSQL += "  from sbt_log_user_login    \n";
            sSQL += "  where login_log_uid = " + Quote(_loginLogUid) + "\n";

            //======= 2. ����SQL��� ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbLogUserLogin.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. ��ȡ��¼ ========
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
        /// ����һ������
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
        /// ������ɾ��һ������
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
        /// ����������һ������
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
        /// �������жϼ�¼�Ƿ����
        /// </summary>
        /// <returns>��¼�Ƿ����</returns>
        public override bool RecordExists()
        {
            string sSQL;
            int nRecordCount;

            //======= 1. �õ�SQL��� ==============
            sSQL = "select count(*) from sbt_log_user_login \n";
            sSQL += "  where login_log_uid = " + Quote(_loginLogUid) + "\n";

            //======= 2. ����SQL��� ========
            nRecordCount = Convert.ToInt32(DataHelper.Instance.ExecuteScalar(sSQL));
            if (nRecordCount == 1)
                return true;
            else
                return false;
        }

        #endregion

        #region ������Ϊ�����Ĳ���
        /// <summary>
        /// ������Ϊ������ȡһ�����ݣ��������������⣩
        /// </summary>
        public void FetchByE(string loginLogUid)
        {
            bool hasData;
            hasData = FetchBy(loginLogUid);
            if (!hasData)
                throw new Exception("TbLogUserLogin.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// ������Ϊ������ȡһ������
        /// </summary>
        /// <returns>�Ƿ���ʵ�����</returns>
        public bool FetchBy(string loginLogUid)
        {
            _loginLogUid = loginLogUid;

            return Fetch(true);
        }

        /// <summary>
        /// ������Ϊ������ȡ���ݣ����������ʵ��
        /// </summary>
        /// <returns>���ʵ��</returns>
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
        /// ������Ϊ����ɾ��һ������
        /// </summary>
        static public void DeleteBy(string loginLogUid)
        {
            TbLogUserLogin tbl;
            tbl = new TbLogUserLogin();

            tbl.LoginLogUid = loginLogUid;

            tbl.Delete();
        }

        #endregion

        #region �ļ��ͷ��������ص�֧�ֺ���
        /// <summary>
        /// ͨ��DataRow���и�ֵ
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
        /// ͨ��DataSet���и�ֵ
        /// </summary>
        /// <param name="dataSet">���ݼ�</param>
        /// <param name="rowNum">�к�</param>
        public override void AssignByDataRow(DataSet dataSet, int rowNum)
        {
            DataRow row;
            row = dataSet.Tables[0].Rows[rowNum];

            AssignByDataRow(row);
        }

        /// <summary>
        /// ����ǰ��¼����Ϣ���浽�ļ�
        /// </summary>
        /// </param name="fileName">���浽���ļ���</param>
        public override void DumpToFile(string fileName)
        {
            //========= 1. ���ļ� ============
            StreamWriter writer;
            string sLine;
            writer = File.CreateText(fileName);

            //========= 2. д���ļ� ============
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

            //========= 3. �ر��ļ� ============
            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// �����е����м�¼���浽�ļ�
        /// </summary>
        /// </param name="fileName">���浽���ļ���</param>
        public override void DumpAllRecordsToFile(string fileName)
        {
            string sSQL;
            int i, nCol, nRecordCount;
            DataSet dataSet;
            DataRow row;
            string sFieldValue, sLine;
            StreamWriter writer;

            //======== 1. ���ļ� ========
            writer = File.CreateText(fileName);

            //======== 2. д���һ�У������У� ========
            sLine = "login_log_uid\tapplication_name\tuser_uid\t";
            sLine += "user_name\tlogin_time\tlogin_ip_address\t";
            sLine += "is_login_success\tlogin_fail_type\tlogin_fail_desc\t";
            sLine += "lock_operation\tlast_heartbeat_time\thas_logout\t";
            sLine += "logout_time\tlogout_cause\tin_system_duration\t";
            sLine += "remarks";
            writer.WriteLine(sLine);

            //======== 3. �õ�SQL��� ========
            sSQL = "select login_log_uid,   application_name,user_uid,        \n";
            sSQL += "       user_name,       login_time,      login_ip_address,\n";
            sSQL += "       is_login_success,login_fail_type, login_fail_desc, \n";
            sSQL += "       lock_operation,  last_heartbeat_time,has_logout,      \n";
            sSQL += "       logout_time,     logout_cause,    in_system_duration,\n";
            sSQL += "       remarks         \n";
            sSQL += "  from sbt_log_user_login";

            //======== 4. ����SQL��� ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. ����ÿһ�� ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 ����ÿһ�� ========
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

                //======== 5.2 ��һ�е�ֵд���ļ� ========
                writer.WriteLine(sLine);
            }

            //======== 6. �ر��ļ� ========
            writer.Flush();
            writer.Close();
        }

        #endregion

        #region ����֧�ֺ���
        /// <summary>
        /// ��ȡһ���ֶε����ݿ�����
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
        /// ��ȡһ���ֶε�CSharp����
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
        /// ͨ��һ���ֶε�ֵ���ʵõ���һ���ֶε�ֵ
        /// </summary>
        static public string GetFieldValueBy(string fromFieldName, string fromFieldValue, string toFieldName)
        {
            string sSQL;
            int nRecordCount;
            DataSet dataSet;
            DataRow row;

            //======= 1. �õ�SQL��� ==============
            sSQL = "select " + toFieldName + " from sbt_log_user_login \n";
            sSQL += "where " + fromFieldName + " = ";
            if (CSharpTypeOfFieldName(fromFieldName) == "string")
                sSQL += "'" + fromFieldValue + "'";
            else
                sSQL += fromFieldValue;

            //======= 2. ����SQL��� ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount == 0)
                return "";

            //======= 3. ��ȡ��¼ ========
            row = dataSet.Tables[0].Rows[0];
            return row[toFieldName].ToString();
        }

        #endregion

    }
}
