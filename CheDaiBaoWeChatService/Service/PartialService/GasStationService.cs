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
    public partial class GasStationService
    {
        public List<GasStation> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<GasStation>(sqltran);
        }

        public GasStation GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<GasStation>(id, sqltran);
        }

        public int Insert(GasStation GasStation, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<GasStation>(GasStation, sqltran);
        }

        public void Update(GasStation GasStation, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<GasStation>(GasStation, sqltran);
        }

        public void Update(List<GasStation> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<GasStation>(list, sqltran);
        }

        public List<GasStation> Search(GasStation t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<GasStation>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<GasStation>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(GasStation t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<GasStation>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}
