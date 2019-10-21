<%@ Page Language="C#" AutoEventWireup="true" CodeFile="role_manage_modify.aspx.cs" Inherits="genui_ETJJ_role_manage_modify" %>

<html>
<head runat="server">
    <title>用户角色信息属性修改</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/netmeeting.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="用户角色属性"></asp:Label></td>
                </tr>
            </table>
            <hr />
        </div>

        <div class="contentTable">
        <br />
        <table border="0" width="450" cellpadding="4" cellspacing="1" align="center">
        <tbody>
            <tr>
                <td nowrap>
                    角色名称：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtRoleName" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    备注：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtRemarks" runat="server" Width="300px" Height="100px" SkinID="multiLine" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    &nbsp;<asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click" />
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" /></td>
            </tr>
        </tfoot>
        </table>
        </div>
        <br/>
        <table width="450" align="center">
            <tr>
                <td>
                    &nbsp;<asp:Label ID="lblErrMessage" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
