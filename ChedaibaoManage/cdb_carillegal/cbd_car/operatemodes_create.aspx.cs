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

public partial class cdb_carillegal_cbd_car_operatemodes_create : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        PageParameter.SetCustomParamString("rec_key", sRecordPrimaryKey);


        OperateModel operateModel = new OperateModel();
        OperateModelService operateModelService = new OperateModelService();
        operateModel = operateModelService.GetById(ConvertUtil.ToInt(sRecordPrimaryKey));

        edtName.Text = operateModel.Name;

    }
    protected void btn_sure_Click(object sender, EventArgs e)
    {
        bool bAppendMode = false;
        string sParamRecKey = PageParameter.GetCustomParamString("rec_key");
        if (sParamRecKey == "")
            bAppendMode = true;

        OperateModelService operateModelService = new OperateModelService();
        OperateModel operateModel = new OperateModel();
        if (bAppendMode)
        {
            operateModel.Name = edtName.Text;
            operateModelService.Insert(operateModel);
        }
        else
        {
            operateModel = operateModelService.GetById(ConvertUtil.ToInt(sParamRecKey));
            operateModel.Name = edtName.Text;
            operateModelService.Update(operateModel);
        }
        Response.Redirect("operatemodes_list.aspx");
    }
}