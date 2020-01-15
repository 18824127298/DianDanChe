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
    public partial class CardService
    {
        public List<Card> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<Card>(sqltran);
        }

        public Card GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<Card>(id, sqltran);
        }

        public int Insert(Card Card, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<Card>(Card, sqltran);
        }

        public void Insert(List<Card> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<Card>(list, sqltran);
        }

        public void Update(Card Card, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<Card>(Card, sqltran);
        }

        public void Update(List<Card> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<Card>(list, sqltran);
        }

        public List<Card> Search(Card t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<Card>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<Card>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(Card t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<Card>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}
