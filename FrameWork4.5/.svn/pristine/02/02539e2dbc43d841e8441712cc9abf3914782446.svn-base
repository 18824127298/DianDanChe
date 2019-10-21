using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework.Role
{
    /// <summary>
    /// 用户、权限的实用例程
    /// </summary>
    public class SbtRoleUserUtil
    {
        /// <summary>
        /// 用户的角色标识
        /// </summary>
        /// <param name="sUserUid">用户标识</param>
        /// <returns>角色标识。如未指定角色，则返回空串。</returns>
        public static string GetRoleUidOfUser(string sUserUid)
        {
            string sSQL = "select role_uid from sbt_role_user "
                    + " where user_uid = " + StringUtil.QuotedToDBStr(sUserUid);
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);
            if (ds.Tables[0].Rows.Count == 0)
                return "";

            string sRoleUid = (string)ds.Tables[0].Rows[0]["role_uid"];
            return sRoleUid;
        }

        public static void SetRoleUidToUser(string sUserUid, string sRoleUid)
        {
            TbRoleUser.DeleteAllRolesOfUser(sUserUid);

            if (sRoleUid == "")
                return;

            TbRoleUser tbl = new TbRoleUser();
            tbl.UserUid = sUserUid;
            tbl.RoleUid = sRoleUid;
            tbl.Insert();
        }
    }
}
