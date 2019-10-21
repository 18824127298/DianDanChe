<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gcmap_show_point.aspx.cs"
    Inherits="module_gcmap_gcmap_show_point" %>

 
<%--<script type="text/javascript" language="javascript">
    var map = null;
    function initialize(dLat,dLng,nScale,sTitle) 
    {
        if (GBrowserIsCompatible()) 
        {                         
            //============== 1.0 设置地图默认控件 =============
            map = new GMap2(document.getElementById("map_canvas")); 
        
            //============== 2.0 设置地图默认控件 ============== 
            map.setUIToDefault();
                      
            //============== 3.0 给地图添加小图控件 ============= 
            map.addControl(new GOverviewMapControl()); 
                                                              
            //============== 4.0 设置地图显示中心 ============= 
	        var centerPoint = new GLatLng(dLat,dLng);
	        map.setCenter(centerPoint , nScale);
	
	        //============== 5.0 显示地标 ==================
	        var marker0 = createMarker (centerPoint, 0, sTitle);
	        map.addOverlay(marker0);       
        }
    } 
    
    
    //============= 11.0 创建围栏前显示对应给定索引的字母的标记 =============
    function createMarker (point, index, title) 
    {
        //============== 11.1  为所有标记创建指定阴影、灯的基础图标============
        var baseIcon = new GIcon();
        baseIcon.shadow = "http://www.google.cn/mapfiles/shadow50.png";
        baseIcon.iconSize = new GSize(20, 34);
        baseIcon.shadowSize = new GSize(37, 34);
        baseIcon.iconAnchor = new GPoint(9, 34);
        baseIcon.infoWindowAnchor = new GPoint(9, 2);
        baseIcon.infoShadowAnchor = new GPoint(18, 25);
 
        //============== 11.2 创建一个气球上显示的字母 ==================
        var letter = String.fromCharCode("A".charCodeAt(0) + index);
        var letteredIcon = new GIcon(baseIcon);
        letteredIcon.image = "http://www.google.cn/mapfiles/marker" + letter + ".png";
 
        //============== 11.3 设置 GMarkerOptions 对象 ==================
        var markerOptions = { icon:letteredIcon ,title: title};
        var marker = new GMarker(point, markerOptions);    
        
        //===============11.4 显示当前点描述=============================
        createMarkerDesc(point,marker,title);
                
                  
        //============== 11.5 给地标添加事件 ==================
        GEvent.addListener(marker, "click", 
            function() 
            {
                createMarkerDesc(point,marker,title);
            });
        return marker;
    }
    
    function createMarkerDesc(point,marker,title)
    {
        if(title=="")
        {
            var geocoder = new GClientGeocoder();  
            geocoder.getLocations(point, 
            function(response) 
            {  
                if (!response || response.Status.code != 200) 
                {  
		            //alert("没有找到经纬度为" + response.name + "的地理信息");
		            //return;
                } 
                else 
                {
                    var place = response.Placemark[0];   
                
		            //============= 2.1 添加实际点标 ====================
		            var nPoint = response.name.split(',');
                    var point0 = new GLatLng(nPoint[0],nPoint[1]); 
                
                    var sInfo = '<b>经纬度信息:</b>' + response.name + '<br/>' +             
                        '<b>最近地点信息:</b>' + place.address + '<br>' + 
                        '<b>精确度:</b>' + place.AddressDetails.Accuracy + '<br>';
                    marker.openInfoWindowHtml(sInfo);       
                }
            }); 
        }
        else
        {
            marker.openInfoWindowHtml(title);       
        }  
    }
    </script>

</head>
<body leftmargin="0" topmargin="0" onload="initialize(<%=_currentLat %>,<%=_currentLng %>,<%=_nScale %>,'<%=_sTitle %>')"
    onunload="GUnload()">
    <div style="width: 512px; height: 512px;" id="map_canvas">
    </div>
</body>
</html>--%>

<!DOCTYPE html>
<html>
<head>
<meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
<meta http-equiv="content-type" content="text/html; charset=UTF-8"/>
<title>Sogou Maps JavaScript API v2 Example: Marker Simple</title>
<link href="http://code.google.com/apis/maps/documentation/javascript/examples/default.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://api.go2map.com/maps/js/api_v2.5.1.js"></script>
<script type="text/javascript">
function initialize(dLat,dLng,nScale,sTitle) {
var myLatlng = new sogou.maps.LatLng(dLat,dLng);
//可以直接使用搜狗坐标。
//var myLatlng = new sogou.maps.Point(12948872.55859375,4835520.5078125);
var myOptions = {
zoom: nScale,
center: myLatlng,
mapTypeId: sogou.maps.MapTypeId.ROADMAP
}
var map = new sogou.maps.Map(document.getElementById("map_canvas"), myOptions);

var marker = new sogou.maps.Marker({
position: myLatlng, 
map: map,
title:sTitle
}); 
}
</script>
</head>
<body onload="initialize(<%=_currentLat %>,<%=_currentLng %>,<%=_nScale %>,'<%=_sTitle %>')">
<div id="map_canvas"></div>
</body>
</html>



