<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu_framework.aspx.cs" Inherits="framework_menu_navigate_menu_framework" %>

<html>
<head id="Head1" runat="server">
    <title>菜单管理</title>
    <meta content="MSHTML 6.00.2900.3199" name="GENERATOR">
</head>
<frameset id="frameMain" rows="40,*" border="1" framespacing="0" cols="*">
    <frame name="frameMenuTitle" src="menu_title.aspx" frameborder="no" noresize scrolling="no">
    <frameset id="frameMenuBody" frameborder="1" rows="*" cols="200,*">
        <frame name="frameMenuList" src="menu_list.aspx" frameborder="1" noresize>
        <frame name="frameMenuConfig" src="menu_new.aspx?mnu_uid=" frameborder="1">
    </frameset>
</frameset>
</html>
