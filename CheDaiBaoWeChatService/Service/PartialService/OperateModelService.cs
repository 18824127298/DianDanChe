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
    public partial class OperateModelService
    {
        public List<OperateModel> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<OperateModel>(sqltran);
        }

        public OperateModel GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<OperateModel>(id, sqltran);
        }

        public int Insert(OperateModel OperateModel, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<OperateModel>(OperateModel, sqltran);
        }

        public void Insert(List<OperateModel> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<OperateModel>(list, sqltran);
        }

        public void Update(OperateModel OperateModel, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<OperateModel>(OperateModel, sqltran);
        }

        public void Update(List<OperateModel> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<OperateModel>(list, sqltran);
        }

        public List<OperateModel> Search(OperateModel t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<OperateModel>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<OperateModel>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(OperateModel t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<OperateModel>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}

