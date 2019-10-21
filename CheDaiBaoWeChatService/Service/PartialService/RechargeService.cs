 

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
    public partial class RechargeService  
    {    
		public List<Recharge> GetAll(SqlTransaction sqltran=null)
        {
            return SqlConnections.GetOpenConnection().GetAll<Recharge>(sqltran);
        }

        public Recharge GetById(int id,SqlTransaction sqltran=null)
        {
            return SqlConnections.GetOpenConnection().GetById<Recharge>(id,sqltran);
        }

        public int Insert(Recharge Recharge,SqlTransaction sqltran=null)
        {
            return SqlConnections.GetOpenConnection().Insert<Recharge>(Recharge,sqltran);
        }

        public void Insert(List<Recharge> list,SqlTransaction sqltran=null)
        {
            SqlConnections.GetOpenConnection().Insert<Recharge>(list,sqltran);
        }

        public void Update(Recharge Recharge,SqlTransaction sqltran=null)
        {
            SqlConnections.GetOpenConnection().Update<Recharge>(Recharge,sqltran);
        }

        public void Update(List<Recharge> list,SqlTransaction sqltran=null)
        {
            SqlConnections.GetOpenConnection().Update<Recharge>(list,sqltran);
        }

		public List<Recharge> Search(Recharge t,SqlTransaction sqltran=null)
        {
            return SqlConnections.GetOpenConnection().Search<Recharge>(t,sqltran);
        }

        public bool Delete(int id,bool isDelete=false,SqlTransaction sqltran=null)
        {
            return SqlConnections.GetOpenConnection().Delete<Recharge>(id,isDelete: isDelete,sqlTransation: sqltran);
        }

		public void DeleteBySearch(Recharge t, bool isDelete = false,SqlTransaction sqltran=null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<Recharge>(t, isDelete: isDelete,sqlTransation: sqltran);
        }
    }                   
}
