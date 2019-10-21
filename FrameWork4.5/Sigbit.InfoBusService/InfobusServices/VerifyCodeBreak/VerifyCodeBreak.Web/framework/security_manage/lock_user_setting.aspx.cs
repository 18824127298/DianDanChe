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
    private void ShowLockEnabled(bool bEnabled)
    {
        lblLockHint1.Enabled = bEnabled;
        lblLockHint2.Enabled = bEnabled;
        edtLockTimes.Enabled = bEnabled;
    }

    private void ShowUnlockEnabled(bool bEnabled)
    {
        lblUnlockHint1.Enabled = bEnabled;
        lblUnlockHint2.Enabled = bEnabled;
        edtUnlockMinutes.Enabled = bEnabled;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //=========== 1. 自动锁定 ===========
        if (SbtSecurityConfig.LockUser.AutoLockEnabled == Bool3State.True)
            cbxLockEnabled.Checked = true;
        else
            cbxLockEnabled.Checked = false;

        edtLockTimes.Text = SbtSecurityConfig.LockUser.AutoLockTimes.ToString();

        //========= 2. 自动解锁 ============
        if (SbtSecurityConfig.LockUser.AutoUnunlockEnabled == Bool3State.True)
            cbxUnlockEnabled.Checked = true;
        else
            cbxUnlockEnabled.Checked = false;

        edtUnlockMinutes.Text = SbtSecurityConfig.LockUser.AutoUnlockMinutes.ToString();

        cbxLockEnabled_CheckedChanged(sender, e);
        cbxUnlockEnabled_CheckedChanged(sender, e);
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. 设置配置值 =================
        //=========== 1.1 自动锁定 ==============
        if (cbxLockEnabled.Checked)
            SbtSecurityConfig.LockUser.AutoLockEnabled = Bool3State.True;
        else
            SbtSecurityConfig.LockUser.AutoLockEnabled = Bool3State.False;

        SbtSecurityConfig.LockUser.AutoLockTimes = ConvertUtil.ToInt(edtLockTimes.Text);

        //============ 1.2 自动解锁 ===========
        if (cbxUnlockEnabled.Checked)
            SbtSecurityConfig.LockUser.AutoUnunlockEnabled = Bool3State.True;
        else
            SbtSecurityConfig.LockUser.AutoUnunlockEnabled = Bool3State.False;

        SbtSecurityConfig.LockUser.AutoUnlockMinutes = ConvertUtil.ToInt(edtUnlockMinutes.Text);

        //========== 2. 显示结果消息 ===========
        SbtMessageBoxPage page = new SbtMessageBoxPage(this);
        page.ReturnUrl
                = "~/framework/security_manage/lock_user_setting.aspx";
        page.MessageText = "已重新设置用户锁定的相关配置。";
        page.Show();
    }

    protected void cbxLockEnabled_CheckedChanged(object sender, EventArgs e)
    {
        ShowLockEnabled(cbxLockEnabled.Checked);
    }

    protected void cbxUnlockEnabled_CheckedChanged(object sender, EventArgs e)
    {
        ShowUnlockEnabled(cbxUnlockEnabled.Checked);
    }

    protected void btnDefaultValue_Click(object sender, EventArgs e)
    {
        cbxLockEnabled.Checked = false;
        cbxUnlockEnabled.Checked = false;

        cbxLockEnabled_CheckedChanged(sender, e);
        cbxUnlockEnabled_CheckedChanged(sender, e);

        edtLockTimes.Text = "3";
        edtUnlockMinutes.Text = "5";
    }
}
