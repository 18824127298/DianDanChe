<%@ Page Language="C#" AutoEventWireup="true" CodeFile="switch_sub_system.aspx.cs" Inherits="framework_menu_manage_switch_sub_system" %>

<html>
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/plug.gif" />
                            切换子系统</td>
                    </tr>
                </table>
                <hr />
            </div>
            
            <br />
            <br />
            
            <div class="contentTable">
                <table border="0" width="450" cellpadding="4" cellspacing="1" align="center">
                    <tbody>
                        <tr>
                            <td nowrap>
                                目标子系统：
                            </td>
                            <td>
                                &nbsp;<asp:DropDownList ID="ddlbSubSystem" runat="server" Width="250px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr align="center">
                            <td colspan="2" nowrap>
                                &nbsp;<asp:Button ID="btnSwitch" runat="server" Text="切换子系统" 
                                    onclick="btnSwitch_Click" /></td>
                        </tr>
                    </tfoot>
                </table>
            </div>
            <br />
            <table width="450" align="center">
                <tr>
                    <td>
                        &nbsp;<asp:Label ID="lblErrMessage" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label></td>
                </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
