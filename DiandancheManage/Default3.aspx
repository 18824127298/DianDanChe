<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="edtSN" runat="server"></asp:TextBox>
            <asp:Button ID="btn_dayinji" runat="server" Text="测试打印机" OnClick="btn_dayinji_Click" style="height: 21px" />
        </div>
    </form>
</body>
</html>

