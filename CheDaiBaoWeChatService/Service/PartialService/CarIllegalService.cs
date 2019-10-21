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
    public partial class CarIllegalService
    {
        public List<CarIllegal> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<CarIllegal>(sqltran);
        }

        public CarIllegal GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<CarIllegal>(id, sqltran);
        }

        public int Insert(CarIllegal CarIllegal, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<CarIllegal>(CarIllegal, sqltran);
        }

        public void Update(CarIllegal CarIllegal, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<CarIllegal>(CarIllegal, sqltran);
        }

        public List<CarIllegal> Search(CarIllegal t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<CarIllegal>(t, sqltran);
        }
    }
}
