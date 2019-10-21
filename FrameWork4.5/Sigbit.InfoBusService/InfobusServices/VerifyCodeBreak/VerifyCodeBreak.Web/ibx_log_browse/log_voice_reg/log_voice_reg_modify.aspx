<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log_voice_reg_modify.aspx.cs" Inherits="genui_NVAI_log_voice_reg_modify" %>

<html>
<head runat="server">
    <title>语音识别日志属性</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/comm.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="语音识别日志属性"></asp:Label></td>
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
                    第三方编码：</td>
                <td>
                    &nbsp;<asp:Label ID="lblReqeustThirdId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    本地文件：</td>
                <td>
                    &nbsp;<asp:Label ID="lblVoiceFileLocal" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    上传文件：</td>
                <td>
                    &nbsp;<asp:Label ID="lblVoiceFileUpload" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    语音文件：</td>
                <td>
                    &nbsp;<asp:Label ID="lblVoiceFileForIsr" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    语法：</td>
                <td>
                    &nbsp;<asp:Label ID="lblGrammarId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    来源系统：</td>
                <td>
                    &nbsp;<asp:Label ID="lblFromSystem" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    客户端：</td>
                <td>
                    &nbsp;<asp:Label ID="lblFromClientId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    客户端描述：</td>
                <td>
                    &nbsp;<asp:Label ID="lblFromClientDesc" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    当前状态：</td>
                <td>
                    &nbsp;<asp:Label ID="lblCurrentStatus" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    破解结果：</td>
                <td>
                    &nbsp;<asp:Label ID="lblRegResultCode" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    失败原因：</td>
                <td>
                    &nbsp;<asp:Label ID="lblRegFailDesc" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    识别文字：</td>
                <td>
                    &nbsp;<asp:Label ID="lblRegResultText" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    请求时间：</td>
                <td>
                    &nbsp;<asp:Label ID="lblRequestTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    开始识别时间：</td>
                <td>
                    &nbsp;<asp:Label ID="lblIsrBeginTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    结束识别时间：</td>
                <td>
                    &nbsp;<asp:Label ID="lblIsrEndTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    取回结果时间：</td>
                <td>
                    &nbsp;<asp:Label ID="lblResultFetchTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    识别时延：</td>
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
                    &nbsp;<asp:Button ID="btnCancel" runat="server" Text="返回" 
                        OnClick="btnCancel_Click" /></td>
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
