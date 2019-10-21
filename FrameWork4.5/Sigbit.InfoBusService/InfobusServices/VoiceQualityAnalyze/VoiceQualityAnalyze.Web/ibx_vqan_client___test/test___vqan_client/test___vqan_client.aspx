<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test___vqan_client.aspx.cs" Inherits="ibx_vcode_client___test_test___vcode_client_test___vcode_client" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>语音分析客户端测试</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/vehicle.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="语音分析客户端测试"></asp:Label></td>
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
                    语音文件：</td>
                <td>
                    &nbsp;<asp:FileUpload ID="FileUploadVoice" runat="server" Width="300px" /></td>
            </tr>
            <tr>
                <td nowrap>
                    第三方编码：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtRequestThirdID" runat="server" Text="" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    上传回执：</td>
                <td>
                    &nbsp;<asp:Label ID="lblUploadReceipt" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    </td>
                <td>
                    &nbsp;<asp:CheckBox ID="cbxSyncCall" runat="server" Checked="false" Text="同步调用" /></td>
            </tr>
            <tr>
                <td nowrap>
                    请求结果：</td>
                <td>
                    &nbsp;<asp:Label ID="lblRequestRET" runat="server" Text="" ForeColor="Red"></asp:Label></td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    &nbsp;<asp:Button ID="btnUploadImage" runat="server" Text="上传语音得到回执" 
                        onclick="btnUploadImage_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                            ID="btnVQANRequest" runat="server" Text="根据回执发送请求" 
                        onclick="btnVQANRequest_Click" /></td>
            </tr>
        </tfoot>
        </table>
        <br />
        
        <table border="0" width="450" cellpadding="4" cellspacing="1" align="center">
        <tbody>
            <tr>
                <td nowrap>
                    分析结果：</td>
                <td>
                    &nbsp;<asp:Label ID="lblFetchResult" runat="server" Text="----------------" ForeColor="Red" Font-Bold="True"></asp:Label></td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    &nbsp;<asp:Button ID="btnFetchBreakResult" runat="server" Text="获取验证码破解的结果" 
                        onclick="btnFetchBreakResult_Click" /></td>
            </tr>
        </tfoot>
        </table>

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
