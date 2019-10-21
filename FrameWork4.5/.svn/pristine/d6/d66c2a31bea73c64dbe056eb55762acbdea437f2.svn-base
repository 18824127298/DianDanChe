using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Collections;

using Sigbit.Data;

namespace Sigbit.Framework.Role
{
    /// <summary>
    /// 角色和权限的对应关系库
    /// </summary>
    public class SbtRoleRight__Lib
    {
        /// <summary>
        /// 哈希表：角色拥有的权限
        /// </summary>
        private Hashtable _htRoleOwnsRight = new Hashtable();

        /// <summary>
        /// 哈希表：权限属于的角色
        /// </summary>
        private Hashtable _htRightBelongToRole = new Hashtable();

        private static SbtRoleRight__Lib _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static SbtRoleRight__Lib Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new SbtRoleRight__Lib();
                return _thisInstance;
            }
        }

        /// <summary>
        /// 清除库的内容，准备重取
        /// </summary>
        public static void Reset()
        {
            _thisInstance = null;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SbtRoleRight__Lib()
        {
            string sSQL = "select * from sbt_role_right";
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TbRoleRight tbl = new TbRoleRight();
                tbl.AssignByDataRow(ds, i);

                AddRoleRightRecordToLib(tbl);
            }
        }

        /// <summary>
        /// 增加一个角色权限对应关系到库中
        /// </summary>
        /// <param name="tblRoleRight">角色权限对应关系记录</param>
        private void AddRoleRightRecordToLib(TbRoleRight tblRoleRight)
        {
            string sRole = tblRoleRight.RoleUid;
            string sRight = tblRoleRight.MenuCode;

            if (_htRoleOwnsRight[sRole] == null)
            {
                Hashtable htRight = new Hashtable();
                htRight.Add(sRight, sRight);
                _htRoleOwnsRight.Add(sRole, htRight);
            }
            else
            {
                Hashtable htRight = (Hashtable)_htRoleOwnsRight[sRole];
                htRight.Add(sRight, sRight);
            }

            if (_htRightBelongToRole[sRight] == null)
            {
                Hashtable htRole = new Hashtable();
                htRole.Add(sRole, sRole);
                _htRightBelongToRole.Add(sRight, htRole);
            }
            else
            {
                Hashtable htRole = (Hashtable)_htRightBelongToRole[sRight];
                htRole.Add(sRole, sRole);
            }
        }

        /// <summary>
        /// 角色是否拥有权限的判断
        /// </summary>
        /// <param name="sRoleUid">角色标识</param>
        /// <param name="sPopedomUid">权限标识</param>
        /// <returns>是否拥有权限</returns>
        public bool RoleHasPopedom(string sRoleUid, string sPopedomUid)
        {
            Hashtable htRight = (Hashtable)_htRoleOwnsRight[sRoleUid];
            if (htRight == null)
                return false;

            if (htRight[sPopedomUid] == null)
                return false;
            else
                return true;
        }
    }
}
