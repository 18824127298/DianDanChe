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

public partial class framework_main_table_right : SbtPageBase
{
    protected string _bodyTemplate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        _bodyTemplate = SbtTheme.GetThemeTemplate(CurrentUser.ThemePreference, "table_right");
    }
}
