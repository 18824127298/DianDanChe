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
    public partial class YouKaService
    {
        public List<YouKa> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<YouKa>(sqltran);
        }

        public YouKa GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<YouKa>(id, sqltran);
        }

        public int Insert(YouKa YouKa, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<YouKa>(YouKa, sqltran);
        }

        public void Insert(List<YouKa> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<YouKa>(list, sqltran);
        }

        public void Update(YouKa YouKa, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<YouKa>(YouKa, sqltran);
        }

        public void Update(List<YouKa> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<YouKa>(list, sqltran);
        }

        public List<YouKa> Search(YouKa t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<YouKa>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<YouKa>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(YouKa t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<YouKa>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}
