<%@ Page Language="C#" AutoEventWireup="true" CodeFile="loan_tihuan.aspx.cs" Inherits="caiwu_caiwu_borrower_loan_tihuan" %>

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
                            <asp:Label ID="lblTitle" runat="server" Text="提还"></asp:Label></td>
                    </tr>
                </table>
                <hr />
            </div>

            <div class="contentTable">
                <table border="0" width="450" cellpadding="8" cellspacing="1" align="center">
                    <thead>
                        <tr>
                            <td colspan="2" align="center">提还金额详情
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
                            <td>总借款金额：
                           <asp:Label ID="lblAmount" runat="server"></asp:Label>元
                            </td>
                        </tr>
                        <tr>
                            <td>业务员：
                           <asp:Label ID="lblSalesman" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>未还总融资租赁总额：
                          <asp:Label ID="lblunSumPrincipal" runat="server"></asp:Label>元
                            </td>
                        </tr>
                        <tr>
                            <td>手续费：
                           <asp:Label ID="lblServiceCharge" runat="server"></asp:Label>元
                            </td>
                        </tr>
                        <tr>
                            <td>用户的余额：
                          <asp:Label ID="lblBalance" runat="server"></asp:Label>元
                            </td>
                        </tr>
                        <tr>
                            <td>减免额：
                          <asp:Label ID="lblStandardDeduction" runat="server"></asp:Label>元
                            </td>
                        </tr>
                        <tr>
                            <td>支付总金额：
                           <asp:TextBox ID="edtSumAmount" runat="server"></asp:TextBox>元
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr align="center">
                            <td colspan="2" nowrap>
                                <asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" OnClientClick="return confirm('确认提还吗?')" Text="提还" /></td>
                        </tr>
                    </tfoot>
                </table>
            </div> 
        </div>
    </form>
</body>
</html>
