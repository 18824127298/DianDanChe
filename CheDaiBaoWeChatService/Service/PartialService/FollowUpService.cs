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
    public partial class FollowUpService
    {
        public List<FollowUp> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<FollowUp>(sqltran);
        }

        public FollowUp GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<FollowUp>(id, sqltran);
        }

        public int Insert(FollowUp FollowUp, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<FollowUp>(FollowUp, sqltran);
        }

        public void Insert(List<FollowUp> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<FollowUp>(list, sqltran);
        }

        public void Update(FollowUp FollowUp, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<FollowUp>(FollowUp, sqltran);
        }

        public void Update(List<FollowUp> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<FollowUp>(list, sqltran);
        }

        public List<FollowUp> Search(FollowUp t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<FollowUp>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<FollowUp>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(FollowUp t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<FollowUp>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }                   
}
