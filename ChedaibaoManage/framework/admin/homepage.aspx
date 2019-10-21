<%@ Page Language="C#" AutoEventWireup="true" CodeFile="homepage.aspx.cs" Inherits="farmwork_homepage" %>

<!DOCTYPE HTML>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>欢迎使用</title>
    <meta http-equiv="Content-Type" content="text/html;">
    <style>
        body, p, ul, li
        {
            padding: 0;
            margin: 0;
        }
        ul, li
        {
            list-style: none;
        }
        img
        {
            border: none;
            vertical-align: middle;
        }
        a
        {
            text-decoration: none;
        }
        a:hover
        {
            text-decoration: underline;
        }
        body
        {
            background: #5267da url(images/homepage/bg.jpg) repeat-x 0 0;
        }
        .warp
        {
            background: url(images/homepage/bgf2.jpg) no-repeat 50% 0;
            padding-top: 140px;
        }
        .index_top_font
        {
            font-family: 微软雅黑,宋体,黑体,Arial,sans-serif;
            font-weight: bold;
            font-size: 22px;
            color: White;
        }
        .index_top_user_info
        {
            font-family: 微软雅黑,宋体,黑体,Arial,sans-serif;
            font-weight: bold;
            font-size: 12px;
            color: White;
            text-align: left;
            display: inline-block;
            vertical-align: bottom;
            margin-top: 15px;
        }
        .module_title
        {
            font-family: 微软雅黑,宋体,黑体,Arial,sans-serif;
            font-weight: bold;
            font-size: 16px;
            color: White;
            float: left;
            margin-left: 5px;
            margin-top: 50px;
            
        }
        .bottom_font
        {
            font-family: 微软雅黑,宋体,黑体,Arial,sans-serif;
            font-weight: bold;
            font-size: 12px;
            color: White;
        }
        a:link
        {
            text-decoration: none;
            color: blue;
        }
        a:active
        {
            text-decoration: blink;
        }
        a:hover
        {
            text-decoration: underline;
            color: red;
        }
        a:visited
        {
            text-decoration: none;
            color: green;
        }
        .grid
        {
            width: 220px;
            height: 120px;
            float: left;
            margin: 5px;
        }
        .grid-image
        {
            width: 70px;
            height: 70px;
            margin-top: 25px;
            margin-left: 30px;
            float: left;
        }
    </style>
</head>
<body>
    <form id="frmMain" runat="server">
    <div class="warp" align="center">
        <table width="700" border="0" cellpadding="0" height="100%">
            <tr>
                <td align="right" height="40">
                    <table width="100%" border="0">
                        <tr>
                            <td class="index_top_user_info">
                                <asp:Literal ID="ltCurrentUser" runat="server"></asp:Literal>
                                &nbsp;<asp:HyperLink ID="hlkLogout" runat="server" ForeColor="White" />
                            </td>
                            <td align="right">
                                <div class="index_top_font">
                                    <asp:Label ID="lblSystemName" runat="server" Text="xxxx" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="width: 100%;">
                        <asp:Repeater ID="rptSubSystemList" runat="server">
                            <ItemTemplate>
                                <%# VIVSubSystemGrid(Eval("system_id").ToString()) %>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <%--<table width="100%" border="0" cellpadding="0" cellspacing="5">
                        <tr align="center">
                            <td bgcolor="#7aaa1a" class="grid">
                                <a href="admin.aspx?sub_sys=ivr">
                                    <img src="images/homepage/busi_10000.gif" class="grid-image" />
                                    <span class="module_title">IVR厅</span></a>
                            </td>
                            <td bgcolor="#1eb3b9" class="grid">
                                <a href="admin.aspx?sub_sys=sms">
                                    <img src="images/homepage/busi_elechan.gif" style="width: 70px; height: 70px;" />
                                    <span class="module_title">SMS短厅</span></a>
                            </td>
                            <td bgcolor="#E35040" class="grid">
                                <a href="admin.aspx?sub_sys=wap">
                                    <img src="images/homepage/monitor.gif" class="grid-image" />
                                    <span class="module_title">WAP厅</span></a>
                            </td>
                        </tr>
                        <tr align="center">
                            <td bgcolor="#0a15ff" width="220">
                                <a href="admin.aspx?sub_sys=taskflow">
                                    <img src="images/homepage/taskflow.gif" class="grid-image" /><br />
                                    <span class="module_title">任务管理</span></a>
                            </td>
                            <td bgcolor="#bc1f3e" class="grid">
                                <a href="admin.aspx?sub_sys=resource">
                                    <img src="images/homepage/resource.gif" style="width: 75px; height: 75px;" /><br />
                                    <span class="module_title">资源管理</span></a>
                            </td>
                            <td bgcolor="#19a355" class="grid">
                                <a href="admin.aspx?sub_sys=parameter">
                                    <img src="images/homepage/parameter.gif" style="width: 70px; height: 75px;" /><br />
                                    <span class="module_title">配置管理</span></a>
                            </td>
                        </tr>
                    </table>--%>
                </td>
            </tr>
            <tr>
                <td valign="bottom" height="100%">
                    <table width="100%" border="0" cellpadding="0" cellspacing="5">
                        <tr>
                            <td>
                                &nbsp;<br />
                                <%--<img src="./images/homepage/cmcc_logo.png" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
