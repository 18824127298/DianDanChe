using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using Sigbit.Common;
using Sigbit.Common.WordProcess.Excel;
using Sigbit.Data;
using Sigbit.Framework;
using Sigbit.Web.MediaServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class borrower_borrow_list_overdue_followup_list : SbtPageBase
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
                       = @"select b.BorrowerId, ISNULL(MAX(b.OverDay),0) overday, l.InterestDate, l.MonthlyPayment, br.FullName, br.Phone, l.SalesmanId,l.Id, l.BusinessName, l.RecruitmentName from Borrow b join LoanApply l
 on b.LoanApplyId= l.Id join Borrower br on b.BorrowerId= br.Id where b.IsValid= 1 and ((b.OverDay>0 and UnTotalInterest+ UnPrincipal > 0) or 
 (RepaymentDate=CONVERT(varchar(100),GETDATE(),23) and UnTotalInterest + UnPrincipal >0)) group by b.BorrowerId,l.InterestDate, l.MonthlyPayment, br.FullName, br.Phone, l.SalesmanId, l.Id,l.BusinessName, l.RecruitmentName order by overday desc";

            if (CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql == "")
            {
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


        PageParameter.SetCustomParamObject("overdue_followup_list", ds);
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

    public string VIVSalesmanName(object SalesmanId)
    {
        BorrowerService borrowerService = new BorrowerService();
        return borrowerService.GetById(Convert.ToInt32(SalesmanId)).FullName;
    }

    public string VIVBorrowerId(object BorrowerId)
    {
        BorrowService borrowService = new BorrowService();
        List<Borrow> borrowList = borrowService.Search(new Borrow() { IsValid = true }).Where(o => ((o.OverDay > 0 && o.UnTotalInterest + o.UnPrincipal > 0) || (o.RepaymentDate == DateTime.Now.Date && o.UnTotalInterest + o.UnPrincipal > 0)) && o.BorrowerId == ConvertUtil.ToInt(BorrowerId)).ToList();
        return borrowList.Count.ToString();
    }

    public string VIVStagesCount(object BorrowerId)
    {
        BorrowService borrowService = new BorrowService();
        List<Borrow> borrowList = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.ActualRepaymentDate != null && o.RepaymentPlanMode != null && o.BorrowerId == ConvertUtil.ToInt(BorrowerId)).ToList();
        return borrowList.Count.ToString();
    }

    public string VIVInterestDate(object InterestDate)
    {
        return Convert.ToDateTime(InterestDate.ToString()).ToString("yyyy-MM-dd");
    }

    Hashtable KH = new Hashtable();
    Hashtable QY = new Hashtable();
    Hashtable FK = new Hashtable();
    static FollowUpService followUpService = new FollowUpService();

    static BorrowService borrowService = new BorrowService();

    protected string VIVKH(object oId, object oPhone)
    {
        DateTime dtRepaymentDate = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.LoanApplyId == Convert.ToInt32(oId) && (o.UnPrincipal + o.UnTotalInterest) > 0).OrderBy(o => o.Stages).First().RepaymentDate.Value;
        string sPhone = oPhone.ToString();

        if (KH.Contains(sPhone))
        {
            return "";
        }
        else
        {
            string sKH = "";
            int i = 1;
            List<FollowUp> KHFloowUpList = followUpService.GetAll().FindAll(o => o.CreatorType == CreatorType.客户经理 && o.LoanApplyId == Convert.ToInt32(oId) && o.CreateTime > dtRepaymentDate);
            foreach (FollowUp item in KHFloowUpList)
            {
                sKH += i.ToString() + "：" + item.Remark + "(" + item.CreateTime.Value.ToString("yyyy-MM-dd") + ")\r\n";
                i++;
            }
            KH.Add(sPhone, sPhone);
            return sKH;
        }
    }


    protected string VIVQY(object oId, object oPhone)
    {
        DateTime dtRepaymentDate = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.LoanApplyId == Convert.ToInt32(oId) && (o.UnPrincipal + o.UnTotalInterest) > 0).OrderBy(o => o.Stages).First().RepaymentDate.Value;

        string sPhone = oPhone.ToString();

        if (QY.Contains(sPhone))
        {
            return "";
        }
        else
        {
            string sQY = "";
            int i = 1;
            List<FollowUp> QYFloowUpList = followUpService.GetAll().FindAll(o => o.CreatorType == CreatorType.区域经理 && (o.LoanApplyId == Convert.ToInt32(oId)) && o.CreateTime > dtRepaymentDate);
            foreach (FollowUp item in QYFloowUpList)
            {
                sQY += i.ToString() + "：" + item.Remark + "(" + item.CreateTime.Value.ToString("yyyy-MM-dd") + ")\r\n";
                i++;
            }
            QY.Add(sPhone, sPhone);
            return sQY;
        }
    }

    protected string VIVFK(object oId, object oPhone)
    {
        DateTime dtRepaymentDate = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.LoanApplyId == Convert.ToInt32(oId) && (o.UnPrincipal + o.UnTotalInterest) > 0).OrderBy(o => o.Stages).First().RepaymentDate.Value;

        string sPhone = oPhone.ToString();

        if (FK.Contains(sPhone))
        {
            return "";
        }
        else
        {
            string sFK = "";
            int i = 1;
            List<FollowUp> FKFloowUpList = followUpService.GetAll().FindAll(o => o.CreatorType == CreatorType.风控专员 && (o.LoanApplyId == Convert.ToInt32(oId)) && o.CreateTime > dtRepaymentDate);
            foreach (FollowUp item in FKFloowUpList)
            {
                sFK += i.ToString() + "：" + item.Remark + "(" + item.CreateTime.Value.ToString("yyyy-MM-dd") + ")\r\n";
                i++;
            }
            FK.Add(sPhone, sPhone);
            return sFK;
        }
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

    protected string VIVFollowUp(object oId)
    {
        int nId = ConvertUtil.ToInt(oId);
        string sRet = "<a href=\"../../salesman/salesman_list/borrow_follow_list.aspx?Id=" + oId.ToString() + "\"><image src='../../images/menu_icon/erp.gif' title='查看' /></a>";
        return sRet;
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        DataSet ds = PageParameter.GetCustomParamObject("overdue_followup_list") as DataSet;

        string sTemplateFile = MapPath("../excel_template/跟踪的情况.xls");

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

        string sFileName = "跟踪的情况" + DateTime.Now.ToString("yyyyMMddHHmms") + ".xls";
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
        dt.Columns.Add("RecruitmentName");
        dt.Columns.Add("InterestDate");
        dt.Columns.Add("MonthlyPayment");
        dt.Columns.Add("overday");
        dt.Columns.Add("BorrowerId");
        dt.Columns.Add("Id");
        dt.Columns.Add("Id1");
        dt.Columns.Add("Id2");
        dt.Columns.Add("Id3");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataRow dRow = ds.Tables[0].Rows[i];

            dt.Rows.Add(
            dRow["FullName"].ToString(),
            dRow["Phone"].ToString(),
            VIVSalesmanName(dRow["SalesmanId"]),
            dRow["RecruitmentName"].ToString(),
            VIVInterestDate(dRow["InterestDate"]),
            dRow["MonthlyPayment"].ToString(),
            dRow["overday"].ToString(),
            VIVBorrowerId(dRow["BorrowerId"]),
            VIVStagesCount(dRow["BorrowerId"]),
            VIVKH(dRow["Id"], dRow["Phone"]),
            VIVQY(dRow["Id"], dRow["Phone"]),
            VIVFK(dRow["Id"], dRow["Phone"])
            );
        }

        dsRet.Tables.Add(dt);


        return dsRet;
    }
}