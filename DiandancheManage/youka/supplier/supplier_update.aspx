<%@ Page Language="C#" AutoEventWireup="true" CodeFile="supplier_update.aspx.cs" Inherits="youka_supplier_supplier_update" %>

<%@ Register Src="../../module/My97DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<html>
<head id="Head1" runat="server">
    <title>修改基本信息</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/bbs.gif" />
                            <asp:Label ID="lblTitle" runat="server" Text="修改基本信息"></asp:Label>
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
                            <td>&nbsp;<asp:TextBox ID="Name" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>手机号：</td>
                            <td>&nbsp;<asp:TextBox ID="Phone" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>旧的优惠价：</td>
                            <td>&nbsp;<asp:TextBox ID="Concessional" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>新的优惠价：</td>
                            <td>&nbsp;<asp:TextBox ID="NewConcessional" runat="server" /></td>
                        </tr>
                         <tr>
                            <td>时间点：</td>
                            <td>&nbsp;<uc1:DatePicker ID="ConcessionalPointTime" runat="server" ShowDateFmt="yyyy-MM-dd HH:mm:ss" /></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Button ID="btnOK" runat="server" Text="修改" OnClientClick="return confirm('您确认修改信息吗?')"
                                    OnClick="btnOK_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text=" 返回 " OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </div>
            <br /> 

        </div>
    </form>
</body>
</html>


