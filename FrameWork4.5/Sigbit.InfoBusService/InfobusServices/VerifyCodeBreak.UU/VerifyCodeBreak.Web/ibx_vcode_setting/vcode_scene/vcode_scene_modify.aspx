<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vcode_scene_modify.aspx.cs" Inherits="genui_LYOG_vcode_scene_modify" %>

<html>
<head runat="server">
    <title>��֤�볡������</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/house.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="��֤�볡������"></asp:Label></td>
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
                    &nbsp;<asp:TextBox ID="edtVcodeId" runat="server" Width="200px"></asp:TextBox><asp:Label
                        ID="lblVcodeId" runat="server" Text=""></asp:Label></td>
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
                    &nbsp;<asp:TextBox ID="edtVcodeDesc" runat="server" Width="300px" 
                        Height="50px" SkinID="multiLine" TextMode="MultiLine"></asp:TextBox></td>
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
            <tr>
                <td nowrap>
                    ��������ʣ�</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtCallRate" runat="server" Width="50px">100</asp:TextBox>&nbsp;%</td>
            </tr>
            <tr>
                <td nowrap>
                    αװʱ�ӣ�</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtCallFakeMinSec" runat="server" Width="50px"></asp:TextBox>
                    �� �� <asp:TextBox ID="edtCallFakeMaxSec" runat="server" Width="50px"></asp:TextBox>
                    ��</td>
            </tr>
            <tr>
                <td nowrap>
                    ǿ��ʱ�ӣ�</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtCallForceMinSec" runat="server" Width="50px"></asp:TextBox>
                    �� �� <asp:TextBox ID="edtCallForceMaxSec" runat="server" Width="50px"></asp:TextBox>
                    ��</td>
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
