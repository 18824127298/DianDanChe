<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dns_trans_code_modify.aspx.cs" Inherits="genui_LNWX_dns_trans_code_modify" %>

<html>
<head runat="server">
    <title>������������</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/plug.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="������������"></asp:Label></td>
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
                    �����룺</td>
                <td>
                    &nbsp;<asp:Label ID="lblTransCode" runat="server" Text="Label"></asp:Label><asp:TextBox ID="edtTransCode" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    ���ƣ�</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtTransCodeName" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    Ѱַ����</td>
                <td>
                    &nbsp;<asp:DropDownList ID="ddlbServiceId" runat="server" Width="200px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td nowrap>
                    ������</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtTransCodeDesc" runat="server" Width="300px" TextMode="MultiLine" SkinID="multiLine" Height="75px"></asp:TextBox></td>
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
