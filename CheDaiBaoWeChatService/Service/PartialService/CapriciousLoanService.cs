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
    public partial class CapriciousLoanService  
    {
        public List<CapriciousLoan> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<CapriciousLoan>(sqltran);
        }

        public CapriciousLoan GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<CapriciousLoan>(id, sqltran);
        }

        public int Insert(CapriciousLoan CapriciousLoan, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<CapriciousLoan>(CapriciousLoan, sqltran);
        }

        public void Insert(List<CapriciousLoan> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<CapriciousLoan>(list, sqltran);
        }

        public void Update(CapriciousLoan CapriciousLoan, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<CapriciousLoan>(CapriciousLoan, sqltran);
        }

        public void Update(List<CapriciousLoan> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<CapriciousLoan>(list, sqltran);
        }

        public List<CapriciousLoan> Search(CapriciousLoan t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<CapriciousLoan>(t, sqltran);
        }

        public bool Delete(int id,bool isDelete=false,SqlTransaction sqltran=null)
        {
            return SqlConnections.GetOpenConnection().Delete<CapriciousLoan>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(CapriciousLoan t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<CapriciousLoan>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }                   
}
