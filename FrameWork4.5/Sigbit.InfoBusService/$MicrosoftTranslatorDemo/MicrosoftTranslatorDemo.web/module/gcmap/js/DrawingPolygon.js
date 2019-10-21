//================= DrawingPolygonControl 这是一个 GControl 地图工具栏控件 ==================
//**** 可以显示一些按钮。（＂绘制围栏＂和＂删除＂按钮）***************************
//====================================================================================

//============ 1.0 创建这个控件 ================
function DrawingPolygonControl() { }
DrawingPolygonControl.prototype = new GControl();
DrawingPolygonControl.prototype.initialize = function() { 
    this.name = "绘制围栏，删除围栏";
    this.map = null;   
    this.counter = 0;   //点击次数
    this.latlngA = null;//A点座标
    this.markerA = null;//A点地标
    this.latlngB = null;//B点座标
    this.markerB = null;//B点地标
    this.polygon = null;//围栏
    this.parentControl = null;//父级控件
    this.parentVessel = null;//父级容器
    this.drawImgDiv = null;//绘制控件
    this.onDrawListenerClick = null;//绘制事件
    this.onListenerMousemove = null;//移动事件
}

//============ 2.0 创建一个绘制围栏图片,并将其返回 ==============    
DrawingPolygonControl.prototype.initiDrawing = function(map,imgUrl,isDirect) {   
    this.counter = 0;   //点击次数  
    
    if(map != null)
        this.map = map;  
	//============ 2.1 绘制围栏信息 ==============
    this.drawImgDiv = document.createElement("div"); 
    if(imgUrl != null && imgUrl != "")
    {
        this.drawImgDiv.style.backgroundImage = "url("+imgUrl+")";  
    }else
    {
        this.drawImgDiv.style.backgroundImage = "url(./images/MARKPOINTB.gif)";  
    }
    this.drawImgDiv.title = "绘制围栏信息";  
    this.drawImgDiv.tools = this;   
         
    //============= 2.2 直接触发，还是注册事件 ==============
    if(isDirect)
    {        
	    this.onDrawListenerClick = GEvent.bind(GlobalMap, "click", this, this.onDrawMapClick); 
    }else
    {
        GEvent.addDomListener(this.drawImgDiv, "click", this.onDrawClick); 
    }
    return this.drawImgDiv;
}  


  //============ 4.0 创建一个能够返回原点的图标 ==============    
DrawingPolygonControl.prototype.initiReturn = function(map,imgUrl) {   
          
    if(map != null)
        this.map = map;
          
	//============ 2.1 绘制围栏信息 ==============
    var returnImgDiv = document.createElement("div"); 
    if(imgUrl != null && imgUrl != "")
    {
        returnImgDiv.style.backgroundImage = "url("+imgUrl+")";  
    }else
    {
        returnImgDiv.style.backgroundImage = "url(./images/BACKWARD.gif)";  
    }
    returnImgDiv.title = "返回到原点";  
    returnImgDiv.tools = this;   
         
    //============= 2.2 注册事件 ==============
    GEvent.addDomListener(returnImgDiv, "click", this.onReturnClick); 
    return returnImgDiv;
} 


  //============= 8.0 给地图添加回返事件 =========== 
DrawingPolygonControl.prototype.onReturnClick = function() {
    var map = this.tools.map;
    //============= 8.1 清除地图上以有的所有事件 ===========
    GEvent.clearInstanceListeners(GlobalMap); 

    //============= 8.2 返回地图原点 ===========
    if (GlobalScale != null && GlobalCenter != null)
    {
        GlobalMap.setCenter(GlobalCenter , GlobalScale); 
    }                             
}   


//============ 3.0 创建一个清除围栏图片，并将其返回 ==========
DrawingPolygonControl.prototype.initiClear = function(map,imgUrl) {
    if(map != null)
        this.map = map;   
    //============= 3.1 清除围栏信息 ==============
    var clearDrawImgDiv = document.createElement("div");
    if(imgUrl != null && imgUrl != "")
    {
        clearDrawImgDiv.style.backgroundImage = "url("+imgUrl+")";  
    }else
    {
        clearDrawImgDiv.style.backgroundImage = "url(./images/minpath.gif)";  
    }                                                                         
    clearDrawImgDiv.title = "清除围栏信息";    
    clearDrawImgDiv.tools = this;
    
    //============= 3.2 注册事件 ==============
    GEvent.addDomListener(clearDrawImgDiv, "click", this.onClearDrawClick);                                          
    return clearDrawImgDiv;
}
     
//============= 4.0 给地图添加绘制围栏事件 =========== 
DrawingPolygonControl.prototype.onDrawClick = function() {    
    //============= 4.1 清除地图上以有的所有事件 ===========
    GEvent.clearInstanceListeners(GlobalMap); 

    //============= 4.2 清除地图上以有的图层 ===========
    GlobalMap.clearOverlays(); 
    if(this.tools == null)
    {
        this.counter = 0;  
	    this.onDrawListenerClick = GEvent.bind(GlobalMap, "click", this, this.onDrawMapClick); 
    }else{
        this.tools.counter = 0;  
	    var listenerClick = GEvent.bind(GlobalMap, "click", this.tools, this.tools.onDrawMapClick); 
	    this.tools.onDrawListenerClick = listenerClick; 
    }    
}


     
//============= 5.0 给地图添加清除绘制事件 =========== 
DrawingPolygonControl.prototype.onClearDrawClick = function() {
    var map = this.tools.map;
    //============= 4.1 清除地图上以有的所有事件 ===========
    GEvent.clearInstanceListeners(GlobalMap); 

    //============= 4.2 清除地图上以有的图层 ===========
    GlobalMap.clearOverlays(); 
    this.tools.counter = 0;       
}  

//============= 6.0 给地图绘制移动围栏（目前暂不用，有重叠） =========== 
DrawingPolygonControl.prototype.onMapMousemove = function(latlng) {
    var map = this.map;
    //绘制移动围栏
    var point0 = this.latlngA;
    var point1 = this.ToCountPoint(point0,latlng,"left");
    var point2 = latlng; 
    var point3 = this.ToCountPoint(point0,latlng,"right");
    var point4 = this.latlngA;  
    var polygon = this.drawPolygonImg(point0,point1,point2,point3,point4);
    map.addOverlay(polygon); 
}
              
//============= 7.0 知道两点给地图画围栏 =========== 
DrawingPolygonControl.prototype.MapDrawPolygon = function(latlngA,latlngB) { 
    var map = this.map;    
    if(latlngA == null ||latlngB == null )
        return;
     // debugger;
    //============= 7.1 设置控件属性参数 ===========
    this.latlngA = latlngA;        
    this.latlngB = latlngB;
                             
    //============= 7.2 添加地标A ==============                                      
    var markerA = this.createMarker(latlngA, 0);              
    HiddenALatitude.value = latlngA.y;
    HiddenALongitude.value = latlngA.x;   
    this.markerA= markerA;  
 
    //=========== 7.3 绘制围栏 ==================
    var point0 = this.latlngA;
    var point1 = this.ToCountPoint(point0,latlngB,"left");
    var point2 = latlngB; 
    var point3 = this.ToCountPoint(point0,latlngB,"right");
    var point4 = this.latlngA;  
    var polygon = this.drawPolygonImg(point0,point1,point2,point3,point4);
    map.addOverlay(polygon);  
	                             
    //=========== 7.4 添加地标B ==================                                  
    var markerB = this.createMarker(latlngB, 1);   
		    
    //=========== 7.5 加入数据用于删除 ===========
    this.latlngB = latlngB;
    this.markerB= markerB;
    this.polygon = polygon;                                             
    HiddenBLatitude.value = latlngB.y;
    HiddenBLongitude.value = latlngB.x;
		 
    //============= 7.6 点击量加加 ============== 
    this.counter = 1;  
                      
    //============= 7.1 清除地图上以有的所有事件 ===========
    GEvent.clearInstanceListeners(GlobalMap);    
    this.parentVessel.innerHTML = "";                   
}

//============= 7.0 给地图点击画围栏 =========== 
DrawingPolygonControl.prototype.onDrawMapClick = function(overlay, latlng, latlng2) { 
    var map = this.map;    
    //============= 7.1 点击两次后返回 ===========
    if(this.counter > 2)
    {                      
        return;
    }
    //debugger;               
    //============= 7.2 确定点击的座标 ===========
    if(latlng == null && latlng2 != null)
    {
        latlng = latlng2;
    }            
                   
    if(this.counter == 2)
    {
    
        this.counter = 0;
        this.onDrawClick();   
        this.parentVessel.innerHTML = "请点击您要设置的第一点！";    
        return;
    }   
	           
    //============= 7.3 确定点击的座标不为空时 ============
    if (latlng != null) 
    {   
        //============= 7.3.1 第二次进入 ==================
        if(this.counter == 1)
        {
            //=========== 7.3.2 绘制围栏 ==================
            var point0 = this.latlngA;
            var point1 = this.ToCountPoint(point0,latlng,"left");
            var point2 = latlng; 
	        var point3 = this.ToCountPoint(point0,latlng,"right");
	        var point4 = this.latlngA;  
	        var polygon = this.drawPolygonImg(point0,point1,point2,point3,point4);
	        map.addOverlay(polygon); 
	                             
            //=========== 7.3.3 添加地标B ==================                                  
		    var markerB = this.createMarker(latlng, this.counter);   
		    
            //=========== 7.3.4 加入数据用于删除 ===========
	        this.latlngB = latlng;
            this.markerB= markerB;
	        this.polygon = polygon;                                             
		    HiddenBLatitude.value = latlng.y;
		    HiddenBLongitude.value = latlng.x;
		    SetPointDesc(latlng,'B');
	        //=========== 7.3.5 删除点击事件 ===============  
	        //GEvent.clearInstanceListeners(map); 
	        //GEvent.removeListener(this.onListenerMousemove); 
	        //GEvent.removeListener(this.onDrawListenerClick);  
            if(this.parentVessel != null)
            {                      
	            this.parentVessel.innerHTML = "注：再次点击将会清除围栏！"; 
                //this.onDrawClick();                                     
            }
	     }else
	     {              
            //============= 7.3.6 第一次进入，添加移动事件 =======     
            //var listenerMousemove = GEvent.bind(map, "mousemove", this, this.onMapMousemove);
            //this.onListenerMousemove = listenerMousemove;
 
            //============= 7.3.7 添加地标A ==============                                      
		    var markerA = this.createMarker(latlng, this.counter);              
		    HiddenALatitude.value = latlng.y;
		    HiddenALongitude.value = latlng.x;   
            this.markerA= markerA; 
            this.latlngA= latlng;   
            if(this.parentVessel != null)
            {
                    SetPointDesc(latlng,'A');
	            this.parentVessel.innerHTML = "请点击第二点，将绘制成围栏。";
            }                 
	     } 
        //============= 7.3 点击量加加 ============== 
        this.counter++;    
      }                                         
}

function SetPointDesc(point,pointName)
{
        var sPointDesc='';
        var geocoder = new GClientGeocoder();  
        geocoder.getLocations(point, 
        function(response) 
            {  
                if (!response || response.Status.code != 200) 
                {  
		            sPointDesc='';
                } 
                else 
                {
                     var place = response.Placemark[0];  
                    sPointDesc=place.address;
                     if(pointName=='A')
                    {
                        FenceADesc=sPointDesc;
                    }
                    else
                    {
                        FenceBDesc=sPointDesc;
                    }
                }
            }); 
       
}
    
//============= 8.0 绘制围栏矩形 =================
DrawingPolygonControl.prototype.drawPolygonImg = function (point0,point1,point2,point3,point4){
    var polygon = new GPolygon(
    [point0,point1,point2,point3,point4 ], //GLatLng()数组 
    "#FF0000", //边线颜色
    1, //边线宽度
    0.8, //边线透明度
    "#00FF00", //填充颜色
    0.3 //填充颜色透明度
    ); 
    return polygon;
}
         
//============= 9.0 计算围栏矩形左下，右上点 =====
DrawingPolygonControl.prototype.ToCountPoint = function (leftTop,rightDown,type){
    var point = null;
    if(type=="left")
    {
        point = new GLatLng(rightDown.y,leftTop.x);
    }else{
        point = new GLatLng(leftTop.y,rightDown.x);
    } 
    return point;
}
   
//============= 10.0 创建围栏前显示对应给定索引的字母的标记 =============
DrawingPolygonControl.prototype.createMarker = function (point, index) {
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
    var geocoder = new GClientGeocoder();  
    var sLocations = geocoder.getLocations(point, function(response) { 
            var marker0 = new GMarker(point,markerOptions);
            GlobalMap.addOverlay(marker0);
            if (!response || response.Status.code != 200) {  
		        //alert("没有找到经纬度为" + response.name + "的地理信息");
            } else {
                var place = response.Placemark[0];   

		        //============= 2.0 添加实际点标 ====================
                
                var sInfo = '<b>经纬度信息:</b>' + response.name + '<br/>' +             
                '<b>最近地点信息:</b>' + place.address + '<br>' + 
                '<b>精确度:</b>' + place.AddressDetails.Accuracy + '<br>';
                       
		        //================ 3.0 显示地理信息 ====================
                //marker0.openInfoWindowHtml(sInfo);
                
                 //============== 4.0 给地标添加事件 ==================
                GEvent.addListener(marker0, "click", function() {
                    marker0.openInfoWindowHtml(sInfo);
                }); 
              return marker0;
          }
    });
 
    return null;                       
}

//============== 4.0 地图单击后回调 =================
DrawingPolygonControl.prototype.showAddress = function(response) { 
    
    GlobalMap.clearOverlays();
    if (!response || response.Status.code != 200) {  
		//alert("没有找到经纬度为" + response.name + "的地理信息");
    } else {
        var place = response.Placemark[0];   

		//============= 2.0 添加实际点标 ====================
		var nPoint = response.name.split(',');
        var point0 = new GLatLng(nPoint[0],nPoint[1]);
        var marker0 = new GMarker(point0,{title: "实际所在地点"});
        GlobalMap.addOverlay(marker0);
        
        var sInfo = '<b>经纬度信息:</b>' + response.name + '<br/>' +             
        '<b>最近地点信息:</b>' + place.address + '<br>' + 
        '<b>精确度:</b>' + place.AddressDetails.Accuracy + '<br>';
               
		//============= 4.0 显示地理信息 ====================
        marker0.openInfoWindowHtml(sInfo);
      }
}

