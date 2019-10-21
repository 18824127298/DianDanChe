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
using Sigbit.Web.WebControlUtil;

public partial class framework_ctrlpanel_test : SbtPageBase
{
    protected string _refreshDesktop = "false";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========= 1. 初始化下拉列表的内容 ========
        WCUComboBox.InitComboBox(ddlbLanguage, SbtCodeTables.PreferenceLanguage);
        WCUComboBox.InitComboBox(ddlbTheme, SbtCodeTables.PreferenceTheme);

        //========= 2. 置为当前用户的偏好设置 ==========
        ddlbLanguage.SelectedValue = CurrentUser.LanguagePreference;
        ddlbTheme.SelectedValue = CurrentUser.ThemePreference;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========= 1. 设置当前用户的偏好 ==========
        string sOldTheme = CurrentUser.ThemePreference;

        CurrentUser.SetThemePreference(ddlbTheme.SelectedValue);
        CurrentUser.SetLanguagePreference(ddlbLanguage.SelectedValue);

        if (sOldTheme == ddlbTheme.SelectedValue)
        {
            //======== 2. 显示结果消息 ==============
            SbtMessageBoxPage page = new SbtMessageBoxPage(this);

            //====== 2+. 由于用historyBack的方式，页面没有刷新，故强制一个页面 ====
            page.ReturnUrl = "~/framework/ctrlpanel/preference_setting.aspx";
            page.MessageText = "个人喜好设置已修改。"
                    + "喜好的语言改为" + ddlbLanguage.SelectedItem.Text + "，"
                    + "页面风格改为" + ddlbTheme.SelectedItem.Text + "。";
            page.Show();
        }
        else
        {
            _refreshDesktop = "true";
        }
    }
}
