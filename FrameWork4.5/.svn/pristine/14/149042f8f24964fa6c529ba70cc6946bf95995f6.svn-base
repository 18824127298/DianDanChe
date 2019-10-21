using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;

namespace Sigbit.Framework
{
    public class LoginLogger__ForJS
    {
        public static void Logout(string sLogoutCause)
        {
            TbLogUserLogin tblLogLogin = SbtAppContext.CurrentUserLogLoginRec;
            if (tblLogLogin == null)
                return;

            tblLogLogin.LogoutTime = DateTimeUtil.Now;
            tblLogLogin.InSystemDuration = DateTimeUtil.SecondsAfter(tblLogLogin.LoginTime, tblLogLogin.LogoutTime);

            if (tblLogLogin.InSystemDuration < 0 || tblLogLogin.InSystemDuration > 24 * 3600)
                tblLogLogin.InSystemDuration = 0;

            tblLogLogin.LogoutCause = sLogoutCause;
            tblLogLogin.HasLogout = "Y";

            tblLogLogin.Update();

            SbtAppContext.CurrentUserLogLoginRec = null;
        }

        public static void HeartBeat()
        {
            TbLogUserLogin tblLogLogin = SbtAppContext.CurrentUserLogLoginRec;
            if (tblLogLogin == null)
                return;

            tblLogLogin.LastHeartbeatTime = DateTimeUtil.Now;

            tblLogLogin.Update();
        }
    }
}
