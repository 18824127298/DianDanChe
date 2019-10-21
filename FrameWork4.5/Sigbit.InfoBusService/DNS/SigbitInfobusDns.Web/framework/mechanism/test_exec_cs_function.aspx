<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test_exec_cs_function.aspx.cs" Inherits="framework_mechanism_test_exec_cs_function" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<script language="javascript" type="text/javascript">
<!--

function Button1_onclick() {
    var inpText = document.getElementById("edtInput").value;
    divShow.innerText = TCXMenuNavigator(inpText);
//    alert(inpText);
//    alert(TCXMenuNavigator(inpText));
}

// -->
</script>
</head>

    <script language="javascript" src="../../js/tcx_cs_function_calling.js"></script>
	<script language="javascript">
	<!--

    function TCXMenuNavigator(sMenuCode)
    {
        //==== 1. 调用C#的处理 ============
        var tmp = new TCXCSFunction();
        tmp.AsseblyName = "Sigbit.Framework.dll";
        tmp.TypeName = "Sigbit.Framework.Test.TestExecCSFunction";
        tmp.FunctionName = "DoTest";
        tmp.AddParameter( "string", sMenuCode);
        var rtn = tmp.Execute();
        tmp = null;
        
        return rtn;
    }
    
	//-->
	</script>


<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<input id="edtInput" type="text" value="ctrlpanel" name="edtInput" />
        <br />
        <input id="Button2" type="button" value="test" language="javascript" onclick="return Button1_onclick()" /><br />
        <div id="divShow" style="width: 100px; height: 100px">
        </div>
    </div>
    </form>
</body>
</html>
