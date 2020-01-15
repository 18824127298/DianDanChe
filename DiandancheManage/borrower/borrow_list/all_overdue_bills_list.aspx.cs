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

public partial class borrower_borrow_list_all_overdue_bills_list : SbtPageBase
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
                       = @"select *, UnPrincipal+UnTotalInterest as sumuntotal, br.FullName from Borrow b join LoanApply l 
on b.LoanApplyId = l.Id join Borrower br on b.BorrowerId= br.Id where b.IsValid= 1 and b.OverDay > 0";

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
        //========= 1. 取出数据，并绑定 ========
        DataSet ds = DataHelper.Instance.ExecuteDataSet
                (CurrentPageStatus.DataViewStatus.SqlBuilder.ToString());
        PageParameter.SetCustomParamObject("all_overdue_bills_list", ds);

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



    protected string VIVHTime(object oTime)
    {

        if (string.IsNullOrEmpty(oTime.ToString()))
        {
            return "";
        }
        else
        {
            return Convert.ToDateTime(oTime).ToString("yyyy-MM-dd");
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        DataSet ds = PageParameter.GetCustomParamObject("all_overdue_bills_list") as DataSet;

        string sTemplateFile = MapPath("../excel_template/所有的逾期账单.xls");

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

        string sFileName = "所有的逾期账单" + DateTime.Now.ToString("yyyyMMddHHmms") + ".xls";
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
        dt.Columns.Add("RepaymentDate");
        dt.Columns.Add("Principal");
        dt.Columns.Add("UnPrincipal");
        dt.Columns.Add("Interest");
        dt.Columns.Add("OverInterest");
        dt.Columns.Add("OverDay");
        dt.Columns.Add("UnTotalInterest");
        dt.Columns.Add("sumuntotal");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataRow dRow = ds.Tables[0].Rows[i];

            dt.Rows.Add(dRow["FullName"].ToString(),
                VIVHTime(dRow["RepaymentDate"]),
                dRow["Principal"].ToString(),
                dRow["UnPrincipal"].ToString(),
                dRow["Interest"].ToString(),
                dRow["OverInterest"].ToString(),
                dRow["OverDay"].ToString(),
                dRow["UnTotalInterest"].ToString(),
                dRow["sumuntotal"].ToString()
            );
        }

        dsRet.Tables.Add(dt);


        return dsRet;

    }
}