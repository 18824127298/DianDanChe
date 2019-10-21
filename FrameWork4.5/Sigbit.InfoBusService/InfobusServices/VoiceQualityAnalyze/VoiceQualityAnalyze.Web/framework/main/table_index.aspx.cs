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

public partial class farmwork_main_table_index : SbtPageBase
{
    protected string _frameSetTemplate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        _frameSetTemplate = SbtTheme.GetThemeTemplate(CurrentUser.ThemePreference, "table_index");
    }
}

/*
 <frameset border="0" framespacing="0" rows="9,*,8" frameborder="NO" cols="*" class="panel">
    <frame name="table_top" src="table_index/table_top.aspx" frameborder="0" noresize scrolling="no">
    <frame name="table_main" src="table_index/intel_view.aspx" frameborder="0" noresize scrolling="yes" class="panelTableMain">
    <frame name="table_bottom" src="table_index/table_bottom.aspx" frameborder="0" noresize scrolling="no">
</frameset>
*/