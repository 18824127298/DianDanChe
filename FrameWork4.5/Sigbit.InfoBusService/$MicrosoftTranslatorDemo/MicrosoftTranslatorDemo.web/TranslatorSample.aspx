<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TranslatorSample.aspx.cs" Inherits="TranslatorSample" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
　 Query Text: <asp:TextBox ID="edtQueryText" runat="server" Width="800px" TextMode="MultiLine" 
　      SkinID="multiline" Rows="3" Text="飞信是中国移动推出的一项业务，可以实现即时消息、短信、语音、GPRS等多种通信方式，保证用户永不离线。实现无缝链接的多端信息接收，让您随时随地都可与好友保持畅快有效的沟通。"></asp:TextBox><br />
        Process Result: <asp:TextBox ID="edtResult" runat="server" TextMode="MultiLine" SkinID="multiline" Width="800px" Rows="10"></asp:TextBox><br />
        &nbsp;<br />
        <asp:Button ID="btnDetectLanguage" runat="server" Text=" Detect " 
            onclick="btnDetectLanguage_Click" /><br />&nbsp;<br />
        <asp:Button ID="btnDoTranslate" runat="server" Text=" Translate " 
            onclick="btnDoTranslate_Click" />&nbsp;&nbsp;from:<asp:DropDownList ID="ddlbFromLanguage" runat="server"></asp:DropDownList>
            &nbsp;&nbsp;to:<asp:DropDownList ID="ddlbToLanguage" runat="server"></asp:DropDownList>
        <br />&nbsp;<br />
        <asp:Button ID="btnSpeak" runat="server" Text=" Speak " 
            onclick="btnSpeak_Click" />&nbsp;&nbsp;language:<asp:DropDownList ID="ddlbSpeakLanguage" runat="server"></asp:DropDownList>
            &nbsp;&nbsp;format:<asp:DropDownList ID="ddlbFormat" runat="server"></asp:DropDownList>
            &nbsp;&nbsp;quality:<asp:DropDownList ID="ddlbQuality" runat="server"></asp:DropDownList>
    </div>
    </form>
</body>
</html>
