﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="message_box.aspx.cs" Inherits="framework_general_win_message_box" %>

<!--DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <!--link rel="stylesheet" type="text/css" href="pref_style.css"-->
</head>
<body topmargin="5">
    <form id="form1" runat="server">
    <div>
        <div align="center" title="提示信息框">
            <span class="messageBox" style="width=180px">
                <img src="attention.gif" > 
                <font color="#FF0000"><b>提示</b></font>
                <hr>
                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
                <asp:Label ID="lblMessage" runat="server" Text="xxxxxxxxxx"></asp:Label></asp:Panel>
            </span>
        </div>
        <br>
        <center>
            <input id="btnHistoryBack" runat="server" type="button" class='normalButton' value="返回" onclick="history.back();">
            <asp:Button ID="btnUrlBack" runat="server" Text="返回" CssClass="normalButton" OnClick="btnUrlBack_Click" />
        </center>    
    </div>
    </form>
</body>
</html>
