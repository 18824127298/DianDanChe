

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoCommonService.Data;
namespace CheDaiBaoWeChatService.Service
{
    /// <summary>
    ///  
    /// </summary>
    public partial class BankCardService
    {
        public List<BankCard> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<BankCard>(sqltran);
        }

        public BankCard GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<BankCard>(id, sqltran);
        }

        public int Insert(BankCard BankCard, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<BankCard>(BankCard, sqltran);
        }

        public void Insert(List<BankCard> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<BankCard>(list, sqltran);
        }

        public void Update(BankCard BankCard, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<BankCard>(BankCard, sqltran);
        }

        public void Update(List<BankCard> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<BankCard>(list, sqltran);
        }

        public List<BankCard> Search(BankCard t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<BankCard>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<BankCard>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(BankCard t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<BankCard>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}
