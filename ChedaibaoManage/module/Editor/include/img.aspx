<%@ Page Language="C#" AutoEventWireup="true" CodeFile="img.aspx.cs" Inherits="Editor_include_img" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta http-equiv="Expires" content="0">
    <title>����ͼƬ</title>
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

document.write("<title>50507_com�๦�ܱ༭��--ͼƬ���ԣ�" + sTitle + "��</title>");

// ��ʼֵ
function InitDocument(){
	SearchSelectValue(document.form1.d_align, sAlign.toLowerCase());
	document.form1.d_fromurl.value = sFromUrl;
	document.form1.d_border.value = sBorder;
	document.form1.d_width.value = sWidth;
	document.form1.d_height.value = sHeight;
	showimages();
}

// ����������ֵ��ָ��ֵƥ�䣬��ѡ��ƥ����
function SearchSelectValue(o_Select, s_Value){
	for (var i=0;i<o_Select.length;i++){
		if (o_Select.options[i].value == s_Value){
			o_Select.selectedIndex = i;
			return true;
		}
	}
	return false;
}

// �����ڷ���ֵ
function ReturnValue(){
	sFromUrl = document.form1.d_fromurl.value;
	sBorder = document.form1.d_border.value;
	sAlign = document.form1.d_align.value;
	sWidth = document.form1.d_width.value;
	sHeight = document.form1.d_height.value;
	if (sAction == "MODI") {
		oControl.src = sFromUrl;
		oControl.alt = sAlt;
		oControl.border = sBorder;
		oControl.align = sAlign;
		oControl.width = sWidth;
		oControl.height = sHeight;
	}else{
		var sHTML = '<img src="'+sFromUrl+'" border="'+sBorder+'" align="'+sAlign+'"';
		if (sWidth!=""){
			sHTML=sHTML+' width="'+sWidth+'"';
		}
		if (sHeight!=""){
			sHTML=sHTML+' height="'+sHeight+'"';
		}
		sHTML = sHTML+'>';
	}
	if (sFromUrl.length<10){sHTML='';}
	window.returnValue = sHTML;
	window.close();
}

function showimages(){
	sFromUrl = document.form1.d_fromurl.value;
	sBorder = document.form1.d_border.value;
	sAlign = document.form1.d_align.value;
	sWidth = document.form1.d_width.value;
	sHeight = document.form1.d_height.value;
	var sHTML = '<img src="'+sFromUrl+'" border="'+sBorder+'" align="'+sAlign+'"';
	if (sWidth!=""){
		sHTML=sHTML+' width="'+sWidth+'"';
	}
	if (sHeight!=""){
		sHTML=sHTML+' height="'+sHeight+'"';
	}
	sHTML = sHTML+'>';
	if (sFromUrl.length<10){sHTML='';}
	document.getElementById('showimage').innerHTML = sHTML;
}

// ��ȷ��ʱִ��
function ok(){
	// �������������Ч��
	document.form1.d_border.value = ToInt(document.form1.d_border.value);
	document.form1.d_width.value = ToInt(document.form1.d_width.value);
	document.form1.d_height.value = ToInt(document.form1.d_height.value);
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
function showimg()
	{
		sFromUrl = document.form1.fupImage.value;	
	var sHTML = '<img width="200px" src="'+sFromUrl+'" ';	
	sHTML = sHTML+'>';
	if (sFromUrl.length<10){sHTML='';}
	document.getElementById('showimage').innerHTML = sHTML;
	
	}
    </script>

    <base target="_self">
</head>
<body bgcolor="menu" onload="InitDocument()">
    <form id="form1" method="post" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" align="center" width="97%">
            <tr>
                <td width="300" valign="top">
                    <fieldset>
                        <legend>ͼƬ��Դ</legend>
                        <table border="0" cellpadding="2" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="5" height="5">
                                </td>
                            </tr>
                            <tr>
                                <td width="2">
                                </td>
                                <td width="40" align="right">
                                    ����:</td>
                                <td width="5">
                                </td>
                                <td width="*">
                                    <input type="text" id="d_fromurl" style="width: 140px" size="30" value="http://"
                                        onkeyup="showimages()" maxlength="1000">
                                    <input type="submit" value='ȷ��' id="Ok" onclick="ok()" style="width: 30px" contenteditable="">&nbsp;<input
                                        type="button" value='ȡ��' onclick="window.close();" style="width: 30px"></td>
                                <td width="7">
                                </td>
                            </tr>
                            <tr>
                                <td width="2">
                                </td>
                                <td width="40" align="right">
                                    ����:</td>
                                <td width="5">
                                </td>
                                <td width="*">
                                    <%--<INPUT id="File1" onpropertychange="showimg()" style="WIDTH: 140px; HEIGHT: 18px" type="file"
											size="6" name="File1" runat="server">--%>
                                    <asp:FileUpload ID="fupImage" runat="server" onpropertychange="showimg()" Style="width: 140px;
                                        height: 18px" type="file" size="6" />
                                    <asp:Button ID="btnUpLoad" runat="server" Text="�ϴ�" Height="18px"></asp:Button></td>
                                <td width="7">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" height="5">
                                    <font face="����"></font>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <br>
                    <fieldset>
                        <legend>ͼƬ����</legend>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td colspan="8" height="5">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8" height="5">
                                </td>
                            </tr>
                            <tr>
                                <td width="2">
                                </td>
                                <td width="60" nowrap>
                                    �߿��ϸ:</td>
                                <td width="5">
                                </td>
                                <td width="2">
                                    <input type="text" id="d_border" size="10" onkeypress="event.returnValue=IsDigit();"
                                        onkeyup="showimages()" maxlength="4"></td>
                                <td width="7">
                                </td>
                                <td width="60" nowrap>
                                    ���뷽ʽ:</td>
                                <td width="5">
                                </td>
                                <td width="2">
                                    <select id="d_align" size="1" style="width: 72px" onchange="showimages()">
                                        <option value='' selected>Ĭ��</option>
                                        <option value='left'>����</option>
                                        <option value='right'>����</option>
                                        <option value='top'>����</option>
                                        <option value='middle'>�в�</option>
                                        <option value='bottom'>�ײ�</option>
                                        <option value='absmiddle'>���Ծ���</option>
                                        <option value='absbottom'>���Եײ�</option>
                                        <option value='baseline'>����</option>
                                        <option value='texttop'>�ı�����</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="9" height="5">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td width="60" nowrap>
                                    ͼƬ����:</td>
                                <td>
                                </td>
                                <td width="2">
                                    <input type="text" id="d_width" size="10" onkeypress="event.returnValue=IsDigit();"
                                        onkeyup="showimages()" maxlength="4"></td>
                                <td>
                                </td>
                                <td width="60" nowrap>
                                    ͼƬ�߶�:</td>
                                <td>
                                </td>
                                <td width="2">
                                    <input type="text" id="d_height" size="10" onkeypress="event.returnValue=IsDigit();"
                                        onkeyup="showimages()" maxlength="4"></td>
                            </tr>
                            <tr>
                                <td colspan="8" height="5">
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
                <td valign="top">
                    <fieldset>
                        <legend>ͼƬԤ��</legend>
                        <table border="0" cellpadding="0" cellspacing="0" height="100%">
                            <tr>
                                <td height="5">
                                </td>
                            </tr>
                            <tr>
                                <td height="*" id="showimage">
                                </td>
                            </tr>
                            <tr>
                                <td height="5">
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>