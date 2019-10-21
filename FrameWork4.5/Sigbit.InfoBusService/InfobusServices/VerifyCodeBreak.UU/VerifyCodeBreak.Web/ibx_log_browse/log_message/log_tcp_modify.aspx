<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log_tcp_modify.aspx.cs" Inherits="genui_VIZP_log_tcp_modify" %>

<html>
<head runat="server">
    <title>ͨ����Ϣ��־����</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/note.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="ͨ����Ϣ��־����"></asp:Label></td>
                </tr>
            </table>
            <hr />
        </div>

        <div class="contentTable">
        <br />
        <table border="0" width="600" cellpadding="4" cellspacing="1" align="center">
        <tbody>
            <tr>
                <td nowrap width="125">
                    �����֣�</td>
                <td>
                    &nbsp;<asp:Label ID="lblCommandIdEng" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap width="125">
                    ��Ϣ���ƣ�</td>
                <td>
                    &nbsp;<asp:Label ID="lblCommandIdChs" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ��Ϣʱ�䣺</td>
                <td>
                    &nbsp;<asp:Label ID="lblRequestTime" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ��Ӧʱ����</td>
                <td>
                    &nbsp;<asp:Label ID="lblCallDuration" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �������ݰ���</td>
                <td class="wordBreak">
                    &nbsp;<asp:Label ID="lblRequestPacket" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �������ݣ�</td>
                <td>
                    &nbsp;<asp:Label ID="lblRequestDesc" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ��Ӧ���ݰ���</td>
                <td class="wordBreak">
                    &nbsp;<asp:Label ID="lblResponsePacket" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ��Ӧ���ݣ�</td>
                <td>
                    &nbsp;<asp:Label ID="lblResponseDesc" runat="server" Text="Label"></asp:Label></td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text=" ���� " OnClick="btnCancel_Click" /></td>
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
