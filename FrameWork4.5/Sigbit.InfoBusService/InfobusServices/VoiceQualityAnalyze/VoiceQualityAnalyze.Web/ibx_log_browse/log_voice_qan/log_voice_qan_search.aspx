<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log_voice_qan_search.aspx.cs" Inherits="genui_FBSL_log_voice_qan_search" %>

<%@ Register Src="../../module/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>

<html>
<head runat="server">
    <title>搜索语音分析日志</title>
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
                        搜索语音分析日志</td>
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
                    起始日期：</td>
                <td>
                    &nbsp;<uc1:DatePicker ID="DatePickerFrom" runat="server" /></td>
            </tr>
            <tr>
                <td nowrap>
                    终止日期：</td>
                <td>
                    &nbsp;<uc1:DatePicker ID="DatePickerTo" runat="server" /></td>
            </tr>
            <tr>
                <td nowrap>
                    第三方标识：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtReqeustThirdId" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    本地语音文件：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtVoiceFileLocal" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    上传保存文件：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtVoiceFileUpload" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    分析语音文件：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtVoiceFileForQan" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    状态：</td>
                <td>
                    &nbsp;<asp:DropDownList ID="ddlbCurrentStatus" runat="server"></asp:DropDownList></td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    &nbsp;<asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click" />
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" /></td>
            </tr>
        </tfoot>
        </table>
        </div>
    </div>
    </form>
</body>
</html>
