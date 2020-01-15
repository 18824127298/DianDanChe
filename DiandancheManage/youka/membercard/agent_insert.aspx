<%@ Page Language="C#" AutoEventWireup="true" CodeFile="agent_insert.aspx.cs" Inherits="youka_membercard_agent_insert" %>

<html>
<head id="Head1" runat="server">
    <title>代理信息编辑</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/bbs.gif" />
                            <asp:Label ID="lblTitle" runat="server" Text="代理信息编辑"></asp:Label>
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
                            <td>名字：</td>
                            <td>&nbsp;<asp:TextBox ID="Name" runat="server"></asp:TextBox></td>
                        </tr> 
                        <tr>
                            <td>手机号：</td>
                            <td>&nbsp;<asp:TextBox ID="Phone" runat="server" /></td>
                        </tr>
                        
                    </tbody> 
                    <tfoot>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Button ID="btnOK" runat="server" Text="新增" OnClientClick="return confirm('您确认新增吗
                                    ?')"
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

