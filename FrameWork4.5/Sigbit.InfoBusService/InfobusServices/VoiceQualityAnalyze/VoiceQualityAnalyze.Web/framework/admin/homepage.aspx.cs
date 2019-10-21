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
using Sigbit.Framework;

//using Sigbit.App.DialWifiInspector.Common;

public partial class farmwork_homepage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetNoStore();

        if (IsPostBack)
            return;

        if (ConvertUtil.ToString(Request["log_out"]) == "Y")
        {
            Session.Abandon();
            Session["currentUserMEFTMG"] = null;
        }

        if (Session["currentUserMEFTMG"] != null)
        {
            SbtUser user = Session["currentUserMEFTMG"] as SbtUser;
            ltCurrentUser.Text = "当前用户：" + user.RealName;
        }
    }   
}
