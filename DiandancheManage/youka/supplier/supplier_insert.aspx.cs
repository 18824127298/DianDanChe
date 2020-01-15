using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using Sigbit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class youka_supplier_supplier_insert : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        SupplierService supplierService = new SupplierService();
        Supplier supplier = new Supplier();
        supplier.Name = Name.Text;
        supplier.Phone = Phone.Text;
        supplier.Concessional = Convert.ToDecimal(Concessional.Text);
        supplier.NewConcessional = Convert.ToDecimal(NewConcessional.Text);
        supplier.ConcessionalPointTime = ConcessionalPointTime.DateTime;
        supplier.Balance = 0;
        supplierService.Insert(supplier);

        Response.Redirect("../supplier/supplier_list.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../supplier/supplier_list.aspx");
    }
}