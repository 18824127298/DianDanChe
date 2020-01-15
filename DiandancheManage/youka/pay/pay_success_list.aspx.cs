using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Sigbit.Common;
using Sigbit.Data;
using Sigbit.Framework;
using Sigbit.Web.WebControlUtil;
using Sigbit.Web.MediaServer;
using System.IO;
using Sigbit.Common.WordProcess.Excel;
using CheDaiBaoWeChatModel;

public partial class youka_pay_pay_success_list : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //divSearchCondition.Visible = false;

        //========== 0. 绑定DataGrid和PageControl的关系 ===========
        gridViewPager.GridViewControl = gvList;
        gridViewPager.PageIndexChanged
            = new module_WebUserControl.PageIndexChangedHandler(gvList__PageIndexChanged);



        //========= 1. 如果首次进来，则取出前次的状态 ===========
        if (!IsPostBack)
        {


            string sBeginTime = CurrentPageStatus.DataViewStatus.SqlBuilder.GetConditionValueString("PayTime", SQLConditionOperator.GreaterEqualThan);

            string sEndTime = CurrentPageStatus.DataViewStatus.SqlBuilder.GetConditionValueString("PayTime", SQLConditionOperator.LessEqualThan);

            DatePickerFrom.DateTimeString = sBeginTime;
            DatePickerTo.DateTimeString = sEndTime;

            object oIsAudit = CurrentPageStatus.DataViewStatus.SqlBuilder.GetConditionValue("IsAudit");

            if (oIsAudit == null)
            {
                ckbIsAudit.Checked = true;
            }


            if (CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql == "")
            {
                CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                        = @"select m.Phone, g.Name, pf.* from PaymentForm pf join GasStation g on pf.GasStationId = g.Id join Member m on m.Id= pf.MemberId where pf.IsValid=1";

                CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField("PayTime");
                CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField("PayTime");
            }
            else
            {
                gvList.PageSize = CurrentPageStatus.DataViewStatus.PageSize;
                gvList.PageIndex = CurrentPageStatus.DataViewStatus.CurrentPageIndex;
            }

            CurrentPageStatus.DataViewStatus.SqlBuilder.ClearFixConditions();


            FetchDataFromDB();
            gridViewPager.ShowPageInfo();
        }


    }

    private void gvList__PageIndexChanged(int nNewPageIndex)
    {
        gvList.PageIndex = nNewPageIndex;
        FetchDataFromDB();
    }

    private void FetchDataFromDB()
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.ClearConditions();



        if (DatePickerFrom.DateString.Trim() != "")
            CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition("PayTime",
                "开始时间", DatePickerFrom.DateString, SQLConditionOperator.GreaterEqualThan);
        if (DatePickerTo.DateString.Trim() != "")
            CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition("PayTime",
                "结束时间", DatePickerTo.DateString, SQLConditionOperator.LessEqualThan);
        if (Phone.Text.Trim() != "")
            CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition("m.Phone",
                "手机号", Phone.Text.Trim(), SQLConditionOperator.Like);


        if (ckbIsAudit.Checked)
        {
            CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition("IsAudit",
                "支付成功", 1, SQLConditionOperator.Equal, "支付成功");
        }

        if (GasStationName.Text != "")
        {
            CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition("Name",
                "结束时间", GasStationName.Text, SQLConditionOperator.Like);
        }

        //========= 1. 取出数据，并绑定 ========

        string sSQL = CurrentPageStatus.DataViewStatus.SqlBuilder.ToString();

        DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

        PageParameter.SetCustomParamObject("recharge_online_xxx3", ds);
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

        //=========== 4.汇总信息 ================
        int nRecordCnt = ds.Tables[0].Rows.Count;

        double fSummaryOfAmount = 0;

        double fSummaryOfFee = 0;

        int nSuccRecordCnt = 0;

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataRow dRow = ds.Tables[0].Rows[i];

            int nIsAudit = ConvertUtil.ToInt(dRow["IsAudit"]);

            if (nIsAudit == 1)
            {
                double fAmount = ConvertUtil.ToFloat(dRow["ActualAmount"]);
                fSummaryOfAmount += fAmount;

                double fActualRechargeFee = ConvertUtil.ToFloat(dRow["ServiceFee"]);
                fSummaryOfFee += fActualRechargeFee;

                nSuccRecordCnt++;
            }

        }

        lblSummaryInfo.Text = string.Format("累计充值笔数:<b>{0}</b>笔:其中成功<b>{1}</b>笔,失败<b>{2}</b>笔;累计充值金额:共<b>{3}</b>元，手续费：<b>{4}</b>元；",
            nRecordCnt, nSuccRecordCnt, nRecordCnt - nSuccRecordCnt, fSummaryOfAmount.ToString("0.00"), fSummaryOfFee.ToString("0.00"));

    }

    protected string VIVamount(object oAmount)
    {
        return Convert.ToDecimal(oAmount).ToString("#0.00");
    }

    protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField(e.SortExpression);
        gridViewPager.RefreshGridView();
    }

    protected string VIVRechargemode(object oRechargeMode)
    {
        int nRechargeMode = ConvertUtil.ToInt(oRechargeMode);

        return ((RechargeMode)nRechargeMode).ToString();
    }

    protected string VIVIsAudit(object oIsAudit, object oCreateTime, object oId)
    {

        if (!string.IsNullOrEmpty(oIsAudit.ToString()))
        {
            bool bIsAudit = ConvertUtil.ToBool(oIsAudit);

            if (bIsAudit)
            {
                return "支付成功";
            }
            else
            {
                return "支付失败";
            }
        }
        else
        {
            DateTime dtCreateTime = DateTimeUtil.ToDateTime(oCreateTime.ToString());

            if (dtCreateTime.AddMinutes(1) < DateTime.Now)
            {
                return "<span class=\"bkcRed\">未完成</span>";
            }
            else
            {
                return "<span class=\"bkcYellow\">支付中</span>";
            }

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FetchDataFromDB();
        gridViewPager.ShowPageInfo();
    }

    protected void btnClearCondition_Click(object sender, EventArgs e)
    {
        ckbIsAudit.Checked = true;

        DatePickerFrom.DateTimeString = "";
        DatePickerTo.DateTimeString = "";
        Phone.Text = "";

        

        CurrentPageStatus.DataViewStatus.SqlBuilder.ClearConditions();
        gridViewPager.RefreshGridView();
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        DataSet ds = PageParameter.GetCustomParamObject("recharge_online_xxx3") as DataSet;

        string sTemplateFile = MapPath("../excel_template/所有客户线上支付.xls");

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

        string sFileName = "所有客户线上支付" + DateTime.Now.ToString("yyyyMMddHHmms") + ".xls";
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
        dt.Columns.Add("Phone");
        dt.Columns.Add("PayTime");
        dt.Columns.Add("GasStationAmount");
        dt.Columns.Add("ActualAmount");
        dt.Columns.Add("SupplierAmount");
        dt.Columns.Add("ServiceFee");
        dt.Columns.Add("RiseNumber");
        dt.Columns.Add("Name");
        dt.Columns.Add("OrderNumber");
        dt.Columns.Add("IsAudit");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataRow dRow = ds.Tables[0].Rows[i];

            dt.Rows.Add(dRow["Phone"].ToString(),
                dRow["PayTime"].ToString(),
                VIVamount(dRow["GasStationAmount"].ToString()),
                VIVamount(dRow["ActualAmount"].ToString()),
                VIVamount(dRow["SupplierAmount"].ToString()),
                VIVamount(dRow["ServiceFee"].ToString()),
                dRow["RiseNumber"].ToString(),
                dRow["Name"].ToString(),
                dRow["OrderNumber"].ToString(),
                VIVIsAudit(dRow["IsAudit"].ToString(), dRow["CreateTime"].ToString(), dRow["Id"].ToString())
            );
        }

        dsRet.Tables.Add(dt);


        return dsRet;
    }
}
