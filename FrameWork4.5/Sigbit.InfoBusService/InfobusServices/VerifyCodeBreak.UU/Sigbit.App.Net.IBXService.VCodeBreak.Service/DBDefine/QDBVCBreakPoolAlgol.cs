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
    public class QDBVCBreakPoolAlgol
    {
        #region 容器
        private ArrayList _arrAlgol = new ArrayList();
        private Hashtable _htAlgolID = new Hashtable();
        #endregion

        #region 构造函数
        public QDBVCBreakPoolAlgol()
        {
            string sSQL = "select * from vcb_sys_break_algol order by algol_name";
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TbSysBreakAlgol tblAlgol = new TbSysBreakAlgol();
                tblAlgol.AssignByDataRow(ds, i);

                _arrAlgol.Add(tblAlgol);
                _htAlgolID[tblAlgol.AlgolId] = tblAlgol;
            }
        }
        #endregion

        #region 基础操作
        public int AlgolCount
        {
            get
            {
                return _arrAlgol.Count;
            }
        }

        public TbSysBreakAlgol GetAlgolRec(int nIndex)
        {
            return (TbSysBreakAlgol)_arrAlgol[nIndex];
        }

        public TbSysBreakAlgol GetAlgolRec(string sAlgolID)
        {
            return (TbSysBreakAlgol)_htAlgolID[sAlgolID];
        }
        #endregion

        #region 定位查找
        public string GetAlgolNameByID(string sAlgolID)
        {
            TbSysBreakAlgol tbl = GetAlgolRec(sAlgolID);

            if (tbl == null)
                return sAlgolID;
            else
                return tbl.AlgolName;
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

                    for (int i = 0; i < this.AlgolCount; i++)
                    {
                        TbSysBreakAlgol tblAlgol = GetAlgolRec(i);
                        _codeTableOfAll.AddItem(tblAlgol.AlgolId, tblAlgol.AlgolName);
                    }
                }
                return _codeTableOfAll;
            }
        }
        #endregion
    }
}
