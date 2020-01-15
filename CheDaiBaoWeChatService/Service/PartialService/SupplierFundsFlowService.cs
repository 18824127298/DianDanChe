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

    public partial class SupplierFundsFlowService
    {
        public List<SupplierFundsFlow> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<SupplierFundsFlow>(sqltran);
        }

        public SupplierFundsFlow GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<SupplierFundsFlow>(id, sqltran);
        }

        public int Insert(SupplierFundsFlow SupplierFundsFlow, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<SupplierFundsFlow>(SupplierFundsFlow, sqltran);
        }

        public void Update(SupplierFundsFlow SupplierFundsFlow, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<SupplierFundsFlow>(SupplierFundsFlow, sqltran);
        }

        public void Update(List<SupplierFundsFlow> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<SupplierFundsFlow>(list, sqltran);
        }

        public List<SupplierFundsFlow> Search(SupplierFundsFlow t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<SupplierFundsFlow>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<SupplierFundsFlow>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(SupplierFundsFlow t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<SupplierFundsFlow>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}
