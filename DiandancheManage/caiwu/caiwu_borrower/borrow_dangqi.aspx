<%@ Page Language="C#" AutoEventWireup="true" CodeFile="borrow_dangqi.aspx.cs" Inherits="caiwu_caiwu_borrower_borrow_dangqi" %>

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
                            <asp:Label ID="lblTitle" runat="server" Text="当期"></asp:Label></td>
                    </tr>
                </table>
                <hr />
            </div>

            <div class="contentTable">
                <table border="0" width="450" cellpadding="8" cellspacing="1" align="center">
                    <thead>
                        <tr>
                            <td colspan="2" align="center">当期金额详情
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
                            <td>总融资租赁金额：
                           <asp:Label ID="lblAmount" runat="server"></asp:Label>元
                            </td>
                        </tr>
                        <tr>
                            <td>业务员：
                           <asp:Label ID="lblSalesman" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>当期未还融资租赁总额：
                          <asp:Label ID="lblunSumPrincipal" runat="server"></asp:Label>元
                            </td>
                        </tr>
                        <tr>
                            <td>当期未还手续费：
                           <asp:Label ID="lblInterest" runat="server"></asp:Label>元
                            </td>
                        </tr>
                         <tr>
                            <td>减免额：
                          <asp:Label ID="lblStandardDeduction" runat="server"></asp:Label>元
                            </td>
                        </tr>
                        <tr>
                            <td>用户的余额：
                          <asp:Label ID="lblBalance" runat="server"></asp:Label>元 
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
                                <asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" OnClientClick="return confirm('确认支付吗?')" Text="确定" /></td>
                        </tr>
                    </tfoot>
                </table>
            </div> 
        </div>
    </form>
</body>
</html>
