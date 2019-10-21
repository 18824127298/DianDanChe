using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using Sigbit.Common;
using Sigbit.Data;
using Sigbit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class salesman_borrower_lending_list_before_lending_list : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Action"] != null)
        {
            Response.ContentType = "text/plain";
            string Action = Request.QueryString["Action"];
            switch (Action)
            {
                case "chedan":
                    Response.Write(chedan(Request.QueryString["Id"], Request.QueryString["CancellationRemark"]));
                    break;
            }
            Response.End();
        }
        //========== 0. 绑定DataGrid和PageControl的关系 ===========
        gridViewPager.GridViewControl = gvList;
        gridViewPager.PageIndexChanged
            = new module_WebUserControl.PageIndexChangedHandler(gvList__PageIndexChanged);

        //========= 1. 如果首次进来，则取出前次的状态 ===========
        if (!IsPostBack)
        {
            CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                       = @"select l.Auditor, l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName,
l.TotalAmountStage, l.Deadline, l.CustomerClassification, l.AuditTime,l.Id from LoanApply l join IdCardInformation i on l.BorrowerId= i.BorrowerId join Borrower b on b.Id = l.SalesmanId
 where l.IsValid= 1 and l.RepaymentStatus = 5 and i.IsValid= 1 and (l.IsLending is null or l.IsLending = 0)";

            if (CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql == "")
            {
            }
            else
            {
                gvList.PageSize = CurrentPageStatus.DataViewStatus.PageSize;
                gvList.PageIndex = CurrentPageStatus.DataViewStatus.CurrentPageIndex;
            }


            FetchDataFromDB();
            gridViewPager.ShowPageInfo();

            NaviTabController.ShowTabBar();
        }
    }

    private void gvList__PageIndexChanged(int nNewPageIndex)
    {
        gvList.PageIndex = nNewPageIndex;
        FetchDataFromDB();
    }

    private void FetchDataFromDB()
    {
        //========= 1. 取出数据，并绑定 ========
        DataSet ds = DataHelper.Instance.ExecuteDataSet
                (CurrentPageStatus.DataViewStatus.SqlBuilder.ToString());


        gvList.DataSource = ds;
        gvList.DataBind();

        //========== 2. 保存当前状态 ==============
        CurrentPageStatus.DataViewStatus.PageSize = gvList.PageSize;
        CurrentPageStatus.DataViewStatus.CurrentPageIndex = gvList.PageIndex;
    }

    protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField(e.SortExpression);
        gridViewPager.RefreshGridView();
    }

    protected void btnClearCondition_Click(object sender, EventArgs e)
    {

    }


    public string chedan(string Id, string CancellationRemark)
    {
        try
        {
            int nId = ConvertUtil.ToInt(Id);
            LoanApplyService loanapplyService = new LoanApplyService();
            LoanApply loanapply = loanapplyService.GetById(nId);
            loanapply.RepaymentStatus = CreditStatus.撤单;
            loanapply.CancellationRemark = CancellationRemark;
            loanapplyService.Update(loanapply);
            return "撤单已完成";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sql = @"select l.Auditor, l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName,
l.TotalAmountStage, l.Deadline, l.CustomerClassification, l.AuditTime,l.Id from LoanApply l join IdCardInformation i on l.BorrowerId= i.BorrowerId join Borrower b on b.Id = l.SalesmanId
 where l.IsValid= 1 and l.RepaymentStatus = 5 and i.IsValid= 1 and (l.IsLending is null or l.IsLending = 0)";
        if (edtCreditName.Text.Trim() != "")
        {
            sql += " and i.Name = " + StringUtil.QuotedToDBStr(edtCreditName.Text);
        }

        if (edtCreditPhone.Text.Trim() != "")
        {
            sql += " and l.CreditPhone = " + StringUtil.QuotedToDBStr(edtCreditPhone.Text);
        }
        CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql = sql;
        FetchDataFromDB();
    }
}