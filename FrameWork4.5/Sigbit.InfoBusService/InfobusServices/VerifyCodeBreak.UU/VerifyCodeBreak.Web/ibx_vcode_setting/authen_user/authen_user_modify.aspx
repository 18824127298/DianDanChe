<%@ Page Language="C#" AutoEventWireup="true" CodeFile="authen_user_modify.aspx.cs" Inherits="genui_EEQY_authen_user_modify" %>

<html>
<head runat="server">
    <title>验证码授权用户属性</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/payment.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="验证码授权用户属性"></asp:Label></td>
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
                    授权帐号：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtAuthenUserName" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    密码：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtAuthenPassword" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    分钟限制：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtLimitPerMinute" runat="server" Width="50px"></asp:TextBox> 次</td>
            </tr>
            <tr>
                <td nowrap>
                    小时限制：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtLimitPerHour" runat="server" Width="50px"></asp:TextBox> 次</td>
            </tr>
            <tr>
                <td nowrap>
                    每日限制：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtLimitPerDay" runat="server" Width="50px"></asp:TextBox> 次</td>
            </tr>
            <tr>
                <td nowrap>
                    备注：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtRemarks" runat="server" Width="300px" TextMode="MultiLine" SkinID="multiLine" Height="100px"></asp:TextBox></td>
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
