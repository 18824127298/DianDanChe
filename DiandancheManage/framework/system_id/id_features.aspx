<%@ Page Language="C#" AutoEventWireup="true" CodeFile="id_features.aspx.cs" Inherits="vacc_controller_controller_setting_plan_trigger_setting" %>

<html>
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" width="100%" cellspacing="0" cellpadding="3">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/vchat.gif" width="16" height="16" align="absmiddle">
                        系统标识配置
                    </td>
                </tr>
            </table>
            <hr />
        </div>
        <br />
        <div class="contentTable">
            <table border="0" width="500" cellpadding="4" cellspacing="1" align="center">
                <thead>
                    <tr>
                        <td colspan="2">
                            系统标识信息配置
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td colspan="2">
                            <strong>用户标识及名称</strong>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px">
                            用户标识：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtCustomerCode" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px">
                            用户全称：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtCustomerFullName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px">
                            用户简称：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtCustomerBriefName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <strong>系统名称</strong>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px">
                            系统全称：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtSystemFullName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="100px">
                            系统简称：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtSystemBriefName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr align="center">
                        <td nowrap colspan="2">
                            <asp:Button ID="btnOK" runat="server" Text=" 确定 " OnClick="btnOK_Click" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
        <table width="350" align="center">
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
