<%@ Page Language="C#" AutoEventWireup="true" CodeFile="authen_user_search.aspx.cs" Inherits="genui_EEQY_authen_user_search" %>

<html>
<head runat="server">
    <title>������Ȩ�û�</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/payment.gif" />
                        ������Ȩ�û�</td>
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
                    �û�����</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtAuthenUserName" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    ��ע��</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtRemarks" runat="server" Width="200px"></asp:TextBox></td>
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
