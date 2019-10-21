<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log_vcode_break_search.aspx.cs" Inherits="genui_KTJQ_log_vcode_break_search" %>

<%@ Register Src="../../module/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>

<html>
<head runat="server">
    <title>搜索验证码破解日志</title>
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
                        搜索验证码破解日志</td>
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
                    图像文件：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtImageFileName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    验证码类型：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtVcodeId" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    客户端：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtFromClientId" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    状态：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtCurrentStatus" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    破解结果：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtBreakResult" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    失败原因：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtFailDesc" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td nowrap>
                    验证码文字：</td>
                <td>
                    &nbsp;<asp:TextBox ID="edtBreakText" runat="server"></asp:TextBox></td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    &nbsp;<asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click" />
                    &nbsp; &nbsp; &nbsp;
                    <input id="btnCancel" type="button" value="取消" language="javascript" onclick="history.back()" class="normalButton" /></td>
            </tr>
        </tfoot>
        </table>
        </div>
    </div>
    </form>
</body>
</html>
