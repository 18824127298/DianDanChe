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
    public partial class CarService
    {
        public List<Car> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<Car>(sqltran);
        }

        public Car GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<Car>(id, sqltran);
        }

        public int Insert(Car Bid, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<Car>(Bid, sqltran);
        }

        public void Update(Car Bid, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<Car>(Bid, sqltran);
        }

        public List<Car> Search(Car t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<Car>(t, sqltran);
        }
    }
}
