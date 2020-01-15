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

public partial class loan_loan_list_otherfail_loans_listaspx : SbtPageBase
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
                      = @"select l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName,
         l.BicyclesRent, l.Deadline, l.CustomerClassification, l.AuditTime,l.Id, l.Auditor, l.ElectricCore, l.Remark, l.LoanType from LoanApply l join IdCardInformation i on l.BorrowerId= i.BorrowerId join Borrower b on b.Id = l.SalesmanId
 where l.IsValid= 1 and l.RepaymentStatus =3 and i.IsValid= 1 and l.IsMarchantAgree = 0";

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
                CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField("CreateTime");
            }
            else
            {
                gvList.PageSize = CurrentPageStatus.DataViewStatus.PageSize;
                gvList.PageIndex = CurrentPageStatus.DataViewStatus.CurrentPageIndex;
            }

            CurrentPageStatus.DataViewStatus.SqlBuilder.ClearFixConditions();

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

        PageParameter.SetCustomParamObject("fail_loans_list", ds);
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sql = @"select l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName,
         l.BicyclesRent, l.Deadline, l.CustomerClassification, l.AuditTime,l.Id, l.Auditor, l.ElectricCore, l.Remark,l.LoanType from LoanApply l join IdCardInformation i on l.BorrowerId= i.BorrowerId join Borrower b on b.Id = l.SalesmanId
 where l.IsValid= 1 and l.RepaymentStatus =3 and i.IsValid= 1 and l.IsMarchantAgree = 0";
        if (edtCreditName.Text.Trim() != "") 
        {
            sql += " and i.Name = " + StringUtil.QuotedToDBStr(edtCreditName.Text);
        }

        if (edtCreditPhone.Text.Trim() != "")
        {
            sql += " and l.CreditPhone = " + StringUtil.QuotedToDBStr(edtCreditPhone.Text);
        }
        if (edtFullName.Text.Trim() != "")
        {
            sql += " and b.FullName = " + StringUtil.QuotedToDBStr(edtFullName.Text);
        }
        if (edtRecruitmentName.Text.Trim() != "")
        {
            sql += " and l.RecruitmentName like '%" + edtRecruitmentName.Text + "%'";
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
    protected string VIVNavigateUrl(object oId, object oLoanType)
    {
        int nId = ConvertUtil.ToInt(oId);
        return "second_loan_audit.aspx?id=" + nId.ToString();
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        DataSet ds = PageParameter.GetCustomParamObject("fail_loans_list") as DataSet;

        string sTemplateFile = MapPath("../excel_template/拒绝清单列表.xls");

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

        string sFileName = "拒绝清单列表" + DateTime.Now.ToString("yyyyMMddHHmms") + ".xls";
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
        dt.Columns.Add("Name");
        dt.Columns.Add("FullName");
        dt.Columns.Add("BusinessName");
        dt.Columns.Add("RecruitmentName");
        dt.Columns.Add("Auditor");
        dt.Columns.Add("AuditTime");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataRow dRow = ds.Tables[0].Rows[i];

            dt.Rows.Add(
            dRow["Name"].ToString(),
            dRow["FullName"].ToString(),
            dRow["BusinessName"].ToString(),
            dRow["RecruitmentName"].ToString(),
            dRow["Auditor"].ToString(),
            dRow["AuditTime"].ToString()
            );
        }

        dsRet.Tables.Add(dt);


        return dsRet;

    }
}