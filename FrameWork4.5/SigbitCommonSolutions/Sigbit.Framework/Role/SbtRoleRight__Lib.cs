using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Collections;

using Sigbit.Data;

namespace Sigbit.Framework.Role
{
    /// <summary>
    /// ��ɫ��Ȩ�޵Ķ�Ӧ��ϵ��
    /// </summary>
    public class SbtRoleRight__Lib
    {
        /// <summary>
        /// ��ϣ����ɫӵ�е�Ȩ��
        /// </summary>
        private Hashtable _htRoleOwnsRight = new Hashtable();

        /// <summary>
        /// ��ϣ��Ȩ�����ڵĽ�ɫ
        /// </summary>
        private Hashtable _htRightBelongToRole = new Hashtable();

        private static SbtRoleRight__Lib _thisInstance = null;
        /// <summary>
        /// Ψһʵ��
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
        /// ���������ݣ�׼����ȡ
        /// </summary>
        public static void Reset()
        {
            _thisInstance = null;
        }

        /// <summary>
        /// ���캯��
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
        /// ����һ����ɫȨ�޶�Ӧ��ϵ������
        /// </summary>
        /// <param name="tblRoleRight">��ɫȨ�޶�Ӧ��ϵ��¼</param>
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
        /// ��ɫ�Ƿ�ӵ��Ȩ�޵��ж�
        /// </summary>
        /// <param name="sRoleUid">��ɫ��ʶ</param>
        /// <param name="sPopedomUid">Ȩ�ޱ�ʶ</param>
        /// <returns>�Ƿ�ӵ��Ȩ��</returns>
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
