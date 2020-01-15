<%@ Page Language="C#" AutoEventWireup="true" CodeFile="deposit_refund_audit.aspx.cs" Inherits="youka_pay_deposit_refund_audit" %>

<html>
<head id="Head1" runat="server">
    <title>用户金额回退</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/ruler2.gif" />
                            <asp:Label ID="lblTitle" runat="server" Text="用户金额回退"></asp:Label></td>
                    </tr>
                </table>
                <hr />
            </div>

            <div class="contentTable">
                <br />
                <table border="0" width="600" cellpadding="8" cellspacing="1" align="center">
                    <tbody>
                        <tr>
                            <td nowrap style="width: 20%">用户：</td>
                            <td>&nbsp;<asp:Label ID="lblPhone" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td nowrap>金额：</td>
                            <td>&nbsp;<asp:Label ID="lblAmount" runat="server"></asp:Label>
                                <input type="hidden" value="0" id="vid" name="vid" runat="server">
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>备注：</td>
                            <td>&nbsp;<asp:TextBox ID="edtRemark" runat="server" Width="450px" SkinID="MultiLine" TextMode="MultiLine" Rows="3" />
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr align="center">
                            <td colspan="2" nowrap>&nbsp;<input type="button" value="确定" onclick="fn_Sure()" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text=" 返 回 " OnClick="btnCancel_Click" /></td>
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
    <script src="../../js/jquery-1.8.2.js"></script>
    <script type="text/javascript">
        function fn_Sure() {
            $.ajax({
                type: 'get',
                url: 'deposit_refund_audit.aspx?r=' + Math.random(),
                data: {
                    Action: 'Save',
                    'Id': $("#vid").val(),
                    'Amount': $("#<%=lblAmount.ClientID%>").text(),
                    'Remark': $("#<%=edtRemark.ClientID%>").val()
                },
                success: function (data) {
                    alert(data);
                    if (data == "退款操作成功") {
                        location.href = "gasstation_success_list.aspx";
                    }
                }
            });
        }
    </script>
</body>
</html>
