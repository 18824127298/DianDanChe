
function jcomSelectDateOrTime(sSourceControlName,nIndex)
{
    jcomOpenCalenderWithTime("select",sSourceControlName,nIndex);
}

function jcomOpenCalenderWithTime(HasTime,sSourceControlName,nIndex)
{
	if(nIndex==null) nIndex=0;
	var sOldDate;

	if(typeof(document.all(sSourceControlName)[nIndex])!='undefined')
		sOldDate=document.all(sSourceControlName)[nIndex].value;
	else
		sOldDate=document.all(sSourceControlName).value;
	var parameter=new Object();
	parameter.hasTime=HasTime;
	parameter.oldValue=sOldDate;
	
	var height=240;
	if(HasTime=="no") height=220;
	
	var strNode=showModalDialog('../../module/calendar.aspx',parameter,"dialogWidth:380px;dialogHeight:"+height+"px;status:no;scrollbars=no");
	//var strNode=window.open('../../module/calendar.aspx',0,"dialogWidth:320px;dialogHeight:185px;status:no");
	if (strNode!=-1 && typeof(strNode)!='undefined')
	{
		if(typeof(document.all(sSourceControlName)[nIndex])!='undefined')
			document.all(sSourceControlName)[nIndex].value=strNode;
		else
			document.all(sSourceControlName).value=strNode;
	}
}
