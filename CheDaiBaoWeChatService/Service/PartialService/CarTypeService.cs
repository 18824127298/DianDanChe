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
    public partial class CarTypeService
    {
        public List<CarType> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<CarType>(sqltran);
        }

        public CarType GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<CarType>(id, sqltran);
        }

        public int Insert(CarType Bid, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<CarType>(Bid, sqltran);
        }

        public void Update(CarType Bid, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<CarType>(Bid, sqltran);
        }

        public List<CarType> Search(CarType t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<CarType>(t, sqltran);
        }
    }
}
