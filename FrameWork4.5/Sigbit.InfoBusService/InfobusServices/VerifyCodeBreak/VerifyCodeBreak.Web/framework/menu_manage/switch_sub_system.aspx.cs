using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Sigbit.Web.WebControlUtil;
using Sigbit.Framework;
using Sigbit.Framework.SubSystem;

public partial class framework_menu_manage_switch_sub_system : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        WCUComboBox.InitComboBox(ddlbSubSystem, SUSSubSystem.CodeTableOfSubSystem);
        ddlbSubSystem.SelectedValue = SUSSubSystem.CurrentSubSystemID__ForMenuEdit;
    }

    protected void btnSwitch_Click(object sender, EventArgs e)
    {
        SUSSubSystem.CurrentSubSystemID__ForMenuEdit = ddlbSubSystem.SelectedValue;

        PageParameter.StringParam[0] = "当前编辑的子系统已切换至“"
            + ddlbSubSystem.SelectedItem.Text + "”";
        Response.Redirect("menu_update_message.aspx");
    }
}
