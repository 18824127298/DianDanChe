<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log_voice_reg_modify.aspx.cs" Inherits="genui_NVAI_log_voice_reg_modify" %>

<html>
<head runat="server">
    <title>����ʶ����־����</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/comm.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="����ʶ����־����"></asp:Label></td>
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
                    �ϴ�ʱ�䣺</td>
                <td>
                    &nbsp;<asp:Label ID="lblUploadTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ���������룺</td>
                <td>
                    &nbsp;<asp:Label ID="lblReqeustThirdId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �����ļ���</td>
                <td>
                    &nbsp;<asp:Label ID="lblVoiceFileLocal" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �ϴ��ļ���</td>
                <td>
                    &nbsp;<asp:Label ID="lblVoiceFileUpload" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �����ļ���</td>
                <td>
                    &nbsp;<asp:Label ID="lblVoiceFileForIsr" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �﷨��</td>
                <td>
                    &nbsp;<asp:Label ID="lblGrammarId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ��Դϵͳ��</td>
                <td>
                    &nbsp;<asp:Label ID="lblFromSystem" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �ͻ��ˣ�</td>
                <td>
                    &nbsp;<asp:Label ID="lblFromClientId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �ͻ���������</td>
                <td>
                    &nbsp;<asp:Label ID="lblFromClientDesc" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ��ǰ״̬��</td>
                <td>
                    &nbsp;<asp:Label ID="lblCurrentStatus" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �ƽ�����</td>
                <td>
                    &nbsp;<asp:Label ID="lblRegResultCode" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ʧ��ԭ��</td>
                <td>
                    &nbsp;<asp:Label ID="lblRegFailDesc" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ʶ�����֣�</td>
                <td>
                    &nbsp;<asp:Label ID="lblRegResultText" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ����ʱ�䣺</td>
                <td>
                    &nbsp;<asp:Label ID="lblRequestTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ��ʼʶ��ʱ�䣺</td>
                <td>
                    &nbsp;<asp:Label ID="lblIsrBeginTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ����ʶ��ʱ�䣺</td>
                <td>
                    &nbsp;<asp:Label ID="lblIsrEndTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ȡ�ؽ��ʱ�䣺</td>
                <td>
                    &nbsp;<asp:Label ID="lblResultFetchTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ʶ��ʱ�ӣ�</td>
                <td>
                    &nbsp;<asp:Label ID="lblBreakDelay" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ��ʱ�ӣ�</td>
                <td>
                    &nbsp;<asp:Label ID="lblTotalDelay" runat="server"></asp:Label></td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    &nbsp;<asp:Button ID="btnCancel" runat="server" Text="����" 
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
