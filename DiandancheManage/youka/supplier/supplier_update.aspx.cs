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

public partial class youka_supplier_supplier_update : SbtPageBase
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
        Supplier supplier = supplierService.GetById(nId);
        Name.Text = supplier.Name;
        Phone.Text = supplier.Phone;
        Concessional.Text = supplier.Concessional.Value.ToString();
        NewConcessional.Text = supplier.NewConcessional.Value.ToString();
        ConcessionalPointTime.DateTimeString = supplier.ConcessionalPointTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        SupplierService supplierService = new SupplierService();
        Supplier supplier = new Supplier();
        supplier.Id = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id"));
        supplier.Name = Name.Text;
        supplier.Phone = Phone.Text;
        supplier.Concessional = ConvertUtil.ToDecimal(Concessional.Text);
        supplier.NewConcessional = ConvertUtil.ToDecimal(NewConcessional.Text);
        supplier.ConcessionalPointTime = ConcessionalPointTime.DateTime;
        supplierService.Update(supplier);

        Response.Redirect("../supplier/supplier_list.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../supplier/supplier_list.aspx");
    }
}