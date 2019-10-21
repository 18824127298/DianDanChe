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
using Sigbit.Common;

public partial class module_map_html_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string sLatitude = edtLatitude.Text.Trim();
        string sLongitude = edtLongitude.Text.Trim();

        GCMapShow show = GCMapShow.Instance;
        GCMapShowParameter para = new GCMapShowParameter();
        GCMapPoint point = new GCMapPoint();
        point.Latitude = ConvertUtil.ToFloat(sLatitude, -1);
        point.Longitude = ConvertUtil.ToFloat(sLongitude, -1);

        para.ShowMode = GCMapShowMode.ShowPoint;
        para.CurrentPoint = point;
        para.MapStyle = new GCMapStyle();
        para.MapStyle.CenterPoint = point;
        para.MapStyle.Scale = 17;

        show.OpenMapWindow(para);

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string sLatitude = edtLatitude.Text.Trim();
        string sLongitude = edtLongitude.Text.Trim();

        GCMapShow show = GCMapShow.Instance;
        GCMapShowParameter para = new GCMapShowParameter();
        GCMapPoint point = new GCMapPoint();
        point.Latitude = ConvertUtil.ToFloat(sLatitude, -1);
        point.Longitude = ConvertUtil.ToFloat(sLongitude, -1);

        GCMapStyle style = new GCMapStyle();
        style.Scale = 17;

        for (int i = 0; i < 6; i++)
        {
            GCMapPoint point1 = new GCMapPoint();
            point1.Latitude = point.Latitude + 0.001 * i;
            point1.Longitude = point.Longitude;
            para.AddTrackPoint(point1);
        }

        para.ShowMode = GCMapShowMode.ShowTrack;
        para.CurrentPoint = para.GetCenterPoint();
        style.CenterPoint = para.GetCenterPoint();
        para.MapStyle = style;

        show.OpenMapWindow(para);
    }


    protected void Button3_Click(object sender, EventArgs e)
    {
        GCMapPoint pointA = new GCMapPoint();
        pointA.Latitude = 23.126300;
        pointA.Longitude = 113.368300;

        GCMapPoint pointB = new GCMapPoint();
        pointB.Latitude = 23.128300;
        pointB.Longitude = 113.370300;

        GCFenceDefine define = new GCFenceDefine();
        define.FenceName = "系统定义";
        define.PointA = pointA;
        define.PointB = pointB;

        GCMapStyle style = new GCMapStyle();
        style.Scale = 16;
        style.CenterPoint = define.GetCenterPoint();

        GCFenceParameter para = new GCFenceParameter();
        para.MapStyle = style;
        para.FenceManageMode = GCFenceManageMode.ShowFence;
        para.CurrentFence = define;

        GCMapShow show = new GCMapShow();
        show.OpenMapWindow(para);
    }


    protected void Button4_Click(object sender, EventArgs e)
    {
        string sLatitude = edtLatitude.Text.Trim();
        string sLongitude = edtLongitude.Text.Trim();

        string sLatitudeB = edtLatitudeB.Text.Trim();
        string sLongitudeB = edtLongitudeB.Text.Trim();

        GCMapPoint pointA = new GCMapPoint();
        pointA.Latitude = ConvertUtil.ToFloat(sLatitude, -1);
        pointA.Longitude = ConvertUtil.ToFloat(sLongitude, -1);

        GCMapPoint pointB = new GCMapPoint();
        pointB.Latitude = ConvertUtil.ToFloat(sLatitudeB, -1);
        pointB.Longitude = ConvertUtil.ToFloat(sLongitudeB, -1);

        // = [ (A点经度 - B点经度)^2 + (A点纬度 - B点纬度)^2 ] ^ (1/2) 
        // = [ (11695400 - 11695300)^2  + (3995400 - 3995300)^2 ] ^(1/2) 
        // = (10000+10000) ^ (1/2) =141米

        int LatitudeA = (int)(pointA.Latitude * 100000);
        int LongitudeA = (int)(pointA.Longitude * 100000);

        int LatitudeB = (int)(pointB.Latitude * 100000);
        int LongitudeB = (int)(pointB.Longitude * 100000);

        int nDistance = ((LatitudeA - LatitudeB) ^ 2 + (LongitudeA - LongitudeB) ^ 2) ^ (1 / 2);
        Response.Write("距离为：" + nDistance);

        GCMapShow show = GCMapShow.Instance;
        GCMapShowParameter para = new GCMapShowParameter();

        GCMapStyle style = new GCMapStyle();
        style.Scale = GetScale(nDistance, 600, 450);
        Response.Write("style.Scale= " + style.Scale); 

        para.AddTrackPoint(pointA);
        para.AddTrackPoint(pointB);

        para.ShowMode = GCMapShowMode.ShowTrack;
        para.CurrentPoint = para.GetCenterPoint();
        style.CenterPoint = para.GetCenterPoint();
        para.MapStyle = style;

        show.OpenMapWindow(para);
    }

    private int GetDistance(GCMapPoint pointA, GCMapPoint pointB)
    { 
        //================ 2.0 计算两点的的距离 ========
        int LatitudeA = (int)(pointA.Latitude * 100000);
        int LongitudeA = (int)(pointA.Longitude * 100000);
        int LatitudeB = (int)(pointB.Latitude * 100000);
        int LongitudeB = (int)(pointB.Longitude * 100000);
        int nDistance = ((LatitudeA - LatitudeB) ^ 2 + (LongitudeA - LongitudeB) ^ 2) ^ (1 / 2);
        if (nDistance <= 0)
        {
            nDistance = nDistance * -1;
        }
        Response.Write("nDistance = " + nDistance); 
        return nDistance;
    }


    private int GetScale(int nDistance, int nWidth, int nHeight)
    {
        //================ 1.0 在精度为19时一个像素的距离 ========
        double fScale = 100.0f / 374;

        //================ 3.0 计算画面显示的最大距离 ========
        int nMax = nWidth;
        if (nWidth < nHeight)
        {
            nMax = nHeight;
        }

        for (int i = 19; i >= 1; i--)
        {
            int nPrecision = GetPrecision(i);
            int nTemp = (int)(fScale * nMax * nPrecision);
            Response.Write("</br>nTemp= " + nTemp);    
            Response.Write("</br>nDistance= " + nDistance);  
            if (nTemp > nDistance)
            {
                return (i-1);
            } 
        }
        return 18;
    }

    private int GetPrecision(int nScale)
    {
        int nPrecision = 1;
        for (int j = 0; j < (19 - nScale); j++)
        {
            nPrecision = nPrecision * 2;   
        }
        return nPrecision;
    }

}
