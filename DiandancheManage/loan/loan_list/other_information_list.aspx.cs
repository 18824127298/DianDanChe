using CheDaiBaoCommonService.Service;
using CheDaiBaoWeChatModel;
using Sigbit.Common;
using Sigbit.Data;
using Sigbit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class loan_loan_list_other_information_list : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //========== 0. 绑定DataGrid和PageControl的关系 ===========
        gridViewPager.GridViewControl = gvList;
        gridViewPager.PageIndexChanged
            = new module_WebUserControl.PageIndexChangedHandler(gvList__PageIndexChanged);
        if (IsPostBack)
            return;

        int nId = ConvertUtil.ToInt(Request["id"]);

        if (nId != 0)
        {
            PageParameter.SetCustomParamObject("id", nId);
        }
        //========= 1. 如果首次进来，则取出前次的状态 ===========
        if (!IsPostBack)
        {
            CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                       = @"select * from BusinessFile where IsValid = 1 and Sort = 2 and BusinessType=" + (int)BusinessType.客户的信息照 + " and RelationId=" + nId;

            if (CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql == "")
            {
                CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField("CreateTime");
            }
            else
            {
                gvList.PageSize = CurrentPageStatus.DataViewStatus.PageSize;
                gvList.PageIndex = CurrentPageStatus.DataViewStatus.CurrentPageIndex;
            }

            CurrentPageStatus.DataViewStatus.SqlBuilder.ClearFixConditions();

            FetchDataFromDB();
            gridViewPager.ShowPageInfo();

            NaviTabController.ShowTabBar();
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

    }

    protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField(e.SortExpression);
        gridViewPager.RefreshGridView();
    }


    int i = 0;
    public string VIVFilePath(object oFilePath, object oWeChatPath, object oCreateTime)
    {
        i++;
        if (!string.IsNullOrEmpty(oFilePath.ToString()))
        {
            if (oFilePath.ToString().Contains("CheDaiBaoWeChat"))
            {
                return @"<img src='http://ddc.che01.com/PictureUpload/image/" + oFilePath.ToString().Substring(oFilePath.ToString().Length - 34, 34).Replace("\\", "/") + "' style='width:500px;height:500px;margin-top:5px;margin-bottom:5px;' id='img" + i + @"' />
                <input name='button_test' type='button' value='旋转该图片' onclick='fn_xuanzhuan(" + i + ")'/>";
            }
            else
            {
                return @"<img src='http://manage.che01.com/image/" + oFilePath.ToString().Substring(oFilePath.ToString().Length - 37, 37).Replace("\\", "/") + "' style='width:500px;height:500px;margin-top:5px;margin-bottom:5px;' id='img" + i + @"' />
                <input name='button_test' type='button' value='旋转该图片' onclick='fn_xuanzhuan(" + i + ")'/>";
            }
        }
        else
        {
            return @"<img src='" + VIVPHOTO(oWeChatPath.ToString()) + "' style='width:500px;height:500px;margin-top:5px;margin-bottom:5px;' id='img" + i + @"' />
                <input name='button_test' type='button' value='旋转该图片' onclick='fn_xuanzhuan(" + i + ")'/>";
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

    public string VIVPHOTO(string spath)
    {
        int i = spath.IndexOf("=");
        int j = spath.IndexOf("&");
        return spath.Replace((spath.Substring(i + 1)).Substring(0, j - i - 1), WeChatBaseRequestService.getApptoken());
    }
}