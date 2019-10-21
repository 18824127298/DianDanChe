using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using Sigbit.Common;
using Sigbit.Common.WordProcess.Excel;
using Sigbit.Data;
using Sigbit.Framework;
using Sigbit.Web.MediaServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class loan_loan_list_post_loan_management_list : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //========== 0. 绑定DataGrid和PageControl的关系 ===========
        gridViewPager.GridViewControl = gvList;
        gridViewPager.PageIndexChanged
            = new module_WebUserControl.PageIndexChangedHandler(gvList__PageIndexChanged);

        //========= 1. 如果首次进来，则取出前次的状态 ===========
        if (!IsPostBack)
        {
            CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                      = @"select l.Auditor, l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName,
         l.TotalAmountStage, l.Deadline, l.CustomerClassification, l.AuditTime,l.Id,l.MonthlyPayment,l.LoanType,l.VisitRecord, l.IsRider, l.IsAnswer, l.IsAbnormal from LoanApply l join IdCardInformation i on l.BorrowerId= i.BorrowerId join Borrower b on b.Id = l.SalesmanId
 where l.IsValid= 1 and l.RepaymentStatus = 5 and i.IsValid= 1";

            if (ConvertUtil.ToInt(CurrentUser.ThirdPartyCode) > 0)
            {
                BorrowerService borrowerService = new BorrowerService();
                Borrower borrower = borrowerService.GetById(ConvertUtil.ToInt(CurrentUser.ThirdPartyCode));
                if (borrower.IsSalesman.Value)
                {
                    CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql += " and l.SalesmanId = " + borrower.Id;
                }
            }
            if (CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql == "")
            {
            }
            else
            {
                gvList.PageSize = CurrentPageStatus.DataViewStatus.PageSize;
                gvList.PageIndex = CurrentPageStatus.DataViewStatus.CurrentPageIndex;
            }

            if (Session["successApplyFromTime"] != null)
            {
                dpApplyFromTime.DateTimeString = Session["successApplyFromTime"].ToString();
                CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql += " and l.AuditTime >= " + StringUtil.QuotedToDBStr(DateTimeUtil.BeginTimeOfDay(dpApplyFromTime.DateTimeString));
            }
            if (Session["successApplyToTime"] != null)
            {
                dpApplyToTime.DateTimeString = Session["successApplyToTime"].ToString();
                CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql += " and l.AuditTime <= " + StringUtil.QuotedToDBStr(DateTimeUtil.EndTimeOfDay(dpApplyToTime.DateTimeString));
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
        CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField("l.CreateTime");
        CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField("l.CreateTime");
        //========= 1. 取出数据，并绑定 ========
        DataSet ds = DataHelper.Instance.ExecuteDataSet
                (CurrentPageStatus.DataViewStatus.SqlBuilder.ToString());

        gvList.DataSource = ds;
        gvList.DataBind();

        PageParameter.SetCustomParamObject("post_loan_management_list", ds);
        //========== 2. 保存当前状态 ==============
        CurrentPageStatus.DataViewStatus.PageSize = gvList.PageSize;
        CurrentPageStatus.DataViewStatus.CurrentPageIndex = gvList.PageIndex;

        ////============ 3. 搜索条件的描述 ==========
        //SQLBuilder currentSQLBuilder = CurrentPageStatus.DataViewStatus.SqlBuilder;
        //if (currentSQLBuilder.GetConditionCount() != 0)
        //{
        //    divSearchCondition.Visible = true;
        //    lblConditionDesc.Text = currentSQLBuilder.GetConditionDescription();
        //}
        //else
        //    divSearchCondition.Visible = false;
    }

    protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField(e.SortExpression);
        gridViewPager.RefreshGridView();
    }

    protected void btnClearCondition_Click(object sender, EventArgs e)
    {
        Session["successApplyFromTime"] = null;
        Session["successApplyToTime"] = null;
        Response.Redirect("../loan_list/success_loans_list.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sql = @"select l.Auditor, l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName,
         l.TotalAmountStage, l.Deadline, l.CustomerClassification, l.AuditTime,l.Id,l.MonthlyPayment, l.LoanType, l.VisitRecord, l.IsRider, l.IsAnswer, l.IsAbnormal from LoanApply l join IdCardInformation i on l.BorrowerId= i.BorrowerId join Borrower b on b.Id = l.SalesmanId
 where l.IsValid= 1 and l.RepaymentStatus = 5 and i.IsValid= 1 ";
        if (edtCreditName.Text.Trim() != "")
        {
            sql += " and i.Name like '%" + edtCreditName.Text + "%'";
        }

        if (edtCreditPhone.Text.Trim() != "")
        {
            sql += " and l.CreditPhone = " + StringUtil.QuotedToDBStr(edtCreditPhone.Text);
        }
        if (dpApplyFromTime.DateTimeString != "")
        {
            sql += " and l.AuditTime >= " + StringUtil.QuotedToDBStr(DateTimeUtil.BeginTimeOfDay(dpApplyFromTime.DateTimeString));
            Session["successApplyFromTime"] = dpApplyFromTime.DateTimeString;
        }

        if (dpApplyToTime.DateTimeString != "")
        {
            sql += " and l.AuditTime <= " + StringUtil.QuotedToDBStr(DateTimeUtil.EndTimeOfDay(dpApplyToTime.DateTimeString));
            Session["successApplyToTime"] = dpApplyToTime.DateTimeString;
        }

        if (ddlCompany.SelectedValue != "All")
        {
            if (ddlCompany.SelectedValue == "0")
            {
                sql += " and (l.Company = 0 or l.Company is null)";
            }
            else
            {
                sql += " and l.Company = " + StringUtil.QuotedToDBStr(ddlCompany.SelectedValue);
            }
        }
        if (ddlIsRider.SelectedValue != "All")
        {
            sql += " and l.IsRider = " + StringUtil.QuotedToDBStr(ddlIsRider.SelectedValue);
        }
        if (ddlIsAnswer.SelectedValue != "All")
        {
            sql += " and l.IsAnswer = " + StringUtil.QuotedToDBStr(ddlIsAnswer.SelectedValue);
        }
        if (ddlIsAbnormal.SelectedValue != "All")
        {
            sql += " and l.IsAbnormal = " + StringUtil.QuotedToDBStr(ddlIsAbnormal.SelectedValue);
        }
        if (ConvertUtil.ToInt(CurrentUser.ThirdPartyCode) > 0)
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


    public string VIVIsRider(object oIsRider)
    {
        if (!string.IsNullOrEmpty(oIsRider.ToString()))
        {
            if (ConvertUtil.ToBool(oIsRider.ToString()) == true)
                return "是";
            else
                return "否";
        }
        else
        {
            return "不清楚";
        }
    }

    protected string VIVIsAnswer(object oIsAnswer)
    {
        if (!string.IsNullOrEmpty(oIsAnswer.ToString()))
        {
            if (ConvertUtil.ToBool(oIsAnswer.ToString()) == true)
                return "是";
            else
                return "否";
        }
        else
        {
            return "不清楚";
        }
    }

    protected string VIVIsAbnormal(object oIsAbnormal)
    {
        if (!string.IsNullOrEmpty(oIsAbnormal.ToString()))
        {
            if (ConvertUtil.ToBool(oIsAbnormal.ToString()) == true)
                return "是";
            else
                return "否";
        }
        else
        {
            return "不清楚";
        }
    }

    protected string VIVNavigateUrl(object oId, object oLoanType)
    {
        int nId = ConvertUtil.ToInt(oId);
        if (ConvertUtil.ToInt(oLoanType) == (int)LoanType.租客)
        {
            return "rent_details.aspx?id=" + nId.ToString();
        }
        else
        {
            return "loan_details.aspx?id=" + nId.ToString();
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        DataSet ds = PageParameter.GetCustomParamObject("post_loan_management_list") as DataSet;

        string sTemplateFile = MapPath("../excel_template/贷后管理列表.xls");

        MediaServerPath mediaExportFile = new MediaServerPath();

        mediaExportFile.RelativePath = "phonelee/cust_recharge/" + DateTime.Now.ToString("yyyyMMdd");

        FileUtil.RemoveFilesBeforeTime(mediaExportFile.FullPath, DateTime.Now.AddMonths(-1));

        Directory.CreateDirectory(mediaExportFile.FullPath);

        string sExportFileName = RandUtil.NewString(10, RandStringType.LowerNumber) + ".xls";

        mediaExportFile.RelativePath += "/" + sExportFileName;

        ExcelExportDataSet export = new ExcelExportDataSet(); ;
        export.InputDataSet = ToExcelDownloadDataSet(ds);
        export.TemplateFile = sTemplateFile;
        export.ExportFileName = mediaExportFile.FullPath;
        export.DoExport();

        string sFileName = "贷后管理列表" + DateTime.Now.ToString("yyyyMMddHHmms") + ".xls";
        Response.Buffer = true;
        Response.Charset = "GB2312";
        Response.ContentType = "application/ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" +
            System.Web.HttpUtility.UrlEncode(sFileName, System.Text.Encoding.UTF8));
        Response.TransmitFile(mediaExportFile.FullPath);
        Response.End();

    }

    protected DataSet ToExcelDownloadDataSet(DataSet ds)
    {
        DataSet dsRet = new DataSet();

        DataTable dt = new DataTable();
        dt.Columns.Add("CreditPhone");
        dt.Columns.Add("Name");
        dt.Columns.Add("FullName");
        dt.Columns.Add("BusinessName");
        dt.Columns.Add("RecruitmentName");
        dt.Columns.Add("Auditor");
        dt.Columns.Add("AuditTime");
        dt.Columns.Add("VisitRecord");
        dt.Columns.Add("IsRider");
        dt.Columns.Add("IsAnswer");
        dt.Columns.Add("IsAbnormal");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataRow dRow = ds.Tables[0].Rows[i];

            dt.Rows.Add(
            dRow["CreditPhone"].ToString(),
            dRow["Name"].ToString(),
            dRow["FullName"].ToString(),
            dRow["BusinessName"].ToString(),
            dRow["RecruitmentName"].ToString(),
            dRow["Auditor"].ToString(),
            dRow["AuditTime"].ToString(),
            dRow["VisitRecord"].ToString(),
            VIVIsRider(dRow["IsRider"]),
            VIVIsAnswer(dRow["IsAnswer"]),
            VIVIsAbnormal(dRow["IsAbnormal"])
            );
        }

        dsRet.Tables.Add(dt);


        return dsRet;
    }
}