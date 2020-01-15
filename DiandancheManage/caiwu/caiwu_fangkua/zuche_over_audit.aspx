<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zuche_over_audit.aspx.cs" Inherits="caiwu_caiwu_fangkua_zuche_over_audit" %>

<html>
<head id="Head1" runat="server">
    <title>租车结清审核</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/ruler2.gif" />
                            <asp:Label ID="lblTitle" runat="server" Text="租车结清审核"></asp:Label></td>
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
                            <td nowrap>车架号：</td>
                            <td>&nbsp;<asp:Label ID="lblBicycleNumber" runat="server"></asp:Label></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr align="center">
                            <td colspan="2" nowrap>&nbsp;<asp:Button ID="btnFangKuang" runat="server" Text=" 结 清 " OnClientClick="return confirm('您确认进行结清吗?')" OnClick="btnFangKuang_Click" />
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
