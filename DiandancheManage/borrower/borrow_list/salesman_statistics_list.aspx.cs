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

public partial class borrower_borrow_list_salesman_statistics_list : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Action"] != null)
        {
            Response.ContentType = "text/plain";
            string Action = Request.QueryString["Action"];
            switch (Action)
            {
                case "SalesmanStatisticsList":
                    Response.Write(SalesmanStatisticsList());
                    break;
            }
            Response.End();
        }

        if (!IsPostBack)
        {
        }
    }

    public string SalesmanStatisticsList()
    {
        string sql = @"select b.FullName,
       sum(case when datepart(mm,AuditTime) = 1  then 1 else 0 end) January,
       sum(case when datepart(mm,AuditTime) = 2  then 1 else 0 end) February,
       sum(case when datepart(mm,AuditTime) = 3  then 1 else 0 end) March,
       sum(case when datepart(mm,AuditTime) = 4  then 1 else 0 end) April,
       sum(case when datepart(mm,AuditTime) = 5  then 1 else 0 end) May,
       sum(case when datepart(mm,AuditTime) = 6  then 1 else 0 end) June,
       sum(case when datepart(mm,AuditTime) = 7  then 1 else 0 end) July,
       sum(case when datepart(mm,AuditTime) = 8  then 1 else 0 end) August,
       sum(case when datepart(mm,AuditTime) = 9  then 1 else 0 end) September,
       sum(case when datepart(mm,AuditTime) = 10 then 1 else 0 end) October,
       sum(case when datepart(mm,AuditTime) = 11 then 1 else 0 end) November,
       sum(case when datepart(mm,AuditTime) = 12 then 1 else 0 end) December
from LoanApply l join Borrower b on l.SalesmanId = b.Id 
where year(AuditTime) = '2019' and l.IsValid= 1 and (RepaymentStatus = 5 or RepaymentStatus = 6)
group by b.FullName";

        DataSet ds = DataHelper.Instance.ExecuteDataSet(sql);

        return DataTableJson(ds.Tables[0]);
    }


    public string DataTableJson(DataTable dt)
    {
        return JsonConvert.SerializeObject(dt);
    }
}