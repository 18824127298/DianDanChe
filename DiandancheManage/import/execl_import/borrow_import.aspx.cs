using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using Sigbit.Common;
using Sigbit.Framework;
using Sigbit.Net.BIPPacket;
using Sigbit.Net.CsvPacket;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class import_execl_import_borrow_import : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (fulUpload.PostedFile == null)
        {
            lblErrMessage.Text = "请上传文件";
            lblErrMessage.Visible = true;
            return;
        }

        //============= 1.上传文件 ================

        string sUploadPath = MapPath("");

        string sUploadFileName = "loan_export.csv";

        string sFullUploadFileName = sUploadPath + "\\" + sUploadFileName;

        fulUpload.PostedFile.SaveAs(sFullUploadFileName);


        //============ 2.文件检查 =================
        CsvPacket csvConfig = new CsvPacket();
        csvConfig.ReadFromFile(sFullUploadFileName);
        File.Delete(sFullUploadFileName);

        BIPDataSet ds = csvConfig.GetDataSet();

        string sOutputMessage = "";

        LoanApplyService loanapplyService = new LoanApplyService();
        BorrowerService borrowerService = new BorrowerService();
        BorrowService borrowService = new BorrowService();
        List<LoanApply> LoanApplyAll = loanapplyService.GetAll();
        List<Borrower> borrowerAll = borrowerService.GetAll();
        List<Borrow> borrowAll = borrowService.GetAll();

        int j = 0;
        for (int i = 1; i <= ds.GetRecordCount(); i++)
        {
            string sFullName = ds.GetItemString(i, 0);
            string sRepaymentDate = ds.GetItemString(i, 1);
            string sTages = ds.GetItemString(i, 2).Split('/')[0].ToString().Replace("期", "");
            string sTotalPeriod = ds.GetItemString(i, 2).Split('/')[1].ToString().Replace("期", "");
            string sPrincipal = ds.GetItemString(i, 3);
            string sInterest = ds.GetItemString(i, 4);
            string sOverDay = ds.GetItemString(i, 5);
            string sOverInterest = ds.GetItemString(i, 6);
            string sActualRepaymentDate = ds.GetItemString(i, 7);
            Borrower borrowerId = borrowerAll.Find(o => o.FullName == sFullName.Trim());

            if (borrowerId == null)
            {
                sOutputMessage += "第" + (i + 1) + "行,客户" + sFullName + "找不到该客户会员信息<br/>";
                j++;
                continue;
            }

            LoanApply loanApply = LoanApplyAll.Find(o => o.CreditPhone == borrowerId.Phone && (o.RepaymentStatus == CreditStatus.还款中 || o.RepaymentStatus == CreditStatus.还款完成));

            if (loanApply == null)
            {
                sOutputMessage += "第" + (i + 1) + "行,客户" + sFullName + "找不到借贷<br/>";
                j++;
                continue;
            }


            Borrow borrow = borrowAll.Find(o => o.Stages == ConvertUtil.ToInt(sTages) && o.LoanApplyId == loanApply.Id && o.BorrowerId == borrowerId.Id);
            if (borrow != null)
            {
                sOutputMessage += "第" + (i + 1) + "行,客户" + sFullName + ",第" + sTages + "期账单已存在<br/>";
                j++;
                continue;
            }
            Borrow tbl = new Borrow();
            tbl.BorrowerId = borrowerId.Id;
            tbl.LoanApplyId = loanApply.Id;
            tbl.RepaymentDate = Convert.ToDateTime(sRepaymentDate);
            tbl.Stages = ConvertUtil.ToInt(sTages);
            tbl.Principal = ConvertUtil.ToDecimal(sPrincipal);
            tbl.Interest = ConvertUtil.ToDecimal(sInterest);
            tbl.OverInterest = ConvertUtil.ToDecimal(sOverInterest);
            tbl.OverDay = ConvertUtil.ToInt(sOverDay);
            tbl.UnPrincipal = sActualRepaymentDate == "" ? ConvertUtil.ToDecimal(sPrincipal) : 0;
            tbl.UnTotalInterest = sActualRepaymentDate == "" ? ConvertUtil.ToDecimal(sInterest) : 0;
            tbl.TotalPeriod = ConvertUtil.ToInt(sTotalPeriod);
            if (sActualRepaymentDate != "")
            {
                tbl.ActualRepaymentDate = Convert.ToDateTime(sActualRepaymentDate);
                tbl.RepaymentPlanMode = tbl.RepaymentDate < tbl.ActualRepaymentDate.Value.Date ? RepaymentPlanMode.逾期还款 :
                    tbl.ActualRepaymentDate <= tbl.RepaymentDate.Value.Date.AddMonths(-1) ? RepaymentPlanMode.提前还款 : RepaymentPlanMode.正常还款;
            }
            borrowService.Insert(tbl);

        }
        lblErrMessage.Text = sOutputMessage;
        sOutputMessage += "总共" + ds.GetRecordCount() + "条数据,成功导入" + (ds.GetRecordCount() - j) + "条";
        lblErrMessage.Text = sOutputMessage;
        SbtAppLogger.LogAction("批量导入贷款信息", "导入" + ds.GetRecordCount() + "个贷款");
    }
}