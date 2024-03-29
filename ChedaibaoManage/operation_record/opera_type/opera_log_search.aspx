<%@ Page Language="C#" AutoEventWireup="true" CodeFile="opera_log_search.aspx.cs" Inherits="genui_UZAY_opera_log_search" %>

<html>
<head runat="server">
    <title>搜索列表</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/attendance.gif" />
                        搜索列表</td>
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
                    用户：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtGodid" runat="server" Width="200px"></asp:TextBox>
                    <asp:Button ID="btnHuoQu" runat="server" Text="获取用户" OnClick="btnHuoQu_Click" /><br />
                    &nbsp;<asp:DropDownList ID="ddlbGodName" runat="server" Width="200px"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td nowrap>
                    备注：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtRemark" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    &nbsp;<asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click" />
                    &nbsp; &nbsp; &nbsp;
                    <input id="btnCancel" type="button" value="取消" language="javascript" onclick="history.back()" class="normalButton" /></td>
            </tr>
        </tfoot>
        </table>
        </div>
    </div>
    </form>
</body>
</html>
