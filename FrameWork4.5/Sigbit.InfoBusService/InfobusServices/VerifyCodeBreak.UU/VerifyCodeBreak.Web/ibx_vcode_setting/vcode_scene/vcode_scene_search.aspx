<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vcode_scene_search.aspx.cs" Inherits="genui_LYOG_vcode_scene_search" %>

<html>
<head runat="server">
    <title>������֤�볡��</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/house.gif" />
                        ������֤�볡��</td>
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
                    ��֤���ʶ��</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtVcodeId" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    ��֤�����ƣ�</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtVcodeName" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    ������</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtVcodeDesc" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    �㷨��</td>
                <td>
                    &nbsp;<asp:DropDownList ID="ddlbAlgolId" runat="server" Width="200px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td nowrap>
                    �㷨������</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtAlgolParams" runat="server" Width="200px"></asp:TextBox></td>
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
