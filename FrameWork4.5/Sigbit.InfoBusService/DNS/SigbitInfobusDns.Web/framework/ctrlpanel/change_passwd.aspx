<%@ Page Language="C#" AutoEventWireup="true" CodeFile="change_passwd.aspx.cs" Inherits="framework_ctrlpanel_change_passwd" %>

<html>
<head runat="server">
    <title>修改密码</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" width="100%" cellspacing="0" cellpadding="3">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/workflow.gif" width="16" height="16" align="absmiddle">
                            <span>密码修改</span>
                        </td>
                    </tr>
                </table>
                <hr>
            </div>
            <div align="center" class="contentTable">
                <table width="300" id="tblpwd" border="0" cellpadding="4" cellspacing="1">
                    <tbody>
                        <tr>
                            <td>
                                原密码：
                            </td>
                            <td>
                                <asp:TextBox ID="edtOldPassword" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                新密码：
                            </td>
                            <td>
                                <asp:TextBox ID="edtNewPassword" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                密码确认：
                            </td>
                            <td>
                                <asp:TextBox ID="edtNewPasswordOK" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="normalButton" Text="提交修改" OnClick="btnSubmit_Click" />
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        <table width="300" align="center">
        <tr><td>&nbsp;<asp:Label ID="lblErrMessage" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label></td></tr>
        </table>
        </div>
    </form>
</body>
</html>
