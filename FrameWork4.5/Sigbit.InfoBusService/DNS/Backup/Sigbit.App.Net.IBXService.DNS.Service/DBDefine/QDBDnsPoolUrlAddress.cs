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
    public class QDBDnsPoolUrlAddress
    {
        private ArrayList _arrUrlAddress = new ArrayList();
        private Hashtable _htUrlAddressUid = new Hashtable();

        public QDBDnsPoolUrlAddress()
        {
            string sSQL = "select * from dns_sys_url_address order by url_address_name";
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TbSysUrlAddress tblUrlAddress = new TbSysUrlAddress();
                tblUrlAddress.AssignByDataRow(ds, i);

                _arrUrlAddress.Add(tblUrlAddress);
                _htUrlAddressUid[tblUrlAddress.UrlAddressUid] = tblUrlAddress;
            }
        }

        public TbSysUrlAddress GetUrlAddressRec(int nIndex)
        {
            return (TbSysUrlAddress)_arrUrlAddress[nIndex];
        }

        public int UrlAddressCount
        {
            get
            {
                return _arrUrlAddress.Count;
            }
        }

        private CodeTable _urlAddressCodeTable = null;
        public CodeTable UrlAddressCodeTable
        {
            get
            {
                if (_urlAddressCodeTable == null)
                {
                    _urlAddressCodeTable = new CodeTable();

                    for (int i = 0; i < this.UrlAddressCount; i++)
                    {
                        TbSysUrlAddress tblUrlAddress = this.GetUrlAddressRec(i);
                        _urlAddressCodeTable.AddItem(tblUrlAddress.UrlAddressUid, tblUrlAddress.UrlAddressName);
                    }
                }

                return _urlAddressCodeTable;
            }
        }

        public TbSysUrlAddress GetUrlAddressRecByUrlAddressUid(string sUrlAddressUid)
        {
            return (TbSysUrlAddress)_htUrlAddressUid[sUrlAddressUid];
        }
    }
}
