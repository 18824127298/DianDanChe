using CheDaiBaoCommonService.Service;
using CheDaiBaoWeChatController.Interface;
using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Interface;
using CheDaiBaoWeChatService.Service;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using Sigbit.Common;
using Sigbit.Data;
using Sigbit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class salesman_borrower_list_borrower_contacts_list : SbtPageBase
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
        LoanApplyService loanapplyService = new LoanApplyService();
        LoanApply loanapply = loanapplyService.GetById(nId);

        CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                        = @"select * from Contacts where IsValid = 1 and BorrowerId =" + loanapply.BorrowerId + " and LoanapplyId=" + loanapply.Id;
        FetchDataFromDBContacts();

    }



    private void FetchDataFromDBContacts()
    {
        //========= 1. 取出数据，并绑定 ========
        DataSet ds = DataHelper.Instance.ExecuteDataSet
                (CurrentPageStatus.DataViewStatus.SqlBuilder.ToString());


        ContactsList.DataSource = ds;
        ContactsList.DataBind();

    }

    protected string VIVHTime(object oTime)
    {

        if (string.IsNullOrEmpty(oTime.ToString()))
        {
            return "";
        }
        else
        {
            return Convert.ToDateTime(oTime).ToString("yyyy-MM-dd HH:mm:ss");
        }
    }


    protected string VIVIsKnowStages(object oIsKnowStages)
    {

        if (string.IsNullOrEmpty(oIsKnowStages.ToString()))
        {
            return "";
        }
        else
        {
            return ConvertUtil.ToBool(oIsKnowStages) == true ? "是" : "否";
        }
    }
}