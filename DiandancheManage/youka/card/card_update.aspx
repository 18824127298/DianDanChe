<%@ Page Language="C#" AutoEventWireup="true" CodeFile="card_update.aspx.cs" Inherits="youka_card_card_update" %>

<html>
<head id="Head1" runat="server">
    <title>卡号信息编辑</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/bbs.gif" />
                            <asp:Label ID="lblTitle" runat="server" Text="卡号信息编辑"></asp:Label>
                        </td>
                    </tr>
                </table>
                <hr />
            </div>

            <div class="contentTable">
                <br />
                <table border="0" width="650" cellpadding="6" cellspacing="1" align="center">
                    <thead>
                        <tr align="center">
                            <td>属性
                            </td>
                            <td>信息
                            </td>

                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>卡号：</td>
                            <td>&nbsp;<asp:TextBox ID="CardNumber" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>卡折扣：</td>
                            <td>&nbsp;<asp:TextBox ID="CardDiscount" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>卡等级：</td>
                            <td>&nbsp;<asp:TextBox ID="CardLevel" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>卡提成：</td>
                            <td>&nbsp;<asp:TextBox ID="CardRoyalty" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>代理商：</td>
                            <td>&nbsp;<asp:DropDownList ID="ddlAgent" runat="server">
                                <Items>
                                    <asp:ListItem Value=''>请选择</asp:ListItem>
                                    </Items>
                            </asp:DropDownList></td>
                        </tr>
                         <tr>
                            <td>会员：</td>
                            <td>&nbsp;<asp:TextBox ID="Member" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>主账号：</td>
                            <td>&nbsp;<asp:TextBox ID="SupplierNumber" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>车牌号：</td>
                            <td>&nbsp;<asp:TextBox ID="CarNumber" runat="server" /></td>
                        </tr>
                         <tr>
                            <td>是否可以充值：</td>
                            <td>&nbsp;<asp:DropDownList ID="ddlIsRecharge" runat="server">
                                <Items>
                                    <asp:ListItem Value='True'>可充值</asp:ListItem>
                                    <asp:ListItem Value='False'>不可充</asp:ListItem>
                                </Items>
                            </asp:DropDownList></td>
                        </tr>
                         <tr>
                            <td>卡片的状态：</td>
                            <td>&nbsp;<asp:DropDownList ID="ddlCardStatus" runat="server">
                            </asp:DropDownList></td>
                        </tr>
                         <tr>
                            <td>所属的公司：</td>
                            <td>&nbsp;<asp:DropDownList ID="ddlCardBrand" runat="server">
                            </asp:DropDownList></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Button ID="btnOK" runat="server" Text=" 修 改 " OnClientClick="return confirm('您确认修改卡号信息吗?')"
                                    OnClick="btnOK_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text=" 返 回 " OnClick="btnCancel_Click" />
                            </td>
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


