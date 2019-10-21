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
using Sigbit.App.PTP.DBDefine;

public partial class genui_UZAY_opera_log_search : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========== 1. ��ʼ������ ==========
        //========== 2. ȡ����ѯ���� ==========
        if (PageParameter.StringParam[0] != "opera_logUZAY")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. ˢ��������ʾ ==========
        //========== 3.1 �û� ==========
        //string nGodid = sqlBuilder.GetConditionValueString("GodId");
        //edtGodid.Text = nGodid;

        ////========== 3.2 ��ע ==========
        //string sRemark = sqlBuilder.GetConditionValueString("Remark");
        //edtRemark.Text = sRemark;

    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. ��������������� ==========
        if (PageParameter.StringParam[0] != "opera_logUZAY")
            return;
        int nTypeId = ConvertUtil.ToInt(Request["type_id"]);
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. ������������ ==========
        //========== 2.1 �û� ==========
        int nGodid = ConvertUtil.ToInt(ddlbGodName.SelectedValue);
        if (nGodid != 0)
            sqlBuilder.AddCondition("GodId", "�û�",
                    nGodid, SQLConditionOperator.Equal);

        //========== 2.2 ��ע ==========
        string sRemark = edtRemark.Text.Trim();
        if (sRemark != "")
            sqlBuilder.AddCondition("Remark", "��ע",
                    sRemark, SQLConditionOperator.Like);

        //========== 3. ���ص������б�ҳ�� ==========
        Response.Redirect("opera_log_list.aspx?type_id=" + nTypeId);
    }

    protected void btnHuoQu_Click(object sender, EventArgs e)
    {
        string sGodName = edtGodid.Text.Trim();

        string sSQL = string.Format("select * from God where Aliases like '%{0}%'", sGodName);
        DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

        ddlbGodName.DataSource = ds.Tables[0].DefaultView;
        ddlbGodName.DataTextField = "Aliases";
        ddlbGodName.DataValueField = "Id";
        ddlbGodName.DataBind();
        ddlbGodName.Items.Insert(0, "��ѡ��");
        ddlbGodName.SelectedValue = "��ѡ��";
    }
}
