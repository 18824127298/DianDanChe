<%@ Page Language="C#" AutoEventWireup="true" CodeFile="manual_input_vcode.aspx.cs" Inherits="genui_KTJQ_log_vcode_break_modify" %>

<html>
<head runat="server">
    <title>手工输入语音识别的结果</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/date.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="手工输入语音识别的结果"></asp:Label></td>
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
                    验证码标识：</td>
                <td>
                    &nbsp;<asp:Label ID="lblVCodeId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    回执：</td>
                <td>
                    &nbsp;<asp:Label ID="lblUploadReceipt" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    第三方标识：</td>
                <td>
                    &nbsp;<asp:Label ID="lblRequestThirdId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    请求时间：</td>
                <td>
                    &nbsp;<asp:Label ID="lblRequestTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    本地图像文件：</td>
                <td>
                    &nbsp;<asp:Label ID="lblLocalImageFileName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    图像文件名：</td>
                <td>
                    &nbsp;<asp:Label ID="lblImageFileName" runat="server"></asp:Label></td>
            </tr>
            <tr id="trImageToBreak" runat="server">
                <td nowrap>
                    图像：</td>
                <td>
                    <asp:Image ID="imgToBreak" runat="server" 
                        ImageUrl="~/data/vcode_break/vcode_images/20121011110414-oglg.png" /></td>
            </tr>
            <tr>
                <td nowrap>
                    破解结果：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtBreakResultText" runat="server"></asp:TextBox></td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    <asp:Button ID="btnHaveARest" runat="server" Text="休息一会儿" 
                        onclick="btnHaveARest_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:Button ID="btnRefresh" runat="server" onclick="btnRefresh_Click" 
                        Text="刷新" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnInput" runat="server" onclick="btnInput_Click" Text="填写识别结果" />
                </td>
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
