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