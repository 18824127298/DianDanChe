<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log_vcode_break_search.aspx.cs" Inherits="genui_KTJQ_log_vcode_break_search" %>

<%@ Register Src="../../module/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>

<html>
<head runat="server">
    <title>������֤���ƽ���־</title>
    <script language="javascript" src="../../js/calendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/key3.gif" />
                        ������֤���ƽ���־</td>
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
                    ͼ���ļ���</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtImageFileName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    ��֤�����ͣ�</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtVcodeId" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    �ͻ��ˣ�</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtFromClientId" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    ״̬��</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtCurrentStatus" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    �ƽ�����</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtBreakResult" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    ʧ��ԭ��</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtFailDesc" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    ��֤�����֣�</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtBreakText" runat="server"></asp:TextBox></td>
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
