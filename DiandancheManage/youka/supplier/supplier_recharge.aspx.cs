using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Sigbit.Common;
using Sigbit.Data;
using Sigbit.Framework;
using Sigbit.Web.WebControlUtil;
using System.Collections.Generic;
using System.Text;
using Sigbit.Web;
using CheDaiBaoWeChatService.Service;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatModel;
public partial class youka_supplier_supplier_recharge : SbtPageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {


        //========== 1. 初始化界面 ==========
        if (IsPostBack)
            return;


        int nId = ConvertUtil.ToInt(Request["Id"]);
        ViewState["Id"] = nId.ToString();

        SupplierService supplierService = new SupplierService();
        Supplier supplier = supplierService.GetById(nId);
        lblSupplierName.Text = supplier.Name;
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        int nId = ConvertUtil.ToInt(ViewState["Id"]);


        if (edtAmount.Text.Trim() == "")
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "金额不能为空！";
            edtAmount.Focus();
            return;
        }


        if (edtRemark.Text == "")
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "备注不能为空！";
            edtRemark.Focus();
            return;
        }


        int nInputMoney = 0;
        bool isInt = int.TryParse(edtAmount.Text.Trim(), out nInputMoney);
        if (!isInt)
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "请输入正确格式的金额！";
            edtAmount.Focus();
            return;
        }
        nInputMoney = nInputMoney * 100;
        if (nInputMoney <= 0)
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "请输入大于0的金额！";
            edtAmount.Focus();
            return;
        }

        SupplierFundsFlowService spplierFundsFlowService = new SupplierFundsFlowService();
        int nRelationId = spplierFundsFlowService.Insert(new SupplierFundsFlow()
        {
            Amount = ConvertUtil.ToDecimal(edtAmount.Text.Trim()),
            RechargeTime = RechargeTime.DateTime,
            FeeType = FeeType.后台充值,
            IncomeSupplierId = nId,
            PaySupplierId = 1,
            IsComputing = true,
            IsFreeze = false,
            Remark = "后台充值"
        });


        SupplierService supplierService = new SupplierService();
        Supplier supplier = supplierService.GetById(nId);
        supplier.Balance += ConvertUtil.ToDecimal(edtAmount.Text.Trim());
        supplierService.Update(supplier);
        OperaService operaService = new OperaService();
        operaService.Insert(new Opera()
        {
            BorrowerId = Convert.ToInt32(CurrentUser.RecordData.ThirdPartyCode),
            OperaType = OperaType.后台添加充值数据,
            RelationId = nRelationId,
            Remark = string.Format("后台线下充值，充值会员：{0}，充值金额：{1}，操作人ID：{2}",
                supplier.Name, edtAmount.Text.Trim(), Convert.ToInt32(CurrentUser.RecordData.ThirdPartyCode))
        });



        SbtAppLogger.LogAction("后台线下充值", string.Format("给用户{0}后台线下充值,充值金额{1}元,备注:{2};",
             supplier.Name, edtAmount.Text.Trim(), edtRemark.Text.Trim()));

        SbtMessageBoxPage msgPage = new SbtMessageBoxPage(this);
        msgPage.MessageText = "后台充值成功，充值金额:" + edtAmount.Text.Trim() + "元";
        msgPage.ReturnUrl = this.Request.Url.AbsoluteUri;

        msgPage.Show();
    }
}