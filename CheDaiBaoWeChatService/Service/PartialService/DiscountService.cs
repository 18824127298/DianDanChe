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
    public partial class DiscountService
    {
        public List<Discount> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<Discount>(sqltran);
        }

        public Discount GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<Discount>(id, sqltran);
        }

        public int Insert(Discount Discount, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<Discount>(Discount, sqltran);
        }

        public void Update(Discount Discount, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<Discount>(Discount, sqltran);
        }

        public List<Discount> Search(Discount t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<Discount>(t, sqltran);
        }
    }
}
