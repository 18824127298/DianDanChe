using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Collections;

using Sigbit.Common;
using Sigbit.Web;
using Sigbit.Data;

namespace Sigbit.App.Net.IBXService.DNS.Service.DBDefine
{
    public class QDBDnsPoolTransCode
    {
        private ArrayList _arrTransCode = new ArrayList();

        public QDBDnsPoolTransCode()
        {
            string sSQL = "select * from dns_sys_trans_code order by trans_code";
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TbSysTransCode tblTransCode = new TbSysTransCode();
                tblTransCode.AssignByDataRow(ds, i);

                _arrTransCode.Add(tblTransCode);
            }
        }

        public TbSysTransCode GetTransCodeRec(int nIndex)
        {
            return (TbSysTransCode)_arrTransCode[nIndex];
        }

        public int TransCodeCount
        {
            get
            {
                return _arrTransCode.Count;
            }
        }

        private CodeTable _transCodeCodeTable = null;
        public CodeTable TransCodeCodeTable
        {
            get
            {
                if (_transCodeCodeTable == null)
                {
                    _transCodeCodeTable = new CodeTable();

                    for (int i = 0; i < this.TransCodeCount; i++)
                    {
                        TbSysTransCode tblTransCode = this.GetTransCodeRec(i);
                        _transCodeCodeTable.AddItem(tblTransCode.TransCode, tblTransCode.TransCode 
                                + "(" + tblTransCode.TransCodeName + ")");
                    }
                }

                return _transCodeCodeTable;
            }
        }

    }
}
