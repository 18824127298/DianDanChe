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

public partial class borrower_borrow_list_expect_collected_list : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Action"] != null)
        {
            Response.ContentType = "text/plain";
            string Action = Request.QueryString["Action"];
            switch (Action)
            {
                case "ExpectCollectedList":
                    Response.Write(ExpectCollectedList());
                    break;
            }
            Response.End();
        }

        if (!IsPostBack)
        {
        }
    }

    public string ExpectCollectedList()
    {
        string sql = @"select table1.*, isnull(table2.ThAmount,0) as ThAmount from (select CONVERT(varchar(7), RepaymentDate,120) as RepaymentDate, SUM(UnPrincipal) as UnPrincipal,
SUM(UnTotalInterest) as UnTotalInterest , SUM(UnTotalInterest + UnPrincipal) as SumAmount 
from Borrow where IsValid= 1 group by CONVERT(varchar(7), RepaymentDate,120)) as table1 left join 
(select SUM(f.Amount) as ThAmount, CONVERT(varchar(7), f.CreateTime ,120) as RepaymentDate from FundsFlow f join LoanApply l on f.LoanApplyId = l.Id where f.IsValid= 1 and l.IsValid= 1
and l.RepaymentPlanMode = 2 and (f.Remark = '提还' or f.FeeType = 30) group by CONVERT(varchar(7), f.CreateTime ,120) )
as table2 on table1.RepaymentDate = table2.RepaymentDate  order by CONVERT(varchar(7), table1.RepaymentDate,120) ";

        DataSet ds = DataHelper.Instance.ExecuteDataSet(sql);

        return DataTableJson(ds.Tables[0]);
    }


    public string DataTableJson(DataTable dt)
    { 
        return JsonConvert.SerializeObject(dt);
    }
}