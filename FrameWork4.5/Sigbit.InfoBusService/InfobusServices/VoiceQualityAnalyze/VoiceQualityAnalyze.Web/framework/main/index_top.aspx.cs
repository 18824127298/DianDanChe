using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Sigbit.Framework;

public partial class farmwork_main_index_top : SbtPageBase
{
    protected string _bodyTemplate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        _bodyTemplate = SbtTheme.GetThemeTemplate(CurrentUser.ThemePreference, "index_top");
        _bodyTemplate = _bodyTemplate.Replace("[%LOGIN_USER_ID%]", CurrentUser.UserName);
    }
}


/*
<body class="bodycolor2" onselectstart="return false" leftmargin="0" topmargin="0"
    onload="timeview();">
    <table id="__01" height="70" cellspacing="0" cellpadding="0" width="100%" align="right"
        border="0">
        <tbody>
            <tr align="right">
                <!-- 显示默认图标 -->
                <td nowrap align="left" width="94" rowspan="3">
                    <div align="left">
                        <img height="70" alt="" src="../../theme_images/main/index_top/index-01-sigbit.jpg" width="188" align="middle"></div>
                </td>
                <td nowrap align="left" width="76" rowspan="3">
                    <img height="70" alt="" src="../../theme_images/main/index_top/index_02.jpg" width="152" align="middle"></td>
                <td nowrap align="left" background="../../theme_images/main/index_top/index_03.jpg" rowspan="3">
                    &nbsp;</td>
                <td width="241" height="50">
                    <img height="50" alt="" src="../../theme_images/main/index_top/index_04.jpg" width="241" align="middle"></td>
                <td width="48">
                    <img style="cursor: hand" onclick="javascript:GoTable();" height="50" alt="返回桌面"
                        src="../../theme_images/main/index_top/index_05.jpg" width="48" border="0"></td>
                <td width="40">
                    <img style="cursor: hand" onclick="javascript:iflogout()" height="50" alt="注销 admin"
                        src="../../theme_images/main/index_top/index_06.jpg" width="40" border="0"></td>
                <td width="49">
                    <img style="cursor: hand" onclick="javascript:ifexit()" height="50" alt="退出系统" src="../../theme_images/main/index_top/index_07.jpg"
                        width="49" border="0"></td>
            </tr>
            <tr align="right">
                <td class="small" style="color: #d9e9d6" align="right" background="../../theme_images/main/index_top/index_08.jpg"
                    colspan="4" height="20">
                    现在时刻：<b><span id="time_area"></span></b>&nbsp;&nbsp;&nbsp;&nbsp;</td>
            </tr>
        </tbody>
    </table>
</body>
 */

/*
 <%=_bodyTemplate %>
*/