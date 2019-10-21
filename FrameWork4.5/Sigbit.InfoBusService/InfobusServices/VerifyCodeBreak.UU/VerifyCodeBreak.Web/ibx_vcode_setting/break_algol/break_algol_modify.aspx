<%@ Page Language="C#" AutoEventWireup="true" CodeFile="break_algol_modify.aspx.cs" Inherits="genui_AURP_break_algol_modify" %>

<html>
<head runat="server">
    <title>��֤���ƽ��㷨����</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/key2.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="��֤���ƽ��㷨����"></asp:Label></td>
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
                    �㷨��ʶ��</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtAlgolId" runat="server" Width="200px"></asp:TextBox><asp:Label
                        ID="lblAlgolId" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �㷨���ƣ�</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtAlgolName" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    �㷨������</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtAlgolDesc" runat="server" Width="300px" TextMode="MultiLine" SkinID="multiLine" Height="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    ����һ��</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtAlgolData01" runat="server" Width="300px" TextMode="MultiLine" SkinID="multiLine" Height="50px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    ���ݶ���</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtAlgolData02" runat="server" Width="300px" TextMode="MultiLine" SkinID="multiLine" Height="50px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    ��������</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtAlgolData03" runat="server" Width="300px" TextMode="MultiLine" SkinID="multiLine" Height="50px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    �����ģ�</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtAlgolData04" runat="server" Width="300px" TextMode="MultiLine" SkinID="multiLine" Height="50px"></asp:TextBox></td>
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
