using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

namespace Sigbit.Framework.Role
{
    /// <summary>
    /// 更新角色的权限
    /// </summary>
    public class SbtRoleRightUpdate
    {
        /// <summary>
        /// 更新角色的权限列表
        /// </summary>
        /// <param name="sRoleUid">角色标识</param>
        /// <param name="arrPopedomList">权限列表</param>
        public void UpdateRightsOfRole(string sRoleUid, ArrayList arrPopedomList)
        {
            //========= 1. 裁减权限列表，去掉枝节点，只留叶节点 ========
            ArrayList arrLeafPopedom = new ArrayList();
            for (int i = 0; i < arrPopedomList.Count; i++)
            {
                string sPopedom = (string)arrPopedomList[i];
                if (IsLeafPopedom(sPopedom))
                    arrLeafPopedom.Add(sPopedom);
            }

            //========= 2. 回溯叶节点上级的所有节点，得到所有权限节点 ======
            Hashtable htAllPopedom = new Hashtable();
            for (int i = 0; i < arrLeafPopedom.Count; i++)
            {
                string sLeafPopedom = (string)arrLeafPopedom[i];
                if (htAllPopedom[sLeafPopedom] == null)
                    htAllPopedom.Add(sLeafPopedom, sLeafPopedom);

                ArrayList arrParents = GetAllParentsOfPopedom(sLeafPopedom);
                for (int j = 0; j < arrParents.Count; j++)
                {
                    string sParent = (string)arrParents[j];
                    if (htAllPopedom[sParent] == null)
                        htAllPopedom.Add(sParent, sParent);
                }
            }

            //=========== 3. 清空角色相关的权限 =============
            TbRoleRight.ClearAllRightsOfRole(sRoleUid);

            //========== 4. 逐一增加权限 ================
            foreach (DictionaryEntry entryPopedom in htAllPopedom)
            {
                string sPopedom = (string)entryPopedom.Key;
                TbRoleRight tblRoleRight = new TbRoleRight();
                tblRoleRight.RoleUid = sRoleUid;
                tblRoleRight.MenuCode = sPopedom;
                tblRoleRight.Insert();
            }

            //========= 5. 清空权限、角色对应库 =========
            SbtRoleRight__Lib.Reset();
        }

        /// <summary>
        /// 判断是否叶权限
        /// </summary>
        /// <param name="sPopedom">权限标识</param>
        /// <returns>是否叶权限</returns>
        /// <remarks>如果其子节点有一个是菜单节点，则认为不是叶权限</remarks>
        private bool IsLeafPopedom(string sPopedom)
        {
            ArrayList arrChildPopedom 
                    = TbSysMenu__Lib.Instance.GetChildRecordsByMenuCode(sPopedom);
            for (int i = 0; i < arrChildPopedom.Count; i++)
            {
                TbSysMenu tblMenu = (TbSysMenu)arrChildPopedom[i];
                if (tblMenu.IsMenuItem == "Y")
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 得到权限节点的所有上级节点
        /// </summary>
        /// <param name="sPopedom">权限节点标识</param>
        /// <returns>所有上级节点列表</returns>
        private ArrayList GetAllParentsOfPopedom(string sPopedom)
        {
            ArrayList arrRet = new ArrayList();

            string sCurrentPopedom = sPopedom;
            while (true)
            {
                TbSysMenu tblMenu 
                        = TbSysMenu__Lib.Instance.GetMenuRecordByMenuCode(sCurrentPopedom);
                string sParentPopedom = tblMenu.ParentMenuCode;
                if (sParentPopedom.Length >= sCurrentPopedom.Length)
                    throw new Exception("父节点的标识串长度应小于当前节点的标识串长度");
                if (sParentPopedom.Length == 0)
                    break;

                arrRet.Add(sParentPopedom);
                sCurrentPopedom = sParentPopedom;
            }

            return arrRet;
        }
    }
}
