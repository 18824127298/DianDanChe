<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vcode_scene_modify.aspx.cs" Inherits="genui_LYOG_vcode_scene_modify" %>

<html>
<head runat="server">
    <title>验证码场景属性</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/house.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="验证码场景属性"></asp:Label></td>
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
                    验证码标识：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtVcodeId" runat="server" Width="200px"></asp:TextBox><asp:Label
                        ID="lblVcodeId" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    验证码名称：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtVcodeName" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    描述：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtVcodeDesc" runat="server" Width="300px" 
                        Height="50px" SkinID="multiLine" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    算法：</td>
                <td>
                    &nbsp;<asp:DropDownList ID="ddlbAlgolId" runat="server" Width="200px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td nowrap>
                    算法参数：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtAlgolParams" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    引擎调用率：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtCallRate" runat="server" Width="50px">100</asp:TextBox>&nbsp;%</td>
            </tr>
            <tr>
                <td nowrap>
                    伪装时延：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtCallFakeMinSec" runat="server" Width="50px"></asp:TextBox>
                    秒 至 <asp:TextBox ID="edtCallFakeMaxSec" runat="server" Width="50px"></asp:TextBox>
                    秒</td>
            </tr>
            <tr>
                <td nowrap>
                    强制时延：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtCallForceMinSec" runat="server" Width="50px"></asp:TextBox>
                    秒 至 <asp:TextBox ID="edtCallForceMaxSec" runat="server" Width="50px"></asp:TextBox>
                    秒</td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    &nbsp;<asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click" />
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" /></td>
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
