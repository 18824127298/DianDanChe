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
    public partial class OperaService
    {
        public List<Opera> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<Opera>(sqltran);
        }

        public Opera GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<Opera>(id, sqltran);
        }

        public int Insert(Opera Opera, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<Opera>(Opera, sqltran);
        }

        public void Insert(List<Opera> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<Opera>(list, sqltran);
        }

        public void Update(Opera Opera, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<Opera>(Opera, sqltran);
        }

        public void Update(List<Opera> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<Opera>(list, sqltran);
        }

        public List<Opera> Search(Opera t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<Opera>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<Opera>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(Opera t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<Opera>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}
