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

public partial class genui_WSQV_system_define_search : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========== 1. ��ʼ������ ==========
        //========== 2. ȡ����ѯ���� ==========
        if (PageParameter.StringParam[0] != "system_defineWSQV")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. ˢ��������ʾ ==========
        //========== 3.1 ϵͳ��� ==========
        string sSubSystemId = sqlBuilder.GetConditionValueString("sub_system_id");
        edtSubSystemId.Text = sSubSystemId;

        //========== 3.2 ϵͳ���� ==========
        string sSubSystemName = sqlBuilder.GetConditionValueString("sub_system_name");
        edtSubSystemName.Text = sSubSystemName;
        this.NaviTabController.AppendSelfToBar();
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. ��������������� ==========
        if (PageParameter.StringParam[0] != "system_defineWSQV")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. ������������ ==========
        //========== 2.1 ϵͳ��� ==========
        string sSubSystemId = edtSubSystemId.Text.Trim();
        if (sSubSystemId != "")
            sqlBuilder.AddCondition("sub_system_id", "ϵͳ���",
                    sSubSystemId, SQLConditionOperator.Like);

        //========== 2.2 ϵͳ���� ==========
        string sSubSystemName = edtSubSystemName.Text.Trim();
        if (sSubSystemName != "")
            sqlBuilder.AddCondition("sub_system_name", "ϵͳ����",
                    sSubSystemName, SQLConditionOperator.Like);

        //========== 3. ���ص������б�ҳ�� ==========
        Response.Redirect("system_define_list.aspx");
    }
}
