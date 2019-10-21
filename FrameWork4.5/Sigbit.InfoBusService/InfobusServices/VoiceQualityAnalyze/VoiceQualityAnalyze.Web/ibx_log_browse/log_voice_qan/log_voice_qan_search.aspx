<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log_voice_qan_search.aspx.cs" Inherits="genui_FBSL_log_voice_qan_search" %>

<%@ Register Src="../../module/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>

<html>
<head runat="server">
    <title>��������������־</title>
    <script language="javascript" src="../../js/calendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/salary.gif" />
                        ��������������־</td>
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
                    ��������ʶ��</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtReqeustThirdId" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    ���������ļ���</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtVoiceFileLocal" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    �ϴ������ļ���</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtVoiceFileUpload" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    ���������ļ���</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtVoiceFileForQan" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    ״̬��</td>
                <td>
                    &nbsp;<asp:DropDownList ID="ddlbCurrentStatus" runat="server"></asp:DropDownList></td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    &nbsp;<asp:Button ID="btnOK" runat="server" Text="ȷ��" OnClick="btnOK_Click" />
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="ȡ��" OnClick="btnCancel_Click" /></td>
            </tr>
        </tfoot>
        </table>
        </div>
    </div>
    </form>
</body>
</html>
