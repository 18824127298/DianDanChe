using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using Sigbit.Common;
using Sigbit.Data;
using Sigbit.Framework;

namespace Sigbit.Framework.SupervisorTool.MenuManage
{
    /// <summary>
    /// 批量调整菜单的menu_code，验证调整后父菜单是否存在，本菜单是否重复，同时批量更新相关的子菜单。
    /// </summary>
    public class MBZMenuCodeBatchChange
    {
        private string _oldMenuCode = "";
        /// <summary>
        /// 原来的菜单编码
        /// </summary>
        public string OldMenuCode
        {
            get { return _oldMenuCode; }
            set { _oldMenuCode = value; }
        }

        private string _newMenuCode = "";
        /// <summary>
        /// 新的菜单编码
        /// </summary>
        public string NewMenuCode
        {
            get { return _newMenuCode; }
            set { _newMenuCode = value; }
        }

        public bool DoBatchChange(out string sErrorMsg)
        {
            //============ 1. 获取原来的菜单 =========
            TbSysMenu tblOldMenu = new TbSysMenu();
            tblOldMenu.MenuCode = this.OldMenuCode;
            if (!tblOldMenu.Fetch(true))
            {
                sErrorMsg = "未定位到待调整的菜单编码 - " + this.OldMenuCode;
                return false;
            }

            //======== 2. 判断新的菜单是否和已有的重复 ===========
            TbSysMenu tblNewMenu = new TbSysMenu();
            tblNewMenu.MenuCode = this.NewMenuCode;
            if (tblNewMenu.Fetch(true))
            {
                sErrorMsg = "新的菜单编码已经存在 - " + this.NewMenuCode;
                return false;
            }

            //=========== 3. 得到新菜单的父菜单 =============
            string sParentOfNewCode = ParentOfMenuCode(this.NewMenuCode);
            if (sParentOfNewCode != "")
            {
                TbSysMenu tblNewParent = new TbSysMenu();
                tblNewParent.MenuCode = sParentOfNewCode;
                if (!tblNewParent.Fetch(true))
                {
                    sErrorMsg = "新的菜单编码的父菜单并不存在 - " + this.NewMenuCode;
                    return false;
                }
            }

            //========== 4. 得到原有菜单的所有子菜单 =============
            string sSQLOldChild = "select * from sbt_sys_menu where menu_code like " + StringUtil.QuotedToDBStr(this.OldMenuCode + "%");
            DataSet dsOldChild = DataHelper.Instance.ExecuteDataSet(sSQLOldChild);

            for (int i = 0; i < dsOldChild.Tables[0].Rows.Count; i++)
            {
                TbSysMenu tblOldChild = new TbSysMenu();
                tblOldChild.AssignByDataRow(dsOldChild, i);

                ChangeToNewMenuCode(tblOldChild);
            }

            sErrorMsg = "";
            return true;
        }

        private string ParentOfMenuCode(string sMenuCode)
        {
            int nIndex = sMenuCode.LastIndexOf("_");
            if (nIndex == -1)
                return "";
            else
                return sMenuCode.Substring(0, nIndex);
        }

        private void ChangeToNewMenuCode(TbSysMenu tblOldChild)
        {
            string sNewChildCode = this.NewMenuCode + tblOldChild.MenuCode.Substring(this.OldMenuCode.Length);

            tblOldChild.Delete();

            tblOldChild.MenuCode = sNewChildCode;
            tblOldChild.ParentMenuCode = ParentOfMenuCode(sNewChildCode);
            tblOldChild.LevelNum = StringUtil.Occurs("_", tblOldChild.MenuCode) + 1;
            tblOldChild.Insert();
        }
    }
}
