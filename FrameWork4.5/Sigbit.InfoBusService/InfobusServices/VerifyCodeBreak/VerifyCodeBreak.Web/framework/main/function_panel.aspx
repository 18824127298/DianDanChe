<%@ Page Language="C#" AutoEventWireup="true" CodeFile="function_panel.aspx.cs" Inherits="farmwork_main_function_panel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <script>
var menu_id=1;

//----------- 设置选择的演示 -----------
function setPointer(element,over_flag,menu_id_over)
{
  if(menu_id!=menu_id_over)                        // 判断当前位置是否已经被选中
  {
     if(over_flag==1)
        element.className='menu_operation_3';      // 鼠标进入显示颜色  
     else
        element.className='menu_operation_2';      // 鼠标离开显示演示  
  }
}

//----------- 初始话显示面板 -------------
var init_flag=0;
function init_menu()
{
 // init_flag++;
 // if(init_flag==2)
     view_menu(1);
}

//------------ 查看面板中的页 ------------
function view_menu(id)
{
    return;
  //if(menu_id==id)
    // return;
  menu_id=id;
  for(i=1;i<=6;i++)
  {
     menu_i="menu_"+i;
     if(i==id)
        color_i='menu_operation_1';    //
     else
        color_i="menu_operation_2";    // 鼠标离开显示演示 #EEEEEE

     if(i<=3)
        menu_operation.document.all(menu_i).className=color_i;
     else
        menu_operation.document.all(menu_i).className=color_i;
  }

  if(id==1)
  {
     menu_page.location="about:blank";
	      frame1.rows="68,94,*,0,0,0";
       }
  else if(id==2)
  {
     menu_page.location="about:blank";
	 user_online.location="/function/function_panel/user_online.php?VIEW_USER=1";
          frame1.rows="68,94,0,*,0,0";
       }
  else if(id==3)
  {
     menu_page.location="about:blank";
	      frame1.rows="68,94,0,0,*,0";
	   }
  else if(id==4)
  {
     menu_page.location="shortcut";
	      frame1.rows="68,94,0,0,0,*";
	   }
  else if(id==5)
  {
     menu_page.location="smsbox";
	      frame1.rows="68,94,0,0,0,*";
	   }
  else if(id==6)
  {
     menu_page.location="bookmark";
	      frame1.rows="68,94,0,0,0,*";
	   }
}


 <!-- --------------- 普通状态调用函数 -------------- -->

//----------------- 打开地址 --------------------
function openURL(URL)
{
	parent.parent.table_index.table_main.location=URL;
}       

//----------------- 发送短消息 --------------------
function send_sms(TO_ID,TO_NAME)
{
   mytop=screen.availHeight-246;
   myleft=0; window.open("/function/status_bar/sms_back.php?TO_ID="+TO_ID+"&TO_NAME="+TO_NAME,"send_sms","height=170,width=350,status=0,toolbar=no,menubar=no,location=no,scrollbars=yes,top="+mytop+",left="+myleft+",resizable=yes");
}

//----------------- 发送邮件 --------------------
function send_email(TO_ID,TO_NAME)
{
   parent.parent.table_index.table_main.location="/function/email/new?TO_ID="+TO_ID+"&TO_NAME="+TO_NAME;
}

 
    </script>

    <!-- 普通状态登录 -->
    <meta content="MSHTML 6.00.2900.3059" name="GENERATOR">
</head>
<%=_frameSetTemplate %>      
</html>
