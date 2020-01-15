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
using WxPayApi;

public partial class caiwu_caiwu_fangkua_zuche_fangkuang : SbtPageBase
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


        //========== 4. 取数据 ==========
        LoanApplyService loanapplyService = new LoanApplyService();
        LoanApply loanapply = loanapplyService.GetById(ConvertUtil.ToInt(sRecordPrimaryKey));

        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetById(loanapply.BorrowerId);
        //========== 5. 更新各控件的显示 ==========
        lblFullname.Text = borrower.FullName;
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("zuche_sure_list.aspx");
    }

    protected void btnFangKuang_Click(object sender, EventArgs e)
    {
        int nRecordPrimaryKey = ConvertUtil.ToInt(ViewState["rec_key"]);
        try
        {
            int nId = ConvertUtil.ToInt(nRecordPrimaryKey);
            LoanApplyService loanapplyService = new LoanApplyService();
            LoanApply loanapply = loanapplyService.GetById(nId);
            loanapply.InterestDate = DateTime.Now.Date;
            loanapply.ExpectedRepayment = DateTime.Now.Date.AddMonths(ConvertUtil.ToInt(loanapply.Deadline));
            loanapply.IsLending = true;
            loanapply.LendingDate = DateTime.Now.Date;
            loanapply.BatchDate = DateTime.Now.Date.AddDays(-1);
            loanapply.Brand = Brand.SelectedValue;
            loanapply.CheType = CheType.SelectedValue;
            loanapply.BicycleNumber = BicycleNumber.Text;
            loanapplyService.Update(loanapply);

            decimal dBenjin = ConvertUtil.ToDecimal((loanapply.TotalAmountStage / loanapply.Deadline).Value.ToString("N2"));


            BorrowService borrowService = new BorrowService();
            Borrow borrow = new Borrow();
            for (int i = 0; i < loanapply.Deadline; i++)
            {
                borrow.BorrowerId = loanapply.BorrowerId;
                borrow.LoanApplyId = loanapply.Id;
                borrow.RepaymentDate = DateTime.Now.AddMonths(i + 1).Date;
                borrow.Stages = i + 1;
                borrow.TotalPeriod = loanapply.Deadline.Value;
                borrow.OverInterest = 0;
                borrow.OverDay = 0;
                borrow.BreachAmount = 0;
                if (i + 1 == loanapply.Deadline)
                {
                    borrow.Principal = loanapply.TotalAmountStage.Value - dBenjin * (loanapply.Deadline - 1);
                    borrow.UnPrincipal = loanapply.TotalAmountStage.Value - dBenjin * (loanapply.Deadline - 1);
                    borrow.Interest = loanapply.BicyclesRent - borrow.Principal;
                    borrow.UnTotalInterest = loanapply.BicyclesRent - borrow.UnPrincipal;
                }
                else
                {
                    borrow.Principal = dBenjin;
                    borrow.UnPrincipal = dBenjin;
                    borrow.Interest = loanapply.BicyclesRent - dBenjin;
                    borrow.UnTotalInterest = loanapply.BicyclesRent - dBenjin;
                }


                borrowService.Insert(borrow);
            }
            FundsFlowService fundsflowService = new FundsFlowService();
            fundsflowService.Insert(new FundsFlow()
            {
                Amount = loanapply.TotalAmountStage,
                IncomeGodId = 5,
                FeeType = FeeType.平台打款,
                IsComputing = true,
                PayGodId = 2,
                LoanApplyId = loanapply.Id,
                IsFreeze = false,
                Remark = "打款"
            });

            Response.Redirect("zuche_sure_list.aspx");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}