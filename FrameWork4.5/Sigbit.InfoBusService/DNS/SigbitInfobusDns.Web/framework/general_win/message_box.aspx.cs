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

public partial class framework_general_win_message_box : SbtPageBase
{
    private string _sReturnToUrl = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //========== 1. 显示消息内容 ========
        lblMessage.Text = PageParameter.StringParam[0];

        //======== 2. 如果返回的url未设，则为js:historyBack的方式 ======
        string sBackUrl = PageParameter.StringParam[1];
        if (sBackUrl == "")
        {
            btnHistoryBack.Visible = true;
            btnUrlBack.Visible = false;
        }
        //========= 3. 否则，为redirect到url的方式 ========
        else
        {
            btnHistoryBack.Visible = false;
            btnUrlBack.Visible = true;
            _sReturnToUrl = sBackUrl;
        }
    }

    protected void btnUrlBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(_sReturnToUrl, true);
    }
}
