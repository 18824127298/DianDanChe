using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework.SubSystem
{
    /// <summary>
    /// 将某个菜单项从根开始移至新的子系统
    /// </summary>
    public class SUSMenuChangeSubSystemUtil
    {
        public static void DoChange(string sMenuCode, string sNewSubSystemID)
        {
            //========== 1. 得到菜单项的根菜单 ============
            string sRootMenuCode = RootOfMenuCode(sMenuCode);

            //======== 2. 得到根菜单的所有子菜单 ==============
            string sSQL = "select * from sbt_sys_menu where menu_code like " + StringUtil.QuotedToDBStr(sRootMenuCode + "%");
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TbSysMenu tblMenu = new TbSysMenu();
                tblMenu.AssignByDataRow(ds, i);

                //========== 3. 更新菜单至新的子系统 ==========
                tblMenu.MenuClass = sNewSubSystemID;
                tblMenu.Update();
            }
        }

        private static string RootOfMenuCode(string sMenuCode)
        {
            int nIndex = sMenuCode.IndexOf("_");
            if (nIndex == -1)
                return sMenuCode;
            else
                return sMenuCode.Substring(0, nIndex);
        }
    }

}
