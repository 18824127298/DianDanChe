<%@ Page Language="C#" AutoEventWireup="true" CodeFile="authen_user_modify.aspx.cs" Inherits="genui_EEQY_authen_user_modify" %>

<html>
<head runat="server">
    <title>��֤����Ȩ�û�����</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/payment.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="��֤����Ȩ�û�����"></asp:Label></td>
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
                    ��Ȩ�ʺţ�</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtAuthenUserName" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    ���룺</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtAuthenPassword" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    �������ƣ�</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtLimitPerMinute" runat="server" Width="50px"></asp:TextBox> ��</td>
            </tr>
            <tr>
                <td nowrap>
                    Сʱ���ƣ�</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtLimitPerHour" runat="server" Width="50px"></asp:TextBox> ��</td>
            </tr>
            <tr>
                <td nowrap>
                    ÿ�����ƣ�</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtLimitPerDay" runat="server" Width="50px"></asp:TextBox> ��</td>
            </tr>
            <tr>
                <td nowrap>
                    ��ע��</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtRemarks" runat="server" Width="300px" TextMode="MultiLine" SkinID="multiLine" Height="100px"></asp:TextBox></td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    &nbsp;<asp:Button ID="btnOK" runat="server" Text="ȷ��" OnClick="btnOK_Click" />
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="ȡ��" OnClick="btnCancel_Click" /></td>
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
