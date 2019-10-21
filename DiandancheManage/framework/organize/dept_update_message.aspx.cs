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

public partial class framework_ctrlpanel_setting_result_message : SbtPageBase
{
    //private string _sReturnToUrl = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //========== 1. 显示消息内容 ========
        lblMessage.Text = PageParameter.StringParam[0];
    }
}
