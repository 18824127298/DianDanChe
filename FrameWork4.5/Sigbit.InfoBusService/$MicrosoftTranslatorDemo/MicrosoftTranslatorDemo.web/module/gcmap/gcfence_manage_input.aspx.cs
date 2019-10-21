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
using System.Drawing;
using System.Windows.Forms;

using Sigbit.Framework;
using Sigbit.Common;
using Sigbit.Web.JavaScipt;
using Sigbit.Web.GCMap;

public partial class module_gcmap_gcfence_manage : GCMapPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //================ 1.0 首次返回 ================
        if (IsPostBack)
            return;

        InitFenceType();

        //================ 2.0 设置地图显示参数 ========    
        GCFenceParameter param = (GCFenceParameter)Session["fenceParamMPYN"];
        if (param == null)
        {
            return;
        }

        string sMode = ConvertUtil.ToString(Request["mode"]);
        if (sMode == "save")
        {
            string sALat = ConvertUtil.ToString(Request["ALat"]);
            string sALng = ConvertUtil.ToString(Request["ALng"]);
            string sBLat = ConvertUtil.ToString(Request["BLat"]);
            string sBLng = ConvertUtil.ToString(Request["BLng"]);
            string sADesc = ConvertUtil.ToString(Request["ADesc"]);
            string sBDesc = ConvertUtil.ToString(Request["BDesc"]);

            string sFenceName = ConvertUtil.ToString(Request["fenceName"]);
            string sAlarmType = ConvertUtil.ToString(Request["alarmType"]);

            GCFenceDefine newFence = new GCFenceDefine();
            newFence.FenceName = sFenceName;
            newFence.AlarmType = sAlarmType;
            newFence.PointA = new GCMapPoint();
            newFence.PointA.Latitude = ConvertUtil.ToFloat(sALat);
            newFence.PointA.Longitude = ConvertUtil.ToFloat(sALng);
            newFence.PointA.PointDesc = sADesc;

            newFence.PointB = new GCMapPoint();
            newFence.PointB.Latitude = ConvertUtil.ToFloat(sBLat);
            newFence.PointB.Longitude = ConvertUtil.ToFloat(sBLng);
            newFence.PointB.PointDesc = sBDesc;

            GCFenceResult fenceResult = (GCFenceResult)HttpContext.Current.Session["fenceResultMPYN"];

            switch (param.FenceManageMode)
            {
                case GCFenceManageMode.NewFence:
                    fenceResult = new GCFenceResult();
                    fenceResult.HasModifyFence = false;
                    fenceResult.HasNewFence = true;
                    fenceResult.OldFenceDefine = null;
                    fenceResult.NewFenceDefine = newFence;
                    HttpContext.Current.Session["fenceResultMPYN"] = fenceResult;
                    CloseAndRefreshOpener();
                    break;
                case GCFenceManageMode.ModifyFence:
                    GCFenceDefine oldFence = fenceResult.OldFenceDefine;
                    fenceResult.NewFenceDefine = newFence;
                    bool bHasChanged = CheckFenceHasChange(oldFence, newFence);
                    fenceResult.HasModifyFence = bHasChanged;
                    fenceResult.HasNewFence = false;
                    HttpContext.Current.Session["fenceResultMPYN"] = fenceResult;
                    if (bHasChanged)
                    {
                        CloseAndRefreshOpener();
                    }
                    else
                    {
                        JSWindow.CloseWindow();
                    }
                    break;
                case GCFenceManageMode.ShowFence:
                    JSWindow.CloseWindow();
                    break;
            }
            return;
        }

        //==============显示围栏及地图===========================
        switch (param.FenceManageMode)
        {
            case GCFenceManageMode.ShowFence:
                //edtFenceName.Text = param.CurrentFence.FenceName;
                //ddlbFenceType.SelectedValue = param.CurrentFence.AlarmType;
                //ShowFence(param.CurrentFence.PointA, param.CurrentFence.PointB);
                break;
            case GCFenceManageMode.ModifyFence:
                edtFenceName.Text = param.CurrentFence.FenceName;
                ddlbFenceType.SelectedValue = param.CurrentFence.AlarmType;
                ShowFence(param.CurrentFence.PointA, param.CurrentFence.PointB);
                
                break;
            case GCFenceManageMode.NewFence:
                btnDeleFence.Visible = false;
                break;
        }
        ViewState["center"] = param.MapStyle.CenterPoint.ToString();
        ViewState["scale"] = param.MapStyle.Scale;

    }


    private void InitFenceType()
    {
        ddlbFenceType.Items.Clear();
        ddlbFenceType.Items.Add(new ListItem("进入告警", "in"));
        ddlbFenceType.Items.Add(new ListItem("出来告警", "out"));
        ddlbFenceType.Items.Add(new ListItem("进出告警", "all"));
    }

    /// <summary>
    /// 
    /// </summary>
    public void CloseAndRefreshOpener()
    {
        //string script = "<script language=\"javascript\">";
        //script = script + "  try{ ";
        //script = script + "		top.opener.location.reload(); ";
        //script = script + "		top.opener.focus(); ";
        //script = script + "	}catch(e){} ";
        //script = script + "		window.close(); ";
        //script = script + "    </script> ";

        string script = "<script language=\"javascript\">";
        script = script + "  try{ ";
        script = script + "      var url=top.opener.location.href; ";
        script = script + "      url=url.split('?')[0];  ";
        script = script + "		 top.opener.location.href=url; ";
        script = script + "		";
        script = script + "	    }catch(e){} ";
        script = script + "		window.close(); ";
        script = script + "    </script> ";

        HttpContext.Current.Response.Write(script);
    }


    private bool CheckFenceHasChange(GCFenceDefine oldFence, GCFenceDefine newFence)
    {
        if (oldFence.FenceName != newFence.FenceName)
        {
            return true;
        }

        if (oldFence.AlarmType != newFence.AlarmType)
        {
            return true;
        }

        if (oldFence.PointA.Latitude != newFence.PointA.Latitude)
        {
            return true;
        }

        if (oldFence.PointA.Longitude != newFence.PointA.Longitude)
        {
            return true;
        }

        if (oldFence.PointB.Latitude != newFence.PointB.Latitude)
        {
            return true;
        }

        if (oldFence.PointB.Longitude != newFence.PointB.Longitude)
        {
            return true;
        }

        return false;

    }

    private void ShowFence(GCMapPoint pointA, GCMapPoint pointB)
    {
        string sShowPolygon = "";
        sShowPolygon += "var lngA = new GLatLng(" + pointA.ToString() + ");";
        sShowPolygon += "\r\n\tvar lngB = new GLatLng(" + pointB.ToString() + ");";
        sShowPolygon += "\r\n\ttools.polygonControl.MapDrawPolygon(lngA, lngB);";
        sShowPolygon += "\r\n\t";
        ViewState["showPolygon"] = sShowPolygon;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        GCFenceResult fenceResult = (GCFenceResult)HttpContext.Current.Session["fenceResultMPYN"];
        fenceResult.HasDeleFence = true;
        CloseAndRefreshOpener();
    }
}
