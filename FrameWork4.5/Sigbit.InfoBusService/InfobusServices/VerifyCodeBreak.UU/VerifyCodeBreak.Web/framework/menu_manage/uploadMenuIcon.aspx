<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uploadMenuIcon.aspx.cs" Inherits="module_uploadMenuIcon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择菜单图标</title>

    <script type="text/javascript">
        function ImageChoice(img)
        {
           window.returnValue=img;
           window.close();
        }
    </script>

</head>
<body style="margin-left: 0; margin-top: 0">
    <form id="form1" runat="server">
        <div>
            <asp:DataList ID="dlImages" runat="server" RepeatColumns="15">
                <ItemStyle BorderWidth="1" />
                <ItemTemplate>
                    <img src="<%# Eval("filepath") %>" alt="" onclick="ImageChoice(this.src)" /><br />
                </ItemTemplate>
            </asp:DataList>
            <br />
            <asp:Literal ID="ltUploadFile" runat="server"></asp:Literal>
            <br />
        </div>
        <div align="center">
       
        </div>
    </form>
</body>
</html>