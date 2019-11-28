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

public partial class loan_loan_list_all_loan_list : SbtPageBase
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
                      = @"select l.Auditor, l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName, l.MonthlyPayment, l.DownPayments,
l.TotalAmountStage, l.Deadline, l.CustomerClassification, l.AuditTime,l.Id from LoanApply l 
join IdCardInformation i on l.BorrowerId= i.BorrowerId join Borrower b on b.Id = l.SalesmanId where l.IsValid= 1 and i.IsValid= 1";

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
        CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField("l.CreateTime");
        CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField("l.CreateTime");
        //========= 1. 取出数据，并绑定 ========
        DataSet ds = DataHelper.Instance.ExecuteDataSet
                (CurrentPageStatus.DataViewStatus.SqlBuilder.ToString());

        PageParameter.SetCustomParamObject("all_loan_list", ds);
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


        int nRecordCnt = ds.Tables[0].Rows.Count;

        lblSummaryInfo.Text = string.Format("总数:<b>{0}</b>笔;", nRecordCnt);

        string sql = "select SUM(UnPrincipal) from Borrow where IsValid= 1";

        decimal sSumUnPrincipal = ConvertUtil.ToDecimal(DataHelper.Instance.ExecuteScalar(sql));

        lblUnPrincipal.Text = string.Format("未还总金额:<b>{0}</b>元", sSumUnPrincipal.ToString("N0"));
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
        string sRet = "<a href=\"../loan_list/borrower_borrow_list.aspx?Id=" + oId.ToString() + "\"><image src='../../images/menu_icon/erp.gif' title='查看' /></a>";

        return sRet;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sql = @"select l.Auditor, l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName,l.MonthlyPayment, l.DownPayments,
l.TotalAmountStage, l.Deadline, l.CustomerClassification, l.AuditTime,l.Id from LoanApply l 
join IdCardInformation i on l.BorrowerId= i.BorrowerId join Borrower b on b.Id = l.SalesmanId where l.IsValid= 1 and i.IsValid= 1";
        if (edtRecruitmentName.Text.Trim() != "")
        {
            sql += " and l.RecruitmentName like '%" + edtRecruitmentName.Text + "%'";
        }

        if (edtBusinessName.Text.Trim() != "")
        {
            sql += " and l.BusinessName like '%" + edtBusinessName.Text + "%'";
        }

        if (edtFullName.Text.Trim() != "")
        {
            sql += " and b.FullName = " + StringUtil.QuotedToDBStr(edtFullName.Text);
        }

        if (edtName.Text.Trim() != "")
        {
            sql += " and i.Name = " + StringUtil.QuotedToDBStr(edtName.Text);
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
            if (ddlRepaymentStatus.SelectedValue == "8")
            {
                sql += " and (l.RepaymentStatus = 5 or l.RepaymentStatus = 6)";
            }
            else
            {
                sql += " and l.RepaymentStatus = " + ConvertUtil.ToInt(ddlRepaymentStatus.SelectedValue);
            }
        }


        if (ddlCustomerClassification.SelectedValue != "all")
        {
            sql += " and l.CustomerClassification = " + StringUtil.QuotedToDBStr(ddlCustomerClassification.SelectedValue);
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
        CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql = sql;
        FetchDataFromDB();
    }


    public string VIVinformation(object oId)
    {
        int nId = ConvertUtil.ToInt(oId);
        string sRet = "<a href=\"../loan_list/other_information_list.aspx?Id=" + oId.ToString() + "\"><image src='../../images/menu_icon/erp.gif' title='查看' /></a>";

        return sRet;
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        DataSet ds = PageParameter.GetCustomParamObject("all_loan_list") as DataSet;

        string sTemplateFile = MapPath("../excel_template/融资租赁列表模板.xls");

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

        string sFileName = "融资租赁列表模板" + DateTime.Now.ToString("yyyyMMddHHmms") + ".xls";
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
        dt.Columns.Add("TotalAmountStage");
        dt.Columns.Add("MonthlyPayment");
        dt.Columns.Add("DownPayments");
        dt.Columns.Add("Deadline");
        dt.Columns.Add("CustomerClassification");
        dt.Columns.Add("Auditor");
        dt.Columns.Add("AuditTime");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataRow dRow = ds.Tables[0].Rows[i];

            dt.Rows.Add(dRow["CreditPhone"].ToString(),
                dRow["Name"].ToString(),
                dRow["FullName"].ToString(),
                dRow["BusinessName"].ToString(),
                dRow["RecruitmentName"].ToString(),
                dRow["TotalAmountStage"].ToString(),
                dRow["MonthlyPayment"].ToString(),
                dRow["DownPayments"].ToString(),
                dRow["Deadline"].ToString(),
                dRow["CustomerClassification"].ToString(),
                dRow["Auditor"].ToString(),
                dRow["AuditTime"].ToString()
            );
        }

        dsRet.Tables.Add(dt);


        return dsRet;

    }
}