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
    public partial class BorrowService  
    {
        public List<Borrow> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<Borrow>(sqltran);
        }

        public Borrow GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<Borrow>(id, sqltran);
        }

        public int Insert(Borrow Borrow, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<Borrow>(Borrow, sqltran);
        }

        public void Insert(List<Borrow> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<Borrow>(list, sqltran);
        }

        public void Update(Borrow Borrow, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<Borrow>(Borrow, sqltran);
        }

        public void Update(List<Borrow> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<Borrow>(list, sqltran);
        }

        public List<Borrow> Search(Borrow t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<Borrow>(t, sqltran);
        }

        public bool Delete(int id,bool isDelete=false,SqlTransaction sqltran=null)
        {
            return SqlConnections.GetOpenConnection().Delete<Borrow>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(Borrow t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<Borrow>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }                   
}
