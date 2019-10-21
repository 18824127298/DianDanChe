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

public partial class cdb_carillegal_cdb_carillegal_carillegal_create : SbtPageBase
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
                    Response.Write(Save(Request.QueryString["BorrowPhone"], Request.QueryString["LicensePlate"], Request.QueryString["IllegalTitle"],
                        Request.QueryString["IllegalDescribe"], Request.QueryString["IllegalAddress"], Request.QueryString["IllegalTime"],
                        Request.QueryString["FinePrice"], Request.QueryString["AroundFee"], Request.QueryString["Points"]));
                    break;
                case "Get":
                    Response.Write(GetCar(Request.QueryString["rec_key"]));
                    break;
                case "FindName":
                    Response.Write(FindName(Request.QueryString["Phone"]));
                    break;
            }
            Response.End();
        }


        //========== 1. 初始化界面 ==========
        //========== 2. 取出传入条件 ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        PageParameter.SetCustomParamString("rec_key", sRecordPrimaryKey);

        //========== 3. 新增的判断和处理 ==========
        if (sRecordPrimaryKey == "")
        {
            lblTitle.Text = "上传标的文件";
            return;
        }
    }

    public string FindName(string sPhone)
    {
        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.Search(new Borrower() { IsValid = 1 }).Find(o => o.Phone == EncryptionService.AESEncrypt(sPhone));
        if (borrower != null)
        {
            return EncryptionService.AESDecrypt(borrower.FullName);
        }
        else
        {
            return "找不到此用户";
        }
    }

    public string Save(string BorrowPhone, string LicensePlate, string IllegalTitle, string IllegalDescribe, string IllegalAddress, string IllegalTime,
        string FinePrice, string AroundFee, string Points)
    {
        bool bAppendMode = false;
        string sParamRecKey = PageParameter.GetCustomParamString("rec_key");
        if (sParamRecKey == "")
            bAppendMode = true;

        //========== 1. 验证数据的有效性 ==========
        //========== 1.1 判断文件名称是否为空 ==========

        CarIllegal carIllegal = new CarIllegal();
        CarIllegalService carIllegalService = new CarIllegalService();
        if (sParamRecKey != "")
        {
            carIllegal = carIllegalService.GetById(ConvertUtil.ToInt(sParamRecKey));
        }
        bool bResult = false;
        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.Search(new Borrower() { IsValid = 1 }).Find(o => o.Phone == EncryptionService.AESEncrypt(BorrowPhone));
        if (borrower != null)
        {
            carIllegal.BorrowerId = borrower.Id;
            if (!string.IsNullOrEmpty(LicensePlate))
                carIllegal.LicensePlate = LicensePlate;
            if (!string.IsNullOrEmpty(IllegalTitle))
                carIllegal.IllegalTitle = IllegalTitle;
            if (!string.IsNullOrEmpty(IllegalDescribe))
                carIllegal.IllegalDescribe = IllegalDescribe;
            if (!string.IsNullOrEmpty(IllegalAddress))
                carIllegal.IllegalAddress = IllegalAddress;
            if (!string.IsNullOrEmpty(IllegalTime))
                carIllegal.IllegalTime = DateTimeUtil.ToDateTime(IllegalTime);
            if (!string.IsNullOrEmpty(FinePrice))
                carIllegal.FinePrice = ConvertUtil.ToDecimal(FinePrice);
            if (!string.IsNullOrEmpty(AroundFee))
                carIllegal.AroundFee = ConvertUtil.ToDecimal(AroundFee);
            if (!string.IsNullOrEmpty(Points))
                carIllegal.Points = ConvertUtil.ToInt(Points);

            CarService carService = new CarService();
            Car car = carService.Search(new Car() { IsValid = 1 }).Find(o => o.BorrowerId == borrower.Id);
            carIllegal.CarId = car.Id;

            carIllegal.JudgeId = ConvertUtil.ToInt(CurrentUser.UserUid);
            //========== 2. 数据新增处理 ==========
            if (bAppendMode)
            {
                carIllegalService.Insert(carIllegal);
            }
            //========== 3. 数据更新处理 ==========
            else
            {
                carIllegalService.Update(carIllegal);
            }
            bResult = true;

        }
        if (bResult)
            return "车辆违章记录录入成功";
        else
            return "车辆违章记录录入失败";
    }

    public string GetCar(string sRecKey)
    {
        Car car = new Car();
        CarService carService = new CarService();
        car = carService.GetById(ConvertUtil.ToInt(sRecKey));
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStr = js.Serialize(car);
        return jsonStr;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("credit_go.aspx?page=file");
    }



}
