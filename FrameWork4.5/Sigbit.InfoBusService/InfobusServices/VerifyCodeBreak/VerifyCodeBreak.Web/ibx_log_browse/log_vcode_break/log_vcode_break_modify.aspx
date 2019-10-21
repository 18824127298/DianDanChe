<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log_vcode_break_modify.aspx.cs" Inherits="genui_KTJQ_log_vcode_break_modify" %>

<html>
<head runat="server">
    <title>验证码破解日志属性</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/key3.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="验证码破解日志属性"></asp:Label></td>
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
                    请求时间：</td>
                <td>
                    &nbsp;<asp:Label ID="lblRequestTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    图像文件：</td>
                <td>
                    &nbsp;<asp:Label ID="lblImageFileName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    验证码类型：</td>
                <td>
                    &nbsp;<asp:Label ID="lblVcodeId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    算法的标识：</td>
                <td>
                    &nbsp;<asp:Label ID="lblAlgolId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    客户端：</td>
                <td>
                    &nbsp;<asp:Label ID="lblFromClientId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    状态：</td>
                <td>
                    &nbsp;<asp:Label ID="lblCurrentStatus" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    破解结果：</td>
                <td>
                    &nbsp;<asp:Label ID="lblBreakResult" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    失败原因：</td>
                <td>
                    &nbsp;<asp:Label ID="lblFailDesc" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    验证码文字：</td>
                <td>
                    &nbsp;<asp:Label ID="lblBreakText" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    取出时间：</td>
                <td>
                    &nbsp;<asp:Label ID="lblFetchTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    破解时间：</td>
                <td>
                    &nbsp;<asp:Label ID="lblBreakTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    取出时延：</td>
                <td>
                    &nbsp;<asp:Label ID="lblFetchDelay" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    破解时延：</td>
                <td>
                    &nbsp;<asp:Label ID="lblBreakDelay" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    总时延：</td>
                <td>
                    &nbsp;<asp:Label ID="lblTotalDelay" runat="server"></asp:Label></td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    <asp:Button ID="btnCancel" runat="server" Text="返回" OnClick="btnCancel_Click" /></td>
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
