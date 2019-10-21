<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sms_send.aspx.cs" Inherits="webmanage_sms_sms_send" %>

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
                            <asp:Label ID="lblTitle" runat="server" Text="客户发送短信"></asp:Label></td>
                    </tr>
                </table>
                <hr /> 
            </div>

            <div class="contentTable">
                <table border="0" width="450" cellpadding="8" cellspacing="1" align="center">
                    <thead>
                        <tr>
                            <td colspan="2" align="center">客户发送短信
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>手机号码：
                            </td>
                            <td>&nbsp;<asp:TextBox ID="edtPhone" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>短信模板：
                            </td>
                            <td>&nbsp;
                                <asp:DropDownList ID="ddlMuBan" onchange="fn_MuBan(this)" Width="300px" runat="server">
                                    <asp:ListItem Text="请选择" Value="请选择"></asp:ListItem>
                                    <asp:ListItem Text="尊敬的{0}客户您好，您在广州翼速融资租赁有限公司办理的电单车融资租赁业务已全部结清，感谢您对我们工作的支持。详询020-89851216【车1号】"
                                        Value="尊敬的{0}客户您好，您在广州翼速融资租赁有限公司办理的电单车融资租赁业务已全部结清，感谢您对我们工作的支持。详询020-89851216【车1号】"></asp:ListItem>
                                    <asp:ListItem Text="恭喜您，本次还款成功，还款金额为{0}元，订单号{1}" Value="恭喜您，本次还款成功，还款金额为{0}元，订单号{1}"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>短信内容：
                            </td>
                            <td>&nbsp;
                          <asp:TextBox ID="edtContent" runat="server" Width="100%" SkinID="MultiLine" TextMode="MultiLine" Rows="5" Text="" />
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr align="center">
                            <td colspan="2" nowrap>&nbsp;<asp:Button ID="btnOK" runat="server" Text=" 确 定 " OnClick="btnOK_Click" />
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </div>
            <br />
            <table width="450" align="center">
                <tr>
                    <td>&nbsp;<asp:Label ID="lblErrMessage" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
<script src="../../js/jquery-1.8.2.min.js"></script>
<script>
    function fn_MuBan(curObj) {
        var selectValue = curObj.options[curObj.selectedIndex].value;
        $("#edtContent").val(selectValue);
    }
</script>
