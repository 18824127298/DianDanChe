<%@ Page Language="C#" AutoEventWireup="true" CodeFile="first_discount_audit.aspx.cs" Inherits="loan_loan_discount_first_discoun_audit" %>


<html>
<head id="Head1" runat="server">
    <title>减免金额初审</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/ruler2.gif" />
                            <asp:Label ID="lblTitle" runat="server" Text="减免金额初审"></asp:Label></td>
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
                            <td nowrap>金额：</td>
                            <td>&nbsp;<asp:Label ID="lblAmount" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td nowrap>提单人：</td>
                            <td>&nbsp;<asp:Label ID="lblCreator" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td nowrap>提单时间：</td>
                            <td>&nbsp;<asp:Label ID="lblCreateTime" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td nowrap>客户减免原因：</td>
                            <td>&nbsp;<asp:Label ID="lblRemark" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td nowrap>审核备注：</td>
                            <td>&nbsp;<asp:TextBox ID="edtAuditRemark" runat="server" Width="450px" SkinID="MultiLine" TextMode="MultiLine" Rows="3" />
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr align="center">
                            <td colspan="2" nowrap>&nbsp;<asp:Button ID="btnAudit" runat="server" Text="审核通过" OnClick="btnAudit_Click" OnClientClick="return confirm('您确认进行审核操作吗?')" />
                                &nbsp; &nbsp;
                                <asp:Button ID="btnUnAudit" runat="server" Text="审核不通过" OnClick="btnUnAudit_Click" OnClientClick="return confirm('您确认审核不通过吗?')" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text=" 返 回 " OnClick="btnCancel_Click" /></td>
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
