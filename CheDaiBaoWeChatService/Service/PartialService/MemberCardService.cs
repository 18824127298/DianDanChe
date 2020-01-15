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
    public partial class MemberCardService
    {
        public List<MemberCard> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<MemberCard>(sqltran);
        }

        public MemberCard GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<MemberCard>(id, sqltran);
        }

        public int Insert(MemberCard MemberCard, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<MemberCard>(MemberCard, sqltran);
        }

        public void Insert(List<MemberCard> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<MemberCard>(list, sqltran);
        }

        public void Update(MemberCard MemberCard, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<MemberCard>(MemberCard, sqltran);
        }

        public void Update(List<MemberCard> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<MemberCard>(list, sqltran);
        }

        public List<MemberCard> Search(MemberCard t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<MemberCard>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<MemberCard>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(MemberCard t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<MemberCard>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}
