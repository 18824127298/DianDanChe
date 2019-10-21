

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using CheDaiBaoCommonService.Data;
using CheDaiBaoWeChatModel.Models;


namespace CheDaiBaoWeChatService.Service
{
    public partial class FundsFlowService
    {
        /// <summary>
        /// 获取用户的账户的可用资金
        /// 作者：陈甜 日期：2014-09-22 10：23
        /// </summary>
        /// <param name="godId">用户Id</param>
        /// <param name="sqlTransation"></param>
        /// <returns></returns>
        public decimal GetAmountByBorrowerId(int borrowerId, SqlTransaction sqlTransation = null)
        {
            SqlConnection sqlConnection = sqlTransation == null ? SqlConnections.GetOpenConnection() : sqlTransation.Connection;

            List<FundsFlow> fundsFlowList = sqlConnection.Query<FundsFlow>(
                @"select * from FundsFlow where (IncomeGodId=@IncomeGodId or PayGodId=@PayGodId) and IsValid=1 and IsComputing=1",
                new { IncomeGodId = borrowerId, PayGodId = borrowerId }, sqlTransation).ToList();

            decimal incomeAmount = fundsFlowList.FindAll(fa => fa.IsFreeze == false && fa.IncomeGodId == borrowerId).Sum(s => s.Amount.Value);
            decimal payAmount = fundsFlowList.FindAll(fa => fa.PayGodId == borrowerId).Sum(s => s.Amount.Value);

            return Math.Floor((incomeAmount - payAmount) * 100m) / 100m;
        }

    }
}