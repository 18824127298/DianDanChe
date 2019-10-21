<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gcmap_show_track.aspx.cs"
    Inherits="module_gcmap_gcmap_show_track" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>地图轨迹显示</title>
    <meta content="text/html; charset=UTF-8" http-equiv="content-type">
    <script type="text/javascript" language="javascript">
    function initialize() 
    {
        if (GBrowserIsCompatible())
        {                         
            //============== 1.0 设置地图默认控件 =============
            var map = new GMap2(document.getElementById("map_canvas"));
                                                              
            //============== 2.0 设置地图显示中心 =============
            var centerPoint = new GLatLng(<%=ViewState["Lat"] %>,<%=ViewState["Lng"] %>);
	        map.setCenter(centerPoint , <%=ViewState["Scale"] %>);
	        
	        //var marker0 = createMarker (centerPoint, 25, '中心点');
	        //map.addOverlay(marker0);       
	        
	        

            //============== 3.0 设置地图默认控件 ============== 
            map.setUIToDefault();
                      
            //============== 4.0 给地图添加小图控件 ============= 
            map.addControl(new GOverviewMapControl()); 
                                               
            //============== 5.0 显示一个条线 ==================
            var polyline = new GPolyline(
            [                          
                <%=ViewState["linePoint"] %> 
            ], //GLatLng()数组 
            "#FF0000", //折线颜色
            2, //折线宽度
            0.4 //透明度
        ); 
        map.addOverlay(polyline);
                   
        //============== 6.0 给地标添加显示 ==================            
        <%=ViewState["lineMarker"] %>     
        
            
                                     
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
        index=index%26;
        var letter = String.fromCharCode("A".charCodeAt(0) + index);
        
        var letteredIcon = new GIcon(baseIcon);
        letteredIcon.image = "http://www.google.cn/mapfiles/marker" + letter + ".png";
 
        //============== 11.3 设置 GMarkerOptions 对象 ==================
        var markerOptions = { icon:letteredIcon ,title: title};
        var marker = new GMarker(point, markerOptions);         
                  
        //============== 11.4 给地标添加事件 ==================
        GEvent.addListener
        (
            marker,"click", 
            function() 
            {
                createMarkerDesc(point,marker,title);
            }
        );
        return marker;
    }
    
    //=============创建地标描述信息==============
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
<body onload="initialize()" leftmargin="0" topmargin="0" onunload="GUnload()">
    <div style="width: 512px; height: 512px;" id="map_canvas">
    </div>
</body>
</html>
