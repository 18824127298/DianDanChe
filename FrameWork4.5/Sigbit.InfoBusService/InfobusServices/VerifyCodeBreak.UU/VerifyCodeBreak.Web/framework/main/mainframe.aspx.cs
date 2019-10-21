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

public partial class farmwork_main_mainframe : SbtPageBase
{
    protected string _frameSetTemplate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        _frameSetTemplate = SbtTheme.GetThemeTemplate(CurrentUser.ThemePreference, "mainframe");
    }
}

/*
蓝调的框架集
<frameset rows="70,*,20" cols="*" frameborder="NO" border="0" framespacing="0" id="frame1">    <!-- 上下方式分割为3块 -->
  <frame src="index_top.php" name="topFrame" scrolling="NO" noresize >                         <!--//顶部页面  -->
 
  <frameset rows="*" cols="9,186,5,13,*,8" framespacing="0" frameborder="NO" border="0" id="frame2"><!--//中部再分为几块,左右方式分割 -->
    <frame src="menu_leftbar.php" name="menu_leftbar" scrolling="NO" noresize>                 <!--//菜单左边条 -->
	<frame src="function_panel" name="function_panel" scrolling="NO" noresize>                      <!--//左边的菜单页 -->
    <frame src="menu_rightbar.php" name="menu_rightbar" scrolling="NO" noresize>               <!--//菜单左边条 -->
	<frame src="controlmenu.php" name="controlmenu" scrolling="no" frameborder="0" noresize>   <!--//中间页，控制左边菜单的显隐 --> 
	<frame src="table_index.php" name="table_index" scrolling="no" frameborder="0" noresize>   <!--//右边的内容页面，显示菜单点击页面 -->
	<frame src="table_right.php" name="table_right" scrolling="no" frameborder="0" noresize>   <!--//右边条 -->        
  </frameset>
  
  <frame src="status_bar" name="status_bar" scrolling="NO" noresize >                      <!--//底部的状态页面 -->
</frameset>

<noframes>您的浏览器不支持框架页面，请使用IE6.0以上的浏览器！</noframes>
</html>
 */

/*
 * 引入模板前的东东
 <frameset rows="70,*,20" cols="*" frameborder="NO" border="0" framespacing="0" id="frame1">
    <frame src="index_top.aspx" name="topFrame" scrolling="no" noresize>

<% if (CurrentUser.ThemePreference == "blue-comfort") { %>    
  <frameset rows="*" cols="9,186,5,13,*,8" framespacing="0" frameborder="NO" border="0" id="frame2"><!--//中部再分为几块,左右方式分割 -->
    <frame src="menu_leftbar.aspx" name="menu_leftbar" scrolling="NO" noresize>                 <!--//菜单左边条 -->
	<frame src="function_panel.aspx" name="function_panel" scrolling="NO" noresize>                      <!--//左边的菜单页 -->
    <!--frame src="menu_rightbar.aspx" name="menu_rightbar" scrolling="NO" noresize-->               <!--//菜单左边条 -->
	<frame src="controlmenu.aspx" name="controlmenu" scrolling="no" frameborder="0" noresize>   <!--//中间页，控制左边菜单的显隐 --> 
	<frame src="table_index.aspx" name="table_index" scrolling="no" frameborder="0" noresize>   <!--//右边的内容页面，显示菜单点击页面 -->
	<frame src="table_right.aspx" name="table_right" scrolling="no" frameborder="0" noresize>   <!--//右边条 -->        
  </frameset>
<% } else { %>        
    <frameset runat="server" rows="*" cols="2,186,20,*,8" id="frame2" framespacing="0" frameborder="NO" border="0">
        <frame src="menu_leftbar.aspx" name="menu_leftbar" scrolling="no" noresize>
        <frame src="function_panel.aspx" name="function_panel" scrolling="no" noresize>
        <frame src="controlmenu.aspx" name="controlmenu" scrolling="no" frameborder="0" noresize>
        <frame src="table_index.aspx" name="table_index" scrolling="no" frameborder="0" noresize>
        <frame src="table_right.aspx" name="table_right" scrolling="no" frameborder="0" noresize>
    </frameset>
<% } %>        
    <frame src="status_bar.aspx" name="status_bar" scrolling="no" noresize>
</frameset>
*/