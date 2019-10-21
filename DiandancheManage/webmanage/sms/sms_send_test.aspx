<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sms_send_test.aspx.cs" Inherits="webmanage_sms_sms_send_test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            手机号：
            <asp:TextBox ID="edtPhone" runat="server"></asp:TextBox>
            内容：
            <asp:TextBox ID="edtContent" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="发送" OnClick="Button1_Click" />
            <asp:Label ID="Result" runat="server" Text="Label"></asp:Label>
        </div>
    </form>
</body>
</html>
