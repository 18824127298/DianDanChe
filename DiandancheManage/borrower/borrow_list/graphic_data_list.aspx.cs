using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using Newtonsoft.Json;
using Sigbit.Data;
using Sigbit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class borrower_borrow_list_graphic_data_list : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Action"] != null)
        {
            Response.ContentType = "text/plain";
            string Action = Request.QueryString["Action"];
            switch (Action)
            {
                case "Turnover":
                    Response.Write(Turnover());
                    break;
                case "Transactions":
                    Response.Write(Transactions());
                    break;
                case "DailyTurnover":
                    Response.Write(DailyTurnover());
                    break;
                case "MonthlyRepayment":
                    Response.Write(MonthlyRepayment());
                    break;
                case "CurrentMonthNumber":
                    Response.Write(CurrentMonthNumber());
                    break;
            }
            Response.End();
        }
        if (!IsPostBack)
        {
        }
    }

    public string Turnover()
    {
        LoanApplyService loanApplyService = new LoanApplyService();
        List<LoanApply> MonthlyVolumeList = loanApplyService.MonthlyVolume();
        Int32[] lists19 = new Int32[12];
        foreach (LoanApply loanapply in MonthlyVolumeList)
        {
            if (loanapply.syear.Contains("2019"))
                lists19[loanapply.nmonth.Value - 1] = Convert.ToInt32((loanapply.TotalAmountStage.Value));
        }
        Dictionary<string, Int32[]> lt = new Dictionary<string, Int32[]>();
        lt.Add("lists19", lists19);

        string imfo = JsonConvert.SerializeObject(lt).ToString();
        return imfo;
    }


    public string Transactions()
    {
        LoanApplyService loanApplyService = new LoanApplyService();
        List<LoanApply> MonthlyTransactionsList = loanApplyService.MonthlyTransactions();
        Int32[] lists19 = new Int32[12];
        foreach (LoanApply loanapply in MonthlyTransactionsList)
        {
            if (loanapply.syear.Contains("2019"))
                lists19[loanapply.nmonth.Value - 1] = loanapply.count.Value;
        }
        Dictionary<string, Int32[]> lt = new Dictionary<string, Int32[]>();
        lt.Add("lists19", lists19); 

        string imfo = JsonConvert.SerializeObject(lt).ToString();
        return imfo;
    }


    public string DailyTurnover()
    {
        LoanApplyService loanApplyService = new LoanApplyService();
        List<LoanApply> DailyTurnoverList = loanApplyService.DailyTurnover();
        DateTime dtmonth = DateTime.Now;
        Int32[] lists19 = new Int32[dtmonth.Day];
        foreach (LoanApply loanapply in DailyTurnoverList)
        {
            lists19[loanapply.day.Value - 1] = loanapply.count.Value;
        }
        Dictionary<string, Int32[]> lt = new Dictionary<string, Int32[]>();
        lt.Add("lists19", lists19);

        string imfo = JsonConvert.SerializeObject(lt).ToString();
        return imfo;
    }

    public string MonthlyRepayment()
    {
        RechargeService rechargeService = new RechargeService();
        List<Recharge> MonthlyRepaymentList = rechargeService.MonthlyRepayment();
        Int32[] lists19 = new Int32[12];
        foreach (Recharge recharge in MonthlyRepaymentList)
        {
            if (recharge.syear.Contains("2019"))
                lists19[recharge.nmonth.Value - 1] = Convert.ToInt32((recharge.Amount.Value / 100));
        }
        Dictionary<string, Int32[]> lt = new Dictionary<string, Int32[]>();
        lt.Add("lists19", lists19);

        string imfo = JsonConvert.SerializeObject(lt).ToString();
        return imfo;
    }

    public string CurrentMonthNumber()
    {
        string sql = @"select COUNT(*) as value,b.FullName as name  from LoanApply l join Borrower b on l.SalesmanId = b.Id where l.IsValid= 1 
and (l.RepaymentStatus = 5 or l.RepaymentStatus = 6) and convert(varchar(7),AuditTime ,120) = convert(varchar(7),GETDATE(),120)
  group by  b.FullName order by value desc";

        DataSet ds = DataHelper.Instance.ExecuteDataSet(sql);
        string[] namelists = new string[ds.Tables[0].Rows.Count];

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            namelists[i] = ds.Tables[0].Rows[i]["name"].ToString();
        }
        Dictionary<string, string> lt = new Dictionary<string, string>();
        lt.Add("namelists", string.Join(",", namelists));
        lt.Add("jolists", JsonConvert.SerializeObject(ds.Tables[0]));

        string imfo = JsonConvert.SerializeObject(lt).ToString();
        return imfo;
    }
}