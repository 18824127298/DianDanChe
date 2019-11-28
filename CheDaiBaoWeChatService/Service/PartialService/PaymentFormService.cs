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

    public partial class PaymentFormService
    {
        public List<PaymentForm> GetAll(SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetAll<PaymentForm>(sqltran);
        }

        public PaymentForm GetById(int id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().GetById<PaymentForm>(id, sqltran);
        }

        public int Insert(PaymentForm PaymentForm, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Insert<PaymentForm>(PaymentForm, sqltran);
        }

        public void Insert(List<PaymentForm> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Insert<PaymentForm>(list, sqltran);
        }

        public void Update(PaymentForm PaymentForm, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<PaymentForm>(PaymentForm, sqltran);
        }

        public void Update(List<PaymentForm> list, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().Update<PaymentForm>(list, sqltran);
        }

        public List<PaymentForm> Search(PaymentForm t, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Search<PaymentForm>(t, sqltran);
        }

        public bool Delete(int id, bool isDelete = false, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Delete<PaymentForm>(id, isDelete: isDelete, sqlTransation: sqltran);
        }

        public void DeleteBySearch(PaymentForm t, bool isDelete = false, SqlTransaction sqltran = null)
        {
            SqlConnections.GetOpenConnection().DeleteBySearch<PaymentForm>(t, isDelete: isDelete, sqlTransation: sqltran);
        }
    }
}
