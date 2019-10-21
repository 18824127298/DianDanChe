using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.App.Net.IBXService.VoiceReg.Service.CheckNewRequest__ForJS
{
    public class CheckNewRequest__ForJS
    {
        public static string DoCheck()
        {
            string sSQL = "select * from vreg_log_voice_isr where current_status = 'request' limit 1";
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

            if (ds.Tables[0].Rows.Count == 0)
                return "false";
            else
                return "true";
        }
    }
}
