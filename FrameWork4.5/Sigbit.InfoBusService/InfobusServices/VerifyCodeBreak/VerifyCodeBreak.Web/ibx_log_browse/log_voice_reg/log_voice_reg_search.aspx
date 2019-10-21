<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log_voice_reg_search.aspx.cs" Inherits="genui_NVAI_log_voice_reg_search" %>

<%@ Register Src="../../module/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>

<html>
<head runat="server">
    <title>��������ʶ����־</title>
    <script language="javascript" src="../../js/calendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/comm.gif" />
                        ��������ʶ����־</td>
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
                    ��ʼ���ڣ�</td>
                <td>
                    &nbsp;<uc1:DatePicker ID="DatePickerFrom" runat="server" /></td>
            </tr>
            <tr>
                <td nowrap>
                    ��ֹ���ڣ�</td>
                <td>
                    &nbsp;<uc1:DatePicker ID="DatePickerTo" runat="server" /></td>
            </tr>
            <tr>
                <td nowrap>
                    ���������룺</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtReqeustThirdId" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    �����ļ���</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtVoiceFileLocal" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    �ϴ��ļ���</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtVoiceFileUpload" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    �����ļ���</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtVoiceFileForIsr" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    �﷨��</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtGrammarId" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    �ͻ��ˣ�</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtFromClientId" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    ��ǰ״̬��</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtCurrentStatus" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    �ƽ�����</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtRegResultCode" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    ʧ��ԭ��</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtRegFailDesc" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    ʶ�����֣�</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtRegResultText" runat="server"></asp:TextBox></td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    &nbsp;<asp:Button ID="btnOK" runat="server" Text="ȷ��" OnClick="btnOK_Click" />
                    &nbsp; &nbsp; &nbsp;
                    <input id="btnCancel" type="button" value="ȡ��" language="javascript" onclick="history.back()" class="normalButton" /></td>
            </tr>
        </tfoot>
        </table>
        </div>
    </div>
    </form>
</body>
</html>
