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

public partial class genui_UZAY_opera_log_list : PtpGodBase
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
            CodeTableBase ctOperaType = TbOperaEOperaType.None.ToCodeTable();
            for (int i = 0; i < ctOperaType.Count; i++)
            {
                TbOperaEOperaType eOperaType = (TbOperaEOperaType)EnumExUtil.CodeToEnum(TbOperaEOperaType.None, ctOperaType.GetCode(i));
                ddlbOperaType.Items.Add(new ListItem(eOperaType.ToDescString(), ((int)eOperaType).ToString()));
            }
            ddlbOperaType.Items.Add(new ListItem("=======所有=======", ""));
            ddlbOperaType.SelectedValue = "";

            dpFromTime.DateTimeString = CurrentPageStatus.DataViewStatus.SqlBuilder.GetConditionValueString("Opera.CreateTime", SQLConditionOperator.GreaterEqualThan);
            dpToTime.DateTimeString = CurrentPageStatus.DataViewStatus.SqlBuilder.GetConditionValueString("Opera.CreateTime", SQLConditionOperator.LessEqualThan);

            if (dpFromTime.DateTimeString == "")
            {
                dpFromTime.DateTimeString = DateTimeUtil.BeginTimeOfDay(DateTimeUtil.AddDays(DateTimeUtil.Now, -3));
            }

            if (dpToTime.DateTimeString == "")
            {
                dpToTime.DateTimeString = DateTimeUtil.EndTimeOfDay(DateTimeUtil.Now).Substring(0, 19);
            }

            ddlbOperaType.SelectedValue = CurrentPageStatus.DataViewStatus.SqlBuilder.GetConditionValueString("OperaType");


            edtContent.Text = CurrentPageStatus.DataViewStatus.SqlBuilder.GetConditionValueString("Opera.Remark", SQLConditionOperator.Like);

            gvList.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");

            if (CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql == "")
            {
                CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                        = "select top 1000 * from Opera  ";

                CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField("Opera.CreateTime");
                CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField("Opera.CreateTime");

            }
            else
            {
                gvList.PageSize = CurrentPageStatus.DataViewStatus.PageSize;
                gvList.PageIndex = CurrentPageStatus.DataViewStatus.CurrentPageIndex;
            }

            CurrentPageStatus.DataViewStatus.SqlBuilder.ClearFixConditions();
            CurrentPageStatus.DataViewStatus.SqlBuilder.AddFixCondition("Opera.IsValid", "", "1");
            CurrentPageStatus.DataViewStatus.SqlBuilder.AddFixCondition("Opera.GodId", "", CurrentGod.Id);

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


        string sOperaType = ddlbOperaType.SelectedValue;

        if (sOperaType != "")
        {
            CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition("OperaType",
                "操作类型", sOperaType, SQLConditionOperator.Equal, "操作类型为\"" + ddlbOperaType.SelectedItem.Text + "\"");
        }


        if (edtContent.Text.Trim() != "")
        {
            CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition("Opera.Remark",
                "日志内容", edtContent.Text.Trim(), SQLConditionOperator.Like);
        }


        if (dpFromTime.DateTimeString != "")
            CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition("Opera.CreateTime", "操作时间",
               DateTimeUtil.BeginTimeOfDay(dpFromTime.DateTimeString), SQLConditionOperator.GreaterEqualThan,
               "操作时间大等于\"" + dpFromTime.DateTimeString + "\"");

        if (dpToTime.DateTimeString != "")
            CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition("Opera.CreateTime", "操作时间",
               DateTimeUtil.EndTimeOfDay(dpToTime.DateTimeString), SQLConditionOperator.LessEqualThan,
               "操作时间小等于\"" + dpToTime.DateTimeString + "\"");




        //Response.Write(CurrentPageStatus.DataViewStatus.SqlBuilder.ToString());

        //========= 1. 取出数据，并绑定 ========
        DataSet ds = DataHelper.Instance.ExecuteDataSet
                (CurrentPageStatus.DataViewStatus.SqlBuilder.ToString());
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
        dpToTime.DateTimeString = DateTimeUtil.EndTimeOfDay(DateTimeUtil.Now).Substring(0, 19);
        dpToTime.DateTimeString = "";
        edtContent.Text = "";
        ddlbOperaType.SelectedValue = "";

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


}
