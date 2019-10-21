using System;
using System.Collections.Generic;
using System.Text;

using System.Web;
using System.Web.SessionState;
using System.Data;

using Sigbit.Common;
using Sigbit.Common.Encrypt;
using Sigbit.Data;
using Sigbit.Framework.Role;
using Sigbit.Framework.Security;

namespace Sigbit.Framework
{
    enum SbtUser_Login_LoginResult_Type
    {
        Success,
        UserNotExists,
        PasswdError,
        AccountLocked,
        UserDisable
    }

    enum SbtUser_Login_LoginResult_LockOperation
    {
        None,
        Lock,
        Unlock
    }

    class SbtUser_Login_LoginResult
    {
        private SbtUser_Login_LoginResult_Type _resultType = SbtUser_Login_LoginResult_Type.Success;
        /// <summary>
        /// ������͡��ɹ��������ʧ������
        /// </summary>
        public SbtUser_Login_LoginResult_Type ResultType
        {
            get { return _resultType; }
            set { _resultType = value; }
        }

        private SbtUser_Login_LoginResult_LockOperation _lockOperation = SbtUser_Login_LoginResult_LockOperation.None;
        /// <summary>
        /// ����/��������
        /// </summary>
        public SbtUser_Login_LoginResult_LockOperation LockOperation
        {
            get { return _lockOperation; }
            set { _lockOperation = value; }
        }

        private TbUser _userRec = null;
        /// <summary>
        /// �û���¼
        /// </summary>
        public TbUser UserRec
        {
            get { return _userRec; }
            set { _userRec = value; }
        }

        public string LoginFailDesc
        {
            get
            {
                switch (this.ResultType)
                {
                    case SbtUser_Login_LoginResult_Type.Success:
                        return "";
                    case SbtUser_Login_LoginResult_Type.AccountLocked:
                        return "�ʺű�����";
                    case SbtUser_Login_LoginResult_Type.PasswdError:
                        return "�����";
                    case SbtUser_Login_LoginResult_Type.UserDisable:
                        return "�ʺŽ���";
                    case SbtUser_Login_LoginResult_Type.UserNotExists:
                        return "�û�������";
                    default:
                        return "��¼���ɹ�";
                }
            }
        }
    }

    class SbtUser_CheckHeartBit
    {
        private static DateTime _dtLastCheckTime = DateTime.Now.AddSeconds(-120);

        public static void DoCheckHeartBeat()
        {
            lock (typeof(SbtUser_CheckHeartBit))
            {
                if (_dtLastCheckTime > DateTime.Now.AddSeconds(-60))
                    return;
                else
                    _dtLastCheckTime = DateTime.Now;
            }

            //========== 1. �õ���������ʱ������1����ǰ����δ�˳��ļ�¼ =============
            string sTimeBeforeOneMinutes = DateTimeUtil.AddSeconds(DateTimeUtil.Now, -60);

            string sSQL = "select * from sbt_log_user_login where last_heartbeat_time <= " + StringUtil.QuotedToDBStr(sTimeBeforeOneMinutes)
                    + " and has_logout = 'N' "
                    + " and login_time <= " + StringUtil.QuotedToDBStr(sTimeBeforeOneMinutes);


            try
            {
                DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    TbLogUserLogin tblLogLogin = new TbLogUserLogin();
                    tblLogLogin.AssignByDataRow(ds, i);

                    //============ 2. ����״̬�������˳��� =================
                    string sLastHeartBeatTime = tblLogLogin.LastHeartbeatTime;
                    if (sLastHeartBeatTime == "")
                        sLastHeartBeatTime = tblLogLogin.LoginTime;

                    tblLogLogin.LogoutTime = DateTimeUtil.AddSeconds(sLastHeartBeatTime, 30);
                    tblLogLogin.LogoutCause = TbLogUserLoginFLogoutCause.HeartbeatLost;
                    tblLogLogin.HasLogout = "Y";
                    tblLogLogin.InSystemDuration = DateTimeUtil.SecondsAfter(tblLogLogin.LoginTime, tblLogLogin.LogoutTime);

                    tblLogLogin.Update();
                }

            }
            catch (Exception)
            {
                TableUpdatePatch();

                return;
            }


        }

        /// <summary>
        /// ���ṹ���²���
        /// </summary>
        private static void TableUpdatePatch()
        {

            string sSQL = "show columns from sbt_log_user_login like 'last_heartbeat_time'";

            //============= 1.�����ṹ�Ƿ���� =================

            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

            int nRecordCount = ds.Tables[0].Rows.Count;

            if (nRecordCount > 0)
            {
                return;
            }

            //============= 2.ɾ�����б��ṹ =====================
            sSQL = "drop table sbt_log_user_login";

            DataHelper.Instance.ExecuteNonQuery(sSQL);

            //============= 3.�����µı��ṹ =====================

            sSQL = @"CREATE TABLE `sbt_log_user_login` (
                  `login_log_uid` varchar(36) NOT NULL DEFAULT '',
                  `application_name` varchar(64) NOT NULL DEFAULT '',
                  `user_uid` varchar(36) NOT NULL DEFAULT '',
                  `user_name` varchar(64) NOT NULL DEFAULT '',
                  `login_time` varchar(19) NOT NULL DEFAULT '',
                  `login_ip_address` varchar(16) DEFAULT NULL,
                  `is_login_success` char(1) NOT NULL DEFAULT '',
                  `login_fail_type` varchar(16) NOT NULL DEFAULT '',
                  `login_fail_desc` varchar(255) NOT NULL DEFAULT '',
                  `lock_operation` varchar(16) NOT NULL DEFAULT '',
                  `last_heartbeat_time` varchar(19) NOT NULL DEFAULT '',
                  `has_logout` char(1) NOT NULL DEFAULT '',
                  `logout_time` varchar(19) NOT NULL DEFAULT '',
                  `logout_cause` varchar(16) NOT NULL DEFAULT '',
                  `in_system_duration` int(11) NOT NULL DEFAULT '0',
                  `remarks` varchar(255) DEFAULT NULL,
                  PRIMARY KEY (`login_log_uid`)
                ) ENGINE=MyISAM;";

            DataHelper.Instance.ExecuteNonQuery(sSQL);

            return;
        }

    }

    /// <summary>
    /// ��װ�û�
    /// </summary>
    public class SbtUser : System.Security.Principal.IIdentity
    {
        /// <summary>
        /// �û�������ʵ��
        /// </summary>
        TbUser _tbUser = new TbUser();

        public TbUser RecordData
        {
            get
            {
                return _tbUser;
            }
        }

        #region ��ȡ�ͼ��

        public static bool CheckUserLogin(string sUserName, string sPassword)
        {
            string sLoginErrorMsg = "";
            return CheckUserLogin(sUserName, sPassword, out sLoginErrorMsg);
        }

        /// <summary>
        /// �ж��û���¼��Ϣ�Ƿ���ȷ
        /// </summary>
        /// <param name="sUserName">�û���</param>
        /// <param name="sPassword">����</param>
        /// <returns>�Ƿ���ȷ</returns>
        public static bool CheckUserLogin(string sUserName, string sPassword, out string sLoginErrorMsg)
        {
            SbtUser_CheckHeartBit.DoCheckHeartBeat();

            //============= 1. ����û��������� ==========
            SbtUser_Login_LoginResult loginResult = CheckUserLogin_LoginCheck(sUserName, sPassword, out sLoginErrorMsg);

            //========== 2. ��¼��־ ===================
            TbLogUserLogin tblLogLogin = new TbLogUserLogin();
            tblLogLogin.LoginLogUid = Guid.NewGuid().ToString();

            //========= 2.1 Ӧ���� ==========
            tblLogLogin.ApplicationName = "";

            //======== 2.2 �û���ʶ���û��� ============
            if (loginResult.UserRec != null)
                tblLogLogin.UserUid = loginResult.UserRec.UserUid;
            tblLogLogin.UserName = sUserName;

            //=========== 2.3 ��¼ʱ�䡢��¼IP =============
            tblLogLogin.LoginTime = DateTimeUtil.Now;
            if (HttpContext.Current.Request != null)
            {
                tblLogLogin.LoginIpAddress = HttpContext.Current.Request.UserHostAddress;
            }

            //========== 2.4 ��¼�Ƿ�ɹ�����ʧ��ԭ�� ===========
            if (loginResult.ResultType == SbtUser_Login_LoginResult_Type.Success)
            {
                tblLogLogin.IsLoginSuccess = "Y";
                tblLogLogin.LoginFailType = "";
                tblLogLogin.LoginFailDesc = "";
            }
            else
            {
                tblLogLogin.IsLoginSuccess = "N";
                tblLogLogin.LoginFailType = ConvertUtil.EnumToString(loginResult.ResultType);
                tblLogLogin.LoginFailDesc = loginResult.LoginFailDesc;
            }

            //============ 2.5 �û��ʺ��������� ===========
            if (loginResult.LockOperation == SbtUser_Login_LoginResult_LockOperation.None)
                tblLogLogin.LockOperation = "";
            else
                tblLogLogin.LockOperation = ConvertUtil.EnumToString(loginResult.LockOperation);

            //========== 2.6 ���� =========
            tblLogLogin.LastHeartbeatTime = "";

            //========== 2.7 �˳� =========
            if (loginResult.ResultType == SbtUser_Login_LoginResult_Type.Success)
                tblLogLogin.HasLogout = "N";
            else
                tblLogLogin.HasLogout = "Y";
            tblLogLogin.LogoutTime = "";
            tblLogLogin.LogoutCause = "";
            tblLogLogin.InSystemDuration = 0;


            //========= 2.8 ���� ===========
            tblLogLogin.Remarks = "";

            //=========== 2.9 ��¼��־ =======
            tblLogLogin.Insert();
            SbtAppContext.CurrentUserLogLoginRec = tblLogLogin;

            //=========== x. ���� =============
            if (loginResult.ResultType == SbtUser_Login_LoginResult_Type.Success)
                return true;
            else
                return false;
        }

        private static SbtUser_Login_LoginResult CheckUserLogin_LoginCheck(string sUserName, string sPassword,
                out string sLoginErrorMsg)
        {
            SbtUser_Login_LoginResult loginResult = new SbtUser_Login_LoginResult();

            TbUser tbl = new TbUser();
            if (!tbl.FetchByUserName(sUserName, true))
            {
                loginResult.ResultType = SbtUser_Login_LoginResult_Type.UserNotExists;
                sLoginErrorMsg = "�û��������ڣ����������롣";
                return loginResult;
            }

            loginResult.UserRec = tbl;

            if (tbl.IsActive == "N")
            {
                loginResult.ResultType = SbtUser_Login_LoginResult_Type.UserDisable;
                sLoginErrorMsg = "��¼�ʺ��ѱ����ã�����ϵͳ����Ա��ϵ";
                return loginResult;
            }

            if (SbtSecurityConfig.LockUser.AutoLockEnabled == Bool3State.False)
            {
                if (PasswordCheck(tbl.Password, sPassword))
                {
                    loginResult.ResultType = SbtUser_Login_LoginResult_Type.Success;
                    sLoginErrorMsg = "";
                }
                else
                {
                    loginResult.ResultType = SbtUser_Login_LoginResult_Type.PasswdError;
                    sLoginErrorMsg = "�����������������������롣";
                }

                return loginResult;
            }

            NLSUserLock userLock = new NLSUserLock();
            userLock.UserUid = tbl.UserUid;
            userLock.Fetch();
            if (userLock.AccountAutoLocked(out sLoginErrorMsg))
            {
                loginResult.ResultType = SbtUser_Login_LoginResult_Type.AccountLocked;
                loginResult.LockOperation = userLock.LockOperation;
                return loginResult;
            }


            if (PasswordCheck(tbl.Password, sPassword))
            {
                loginResult.ResultType = SbtUser_Login_LoginResult_Type.Success;
                sLoginErrorMsg = "";

                userLock.LoginSuccess();
            }
            else
            {
                loginResult.ResultType = SbtUser_Login_LoginResult_Type.PasswdError;

                userLock.WrongPasswordBeenInputted(out sLoginErrorMsg);
            }

            loginResult.LockOperation = userLock.LockOperation;
            return loginResult;
        }

        /// <summary>
        /// ��¼������
        /// </summary>
        /// <param name="sDBPassword">���ݿⱣ������</param>
        /// <param name="sInputPassword">�û���������</param>
        /// <returns></returns>
        private static bool PasswordCheck(string sDBPassword, string sInputPassword)
        {

            if (sDBPassword == sInputPassword)
                return true;

            if (sDBPassword == EncryptUtil.MD5String(sInputPassword))
                return true;


            return false;
        }


        /// <summary>
        /// ͨ���û���ȡ���û���Ϣ
        /// </summary>
        /// <param name="sUserName">�û���</param>
        /// <remarks>�û���������ϵͳ����Ψһ��</remarks>
        public void FetchByUserName(string sUserName)
        {
            _tbUser.FetchByUserName(sUserName);
        }
        #endregion ��ȡ�ͼ��

        #region ����ʵ����Ϣ
        public string UserUid
        {
            get
            {
                return _tbUser.UserUid;
            }
            set
            {
                _tbUser.UserUid = value;
            }
        }

        /// <summary>
        /// �û�����
        /// </summary>
        public string UserName
        {
            get
            {
                return _tbUser.UserName;
            }
        }

        /// <summary>
        /// �û���ʵ����
        /// </summary>
        public string RealName
        {
            get
            {
                return _tbUser.RealName;
            }
        }

        /// <summary>
        /// ���ű�ʶ
        /// </summary>
        public string DeptId
        {
            get
            {
                return _tbUser.DeptId;
            }
        }

        /// <summary>
        /// �Ƿ񳬼�����Ա
        /// </summary>
        public bool IsAdmin
        {
            get
            {
                if (_tbUser.IsAdmin == "Y")
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// borrower��ʶ
        /// </summary>
        public string ThirdPartyCode
        {
            get
            {
                return _tbUser.ThirdPartyCode;
            }
        }
        #endregion ����ʵ����Ϣ

        #region ��ɽӿ�
        /// <summary>
        /// �û����������System.Security.Principal.IIdentity�ӿ�
        /// </summary>
        public string Name
        {
            get
            {
                return _tbUser.UserName;
            }
            set
            {
                _tbUser.UserName = value;
            }
        }

        /// <summary>
        /// ��Ȩ���ͣ����System.Security.Principal.IIdentity�ӿ�
        /// </summary>
        public string AuthenticationType
        {
            get { return ""; }
        }

        /// <summary>
        /// �Ƿ�ͨ����Ȩ�����System.Security.Principal.IIdentity�ӿ�
        /// </summary>
        public bool IsAuthenticated
        {
            get { return true; }
        }
        #endregion ��ɽӿ�

        #region ��չ��Ϣ
        private string _departName = "<unk>";
        /// <summary>
        /// ��������
        /// </summary>
        public string DepartName
        {
            get
            {
                if (_departName == "<unk>")
                {
                    TbUserDept tbl = new TbUserDept();
                    tbl.DeptId = DeptId;
                    tbl.Fetch();
                    _departName = tbl.DeptName;
                }
                return _departName;
            }
        }
        #endregion ��չ��Ϣ

        #region ����֧��
        /// <summary>
        /// �û����˵��ĸ��ڵ�
        /// </summary>
        public SbtMenuNode MainMenuRootNode
        {
            get
            {
                SbtMenuNode node = new SbtMenuNode();
                node.MenuCode = "";
                node.MenuLevel = 0;
                node.MenuNavigateMethod = SbtMenuNaviMeth.UserRightMain;
                //node.UserUid = UserUid;
                node.CurrentUser = this;
                node.MenuSetName = "main_menu";
                return node;
            }
        }
        #endregion ����֧��

        #region ƫ��
        public void SetLanguagePreference(string sLanguage)
        {
            SetUserPreference(this.UserUid, "language", sLanguage);
            _languagePreference = sLanguage;
        }

        public void SetThemePreference(string sTheme)
        {
            SetUserPreference(this.UserUid, "theme", sTheme);
            _themePreference = sTheme;
        }

        private void SetUserPreference(string sUserUid, string sPreferenceClass,
                string sPreferenceCode)
        {
            TbUserPreference tbl = new TbUserPreference();
            tbl.UserUid = sUserUid;
            tbl.PreferenceClass = sPreferenceClass;
            if (tbl.RecordExists())
            {
                tbl.PreferenceCode = sPreferenceCode;
                tbl.ModifyTime = DateTimeUtil.Now;
                tbl.Update();
            }
            else
            {
                tbl.PreferenceCode = sPreferenceCode;
                tbl.ModifyTime = DateTimeUtil.Now;
                tbl.Insert();
            }
        }

        private string _languagePreference = "";
        public string LanguagePreference
        {
            get
            {
                if (_languagePreference != "")
                    return _languagePreference;

                _languagePreference = GetUserPreference(this.UserUid, "language");
                return _languagePreference;
            }
        }

        private string _themePreference = "";
        public string ThemePreference
        {
            get
            {
                if (_themePreference != "")
                    return _themePreference;

                _themePreference = SbtPageBase.ForceThemeUsing;
                if (_themePreference == "")
                    _themePreference = GetUserPreference(this.UserUid, "theme");
                return _themePreference;
            }
        }

        private string GetUserPreference(string sUserUid, string sPreferenceClass)
        {
            TbUserPreference tbl = new TbUserPreference();
            tbl.UserUid = sUserUid;
            tbl.PreferenceClass = sPreferenceClass;
            if (tbl.Fetch(true))
                return tbl.PreferenceCode;
            else
                return TbSysPreferenceSetting.GetDefaultPreference(sPreferenceClass);
        }
        #endregion ƫ��

        #region Ȩ��
        private string _sRoleUid = "[UNSET]";
        /// <summary>
        /// �Ƿ�ӵ��Ȩ��
        /// </summary>
        /// <param name="sPopedomCode">Ȩ����</param>
        /// <returns>�Ƿ���Ȩ��</returns>
        public bool HasPopedom(string sPopedomCode)
        {
            if (_tbUser.IsAdmin == "Y")
                return true;

            //======== 1. �õ��û��Ľ�ɫ ===========
            string sRoleUid = _sRoleUid;
            if (sRoleUid == "[UNSET]")
            {
                sRoleUid = SbtRoleUserUtil.GetRoleUidOfUser(_tbUser.UserUid);
                _sRoleUid = sRoleUid;
            }

            //========== 2. �ж��Ƿ���Ȩ�� ===========
            return SbtRoleRight__Lib.Instance.RoleHasPopedom(sRoleUid, sPopedomCode);
        }

        public bool CanEnterSubSystem(string sSubSystemID)
        {
            if (_tbUser.IsAdmin == "Y")
                return true;

            SbtMenuNode rootMenuNode = this.MainMenuRootNode;
            rootMenuNode.MenuSetName = sSubSystemID;

            if (rootMenuNode.ChildNodes.Count == 0)
                return false;
            else
                return true;
        }
        #endregion Ȩ��
    }

    public class SigbitPrincipal : System.Security.Principal.IPrincipal
    {
        public SigbitPrincipal()
        {
        }

        private SbtUser _sigbitUser = null;

        public void LoadUser(SbtUser user)
        {
            this._sigbitUser = user;
        }

        #region IPrincipal ��Ա

        public System.Security.Principal.IIdentity Identity
        {
            get { return this._sigbitUser; }
        }

        public bool IsInRole(string role)
        {
            return true;
        }

        #endregion
    }
}