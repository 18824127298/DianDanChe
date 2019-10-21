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
using Sigbit.Web.GCMap;

public partial class module_gcmap_gcmap_util_desc_input :GCMapPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //================== 1. 获取参数 ===================        
        string sCenterPiont = Request["point"];
        string sPiontType = Request["type"];
        string sInit = "23.1263,113.3683";

        if (sCenterPiont == null)
        {
            ViewState["init"] = sInit;
            return;
        }
        string[] sCenterPionts = sCenterPiont.Split(',');
        if (sCenterPionts.Length != 2)
        {
            ViewState["init"] = sInit;
            return;
        }

        ViewState["init"] = sCenterPiont;
                 
        //================== 2. 经纬度参数 =================== 
        Double fLutitude = ConvertUtil.ToFloat(sCenterPionts[0]);
        Double fLongitude = ConvertUtil.ToFloat(sCenterPionts[1]);
        Double fPrecision = 0.0003;  //精度

        if (sPiontType == "one")
        {                      
            ViewState["latlng"] = "getDescription (new GLatLng(" + sCenterPiont + "),0);";
            ViewState["count"] = 1; 
        }
        else
        {
            ViewState["latlng"] = GetPiontAll(fLutitude, fLongitude, fPrecision);
            ViewState["count"] = 17; 
        }       
        ViewState["center"] = sCenterPiont;//中心  
    }

    /// <summary>
    /// 获取17 个点的设置
    /// </summary>
    /// <param name="fLutitude">经度</param>
    /// <param name="fLongitude">纬度</param>
    /// <param name="fPrecision">精度</param>
    /// <returns></returns>
    private string GetPiontAll(Double fLutitude, Double fLongitude, Double fPrecision)
    {
        //================== 3. 范围参数 ===================
        //================== 3.1 一级范围的4个 =============
        string sPiontDown1 = GetPiontDown(fLutitude, fLongitude, fPrecision);
        string sPiontUp1 = GetPiontUp(fLutitude, fLongitude, fPrecision);
        string sPiontLeft1 = GetPiontLeft(fLutitude, fLongitude, fPrecision);
        string sPiontRight1 = GetPiontRight(fLutitude, fLongitude, fPrecision);

        //================== 3.2 二级范围的4个 =============
        string sPiontRightDown1 = GetPiontRightDown(fLutitude, fLongitude, fPrecision);
        string sPiontRightUp1 = GetPiontRightUp(fLutitude, fLongitude, fPrecision);
        string sPiontLeftDown1 = GetPiontLeftDown(fLutitude, fLongitude, fPrecision);
        string sPiontLeftUp1 = GetPiontLeftUp(fLutitude, fLongitude, fPrecision);

        //================== 3.3 三级范围的4个 =============
        fPrecision = fPrecision * 2;
        string sPiontDown2 = GetPiontDown(fLutitude, fLongitude, fPrecision);
        string sPiontUp2 = GetPiontUp(fLutitude, fLongitude, fPrecision);
        string sPiontLeft2 = GetPiontLeft(fLutitude, fLongitude, fPrecision);
        string sPiontRight2 = GetPiontRight(fLutitude, fLongitude, fPrecision);

        //================== 3.4 四级范围的4个 =============
        string sPiontRightDown2 = GetPiontRightDown(fLutitude, fLongitude, fPrecision);
        string sPiontRightUp2 = GetPiontRightUp(fLutitude, fLongitude, fPrecision);
        string sPiontLeftDown2 = GetPiontLeftDown(fLutitude, fLongitude, fPrecision);
        string sPiontLeftUp2 = GetPiontLeftUp(fLutitude, fLongitude, fPrecision);

        //================== 4. 参数组 ===================
        string sPiont = "getDescription (new GLatLng(" + fLutitude + "," + fLongitude + "),0);";
        sPiont += "\r\n\tgetDescription (new GLatLng(" + sPiontDown1 + "),1);";
        sPiont += "\r\n\tgetDescription (new GLatLng(" + sPiontUp1 + "),2);";
        sPiont += "\r\n\tgetDescription (new GLatLng(" + sPiontLeft1 + "),3);";
        sPiont += "\r\n\tgetDescription (new GLatLng(" + sPiontRight1 + "),4);";

        sPiont += "\r\n\tgetDescription (new GLatLng(" + sPiontRightDown1 + "),5);";
        sPiont += "\r\n\tgetDescription (new GLatLng(" + sPiontRightUp1 + "),6);";
        sPiont += "\r\n\tgetDescription (new GLatLng(" + sPiontLeftDown1 + "),7);";
        sPiont += "\r\n\tgetDescription (new GLatLng(" + sPiontLeftUp1 + "),8);";

        sPiont += "\r\n\tgetDescription (new GLatLng(" + sPiontDown2 + "),9);";
        sPiont += "\r\n\tgetDescription (new GLatLng(" + sPiontUp2 + "),10);";
        sPiont += "\r\n\tgetDescription (new GLatLng(" + sPiontLeft2 + "),11);";
        sPiont += "\r\n\tgetDescription (new GLatLng(" + sPiontRight2 + "),12);";

        sPiont += "\r\n\tgetDescription (new GLatLng(" + sPiontRightDown2 + "),13);";
        sPiont += "\r\n\tgetDescription (new GLatLng(" + sPiontRightUp2 + "),14);";
        sPiont += "\r\n\tgetDescription (new GLatLng(" + sPiontLeftDown2 + "),15);";
        sPiont += "\r\n\tgetDescription (new GLatLng(" + sPiontLeftUp2 + "),16);";
        return sPiont;
    }

    /// <summary>
    /// 获取下方点 纬度-精度
    /// </summary>                             
    private string GetPiontDown(Double fLutitude, Double fLongitude, Double fPrecision)
    {
        string sPiont = (fLutitude - fPrecision) + "," + fLongitude.ToString();
        return sPiont;
    }

    /// <summary>
    /// 获取上方点 纬度+精度
    /// </summary>                             
    private string GetPiontUp(Double fLutitude, Double fLongitude, Double fPrecision)
    {
        string sPiont = (fLutitude + fPrecision) + "," + fLongitude.ToString();
        return sPiont;
    }

    /// <summary>
    /// 获取左方点 经度-精度
    /// </summary>                             
    private string GetPiontLeft(Double fLutitude, Double fLongitude, Double fPrecision)
    {
        string sPiont = fLutitude + "," + (fLongitude - fPrecision);
        return sPiont;
    }

    /// <summary>
    /// 获取右方点 经度+精度
    /// </summary>                             
    private string GetPiontRight(Double fLutitude, Double fLongitude, Double fPrecision)
    {
        string sPiont = fLutitude + "," + (fLongitude + fPrecision);
        return sPiont;
    }


    /// <summary>
    /// 获取右下方点 经度+精度，纬度-精度
    /// </summary>                             
    private string GetPiontRightDown(Double fLutitude, Double fLongitude, Double fPrecision)
    {
        string sPiont = (fLutitude - fPrecision) + "," + (fLongitude + fPrecision);
        return sPiont;
    }


    /// <summary>
    /// 获取右上方点 经度+精度，纬度+精度
    /// </summary>                             
    private string GetPiontRightUp(Double fLutitude, Double fLongitude, Double fPrecision)
    {
        string sPiont = (fLutitude + fPrecision) + "," + (fLongitude + fPrecision);
        return sPiont;
    }


    /// <summary>
    /// 获取左下方点 经度-精度，纬度-精度
    /// </summary>                             
    private string GetPiontLeftDown(Double fLutitude, Double fLongitude, Double fPrecision)
    {
        string sPiont = (fLutitude - fPrecision) + "," + (fLongitude - fPrecision);
        return sPiont;
    }

    /// <summary>
    /// 获取左上方点 经度-精度，纬度+精度
    /// </summary>                             
    private string GetPiontLeftUp(Double fLutitude, Double fLongitude, Double fPrecision)
    {
        string sPiont = (fLutitude + fPrecision) + "," + (fLongitude - fPrecision);
        return sPiont;
    }
}
