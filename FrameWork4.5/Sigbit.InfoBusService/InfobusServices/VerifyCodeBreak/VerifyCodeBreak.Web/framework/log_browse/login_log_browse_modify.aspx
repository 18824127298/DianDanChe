<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login_log_browse_modify.aspx.cs" Inherits="genui_DITJ_login_log_browse_modify" %>

<html>
<head runat="server">
    <title>��¼��־����</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/inbox.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="��¼��־����"></asp:Label></td>
                </tr>
            </table>
            <hr />
        </div>

        <div class="contentTable">
        <br />
        <table border="0" width="450" cellpadding="6" cellspacing="1" align="center">
        <tbody>
            <tr>
                <td nowrap>
                    ��¼ʱ�䣺</td>
                <td>
                    &nbsp;<asp:Label ID="lblLoginTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �˳�ʱ�䣺</td>
                <td>
                    &nbsp;<asp:Label ID="lblLogoutTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ʱ����</td>
                <td>
                    &nbsp;<asp:Label ID="lblInSystemDuration" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �û�����</td>
                <td>
                    &nbsp;<asp:Label ID="lblUserName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ��¼�����</td>
                <td>
                    &nbsp;<asp:Label ID="lblLoginResult" runat="server"></asp:Label></td>
            </tr>
            <tr runat="server" id="trLogoutDesc">
                <td nowrap>
                    Ӧ���˳���</td>
                <td>
                    &nbsp;<asp:Label ID="lblLogoutDesc" runat="server"></asp:Label></td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    <asp:Button ID="btnCancel" runat="server" Text="����" OnClick="btnCancel_Click" /></td>
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
