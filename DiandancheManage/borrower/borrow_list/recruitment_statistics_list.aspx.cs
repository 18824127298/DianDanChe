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

public partial class borrower_borrow_list_recruitment_statistics : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Action"] != null)
        {
            Response.ContentType = "text/plain";
            string Action = Request.QueryString["Action"];
            switch (Action)
            {
                case "RecruitmentStatisticsList":
                    Response.Write(RecruitmentStatisticsList());
                    break;
            }
            Response.End();
        }

        if (!IsPostBack)
        {
        }
    }

    public string RecruitmentStatisticsList()
    {
        string sql = @"select RecruitmentName,
       sum(case when datepart(mm,AuditTime) = 1 and year(AuditTime) = '2020' then 1 else 0 end) January,
       sum(case when datepart(mm,AuditTime) = 2 and year(AuditTime) = '2020' then 1 else 0 end) February,
       sum(case when datepart(mm,AuditTime) = 3 and year(AuditTime) = '2019' then 1 else 0 end) March,
       sum(case when datepart(mm,AuditTime) = 4 and year(AuditTime) = '2019' then 1 else 0 end) April,
       sum(case when datepart(mm,AuditTime) = 5 and year(AuditTime) = '2019' then 1 else 0 end) May,
       sum(case when datepart(mm,AuditTime) = 6 and year(AuditTime) = '2019' then 1 else 0 end) June,
       sum(case when datepart(mm,AuditTime) = 7 and year(AuditTime) = '2020' then 1 else 0 end) July,
       sum(case when datepart(mm,AuditTime) = 8 and year(AuditTime) = '2019' then 1 else 0 end) August,
       sum(case when datepart(mm,AuditTime) = 9 and year(AuditTime) = '2019' then 1 else 0 end) September,
       sum(case when datepart(mm,AuditTime) = 10 and year(AuditTime) = '2019'then 1 else 0 end) October,
       sum(case when datepart(mm,AuditTime) = 11 and year(AuditTime) = '2019'then 1 else 0 end) November,
       sum(case when datepart(mm,AuditTime) = 12 and year(AuditTime) = '2019' then 1 else 0 end) December
from LoanApply 
where IsValid= 1 and (RepaymentStatus = 5 or RepaymentStatus = 6) and RecruitmentName is not null
group by RecruitmentName";

        DataSet ds = DataHelper.Instance.ExecuteDataSet(sql);

        return DataTableJson(ds.Tables[0]);
    }


    public string DataTableJson(DataTable dt)
    {
        return JsonConvert.SerializeObject(dt);
    }
}