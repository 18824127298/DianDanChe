<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test___btm_vcode_client.aspx.cs" Inherits="ibx_vcode_client___test_test___vcode_client_test___vcode_client" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>验证码破解客户端测试</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/vehicle.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="验证码破解客户端测试"></asp:Label></td>
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
                    授权帐号：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtAuthenUserName" runat="server" Width="200px" Text="asia_wap"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    密码：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtAuthenPassword" runat="server" Width="200px" Text="El95cFdFc5"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    图像文件：</td>
                <td>
                    &nbsp;<asp:FileUpload ID="FileUploadImage" runat="server" Width="300px" /></td>
            </tr>
            <tr>
                <td nowrap>
                    破解场景：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtVCodeUsageID" runat="server" Width="200px" Text="cmcc_571_zj_10086_web"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    破解结果：</td>
                <td>
                    &nbsp;<asp:Label ID="lblBreakResultText" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label></td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    <asp:Button ID="btnUploadAndBreak" runat="server" Text="发送破解请求，得到破解结果" 
                        onclick="btnUploadAndBreak_Click" /></td>
            </tr>
        </tfoot>
        </table>
        <br />
        
        </div>
        <br/>
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
