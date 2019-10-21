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
    public partial class IdCardInformationService
    {
        public List<IdCardInformation> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<IdCardInformation>(sqltran);
        }

        public IdCardInformation GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<IdCardInformation>(id, sqltran);
        }

        public int Insert(IdCardInformation IdCardInformation, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<IdCardInformation>(IdCardInformation, sqltran);
        }

        public void Insert(List<IdCardInformation> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<IdCardInformation>(list, sqltran);
        }

        public void Update(IdCardInformation IdCardInformation, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<IdCardInformation>(IdCardInformation, sqltran);
        }

        public void Update(List<IdCardInformation> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<IdCardInformation>(list, sqltran);
        }

        public List<IdCardInformation> Search(IdCardInformation t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<IdCardInformation>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<IdCardInformation>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(IdCardInformation t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<IdCardInformation>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}
