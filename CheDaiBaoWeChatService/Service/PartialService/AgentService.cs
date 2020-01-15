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
    public partial class AgentService
    {
        public List<Agent> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<Agent>(sqltran);
        }

        public Agent GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<Agent>(id, sqltran);
        }

        public int Insert(Agent Agent, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<Agent>(Agent, sqltran);
        }

        public void Insert(List<Agent> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<Agent>(list, sqltran);
        }

        public void Update(Agent Agent, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<Agent>(Agent, sqltran);
        }

        public void Update(List<Agent> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<Agent>(list, sqltran);
        }

        public List<Agent> Search(Agent t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<Agent>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<Agent>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(Agent t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<Agent>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}
