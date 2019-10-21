function chgbuttonimg(img,opid)
{
	var sre=window.event.srcElement;
	if(img!='') sre.style.backgroundImage="url(/images/"+img+")";
	switch(opid)
	{
		case 0:
			sre.className="buttonnormal";
			break
		case 1:
			sre.className="buttonover";
			break;
		case 2:
			sre.className="buttondown"
	}
}

function cboselitem(cboname,selval)
{
	for(i=0;i<document.all(cboname).length;i++)
	{
		var stritem=document.all(cboname).item(i).value;
		if(stritem.toString()==selval.toString())
		{
			document.all(cboname).selectedIndex=i;
			break;
		}
	}
}

//定义一个弹出无边窗口的函数，能数意义见下面“参数说明”，实际使用见最后的实例。
function noBorderWin(fileName,w,h,titleBg,moveBg,titleColor,titleWord,scr)  
/*
------------------参数说明-------------------
fileName   ：无边窗口中显示的文件。
w　　　　   ：窗口的宽度。
h　　　　   ：窗口的高度。
titleBg    ：窗口“标题栏”的背景色以及窗口边框颜色。
moveBg     ：窗口拖动时“标题栏”的背景色以及窗口边框颜色。
titleColor ：窗口“标题栏”文字的颜色。
titleWord  ：窗口“标题栏”的文字。
scr        ：是否出现滚动条。取值yes/no或者1/0。
--------------------------------------------
*/
{
w=screen.width;
h=screen.height;
  var contents="<html>"+
               "<head>"+
	    	   "<title>"+titleWord+"</title>"+
			   "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">"+
			   "<object id=hhctrl type='application/x-oleobject' classid='clsid:adb880a6-d8ff-11cf-9377-00aa003b7a11'><param name='Command' value='minimize'></object>"+
			   "</head>"+
               "<body topmargin=0 leftmargin=0 scroll=no onselectstart='return false' ondragstart='return false'>"+
			   "  <table height=100% width=100% cellpadding=0 cellspacing=1 bgcolor="+titleBg+" id=mainTab>"+
			   "    <tr height=18 style=cursor:default; onmousedown='x=event.x;y=event.y;setCapture();mainTab.bgColor=\""+moveBg+"\";' onmouseup='releaseCapture();mainTab.bgColor=\""+titleBg+"\";' onmousemove='if(event.button==1)self.moveTo(screenLeft+event.x-x,screenTop+event.y-y);'>"+
			   "      <td width=18 align=center><img height=12 width=12 border=0 src=images/win-icon.gif></td>"+
			   "      <td width="+w+"><span style=font-size:12px;color:"+titleColor+";font-family:宋体;position:relative;top:1px;>"+titleWord+"</span></td>"+
			   "      <td width=14><img border=0 width=12 height=12 alt=最小化 src=images/win-minimize.gif onmousedown=hhctrl.Click(); onmouseover=this.src='images/win-minimize2.gif' onmouseout=this.src='images/win-minimize.gif'></td>"+
			   "      <td width=13><img border=0 width=12 height=12 alt=关闭 src=images/win-close.gif onmousedown='frmwclose.submit();self.close();' onmouseover=this.src='images/win-close2.gif' onmouseout=this.src='images/win-close.gif'></td>"+
			   "    </tr>"+
			   "    <tr height=*>"+
			   "      <td colspan=4>"+
			   "        <iframe name=nbw_v6_iframe src="+fileName+" scrolling="+scr+" width=100% height=100% frameborder=0></iframe>"+
			   "      </td>"+
			   "    </tr>"+
			   "  </table>"+
			   "  <FORM action='module/logout.asp' method='POST' name='frmwclose'><INPUT type='hidden' name='txtaction' value='windowclose'></FORM>"+
			   "</body>"+
			   "</html>";

  pop=window.open("","_blank","fullscreen=yes");
  pop.resizeTo(w,h);
  pop.moveTo((screen.width-w)/2,(screen.height-h)/2);
  pop.document.writeln(contents);

  if(pop.document.body.clientWidth!=w||pop.document.body.clientHeight!=h)  //如果无边窗口不是出现在纯粹的IE窗口中
  {
    temp=window.open("","nbw_v6");
	temp.close();
	window.showModalDialog("about:<"+"script language=javascript>window.open('','nbw_v6','fullscreen=yes');window.close();"+"</"+"script>","","dialogWidth:0px;dialogHeight:0px");
	pop2=window.open("","nbw_v6");
    pop2.resizeTo(w,h);
    pop2.moveTo((screen.width-w)/2,(screen.height-h)/2);
    pop2.document.writeln(contents);
	pop.close();
  }
}