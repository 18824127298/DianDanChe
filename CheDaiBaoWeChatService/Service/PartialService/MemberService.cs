using CheDaiBaoCommonService.Data;
using CheDaiBaoWeChatModel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;

namespace CheDaiBaoWeChatService.Service
{
    public partial class MemberService 
    {
            public List<Member> GetAll(SqlTransaction sqltran = null)
            {
                return SqlConnections.GetOpenConnection().GetAll<Member>(sqltran);
            }

            public Member GetById(int id, SqlTransaction sqltran = null)
            {
                return SqlConnections.GetOpenConnection().GetById<Member>(id, sqltran);
            }

            public int Insert(Member Member, SqlTransaction sqltran = null)
            {
                return SqlConnections.GetOpenConnection().Insert<Member>(Member, sqltran);
            }

            public void Insert(List<Member> list, SqlTransaction sqltran = null)
            {
                SqlConnections.GetOpenConnection().Insert<Member>(list, sqltran);
            }

            public void Update(Member Member, SqlTransaction sqltran = null)
            {
                SqlConnections.GetOpenConnection().Update<Member>(Member, sqltran);
            }

            public void Update(List<Member> list, SqlTransaction sqltran = null)
            {
                SqlConnections.GetOpenConnection().Update<Member>(list, sqltran);
            }

            public List<Member> Search(Member t, SqlTransaction sqltran = null)
            {
                return SqlConnections.GetOpenConnection().Search<Member>(t, sqltran);
            }

            public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
            {
                return SqlConnections.GetOpenConnection().Delete<Member>(id, isDelete: isDelete, sqlTransation: sqltran);
            }

            public void DeleteBySearch(Member t, bool isDelete = false, SqlTransaction sqltran = null)
            {
                SqlConnections.GetOpenConnection().DeleteBySearch<Member>(t, isDelete: isDelete, sqlTransation: sqltran);
            }
        
    }
}