<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log_vcode_break_modify.aspx.cs" Inherits="genui_KTJQ_log_vcode_break_modify" %>

<html>
<head runat="server">
    <title>��֤���ƽ���־����</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/key3.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="��֤���ƽ���־����"></asp:Label></td>
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
                    ����ʱ�䣺</td>
                <td>
                    &nbsp;<asp:Label ID="lblRequestTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ͼ���ļ���</td>
                <td>
                    &nbsp;<asp:Label ID="lblImageFileName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ��֤�����ͣ�</td>
                <td>
                    &nbsp;<asp:Label ID="lblVcodeId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �㷨�ı�ʶ��</td>
                <td>
                    &nbsp;<asp:Label ID="lblAlgolId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �ͻ��ˣ�</td>
                <td>
                    &nbsp;<asp:Label ID="lblFromClientId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ״̬��</td>
                <td>
                    &nbsp;<asp:Label ID="lblCurrentStatus" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �ƽ�����</td>
                <td>
                    &nbsp;<asp:Label ID="lblBreakResult" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ʧ��ԭ��</td>
                <td>
                    &nbsp;<asp:Label ID="lblFailDesc" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ��֤�����֣�</td>
                <td>
                    &nbsp;<asp:Label ID="lblBreakText" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ȡ��ʱ�䣺</td>
                <td>
                    &nbsp;<asp:Label ID="lblFetchTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �ƽ�ʱ�䣺</td>
                <td>
                    &nbsp;<asp:Label ID="lblBreakTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ȡ��ʱ�ӣ�</td>
                <td>
                    &nbsp;<asp:Label ID="lblFetchDelay" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �ƽ�ʱ�ӣ�</td>
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
