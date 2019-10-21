<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gcmap_util_desc_input.aspx.cs"
    Inherits="module_gcmap_gcmap_util_desc_input" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>经纬度输入</title>
    <meta content="text/html; charset=UTF-8" http-equiv="content-type">

    <script type="text/javascript" language="javascript">
    var arrayDescription = new Array();
    
    function initialize() {
    if (GBrowserIsCompatible()) {                              
        //============== 1.0 要查找的经纬度信息 =============     
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
                    SendDescription (arrayDescription);
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
                var count = arrayDescription.length;
                if(count == nCount)
                {
                    SendDescription (arrayDescription);
                }  
          }
        }); 
    }
    
    function SendDescription (list)
    {
        var count = list.length;
       if(list.length <= 0)
       {                                                   
           window.location.href = "gcmap_util_desc_result.aspx?info=地理信息获取失败！";
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
              infos += escape(info);
           } 
       }
       window.location.href = "gcmap_util_desc_result.aspx?info=" + infos;
    }
        
    </script>

</head>
<body onload="initialize();">
    <form id="form1" runat="server">
        <div style="font-size: large; text-align: center; color: Red">
            您还没有传经纬度参数了！</div>
        <div id="error" style="font-size: large; text-align: center; color: Red">
            地点位置查看：</div>
        <div style="font-size: large; text-align: center; color: Red">
            <a href='gcmap_util_desc_look.aspx?point=<%=ViewState["init"] %>&type=all'>gcmap_util_desc_look.aspx?point=<%=ViewState["init"] %>&type=all</a></div>
        <div style="font-size: large; text-align: center; color: Red">
            <a href='gcmap_util_desc_look.aspx?point=<%=ViewState["init"] %>&type=one'>gcmap_util_desc_look.aspx?point=<%=ViewState["init"] %>&type=one</a></div>
        <div id="Div1" style="font-size: large; text-align: center; color: Red">
            地点位置信息获取：</div>
        <div style="font-size: large; text-align: center; color: Red">
            <a href='gcmap_util_desc_input.aspx?point=<%=ViewState["init"] %>&type=one'>gcmap_util_desc_input.aspx?point=<%=ViewState["init"] %>&type=one</a></div>
        <div style="font-size: large; text-align: center; color: Red">
            <a href='gcmap_util_desc_input.aspx?point=<%=ViewState["init"] %>&type=all'>gcmap_util_desc_input.aspx?point=<%=ViewState["init"] %>&type=all</a></div>
    </form>
</body>
</html>
