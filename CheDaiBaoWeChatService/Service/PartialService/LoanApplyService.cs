using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoCommonService.Data;

namespace CheDaiBaoWeChatService.Service
{
    public partial class LoanApplyService
    {
        public int Insert(LoanApply LoanApply, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<LoanApply>(LoanApply, sqltran);
        }

        public List<LoanApply> Search(LoanApply t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<LoanApply>(t, sqltran);
        }

        public LoanApply GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<LoanApply>(id, sqltran);
        }

        public void Update(LoanApply LoanApply, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<LoanApply>(LoanApply, sqltran);
        }

        public void Update(List<LoanApply> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<LoanApply>(list, sqltran);
        }

        public List<LoanApply> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<LoanApply>(sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<LoanApply>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(LoanApply t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<LoanApply>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}
