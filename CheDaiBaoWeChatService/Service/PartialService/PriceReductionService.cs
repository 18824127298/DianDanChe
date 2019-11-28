using CheDaiBaoCommonService.Data;
using CheDaiBaoWeChatModel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatService.Service
{
    public partial class PriceReductionService
    {
        public List<PriceReduction> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<PriceReduction>(sqltran);
        }

        public PriceReduction GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<PriceReduction>(id, sqltran);
        }

        public int Insert(PriceReduction Bid, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<PriceReduction>(Bid, sqltran);
        }

        public void Update(PriceReduction Bid, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<PriceReduction>(Bid, sqltran);
        }

        public List<PriceReduction> Search(PriceReduction t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<PriceReduction>(t, sqltran);
        }
    }
}
