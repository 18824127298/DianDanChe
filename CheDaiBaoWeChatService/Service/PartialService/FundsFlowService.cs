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
    /// <summary>
    ///  
    /// </summary>
    public partial class FundsFlowService
    {
        public List<FundsFlow> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<FundsFlow>(sqltran);
        }

        public FundsFlow GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<FundsFlow>(id, sqltran);
        }

        public int Insert(FundsFlow FundsFlow, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<FundsFlow>(FundsFlow, sqltran);
        }

        public void Update(FundsFlow FundsFlow, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<FundsFlow>(FundsFlow, sqltran);
        }

        public void Update(List<FundsFlow> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<FundsFlow>(list, sqltran);
        }

        public List<FundsFlow> Search(FundsFlow t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<FundsFlow>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<FundsFlow>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(FundsFlow t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<FundsFlow>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}
