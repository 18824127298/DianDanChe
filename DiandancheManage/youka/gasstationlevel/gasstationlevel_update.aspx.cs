using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using Sigbit.Common;
using Sigbit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class youka_gasstationlevel_gasstationlevel_update : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        int nId = ConvertUtil.ToInt(Request["id"]);

        if (nId != 0)
        {
            PageParameter.SetCustomParamObject("id", nId);
        }
        int lId = ConvertUtil.ToInt(Request["lId"]);

        if (nId != 0)
        {
            PageParameter.SetCustomParamObject("lId", lId);
        }
        GasStationLevelService gasStationLevelService = new GasStationLevelService();
        GasStationLevel gasStationLevel = gasStationLevelService.GetById(nId);
        MemberLevel.Text = gasStationLevel.MemberLevel.ToString();
        Reduction.Text = gasStationLevel.Reduction.Value.ToString("N2");

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        GasStationLevelService gasStationLevelService = new GasStationLevelService();
        GasStationLevel gasStationLevel = new GasStationLevel();
        gasStationLevel.Id = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id"));
        gasStationLevel.MemberLevel = ConvertUtil.ToInt(MemberLevel.Text);
        gasStationLevel.Reduction = ConvertUtil.ToDecimal(Reduction.Text);
        gasStationLevelService.Update(gasStationLevel);
        Response.Redirect("../gasstationlevel/gasstationlevel_list.aspx?id=" + ConvertUtil.ToInt(PageParameter.GetCustomParamObject("lId")));
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../gasstationlevel/gasstationlevel_list.aspx?id=" + ConvertUtil.ToInt(PageParameter.GetCustomParamObject("lId")));
    }
}