		<input style="WIDTH: 88px; HEIGHT: 21px" type="hidden" size="9" value="0" name="PopMenuLayer_MouseStatus">
		<script language="javascript">
			var MenuHeight=20;
			function Object_OnContextMenu()
			{
				try{
					PopupMenu_OnDropDown();
				}catch(e){}
				
				if (document.all("PopMenuLayer").style.display == "none"){
				       
						if (event.ctrlKey) return true;
					//设定左边距离
					//alert("left:"+window.screenLeft+";width;"+document.body.scrollWidth+";x;"+event.screenX);
					if ( eval(window.screenLeft+document.body.scrollWidth - event.screenX) > 100 ){
						PopMenuLayer.style.left = event.screenX-window.screenLeft;
					}
					else{
						PopMenuLayer.style.left = event.screenX-window.screenLeft - 100;
					}
					//设定上边距离
					if ( eval(screen.availHeight - event.screenY - 30) > MenuHeight){
						PopMenuLayer.style.top  = event.screenY + document.body.scrollTop-window.screenTop; 
					}
					else{
						PopMenuLayer.style.top  = event.screenY - MenuHeight + document.body.scrollTop-window.screenTop; 
					}
					//将层置为可见
					PopMenuLayer.style.display = "block";
				}
				else{     
					PopMenuLayer.style.display = "none";
					document.all.PopMenuLayer_MouseStatus.value = '0';
				}  
				return false
			}
		</script>
		<script language="javascript">  
			function Object_OnmouseDown()
			{
				if (PopMenuLayer.style.display != "none" && document.all.PopMenuLayer_MouseStatus.value == "0")
					PopMenuLayer.style.display = "none";  
			}
		</script>
		<script language="javascript">
			
			var strLayerHead="";
			var strMenu="";
			var strLayerBottom="";
			strLayerHead=strLayerHead+"	<table onmousemove=\"javascript:document.all.PopMenuLayer_MouseStatus.value='1'\" onmouseout=\"javascript:document.all.PopMenuLayer_MouseStatus.value='0'\"";
			strLayerHead=strLayerHead+"		cellSpacing=\"2\" borderColorDark=\"#faf8f6\" cellPadding=\"1\" width=\"100%\" bgColor=\"#7ba5d0\"";
			strLayerHead=strLayerHead+"		borderColorLight=\"#c7d1e5\" border=\"0\">";
			
			strLayerBottom=strLayerBottom+"	</table>";
			

			function AddPopMenuItem(MenuName,onclick,MenuID)
			{
				strMenu=strMenu+"		<tr class=\"PopMenuTableRow\" onmouseover=\"javascript:this.className='PopMenuLightRow'\" ";
				if(MenuID!=null && MenuID!='')  strMenu=strMenu+"		id=\""+MenuID+"\" ";
				strMenu=strMenu+"			onclick=\"javascript:PopMenuLayer.style.display = 'none';"+onclick+"\" onmouseout=\"javascript:this.className='PopMenuTableRow'\">";
				strMenu=strMenu+"			<td nowrap>&nbsp;"+MenuName+"</td>";
				strMenu=strMenu+"		</tr>";
				MenuHeight=MenuHeight+20;			
			}
			
			function AddPopMenuSpliter()
			{
				strMenu=strMenu+"		<!--分区横线-->";
				strMenu=strMenu+"		<tr>";
				strMenu=strMenu+"			<td bgColor=\"#cdcdcd\" height=\"1\"></td>";
				strMenu=strMenu+"		</tr>";
				MenuHeight=MenuHeight+10;
			}
			
			function SetMenuItemVisible(MenuID,isVisible)
			{
				if(isVisible==true)
				   document.all(MenuID).style.display='block';
				else if(isVisible==false)
				   document.all(MenuID).style.display='none';
			}
			
			function DrawPopMenu(PopMenuAreaName)
			{
				document.write("<DIV class='PopMenuLayer' id='PopMenuLayer'></DIV>");
				if(PopMenuAreaName==null)
				{
					document.oncontextmenu=Object_OnContextMenu;
					document.onmousedown=Object_OnmouseDown;
				}
				else
				{
					document.all(PopMenuAreaName).oncontextmenu=Object_OnContextMenu;
					document.all(PopMenuAreaName).onmousedown=Object_OnmouseDown;
				}
				document.all("PopMenuLayer").innerHTML=strLayerHead+strMenu+strLayerBottom;
			}
			
		</script>