using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using Sigbit.Common;
using Sigbit.Web;
using Sigbit.Data;

namespace Sigbit.App.Net.IBXService.DNS.Service.DBDefine
{
    public class QDBDnsPoolService
    {
        private CodeTable _serviceCodeTable = null;
        public CodeTable ServiceCodeTable
        {
            get
            {
                if (_serviceCodeTable == null)
                {
                    _serviceCodeTable = new CodeTable();

                    string sSQL = "select service_id, service_name from dns_sys_service order by service_name";
                    DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string sServiceId = ConvertUtil.ToString(ds.Tables[0].Rows[i]["service_id"]);
                        string sServiceName = ConvertUtil.ToString(ds.Tables[0].Rows[i]["service_name"]);

                        _serviceCodeTable.AddItem(sServiceId, sServiceId + "(" + sServiceName + ")");
                    }

                    //_codeTableOfService.FillBySQL(sSQL);
                }

                return _serviceCodeTable;
            }
        }
    }
}
