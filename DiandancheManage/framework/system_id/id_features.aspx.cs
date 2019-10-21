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
using Sigbit.Framework;
using Sigbit.Framework.License;


public partial class vacc_controller_controller_setting_plan_trigger_setting : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        edtCustomerCode.Text = SbtLicIDFeaturesConfig.Instance.CustomerCode;
        edtCustomerFullName.Text = SbtLicIDFeaturesConfig.Instance.CustomerFullName;
        edtCustomerBriefName.Text = SbtLicIDFeaturesConfig.Instance.CustomerBriefName;
        edtSystemFullName.Text = SbtLicIDFeaturesConfig.Instance.SystemFullName;
        edtSystemBriefName.Text = SbtLicIDFeaturesConfig.Instance.SystemBriefName;

       
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //======== 1. 校验输入是否正确 =========

        //============ 2. 设置计划相关的时延和访问地址 =============
        SbtLicIDFeaturesConfig.Instance.CustomerCode = edtCustomerCode.Text.Trim();
        SbtLicIDFeaturesConfig.Instance.CustomerFullName = edtCustomerFullName.Text.Trim();
        SbtLicIDFeaturesConfig.Instance.CustomerBriefName = edtCustomerBriefName.Text.Trim();
        SbtLicIDFeaturesConfig.Instance.SystemFullName = edtSystemFullName.Text.Trim();
        SbtLicIDFeaturesConfig.Instance.SystemBriefName = edtSystemBriefName.Text.Trim();

   

        //========== 3. 显示结果消息 ===========
        SbtMessageBoxPage page = new SbtMessageBoxPage(this);
        page.ReturnUrl
                = "~/framework/system_id/id_features.aspx";
        page.MessageText = "已重新设置系统相关标识信息。";
        page.Show();
    }
}
