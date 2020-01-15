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

public partial class youka_gasstation_gasstation_inster : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        SupplierService supplierService= new SupplierService();
        List<Supplier> supplierList = supplierService.GetAll();
        foreach (Supplier sp in supplierList)
        {
            ListItem li = new ListItem();
            li.Value = sp.Id.ToString();
            li.Text = sp.Name;
            ddlSupplier.Items.Insert(0, li);
        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        GasStationService gasStationService = new GasStationService();
        GasStation gasStation = new GasStation();
        gasStation.Name = Name.Text;
        gasStation.AddressName = AddressName.Text;
        gasStation.Longitude = Convert.ToDecimal(Longitude.Text);
        gasStation.Dimension = Convert.ToDecimal(Dimension.Text);
        gasStation.PrinterNumber = PrinterNumber.Text;
        gasStation.Brand = Brand.Text;
        gasStation.SupplierId = ConvertUtil.ToInt(ddlSupplier.SelectedValue);
        int Id = gasStationService.Insert(gasStation);

        OperaService operaService = new OperaService();
        operaService.Insert(new Opera()
        {
            BorrowerId = Id,
            FullName = CurrentUser.RealName,
            OperaType = OperaType.后台新增加油站点,
            RelationId = ConvertUtil.ToInt(CurrentUser.UserUid),
            Remark = "后台新增加油站点",
        });

        Response.Redirect("../gasstation/gasstation_list.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../gasstation/gasstation_list.aspx");
    }
}