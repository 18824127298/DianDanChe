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
using Sigbit.App.PTP.DBDefine.WangDai;
using Sigbit.App.PTP.Web;

public partial class genui_UZAY_opera_log_list : PtpPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //========== 0. ��DataGrid��PageControl�Ĺ�ϵ ===========
        gridViewPager.GridViewControl = gvList;
        gridViewPager.PageIndexChanged
            = new module_WebUserControl.PageIndexChangedHandler(gvList__PageIndexChanged);

        int nTypeId = ConvertUtil.ToInt(Request["type_id"]);

        TbOperaEOperaType eOperaType = (TbOperaEOperaType)nTypeId;
        lblText.Text = "��־�б�--" + EnumExUtil.ToDescString(eOperaType);

        imgIcon.Src = "../../images/menu_icon/comm.gif";

        //========= 1. ����״ν�������ȡ��ǰ�ε�״̬ ===========
        if (!IsPostBack)
        {
            dpFromTime.DateTimeString = CurrentPageStatus.DataViewStatus.SqlBuilder.GetConditionValueString("CreateTime", SQLConditionOperator.GreaterEqualThan);
            dpToTime.DateTimeString = CurrentPageStatus.DataViewStatus.SqlBuilder.GetConditionValueString("CreateTime", SQLConditionOperator.LessEqualThan);

            if (dpFromTime.DateTimeString == "")
            {
                dpFromTime.DateTimeString = DateTimeUtil.BeginTimeOfDay(DateTimeUtil.Now);
            }

            edtContent.Text = CurrentPageStatus.DataViewStatus.SqlBuilder.GetConditionValueString("Remark", SQLConditionOperator.Like);

            gvList.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");

            if (CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql == "")
            {
                CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                        = "select * from opera where OperaType=" + nTypeId;

                CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField("CreateTime");
                CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField("CreateTime");

            }
            else
            {
                gvList.PageSize = CurrentPageStatus.DataViewStatus.PageSize;
                gvList.PageIndex = CurrentPageStatus.DataViewStatus.CurrentPageIndex;
            }

            CurrentPageStatus.DataViewStatus.SqlBuilder.ClearFixConditions();
            CurrentPageStatus.DataViewStatus.SqlBuilder.AddFixCondition("IsValid", "", "1");
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

        if (edtContent.Text.Trim() != "")
        {
            CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition("Remark", "��־����", edtContent.Text.Trim(), SQLConditionOperator.Like);
        }


        if (dpFromTime.DateTimeString != "")
            CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition("CreateTime", "����ʱ��",
               DateTimeUtil.BeginTimeOfDay(dpFromTime.DateTimeString), SQLConditionOperator.GreaterEqualThan,
               "����ʱ������\"" + dpFromTime.DateTimeString + "\"");

        if (dpToTime.DateTimeString != "")
            CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition("CreateTime", "����ʱ��",
               DateTimeUtil.EndTimeOfDay(dpToTime.DateTimeString), SQLConditionOperator.LessEqualThan,
               "����ʱ��С����\"" + dpToTime.DateTimeString + "\"");


        //Response.Write(CurrentPageStatus.DataViewStatus.SqlBuilder.ToString());

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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //int nTypeId = ConvertUtil.ToInt(Request["type_id"]);
        //PageParameter.StringParam[0] = "opera_logUZAY";
        //PageParameter.ObjParam[0] = CurrentPageStatus.DataViewStatus.SqlBuilder;
        //Response.Redirect("opera_log_search.aspx?type_id=" + nTypeId);


        FetchDataFromDB();
        gridViewPager.RefreshGridView();
    }

    protected void btnClearCondition_Click(object sender, EventArgs e)
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.ClearConditions();

        dpFromTime.DateTimeString = DateTimeUtil.BeginTimeOfDay(DateTimeUtil.Now);
        dpToTime.DateTimeString = "";
        edtContent.Text = "";

        FetchDataFromDB();

        CurrentPageStatus.DataViewStatus.SqlBuilder.ClearConditions();
        gridViewPager.RefreshGridView();



    }

    public string VIVOperaType(object oOperaType)
    {
        int nOperaType = ConvertUtil.ToInt(oOperaType);

        TbOperaEOperaType eOperaType = (TbOperaEOperaType)nOperaType;
        return EnumExUtil.ToDescString(eOperaType);
    }

    public string VIVGodName(object oGodId)
    {
        int nGodId = ConvertUtil.ToInt(oGodId);
        return QDBWangDaiPools.PoolGod.GetGodFullName(nGodId);
    }
}
