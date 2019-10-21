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
using Sigbit.App.Net.IBXService.Log;

public partial class mdtseat_chat_log_config_log_config_setting : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        cbxLogInDB.Checked = CTIBXLogMessageConfig.Instance.LogMessageInDB;

        edtClearBeforeHours.Text = CTIBXLogMessageConfig.Instance.LogClearBeforeHours.ToString();

        cbxLogInDB_CheckedChanged(sender, e);
    }

    protected void cbxLogInDB_CheckedChanged(object sender, EventArgs e)
    {
        if (cbxLogInDB.Checked)
        {
            edtClearBeforeHours.Enabled = true;
        }
        else
        {
            edtClearBeforeHours.Enabled = false;
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. 设置配置值 =================
        CTIBXLogMessageConfig.Instance.LogMessageInDB = cbxLogInDB.Checked;
        CTIBXLogMessageConfig.Instance.LogClearBeforeHours = ConvertUtil.ToInt(edtClearBeforeHours.Text.Trim());

        //========== 2. 显示结果消息 ===========
        SbtMessageBoxPage page = new SbtMessageBoxPage(this);
        page.ReturnUrl = Request.Url.AbsoluteUri;
        page.MessageText = "已重新设置总线消息日志配置。";
        page.Show();
    }
}
