<%@ Page Language="C#" AutoEventWireup="true" CodeFile="manual_input_vqan_result.aspx.cs" Inherits="genui_KTJQ_log_vcode_break_modify" %>

<html>
<head runat="server">
    <title>�ֹ���������ʶ��Ľ��</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/date.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="�ֹ���������ʶ��Ľ��"></asp:Label></td>
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
                    ��ִ��</td>
                <td>
                    &nbsp;<asp:Label ID="lblUploadReceipt" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ��������ʶ��</td>
                <td>
                    &nbsp;<asp:Label ID="lblRequestThirdId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ����ʱ�䣺</td>
                <td>
                    &nbsp;<asp:Label ID="lblRequestTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ���������ļ���</td>
                <td>
                    &nbsp;<asp:Label ID="lblLocalVoiceFileName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    �����ļ�����</td>
                <td>
                    &nbsp;<asp:Label ID="lblVoiceFileName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    ������</td>
                <td>
                    <asp:Literal ID="divPlayVoice" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td nowrap>
                    ������</td>
                <td>
                    <asp:RadioButtonList ID="rbListNoise" runat="server" RepeatColumns="6" 
                        CellPadding="2" CellSpacing="0" AutoPostBack="True" 
                        onselectedindexchanged="rbListNoise_SelectedIndexChanged">
                        <asp:ListItem>��</asp:ListItem>
                        <asp:ListItem Selected="True">С</asp:ListItem>
                        <asp:ListItem>��С</asp:ListItem>
                        <asp:ListItem>�е�</asp:ListItem>
                        <asp:ListItem>��</asp:ListItem>
                        <asp:ListItem>�ܴ�</asp:ListItem>
                    </asp:RadioButtonList>
                    &nbsp;����ֵ <asp:TextBox ID="edtNoiseValue" runat="server" Width="50px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td nowrap>
                    ������</td>
                <td>
                    <asp:RadioButtonList ID="rbListVolume" runat="server" RepeatColumns="6" 
                        CellPadding="2" CellSpacing="0" AutoPostBack="True" 
                        onselectedindexchanged="rbListVolume_SelectedIndexChanged">
                        <asp:ListItem>����</asp:ListItem>
                        <asp:ListItem>��С</asp:ListItem>
                        <asp:ListItem>��С</asp:ListItem>
                        <asp:ListItem Selected="True">����</asp:ListItem>
                        <asp:ListItem>�ϴ�</asp:ListItem>
                        <asp:ListItem>�ܴ�</asp:ListItem>
                    </asp:RadioButtonList>
                    &nbsp;����ֵ <asp:TextBox ID="edtVolumeValue" runat="server" Width="50px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td nowrap>
                    ���᣺</td>
                <td>
                    <asp:RadioButtonList ID="rbListSmooth" runat="server" RepeatColumns="3" 
                        CellPadding="2" CellSpacing="0" AutoPostBack="True" 
                        onselectedindexchanged="rbListSmooth_SelectedIndexChanged" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem>������</asp:ListItem>
                        <asp:ListItem>�ܲ����</asp:ListItem>
                        <asp:ListItem>ʱ��ʱ��</asp:ListItem>
                        <asp:ListItem>��ǿ����</asp:ListItem>
                        <asp:ListItem Selected="True">�пɽ���</asp:ListItem>
                        <asp:ListItem>ƽ������</asp:ListItem>
                    </asp:RadioButtonList>
                    &nbsp;����ֵ <asp:TextBox ID="edtSmoothValue" runat="server" Width="50px"></asp:TextBox>
                </td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    <asp:Button ID="btnHaveARest" runat="server" Text="��Ϣһ���" 
                        onclick="btnHaveARest_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:Button ID="btnRefresh" runat="server" onclick="btnRefresh_Click" 
                        Text="ˢ��" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnInput" runat="server" onclick="btnInput_Click" Text="��дʶ����" />
                </td>
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
