<%@ Page Language="C#" AutoEventWireup="true" CodeFile="have_a_rest.aspx.cs" Inherits="ibx_voice_reg_manual_input_reg_result_have_a_rest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <script language="javascript" src="../../js/tcx_cs_function_calling.js"></script>

    <script language="javascript">

function check_reg_rec()
{
        var tmp = new TCXCSFunction();
        tmp.AsseblyName = "Sigbit.App.Net.IBXService.VoiceQAN.Service.dll";
        tmp.TypeName = "Sigbit.App.Net.IBXService.VoiceQAN.Service.CheckNewRequest__ForJS.CheckNewRequest__ForJS";
        tmp.FunctionName = "DoCheck";
        var rtn = tmp.Execute();
        tmp = null;
        
        return rtn;
//    var tmp = new TCXCSFunction();
//    tmp.AsseblyName = "Sigbit.Framework.dll";
//    tmp.TypeName = "Sigbit.Framework.LoginLogger__ForJS";
//    tmp.FunctionName = "HeartBeat";
//    tmp.Execute();
//    tmp = null;
}
    </script>

    <SCRIPT language=JavaScript>

//------------ 显示服务器的当前时间 ------------
var OA_TIME = new Date();
var OA_DATE = new Date();

var heatbeat_seconds_ellapsed = 0;

function timeview()
{
  datestr = OA_DATE.toLocaleDateString();
  datestr = datestr.substr(datestr.indexOf("")); 

  timestr=OA_TIME.toLocaleString();
  timestr=timestr.substr(timestr.indexOf(" "));
  time_area.innerHTML = datestr + "  " + timestr;
  
  heatbeat_seconds_ellapsed ++;
  if (heatbeat_seconds_ellapsed >= 5)
  {
    time_area.innerHTML = "<font color='red'>◎</font>" + time_area.innerHTML;
    heatbeat_seconds_ellapsed = 0;
    chkResult = check_reg_rec();
    time_area.innerHTML = chkResult;
    
    if (chkResult == "true")
        location="new_request_notice.aspx";
  }
  
  OA_TIME.setSeconds(OA_TIME.getSeconds()+1);
  window.setTimeout( "timeview()", 1000 );
}

</SCRIPT>
</head>
<body onload="timeview();">
    <form id="form1" runat="server">
    <div>
    
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/info.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="自动检查识别请求的到达情况"></asp:Label></td>
                </tr>
            </table>
            <hr />
        </div>

        <div class="contentTable">
        <br />
        <table border="0" width="450" cellpadding="4" cellspacing="1" align="center">
        <tbody>
            <tr>
                <td align="center">
                    &nbsp;<b><span id="time_area"></span></b></td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td nowrap>
                    <asp:Button ID="btnBackToFillPage" runat="server" Text="转到填写界面" 
                        onclick="btnBackToFillPage_Click" />
                </td>
            </tr>
        </tfoot>
        </table>
        </div>
    </div>
    </form>
</body>
</html>
