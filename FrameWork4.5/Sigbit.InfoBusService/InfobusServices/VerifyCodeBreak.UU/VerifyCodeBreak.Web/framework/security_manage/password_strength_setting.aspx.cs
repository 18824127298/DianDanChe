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
using Sigbit.Framework.Security;

public partial class mdtseat_chat_log_config_log_config_setting : SbtPageBase
{
    private RadioButton RadioButtonOfLevel(int nLevel)
    {
        switch (nLevel)
        {
            case 1:
                return rblevel1;
            case 2:
                return rbLevel2;
            case 3:
                return rblevel3;
            case 4:
                return rbLevel4;
            default:
                return rbLevel0;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        int nPasswordStrength = SbtSecurityConfig.Password.PasswordStrength;
        RadioButtonOfLevel(nPasswordStrength).Checked = true;

        if (SbtSecurityConfig.Password.ModiPasswordIfLowerStrength == Bool3State.True)
            cbxModiPasswordIfLower.Checked = true;
        else
            cbxModiPasswordIfLower.Checked = false;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. 设置配置值 =================
        //========= 1.1 密码强度 ===============
        int nPasswordStrength = 0;

        for (int i = 0; i <= 4; i++)
        {
            if (RadioButtonOfLevel(i).Checked)
            {
                nPasswordStrength = i;
                break;
            }
        }
        SbtSecurityConfig.Password.PasswordStrength = nPasswordStrength;

        //=========== 1.2 登录后修改低强度密码 ===============
        if (cbxModiPasswordIfLower.Checked)
            SbtSecurityConfig.Password.ModiPasswordIfLowerStrength = Bool3State.True;
        else
            SbtSecurityConfig.Password.ModiPasswordIfLowerStrength = Bool3State.False;

        //========== 2. 显示结果消息 ===========
        SbtMessageBoxPage page = new SbtMessageBoxPage(this);
        page.ReturnUrl
                = "~/framework/security_manage/password_strength_setting.aspx";
        page.MessageText = "已重新设置密码强度的相关配置。";
        page.Show();
    }
}
