<%@ Page Language="C#" AutoEventWireup="true" CodeFile="operation_log_browse_modify.aspx.cs" Inherits="genui_PAIM_operation_log_browse_modify" %>

<html>
<head runat="server">
    <title>ά����־��ϸ��Ϣ</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/browse.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="ά����־��ϸ��Ϣ"></asp:Label></td>
                </tr>
            </table>
            <hr />
        </div>

        <div class="contentTable">
        <br />
        <table border="0" width="450" cellpadding="8" cellspacing="1" align="center">
        <tbody>
            <tr>
                <td nowrap>
                    ����Ա��</td>
                <td>
                    &nbsp;<asp:Label ID="lblUserName" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ʱ�䣺</td>
                <td>
                    &nbsp;<asp:Label ID="lblProcTime" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �������ͣ�</td>
                <td>
                    &nbsp;<asp:Label ID="lblProcClassName" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �����ͣ�</td>
                <td>
                    &nbsp;<asp:Label ID="lblProcSubclassName" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ������</td>
                <td>
                    &nbsp;<asp:Label ID="lblActionName" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ����������</td>
                <td>
                    <asp:Label ID="lblActionDescription" runat="server" Text="Label"></asp:Label>
                    <asp:TextBox ID="memoActionDescription" runat="server" Visible="false" TextMode="MultiLine" Width="350px" Height="125px" SkinID="multiLine"></asp:TextBox></td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    <asp:Button ID="btnModifyDescription" runat="server" Text="�޸Ĳ�������" OnClick="btnModifyDescription_Click" /><asp:Button ID="btnSaveDescription" runat="server" Text="�����������" Visible="false" OnClick="btnSaveDescription_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text=" �� �� " OnClick="btnCancel_Click" /></td>
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
