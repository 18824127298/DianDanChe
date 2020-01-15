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

public partial class youka_gasstationlevel_gasstationlevel_insert : SbtPageBase
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


    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        GasStationLevelService gasStationLevelService = new GasStationLevelService();
        GasStationLevel gasStationLevel = new GasStationLevel();
        gasStationLevel.MemberLevel = ConvertUtil.ToInt(MemberLevel.Text);
        gasStationLevel.Reduction = ConvertUtil.ToDecimal(Reduction.Text);
        gasStationLevel.NewReduction = ConvertUtil.ToDecimal(NewReduction.Text);
        gasStationLevel.ReductionTime = ReductionTime.DateTime;
        gasStationLevel.GasStationId = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id"));
        int Id = gasStationLevelService.Insert(gasStationLevel);

        Response.Redirect("../gasstationlevel/gasstationlevel_list.aspx?id=" + ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id")));
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../gasstationlevel/gasstationlevel_list.aspx?id=" + ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id")));
    }
}
