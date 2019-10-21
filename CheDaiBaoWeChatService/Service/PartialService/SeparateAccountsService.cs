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
    public partial class SeparateAccountsService
    {
        public List<SeparateAccounts> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<SeparateAccounts>(sqltran);
        }

        public SeparateAccounts GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<SeparateAccounts>(id, sqltran);
        }

        public int Insert(SeparateAccounts SeparateAccounts, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<SeparateAccounts>(SeparateAccounts, sqltran);
        }

        public void Update(SeparateAccounts SeparateAccounts, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<SeparateAccounts>(SeparateAccounts, sqltran);
        }

        public List<SeparateAccounts> Search(SeparateAccounts t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<SeparateAccounts>(t, sqltran);
        }
    }
}
