<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log_voice_qan_modify.aspx.cs" Inherits="genui_FBSL_log_voice_qan_modify" %>

<html>
<head runat="server">
    <title>语音分析日志属性</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/salary.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="语音分析日志属性"></asp:Label></td>
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
                    上传时间：</td>
                <td>
                    &nbsp;<asp:Label ID="lblUploadTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    请求时间：</td>
                <td>
                    &nbsp;<asp:Label ID="lblRequestTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    调用方式：</td>
                <td>
                    &nbsp;<asp:Label ID="lblIsSyncCall" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    第三方标识：</td>
                <td>
                    &nbsp;<asp:Label ID="lblReqeustThirdId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    本地语音文件：</td>
                <td>
                    &nbsp;<asp:Label ID="lblVoiceFileLocal" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    上传保存文件：</td>
                <td>
                    &nbsp;<asp:Label ID="lblVoiceFileUpload" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    分析语音文件：</td>
                <td>
                    &nbsp;<asp:Label ID="lblVoiceFileForQan" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    状态：</td>
                <td>
                    &nbsp;<asp:Label ID="lblCurrentStatus" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    分析结果：</td>
                <td>
                    &nbsp;<asp:Label ID="lblQualityValue01" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    开始分析时间：</td>
                <td>
                    &nbsp;<asp:Label ID="lblQanBeginTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    结束分析时间：</td>
                <td>
                    &nbsp;<asp:Label ID="lblQanEndTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    取回结果时间：</td>
                <td>
                    &nbsp;<asp:Label ID="lblResultFetchTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    分析时延：</td>
                <td>
                    &nbsp;<asp:Label ID="lblQanDelay" runat="server"></asp:Label></td>
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
