<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu_list.aspx.cs" Inherits="framework_menu_navigate_menu_list" %>

<!--DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>菜单列表</title>
</head>
<body style="margin-top:15px;">
    <form id="form1" runat="server">
        <div>
            <asp:TreeView ID="tvMenuTree" runat="server" ImageSet="XPFileExplorer" NodeIndent="15"
                ShowLines="True">
                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                    VerticalPadding="0px" />
                <Nodes>
                    <asp:TreeNode Expanded="True" SelectAction="Expand" ShowCheckBox="False" Text="所有菜单"
                        Value="所有菜单" ImageUrl="~/images/menu_icon/star.gif" NavigateUrl="menu_new.aspx?mnu_uid="
                        Target="frameMenuConfig"></asp:TreeNode>
                </Nodes>
                <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                    NodeSpacing="0px" VerticalPadding="2px" />
                <RootNodeStyle ImageUrl="~/images/menu_icon/dept.gif" />
            </asp:TreeView>
        </div>
    </form>
</body>
</html>
