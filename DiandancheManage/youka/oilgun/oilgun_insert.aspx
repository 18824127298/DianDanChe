<%@ Page Language="C#" AutoEventWireup="true" CodeFile="oilgun_insert.aspx.cs" Inherits="youka_oilgun_oilgun_insert" %>

<%@ Register Src="../../module/My97DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<html>
<head id="Head1" runat="server">
    <title>加油站油价编辑</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/bbs.gif" />
                            <asp:Label ID="lblTitle" runat="server" Text="油价"></asp:Label>
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
                        <td>商品：</td>
                            &nbsp;<td>&nbsp;<asp:DropDownList ID="ddlOilNumber" runat="server">
                                <Items>
                                    <asp:ListItem Value='0#柴油'>0#柴油</asp:ListItem>
                                    <asp:ListItem Value='92#汽油'>92#汽油</asp:ListItem>
                                    <asp:ListItem Value='95#汽油'>95#汽油</asp:ListItem>
                                    <asp:ListItem Value='98#汽油'>98#汽油</asp:ListItem>
                                </Items>
                            </asp:DropDownList></td>
                        <tr>
                            <td>枪号：</td>
                            <td>&nbsp;<asp:TextBox ID="GunNumber" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>旧价格：</td>
                            <td>&nbsp;<asp:TextBox ID="Amount" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>新价格：</td>
                            <td>&nbsp;<asp:TextBox ID="NewAmount" runat="server" /></td>
                        </tr>
                         <tr>
                            <td>时间点：</td>
                            <td>&nbsp;<uc1:DatePicker ID="PointTime" runat="server" ShowDateFmt="yyyy-MM-dd HH:mm:ss" /></td>
                        </tr>
                        <tr>
                            <td>国标价：</td>
                            <td>&nbsp;<asp:TextBox ID="CountryMarkPrice" runat="server" /></td>
                        </tr>
                       <tr>
                            <td>新的国标价：</td>
                            <td>&nbsp;<asp:TextBox ID="NewCountryPrice" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>国标价时间点：</td>
                            <td>&nbsp;<uc1:DatePicker ID="CountryPointTime" runat="server" ShowDateFmt="yyyy-MM-dd HH:mm:ss" /></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Button ID="btnOK" runat="server" Text="新增" OnClientClick="return confirm('您确认新增加油站吗?')"
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


