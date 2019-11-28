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

    public partial class OilGunService
    {
        public List<OilGun> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<OilGun>(sqltran);
        }

        public OilGun GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<OilGun>(id, sqltran);
        }

        public int Insert(OilGun OilGun, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<OilGun>(OilGun, sqltran);
        }

        public void Insert(List<OilGun> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<OilGun>(list, sqltran);
        }

        public void Update(OilGun OilGun, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<OilGun>(OilGun, sqltran);
        }

        public void Update(List<OilGun> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<OilGun>(list, sqltran);
        }

        public List<OilGun> Search(OilGun t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<OilGun>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<OilGun>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(OilGun t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<OilGun>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}
