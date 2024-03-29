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
        /// 结果类型。成功、具体的失败类型
        /// </summary>
        public SbtUser_Login_LoginResult_Type ResultType
        {
            get { return _resultType; }
            set { _resultType = value; }
        }

        private SbtUser_Login_LoginResult_LockOperation _lockOperation = SbtUser_Login_LoginResult_LockOperation.None;
        /// <summary>
        /// 锁定/解锁操作
        /// </summary>
        public SbtUser_Login_LoginResult_LockOperation LockOperation
        {
            get { return _lockOperation; }
            set { _lockOperation = value; }
        }

        private TbUser _userRec = null;
        /// <summary>
        /// 用户记录
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
                        return "帐号被锁定";
                    case SbtUser_Login_LoginResult_Type.PasswdError:
                        return "密码错";
                    case SbtUser_Login_LoginResult_Type.UserDisable:
                        return "帐号禁用";
                    case SbtUser_Login_LoginResult_Type.UserNotExists:
                        return "用户不存在";
                    default:
                        return "登录不成功";
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

            //========== 1. 得到所有心跳时间早于1分钟前，且未退出的记录 =============
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

                    //============ 2. 更新状态至“已退出” =================
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
        /// 表结构更新补丁
        /// </summary>
        private static void TableUpdatePatch()
        {

            string sSQL = "show columns from sbt_log_user_login like 'last_heartbeat_time'";

            //============= 1.检查表结构是否更新 =================

            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

            int nRecordCount = ds.Tables[0].Rows.Count;

            if (nRecordCount > 0)
            {
                return;
            }

            //============= 2.删除现有表结构 =====================
            sSQL = "drop table sbt_log_user_login";

            DataHelper.Instance.ExecuteNonQuery(sSQL);

            //============= 3.创建新的表结构 =====================

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
    /// 封装用户
    /// </summary>
    public class SbtUser : System.Security.Principal.IIdentity
    {
        /// <summary>
        /// 用户的数据实体
        /// </summary>
        TbUser _tbUser = new TbUser();

        public TbUser RecordData
        {
            get
            {
                return _tbUser;
            }
        }

        #region 获取和检查

        public static bool CheckUserLogin(string sUserName, string sPassword)
        {
            string sLoginErrorMsg = "";
            return CheckUserLogin(sUserName, sPassword, out sLoginErrorMsg);
        }

        /// <summary>
        /// 判断用户登录信息是否正确
        /// </summary>
        /// <param name="sUserName">用户名</param>
        /// <param name="sPassword">密码</param>
        /// <returns>是否正确</returns>
        public static bool CheckUserLogin(string sUserName, string sPassword, out string sLoginErrorMsg)
        {
            SbtUser_CheckHeartBit.DoCheckHeartBeat();

            //============= 1. 检查用户名和密码 ==========
            SbtUser_Login_LoginResult loginResult = CheckUserLogin_LoginCheck(sUserName, sPassword, out sLoginErrorMsg);

            //========== 2. 记录日志 ===================
            TbLogUserLogin tblLogLogin = new TbLogUserLogin();
            tblLogLogin.LoginLogUid = Guid.NewGuid().ToString();

            //========= 2.1 应用名 ==========
            tblLogLogin.ApplicationName = "";

            //======== 2.2 用户标识、用户名 ============
            if (loginResult.UserRec != null)
                tblLogLogin.UserUid = loginResult.UserRec.UserUid;
            tblLogLogin.UserName = sUserName;

            //=========== 2.3 登录时间、登录IP =============
            tblLogLogin.LoginTime = DateTimeUtil.Now;
            if (HttpContext.Current.Request != null)
            {
                tblLogLogin.LoginIpAddress = HttpContext.Current.Request.UserHostAddress;
            }

            //========== 2.4 登录是否成功，及失败原因 ===========
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

            //============ 2.5 用户帐号锁定操作 ===========
            if (loginResult.LockOperation == SbtUser_Login_LoginResult_LockOperation.None)
                tblLogLogin.LockOperation = "";
            else
                tblLogLogin.LockOperation = ConvertUtil.EnumToString(loginResult.LockOperation);

            //========== 2.6 心跳 =========
            tblLogLogin.LastHeartbeatTime = "";

            //========== 2.7 退出 =========
            if (loginResult.ResultType == SbtUser_Login_LoginResult_Type.Success)
                tblLogLogin.HasLogout = "N";
            else
                tblLogLogin.HasLogout = "Y";
            tblLogLogin.LogoutTime = "";
            tblLogLogin.LogoutCause = "";
            tblLogLogin.InSystemDuration = 0;


            //========= 2.8 其它 ===========
            tblLogLogin.Remarks = "";

            //=========== 2.9 记录日志 =======
            tblLogLogin.Insert();
            SbtAppContext.CurrentUserLogLoginRec = tblLogLogin;

            //=========== x. 返回 =============
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
                sLoginErrorMsg = "用户名不存在，请重新输入。";
                return loginResult;
            }

            loginResult.UserRec = tbl;

            if (tbl.IsActive == "N")
            {
                loginResult.ResultType = SbtUser_Login_LoginResult_Type.UserDisable;
                sLoginErrorMsg = "登录帐号已被禁用，请与系统管理员联系";
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
                    sLoginErrorMsg = "您输入的密码错误，请重新输入。";
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
        /// 登录密码检查
        /// </summary>
        /// <param name="sDBPassword">数据库保存密码</param>
        /// <param name="sInputPassword">用户输入密码</param>
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
        /// 通过用户名取得用户信息
        /// </summary>
        /// <param name="sUserName">用户名</param>
        /// <remarks>用户名在整个系统中是唯一的</remarks>
        public void FetchByUserName(string sUserName)
        {
            _tbUser.FetchByUserName(sUserName);
        }
        #endregion 获取和检查

        #region 基本实体信息
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
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get
            {
                return _tbUser.UserName;
            }
        }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string RealName
        {
            get
            {
                return _tbUser.RealName;
            }
        }

        /// <summary>
        /// 部门标识
        /// </summary>
        public string DeptId
        {
            get
            {
                return _tbUser.DeptId;
            }
        }

        /// <summary>
        /// 是否超级管理员
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
        /// borrower标识
        /// </summary>
        public string ThirdPartyCode
        {
            get
            {
                return _tbUser.ThirdPartyCode;
            }
        }
        #endregion 基本实体信息

        #region 完成接口
        /// <summary>
        /// 用户姓名，完成System.Security.Principal.IIdentity接口
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
        /// 鉴权类型，完成System.Security.Principal.IIdentity接口
        /// </summary>
        public string AuthenticationType
        {
            get { return ""; }
        }

        /// <summary>
        /// 是否通过鉴权，完成System.Security.Principal.IIdentity接口
        /// </summary>
        public bool IsAuthenticated
        {
            get { return true; }
        }
        #endregion 完成接口

        #region 扩展信息
        private string _departName = "<unk>";
        /// <summary>
        /// 部门名称
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
        #endregion 扩展信息

        #region 关联支持
        /// <summary>
        /// 用户主菜单的根节点
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
        #endregion 关联支持

        #region 偏好
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
        #endregion 偏好

        #region 权限
        private string _sRoleUid = "[UNSET]";
        /// <summary>
        /// 是否拥有权限
        /// </summary>
        /// <param name="sPopedomCode">权限码</param>
        /// <returns>是否有权限</returns>
        public bool HasPopedom(string sPopedomCode)
        {
            if (_tbUser.IsAdmin == "Y")
                return true;

            //======== 1. 得到用户的角色 ===========
            string sRoleUid = _sRoleUid;
            if (sRoleUid == "[UNSET]")
            {
                sRoleUid = SbtRoleUserUtil.GetRoleUidOfUser(_tbUser.UserUid);
                _sRoleUid = sRoleUid;
            }

            //========== 2. 判断是否有权限 ===========
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
        #endregion 权限
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

        #region IPrincipal 成员

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
