<%@ Page Language="C#" AutoEventWireup="true" CodeFile="other_discount_insert.aspx.cs" Inherits="loan_loan_discount_other_discount_insert" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/comm.gif" />
                            <asp:Label ID="lblTitle" runat="server" Text="新增减免金额"></asp:Label></td>
                    </tr>
                </table>
                <hr /> 
            </div>

            <div class="contentTable">
                <table border="0" width="450" cellpadding="8" cellspacing="1" align="center">
                    <thead>
                        <tr>
                            <td colspan="2" align="center">新增减免金额
                            </td>
                        </tr>
                    </thead>
                    <tbody id="btnhtml">
                        <tr>
                            <td>手机号：
                            <asp:Label ID="lblPhone" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>姓名：
                            <asp:Label ID="lblName" runat="server"></asp:Label>
                            </td>
                        </tr> 
                        <tr>
                            <td>总金额：
                           <asp:Label ID="lblAmount" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>业务员：
                           <asp:Label ID="lblSalesman" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>减免额：
                           <asp:TextBox ID="edtAmount" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>减免原因说明：
                           <asp:TextBox ID="edtRemark" runat="server" Width="100%" SkinID="MultiLine" TextMode="MultiLine" Rows="3" />
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr align="center">
                            <td colspan="2" nowrap>
                                <asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" OnClientClick="return confirm('确认为该用户减免金额吗?')" Text="新增" /></td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
