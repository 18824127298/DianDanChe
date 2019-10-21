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

using Sigbit.Framework;

public partial class ibx_voice_reg_manual_input_reg_result_new_request_notice : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnBackToFillPage_Click(object sender, EventArgs e)
    {
        Response.Redirect("manual_input_reg_result.aspx");
    }

}
