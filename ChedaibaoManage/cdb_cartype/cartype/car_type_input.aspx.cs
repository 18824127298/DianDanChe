using System;
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

using System.IO;

using Sigbit.Common;
using Sigbit.Data;
using Sigbit.Framework;
using Sigbit.Web.MediaServer;
using Sigbit.Web.WebControlUtil;
using Sigbit.App.PTP.DBDefine.WangDai;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;

using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Collections.Generic;

public partial class cdb_cartype_cartype_car_type_input : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack)
            return;

        if (Request.QueryString["Action"] != null)
        {
            Response.ContentType = "text/plain";
            string Action = Request.QueryString["Action"];
            switch (Action)
            {
                case "Save":
                    Response.Write(Save(Request.QueryString["Hpic"], Request.QueryString["CarName"], Request.QueryString["CarModels"], Request.QueryString["LiftFares"], Request.QueryString["Retainage"], Request.QueryString["MonthPrice"], Request.QueryString["CarTitle"], Request.QueryString["Stages"]));
                    break;
                case "Get":
                    Response.Write(GetCarType(Request.QueryString["rec_key"]));
                    break;
                case "GetCarModels":
                    Response.Write(GetCarModels());
                    break;

            }
            Response.End();
        }

        //========== 0. 删除记录 ==========
        string sDelRecords = ConvertUtil.ToString(Request["del_rec"]);
        if (sDelRecords != "")
        {
            DeleteData(sDelRecords);
            Response.Redirect("credit_go.aspx?page=file");
            return;
        }

        //========== 1. 初始化界面 ==========
        //========== 2. 取出传入条件 ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        PageParameter.SetCustomParamString("rec_key", sRecordPrimaryKey);

        //========== 3. 新增的判断和处理 ==========
        if (sRecordPrimaryKey == "")
        {
            lblTitle.Text = "上传标的文件";
            HidMulti.Value = "true";
            return;
        }

        //========== 4. 取数据 ==========
        CarType cartype = new CarType();
        CarTypeService cartypeService = new CarTypeService();
        cartype = cartypeService.GetById(ConvertUtil.ToInt(sRecordPrimaryKey));

        //========== 5. 更新各控件的显示 ==========

        Hpic.Value = cartype.ImageUrl;
    }

    public string Save(string sHpic, string sCarName, string sCarModels, string sLiftFares, string sRetainage, string sMonthPrice, string sCarTitle, string sStages)
    {
        bool bAppendMode = false;
        string sParamRecKey = PageParameter.GetCustomParamString("rec_key");
        if (sParamRecKey == "")
            bAppendMode = true;

        //========== 1. 验证数据的有效性 ==========
        //========== 1.1 判断文件名称是否为空 ==========

        CarType cartype = new CarType();
        CarTypeService cartypeService = new CarTypeService();
        if (sParamRecKey != "")
        {
            cartype = cartypeService.GetById(ConvertUtil.ToInt(sParamRecKey));
        }
        bool bResult = false;
        if (sHpic == "")
        {
            return "上传文件不能为空";
        }
        if (sHpic != "")
        {
            cartype.ImageUrl = sHpic.Split('|')[0].ToString();
            if (!string.IsNullOrEmpty(sLiftFares))
                cartype.LiftFares = ConvertUtil.ToDecimal(sLiftFares);
            if (!string.IsNullOrEmpty(sCarName))
                cartype.CarName = sCarName;
            if (!string.IsNullOrEmpty(sCarTitle))
                cartype.CarTitle = sCarTitle;
            if (!string.IsNullOrEmpty(sCarModels))
                cartype.OperateModelId = ConvertUtil.ToInt(sCarModels);
            if (!string.IsNullOrEmpty(sRetainage))
                cartype.Retainage = ConvertUtil.ToDecimal(sRetainage);
            if (!string.IsNullOrEmpty(sMonthPrice))
                cartype.MonthPrice = ConvertUtil.ToDecimal(sMonthPrice);
            if (!string.IsNullOrEmpty(sStages))
                cartype.Stages = ConvertUtil.ToInt(sStages);
            //========== 2. 数据新增处理 ==========
            if (bAppendMode)
            {
                cartypeService.Insert(cartype);
            }
            //========== 3. 数据更新处理 ==========
            else
            {
                cartypeService.Update(cartype);
            }
            bResult = true;

        }
        if (bResult)
            return "车型录入成功";
        else
            return "车型录入失败";
    }

    public string GetCarType(string sRecKey)
    {
        CarType cartype = new CarType();
        CarTypeService cartypeService = new CarTypeService();
        cartype = cartypeService.GetById(ConvertUtil.ToInt(sRecKey));
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStr = js.Serialize(cartype);
        return jsonStr;
    }

    public string GetCarModels()
    {
        OperateModelService operateModelService = new OperateModelService();
        List<OperateModel> operateModelList = operateModelService.GetAll();
        string JsonString = string.Empty;
        JsonString = JsonConvert.SerializeObject(operateModelList);
        return JsonString;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("credit_go.aspx?page=file");
    }

    private void DeleteData(string sDelRecords)
    {
        string[] arrsSelectedIDs = sDelRecords.Split(',');
        for (int i = 0; i < arrsSelectedIDs.Length; i++)
        {
            string sSelectedID = arrsSelectedIDs[i];

            TbBusinessFile tbl = new TbBusinessFile();
            tbl.Id = ConvertUtil.ToInt(sSelectedID);
            tbl.Fetch();

            MediaServerPath msCurrent = new MediaServerPath();
            msCurrent.RelativeUrl = tbl.FilePath;

            try
            {
                File.Delete(msCurrent.FullPath);
            }
            catch (Exception ex)
            {
                DebugLogger.LogDebugMessage("删除文件失败:" + msCurrent.FullPath + "," + ex.Message);
            }

            tbl.Delete();

            SbtAppLogger.LogAction("删除咨信文件", string.Format("{0},{1},关联标的:{2}", tbl.FileName, tbl.FilePath, tbl.RelationId));
        }

    }


}
