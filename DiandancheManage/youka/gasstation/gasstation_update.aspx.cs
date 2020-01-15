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

public partial class youka_gasstation_gasstation_update : SbtPageBase
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
        SupplierService supplierService = new SupplierService();
        List<Supplier> supplierList = supplierService.GetAll();
        foreach (Supplier sp in supplierList)
        {
            ListItem li = new ListItem();
            li.Value = sp.Id.ToString();
            li.Text = sp.Name;
            ddlSupplier.Items.Insert(0, li);
        }

        GasStationService gasStationService = new GasStationService();
        GasStation gasStation = gasStationService.GetById(nId);
        Name.Text = gasStation.Name;
        AddressName.Text = gasStation.AddressName.ToString();
        Longitude.Text = gasStation.Longitude.ToString();
        Dimension.Text = gasStation.Dimension.ToString();
        PrinterNumber.Text = gasStation.PrinterNumber.ToString();
        Brand.Text = gasStation.Brand;
        ddlSupplier.SelectedValue = gasStation.SupplierId.ToString();

        
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        GasStationService gasStationService = new GasStationService();
        GasStation gasStation = new GasStation();
        gasStation.Id = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id"));
        gasStation.Name = Name.Text;
        gasStation.AddressName = AddressName.Text;
        gasStation.Longitude = Convert.ToDecimal(Longitude.Text);
        gasStation.Dimension = Convert.ToDecimal(Dimension.Text);
        gasStation.PrinterNumber = PrinterNumber.Text;
        gasStation.Brand = Brand.Text;
        gasStation.SupplierId = ConvertUtil.ToInt(ddlSupplier.SelectedValue);
        gasStationService.Update(gasStation);

        OperaService operaService = new OperaService();
        operaService.Insert(new Opera()
        {
            BorrowerId = gasStation.Id,
            FullName = CurrentUser.RealName,
            OperaType = OperaType.后台修改加油站点,
            RelationId = ConvertUtil.ToInt(CurrentUser.UserUid),
            Remark = "后台修改加油站点",
        });

        Response.Redirect("../gasstation/gasstation_list.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    { 
        Response.Redirect("../gasstation/gasstation_list.aspx");
    }
}