<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gasstation_update.aspx.cs" Inherits="youka_gasstation_gasstation_update" %>

<html>
<head id="Head1" runat="server">
    <title>会员信息编辑</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/bbs.gif" />
                            <asp:Label ID="lblTitle" runat="server" Text="会员信息编辑"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="contentTable">
                <hr />
                <br />
                <table border="0" width="650" cellpadding="6" cellspacing="1" align="center">
                    <thead>
                        <tr align="center">
                            <td>用户属性
                            </td>
                            <td>信息
                            </td>

                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>加油站名字：</td>
                            <td>&nbsp;<asp:TextBox ID="Name" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>地址名：</td>
                            <td>&nbsp;<asp:TextBox ID="AddressName" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>经度：</td>
                            <td>&nbsp;<asp:TextBox ID="Longitude" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>纬度：</td>
                            <td>&nbsp;<asp:TextBox ID="Dimension" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>品牌：</td>
                            <td>&nbsp;<asp:TextBox ID="Brand" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>打印机编号：</td>
                            <td>&nbsp;<asp:TextBox ID="PrinterNumber" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>归属物流公司：</td>
                            <td>&nbsp;<asp:DropDownList ID="ddlSupplier" runat="server">
                            </asp:DropDownList></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Button ID="btnOK" runat="server" Text="修改" OnClientClick="return confirm('您确认修改信息吗?')"
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

