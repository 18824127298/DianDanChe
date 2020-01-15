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

public partial class caiwu_caiwu_fangkua_zuche_over_audit : SbtPageBase
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
        lblBicycleNumber.Text = loanapply.BicycleNumber;
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("zuche_over_list.aspx");
    }

    protected void btnFangKuang_Click(object sender, EventArgs e)
    {
        int nRecordPrimaryKey = ConvertUtil.ToInt(ViewState["rec_key"]);
        try
        {
            int nId = ConvertUtil.ToInt(nRecordPrimaryKey);
            LoanApplyService loanapplyService = new LoanApplyService();
            LoanApply loanapply = loanapplyService.GetById(nId);
            loanapply.RepaymentStatus = CreditStatus.还款完成;
            loanapply.ClosingDate = DateTime.Now;
            loanapply.RepaymentPlanMode = RepaymentPlanMode.提前还款;
            loanapplyService.Update(loanapply);

            BorrowService borrowService = new BorrowService();
            List<Borrow> borrowList = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.LoanApplyId == loanapply.Id && o.BorrowerId == loanapply.BorrowerId && (o.UnPrincipal + o.UnTotalInterest) > 0).ToList();

            foreach (Borrow borrow in borrowList)
            {
                if (borrow.OverDay > 0)
                {
                    borrow.ActualRepaymentDate = DateTime.Now;
                    borrow.RepaymentPlanMode = RepaymentPlanMode.逾期还款;
                    borrow.UnPrincipal = 0;
                    borrow.UnTotalInterest = 0;
                    borrowService.Update(borrow);
                }
                else
                {
                    borrow.ActualRepaymentDate = DateTime.Now;
                    borrow.RepaymentPlanMode = RepaymentPlanMode.提前还款;
                    borrow.UnPrincipal = 0;
                    borrow.UnTotalInterest = 0;
                    borrowService.Update(borrow);
                }
            }

            Response.Redirect("zuche_over_list.aspx");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}