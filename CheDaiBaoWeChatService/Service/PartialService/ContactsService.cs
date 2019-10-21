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
    public partial class ContactsService
    {
        public List<Contacts> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<Contacts>(sqltran);
        }

        public Contacts GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<Contacts>(id, sqltran);
        }

        public int Insert(Contacts Contacts, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<Contacts>(Contacts, sqltran);
        }

        public void Insert(List<Contacts> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<Contacts>(list, sqltran);
        }

        public void Update(Contacts Contacts, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<Contacts>(Contacts, sqltran);
        }

        public void Update(List<Contacts> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<Contacts>(list, sqltran);
        }

        public List<Contacts> Search(Contacts t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<Contacts>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<Contacts>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(Contacts t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<Contacts>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}
