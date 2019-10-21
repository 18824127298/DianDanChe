using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Data;

using Sigbit.Common;
using Sigbit.Data;
using Sigbit.Framework.SubSystem.DBDefine;

namespace Sigbit.Framework.SubSystem.DBDefine
{
    /// <summary>
    /// 子系统的快缓池
    /// </summary>
    public class QDBPoolSubSystem
    {
        /// <summary>
        /// 按子系统标识组织
        /// </summary>
        private Hashtable _htSubSystemID = new Hashtable();

        private ArrayList _arrSubSystem = new ArrayList();

        /// <summary>
        /// 构造函数
        /// </summary>
        public QDBPoolSubSystem()
        {
            string sSQL = "select * from sbt_sys_sub_system_define order by display_order";
            DataSet ds = null;
            try
            {
                ds = DataHelper.Instance.ExecuteDataSet(sSQL);
            }
            catch
            {
                return;
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TbSysSubSystemDefine tblSubSystem = new TbSysSubSystemDefine();
                tblSubSystem.AssignByDataRow(ds, i);

                _htSubSystemID[tblSubSystem.SubSystemId] = tblSubSystem;
                _arrSubSystem.Add(tblSubSystem);
            }
        }

        public int SubSystemCount
        {
            get
            {
                return _arrSubSystem.Count;
            }
        }

        public TbSysSubSystemDefine GetSubSystemRec(int nIndex)
        {
            return (TbSysSubSystemDefine)_arrSubSystem[nIndex];
        }

        public TbSysSubSystemDefine GetSubSystemRecBySubSystemID(string sSubSystemID)
        {
            TbSysSubSystemDefine tblRet = (TbSysSubSystemDefine)_htSubSystemID[sSubSystemID];
            return tblRet;
        }

        public string GetSubSystemNameBySubSystemID(string sSubSystemID)
        {
            TbSysSubSystemDefine tblSubSystem = GetSubSystemRecBySubSystemID(sSubSystemID);
            if (tblSubSystem == null)
                return "";

            return tblSubSystem.SubSystemName;
        }
    }
}
