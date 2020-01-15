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

public partial class loan_loan_list_loan_bill : SbtPageBase
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
            dpFromTime.DateTimeString = DateTime.Now.ToString("yyyy-MM-dd");
            dpToTime.DateTimeString = DateTime.Now.ToString("yyyy-MM-dd");

            CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                       = @"select b.UnPrincipal, b.UnTotalInterest, br.FullName, l.SalesmanId, br.Phone,
                   l.Brand, b.Stages, b.TotalPeriod, l.CreditAmount,br.Id, l.MonthlyPayment,b.RepaymentDate
                  , (b.UnPrincipal+b.UnTotalInterest) as unSumAmount from Borrow b join Borrower br on b.BorrowerId = br.Id join LoanApply l 
                  on b.LoanApplyId= l.Id where b.IsValid = 1 and b.UnPrincipal>0
                  and RepaymentDate >= " + StringUtil.QuotedToDBStr(dpFromTime.DateTimeString) +
                   " and RepaymentDate <=" + StringUtil.QuotedToDBStr(dpToTime.DateTimeString);


                gvList.PageSize = CurrentPageStatus.DataViewStatus.PageSize;
                gvList.PageIndex = CurrentPageStatus.DataViewStatus.CurrentPageIndex;

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
        //========= 1. 取出数据，并绑定 ========
        DataSet ds = DataHelper.Instance.ExecuteDataSet
                (CurrentPageStatus.DataViewStatus.SqlBuilder.ToString());


        gvList.DataSource = ds;
        gvList.DataBind();
        PageParameter.SetCustomParamObject("loan_bill", ds);

        //========== 2. 保存当前状态 ==============
        CurrentPageStatus.DataViewStatus.PageSize = gvList.PageSize;
        CurrentPageStatus.DataViewStatus.CurrentPageIndex = gvList.PageIndex;
    }

    protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField(e.SortExpression);
        gridViewPager.RefreshGridView();
    }

    public string VIVSalesmanName(object SalesmanId)
    {
        BorrowerService borrowerService = new BorrowerService();
        return borrowerService.GetById(Convert.ToInt32(SalesmanId)).FullName;
    }

    protected string VIVHTime(object oTime)
    {

        if (string.IsNullOrEmpty(oTime.ToString()))
        {
            return "";
        }
        else
        {
            return DateTimeUtil.ToDateStr(oTime);
        }
    }


    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        DataSet ds = PageParameter.GetCustomParamObject("loan_bill") as DataSet;

        string sTemplateFile = MapPath("../excel_template/未还款.xls");

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

        string sFileName = "未还款" + DateTime.Now.ToString("yyyyMMddHHmms") + ".xls";
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
        dt.Columns.Add("FullName");
        dt.Columns.Add("Phone");
        dt.Columns.Add("SalesmanId");
        dt.Columns.Add("unSumAmount");
        dt.Columns.Add("Stages");
        dt.Columns.Add("TotalPeriod");
        dt.Columns.Add("RepaymentDate");
        dt.Columns.Add("CreditAmount");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataRow dRow = ds.Tables[0].Rows[i];

            dt.Rows.Add(dRow["FullName"].ToString(),
            dRow["Phone"].ToString(),
            VIVSalesmanName(dRow["SalesmanId"]),
            dRow["unSumAmount"].ToString(),
            dRow["Stages"].ToString(),
            dRow["TotalPeriod"].ToString(),
            VIVRepaymentDate(dRow["RepaymentDate"].ToString()),
            dRow["CreditAmount"].ToString()
            );
        }

        dsRet.Tables.Add(dt);


        return dsRet;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                     = @"select b.UnPrincipal, b.UnTotalInterest, br.FullName, l.SalesmanId, br.Phone,
                   l.Brand, b.Stages, b.TotalPeriod, l.CreditAmount,br.Id, l.MonthlyPayment,b.RepaymentDate
                  , (b.UnPrincipal+b.UnTotalInterest) as unSumAmount from Borrow b join Borrower br on b.BorrowerId = br.Id join LoanApply l 
                  on b.LoanApplyId= l.Id where b.IsValid = 1 and b.UnPrincipal>0
                  and RepaymentDate >= " + StringUtil.QuotedToDBStr(dpFromTime.DateTimeString) +
                   " and RepaymentDate <=" + StringUtil.QuotedToDBStr(dpToTime.DateTimeString);
            gvList.PageSize = CurrentPageStatus.DataViewStatus.PageSize;
            gvList.PageIndex = CurrentPageStatus.DataViewStatus.CurrentPageIndex;

        CurrentPageStatus.DataViewStatus.SqlBuilder.ClearFixConditions();

        FetchDataFromDB();
        gridViewPager.ShowPageInfo();

        NaviTabController.ShowTabBar();
    }

    protected string VIVRepaymentDate(object oDateTime)
    {
        return Convert.ToDateTime(oDateTime).ToString("yyyy-MM-dd");
    }
}