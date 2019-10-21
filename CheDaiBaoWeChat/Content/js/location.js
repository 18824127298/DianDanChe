function setCookie(name,value)
{
var Days = 30;
var exp = new Date();
exp.setTime(exp.getTime() + Days*24*60*60*1000);
document.cookie = name + "="+ escape (value) + ";expires=" + exp.toGMTString();
}

function getCookie(name)
{
var arr,reg=new RegExp("(^| )"+name+"=([^;]*)(;|$)");
if(arr=document.cookie.match(reg))
return unescape(arr[2]);
else
return null;
}

var geolocation = new BMap.Geolocation();
	geolocation.getCurrentPosition(function(r){
		if(this.getStatus() == BMAP_STATUS_SUCCESS){
			var mk = new BMap.Marker(r.point);
			var geoc = new BMap.Geocoder();    	  
		geoc.getLocation(r.point, function(rs){
			var addComp = rs.addressComponents;
			$(".dwcon2").html(addComp.city);
			setCookie("city",addComp.city.replace("市",""));
			$(".bpsite a").eq(0).attr("href","mylist.php?city="+addComp.city.replace("市",""));
			//$("#lt").attr("href","mylist.php?city="+addComp.city.replace("市",""));
		});         
		}
	      
	},{enableHighAccuracy: true})