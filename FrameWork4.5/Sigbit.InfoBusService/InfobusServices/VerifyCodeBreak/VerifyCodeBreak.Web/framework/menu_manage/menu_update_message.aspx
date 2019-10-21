<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu_update_message.aspx.cs" Inherits="framework_menu_navigate_menu_update_message" %>

<!--DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <!--link rel="stylesheet" type="text/css" href="pref_style.css"-->
</head>

<script>
function RefreshTree()
{
    parent.menu_list.location.reload();
}
</script>

<body topmargin="5" onload="RefreshTree()">
    <form id="form1" runat="server">
        <div>
            <div align="center" title="提示信息框">
                <span class="messageBox" style="width=180px">
                    <img src="images/attention.gif">
                    <font color="#FF0000"><b>提示</b></font>
                    <hr>
                    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
                        <asp:Label ID="lblMessage" runat="server" Text="xxxxxxxxxx"></asp:Label></asp:Panel>
                </span>
            </div>
            <center>
                &nbsp;</center>
        </div>
    </form>
</body>
</html>
