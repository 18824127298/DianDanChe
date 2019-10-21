using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Collections;

using Sigbit.Common;
using Sigbit.Data;
using Sigbit.Web;

namespace Sigbit.App.Net.IBXService.VCodeBreak.Service.DBDefine
{
    public class QDBVCBreakPoolVCodeUsage
    {
        #region 容器
        private ArrayList _arrVCodeUsage = new ArrayList();
        private Hashtable _htVCodeID = new Hashtable();
        #endregion

        #region 构造函数
        public QDBVCBreakPoolVCodeUsage()
        {
            string sSQL = "select * from vcb_sys_vcode order by vcode_name";
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TbSysVcode tblVCodeUsage = new TbSysVcode();
                tblVCodeUsage.AssignByDataRow(ds, i);

                _arrVCodeUsage.Add(tblVCodeUsage);
                _htVCodeID[tblVCodeUsage.VcodeId] = tblVCodeUsage;
            }
        }
        #endregion

        #region 基础操作
        public int VCodeUsageCount
        {
            get
            {
                return _arrVCodeUsage.Count;
            }
        }

        public TbSysVcode GetVCodeUsageRec(int nIndex)
        {
            return (TbSysVcode)_arrVCodeUsage[nIndex];
        }

        public TbSysVcode GetVCodeUsageRec(string sVCodeID)
        {
            return (TbSysVcode)_htVCodeID[sVCodeID];
        }
        #endregion

        #region 定位查找
        public string GetVcodeNameByID(string sVCodeID)
        {
            TbSysVcode tbl = GetVCodeUsageRec(sVCodeID);

            if (tbl == null)
                return sVCodeID;
            else
                return tbl.VcodeName;
        }
        #endregion

        #region CodeTable
        private CodeTable _codeTableOfAll = null;
        public CodeTable CodeTableOfAll
        {
            get
            {
                if (_codeTableOfAll == null)
                {
                    _codeTableOfAll = new CodeTable();

                    for (int i = 0; i < this.VCodeUsageCount; i++)
                    {
                        TbSysVcode tblVCodeUsage = GetVCodeUsageRec(i);
                        _codeTableOfAll.AddItem(tblVCodeUsage.VcodeId, tblVCodeUsage.VcodeName);
                    }
                }
                return _codeTableOfAll;
            }
        }
        #endregion
    }
}
