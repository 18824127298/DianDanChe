<%@ Page Language="C#" AutoEventWireup="true" CodeFile="map_trans_code_url_search.aspx.cs" Inherits="genui_HUTL_map_trans_code_url_search" %>

<html>
<head runat="server">
    <title>搜索映射配置</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/email.gif" />
                        搜索映射配置</td>
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
                    寻址服务：</td>
                <td>
                    &nbsp;<asp:DropDownList ID="ddlbServiceId" runat="server" Width="200px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td nowrap>
                    交易码：</td>
                <td>
                    &nbsp;<asp:DropDownList ID="ddlbTransCode" runat="server" Width="200px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td nowrap>
                    服务地址：</td>
                <td>
                    &nbsp;<asp:DropDownList ID="ddlbUrlAddressUid" runat="server" Width="200px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td nowrap>
                    客户端标识：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtFromClientId" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    客户端版本：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtFromClientVersion" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    客户端系统：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtFromSystem" runat="server" Width="200px"></asp:TextBox></td>
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
