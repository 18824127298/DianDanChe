using CheDaiBaoWeChatModel;
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

public partial class youka_oilgun_oilgun_insert : SbtPageBase
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
        OilGunService oilGunService = new OilGunService();
        for (int i = 0; i < GunNumber.Text.Split(',').Length; i++)
        {
            OilGun oilGun = new OilGun();
            oilGun.OilNumber = ddlOilNumber.SelectedValue;
            oilGun.GunNumber = ConvertUtil.ToInt(GunNumber.Text.Split(',')[i]);
            oilGun.Amount = Convert.ToDecimal(Amount.Text);
            oilGun.NewAmount = Convert.ToDecimal(NewAmount.Text);
            oilGun.PointTime = Convert.ToDateTime(PointTime.DateString);
            //oilGun.CountryMarkPrice = Convert.ToDecimal(CountryMarkPrice.Text);
            oilGun.GasStationId = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id"));
            //oilGun.NewCountryPrice = ConvertUtil.ToDecimal(NewCountryPrice.Text);
            //oilGun.CountryPointTime = Convert.ToDateTime(CountryPointTime.DateString);
            int Id = oilGunService.Insert(oilGun);
        }

        Response.Redirect("../oilgun/oilgun_list.aspx?id=" + ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id")));
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../oilgun/oilgun_list.aspx?id=" + ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id")));
    }
}