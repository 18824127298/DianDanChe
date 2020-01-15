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
    public partial class GodBounsService
    {
        public int Insert(GodBouns GodBouns, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<GodBouns>(GodBouns, sqltran);
        }

        public GodBouns GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<GodBouns>(id, sqltran);
        }

        public List<GodBouns> Search(GodBouns t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<GodBouns>(t, sqltran);
        }

        public void Update(GodBouns GodBouns, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<GodBouns>(GodBouns, sqltran);
        }

    }
}
