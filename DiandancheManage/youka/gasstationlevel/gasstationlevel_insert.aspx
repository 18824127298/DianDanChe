<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gasstationlevel_insert.aspx.cs" Inherits="youka_gasstationlevel_gasstationlevel_insert" %>

<%@ Register Src="../../module/My97DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<html>
<head id="Head1" runat="server">
    <title>加油站等级优惠</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/bbs.gif" />
                            <asp:Label ID="lblTitle" runat="server" Text="加油站等级优惠"></asp:Label>
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
                            <td>会员的等级：</td>
                            <td>&nbsp;<asp:TextBox ID="MemberLevel" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>旧的优惠额：</td>
                            <td>&nbsp;<asp:TextBox ID="Reduction" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>新的优惠额：</td>
                            <td>&nbsp;<asp:TextBox ID="NewReduction" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>时间点：</td>
                            <td>&nbsp;<uc1:datepicker id="ReductionTime" runat="server" showdatefmt="yyyy-MM-dd HH:mm:ss" />
                            </td>
                        </tr>

                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Button ID="btnOK" runat="server" Text="新增" OnClientClick="return confirm('您确认新增优惠吗?')"
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
