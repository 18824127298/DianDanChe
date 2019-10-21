<%@ Page Language="C#" AutoEventWireup="true" CodeFile="voice_compare_test.aspx.cs"
    Inherits="ibx_vcode_client___test_test___vcode_client_test___vcode_client" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>语音比对测试</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/vehicle.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="语音比对测试"></asp:Label>
                    </td>
                </tr>
            </table>
            <hr />
        </div>
        <div class="contentTable">
            <br />
            <table border="0" width="450" cellpadding="4" cellspacing="1" align="center">
                <tbody>
                    <tr>
                        <td nowrap>
                            基准音：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtStandVoice" runat="server" Width="200px" />
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            待比较语音：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtMatchVoice" runat="server" Text="" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            请求结果：
                        </td>
                        <td>
                            &nbsp;<asp:Label ID="lblRequestRET" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr align="center">
                        <td colspan="2" nowrap>
                            &nbsp;<asp:Button ID="btnOK" runat="server" Text=" 测 试 " 
                                onclick="btnOK_Click" />
                        </td>
                    </tr>
                </tfoot>
            </table>
            <br />
        </div>
        <br />
        <table width="450" align="center">
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
