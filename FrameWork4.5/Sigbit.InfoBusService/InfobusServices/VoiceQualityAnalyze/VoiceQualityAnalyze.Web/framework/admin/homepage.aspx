<%@ Page Language="C#" AutoEventWireup="true" CodeFile="homepage.aspx.cs" Inherits="farmwork_homepage" %>

<!DOCTYPE HTML>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>欢迎使用</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
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
            background: #5267da url(./images/homepage/bg.jpg) repeat-x 0 0;
        }
        .warp
        {
            background: url(./images/homepage/bgf2.jpg) no-repeat 50% 0;
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
            display:inline-block;
        }
        
        .module_title
        {
            font-family: 微软雅黑,宋体,黑体,Arial,sans-serif;
            font-weight: bold;
            font-size: 16px;
            color: White;
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
    </style>
</head>
<body>
    <div class="warp" align="center">
        <table width="700" border="0" cellpadding="0" cellpadding="0" height="100%">
            <tr>
                <td align="right" height="40">
                    <table width="100%" border=0>
                        <tr>
                            <td valign="bottom" class="index_top_user_info"><asp:Literal ID="ltCurrentUser" runat="server"></asp:Literal></td>
                            <td><div class="index_top_font">
                        IVR自动语音拨测管理平台</div></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellpadding="0" cellspacing="5">
                        <tr>
                            <td rowspan="2" bgcolor="#ec7c24" height="240" width="220">
                                <a href="admin.aspx?sub_sys=result">
                                    <img src="./images/homepage/micon_1.png" /><br />
                                    <span class="module_title">统计查询</span></a>
                            </td>
                            <td bgcolor="#1eb3b9" height="60">
                                <a href="admin.aspx?sub_sys=resource">
                                    <img src="./images/homepage/micon_2.png" />
                                    <span class="module_title">资源配置</span></a>
                            </td>
                            <td bgcolor="#7aaa1a" height="60">
                                <a href="admin.aspx?sub_sys=monitor">
                                    <img src="./images/homepage/micon_3.png" />
                                    <span class="module_title">监控告警</span></a>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#088ed7" height="60">
                                <a href="admin.aspx?sub_sys=schedule">
                                    <img src="./images/homepage/micon_4.png" />
                                    <span class="module_title">流程任务</span></a>
                            </td>
                            <td bgcolor="#8254ce" height="60">
                                <a href="admin.aspx?sub_sys=system">
                                    <img src="./images/homepage/micon_5.png" />
                                    <span class="module_title">系统管理</span></a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="bottom" height="100%">
                    <table width="100%" border="0" cellpadding="0" cellspacing="5">
                        <tr>
                            <td>
                                &nbsp;<br />
                                <img src="./images/homepage/cmcc_logo.png" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
