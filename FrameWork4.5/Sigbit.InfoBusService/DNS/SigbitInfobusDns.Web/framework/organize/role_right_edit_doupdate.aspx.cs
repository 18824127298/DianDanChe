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

using Sigbit.Framework.Role;

public partial class framework_organize_role_right_edit_doupdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ArrayList arrAllPopedom = new ArrayList();
        string sRoleUid = "";

        //======== 1. 判断每一个转入的参数，得到角色标识和权限列表 ===========
        for (int i = 0; i < Request.Params.Count; i++)
        {
            string sKey = Request.Params.GetKey(i);
            string[] arrValue = Request.Params.GetValues(i);
            //Label1.Text += sKey + " : ";

            if (sKey == "hidSBMXRoleUid")
            {
                if (arrValue.Length == 0)
                    throw new Exception("角色标识为空");
                sRoleUid = arrValue[0];
            }

            if (!sKey.StartsWith("SBMX"))
                continue;

            arrAllPopedom.Add(sKey.Substring(4));

            for (int j = 0; j < arrValue.Length; j++)
            {
                string sValue = arrValue[j];
                if (!sValue.StartsWith("SBMX"))
                    continue;

                arrAllPopedom.Add(sValue.Substring(4));
                //if (j != 0)
                //    Label1.Text += ",";
                //Label1.Text += sValue;
            }
        }

        SbtRoleRightUpdate roleRightUpdate = new SbtRoleRightUpdate();
        roleRightUpdate.UpdateRightsOfRole(sRoleUid, arrAllPopedom);

        Label1.Text = "角色标识：" + sRoleUid + "<br />\r\n";
        Label1.Text += "权限：<br />\r\n";
        for (int i = 0; i < arrAllPopedom.Count; i++)
        {
            string sPopedom = (string)arrAllPopedom[i];
            Label1.Text += sPopedom + "<br />\r\n";
        }

        Response.Redirect("role_manage_list.aspx");
    }
}
