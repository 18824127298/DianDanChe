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

public partial class salesman_salesman_list_salesman_achievement_list : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //========== 0. 绑定DataGrid和PageControl的关系 ===========
        gridViewPager.GridViewControl = gvList;
        gridViewPager.PageIndexChanged
            = new module_WebUserControl.PageIndexChangedHandler(gvList__PageIndexChanged);


        lblAllCount.Text = "0";
        lblHkz.Text = "0";
        lblHkzwc.Text = "0";
        lblWtg.Text = "0";

        //========= 1. 如果首次进来，则取出前次的状态 ===========
        if (!IsPostBack)
        {
            string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
            ViewState["rec_key"] = sRecordPrimaryKey;

            //========== 3. 新增的判断和处理 ==========
            if (sRecordPrimaryKey != "")
            {
                PageParameter.SetCustomParamObject("RecordPrimaryKey", sRecordPrimaryKey);
            }


            CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                      = @"select l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName,
         l.TotalAmountStage, l.Deadline, l.CustomerClassification, l.AuditTime,l.Id,l.RepaymentStatus from LoanApply l join IdCardInformation i on l.BorrowerId= i.BorrowerId join Borrower b on b.Id = l.SalesmanId
 where l.IsValid= 1 and (l.RepaymentStatus = 5 or l.RepaymentStatus = 6 or l.RepaymentStatus = 3) and i.IsValid= 1";

            if (sRecordPrimaryKey != "")
            {
                CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql += " and l.SalesmanId = " + ConvertUtil.ToInt(sRecordPrimaryKey);
            }
            else if (ConvertUtil.ToInt(CurrentUser.ThirdPartyCode) > 0)
            {
                PageParameter.SetCustomParamObject("RecordPrimaryKey", CurrentUser.ThirdPartyCode);
                BorrowerService borrowerService = new BorrowerService();
                Borrower borrower = borrowerService.GetById(ConvertUtil.ToInt(CurrentUser.ThirdPartyCode));
                if (borrower.IsSalesman.Value)
                {
                    CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql += " and l.SalesmanId = " + borrower.Id;
                }
            }
            if (CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql == "")
            {
                CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField("CreateTime");
                CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField("CreateTime");
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

        //============ 3. 搜索条件的描述 ==========
        SQLBuilder currentSQLBuilder = CurrentPageStatus.DataViewStatus.SqlBuilder;
        if (currentSQLBuilder.GetConditionCount() != 0)
        {
            divSearchCondition.Visible = true;
            lblConditionDesc.Text = currentSQLBuilder.GetConditionDescription();
        }
        else
            divSearchCondition.Visible = false;
    }

    protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField(e.SortExpression);
        gridViewPager.RefreshGridView();
    }

    protected void btnClearCondition_Click(object sender, EventArgs e)
    {

    }
    protected string VIVOper(object oId)
    {
        string sRet = "<a href=\"../../loan/loan_list/borrower_borrow_list.aspx?Id=" + oId.ToString() + "\"><image src='../../images/menu_icon/erp.gif' title='查看' /></a>";

        return sRet;
    }

    protected string VIVRepaymentStatus(object oRepaymentStatus)
    {
        int nRepaymentStatus = ConvertUtil.ToInt(oRepaymentStatus);
        lblAllCount.Text = (ConvertUtil.ToInt(lblAllCount.Text) + 1).ToString();
        if ((CreditStatus)nRepaymentStatus == CreditStatus.还款中)
        {
            lblHkz.Text = (ConvertUtil.ToInt(lblHkz.Text) + 1).ToString();
            return "支付中"; 
        }
        else if ((CreditStatus)nRepaymentStatus == CreditStatus.还款完成)
            lblHkzwc.Text = (ConvertUtil.ToInt(lblHkzwc.Text) + 1).ToString();
        else if ((CreditStatus)nRepaymentStatus == CreditStatus.审核未通过)
            lblWtg.Text = (ConvertUtil.ToInt(lblWtg.Text) + 1).ToString();

        return ((CreditStatus)nRepaymentStatus).ToString();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sql = @"select l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName,
         l.TotalAmountStage, l.Deadline, l.CustomerClassification, l.AuditTime,l.Id,l.RepaymentStatus from LoanApply l join IdCardInformation i on l.BorrowerId= i.BorrowerId join Borrower b on b.Id = l.SalesmanId
 where l.IsValid= 1 and (l.RepaymentStatus = 5 or l.RepaymentStatus = 6 or l.RepaymentStatus = 3) and i.IsValid= 1 ";
        if (edtCreditName.Text.Trim() != "")
        {
            sql += " and i.Name = " + StringUtil.QuotedToDBStr(edtCreditName.Text);
        }

        if (edtCreditPhone.Text.Trim() != "")
        {
            sql += " and l.CreditPhone = " + StringUtil.QuotedToDBStr(edtCreditPhone.Text);
        }
        if (dpApplyFromTime.DateTimeString != "")
        {
            sql += " and l.AuditTime >= " + StringUtil.QuotedToDBStr(DateTimeUtil.BeginTimeOfDay(dpApplyFromTime.DateTimeString));
        }

        if (dpApplyToTime.DateTimeString != "")
        {
            sql += " and l.AuditTime <= " + StringUtil.QuotedToDBStr(DateTimeUtil.EndTimeOfDay(dpApplyToTime.DateTimeString));
        }
        if (ddlRepaymentStatus.SelectedValue != "all")
        {
            sql += " and l.RepaymentStatus = " + ConvertUtil.ToInt(ddlRepaymentStatus.SelectedValue);
        }

        string sRecordPrimaryKey = PageParameter.GetCustomParamObject("RecordPrimaryKey").ToString();
        if (sRecordPrimaryKey != "")
        {
            sql += " and l.SalesmanId = " + ConvertUtil.ToInt(sRecordPrimaryKey);
        }
        else if (ConvertUtil.ToInt(CurrentUser.ThirdPartyCode) > 0)
        {
            BorrowerService borrowerService = new BorrowerService();
            Borrower borrower = borrowerService.GetById(ConvertUtil.ToInt(CurrentUser.ThirdPartyCode));
            if (borrower.IsSalesman.Value)
            {
                sql += " and l.SalesmanId = " + borrower.Id;
            }
        }
        CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql = sql;

        FetchDataFromDB();
    }


}