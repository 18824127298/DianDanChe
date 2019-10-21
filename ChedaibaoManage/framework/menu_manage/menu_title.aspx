<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu_title.aspx.cs" Inherits="framework_menu_navigate_menu_title" %>

<html>
<head id="Head1" runat="server">
    <title>菜单管理标题</title>
</head>
<body class="bodycolor" topmargin="5">
    <form id="form1" runat="server">
        <div class="titleTable">
            <table border="0" width="100%" cellspacing="0" cellpadding="3" class="small">
                <tr>
                    <td width="50%">
                        <img src="../../images/menu_icon/struct.gif" width="16" height="16" align="absmiddle">
                        菜单管理</td>
                    <td align="right">
                        <a href="menu_csv_import.aspx" target="frameMenuConfig" style="color: #0000FF; text-decoration: underline">导入菜单</a>
                        &nbsp;&nbsp;<a href="menu_csv_export.aspx" target="frameMenuConfig" style="color: #0000FF; text-decoration: underline">导出菜单</a>
                        &nbsp;&nbsp;<a href="switch_sub_system.aspx" target="frameMenuConfig" style="color: #0000FF; text-decoration: underline">切换子系统</a>
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnTrimMenuOrder" runat='server' Text="规整菜单显示顺序" OnClick="btnTrimMenuOrder_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
