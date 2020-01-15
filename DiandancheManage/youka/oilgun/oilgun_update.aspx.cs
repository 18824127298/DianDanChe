using CheDaiBaoCommonService.Data;
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

        if (lId != 0)
        {
            PageParameter.SetCustomParamObject("lId", lId);
        }

        string oilne = ConvertUtil.ToString(Request["oilne"]);

        if (!string.IsNullOrEmpty(oilne))
        {
            PageParameter.SetCustomParamObject("oilne", oilne);
        }

        string oilnr = ConvertUtil.ToString(Request["oilnr"]);

        if (!string.IsNullOrEmpty(oilnr))
        {
            PageParameter.SetCustomParamObject("oilnr", oilnr);
        }
        string sql = "select GunNumber from OilGun where IsValid= 1 and GasStationId = " + nId + " and OilNumber= " + StringUtil.QuotedToDBStr(oilne + "#" + oilnr);
        List<int> lstGunNumber = SqlConnections.GetOpenConnection().Query<int>(sql).ToList();
        string gunNumber = "";
        foreach (int sGunNumber in lstGunNumber)
        {
            gunNumber += sGunNumber.ToString() + ",";
        }

        OilGunService oilGunService = new OilGunService();
        OilGun oilGun = oilGunService.Search(new OilGun() { IsValid = true }).Where(o => o.GasStationId == nId && o.OilNumber == oilne + "#" + oilnr).FirstOrDefault();
        ddlOilNumber.SelectedValue = oilGun.OilNumber;
        GunNumber.Text = gunNumber.TrimEnd(',');
        Amount.Text = oilGun.Amount.Value.ToString("N2");
        NewAmount.Text = oilGun.NewAmount.Value.ToString();
        PointTime.DateString = oilGun.PointTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //CountryMarkPrice.Text = oilGun.CountryMarkPrice.Value.ToString();
        //NewCountryPrice.Text = oilGun.NewCountryPrice.ToString();
        //CountryPointTime.DateString = oilGun.CountryPointTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        OilGunService oilGunService = new OilGunService();
        List<OilGun> oilGunList = oilGunService.Search(new OilGun() { IsValid = true }).Where(o => o.GasStationId == ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id")) && o.OilNumber == ConvertUtil.ToString(PageParameter.GetCustomParamObject("oilne")) + "#" + ConvertUtil.ToString(PageParameter.GetCustomParamObject("oilnr"))).ToList();
        foreach (OilGun oilGun in oilGunList)
        {
            oilGun.OilNumber = ddlOilNumber.SelectedValue;
            oilGun.GunNumber = ConvertUtil.ToInt(GunNumber.Text);
            oilGun.Amount = Convert.ToDecimal(Amount.Text);
            oilGun.NewAmount = Convert.ToDecimal(NewAmount.Text);
            oilGun.PointTime = Convert.ToDateTime(PointTime.DateString);
            //oilGun.CountryMarkPrice = Convert.ToDecimal(CountryMarkPrice.Text);
            //oilGun.NewCountryPrice = ConvertUtil.ToDecimal(NewCountryPrice.Text);
            //oilGun.CountryPointTime = Convert.ToDateTime(CountryPointTime.DateString);
            oilGunService.Update(oilGun);
        }
        Response.Redirect("../oilgun/oilgun_list.aspx?id=" + ConvertUtil.ToInt(PageParameter.GetCustomParamObject("lId")));
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../oilgun/oilgun_list.aspx?id=" + ConvertUtil.ToInt(PageParameter.GetCustomParamObject("lId")));
    }
}