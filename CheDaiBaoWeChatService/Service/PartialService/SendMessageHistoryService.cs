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
    public partial class SendMessageHistoryService
    {
        public List<SendMessageHistory> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<SendMessageHistory>(sqltran);
        }

        public SendMessageHistory GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<SendMessageHistory>(id, sqltran);
        }

        public int Insert(SendMessageHistory SendMessageHistory, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<SendMessageHistory>(SendMessageHistory, sqltran);
        }

        public void Insert(List<SendMessageHistory> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<SendMessageHistory>(list, sqltran);
        }

        public void Update(SendMessageHistory SendMessageHistory, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<SendMessageHistory>(SendMessageHistory, sqltran);
        }

        public void Update(List<SendMessageHistory> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<SendMessageHistory>(list, sqltran);
        }

        public List<SendMessageHistory> Search(SendMessageHistory t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<SendMessageHistory>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<SendMessageHistory>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(SendMessageHistory t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<SendMessageHistory>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}
