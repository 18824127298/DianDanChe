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

public partial class genui_LYOG_vcode_scene_search : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========== 1. 初始化界面 ==========
        WCUComboBox.InitComboBox(ddlbAlgolId, QDBVCBreakPools.PoolAlgol.CodeTableOfAll);
        ddlbAlgolId.Items.Add(new ListItem("[所有算法]", "ALL"));
        ddlbAlgolId.SelectedValue = "ALL";

        //========== 2. 取出查询条件 ==========
        if (PageParameter.StringParam[0] != "vcode_sceneLYOG")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. 刷新数据显示 ==========
        //========== 3.1 验证码标识 ==========
        string sVcodeId = sqlBuilder.GetConditionValueString("vcode_id");
        edtVcodeId.Text = sVcodeId;

        //========== 3.2 验证码名称 ==========
        string sVcodeName = sqlBuilder.GetConditionValueString("vcode_name");
        edtVcodeName.Text = sVcodeName;

        //========== 3.3 描述 ==========
        string sVcodeDesc = sqlBuilder.GetConditionValueString("vcode_desc");
        edtVcodeDesc.Text = sVcodeDesc;

        //========== 3.4 算法 ==========
        string sAlgolId = sqlBuilder.GetConditionValueString("algol_id");
        if (sAlgolId != "")
            ddlbAlgolId.SelectedValue = sAlgolId;

        //========== 3.5 算法参数 ==========
        string sAlgolParams = sqlBuilder.GetConditionValueString("algol_params");
        edtAlgolParams.Text = sAlgolParams;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. 重新清空搜索条件 ==========
        if (PageParameter.StringParam[0] != "vcode_sceneLYOG")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. 设置搜索条件 ==========
        //========== 2.1 验证码标识 ==========
        string sVcodeId = edtVcodeId.Text.Trim();
        if (sVcodeId != "")
            sqlBuilder.AddCondition("vcode_id", "验证码标识",
                    sVcodeId, SQLConditionOperator.Like);

        //========== 2.2 验证码名称 ==========
        string sVcodeName = edtVcodeName.Text.Trim();
        if (sVcodeName != "")
            sqlBuilder.AddCondition("vcode_name", "验证码名称",
                    sVcodeName, SQLConditionOperator.Like);

        //========== 2.3 描述 ==========
        string sVcodeDesc = edtVcodeDesc.Text.Trim();
        if (sVcodeDesc != "")
            sqlBuilder.AddCondition("vcode_desc", "描述",
                    sVcodeDesc, SQLConditionOperator.Like);

        //========== 2.4 算法 ==========
        string sAlgolId = ddlbAlgolId.SelectedValue;
        if (sAlgolId != "ALL")
            sqlBuilder.AddCondition("algol_id", "算法", sAlgolId,
                    SQLConditionOperator.Equal, ddlbAlgolId.SelectedItem.Text);

        //========== 2.5 算法参数 ==========
        string sAlgolParams = edtAlgolParams.Text.Trim();
        if (sAlgolParams != "")
            sqlBuilder.AddCondition("algol_params", "算法参数",
                    sAlgolParams, SQLConditionOperator.Like);

        //========== 3. 返回到数据列表页面 ==========
        Response.Redirect("vcode_scene_list.aspx");
    }
}
