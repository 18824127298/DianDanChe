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

public partial class framework_main_table_index_table_top : SbtPageBase
{
    protected string _bodyTemplate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        _bodyTemplate = SbtTheme.GetThemeTemplate(CurrentUser.ThemePreference, 
                "tblidx_table_top");
    }
}
/*
<body>
    <table height="9" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tbody>
            <tr>
                <td nowrap background="../../../theme_images/main/table/index_12.gif">
                    <img height="9" src="../../../theme_images/main/table/index_12.gif" width="20"></td>
            </tr>
        </tbody>
    </table>
</body>
 */