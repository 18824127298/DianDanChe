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
using Sigbit.App.Net.IBXService;

public partial class genui_FIEI_dns_service_search : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========== 1. ��ʼ������ ==========
        //========== 2. ȡ����ѯ���� ==========
        if (PageParameter.StringParam[0] != "dns_serviceFIEI")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. ˢ��������ʾ ==========
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. ��������������� ==========
        if (PageParameter.StringParam[0] != "dns_serviceFIEI")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. ������������ ==========
        //========== 3. ���ص������б�ҳ�� ==========
        Response.Redirect("dns_service_list.aspx");
    }
}
