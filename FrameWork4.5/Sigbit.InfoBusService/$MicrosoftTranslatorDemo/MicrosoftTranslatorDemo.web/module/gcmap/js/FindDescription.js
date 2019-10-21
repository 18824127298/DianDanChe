//================= FindDescriptionControl 这是一个 GControl 地图工具栏控件 ==================
//**** 可以显示一个按钮。（获取您点击点的地点信息）***************************
//====================================================================================

var GlobalMap = null;
var GlobalDomMap = null;

//================ 1.0 初始获取地点信息控件 ========= 
function FindDescriptionControl() { }   
FindDescriptionControl.prototype = new GControl();    
FindDescriptionControl.prototype.initialize = function() {  
   this.name = "获取地点信息";
   this.map = null;   
   this.descriptionClickEvent = null;
}

//================ 1.2 初始控件图片事件 ========= 
FindDescriptionControl.prototype.initi = function(map) {   
     this.map = map;
     GlobalMap = map;
                              
    //============= 1.2.1 清除围栏信息 ==============
    var descriptionImgDiv = document.createElement("div");   
    descriptionImgDiv.style.backgroundImage = "url(./images/findb.gif)";     
    descriptionImgDiv.title = "获取地点信息";   
    descriptionImgDiv.tools = this;
    GEvent.addDomListener(descriptionImgDiv, "click", this.descriptionImgClick);
                                                         
    return descriptionImgDiv;
}
  
//================= 2.0 给地图单击添加一个获取信息事件 ===============  
FindDescriptionControl.prototype.descriptionImgClick = function() {
    var map = this.tools.map;  
    GEvent.clearInstanceListeners(map);  
    GlobalMap.clearOverlays();
        
	var listenerClick1 = GEvent.bind(map, "click", this.tools, this.tools.getAddress); 
	this.tools.descriptionClickEvent = listenerClick1; 
	this.tools.geocoder = new GClientGeocoder(); 
}
                
//============== 3.0 地图单击事件 =================
FindDescriptionControl.prototype.getAddress = function(overlay, latlng) {
      if (latlng != null) {
        this.address = latlng;
        this.geocoder.getLocations(latlng, this.showAddress);
      }
}
            
//============== 4.0 地图单击后回调 =================
FindDescriptionControl.prototype.showAddress = function(response) { 
    GlobalMap.clearOverlays();
                             
    var nPoint = response.name.split(',');
    var point0 = new GLatLng(nPoint[0],nPoint[1]);    
    var marker0 = new GMarker(point0,{title: "实际所在地点"});
    GlobalMap.addOverlay(marker0);
    
    if (!response || response.Status.code != 200) {  
		//alert("没有找到经纬度为" + response.name + "的地理信息");
    } else {
        var place = response.Placemark[0];                    
		//============= 2.0 添加实际点标 ====================         
        try{                                 
            
            var sInfo = '<b>经纬度信息:</b>' + response.name + '<br/>' + 
            //'<b>最近地点经纬度信息:</b>' + place.Point.coordinates[1] + "," + place.Point.coordinates[0] + '<br>' +
            //'<b>状态代码:</b>' + response.Status.code + '<br>' +
            //'<b>请求状态:</b>' + response.Status.request + '<br>' +
            //'<b>国家代码:</b> ' + place.AddressDetails.Country.CountryNameCode +
            '<b>最近地点信息:</b>' + place.address + '<br>' + 
            '<b>精确度:</b>' + place.AddressDetails.Accuracy + '<br>';
                    
            //============== 3.0 给地标添加事件 ==================
            GEvent.addListener(marker0, "click", function() {
                marker0.openInfoWindowHtml(sInfo);
            }); 

		    //============= 4.0 显示地理信息 ====================
            marker0.openInfoWindowHtml(sInfo);
          }catch(error){}
      }
}

 