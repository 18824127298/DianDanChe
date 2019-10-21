	//打开关闭搜索条
	function ToggleSearchPanel()
	{
		if(document.all("tbSearchPanel").style.display=="block")
			document.all("tbSearchPanel").style.display="none";
		else
			document.all("tbSearchPanel").style.display="block";
	}
	
	//选择一个Grid里面全部的数据
	function jcomSelectAllRecords()
	{
	    jcomSelectAllRecords("", "");
    }
    
	function jcomSelectAllRecords(SelectAllName,SelectName)
	{
		if (!SelectAllName)
			SelectAllName="chkSelectAll";
		if (!SelectName)
			SelectName="chkSelect";
		if (document.all(SelectName)==null) 
		    return "";	
		    
		var checked=document.all(SelectAllName).checked;
		var sSelectedIDs="";
		if (typeof(document.all(SelectName).length) == 'undefined')
		{
			document.all(SelectName).checked=checked;
			sSelectedIDs=document.all(SelectName).value;
			return sSelectedIDs;
		}
		else
		{
			for (i=0; i < document.all(SelectName).length; i ++) 
			{
				document.all(SelectName)[i].checked=checked;
				sSelectedIDs=sSelectedIDs+document.all(SelectName).value+",";
			}
		}
		if(sSelectedIDs!="") sSelectedIDs=sSelectedIDs.substr(0,sSelectedIDs.length-1);
		return sSelectedIDs;
	}

	function jcomGetAllSelectedRecords(SelectName)
	{
		if(!SelectName)
			SelectName="chkSelect";
			
			
		if(document.all(SelectName)==null)
			return "";
			
		var sSelectIDs="";
		if(typeof(document.all(SelectName).length)=='undefined')
		{
			if (document.all(SelectName).checked == true){
				sSelectIDs=document.all(SelectName).value;
				return sSelectIDs;
			}
			else
			{
				return "";
			}
		}
		
		var nCount = document.all(SelectName).length;
		for (var i=0; i < nCount; i ++) 
		{
			if (document.all(SelectName)[i].checked == true){
				sSelectIDs=sSelectIDs+document.all(SelectName)[i].value+",";
			} 				
		}
		if (sSelectIDs!="")
		{
			sSelectIDs=sSelectIDs.substr(0,sSelectIDs.length-1);
		}
		return sSelectIDs;
	}
	
