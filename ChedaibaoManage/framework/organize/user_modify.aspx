<%@ Page Language="C#" AutoEventWireup="true" CodeFile="user_modify.aspx.cs" Inherits="genui_CRPN_user_modify" %>

<html>
<head runat="server">
    <title>用户属性</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/netchat.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="用户属性"></asp:Label>
                    </td>
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
                            帐号：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtUserName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            姓名：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtRealName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            密码：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtPassword" runat="server" Width="200px">***** ***** ***** *****</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            所在地市：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtAreaName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            固定电话：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtTelephone" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            移动电话：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtMobilePhone" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            联系邮箱：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtEmail" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            性别：
                        </td>
                        <td>
                            &nbsp;<asp:DropDownList ID="ddlbSex" runat="server" Width="50px">
                                <asp:ListItem Value="M">男</asp:ListItem>
                                <asp:ListItem Value="F">女</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            角色：
                        </td>
                        <td>
                            &nbsp;<asp:DropDownList ID="ddlbRole" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            状态：
                        </td>
                        <td>
                            &nbsp;<asp:DropDownList ID="ddlbIsActive" runat="server" Width="150px">
                                <asp:ListItem Value="Y">激活</asp:ListItem>
                                <asp:ListItem Value="N">禁用</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <asp:Panel ID="pnlAdminArea" runat="server">
                        <tr>
                            <td nowrap>
                            </td>
                            <td>
                                &nbsp;<asp:CheckBox ID="cbxIsAdmin" runat="server" Text="超级管理员" />
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td nowrap>
                            备注：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtRemarks" runat="server" Width="300px" Height="50px" SkinID="multiLine"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr align="center">
                        <td colspan="2" nowrap>
                            &nbsp;<asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click" />
                            &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
        <table width="450" align="center">
            <tr>
                <td>
                    &nbsp;<asp:Label ID="lblErrMessage" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
