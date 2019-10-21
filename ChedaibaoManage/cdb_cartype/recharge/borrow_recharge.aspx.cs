using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;


using Sigbit.Common;
using Sigbit.Data;
using Sigbit.Framework;
using Sigbit.Web;
using Sigbit.Web.WebControlUtil;
using Sigbit.App.PTP.DBDefine.WangDai;
using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatService.Service;
using CheDaiBaoWeChatModel.Models;
using System.Web.Script.Serialization;


public partial class cdb_cartype_recharge_borrow_recharge : SbtPageBase //System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        //========== 1. 初始化界面 ==========
        if (IsPostBack)
            return;

        if (Request.QueryString["Action"] != null)
        {
            PageParameter.SetCustomParamString("BorrowerId", "0");
            Response.ContentType = "text/plain";
            string Action = Request.QueryString["Action"];
            switch (Action)
            {
                case "FindName":
                    Response.Write(FindName(Request.QueryString["Phone"]));
                    break;
            }
            Response.End();

            return;

        }
    }

    public string FindName(string sPhone)
    {
        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.Search(new Borrower() { IsValid = 1 }).Find(o => o.Phone == EncryptionService.AESEncrypt(sPhone));
        if (borrower != null)
        {
            PageParameter.SetCustomParamString("BorrowerId", borrower.Id.ToString());
            BankCardService bankCardService = new BankCardService();
            BankCard bankCard = bankCardService.Search(new BankCard() { IsValid = 1 }).Find(o => o.BorrowerId == ConvertUtil.ToInt(PageParameter.GetCustomParamString("BorrowerId")));

            if (bankCard != null)
            {
                borrower.BankCard = bankCard.Name + "，" + bankCard.Subbranch + "，" + bankCard.Number;
                //borrower.BankCardId = bankCard.Id;
                borrower.FullName = EncryptionService.AESDecrypt(borrower.FullName);
            } JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonStr = js.Serialize(borrower);
            return jsonStr;
        }
        else
        {
            return "找不到此用户";
        }
    }



    protected void btnOK_Click(object sender, EventArgs e)
    {


        if (edtAmount.Text.Trim() == "")
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "金额不能为空！";
            edtAmount.Focus();
            return;
        }

        string sRechageTime = dpRechargeTime.DateTimeString;

        if (sRechageTime == "")
        {
            dpRechargeTime.Focus();
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "到账日期不能为空";
            return;
        }

        if (edtRemark.Text == "")
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "备注不能为空！";
            edtRemark.Focus();
            return;
        }


        decimal fInputMoney = ConvertUtil.ToDecimal(edtAmount.Text.Trim());

        if (fInputMoney <= 0)
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "请输入大于0的金额！";
            edtAmount.Focus();
            return;
        }

        //BankCardService bankcardService = new BankCardService();
        //BankCard tblBankCard = new BankCard();
        //tblBankCard.Id = bankcardService.GetById(ConvertUtil.ToInt(this.LblBankCardId.Text)).Id;



        // 数据操作
        RechargeService rechargeService = new RechargeService();


        Recharge tbl = new Recharge();
        tbl.BorrowerId = Convert.ToInt32(PageParameter.GetCustomParamString("BorrowerId"));
        tbl.IsValid = 1;
        tbl.Amount = fInputMoney;
        //tbl.ActualRechargeFee = ConvertUtil.ToFloat(edtActualRechargeFee.Text.Trim());
        //tbl.Remark = edtRemark.Text.Trim();
        tbl.IsAudit = 0;
        tbl.RechargeMode = RechargeMode.后台充值;
        tbl.RechargeTime = sRechageTime;
        tbl.BankCardName = edtBankCard.Text.Split('，')[0];
        tbl.BankCardNumber = edtBankCard.Text.Split('，')[2];
        tbl.BankCardSubbranch = edtBankCard.Text.Split('，')[1];
        tbl.OrderNumber = edtOrderNumber.Text.Trim();
        tbl.Creator = CurrentUser.RealName;
        tbl.RechargeTime = DateTimeUtil.Now;
        tbl.RechargeRemark = edtRemark.Text.Trim();

        int nNewRechargeID = rechargeService.Insert(tbl);

        // 获取会员信息
        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetById(Convert.ToInt32(PageParameter.GetCustomParamString("BorrowerId")));

        // 记录操作日志
        TbOpera tblOpera = new TbOpera();
        tblOpera.IsValid = 1;
        tblOpera.GodId = Convert.ToInt32(CurrentUser.RecordData.ThirdPartyCode);
        tblOpera.OperaType = (int)OperaType.后台添加充值数据;
        tblOpera.RelationId = nNewRechargeID;
        tblOpera.Remark = string.Format("后台线下充值，充值会员：{0}，充值金额：{1}，操作人ID：{2}",
            borrower.Aliases, fInputMoney, tblOpera.GodId);
        tblOpera.ClientAddress = Request.UserHostAddress;
        tblOpera.Insert();
        //tbl.ActualRechargeFee = 0m;

        NaviTabController.ClearBar();


        SbtAppLogger.LogAction("后台线下充值", string.Format("给用户{0}后台线下充值,充值金额{1}元,备注:{2};",
            borrower.Aliases, fInputMoney, edtRemark.Text.Trim()));

        SbtMessageBoxPage msgPage = new SbtMessageBoxPage(this);
        msgPage.MessageText = "后台充值成功，充值金额:" + tbl.Amount + "元";
        msgPage.ReturnUrl = this.Request.Url.AbsoluteUri;

        msgPage.Show();

        //Response.Redirect("recharge_go.aspx");
    }

}
