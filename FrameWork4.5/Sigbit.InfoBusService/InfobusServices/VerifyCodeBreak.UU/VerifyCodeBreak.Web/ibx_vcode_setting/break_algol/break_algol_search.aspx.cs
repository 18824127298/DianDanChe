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

using Sigbit.Common;
using Sigbit.Data;
using Sigbit.Framework;
using Sigbit.Web.WebControlUtil;
using Sigbit.App.Net.IBXService.VCodeBreak.Service.DBDefine;

public partial class genui_AURP_break_algol_search : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========== 1. ��ʼ������ ==========
        //========== 2. ȡ����ѯ���� ==========
        if (PageParameter.StringParam[0] != "break_algolAURP")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. ˢ��������ʾ ==========
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. ��������������� ==========
        if (PageParameter.StringParam[0] != "break_algolAURP")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. ������������ ==========
        //========== 3. ���ص������б�ҳ�� ==========
        Response.Redirect("break_algol_list.aspx");
    }
}
