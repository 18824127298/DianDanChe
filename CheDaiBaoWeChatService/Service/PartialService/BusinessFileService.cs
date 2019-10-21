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
    public class BusinessFileService
    {
        public List<BusinessFile> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<BusinessFile>(sqltran);
        }

        public BusinessFile GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<BusinessFile>(id, sqltran);
        }

        public int Insert(BusinessFile BusinessFile, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<BusinessFile>(BusinessFile, sqltran);
        }

        public void Insert(List<BusinessFile> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<BusinessFile>(list, sqltran);
        }

        public void Update(BusinessFile BusinessFile, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<BusinessFile>(BusinessFile, sqltran);
        }

        public void Update(List<BusinessFile> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<BusinessFile>(list, sqltran);
        }

        public List<BusinessFile> Search(BusinessFile t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<BusinessFile>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<BusinessFile>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(BusinessFile t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<BusinessFile>(t, isDelete: isDelete, sqlTransation: sqltran);
        }



    }
}
