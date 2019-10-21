using System;
using System.Collections.Generic;
using System.Text;

using System.Web;

using Sigbit.Common;

namespace Sigbit.Framework
{
    public class SbtAppContextActionInfo
    {
        private string _procClassId = "";
        /// <summary>
        /// 处理分类标识
        /// </summary>
        public string ProcClassId
        {
            get { return _procClassId; }
            set { _procClassId = value; }
        }

        private string _procClassName = "";
        /// <summary>
        /// 处理分类名称
        /// </summary>
        public string ProcClassName
        {
            get { return _procClassName; }
            set { _procClassName = value; }
        }

        private string _procSubclassId = "";
        /// <summary>
        /// 处理子分类标识
        /// </summary>
        public string ProcSubclassId
        {
            get { return _procSubclassId; }
            set { _procSubclassId = value; }
        }

        private string _procSubclassName = "";
        /// <summary>
        /// 处理子分类名称
        /// </summary>
        public string ProcSubclassName
        {
            get { return _procSubclassName; }
            set { _procSubclassName = value; }
        }
    }

    public class SbtAppContext
    {
        public static SbtUser CurrentUser
        {
            get
            {
                SbtUser user = (SbtUser)HttpContext.Current.Session["currentUserMEFTMG"];
                if (user == null)
                    user = new SbtUser();

                return user;
            }
        }

        /// <summary>
        /// 当前用户登录日志的数据记录
        /// </summary>
        public static TbLogUserLogin CurrentUserLogLoginRec
        {
            get
            {
                TbLogUserLogin tblLogLogin = (TbLogUserLogin)HttpContext.Current.Session["currentLoginLogHZZZBS"];
                return tblLogLogin;
            }
            set
            {
                HttpContext.Current.Session["currentLoginLogHZZZBS"] = value;
            }
        }

        public static string CurrentSubSystem
        {
            get
            {
                string sSubSystem = ConvertUtil.ToString(HttpContext.Current.Session["currentSubSystemLIUOW"]);
                return sSubSystem;
            }
            set
            {
                HttpContext.Current.Session["currentSubSystemLIUOW"] = value;
            }
        }

        public static SbtAppContextActionInfo ActionInfo
        {
            get
            {
                SbtAppContextActionInfo actionInfo 
                        = (SbtAppContextActionInfo)HttpContext.Current.Session["actionInfoSMJQLE"];
                if (actionInfo == null)
                {
                    actionInfo = new SbtAppContextActionInfo();
                    HttpContext.Current.Session["actionInfoSMJQLE"] = actionInfo;
                }

                return actionInfo;
            }
        }

        public static void VisitMenu(string sMenuCode)
        {
            SbtAppContextActionInfo actionInfo = ActionInfo;
            actionInfo.ProcClassId = "";
            actionInfo.ProcClassName = "";
            actionInfo.ProcSubclassId = "";
            actionInfo.ProcSubclassName = "";

            //========= 1. 得到这个菜单的信息 ===========
            TbSysMenu tblMenu = TbSysMenu__Lib.Instance.GetMenuRecordByMenuCode(sMenuCode);
            if (tblMenu == null)
                return;
            actionInfo.ProcSubclassId = tblMenu.MenuCode;
            actionInfo.ProcSubclassName = tblMenu.MenuName;

            //========= 2. 得到首层菜单的信息 ==============
            int nUnderLinePos = sMenuCode.IndexOf("_");
            string sFirstLevelMenuCode = sMenuCode;
            if (nUnderLinePos != -1)
                sFirstLevelMenuCode = sMenuCode.Substring(0, nUnderLinePos);

            TbSysMenu tblFirstLevelMenu = TbSysMenu__Lib.Instance.GetMenuRecordByMenuCode(sFirstLevelMenuCode);
            if (tblFirstLevelMenu == null)
                return;

            actionInfo.ProcClassId = tblFirstLevelMenu.MenuCode;
            actionInfo.ProcClassName = tblFirstLevelMenu.MenuName;
        }
    }
}
