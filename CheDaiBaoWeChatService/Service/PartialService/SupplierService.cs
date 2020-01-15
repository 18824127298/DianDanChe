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
    public partial class SupplierService
    {
        public List<Supplier> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<Supplier>(sqltran);
        }

        public Supplier GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<Supplier>(id, sqltran);
        }

        public int Insert(Supplier Supplier, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<Supplier>(Supplier, sqltran);
        }

        public void Insert(List<Supplier> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<Supplier>(list, sqltran);
        }

        public void Update(Supplier Supplier, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<Supplier>(Supplier, sqltran);
        }

        public void Update(List<Supplier> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<Supplier>(list, sqltran);
        }

        public List<Supplier> Search(Supplier t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<Supplier>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<Supplier>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(Supplier t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<Supplier>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}
