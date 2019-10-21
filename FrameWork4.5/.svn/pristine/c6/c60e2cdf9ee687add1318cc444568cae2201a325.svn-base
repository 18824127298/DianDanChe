using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Data;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.App.Net.IBXService.DNS.Service.DBDefine
{
    public class QDBDnsPoolMapTransCodeUrl
    {
        private Hashtable _htTransCode = new Hashtable();
        private Hashtable _htTransCodeFromSystem = new Hashtable();

        public QDBDnsPoolMapTransCodeUrl()
        {
            string sSQL = "select * from dns_map_trans_code_url";
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TbMapTransCodeUrl tblMap = new TbMapTransCodeUrl();
                tblMap.AssignByDataRow(ds, i);

                if (tblMap.FromClientId == "" && tblMap.FromClientVersion == "" && tblMap.FromSystem == "")
                {
                    _htTransCode[tblMap.TransCode] = tblMap;
                }
                else if (tblMap.FromSystem != "")
                {
                    _htTransCodeFromSystem[tblMap.TransCode + "/" + tblMap.FromSystem] = tblMap;
                }
            }
        }

        private TbMapTransCodeUrl GetMapRecByTransCodeOnly(string sTransCode)
        {
            return (TbMapTransCodeUrl)_htTransCode[sTransCode];
        }

        public TbMapTransCodeUrl GetMapRecByTransCodeFromSystem(string sTransCode, string sFromSystem)
        {
            TbMapTransCodeUrl tblRet = (TbMapTransCodeUrl)_htTransCodeFromSystem[sTransCode + "/" + sFromSystem];
            if (tblRet == null)
                tblRet = GetMapRecByTransCodeOnly(sTransCode);

            return tblRet;
        }
    }
}
