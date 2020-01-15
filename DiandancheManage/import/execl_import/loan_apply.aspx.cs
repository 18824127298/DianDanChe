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

public partial class import_execl_import_loan_apply : System.Web.UI.Page
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

        List<LoanApply> LoanApplyAll = loanapplyService.GetAll();
        List<Borrower> borrowerAll = borrowerService.GetAll();
        Hashtable htPhone = new Hashtable();

        int j = 0;
        for (int i = 1; i <= ds.GetRecordCount(); i++)
        {
            string sFullName = ds.GetItemString(i, 0);
            string sCreditAmount = ds.GetItemString(i, 1);
            string sDeadline = ds.GetItemString(i, 2);
            string sPhone = ds.GetItemString(i, 3);
            string sDownPayments = ds.GetItemString(i, 4);
            string sYewuyuanName = ds.GetItemString(i, 5);
            string sIsHuanKuang = ds.GetItemString(i, 6);
            string sInterestDate = ds.GetItemString(i, 7);
            string sBusinessName = ds.GetItemString(i, 8);
            string sMonthlyPayment = ds.GetItemString(i, 9);
            string sAuditor = ds.GetItemString(i, 10);
            string sExpectedRepayment = ds.GetItemString(i, 11);
            List<LoanApply> loanApplyList = LoanApplyAll.FindAll(o => o.CreditPhone == sPhone && (o.RepaymentStatus == CreditStatus.还款中 || o.RepaymentStatus == CreditStatus.还款完成));
            Borrower salesmanId = borrowerAll.Find(o => o.FullName == sYewuyuanName.Trim() && o.IsSalesman == true);
            Borrower borrowerId = borrowerAll.Find(o => o.Phone == sPhone);
            if (salesmanId == null)
            {
                sOutputMessage += "第" + (i + 1) + "行," + sYewuyuanName + "业务员姓名查找不到<br/>";
                j++;
                continue;
            }

            else if (htPhone.Contains(sPhone))
            {
                sOutputMessage += "第" + (i + 1) + "行,电话号码" + sPhone + "在您的execl文档中出现了两次<br/>";
                j++;
                continue;
            }
            else if (loanApplyList.Count > 0)
            {
                sOutputMessage += "第" + (i + 1) + "行,:" + sPhone + "在数据库中已存在！<br/>";
                j++;
                continue;
            }
            else if (sPhone.Length != 11)
            {
                sOutputMessage += "第" + (i + 1) + "行,电话号码" + sPhone + "格式不正确<br/>";
                j++;
                continue;
            }
            else if (borrowerId == null)
            {
                sOutputMessage += "第" + (i + 1) + "行,电话号码" + sPhone + "找不到该客户会员信息<br/>";
                j++;
                continue;
            }
            LoanApply tbl = new LoanApply();
            tbl.BorrowerId = borrowerId.Id;
            tbl.SalesmanId = salesmanId.Id;
            tbl.MonthlyPayment = ConvertUtil.ToDecimal(sMonthlyPayment);
            tbl.BusinessName = sBusinessName;
            tbl.CreditPhone = sPhone;
            tbl.Deadline = ConvertUtil.ToInt(sDeadline);
            tbl.CreditAmount = ConvertUtil.ToDecimal(sCreditAmount) + ConvertUtil.ToDecimal(sDownPayments);
            tbl.TotalAmountStage = ConvertUtil.ToDecimal(sCreditAmount);
            tbl.DownPayments = ConvertUtil.ToDecimal(sDownPayments);
            tbl.AuditTime = Convert.ToDateTime(sInterestDate).AddDays(-1);
            tbl.Auditor = sAuditor;
            tbl.RepaymentStatus = sIsHuanKuang.Contains("结清") ? CreditStatus.还款完成 : CreditStatus.还款中;
            tbl.InterestDate = Convert.ToDateTime(sInterestDate).AddDays(-1);
            if (Convert.ToDateTime(sInterestDate).Date.AddDays(-1) < DateTime.Now.Date)
                tbl.IsLending = true;
            tbl.ExpectedRepayment = Convert.ToDateTime(sExpectedRepayment);
            if (sIsHuanKuang.Contains("结清"))
            {
                tbl.RepaymentPlanMode = RepaymentPlanMode.提前还款;
                tbl.ClosingDate = Convert.ToDateTime(sIsHuanKuang.Split('：')[1].ToString()).Date;
            }
            tbl.CustomerClassification = "A";
            tbl.BatchDate = DateTime.Now.AddDays(-1).Date;


            loanapplyService.Insert(tbl);


            htPhone.Add(sPhone, tbl);
        }
        lblErrMessage.Text = sOutputMessage;
        sOutputMessage += "总共" + ds.GetRecordCount() + "条数据,成功导入" + (ds.GetRecordCount() - j) + "条";
        lblErrMessage.Text = sOutputMessage;
        SbtAppLogger.LogAction("批量导入融资租赁信息", "导入" + ds.GetRecordCount() + "个融资租赁");
    }
}