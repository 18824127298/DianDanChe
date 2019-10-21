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

public partial class genui_PAIM_operation_log_browse_list : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //========== 0. ��DataGrid��PageControl�Ĺ�ϵ ===========
        gridViewPager.GridViewControl = gvList;
        gridViewPager.PageIndexChanged
            = new module_WebUserControl.PageIndexChangedHandler(gvList__PageIndexChanged);

        //========= 1. ����״ν�������ȡ��ǰ�ε�״̬ ===========
        if (!IsPostBack)
        {
            if (CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql == "")
            {
                CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                        = "select * from sbt_log_operation_audit";
                CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition
                        ("proc_time", "ʱ��",
                        DateTimeUtil.BeginTimeOfDay(DateTimeUtil.Now),
                        SQLConditionOperator.GreaterEqualThan,
                        "��ʼ����Ϊ" + DateTimeUtil.GetDatePart(DateTimeUtil.Now));
                CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition
                        ("proc_time", "ʱ��",
                        DateTimeUtil.EndTimeOfDay(DateTimeUtil.Now),
                        SQLConditionOperator.LessEqualThan,
                        "��������Ϊ" + DateTimeUtil.GetDatePart(DateTimeUtil.Now));
                CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField("proc_time");
                CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField("proc_time");
            }
            else
            {
                gvList.PageSize = CurrentPageStatus.DataViewStatus.PageSize;
                gvList.PageIndex = CurrentPageStatus.DataViewStatus.CurrentPageIndex;
            }
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
        //========= 1. ȡ�����ݣ����� ========
        DataSet ds = DataHelper.Instance.ExecuteDataSet
                (CurrentPageStatus.DataViewStatus.SqlBuilder.ToString());
        gvList.DataSource = ds;
        gvList.DataBind();

        //========== 2. ���浱ǰ״̬ ==============
        CurrentPageStatus.DataViewStatus.PageSize = gvList.PageSize;
        CurrentPageStatus.DataViewStatus.CurrentPageIndex = gvList.PageIndex;

        //============ 3. �������������� ==========
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

    protected string VIVProcTime(string sProcTime)
    {
        return sProcTime.Substring(5, 11);
    }

    protected string VIVActionDescription(string sActionDescription)
    {
        string sRet = sActionDescription.Replace("\r", "").Replace("\n", " ");
        if (sRet.Length > 64)
            sRet = sRet.Substring(0, 64) + "...";
        return sRet;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        PageParameter.StringParam[0] = "operation_log_browsePAIM";
        PageParameter.ObjParam[0] = CurrentPageStatus.DataViewStatus.SqlBuilder;
        Response.Redirect("operation_log_browse_search.aspx");
    }

    protected void btnClearCondition_Click(object sender, EventArgs e)
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.ClearConditions();

        CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition
                ("proc_time", "ʱ��",
                DateTimeUtil.BeginTimeOfDay(DateTimeUtil.Now),
                SQLConditionOperator.GreaterEqualThan,
                "��ʼ����Ϊ" + DateTimeUtil.GetDatePart(DateTimeUtil.Now));
        CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition
                ("proc_time", "ʱ��",
                DateTimeUtil.EndTimeOfDay(DateTimeUtil.Now),
                SQLConditionOperator.LessEqualThan,
                "��������Ϊ" + DateTimeUtil.GetDatePart(DateTimeUtil.Now));

        gridViewPager.RefreshGridView();
    }

}
