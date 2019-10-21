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

public partial class farmwork_main_menu_leftbar : SbtPageBase
{
    protected string _bodyTemplate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        _bodyTemplate = SbtTheme.GetThemeTemplate(CurrentUser.ThemePreference, "menu_leftbar");
    }
}

/*
 <body>
    <table height="100%" cellspacing="0" cellpadding="0" width="2" border="0">
        <tbody>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" height="270">
                    <img height="270" alt="" src="../../theme_images/main/menu/index_25.jpg" width="2"
                        align="absBottom"></td>
            </tr>
        </tbody>
    </table>
</body>
*/