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
    public partial class BorrowerService
    {
        public List<Borrower> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<Borrower>(sqltran);
        }

        public Borrower GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<Borrower>(id, sqltran);
        }

        public int Insert(Borrower Borrower, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<Borrower>(Borrower, sqltran);
        }

        public void Update(Borrower Borrower, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<Borrower>(Borrower, sqltran);
        }

        public List<Borrower> Search(Borrower t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<Borrower>(t, sqltran);
        }
    }
}
