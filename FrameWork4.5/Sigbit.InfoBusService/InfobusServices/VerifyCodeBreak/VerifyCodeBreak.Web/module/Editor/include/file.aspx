<%@ Page Language="C#" AutoEventWireup="true" CodeFile="file.aspx.cs" Inherits="module_Editor_include_file" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta http-equiv="Expires" content="0">
    <link rel="stylesheet" type="text/css" href="pop.css">

    <script language="JavaScript">
var sAction = "INSERT";
var sTitle = "����";
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
		sTitle = "�޸�";
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


// ��ʼֵ
function InitDocument(){
	//SearchSelectValue(document.form1.d_align, sAlign.toLowerCase());
	document.form1.d_fromurl.value = sFromUrl;
	document.form1.d_border.value = sBorder;
	document.form1.d_width.value = sWidth;
	document.form1.d_height.value = sHeight;
	//showimages();
}

// ����������ֵ��ָ��ֵƥ�䣬��ѡ��ƥ����
//function SearchSelectValue(o_Select, s_Value){
//	for (var i=0;i<o_Select.length;i++){
//		if (o_Select.options[i].value == s_Value){
//			o_Select.selectedIndex = i;
//			return true;
//		}
//	}
//	return false;
//}

// �����ڷ���ֵ
function ReturnValue()
{
    var sFileDesc=document.form1.edtFileDesc.value;
    if(sFileDesc.length<=0)
    {
        alert("�����븽������!");
        return;
    }
	sFromUrl = document.form1.edtFromUrl.value;
	if(sFromUrl.length<=10)
	{
	    alert("��������Ч������Դ!");
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


// ��ȷ��ʱִ��
function ok(){
	// �������������Ч��
//	document.form1.d_border.value = ToInt(document.form1.d_border.value);
//	document.form1.d_width.value = ToInt(document.form1.d_width.value);
//	document.form1.d_height.value = ToInt(document.form1.d_height.value);
	ReturnValue();
}
// �Ƿ���Ч��ɫֵ
function IsColor(color){
	var temp=color;
	if (temp=="") return true;
	if (temp.length!=7) return false;
	return (temp.search(/\#[a-fA-F0-9]{6}/) != -1);
}
// תΪ�����ͣ�����ǰ��0������ת�򷵻�""
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
// תΪ�����ͣ�����ǰ��0������ת�򷵻�""
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
// ������Ϣ��ʾ���õ����㲢ѡ��
function BaseAlert(theText,notice){
	alert(notice);
	theText.focus();
	theText.select();
	return false;
}
// ֻ������������
function IsDigit(){
  return ((event.keyCode >= 48) && (event.keyCode <= 57));
}
// ѡ��ɫ
function SelectColor(what){
	var dEL = document.all("d_"+what);
	var sEL = document.all("s_"+what);
	var arr = showModalDialog("selcolor.html", "", "dialogWidth:18.5em; dialogHeight:17.5em; status:0; help:0");
	if (arr) {
		dEL.value=arr;
		sEL.style.backgroundColor=arr;
	}
}
// ȥ�ո�left,right,all��ѡ
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
                        <legend>�ϴ�����</legend>
                        <table border="0" cellpadding="2" cellspacing="0" style="width: 95%">
                            <tr>
                                <td width="2" style="height: 14px">
                                </td>
                                <td align="right" style="width: 66px; height: 14px;">
                                    ��������:</td>
                                <td colspan="3" style="width: 344px">
                                    <asp:TextBox ID="edtFileDesc" Text="��������" runat="server" Width="302px" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td width="2" style="height: 14px">
                                </td>
                                <td align="right" style="width: 66px; height: 14px;">
                                    ������Դ:</td>
                                <td colspan="3" style="width: 344px">
                                    <asp:TextBox ID="edtFromUrl" runat="server" Width="302px" Text="http://www." />
                                   
                                </td>
                                <td> <input type="submit" value='ȷ��' onclick="ok()" style="width: 35px" />&nbsp;&nbsp;
                                    <input type="button" value='ȡ��' onclick="window.close();" style="width: 35px"></td>
                            </tr>
                            <tr>
                                <td width="2">
                                </td>
                                <td align="right" style="width: 66px;">
                                    �����ļ�:</td>
                                <td width="*" style="width: 344px;" colspan="3">
                                    &nbsp;&nbsp;
                                    <asp:FileUpload ID="fuUpload" runat="server" Width="299px" />
                                    </td>
                                    <td><asp:Button ID="btnUpLoad" runat="server" Text="�ϴ�" Width="65px" OnClick="btnUpLoad_Click">
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
