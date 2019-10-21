<%@ Page Language="C#" AutoEventWireup="true" CodeFile="file.aspx.cs" Inherits="module_Editor_include_file" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta http-equiv="Expires" content="0">
    <link rel="stylesheet" type="text/css" href="pop.css">

    <script language="JavaScript">
var sAction = "INSERT";
var sTitle = "插入";
var oControl;
var oSeletion;
var sRangeType;
var sFromUrl = "http://";
var sAlt = "";
var sBorder = "0";
var sBorderColor = "#000000";
var sFilter = "";
var sAlign = "";
var sWidth = "";
var sHeight = "";
var sVSpace = "";
var sHSpace = "";
var sCheckFlag = "file";

oSelection = dialogArguments.oEditor.document.selection.createRange();
sRangeType = dialogArguments.oEditor.document.selection.type;

if (sRangeType == "Control") {
	if (oSelection.item(0).tagName == "IMG"){
		sAction = "MODI";
		sTitle = "修改";
		sCheckFlag = "url";
		oControl = oSelection.item(0);
		sFromUrl = oControl.src;
		sAlt = oControl.alt;
		sBorder = oControl.border;
		sAlign = oControl.align;
		sWidth = oControl.width;
		sHeight = oControl.height;
	}
}


// 初始值
function InitDocument(){
	//SearchSelectValue(document.form1.d_align, sAlign.toLowerCase());
	document.form1.d_fromurl.value = sFromUrl;
	document.form1.d_border.value = sBorder;
	document.form1.d_width.value = sWidth;
	document.form1.d_height.value = sHeight;
	//showimages();
}

// 搜索下拉框值与指定值匹配，并选择匹配项
//function SearchSelectValue(o_Select, s_Value){
//	for (var i=0;i<o_Select.length;i++){
//		if (o_Select.options[i].value == s_Value){
//			o_Select.selectedIndex = i;
//			return true;
//		}
//	}
//	return false;
//}

// 本窗口返回值
function ReturnValue()
{
    var sFileDesc=document.form1.edtFileDesc.value;
    if(sFileDesc.length<=0)
    {
        alert("请输入附件描述!");
        return;
    }
	sFromUrl = document.form1.edtFromUrl.value;
	if(sFromUrl.length<=10)
	{
	    alert("请输入有效网络资源!");
	    return;
	}
//	sBorder = document.form1.d_border.value;
//	sAlign = document.form1.d_align.value;
//	sWidth = document.form1.d_width.value;
//	sHeight = document.form1.d_height.value;
//	if (sAction == "MODI") {
//		oControl.src = sFromUrl;
//		oControl.alt = sAlt;
//		oControl.border = sBorder;
//		oControl.align = sAlign;
//		oControl.width = sWidth;
//		oControl.height = sHeight;
//	}else{
//		var sHTML = '<img src="'+sFromUrl+'" border="'+sBorder+'" align="'+sAlign+'"';
//		if (sWidth!=""){
//			sHTML=sHTML+' width="'+sWidth+'"';
//		}
//		if (sHeight!=""){
//			sHTML=sHTML+' height="'+sHeight+'"';
//		}
//		sHTML = sHTML+'>';
//	}
    var sHTML='<a href="'+sFromUrl+'" target="_blank">'+sFileDesc+'</a>';
	if(sHTML.length<10)
	{
	    sHTML='';
	}
	window.returnValue = sHTML;
	//alert(sHTML);
	window.close();
}


// 点确定时执行
function ok(){
	// 数字型输入的有效性
//	document.form1.d_border.value = ToInt(document.form1.d_border.value);
//	document.form1.d_width.value = ToInt(document.form1.d_width.value);
//	document.form1.d_height.value = ToInt(document.form1.d_height.value);
	ReturnValue();
}
// 是否有效颜色值
function IsColor(color){
	var temp=color;
	if (temp=="") return true;
	if (temp.length!=7) return false;
	return (temp.search(/\#[a-fA-F0-9]{6}/) != -1);
}
// 转为数字型，并无前导0，不能转则返回""
function ToInt(str){
	str=BaseTrim(str);
	if (str!=""){
		var sTemp=parseFloat(str);
		if (isNaN(sTemp)){
			str="";
		}else{
			str=sTemp;
		}
	}
	return str;
}
// 转为数字型，并无前导0，不能转则返回""
function ToInt(str){
	str=BaseTrim(str);
	if (str!=""){
		var sTemp=parseFloat(str);
		if (isNaN(sTemp)){
			str="";
		}else{
			str=sTemp;
		}
	}
	return str;
}
// 基本信息提示，得到焦点并选定
function BaseAlert(theText,notice){
	alert(notice);
	theText.focus();
	theText.select();
	return false;
}
// 只允许输入数字
function IsDigit(){
  return ((event.keyCode >= 48) && (event.keyCode <= 57));
}
// 选颜色
function SelectColor(what){
	var dEL = document.all("d_"+what);
	var sEL = document.all("s_"+what);
	var arr = showModalDialog("selcolor.html", "", "dialogWidth:18.5em; dialogHeight:17.5em; status:0; help:0");
	if (arr) {
		dEL.value=arr;
		sEL.style.backgroundColor=arr;
	}
}
// 去空格，left,right,all可选
function BaseTrim(str){
	  lIdx=0;rIdx=str.length;
	  if (BaseTrim.arguments.length==2)
	    act=BaseTrim.arguments[1].toLowerCase()
	  else
	    act="all"
      for(var i=0;i<str.length;i++){
	  	thelStr=str.substring(lIdx,lIdx+1)
		therStr=str.substring(rIdx,rIdx-1)
        if ((act=="all" || act=="left") && thelStr==" "){
			lIdx++
        }
        if ((act=="all" || act=="right") && therStr==" "){
			rIdx--
        }
      }
	  str=str.slice(lIdx,rIdx)
      return str
}

    </script>

    <base target="_self">
    </base>
</head>
<body bgcolor="menu">
    <form id="form1" method="post" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" align="center" width="97%">
            <tr>
                <td valign="top" style="width: 280px; height: 152px;">
                    &nbsp;<fieldset style="width: 567px">
                        <legend>上传附件</legend>
                        <table border="0" cellpadding="2" cellspacing="0" style="width: 95%">
                            <tr>
                                <td width="2" style="height: 14px">
                                </td>
                                <td align="right" style="width: 66px; height: 14px;">
                                    附件描述:</td>
                                <td colspan="3" style="width: 344px">
                                    <asp:TextBox ID="edtFileDesc" Text="附件下载" runat="server" Width="302px" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td width="2" style="height: 14px">
                                </td>
                                <td align="right" style="width: 66px; height: 14px;">
                                    网络资源:</td>
                                <td colspan="3" style="width: 344px">
                                    <asp:TextBox ID="edtFromUrl" runat="server" Width="302px" Text="http://www." />
                                   
                                </td>
                                <td> <input type="submit" value='确定' onclick="ok()" style="width: 35px" />&nbsp;&nbsp;
                                    <input type="button" value='取消' onclick="window.close();" style="width: 35px"></td>
                            </tr>
                            <tr>
                                <td width="2">
                                </td>
                                <td align="right" style="width: 66px;">
                                    本地文件:</td>
                                <td width="*" style="width: 344px;" colspan="3">
                                    &nbsp;&nbsp;
                                    <asp:FileUpload ID="fuUpload" runat="server" Width="299px" />
                                    </td>
                                    <td><asp:Button ID="btnUpLoad" runat="server" Text="上传" Width="65px" OnClick="btnUpLoad_Click">
                                    </asp:Button></td>
                            </tr>
                        </table>
                    </fieldset>
                    <br>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
