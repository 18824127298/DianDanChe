<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gcfence_manage_input.aspx.cs"
    Inherits="module_gcmap_gcfence_manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title>告警围栏设置</title>
    <meta content="text/html; charset=UTF-8" http-equiv="content-type">

    <script type="text/javascript" language="javascript" src="js/GlobalJS.js"></script>

    <script type="text/javascript" language="javascript" src="js/Tools.js"></script>

    <script type="text/javascript" language="javascript" src="js/DrawingPolygon.js"></script>

    <script type="text/javascript" language="javascript" src="js/FindDescription.js"></script>

    <script type="text/javascript" language="javascript">
                            
//============== 2.0 设置地图默认控件 =================
function initialize() {
    if (GBrowserIsCompatible()) {    
        
        //============== 2.1 设置地图默认控件 =================  
        HiddenALatitude = document.getElementById("pointALatitude"); 
        HiddenALongitude = document.getElementById("pointALongitude"); 
        HiddenBLatitude = document.getElementById("pointBLatitude"); 
        HiddenBLongitude = document.getElementById("pointBLongitude"); 
      
        
        HiddenCLongitude = document.getElementById("pointCLongitude"); 
                                                
        GlobalDomMap = document.getElementById("map_canvas");
      
        GlobalMap = new GMap2(GlobalDomMap);
      
        GlobalMap.setCenter(new GLatLng(<%=ViewState["center"] %>), <%=ViewState["scale"] %>);

        //============== 2.2 设置地图默认控件 =================
        GlobalMap.setUIToDefault();
                      
        //============== 2.3 给地图添加小图控件 =================
        GlobalMap.addControl(new GOverviewMapControl());
                                                   
        //============== 2.4 给地图添加工具控件 =================
        var tools = new ToolsControl();         
        GlobalMap.addControl(tools);
                      
        //============== 2.5 显示已有围栏 =================  
        <%= ViewState["showPolygon"]  %>  
//        var lngA = new GLatLng(23.80042,113.2635);
//        var lngB = new GLatLng(23.03424,113.87329);
//        tools.polygonControl.MapDrawPolygon(lngA,lngB);
      }
} 

function saveFence()
{     
 
    if(HiddenALatitude.value=='')
    {
        alert("请点击设置围栏按钮设置围栏");
        return;
    }

    var sFenceName=document.getElementById("edtFenceName").value;
    if(sFenceName=='')
    {
        alert("请填写围栏名称");
        return;
    }
    
    var sAlarmType=document.getElementById("ddlbFenceType").value;
   
                                              
    var sParam = "mode=save&ALat=" + HiddenALatitude.value + "&ALng=" + HiddenALongitude.value + 
    "&BLat=" + HiddenBLatitude.value + "&BLng=" + HiddenBLongitude.value
    +"&ADesc="+escape(FenceADesc)+"&BDesc="+escape(FenceBDesc)+"&fenceName="+escape(sFenceName)
    +"&alarmType="+sAlarmType;
    
    location.href = "gcfence_manage_input.aspx?" + sParam;
   
}
    function SetFocus()
    {
        document.getElementById("btnSave").focus();
    }
    
    function DelePrompt()
    {
        if(window.confirm("确认删除当前围栏吗?"))
        {
            return true;
        }
        return false;
    }
    
    </script>

</head>
<body onload="initialize()" style="margin-left: 0; margin-top: 0">
    <form runat="server" id="myForm" onfocus="SetFocus()">
        <div align="center">
            <table width="512px" border="1">
                <tr>
                    <td align="center">
                        <div style='width: 512px; height: 400px;' id="map_canvas">
                            <asp:HiddenField ID="pointALatitude" runat="server" />
                            <asp:HiddenField ID="pointALongitude" runat="server" />
                            <asp:HiddenField ID="pointBLatitude" runat="server" />
                            <asp:HiddenField ID="pointBLongitude" runat="server" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;围栏名称：<asp:TextBox ID="edtFenceName" Width="240px" runat="server" />
                        &nbsp;围栏告警：<asp:DropDownList ID="ddlbFenceType" runat="server" Width="75px" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        &nbsp;&nbsp;
                        <input id="btnSave" type="button" onclick="saveFence()" value="保存当前设置围栏" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnDeleFence" runat="server" Visible="false" Text=" 删除当前围栏 " OnClientClick="return DelePrompt();" OnClick="btnDelete_Click" />
                    </td>
                </tr>
            </table>
        </div>
       
    </form>
</body>
</html>
