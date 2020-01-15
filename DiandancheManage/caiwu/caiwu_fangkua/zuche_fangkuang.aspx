<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zuche_fangkuang.aspx.cs" Inherits="caiwu_caiwu_fangkua_zuche_fangkuang" %>

<html>
<head id="Head1" runat="server">
    <title>放款</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/ruler2.gif" />
                            <asp:Label ID="lblTitle" runat="server" Text="放款"></asp:Label></td>
                    </tr>
                </table>
                <hr />
            </div>

            <div class="contentTable">
                <br />
                <table border="0" width="600" cellpadding="8" cellspacing="1" align="center">
                    <tbody>
                        <tr>
                            <td nowrap style="width: 20%">用户：</td>
                            <td>&nbsp;<asp:Label ID="lblFullname" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td nowrap>品牌：</td>
                            &nbsp;<td>&nbsp;<asp:DropDownList ID="Brand" runat="server">
                                <Items>
                                    <asp:ListItem Value='康景'>康景</asp:ListItem>
                                    <asp:ListItem Value='愉途'>愉途</asp:ListItem>
                                    <asp:ListItem Value='巨高'>巨高</asp:ListItem>
                                    <asp:ListItem Value='雅迪'>雅迪</asp:ListItem>
                                    <asp:ListItem Value='相悦'>相悦</asp:ListItem>
                                </Items>
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td nowrap>型号：</td>
                            <td>&nbsp;<asp:DropDownList ID="CheType" runat="server">
                                <Items>
                                    <asp:ListItem Value='TDT071Z'>TDT071Z</asp:ListItem>
                                    <asp:ListItem Value='TDR1033Z'>TDR1033Z</asp:ListItem>
                                    <asp:ListItem Value='外卖车'>外卖车</asp:ListItem>
                                </Items>
                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>车架号：</td>
                            <td>&nbsp;<asp:TextBox ID="BicycleNumber" runat="server" Width="450px" Rows="3" />
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr align="center">
                            <td colspan="2" nowrap>&nbsp;<asp:Button ID="btnFangKuang" runat="server" Text=" 放 款 " OnClientClick="return confirm('您确认进行放款吗?')" OnClick="btnFangKuang_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text=" 返 回 " /></td>
                        </tr>
                    </tfoot>
                </table>
            </div>
            <br />
            <table width="450" align="center">
                <tr>
                    <td>&nbsp;<asp:Label ID="lblErrMessage" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
