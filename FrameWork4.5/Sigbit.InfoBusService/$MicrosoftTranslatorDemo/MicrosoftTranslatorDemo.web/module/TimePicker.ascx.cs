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

using Sigbit.Web.JavaScipt;

public partial class module_DateTime : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        string sJsRootPath = JSUtil.GenJSRootPath(this.Page);
        ltJS.Text = string.Format("<script src='{0}js/setTime.js' type='text/javascript'></script>", sJsRootPath);
    }
}
