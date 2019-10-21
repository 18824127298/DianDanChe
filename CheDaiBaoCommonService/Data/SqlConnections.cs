using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Sigbit.Common;
using Sigbit.Data;

namespace CheDaiBaoCommonService.Data
{
    public class SqlConnections
    {

        public static SqlConnection GetOpenConnection()
        {
            //var cs = ConfigurationManager.ConnectionStrings["ConnectionOfString"].ConnectionString;
            var cs = DataHelperConfig.Instance.GetConnectString("instanceDefault");
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder(cs);
            scsb.MultipleActiveResultSets = true;
            cs = scsb.ConnectionString;
            SqlConnection sqlconnections = new SqlConnection(cs);
            return sqlconnections;
        }
    }
}
