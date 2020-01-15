using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoCommonService.Data;


namespace CheDaiBaoWeChatService.Service
{
    public partial class GodBounsRecordService
    {
        public int Insert(GodBounsRecord GodBounsRecord, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<GodBounsRecord>(GodBounsRecord, sqltran);
        }

        public GodBounsRecord GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<GodBounsRecord>(id, sqltran);
        }

        public List<GodBounsRecord> Search(GodBounsRecord t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<GodBounsRecord>(t, sqltran);
        }

        public void Update(GodBounsRecord GodBounsRecord, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<GodBounsRecord>(GodBounsRecord, sqltran);
        }

        public void Update(List<GodBounsRecord> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<GodBounsRecord>(list, sqltran);
        }
    }
}
