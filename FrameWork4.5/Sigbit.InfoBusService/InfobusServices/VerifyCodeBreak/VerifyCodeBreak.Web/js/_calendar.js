function drawcalendar(){
	document.write("<div id='rl_move' style='visibility:hidden;position:absolute;left:0px;top:0px;width:250px;height:160px'>")
	document.write("<TABLE WIDTH=100% BORDER=0 CELLSPACING=0 CELLPADDING=0 bgcolor=#dcdcdc class=sl>")
	document.write("<tr bgcolor=CornflowerBlue height='18'><td colspan=6><font color=white>日历</font></td>")
	document.write("<td align=right><INPUT type='button' value='╳' id=close style='border:0px;height:18px;width:18px;' onclick='cls();'></td></tr>")
	document.write("<TR align=center>")
	document.write("<TD class=sl><a href='javascript:' onclick='changeyear(-1)' style='cursor:hand'>▲</a></td>")
	document.write("<TD colspan=5 rowspan=2><font size=5 color=SeaGreen><span id='valyear'></span>年<span id='valmonth'></span>月</font></TD>")
	document.write("<TD class=sl><a href='javascript:' onclick='changemonth(-1)' style='cursor:hand'>▲</a></TD>")
	document.write("</TR>")
	document.write("<TR align=center valign=bottom>")
	document.write("<td class=sl><a href='javascript:' onclick='changeyear(1)'>▼</a></TD>")
	document.write("<td class=sl><a href='javascript:' onclick='changemonth(1)'>▼</a></td>")
	document.write("</tr>	<TR bgcolor=LimeGreen align=center>")
	document.write("<TD width=30px>日</TD><TD width=30px>一</TD><TD width=30px>二</TD><TD width=30px>三</TD><TD width=30px>四</TD><TD width=30px>五</TD><TD width=30px>六</TD>")
	document.write("</TR>")
var i=0;
while(i<37){
	if(i-Math.floor(i/7)*7==0){document.write("<TR align=center>")}
	document.write("<TD style='cursor:hand' id='d"+i+"' onclick='getvalue()' onmouseover='ffff("+i+")'></TD>")
	if(i-Math.floor(i/7)*7==6){document.write("</TR>")}
	i+=1
}
document.write("</TABLE></div>")}
drawcalendar();
var curdate=new Date();
var id;
var oldid="";
var ison;
var crdate=new Date();
var currY=crdate.getFullYear();
var currM=crdate.getMonth();
var currD=crdate.getDate();

function getweek(){
	var ye=curdate.getFullYear();
	var mo=curdate.getMonth();
	var dv=curdate.getDate();
	window.valyear.innerText=ye;
	window.valmonth.innerText=mo+1
	var day=1;
	var firstday=new Date(ye,mo,1);
	var lastday=new Date(ye,mo+1,0);
	var week=firstday.getDay();
	for( i=0;i<=6;i++){
		var obj=eval("d"+i)
		obj.innerText=""}
	for(i=28;i<=36;i++){
		var obj=eval("d"+i)
		obj.innerText=""}
	var i=0;
	while(firstday<=lastday){
		var j=7*i+week
		var obj=eval("d"+j)
		obj.innerText=day;
		if(currY==ye&&currM==mo&&currD==day&&document.all(id).value=='')
			{obj.style.backgroundColor="red";}
		else
			{obj.style.backgroundColor="#dcdcdc";}
		day+=1;
		firstday.setDate(day);
		week+=1;
		if(week>6){week=0;i+=1}}
}

function show(e){
	id=e;
	if(window.event!=null){
	var x=window.event.x
	var y=window.event.y
	x=x-100
	y=y+10
	var x1=document.body.clientWidth
	var y1=document.body.clientHeight
	if(x>x1-250){x=x1-250}
	if(y>y1-160){y=y1-160}
	x+=document.body.scrollLeft
	y+=document.body.scrollTop
	window.rl_move.style.left=x
	window.rl_move.style.top=y
	}else{
	window.rl_move.style.left=200
	window.rl_move.style.top=150}
	window.rl_move.style.visibility="visible"
	ison=true;
	var str=document.all(e).value
	if(str!=""){
		var pos=str.indexOf("-")
		var iy=Math.floor(str.substring(0,pos))
		//var iy=Math.floor(str.substring(0,4))
		var oldp=pos+1
		var pos=str.lastIndexOf("-")
		var im=Math.floor(str.substring(oldp,pos))
//		var gd=new Date(iy,im-1,1)
		if(iy>0 && im>0){
		curdate.setYear(iy)
		curdate.setMonth(im-1)
		curdate.setDate(1)}
		else{curdate=new Date()}
	}else{curdate=new Date()}
	getweek();
}

function changeyear(e){
	var ye=curdate.getFullYear()
	curdate.setYear(ye+e)
	getweek();}

function changemonth(e){
	var mo=curdate.getMonth();
	curdate.setMonth(mo+e);
	getweek();}

function getvalue(){
	var ye=curdate.getFullYear();
	var mo=curdate.getMonth()+1;
	var da=window.event.srcElement.innerText
	if(da!=""){
	   if(mo<10){mo="0"+mo;}
	   if(da<10){da="0"+da;}
	document.all(id).value=ye+"-"+mo+"-"+da;
	window.rl_move.style.visibility="hidden"
	ison=false}
	}

function ffff(e){
	if(oldid!=""){
	var obj=eval(oldid);
	obj.style.backgroundColor=""}
	oldid="d"+e;
	var obj=eval(oldid);
	obj.style.backgroundColor="#6495ed"
}
function losef(){
	if(ison){
	window.rl_move.style.visibility="hidden"
	ison=false}}
function cls(){window.rl_move.style.visibility="hidden";
	ison=false;}
