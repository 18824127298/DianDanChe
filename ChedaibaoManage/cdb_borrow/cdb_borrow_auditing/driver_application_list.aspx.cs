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
using Sigbit.App.PTP.DBDefine.WangDai;
using Sigbit.App.PTP.Web;
using System.Collections.Generic;
using Sigbit.Web.MediaServer;
using CheDaiBaoWeChatService.Service;
using CheDaiBaoWeChatModel.Models;

public partial class cdb_borrow_cdb_borrow_auditing_driver_application_list : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack)
            return;

        //========= 1. 如果首次进来，则取出前次的状态 ===========
        if (!IsPostBack)
        {
            if (CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql == "")
            {
                CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                        = @"select d.*, b.IsIdNumber from DriverApplication d join Borrower b on d.BorrowerId = b.Id where d.IsValid = 1 and b.IsValid = 1";

            }
            else
            {
                gvList.PageSize = CurrentPageStatus.DataViewStatus.PageSize;
                gvList.PageIndex = CurrentPageStatus.DataViewStatus.CurrentPageIndex;
            }
            FetchDataFromDB();
            gridViewPager.ShowPageInfo();
        }
    }

    private void gvList__PageIndexChanged(int nNewPageIndex)
    {
        gvList.PageIndex = nNewPageIndex;
        FetchDataFromDB();
    }

    private void FetchDataFromDB()
    {
        //========= 1. 取出数据，并绑定 ========
        DataSet ds = DataHelper.Instance.ExecuteDataSet
                (CurrentPageStatus.DataViewStatus.SqlBuilder.ToString());
        gvList.DataSource = ds;
        gvList.DataBind();

        //========== 2. 保存当前状态 ==============
        CurrentPageStatus.DataViewStatus.PageSize = gvList.PageSize;
        CurrentPageStatus.DataViewStatus.CurrentPageIndex = gvList.PageIndex;

        //============ 3. 搜索条件的描述 ==========
        SQLBuilder currentSQLBuilder = CurrentPageStatus.DataViewStatus.SqlBuilder;
        if (currentSQLBuilder.GetConditionCount() != 0)
        {
            divSearchCondition.Visible = true;
            lblConditionDesc.Text = currentSQLBuilder.GetConditionDescription();
        }
        else
            divSearchCondition.Visible = false;
    }

    protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField(e.SortExpression);
        gridViewPager.RefreshGridView();
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.ClearConditions();
        string sCreditPhone = edtCreditPhone.Text.Trim();
        if (sCreditPhone != "")
        {
            CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition("Phone", "手机号",
                     sCreditPhone,
                     SQLConditionOperator.Like);
        }
        string sCreditName = edtCreditName.Text.Trim();
        if (sCreditName != "")
        {
            CurrentPageStatus.DataViewStatus.SqlBuilder.AddCondition("FullName", "姓名",
                    sCreditName,
                    SQLConditionOperator.Like);
        }
        FetchDataFromDB();
        gridViewPager.ShowPageInfo();
    }

    protected void btnClearCondition_Click(object sender, EventArgs e)
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.ClearConditions();
        gridViewPager.RefreshGridView();
    }


    protected string VIVaduit(object oIsaduit, int nId, string sAuditor)
    {
        int bIsaduit = ConvertUtil.ToInt(oIsaduit);
        if (bIsaduit == 0 && sAuditor == "")
        {
            return string.Format("<a onclick=\"window.open('{0}/cdb_borrow/cdb_borrow_auditing/driver_application_audit.aspx?id={1}')\"><image src='../../images/menu_icon/erp.gif' title='审核申请借款标的' /></a>", "http://sigbitoldix.phonelee.com", nId);
        }
        else
            return bIsaduit == 1 ? "已通过" : "未通过";
    }


    protected string VIVAESDecrypt(object obj)
    {
        return EncryptionService.AESDecrypt(obj.ToString());
    }

    protected string VIVIsIdNumber(object IsIdNumber)
    {
        return ConvertUtil.ToInt(IsIdNumber) == 1 ? "已实名" : "未实名";
    }
}



