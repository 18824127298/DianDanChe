<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu.aspx.cs" Inherits="farmwork_main_function_panel_menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>���˵�</title>
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
                                <asp:TreeNode Text="���ܽ���ƽ̨" Value="���ܽ���ƽ̨" Expanded="True" ShowCheckBox="False" SelectAction="Expand">
                                    <asp:TreeNode ImageUrl="~/theme_images/main/menu/@system.gif" Text="��֯�ṹ" Value="��֯�ṹ" Expanded="False">
                                        <asp:TreeNode ImageUrl="~/theme_images/main/menu/sys.gif" Text="���Ź���" Value="���Ź���"></asp:TreeNode>
                                        <asp:TreeNode ImageUrl="~/theme_images/main/menu/sys.gif" Text="�г�����" Value="�г�����" NavigateUrl="dsf.aspx" Target="table_main"></asp:TreeNode>
                                        <asp:TreeNode ImageUrl="~/theme_images/main/menu/sys.gif" Text="�û�����" Value="�û�����"></asp:TreeNode>
                                        <asp:TreeNode ImageUrl="~/theme_images/main/menu/sys.gif" Text="�������" Value="�������"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode ImageUrl="~/theme_images/main/menu/hrms.gif" Text="������Դ" Value="������Դ" Expanded="False">
                                        <asp:TreeNode ImageUrl="~/theme_images/main/menu/@score.gif" Text="���ڹ���" Value="���ڹ���">
                                        </asp:TreeNode>
                                        <asp:TreeNode ImageUrl="~/theme_images/main/menu/hrms.gif" Text="���µ���" Value="���µ���">
                                        </asp:TreeNode>
                                        <asp:TreeNode ImageUrl="~/theme_images/main/menu/@salary.gif" Text="���ʹ���" Value="���ʹ���">
                                        </asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode ImageUrl="~/theme_images/main/table/erp.gif" Text="��������" Value="��������" Expanded="False">
                                        <asp:TreeNode ImageUrl="~/theme_images/main/table/notify.gif" Text="����֪ͨ����" Value="����֪ͨ����">
                                        </asp:TreeNode>
                                        <asp:TreeNode ImageUrl="~/theme_images/main/table/news.gif" Text="���Ź���" Value="���Ź���">
                                        </asp:TreeNode>
                                        <asp:TreeNode ImageUrl="~/theme_images/main/table/vote.gif" Text="ͶƱ����" Value="ͶƱ����">
                                        </asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode ImageUrl="~/theme_images/main/menu/attendance.gif" Text="ϵͳ����" Value="ϵͳ����" Expanded="False">
                                        <asp:TreeNode ImageUrl="~/theme_images/main/menu/attendance.gif" Text="ϵͳ��־" Value="ϵͳ��־">
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
                                <asp:TreeNode Text="���ܽ���ƽ̨" Value="���ܽ���ƽ̨" Expanded="True" ShowCheckBox="False" SelectAction="Expand">
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
