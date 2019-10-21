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

public partial class module_gcmap_gcmap_show_point : GCMapPageBase
{
    protected double _currentLat = 0.0;
    protected double _currentLng = 0.0;
    protected int _nScale = 0;
    protected string _sTitle = "";

    protected void Page_Load(object sender, EventArgs e)
    {     
        //================ 1.0 首次返回 ================
        if (IsPostBack)
            return;


        //================ 2.0 设置地图显示参数 ================
        GCMapShowParameter param = (GCMapShowParameter)Session["mapShowPointParamMPYN"];

        if (param != null)
        {
            _currentLat = param.CurrentPoint.Latitude;
            _currentLng = param.CurrentPoint.Longitude;
            //if (param.MapStyle == null)
            //{
            //    param.MapStyle = new GCMapStyle();
            //    param.MapStyle.Scale = 17;
            //}
            _nScale = param.MapStyle.Scale;
            _sTitle = param.CurrentPoint.PointDesc;
        }

    }

   

}
