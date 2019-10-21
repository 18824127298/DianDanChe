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

using Sigbit.Framework;

public partial class framework_ctrlpanel_preference_setting : SbtPageBase //System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        CurrentUser.SetThemePreference(ddlbTheme.SelectedValue);
        CurrentUser.SetLanguagePreference(ddlbLanguage.SelectedValue);

        //SbtMessageBoxPage page = new SbtMessageBoxPage(this);
        //page.MessageText = "个人喜好设置已修改。"
        //        + "喜好的语言改为" + ddlbLanguage.SelectedItem.Text + "，"
        //        + "页面风格改为" + ddlbTheme.SelectedItem.Text + "。";
        //page.Show();

        PageParameter.StringParam[0] = "个人喜好设置已修改。"
                + "个人喜好的语言改为" + ddlbLanguage.SelectedItem.Text + "，"
                + "页面风格改为" + ddlbTheme.SelectedItem.Text + "。";
        Response.Redirect("setting_result_message.aspx", true);
    }
}
