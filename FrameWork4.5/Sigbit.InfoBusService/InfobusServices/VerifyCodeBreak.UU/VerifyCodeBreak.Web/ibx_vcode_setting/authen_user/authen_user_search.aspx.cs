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
using Sigbit.App.Net.IBXService.VCodeBreak.Service.DBDefine;

public partial class genui_EEQY_authen_user_search : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========== 1. ��ʼ������ ==========
        //========== 2. ȡ����ѯ���� ==========
        if (PageParameter.StringParam[0] != "authen_userEEQY")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. ˢ��������ʾ ==========
        //========== 3.1 �û��� ==========
        string sAuthenUserName = sqlBuilder.GetConditionValueString("authen_user_name");
        edtAuthenUserName.Text = sAuthenUserName;

        //========== 3.2 ��ע ==========
        string sRemarks = sqlBuilder.GetConditionValueString("remarks");
        edtRemarks.Text = sRemarks;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. ��������������� ==========
        if (PageParameter.StringParam[0] != "authen_userEEQY")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. ������������ ==========
        //========== 2.1 �û��� ==========
        string sAuthenUserName = edtAuthenUserName.Text.Trim();
        if (sAuthenUserName != "")
            sqlBuilder.AddCondition("authen_user_name", "�û���",
                    sAuthenUserName, SQLConditionOperator.Like);

        //========== 2.2 ��ע ==========
        string sRemarks = edtRemarks.Text.Trim();
        if (sRemarks != "")
            sqlBuilder.AddCondition("remarks", "��ע",
                    sRemarks, SQLConditionOperator.Like);

        //========== 3. ���ص������б�ҳ�� ==========
        Response.Redirect("authen_user_list.aspx");
    }
}
