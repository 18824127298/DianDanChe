<%@ Page Language="C#" AutoEventWireup="true" CodeFile="operatemodes_create.aspx.cs" Inherits="cdb_carillegal_cbd_car_operatemodes_create" %>

<html>
<head id="Head1" runat="server">
    <title>汽车类型录入</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/picture.gif" />
                            <asp:Label ID="lblTitle" runat="server" Text="会员汽车录入"></asp:Label>
                    </tr>
                </table>
                <hr />
            </div>
            <asp:HiddenField ID="HidMulti" runat="server" Value="false" />
            <div class="contentTable">
                <br />
                <table border="0" width="450" cellpadding="8" cellspacing="1" align="center">
                    <tbody>
                        <tr>
                            <td nowrap>运营车型名称：</td>
                            <td>&nbsp;
                                <asp:TextBox ID="edtName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        、
                    </tbody>
                    <tfoot>
                        <tr align="center">
                            <td colspan="2" nowrap>&nbsp;
                                <asp:Button ID="btn_sure" runat="server" Text="确定" OnClick="btn_sure_Click" />
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

