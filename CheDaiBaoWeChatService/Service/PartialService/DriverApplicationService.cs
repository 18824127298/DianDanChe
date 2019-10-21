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
    public partial class DriverApplicationService
    {
        public List<DriverApplication> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<DriverApplication>(sqltran);
        }

        public DriverApplication GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<DriverApplication>(id, sqltran);
        }

        public int Insert(DriverApplication DriverApplication, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<DriverApplication>(DriverApplication, sqltran);
        }

        public void Update(DriverApplication DriverApplication, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<DriverApplication>(DriverApplication, sqltran);
        }

        public List<DriverApplication> Search(DriverApplication t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<DriverApplication>(t, sqltran);
        }
    }
}
