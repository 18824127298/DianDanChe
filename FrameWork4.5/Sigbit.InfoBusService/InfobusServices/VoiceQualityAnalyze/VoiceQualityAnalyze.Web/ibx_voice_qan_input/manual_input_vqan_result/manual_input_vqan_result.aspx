<%@ Page Language="C#" AutoEventWireup="true" CodeFile="manual_input_vqan_result.aspx.cs" Inherits="genui_KTJQ_log_vcode_break_modify" %>

<html>
<head runat="server">
    <title>手工输入语音识别的结果</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/date.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="手工输入语音识别的结果"></asp:Label></td>
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
                    回执：</td>
                <td>
                    &nbsp;<asp:Label ID="lblUploadReceipt" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    第三方标识：</td>
                <td>
                    &nbsp;<asp:Label ID="lblRequestThirdId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    请求时间：</td>
                <td>
                    &nbsp;<asp:Label ID="lblRequestTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    本地语音文件：</td>
                <td>
                    &nbsp;<asp:Label ID="lblLocalVoiceFileName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    语音文件名：</td>
                <td>
                    &nbsp;<asp:Label ID="lblVoiceFileName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td nowrap>
                    语音：</td>
                <td>
                    <asp:Literal ID="divPlayVoice" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td nowrap>
                    杂音：</td>
                <td>
                    <asp:RadioButtonList ID="rbListNoise" runat="server" RepeatColumns="6" 
                        CellPadding="2" CellSpacing="0" AutoPostBack="True" 
                        onselectedindexchanged="rbListNoise_SelectedIndexChanged">
                        <asp:ListItem>无</asp:ListItem>
                        <asp:ListItem Selected="True">小</asp:ListItem>
                        <asp:ListItem>较小</asp:ListItem>
                        <asp:ListItem>中等</asp:ListItem>
                        <asp:ListItem>大</asp:ListItem>
                        <asp:ListItem>很大</asp:ListItem>
                    </asp:RadioButtonList>
                    &nbsp;杂音值 <asp:TextBox ID="edtNoiseValue" runat="server" Width="50px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td nowrap>
                    音量：</td>
                <td>
                    <asp:RadioButtonList ID="rbListVolume" runat="server" RepeatColumns="6" 
                        CellPadding="2" CellSpacing="0" AutoPostBack="True" 
                        onselectedindexchanged="rbListVolume_SelectedIndexChanged">
                        <asp:ListItem>无音</asp:ListItem>
                        <asp:ListItem>很小</asp:ListItem>
                        <asp:ListItem>较小</asp:ListItem>
                        <asp:ListItem Selected="True">适中</asp:ListItem>
                        <asp:ListItem>较大</asp:ListItem>
                        <asp:ListItem>很大</asp:ListItem>
                    </asp:RadioButtonList>
                    &nbsp;音量值 <asp:TextBox ID="edtVolumeValue" runat="server" Width="50px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td nowrap>
                    连贯：</td>
                <td>
                    <asp:RadioButtonList ID="rbListSmooth" runat="server" RepeatColumns="3" 
                        CellPadding="2" CellSpacing="0" AutoPostBack="True" 
                        onselectedindexchanged="rbListSmooth_SelectedIndexChanged" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem>纯噪音</asp:ListItem>
                        <asp:ListItem>很不清楚</asp:ListItem>
                        <asp:ListItem>时延时续</asp:ListItem>
                        <asp:ListItem>勉强听清</asp:ListItem>
                        <asp:ListItem Selected="True">尚可接受</asp:ListItem>
                        <asp:ListItem>平滑流畅</asp:ListItem>
                    </asp:RadioButtonList>
                    &nbsp;连贯值 <asp:TextBox ID="edtSmoothValue" runat="server" Width="50px"></asp:TextBox>
                </td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td colspan="2" nowrap>
                    <asp:Button ID="btnHaveARest" runat="server" Text="休息一会儿" 
                        onclick="btnHaveARest_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:Button ID="btnRefresh" runat="server" onclick="btnRefresh_Click" 
                        Text="刷新" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnInput" runat="server" onclick="btnInput_Click" Text="填写识别结果" />
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
