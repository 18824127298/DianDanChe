using System;
using System.Collections.Generic;
using System.Text;

using System.Web;

using Sigbit.Common;
using Sigbit.Web;
using Sigbit.Framework.SubSystem.DBDefine;

namespace Sigbit.Framework.SubSystem
{
    public class SUSSubSystem
    {
        private static CodeTable _codeTableOfSubSystem = null;
        /// <summary>
        /// 子系统表
        /// </summary>
        public static CodeTable CodeTableOfSubSystem
        {
            get
            {
                if (_codeTableOfSubSystem == null)
                {
                    _codeTableOfSubSystem = new CodeTable();
                    try
                    {
                        string sSQL = "select sub_system_id, sub_system_name "
                                + " from sbt_sys_sub_system_define "
                                + " order by display_order";
                        _codeTableOfSubSystem.FillBySQL(sSQL);
                    }
                    catch
                    {
                    }

                    _codeTableOfSubSystem.AddItem("main_menu", "[默认]");
                    _codeTableOfSubSystem.DefaultCode = "main_menu";
                }

                return _codeTableOfSubSystem;
            }
        }

        public static string CurrentSubSystemID__ForMenuEdit
        {
            get
            {
                string sSubSystem = ConvertUtil.ToString(HttpContext.Current.Session["SubSystemID__ForMenuEditBJEBRR"]);

                if (sSubSystem == "")
                    sSubSystem = "main_menu";

                return sSubSystem;
            }
            set
            {
                HttpContext.Current.Session["SubSystemID__ForMenuEditBJEBRR"] = value;
            }
        }

        private static QDBPoolSubSystem _poolSubSystem = null;
        /// <summary>
        /// 子系统的快缓池
        /// </summary>
        public static QDBPoolSubSystem PoolSubSystem
        {
            get
            {
                if (_poolSubSystem == null)
                    _poolSubSystem = new QDBPoolSubSystem();
                return _poolSubSystem;
            }
        }

        public static void ResetSubSystem()
        {
            _poolSubSystem = null;
            _codeTableOfSubSystem = null;
        }
    }
}
