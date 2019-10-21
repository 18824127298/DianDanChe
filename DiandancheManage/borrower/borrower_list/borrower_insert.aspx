<%@ Page Language="C#" AutoEventWireup="true" CodeFile="borrower_insert.aspx.cs" Inherits="borrower_borrower_list_borrower_insert" %>

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
                <hr />
            </div>

            <div class="contentTable">
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
                            <td>商户名：</td>
                            <td>&nbsp;<asp:TextBox ID="edtFullName" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>手机号码：</td>
                            <td>&nbsp;<asp:TextBox ID="edtPhone" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>秘钥：</td>
                            <td>&nbsp;<asp:TextBox ID="edtLoginKey" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>是否业务员：</td>
                            <td>&nbsp;<asp:DropDownList ID="ddlIsSalesman" runat="server">
                                <Items>
                                    <asp:ListItem Selected='True' Value='true'>是</asp:ListItem>
                                    <asp:ListItem Selected='False' Value='false'>否</asp:ListItem>
                                </Items>
                            </asp:DropDownList></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Button ID="btnOK" runat="server" Text="新增" OnClientClick="return confirm('您确认新增用户吗
                                    ?')" OnClick="btnOK_Click" />
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

