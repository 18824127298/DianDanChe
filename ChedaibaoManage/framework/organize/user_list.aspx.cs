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

public partial class genui_CRPN_user_list : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        string sDeptId = ConvertUtil.ToString(Request["dpt_uid"]);

        PageParameter.StringParam[22] = "user_dept_tree_current_dept_EBNP";
        PageParameter.StringParam[23] = sDeptId;

        if (sDeptId == "")
        {
            lblDeptName.Text = "";
            btnAdd.Visible = false;
        }
        else
        {
            TbUserDept tblDept = new TbUserDept();
            tblDept.DeptId = sDeptId;
            tblDept.Fetch();
            lblDeptName.Text = tblDept.DeptName;
        }

        if (CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql == "")
        {
            CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                    = "select * from sbt_user";
            CurrentPageStatus.DataViewStatus.SqlBuilder.AddFixCondition
                    ("dept_id", "部门", sDeptId);
        }
        else
        {
            gvList.PageSize = CurrentPageStatus.DataViewStatus.PageSize;
            gvList.PageIndex = CurrentPageStatus.DataViewStatus.CurrentPageIndex;
        }
        FetchDataFromDB();
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

        if (ds.Tables[0].Rows.Count == 0)
            btnDelete.Visible = false;

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
        FetchDataFromDB();
    }

    protected string VIVSex(string sSex)
    {
        if (sSex == "F")
            return "sex_female.gif";
        else
            return "sex_male.gif";
    }

    protected string VIVIsActive(string sIsActive)
    {
        if (sIsActive == "N")
            return "sts_disable.gif";
        else
            return "sts_enable.gif";
    }

    protected void btnClearCondition_Click(object sender, EventArgs e)
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.ClearConditions();
        FetchDataFromDB();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("user_modify.aspx");
    }

}
