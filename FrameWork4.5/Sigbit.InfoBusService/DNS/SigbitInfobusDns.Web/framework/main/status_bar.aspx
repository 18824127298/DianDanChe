<%@ Page Language="C#" AutoEventWireup="true" CodeFile="status_bar.aspx.cs" Inherits="framework_main_status_bar" %>

<!--DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <SCRIPT language=JavaScript>
//============================== 普通界面调用函数 ====================================
function killErrors()
{
  return true;
}
window.onerror = killErrors;

function show_online()
{
   parent.function_panel.view_menu(2);
}

function main_refresh()
{
   parent.table_index.table_main.location.reload();
}
</SCRIPT>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" marginheight="0" marginwidth="0">
<div class="statusBar">
    <table height="20" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tbody>
            <tr>
                <td width="6">
                    <asp:Image ID="Image1" runat="server" SkinID="statusBarLeftImage" />
                </td>
                <td style="cursor: hand" align="left" width="85">
                    <input 
                        class="statusBarOnlineNum" readonly size="3" name="user_count1" id="Text1" runat="server">
                </td>
                <td style="color: #ffffff" align=center width="38">
                    <span id="new_sms"></span>
                </td>
                <td style="color: #ffffff" align=center width="57">
                    <span id="new_leter"></span>
                </td>
                <td class="statusBarCenterText" title="点击这里刷新主操作区" 
                    nowrap align=center width="200">
                    </td>
                <td valign= middle align=center width="91">
                    <a style="color: #d50000" href="" target="table_main">
                        <b></b></a></td>
                <td align="right" width="25">
                    <a href="javascript:history.back()">
                        <img height="16" alt="后退" src="../images/status_bar_icon/arrow_back.gif" width="16" border="0"></a></td>
                <td align="right" width="25">
                    <a href="javascript:history.forward()">
                        <img height="16" alt="前进" src="../images/status_bar_icon/arrow_forward.gif" width="16" border="0"></a></td>
                <td nowrap align="right" width="25">
                    <a href="" target="_blank">
                        <img height="16" alt="使用帮助" src="../images/status_bar_icon/help.gif" width="16" border="0"></a></td>
                <td nowrap align="right" width="25">
                    <a title="显示软件版权信息" href="" target="table_main">
                        <img height="16" alt="版权信息" src="../images/status_bar_icon/i_about.gif" width="16" border="0"></a></td>
                <td width="6">
                    &nbsp;</td>
            </tr>
        </tbody>
    </table>
</div>
    <iframe name="ref_new_letter" src="" width="0"
        height="0"></iframe>
    <iframe name="ref_sms" src="" width="0" height="0"></iframe>
</body>
</html>
