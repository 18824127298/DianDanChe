<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index_top.aspx.cs" Inherits="framework_main_index_top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>欢迎登陆</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">

    <script language="javascript" src="../../js/tcx_cs_function_calling.js"></script>

    <script language="javascript">

function logger_logout(logout_cause)
{
//======= 取消相关注销机制 =============
//    var tmp = new TCXCSFunction();
//    tmp.AsseblyName = "Sigbit.Framework.dll";
//    tmp.TypeName = "Sigbit.Framework.LoginLogger__ForJS";
//    tmp.FunctionName = "Logout";
//    tmp.AddParameter("string", logout_cause);
//    tmp.Execute();
//    tmp = null;
}

function logger_heartbeat()
{
//======== 取消心跳机制 ================
//    var tmp = new TCXCSFunction();
//    tmp.AsseblyName = "Sigbit.Framework.dll";
//    tmp.TypeName = "Sigbit.Framework.LoginLogger__ForJS";
//    tmp.FunctionName = "HeartBeat";
//    tmp.Execute();
//    tmp = null;
}

window.onbeforeunload = logger_logout_winclose;

function logger_logout_winclose()
{
    logger_logout("win_close");
}

    </script>

    <script language="JavaScript">

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
  if (heatbeat_seconds_ellapsed >= 30)
  {
    time_area.innerHTML = "<font color='red'>◎</font>" + time_area.innerHTML;
    heatbeat_seconds_ellapsed = 0;
    logger_heartbeat();
  }
  
  OA_TIME.setSeconds(OA_TIME.getSeconds()+1);
  window.setTimeout( "timeview()", 1000 );
}

//------------ 询问注销系统 ------------
function iflogout()
{
if(window.confirm('确定注销吗？'))
 {
  logger_logout("logout");
  parent.parent.location="../admin/admin.aspx";        // 页面跳转
 }
}

//------------ 询问退出系统 ------------
function ifexit()
{
if(window.confirm('确定退出吗？'))
 {
  logger_logout("exit");
  parent.parent.location="../admin/admin.aspx";         // 页面跳转
  parent.parent.close();                                // 关闭当前窗口 
  }
}

//------------ 返回桌面 ------------
function GoTable()
{
  parent.parent.location="mainframe.aspx";
}

// <!--屏蔽鼠标右键开始-->
if (window.Event)
  document.captureEvents(Event.MOUSEUP);

function nocontextmenu()
{
 event.cancelBubble = true
 event.returnValue = false;

 return false;
}

function norightclick(e)
{
 if (window.Event)
 {
  if (e.which == 2 || e.which == 3)
   return false;
 }
 else
  if (event.button == 2 || event.button == 3)
  {
   event.cancelBubble = true
   event.returnValue = false;
   return false;
  }

}
document.oncontextmenu = nocontextmenu;  // for IE5+
document.onmousedown = norightclick;     // for all others
// <!--屏蔽鼠标右键结束-->

    </script>

    <meta content="MSHTML 6.00.2900.3059" name="GENERATOR">
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</head>
<%=_bodyTemplate %>
</html>
