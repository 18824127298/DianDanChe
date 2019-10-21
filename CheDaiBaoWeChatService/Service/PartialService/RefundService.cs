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
    /// </summary>
    public partial class RefundService
    {
        public List<Refund> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<Refund>(sqltran);
        }

        public Refund GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<Refund>(id, sqltran);
        }

        public int Insert(Refund Refund, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<Refund>(Refund, sqltran);
        }

        public void Insert(List<Refund> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<Refund>(list, sqltran);
        }

        public void Update(Refund Refund, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<Refund>(Refund, sqltran);
        }

        public void Update(List<Refund> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<Refund>(list, sqltran);
        }

        public List<Refund> Search(Refund t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<Refund>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<Refund>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(Refund t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<Refund>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}
