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

public partial class farmwork_main_function_panel : SbtPageBase
{
    protected string _frameSetTemplate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        _frameSetTemplate = SbtTheme.GetThemeTemplate(CurrentUser.ThemePreference, "function_panel");
    }
}

/*
<frameset rows="74,*,0,0,0,58"  cols="*" frameborder="NO" border="0" framespacing="0" id="frame1">
 <frame name="menu_info" scrolling="no" noresize src="menu_info.php" frameborder="0">

    <frame name="menu_main" scrolling="auto" noresize src="menu.php?HELPER=" frameborder="0">
    <frame name="user_online" scrolling="auto" noresize src="user_online.php" frameborder="0">
    <frame name="user_all" scrolling="auto" noresize src="user_all.php?HELPER=" frameborder="0">
    <frame name="menu_page" scrolling="auto" noresize src="" frameborder="0">
    <frame name="menu_operation" scrolling="no" noresize src="menu_operation.php" frameborder="0">
</frameset>


 */

/*template之前
<% if (CurrentUser.ThemePreference == "blue-comfort") { %>    
<frameset rows="74,*,0,0,0,58"  cols="*" frameborder="NO" border="0" framespacing="0" id="frame1">
 <frame name="menu_info" scrolling="no" noresize src="function_panel/menu_info.aspx" frameborder="0">

    <frame name="menu_main" scrolling="auto" noresize src="function_panel/menu.aspx" frameborder="0">
    <frame name="user_online" scrolling="auto" noresize src="#" frameborder="0">
    <frame name="user_all" scrolling="auto" noresize src="function_panel/user_all.aspx" frameborder="0">
    <frame name="menu_page" scrolling="auto" noresize src="" frameborder="0">
    <frame name="menu_operation" scrolling="no" noresize src="function_panel/menu_operation.aspx" frameborder="0">
</frameset>
<% } else { %>        
<frameset id="frame1" border="0" framespacing="0" rows="68,94,*,0,0,0" frameborder="NO"
    cols="*" class="panel">
    <frame name="menu_info" src="function_panel/menu_info.aspx" frameborder="0"
        noresize scrolling="no" class="panel">
    <frame name="menu_operation" src="function_panel/menu_operation.aspx" frameborder="0"
        noresize scrolling="no" class="panel">
    <frame name="menu_main" src="function_panel/menu.aspx" frameborder="0" noresize class="panel">
<%--    <frame name="user_online" src="function_panel.files/user_online.htm" frameborder="0"
        noresize>--%>
    <frame name="user_all" src="function_panel/user_all.aspx" frameborder="0" noresize>
    <frame name="menu_page" src="about:blank" frameborder="0" noresize>
</frameset>
<% } %>   */