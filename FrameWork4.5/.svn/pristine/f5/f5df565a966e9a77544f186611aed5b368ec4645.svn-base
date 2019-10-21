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

using Sigbit.Framework.Security;

public partial class framework_organize_user_list : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool bLockColumnVisible = false;
        if (SbtSecurityConfig.LockUser.AutoLockEnabled == Bool3State.True)
        {
            if (CurrentUser.IsAdmin)
                bLockColumnVisible = true;
        }
        if (bLockColumnVisible)
            gvList.Columns[5].Visible = true;
        else
            gvList.Columns[5].Visible = false;

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
                    ("dept_id", "����", sDeptId);
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
        //========= 1. ȡ�����ݣ����� ========
        DataSet ds = DataHelper.Instance.ExecuteDataSet
                (CurrentPageStatus.DataViewStatus.SqlBuilder.ToString());
        gvList.DataSource = ds;
        gvList.DataBind();

        if (ds.Tables[0].Rows.Count == 0)
            btnDelete.Visible = false;

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

    protected string VIVLockWidth(string sUserUid)
    {
        if (SbtSecurityConfig.LockUser.AutoLockEnabled == Bool3State.False)
            return "0";

        TbUserSecurity tblSecurity = new TbUserSecurity();
        tblSecurity.UserUid = sUserUid;

        bool bIsLockedNow = false;
        if (tblSecurity.Fetch(true))
        {
            if (tblSecurity.LockStatus != "")
                bIsLockedNow = true;
        }

        if (bIsLockedNow)
            return "16";
        else
            return "0";
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
