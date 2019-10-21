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

using Sigbit.Common;
using Sigbit.Framework;
using Sigbit.App.Net.IBXService.VoiceCOMP.Service.VCOMPService;

public partial class ibx_voice_reg_manual_input_reg_result_new_request_notice : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        edtRootUrl.Text = IBMVoiceCOMPConfig.Instance.VoiceRootPath;
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        IBMVoiceCOMPConfig.Instance.VoiceRootPath = edtRootUrl.Text.Trim();

        SbtMessageBoxPage msgPage = new SbtMessageBoxPage(this);
        msgPage.MessageText = "配置成功，当前路径修改为" + IBMVoiceCOMPConfig.Instance.VoiceRootPath;
        msgPage.ReturnUrl = this.Request.Url.AbsoluteUri;

        msgPage.Show();
    }
}
