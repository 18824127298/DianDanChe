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

public partial class youka_oilgun_oilgun_update : SbtPageBase
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
        OilGunService oilGunService = new OilGunService();
        OilGun oilGun = oilGunService.GetById(nId);
        ddlOilNumber.SelectedValue = oilGun.OilNumber;
        GunNumber.Text = oilGun.GunNumber.ToString();
        Amount.Text = oilGun.Amount.Value.ToString("N2");
        NewAmount.Text = oilGun.NewAmount.Value.ToString();
        PointTime.DateString = oilGun.PointTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
        CountryMarkPrice.Text = oilGun.CountryMarkPrice.Value.ToString();
        NewCountryPrice.Text = oilGun.NewCountryPrice.ToString();
        CountryPointTime.DateString = oilGun.CountryPointTime.Value.ToString("yyyy-MM-dd HH:mm:ss"); 
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        OilGunService oilGunService = new OilGunService();
        OilGun oilGun = oilGunService.GetById(ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id")));
        oilGun.OilNumber = ddlOilNumber.SelectedValue;
        oilGun.GunNumber = ConvertUtil.ToInt(GunNumber.Text);
        oilGun.Amount = Convert.ToDecimal(Amount.Text);
        oilGun.NewAmount = Convert.ToDecimal(NewAmount.Text);
        oilGun.PointTime = Convert.ToDateTime(PointTime.DateString);
        oilGun.CountryMarkPrice = Convert.ToDecimal(CountryMarkPrice.Text);
        oilGun.NewCountryPrice = ConvertUtil.ToDecimal(NewCountryPrice.Text);
        oilGun.CountryPointTime = Convert.ToDateTime(CountryPointTime.DateString);
        oilGunService.Update(oilGun);

        Response.Redirect("../oilgun/oilgun_list.aspx?id=" + ConvertUtil.ToInt(PageParameter.GetCustomParamObject("lId")));
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../oilgun/oilgun_list.aspx?id=" + ConvertUtil.ToInt(PageParameter.GetCustomParamObject("lId")));
    }
}