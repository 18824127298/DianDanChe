<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gcmap_util_desc_input.aspx.cs" Inherits="module_gcmap_gcmap_util_desc_input" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>获取位置描述</title> <meta content="text/html; charset=UTF-8" http-equiv="content-type">
 <%--   <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAA3iglYFtlRBk7t4bt7HcvWxT2yXp_ZAY8_ufC3CFXhHIE1NvwkxRGfWwbL9deXLm1SbotQ16CTgVsUg" type="text/javascript"></script>
      --%>
    <%--<script type="text/javascript" language="javascript" src="js/maps.js"></script>--%>
    <meta content="text/html; charset=UTF-8" http-equiv="content-type">
    <script type="text/javascript" language="javascript" >
    var arrayDescription = new Array();
    var GlobalMap = null;
    
    function initialize() {
    if (GBrowserIsCompatible()) {          
                                            
        GlobalMap = new GMap2(document.getElementById("map_canvas"));
        
        GlobalMap.setCenter(new GLatLng(<%=ViewState["center"] %>), 18);

      //============== 1.2 设置地图默认控件 =================
        GlobalMap.setUIToDefault();
                      
      //============== 2.3 给地图添加小图控件 =================
        GlobalMap.addControl(new GOverviewMapControl());   
          
        //============ 3.0 要查找的经纬度信息 =============     
        <%=ViewState["latlng"] %>
      }
    } 
       
    function getDescription (point,num)
    {                        
        var nCount = <%=ViewState["count"] %>;
        //============== 1.0 设置 GMarkerOptions 对象 ==================
        var geocoder = new GClientGeocoder();  
        geocoder.getLocations(point, function(response) {   
		    //alert("没有找到经纬度为" + response.name + "的地理信息");
            if (!response || response.Status.code != 200) {  
                arrayDescription[num] = null;  
                var count = arrayDescription.length;
                if(count == nCount)
                {
                    //SendDescription (arrayDescription);
                }  
            } else {
                var place = response.Placemark[0];   

		        //============= 1.1 添加实际点标 ====================
		        var nPoint = response.name.split(',');
                var point0 = new GLatLng(nPoint[0],nPoint[1]); 
                var sInfo = '<b>经纬度信息:</b>' + response.name + '<br/>' +             
                '<b>最近地点信息:</b>' + place.address + '<br>' + 
                '<b>精确度:</b>' + place.AddressDetails.Accuracy + '<br>';
                arrayDescription[num] = place.address;
                                                     
                  //============= 1.0 添加最近点标 ====================
                var point2 = new GLatLng(place.Point.coordinates[1],place.Point.coordinates[0]);
//                var marker2 = new GMarker(point2,{title: "最近标志点"});
//                GlobalMap.addOverlay(marker2);
         
		        //============= 2.0 添加实际点标 ====================   
                var marker0 = new GMarker(point0,{icon: getIcon(),title: "实际所在地点"});
                GlobalMap.addOverlay(marker0);
                
                createMarker(point2, num);
                                           
                var count = arrayDescription.length;
                if(count == nCount)
                {
                    //SendDescription (arrayDescription);
                }  
          }
        }); 
    }
    
    function SendDescription (list)
    {
        var count = list.length;
       if(list.length <= 0)
       {                                                   
           window.location.href = "description.aspx?info=地理信息获取失败！";
           return;
       }
       
       var infos = "";
       for(var i=0;i < count;i++)
       {                 
           if(i != 0)
           {
              infos += "|";
           }
           var info = list[i];
           if(info != null)
           {
              infos += info;
           } 
       }
       window.location.href = "description.aspx?info=" + infos;
    }
    
    //========== 创建一个自定义的GIcon ============
	function getIcon()
	{
		//创建一个自定义的GIcon
		var myIcon= new GIcon();
		//前景图片
		myIcon.image = "./images/boy.gif";
		//阴影图片
		myIcon.shadow = "http://labs.google.com/ridefinder/images/mm_20_shadow.png";
		//前景图片大小，长x宽
		myIcon.iconSize = new GSize(16, 16);
		//阴影图片大小，长x宽
		myIcon.shadowSize = new GSize(16, 16);
		
		//以下两个属性很难解释，读者可自行调整其数值以便理解其意义
		// myIcon锚定点相对于myIcon图片左上角的像素距离
		myIcon.iconAnchor = new GPoint(6, 10);
		//信息窗口相对于myIcon图片左上角的像素距离
		//关于信息窗口会在以后介绍
		myIcon.infoWindowAnchor = new GPoint(5, 1);
		return myIcon;
	}
	
	
    function createMarker(point, index) {
    //============== 10.1  为所有标记创建指定阴影、灯的基础图标============
    var baseIcon = new GIcon();
    baseIcon.shadow = "http://www.google.cn/mapfiles/shadow50.png";
    baseIcon.iconSize = new GSize(20, 34);
    baseIcon.shadowSize = new GSize(37, 34);
    baseIcon.iconAnchor = new GPoint(9, 34);
    baseIcon.infoWindowAnchor = new GPoint(9, 2);
    baseIcon.infoShadowAnchor = new GPoint(18, 25);
    var markerOptions = null;
 
    //============== 10.2 创建一个气球上显示的字母 ==================
    if(index >=0 ){
    var letter = String.fromCharCode("A".charCodeAt(0) + index);
    var letteredIcon = new GIcon(baseIcon);
    letteredIcon.image = "http://www.google.cn/mapfiles/marker" + letter + ".png";
 
    //============== 10.3 设置 GMarkerOptions 对象 ==================
    var markerOptions = { icon:letteredIcon };
    }                                                  
              
    //============== 10.3 设置 GMarkerOptions 对象 ==================
//    var geocoder = new GClientGeocoder();  
//    var sLocations = geocoder.getLocations(point, function(response) {  
//            if (!response || response.Status.code != 200) {  
//		        //alert("没有找到经纬度为" + response.name + "的地理信息");
//            } else {
//                var place = response.Placemark[0];   

//		        //============= 2.0 添加实际点标 ====================
//		        var nPoint = response.name.split(',');
//                var point0 = new GLatLng(nPoint[0],nPoint[1]);
                var marker0 = new GMarker(point,markerOptions);
                GlobalMap.addOverlay(marker0);
//                
//                var sInfo = '<b>经纬度信息:</b>' + response.name + '<br/>' +             
//                '<b>最近地点信息:</b>' + place.address + '<br>' + 
//                '<b>精确度:</b>' + place.AddressDetails.Accuracy + '<br>';
//                                                  
//                 //============== 4.0 给地标添加事件 ==================
//                GEvent.addListener(marker0, "click", function() {
//                    marker0.openInfoWindowHtml(sInfo);
//                }); 
//              return marker0;
//          }
//    });
 
    return null;                       
}


        
    </script>  
</head>
<body onload= "initialize();">
    <form id="form1" runat="server">
      <div style="width: 600px; height: 450px;" id="map_canvas">
          &nbsp;</div>   
    <div style="font-size:large; text-align:center; color:Red">您还没有传经纬度参数了！</div> 
    <div id="error" style="font-size:large; text-align:center; color:Red">地点位置查看：</div> 
    <div style="font-size:large; text-align:center; color:Red"><a href='gcmap_util_desc_look.aspx?piont=<%=ViewState["init"] %>&type=all'>gcmap_util_desc_look.aspx?piont=<%=ViewState["init"] %>&type=all</a></div>  
    <div style="font-size:large; text-align:center; color:Red"><a href='gcmap_util_desc_look.aspx?piont=<%=ViewState["init"] %>&type=one'>gcmap_util_desc_look.aspx?piont=<%=ViewState["init"] %>&type=one</a></div>  
    <div id="Div1" style="font-size:large; text-align:center; color:Red">地点位置信息获取：</div> 
    <div style="font-size:large; text-align:center; color:Red"><a href='gcmap_util_desc_input.aspx?piont=<%=ViewState["init"] %>&type=one'>gcmap_util_desc_input.aspx?piont=<%=ViewState["init"] %>&type=one</a></div>  
    <div style="font-size:large; text-align:center; color:Red"><a href='gcmap_util_desc_input.aspx?piont=<%=ViewState["init"] %>&type=all'>gcmap_util_desc_input.aspx?piont=<%=ViewState["init"] %>&type=all</a></div>  
    </form>
</body>
</html>
