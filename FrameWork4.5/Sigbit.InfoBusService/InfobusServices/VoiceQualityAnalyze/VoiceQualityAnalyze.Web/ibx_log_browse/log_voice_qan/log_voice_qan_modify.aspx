<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log_voice_qan_modify.aspx.cs" Inherits="genui_FBSL_log_voice_qan_modify" %>

<html>
<head runat="server">
    <title>����������־����</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/salary.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="����������־����"></asp:Label></td>
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
                    ����ʱ�䣺</td>
                <td>
                    &nbsp;<asp:Label ID="lblRequestTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ���÷�ʽ��</td>
                <td>
                    &nbsp;<asp:Label ID="lblIsSyncCall" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ��������ʶ��</td>
                <td>
                    &nbsp;<asp:Label ID="lblReqeustThirdId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ���������ļ���</td>
                <td>
                    &nbsp;<asp:Label ID="lblVoiceFileLocal" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �ϴ������ļ���</td>
                <td>
                    &nbsp;<asp:Label ID="lblVoiceFileUpload" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ���������ļ���</td>
                <td>
                    &nbsp;<asp:Label ID="lblVoiceFileForQan" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ״̬��</td>
                <td>
                    &nbsp;<asp:Label ID="lblCurrentStatus" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ���������</td>
                <td>
                    &nbsp;<asp:Label ID="lblQualityValue01" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ��ʼ����ʱ�䣺</td>
                <td>
                    &nbsp;<asp:Label ID="lblQanBeginTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ��������ʱ�䣺</td>
                <td>
                    &nbsp;<asp:Label ID="lblQanEndTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ȡ�ؽ��ʱ�䣺</td>
                <td>
                    &nbsp;<asp:Label ID="lblResultFetchTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ����ʱ�ӣ�</td>
                <td>
                    &nbsp;<asp:Label ID="lblQanDelay" runat="server"></asp:Label></td>
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
                    <asp:Button ID="btnCancel" runat="server" Text="����" OnClick="btnCancel_Click" /></td>
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
