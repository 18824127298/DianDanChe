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
    public class LoanFileService
    {
        public List<LoanFile> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<LoanFile>(sqltran);
        }

        public LoanFile GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<LoanFile>(id, sqltran);
        }

        public int Insert(LoanFile LoanFile, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<LoanFile>(LoanFile, sqltran);
        }

        public void Insert(List<LoanFile> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<LoanFile>(list, sqltran);
        }

        public void Update(LoanFile LoanFile, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<LoanFile>(LoanFile, sqltran);
        }

        public void Update(List<LoanFile> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<LoanFile>(list, sqltran);
        }

        public List<LoanFile> Search(LoanFile t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<LoanFile>(t, sqltran);
        }

    }
}
