<%@ Page Language="C#" AutoEventWireup="true" CodeFile="system_define_search.aspx.cs" Inherits="genui_WSQV_system_define_search" %>

<html>
<head runat="server">
    <title>����ϵͳ����</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/comm.gif" />
                        ����ϵͳ����</td>
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
                    ϵͳ��ţ�</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtSubSystemId" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    ϵͳ���֣�</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtSubSystemName" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    &nbsp;<asp:Button ID="btnOK" runat="server" Text="ȷ��" OnClick="btnOK_Click" />
                    &nbsp; &nbsp; &nbsp;
                    <input id="btnCancel" type="button" value="ȡ��" language="javascript" onclick="history.back()" class="normalButton" /></td>
            </tr>
        </tfoot>
        </table>
        </div>
    </div>
    </form>
</body>
</html>
