<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log_tcp_modify.aspx.cs" Inherits="genui_VIZP_log_tcp_modify" %>

<html>
<head runat="server">
    <title>通信消息日志详情</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/note.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="通信消息日志详情"></asp:Label></td>
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
                    命令字：</td>
                <td>
                    &nbsp;<asp:Label ID="lblCommandIdEng" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap width="125">
                    消息名称：</td>
                <td>
                    &nbsp;<asp:Label ID="lblCommandIdChs" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    消息时间：</td>
                <td>
                    &nbsp;<asp:Label ID="lblRequestTime" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    响应时长：</td>
                <td>
                    &nbsp;<asp:Label ID="lblCallDuration" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    请求数据包：</td>
                <td class="wordBreak">
                    &nbsp;<asp:Label ID="lblRequestPacket" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    请求内容：</td>
                <td>
                    &nbsp;<asp:Label ID="lblRequestDesc" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    响应数据包：</td>
                <td class="wordBreak">
                    &nbsp;<asp:Label ID="lblResponsePacket" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    响应内容：</td>
                <td>
                    &nbsp;<asp:Label ID="lblResponseDesc" runat="server" Text="Label"></asp:Label></td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text=" 返回 " OnClick="btnCancel_Click" /></td>
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
