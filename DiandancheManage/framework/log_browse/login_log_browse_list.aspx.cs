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

public partial class genui_DITJ_login_log_browse_list : SbtPageBase
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
                        = "select * from sbt_log_user_login";
                CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition
                        ("login_time", "��¼ʱ��",
                        DateTimeUtil.BeginTimeOfDay(DateTimeUtil.Now),
                        SQLConditionOperator.GreaterEqualThan,
                        "��ʼ����Ϊ" + DateTimeUtil.GetDatePart(DateTimeUtil.Now));
                CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition
                        ("login_time", "��¼ʱ��",
                        DateTimeUtil.EndTimeOfDay(DateTimeUtil.Now),
                        SQLConditionOperator.LessEqualThan,
                        "��������Ϊ" + DateTimeUtil.GetDatePart(DateTimeUtil.Now));
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

    protected string VIVIsLoginSuccess(string sIsLoginSuccess)
    {
        if (sIsLoginSuccess == "N")
            return "no.gif";
        else
            return "yes.gif";
    }

    protected string VIVHasLogout(string sHasLogout)
    {
        if (sHasLogout == "N")
            return "no.gif";
        else
            return "yes.gif";
    }

    protected string VIVLockOperationImage(string sLockOperation)
    {
        if (sLockOperation == "")
            return "none.gif";
        else
            return sLockOperation + ".gif";
    }

    protected string VIVInSystemDuration(string sInSystemDuration)
    {
        int nDuration = ConvertUtil.ToInt(sInSystemDuration);
        string sRet = DateTimeUtil.ToTimeStrFromSecond(nDuration);

        return sRet;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        PageParameter.StringParam[0] = "login_log_browseDITJ";
        PageParameter.ObjParam[0] = CurrentPageStatus.DataViewStatus.SqlBuilder;
        Response.Redirect("login_log_browse_search.aspx");
    }

    protected void btnClearCondition_Click(object sender, EventArgs e)
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.ClearConditions();

        CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition
                ("login_time", "��¼ʱ��",
                DateTimeUtil.BeginTimeOfDay(DateTimeUtil.Now),
                SQLConditionOperator.GreaterEqualThan,
                "��ʼ����Ϊ" + DateTimeUtil.GetDatePart(DateTimeUtil.Now));
        CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition
                ("login_time", "��¼ʱ��",
                DateTimeUtil.EndTimeOfDay(DateTimeUtil.Now),
                SQLConditionOperator.LessEqualThan,
                "��������Ϊ" + DateTimeUtil.GetDatePart(DateTimeUtil.Now));

        gridViewPager.RefreshGridView();
    }

}
