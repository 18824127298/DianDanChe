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
using Sigbit.Web.GCMap;

public partial class module_gcmap_gcmap_show_track : GCMapPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //================ 1.0 首次返回 ================
        if (IsPostBack)
            return;
     
        //================ 2.0 设置地图显示参数 ========           
        Bind_View();
    }

    /// <summary>
    /// 视图绑定
    /// </summary>
    public void Bind_View()
    {
        //================ 1.0 设置地图显示参数 ========        
        double dLat = 0.0;
        double dLng = 0.0;
        int nScale = 17;


        GCMapShowParameter param = (GCMapShowParameter)Session["mapShowLineParamMPYN"];

        if (param != null)
        {
            if (param.MapStyle == null)
            {
                //param.MapStyle = new GCMapStyle();
                //param.MapStyle.CenterPoint = param.GetCenterPoint();
                //param.MapStyle.Scale = 17;
                param.MapStyle = GCMapUtil.GetAutoMapStyle(param, 512, 512);
            }

            dLat = param.MapStyle.CenterPoint.Latitude;
            dLng = param.MapStyle.CenterPoint.Longitude;
            nScale = param.MapStyle.Scale;

            ViewState["linePoint"] = param.ToLinePointString();
            ViewState["lineMarker"] = param.ToLineMarkerString();
        }

        ViewState["Lat"] = dLat;
        ViewState["Lng"] = dLng;
        ViewState["Scale"] = nScale;


    }

}
