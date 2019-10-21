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
    public partial class LoanApplyService
    {

        public List<LoanApply> GetLoanApplyListBySalesmanId(int salesmanId, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Query<LoanApply, Borrower, LoanApply>(
                @"select * from LoanApply l join Borrower b on l.borrowerId = b.Id
and l.IsValid= 1 and l.SalesmanId = @SalesmanId and CONVERT(varchar(100), l.CreateTime, 111) = CONVERT(varchar(100), GETDATE(), 111)
and l.RepaymentStatus != 11 order by l.CreateTime desc",
                (loanApply, borrower) =>
                {
                    loanApply.borrower = borrower;
                    return loanApply;
                },
                new { SalesmanId = salesmanId }, sqltran).ToList();
        }

        public List<LoanApply> GetAllLoanApplyListBySalesmanId(int salesmanId, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Query<LoanApply, Borrower, LoanApply>(
                @"select * from LoanApply l join Borrower b on l.borrowerId = b.Id
and l.IsValid= 1 and l.SalesmanId = @SalesmanId and l.RepaymentStatus != 11  order by l.CreateTime desc",
                (loanApply, borrower) =>
                {
                    loanApply.borrower = borrower;
                    return loanApply;
                },
                new { SalesmanId = salesmanId }, sqltran).ToList();
        }

        public List<LoanApply> MonthlyVolume()
        {
            return
            SqlConnections.GetOpenConnection().Query<LoanApply>(@"
            select SUM(TotalAmountStage) as TotalAmountStage,convert(varchar(7),AuditTime,120) AS syear, Month(AuditTime) AS nmonth from LoanApply
 where IsValid= 1 and (RepaymentStatus = 5 or RepaymentStatus = 6) group by convert(varchar(7),AuditTime,120),Month(AuditTime)").ToList();
        }

        public List<LoanApply> MonthlyTransactions()
        {
            return
            SqlConnections.GetOpenConnection().Query<LoanApply>(@"
            select COUNT(*) as count,convert(varchar(7),AuditTime,120) AS syear, Month(AuditTime) AS nmonth from LoanApply
 where IsValid= 1 and (RepaymentStatus = 5 or RepaymentStatus = 6) group by convert(varchar(7),AuditTime,120),Month(AuditTime)").ToList();
        }

        public List<LoanApply> DailyTurnover()
        {
            return
            SqlConnections.GetOpenConnection().Query<LoanApply>(@"
            SELECT day(AuditTime) as day, COUNT(*) as count FROM LoanApply WHERE IsValid = 1 AND (RepaymentStatus = 5 or RepaymentStatus = 6)
 AND convert(varchar(7),AuditTime ,120) = convert(varchar(7),GETDATE(),120) GROUP BY day(AuditTime)").ToList();
        }
    }
}
