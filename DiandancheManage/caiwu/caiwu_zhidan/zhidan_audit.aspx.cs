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

public partial class caiwu_caiwu_zhidan_zhidan_audit : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        //========== 1. 初始化界面 ==========
        //========== 2. 取出传入条件 ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. 新增的判断和处理 ==========
        if (sRecordPrimaryKey == "")
        {
            lblTitle.Text = "新增充值记录";

            return;
        }

        //========== 4. 取数据 ==========
        RechargeService rechargeService = new RechargeService();
        Recharge recharge = rechargeService.GetById(ConvertUtil.ToInt(sRecordPrimaryKey));

        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetById(recharge.BorrowerId);


        //========== 5. 更新各控件的显示 ==========
        lblFullname.Text = borrower.FullName;
        lblAmount.Text = (recharge.Amount / 100).ToString();
        lblOrderNumber.Text = recharge.OrderNumber;
        lblCreator.Text = recharge.Creator;
        lblCreateTime.Text = recharge.RechargeTime;
        lblRechargeRemark.Text = recharge.RechargeRemark;
    }

    protected void btnAudit_Click(object sender, EventArgs e)
    {
        SbtMessageBoxPage msgPage = new SbtMessageBoxPage(this);
        msgPage.ReturnUrl = "~/caiwu/caiwu_zhidan/caiwu_zhidan_list.aspx";
        int nRecordPrimaryKey = ConvertUtil.ToInt(ViewState["rec_key"]);

        string sRemark = edtRemark.Text.Trim();
        if (sRemark == "")
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "备注不能为空！";
            edtRemark.Focus();
            return;
        }

        RechargeService rechargeService = new RechargeService();
        Recharge recharge = rechargeService.GetById(nRecordPrimaryKey);


        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetById(recharge.BorrowerId);

        string sRecharegAlias = borrower.Phone;

        if (recharge.RechargeMode != RechargeMode.后台充值)
        {
            msgPage.MessageText = "只能审核后台充值记录";
            msgPage.Show();
            return;
        }

        if (recharge.IsAudit != null)
        {
            msgPage.MessageText = "该记录已经审核操作过了，无需再进行审核操作";
            msgPage.Show();
            return;
        }


        recharge.Auditor = CurrentUser.RealName;
        recharge.AuditRemark = sRemark;
        recharge.IsAudit = true;
        recharge.AuditTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        rechargeService.Update(recharge);

        FundsFlowService fundsflowService = new FundsFlowService();
        fundsflowService.Insert(new FundsFlow()
        {
            Amount = recharge.Amount / 100,
            FeeType = FeeType.后台充值,
            IncomeGodId = recharge.BorrowerId,
            IsFreeze = false,
            PayGodId = 3,
            Remark = FeeType.后台充值 + "",
            IsComputing = true,
            RelationId = recharge.Id
        });

        if (recharge.ActualRechargeFee.Value > 0)
        {
            fundsflowService.Insert(new FundsFlow()
            {
                Amount = recharge.ActualRechargeFee.Value / 100,
                FeeType = FeeType.第三方收取的充值手续费,
                IncomeGodId = 3,
                PayGodId = 2,
                IsComputing = true,
                IsFreeze = false,
                RelationId = recharge.Id,
                Remark = string.Format("支付给{0}的充值手续费", recharge.RechargeMode)
            });
        }


        OperaService operaService = new OperaService();
        operaService.Insert(new Opera()
        {
            OperaType = OperaType.后台充值,
            BorrowerId = borrower.Id,
            RelationId = nRecordPrimaryKey,
            Remark = sRemark,
            ClientAddress = Request.UserHostAddress
        });




        SbtAppLogger.LogAction("充值审核通过", string.Format("充值用户{0},充值金额{1},充值时间{2},操作备注:{3};",
            borrower.Aliases, recharge.Amount.Value.ToString("0.00"), recharge.RechargeTime, sRemark));


        msgPage.MessageText = "充值审核通过";
        msgPage.Show();

    }

    protected void btnUnAudit_Click(object sender, EventArgs e)
    {
        SbtMessageBoxPage msgPage = new SbtMessageBoxPage(this);
        msgPage.ReturnUrl = "~/caiwu/caiwu_zhidan/caiwu_zhidan_list.aspx";

        int nRecordPrimaryKey = ConvertUtil.ToInt(ViewState["rec_key"]);

        string sRemark = edtRemark.Text.Trim();
        if (sRemark == "")
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "请填写不通过原因！";
            edtRemark.Focus();
            return;
        }

        RechargeService rechargeService = new RechargeService();
        Recharge recharge = rechargeService.GetById(nRecordPrimaryKey);


        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetById(recharge.BorrowerId);

        string sRecharegAlias = borrower.Phone;

        recharge.Auditor = CurrentUser.RealName;
        recharge.AuditRemark = sRemark;
        recharge.IsAudit = false;
        recharge.AuditTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        rechargeService.Update(recharge);




        SbtAppLogger.LogAction("充值审核不通过", string.Format("充值用户{0},充值金额{1},充值时间{2},不通过原因:{3};",
              borrower.Aliases, recharge.Amount.Value.ToString("0.00"), recharge.RechargeTime, sRemark));

        NaviTabController.ClearBar();
        //========== 4. 返回到主页面 ==========
        msgPage.MessageText = "充值审核不通过操作完成";
        msgPage.Show();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("caiwu_zhidan_list.aspx");
    }

}
