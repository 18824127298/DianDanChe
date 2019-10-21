using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;
using Sigbit.Web;

namespace Sigbit.Framework
{
    /// <summary>
    /// 框架用到的代码表
    /// </summary>
    public class SbtCodeTables
    {
        private static CodeTable _preferenceLanguage = null;
        /// <summary>
        /// 语言偏好代码表
        /// </summary>
        public static CodeTable PreferenceLanguage
        {
            get
            {
                if (_preferenceLanguage == null)
                {
                    _preferenceLanguage = new CodeTable();
                    string sSQL = "select preference_code, preference_name "
                            + " from sbt_sys_preference_setting "
                            + " where preference_class = 'language'";
                    _preferenceLanguage.FillBySQL(sSQL);
                }

                return _preferenceLanguage;
            }
        }

        private static CodeTable _preferenceTheme = null;
        /// <summary>
        /// 主题偏好代码表
        /// </summary>
        public static CodeTable PreferenceTheme
        {
            get
            {
                if (_preferenceTheme == null)
                {
                    _preferenceTheme = new CodeTable();
                    string sSQL = "select preference_code, preference_name "
                            + " from sbt_sys_preference_setting "
                            + " where preference_class = 'theme'";
                    _preferenceTheme.FillBySQL(sSQL);
                }

                return _preferenceTheme;
            }
        }

        private static CodeTable _role = new CodeTable();
        public static CodeTable Role
        {
            get
            {
                if (_role.Count == 0)
                {
                    _role = new CodeTable();
                    string sSQL = "select role_uid, role_name "
                            + " from sbt_role ";
                    _role.FillBySQL(sSQL);
                }

                return _role;
            }
        }

        private static CodeTable _deptTree = new CodeTable();
        public static CodeTable DeptTree
        {
            get
            {
                if (_deptTree.Count == 0)
                {
                    TbUserDept tblRootDept = new TbUserDept();
                    tblRootDept.DeptLevel = 0;

                    ExpandDeptNode(tblRootDept);
                }
                return _deptTree;
            }
        }

        private static void ExpandDeptNode(TbUserDept tblDept)
        {
            if (tblDept.DeptLevel != 0)
            {
                string sKey = tblDept.DeptId;
                string sValue = StringUtil.RepeatChar('—', tblDept.DeptLevel - 1) + tblDept.DeptName;
                _deptTree.AddItem(sKey, sValue);
            }

            TbUserDeptList childDepts = tblDept.GetChildDepts();
            for (int i = 0; i < childDepts.Count; i++)
            {
                TbUserDept childDept = childDepts.GetDept(i);
                ExpandDeptNode(childDept);
            }
        }
    }
}
