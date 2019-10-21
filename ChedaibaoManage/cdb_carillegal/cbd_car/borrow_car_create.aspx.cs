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

public partial class cdb_carillegal_cbd_car_borrow_car_create : SbtPageBase
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
                    Response.Write(Save(Request.QueryString["Hpic"], Request.QueryString["BorrowPhone"], Request.QueryString["CarSystem"], Request.QueryString["CarNumber"], Request.QueryString["EngineNumber"], Request.QueryString["BodyRackNumber"], Request.QueryString["Remark"]));
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

    public string Save(string sHpic, string BorrowPhone, string CarSystem, string CarNumber, string EngineNumber, string BodyRackNumber, string Remark)
    {
        bool bAppendMode = false;
        string sParamRecKey = PageParameter.GetCustomParamString("rec_key");
        if (sParamRecKey == "")
            bAppendMode = true;

        //========== 1. 验证数据的有效性 ==========
        //========== 1.1 判断文件名称是否为空 ==========

        Car car = new Car();
        CarService carService = new CarService();
        if (sParamRecKey != "")
        {
            car = carService.GetById(ConvertUtil.ToInt(sParamRecKey));
        }
        bool bResult = false;
        if (sHpic == "")
        {
            return "上传文件不能为空";
        }
        if (sHpic != "")
        {
            BorrowerService borrowerService = new BorrowerService();
            Borrower borrower = borrowerService.Search(new Borrower() { IsValid = 1 }).Find(o => o.Phone == EncryptionService.AESEncrypt(BorrowPhone));
            if (borrower != null)
            {
                car.ImageUrl = sHpic.Split('|')[0].ToString();
                car.BorrowerId = borrower.Id;
                if (!string.IsNullOrEmpty(CarSystem))
                    car.CarSystem = CarSystem;
                if (!string.IsNullOrEmpty(CarNumber))
                    car.CarNumber = CarNumber;
                if (!string.IsNullOrEmpty(EngineNumber))
                    car.EngineNumber = EngineNumber;
                if (!string.IsNullOrEmpty(BodyRackNumber))
                    car.BodyRackNumber = BodyRackNumber;
                if (!string.IsNullOrEmpty(Remark))
                    car.Remark = Remark;
                //========== 2. 数据新增处理 ==========
                if (bAppendMode)
                {
                    carService.Insert(car);
                }
                //========== 3. 数据更新处理 ==========
                else
                {
                    carService.Update(car);
                }
                bResult = true;
            }
        }
        if (bResult)
            return "车型录入成功";
        else
            return "车型录入失败";
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


}
