<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu.aspx.cs" Inherits="farmwork_main_function_panel_menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>主菜单</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <style type="text/css">A:link {
	COLOR: #000000; TEXT-DECORATION: none
}
A:visited {
	COLOR: #000000; TEXT-DECORATION: none
}
A:active {
	COLOR: #3333ff; TEXT-DECORATION: none
}
A:hover {
	COLOR: #ff0000; TEXT-DECORATION: underline
}
BODY {
	MARGIN: 0px
}
</style>
    <meta content="MSHTML 6.00.2900.3059" name="GENERATOR">
    <link href="../../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/xtree.css" rel="stylesheet" type="text/css" />
</head>
<body class="menuPanel" leftmargin="0" topmargin="0" rightmargin="0" marginheight="0" marginwidth="0" runat="server">
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0"
                border="0" align="left" height="100%" width="100" runat="server" class=small>
                <tr>
                    <td valign="top" align="left">
                        <asp:TreeView ID="TreeViewMenu" runat="server" ImageSet="XPFileExplorer" ShowLines="True" NodeIndent="15" Visible="False">

                            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                                VerticalPadding="0px" />
                            <Nodes>
                                <asp:TreeNode Text="智能交互平台" Value="智能交互平台" Expanded="True" ShowCheckBox="False" SelectAction="Expand">
                                    <asp:TreeNode ImageUrl="~/theme_images/main/menu/@system.gif" Text="组织结构" Value="组织结构" Expanded="False">
                                        <asp:TreeNode ImageUrl="~/theme_images/main/menu/sys.gif" Text="部门管理" Value="部门管理"></asp:TreeNode>
                                        <asp:TreeNode ImageUrl="~/theme_images/main/menu/sys.gif" Text="市场管理" Value="市场管理" NavigateUrl="dsf.aspx" Target="table_main"></asp:TreeNode>
                                        <asp:TreeNode ImageUrl="~/theme_images/main/menu/sys.gif" Text="用户管理" Value="用户管理"></asp:TreeNode>
                                        <asp:TreeNode ImageUrl="~/theme_images/main/menu/sys.gif" Text="财务管理" Value="财务管理"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode ImageUrl="~/theme_images/main/menu/hrms.gif" Text="人力资源" Value="人力资源" Expanded="False">
                                        <asp:TreeNode ImageUrl="~/theme_images/main/menu/@score.gif" Text="考勤管理" Value="考勤管理">
                                        </asp:TreeNode>
                                        <asp:TreeNode ImageUrl="~/theme_images/main/menu/hrms.gif" Text="人事档案" Value="人事档案">
                                        </asp:TreeNode>
                                        <asp:TreeNode ImageUrl="~/theme_images/main/menu/@salary.gif" Text="劳资管理" Value="劳资管理">
                                        </asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode ImageUrl="~/theme_images/main/table/erp.gif" Text="公共事物" Value="公共事物" Expanded="False">
                                        <asp:TreeNode ImageUrl="~/theme_images/main/table/notify.gif" Text="公告通知管理" Value="公告通知管理">
                                        </asp:TreeNode>
                                        <asp:TreeNode ImageUrl="~/theme_images/main/table/news.gif" Text="新闻管理" Value="新闻管理">
                                        </asp:TreeNode>
                                        <asp:TreeNode ImageUrl="~/theme_images/main/table/vote.gif" Text="投票管理" Value="投票管理">
                                        </asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode ImageUrl="~/theme_images/main/menu/attendance.gif" Text="系统管理" Value="系统管理" Expanded="False">
                                        <asp:TreeNode ImageUrl="~/theme_images/main/menu/attendance.gif" Text="系统日志" Value="系统日志">
                                        </asp:TreeNode>
                                    </asp:TreeNode>
                                </asp:TreeNode>
                            </Nodes>
                            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                NodeSpacing="0px" VerticalPadding="2px" />
                        </asp:TreeView><asp:TreeView ID="tvMainMenu" runat="server" ImageSet="XPFileExplorer" ShowLines="True" NodeIndent="15">
                            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                                VerticalPadding="0px" />
                            <Nodes>
                                <asp:TreeNode Text="智能交互平台" Value="智能交互平台" Expanded="True" ShowCheckBox="False" SelectAction="Expand">
                                </asp:TreeNode>
                            </Nodes>
                            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                NodeSpacing="0px" VerticalPadding="2px" />
                        </asp:TreeView>
                    </td>
                </tr>
            </table>
            &nbsp;</div>
    </form>
</body>
</html>
