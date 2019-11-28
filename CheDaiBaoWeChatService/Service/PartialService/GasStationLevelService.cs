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
    public partial class GasStationLevelService
    {
        public List<GasStationLevel> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<GasStationLevel>(sqltran);
        }

        public GasStationLevel GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<GasStationLevel>(id, sqltran);
        }

        public int Insert(GasStationLevel GasStationLevel, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<GasStationLevel>(GasStationLevel, sqltran);
        }

        public void Insert(List<GasStationLevel> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<GasStationLevel>(list, sqltran);
        }

        public void Update(GasStationLevel GasStationLevel, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<GasStationLevel>(GasStationLevel, sqltran);
        }

        public void Update(List<GasStationLevel> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<GasStationLevel>(list, sqltran);
        }

        public List<GasStationLevel> Search(GasStationLevel t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<GasStationLevel>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<GasStationLevel>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(GasStationLevel t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<GasStationLevel>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}
