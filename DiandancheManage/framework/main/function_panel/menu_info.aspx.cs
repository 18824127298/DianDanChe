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

public partial class farmwork_main_function_panel_menu_info : SbtPageBase
{
    protected string _bodyTemplate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        _bodyTemplate = SbtTheme.GetThemeTemplate(CurrentUser.ThemePreference, 
                "fpanel_menu_info");

        //========= 1. 部门名称, DEPT_NAME ===========
        _bodyTemplate = _bodyTemplate.Replace("[%DEPT_NAME%]", CurrentUser.DepartName);

        //======== 2. 用户名, USER_PRIV ===========
        _bodyTemplate = _bodyTemplate.Replace("[%USER_PRIV%]", CurrentUser.UserName);

        //========= 3. 用户真实姓名，USER_NAME ========
        _bodyTemplate = _bodyTemplate.Replace("[%USER_NAME%]", CurrentUser.RealName);

        //========= 4. 欢迎词，HELLO_STR ==========
        DateTime dtNow = DateTime.Now;
        string sMorning = "";
        int nHour = dtNow.Hour;
        int nMinute = dtNow.Minute;
        int nCurrentTime = nHour * 100 + nMinute;
        if (nCurrentTime <= 430 || nCurrentTime >= 2300)
            sMorning = "夜里";
        else if (nCurrentTime <= 740)
            sMorning = "早晨";
        else if (nCurrentTime <= 850)
            sMorning = "早上";
        else if (nCurrentTime <= 1130)
            sMorning = "上午";
        else if (nCurrentTime <= 1330)
            sMorning = "中午";
        else if (nCurrentTime <= 1730)
            sMorning = "下午";
        else if (nCurrentTime <= 1920)
            sMorning = "傍晚";
        else
            sMorning = "晚上";
        sMorning += "好！";
        _bodyTemplate = _bodyTemplate.Replace("[%HELLO_STR%]", sMorning);
        
        /*
        SbtUser user = (SbtUser)Session["currentUserMEFTMG"];
        lblUserRealName.Text = user.RealName;
        lblUserDepartName.Text = user.DepartName;
        lblUserName.Text = user.Name;

        //======= 1. 得到当前的小时数 ======
        DateTime dtNow = DateTime.Now;
        string sMorning = "";
        int nHour = dtNow.Hour;
        int nMinute = dtNow.Minute;
        int nCurrentTime = nHour * 100 + nMinute;
        if (nCurrentTime <= 430 || nCurrentTime >= 2300)
            sMorning = "夜里";
        else if (nCurrentTime <= 740)
            sMorning = "早晨";
        else if (nCurrentTime <= 850)
            sMorning = "早上";
        else if (nCurrentTime <= 1130)
            sMorning = "上午";
        else if (nCurrentTime <= 1330)
            sMorning = "中午";
        else if (nCurrentTime <= 1730)
            sMorning = "下午";
        else if (nCurrentTime <= 1920)
            sMorning = "傍晚";
        else 
            sMorning = "晚上";
        lblMorning.Text = sMorning;
         */ 
    }
}

/*
<body class="bodycolor2" onselectstart="return false">
    <table height="68" cellspacing="0" cellpadding="0" width="186" border="0">
        <tbody>
            <tr>
                <td nowrap background="../../../theme_images/main/menu/index_10.jpg" colspan="2">
                    <table cellspacing="2" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td>
                                    <div class="small" style="color: #ffffff" align="center">
                                        <asp:Label ID="lblUserDepartName" runat="server" Text="Label"></asp:Label>
                                        &gt;&gt; <asp:Label ID="lblUserName" runat="server" Text="Label"></asp:Label>
                                        </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="small" style="color: #ffffff" align="center">
                                        <img src="../../../theme_images/main/menu/1.gif" align="absMiddle">
                                        <asp:Label ID="lblUserRealName" runat="server" Text="Label"></asp:Label>
                                        ，<asp:Label ID="lblMorning" runat="server" Text="Label"></asp:Label>好！<b></b></div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</body>
 */