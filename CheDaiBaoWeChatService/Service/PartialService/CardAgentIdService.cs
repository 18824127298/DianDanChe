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
    public partial class CardAgentIdService
    {
        public List<CardAgentId> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<CardAgentId>(sqltran);
        }

        public CardAgentId GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<CardAgentId>(id, sqltran);
        }

        public int Insert(CardAgentId CardAgentId, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<CardAgentId>(CardAgentId, sqltran);
        }

        public void Insert(List<CardAgentId> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<CardAgentId>(list, sqltran);
        }

        public void Update(CardAgentId CardAgentId, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<CardAgentId>(CardAgentId, sqltran);
        }

        public void Update(List<CardAgentId> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<CardAgentId>(list, sqltran);
        }

        public List<CardAgentId> Search(CardAgentId t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<CardAgentId>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<CardAgentId>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(CardAgentId t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<CardAgentId>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}
