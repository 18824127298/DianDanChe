<%@ Page Language="C#" AutoEventWireup="true" CodeFile="user_dept_tree.aspx.cs" Inherits="framework_organize_dept_list" %>

<!--DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>部门列表</title>
</head>
<body marginwidth="0" marginheight="0">
    <form id="form1" runat="server">
        <div>
            <asp:TreeView ID="tvDeptTree" runat="server" ImageSet="XPFileExplorer" NodeIndent="15"
                ShowLines="True">
                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                    VerticalPadding="0px" />
                <Nodes>
                    <asp:TreeNode Expanded="True" SelectAction="Expand" ShowCheckBox="False" Text="所有部门"
                        Value="所有部门" ImageUrl="~/images/menu_icon/star.gif" NavigateUrl="user_list.aspx?dpt_uid=" Target="user_main"></asp:TreeNode>
                </Nodes>
                <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                    NodeSpacing="0px" VerticalPadding="2px" />
                <RootNodeStyle ImageUrl="~/images/menu_icon/dept.gif" />
            </asp:TreeView>
        </div>
    </form>
</body>
</html>
