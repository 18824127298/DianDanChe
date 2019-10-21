/*
|-------------------------------------------------
|  作者：马先光，QQ：83629801
|  邮箱： accp2002@163.com，MSN：maxianguang2005@hotmail.com
|-------------------------------------------------

main:mothod
	<SCRIPT LANGUAGE="JavaScript">
	<!--
		Editor(document.form1.content.value);
	//-->
	</SCRIPT>
*/

var RootPath="";


function Editor(content,path)
{
    //将参数path送给根路径RootPath
    RootPath=path;
    
   	document.write('<iframe name="wrEditor" id="wrEditor" width="700" height="250" src="about:blank"></iframe>');
	oEditor = document.wrEditor;
	var strHtml = '<html><style>body{font-size:14px;line-height: 20px; margin:2px;}\ntd, a{color:#0000FF; font-size:14px;}</style><body>'+content+'</body></html>';
	oEditor.document.open();
	oEditor.document.write(strHtml);
	oEditor.document.close();

	oEditor.document.designMode="On";
	oEditor.focus();
}
//文字加粗
function bold(){
	var sText = oEditor.document.selection.createRange();
	if(sText!=""){
		oEditor.document.execCommand("bold");
	}
}
//倾斜
function italic(){
	var sText = oEditor.document.selection.createRange();
	if(sText!=""){
		oEditor.document.execCommand("italic");
	}
}
//下划线
function underline(){
	var sText = oEditor.document.selection.createRange();
	if(sText!=""){
		oEditor.document.execCommand("underline");
	}
}
//超链接
function url(){
	var sText = oEditor.document.selection.createRange();
	if(sText!=""){
		oEditor.document.execCommand("createLink");
		oEditor.document.execCommand("ForeColor", "false", "#FF0000");
	}
}
//取消链接
function unurl(){
	var sText = oEditor.document.selection.createRange();
	if(sText!=""){
		oEditor.document.execCommand("unlink");
	}	
}
//插入图片
function image(url){
    
	var arr = showModalDialog(RootPath+"Editor/include/img.aspx?path="+url, window, "dialogWidth:600px; dialogHeight:230px; status:0; help:0");
	if (arr)
	{
		oEditor.document.body.innerHTML+=arr;
	}
	oEditor.focus();
}

//上传Bt文件
function bt(){
	var arr = showModalDialog(RootPath+"Editor/include/bt.aspx", window, "dialogWidth:600px; dialogHeight:200px; status:0; help:0");
	if (arr)
	{
		oEditor.document.body.innerHTML+=arr;
	}
	oEditor.focus();
}

//插入Flash
function flash(){
	var arr = showModalDialog(RootPath+"Editor/include/swf.htm", "", "dialogWidth:400px; dialogHeight:180px; status:0; help:0");
	if (arr != null)
	{
		var ss;
		ss=arr.split("*");
		path=ss[0];
		width=ss[1];
		height=ss[2];
		var string;
		string="<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B  8-444553540000\"  codebase=\"\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0\"\" width=\""+width+"\" height=\""+height+"\"><param name=\"movie\" value=\""+path+"\"><param name=\"quality\" value=\"high\"><embed src=\""+path+"\" pluginspage=\"\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\"\" type=\"\"application/x-shockwave-flash\"\" width=\""+width+"\" height=\""+height+"\"></embed></object>"
		//string="[flash="+row+","+col+"]"+path+"[/flash]";
		oEditor.document.body.innerHTML+=string;
	}
	oEditor.focus();
}

//插入 MediaPlayer 播放器
function wmv(){    
	var arr = showModalDialog(RootPath+"Editor/include/wmv.htm", window, "dialogWidth:400px; dialogHeight:220px; status:0; help:0");
	if (arr != null)
	{
		var ss;
		ss=arr.split("*");
		path=ss[0];
		autostart=ss[1];		
		var string;
		//string="<OBJECT id=\"\"preListener\"\" codeBase=\"\"http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701\"\" type=\"application/x-oleobject\" height=\"0\" standby=\"\"加载 Microsoft Windows Media Player 组件...\"\" width=\"0\" classid=\"\"CLSID:22D6F312-B0F6-11D0-94AB-0080C74C7E95\"\" name=\"wmplayer\" VIEWASTEXT><param name=\"ShowStatusBar\" value=\"false\"><param name=\"movie\" value=\""+path+"\"><param name=\"quality\" value=\"high\"><embed src=\""+path+"\" pluginspage=\"\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\"\" type=\"application/x-shockwave-flash\" width=\""+width+"\" height=\""+height+"\"></embed></object>"
		//string="[wmv="+ width +","+ height +","+ autostart +"]"+ path +"[/wmv]";
		string  = "<EMBED align=middle src="+path+" width=180 height=40 type=audio/mpeg loop=\"true\" autostart=\"true\">";
		oEditor.document.body.innerHTML+=string;
	}
	oEditor.focus();
}
//插入 RealPlayer 播放器
function rm(){
    alert('功能未完善，暂不开放！');
//	var arr = showModalDialog("Editor/include/rm.htm", window, "dialogWidth:400px; dialogHeight:220px; status:0; help:0");
//	if (arr != null)
//	{
//		var ss;
//		ss=arr.split("*");
//		path=ss[0];
//		autostart=ss[3];
//		width=ss[1];
//		height=ss[2];
//		var string;
//		string = "<OBJECT ID=\"video1\" CLASSID=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\" HEIGHT=\""+height+"\" WIDTH=\""+width+"\"><param name=\"_ExtentX\" value=\"9313\"><param name=\"_ExtentY\" value=\"7620\"><param name=\"AUTOSTART\" value=\""+autostart+"\"><param name=\"SHUFFLE\" value=\"0\"><param name=\"PREFETCH\" value=\"0\"><param name=\"NOLABELS\" value=\"0\"><param name=\"SRC\" value=\""+path+"\"><param name=\"CONTROLS\" value=\"ImageWindow\"><param name=\"CONSOLE\" value=\"Clip1\"><param name=\"LOOP\" value=\"0\"><param name=\"NUMLOOP\" value=\"0\"><param name=\"CENTER\" value=\"0\"><param name=\"MAINTAINASPECT\" value=\"0\"><param name=\"BACKGROUNDCOLOR\" value=\"#000000\"><embed SRC type=\"\"audio/x-pn-realaudio-plugin\"\" CONSOLE=\"Clip1\" CONTROLS=\"ImageWindow\" HEIGHT=\""+height+"\" WIDTH=\""+width+"\" AUTOSTART=\""+autostart+"\"></OBJECT>"
//		string +="已插入播放器...<br>";
//		//string="[rm="+ width +","+ height +","+ autostart +"]"+ path +"[/rm]";
//		oEditor.document.body.innerHTML+=string;
//	}
//	oEditor.focus();
}
/*
参数：left/center/right
*/
function ralign(aStr){
	switch(aStr){
	case "left":
		oEditor.document.execCommand("JustifyLeft");
		break;
	case "center":
		oEditor.document.execCommand("JustifyCenter");
		break;
	case "right":
		oEditor.document.execCommand("JustifyRight");
		break;
	default:
		return false;
	}
}
/*
字体颜色
*/
function FontColor(){
	var arr = showModalDialog(RootPath+"Editor/include/selcolor.htm", window, "dialogWidth:300px; dialogHeight:300px; status:0; help:0");
	if (arr)
	{
		var sText = oEditor.document.selection.createRange();
		if(sText){
			oEditor.document.execCommand("ForeColor", "false", arr);
		}
	}
	oEditor.focus();
}
/*
字体背景颜色
*/
function BackColor(){
	var arr = showModalDialog(RootPath+"Editor/include/selcolor.htm", window, "dialogWidth:300px; dialogHeight:300px; status:0; help:0");
	if (arr)
	{
		var sText = oEditor.document.selection.createRange();
		if(sText){
			oEditor.document.execCommand("BackColor", "false", arr);
		}
	}
	oEditor.focus();
}
/*

*/
function FontSize(value){
	var sText = oEditor.document.selection.createRange();
	if(sText){
		oEditor.document.execCommand("FontSize", "false", value);
	}	
}
/*

*/

function FontName(value){
	var sText = oEditor.document.selection.createRange();
	if(sText){
		oEditor.document.execCommand("FontName", "false", value);
	}	
}
//取消格式
function unformat(){
	var sText = oEditor.document.selection.createRange();
	if(sText){
		oEditor.document.execCommand("RemoveFormat", false, "");
	}	
}

function getContent()
{
	//return correctUrl(oEditor.document.body.innerHTML);
	//alert("GetContent:"+oEditor.document.body.innerHTML);
	return oEditor.document.body.innerHTML;
}

function setContent(str)
{
    //alert("SetContent:"+str);
    oEditor.document.body.innerHTML=str;
}

function correctUrl(cont)
{
	var regExp;
	regExp = /<a([^>]*) href\s*=\s*([^\s|>]*)([^>]*)/gi
	cont = cont.replace(regExp, "<a href=$2 target=\"_blank\"");
	regExp = /<a([^>]*)><\/a>/gi
	cont = cont.replace(regExp, "");
	return cont;
}

