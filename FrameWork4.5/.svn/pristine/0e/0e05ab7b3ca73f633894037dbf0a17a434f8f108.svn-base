// JScript 文件

//定义显示时间的格式
//1. 年-月-日 如：2002-3-6
//2. 月/日/年 如：3/6/2002

var showType=1;//默认为1;2002-3-6

//中文月份,如果想显示英文月份，修改下面的注释
/*var months = new Array("January?, "February?, "March",
    "April", "May", "June", "July", "August", "September",
    "October", "November", "December");*/
var months = new Array("一月", "二月", "三月",
    "四月", "五月", "六月", "七月", "八月", "九月",
    "十月", "十一月", "十二月");
var daysInMonth = new Array(31, 28, 31, 30, 31, 30, 31, 31,
    30, 31, 30, 31);
  //中文周  如果想显示 英文的，修改下面的注释
/*var days = new Array("Sunday", "Monday", "Tuesday",
    "Wednesday", "Thursday", "Friday", "Saturday");*/
var days = new Array("日","一", "二", "三",
    "四", "五", "六");
var getObject,oldObject;//取得当前对象
function getDays(month, year) {
    //下面的这段代码是判断当前是否是闰年的
    if (1 == month)
       return ((0 == year % 4) && (0 != (year % 100))) ||
          (0 == year % 400) ? 29 : 28;
    else
       return daysInMonth[month];
}

function getToday() 
{
    //得到今天的年,月,日
    this.now = new Date();
    this.year = this.now.getFullYear();
    this.month = this.now.getMonth();
    this.day = this.now.getDate();
}

today = new getToday();

function newCalendar() 
{ 
    today = new getToday();
    var parseYear = parseInt(document.all.year
       [document.all.year.selectedIndex].text);
    var newCal = new Date(parseYear,document.all.month.selectedIndex, 1);
    var day = -1;
    var startDay = newCal.getDay();
    var daily = 0;
    if ((today.year == newCal.getFullYear()) &&(today.month == newCal.getMonth()))
       day = today.day;
    var tableCal = document.all.calendar.tBodies.dayList;
    var intDaysInMonth =getDays(newCal.getMonth(), newCal.getFullYear());
    for (var intWeek = 0; intWeek < tableCal.rows.length;intWeek++)
         for (var intDay = 0;intDay < tableCal.rows[intWeek].cells.length;intDay++)
         {
          var cell = tableCal.rows[intWeek].cells[intDay];
          if ((intDay == startDay) && (0 == daily))
             daily = 1;
          if(day==daily)
              //今天，调用今天的Class
              cell.className = "caltoday";
          else if(intDay==6)
              //周六
              cell.className = "calsatday";
          else if (intDay==0)
              //周日
              cell.className ="calsunday";
          else
              //平常
              cell.className="calnormal";        
          if ((daily > 0) && (daily <= intDaysInMonth))
             { 
               cell.innerText = daily;
               daily++;
             }
          else
              cell.innerText = "";
       }
}

function FullLengthMonthDay(s)
{
    if (s.length == 1)
        s = "0" + s;
    return s;
}

function getDate() 
{
    var sDate;
    //这段代码处理鼠标点击的情况
    if ("TD" == event.srcElement.tagName)
       if ("" != event.srcElement.innerText)
         {//从这儿可以改显示日期的格式
            if(showType==1)
               sDate = document.all.year.value + "-" + FullLengthMonthDay(document.all.month.value) + "-" + FullLengthMonthDay(event.srcElement.innerText) + "";
                else if(showType==2)
           sDate =  FullLengthMonthDay(document.all.month.value) + "/" + FullLengthMonthDay(event.srcElement.innerText) +"/"+ document.all.year.value;
                else
                        sDate = document.all.year.value + "-" + FullLengthMonthDay(document.all.month.value) + "-" + FullLengthMonthDay(event.srcElement.innerText) + "";
                              //    getObject.value=sDate;
                                  //Document.all.Calendar1_edtDate.value=sDate;
                                 document.all(getObject).value=sDate;
                              //    setUpdateFlag(getObject);//触发事件，保存数据
                                HideLayer();

                  }
}

function HideLayer() 
{
        Layer.style.visibility = "hidden";
        oldObject=null;  //%%%%ADD
}

function LayerShow()
{
        Layer.style.visibility = "visible";
}

function ShowLayer(t,l) 
{
    if(oldObject==null)
    {
        oldObject=getObject;//付值
        if(Layer.style.visibility != "visible" && oldObject==getObject)//判断是否操作同一表格
        {
            //if(l>480)l=l-131;
            Layer.style.top = t+document.body.scrollTop;
            Layer.style.left = l+document.body.scrollLeft;
            Layer.style.visibility = "visible";
            oldObject=getObject;
        }
    }
    else if(oldObject==getObject)
    {
        HideLayer();
    }
    else
    {
        //if(l>480)l=l-131;
        Layer.style.top = t+document.body.scrollTop;
        Layer.style.left = l+document.body.scrollLeft;
        Layer.style.visibility = "visible";
        oldObject=getObject;
    }
}
function selectDate(x)
{
    window.event.cancelBubble = true;//冒泡，使触发页面事件失效
    getObject=x;
    var xx=event.clientX;//取得x坐标;
    var yy=event.clientY;//取得y坐标;
    newCalendar();
    ShowLayer(yy,xx);//显示出div
}

function show()//冒泡，点击层失效
{
    window.event.cancelBubble = true;
}

//定义移动
var Obj=''
document.onmouseup=MUp
document.onmousemove=MMove

function MDown(Object)
{
    Obj=Object.id
    document.all(Obj).setCapture();
    pX=event.x-document.all(Obj).style.pixelLeft;
    pY=event.y-document.all(Obj).style.pixelTop;
}

function MMove()
{
    if(Obj!='')
    {
        document.all(Obj).style.left=event.x-pX;
        document.all(Obj).style.top=event.y-pY;
    }
}

function MUp()
{
    if(Obj!='')
    {
        document.all(Obj).releaseCapture();
        Obj='';
    }
}


//定义div显示日期;
function getLayer()
{
    document.write("<div id=\"Layer\" onclick=show() class=\"calbackcolor\" style=\"position:absolute; width:131; z-index:2; border:1px inset black;layer-background-color: #f0f8ff; visibility: hidden; height: 38; left:58; top:137;\"><input type=\"hidden\" name=\"ret\"><table width=100% class=\"caltable\" style=\"cursor:move\" onmousedown=MDown(Layer)><tr align=center><td>按住鼠标拖动</td></tr></table><table id=\"calendar\" cellpadding=\"0\" align=\"center\"><thead><tr><td colspan=7 align=CENTER><select id=\"month\" onChange=\"newCalendar()\" name=\"select\" class=\"calsmallSel\">");
     for (var intLoop = 0; intLoop < months.length;  intLoop++)
     document.write("<OPTION VALUE= " + (intLoop + 1) + " " + (today.month == intLoop ? "Selected" : "") + ">" + months[intLoop]);
    document.write("</select><select id=\"year\" onChange=\"newCalendar()\" name=\"select\" class=\"calsmallSel\">");
     for (var intLoop = today.year-50; intLoop < (today.year + 20);                          intLoop++)
     document.write("<OPTION VALUE= " + intLoop + " " +
     (today.year == intLoop ?  "Selected" : "") + ">" + intLoop);
    document.write("</select></td></tr><tr class=\"caldays\">");
    document.write("<TD class=calsunday>" + days[0] + "</TD>");
    for (var intLoop = 1; intLoop < days.length-1;intLoop++)
    document.write("<TD>" + days[intLoop] + "</TD>");
    document.write("<TD class=calsatday>" + days[intLoop] + "</TD>");
    document.write("      </tr>      </thead> <tbody border=1 cellspacing=\"0\" cellpadding=\"0\" id=\"dayList\" align=CENTER ONCLICK='getDate()'>");
     for (var intWeeks = 0; intWeeks < 6; intWeeks++) {
    document.write("<TR style='cursor:hand'>");
     for (var intDays = 0; intDays < days.length;intDays++)
    document.write("<TD></TD>");
    document.write("</TR>");
     }
    document.write("</tbody></table></div>");

    document.body.onclick=HideLayer;//触发点击页面事件，关闭窗口
    document.oncontextmenu=HideLayer;//触发点击页面右键，关闭窗口
}
//显示css
//document.write("<style>TABLE        {font-family:宋体,MS SONG,SimSun,tahoma,sans-serif; font-size:9pt;border:0px}.drpdwn        {font-family:宋体,MS SONG,SimSun,tahoma,sans-serif;font-size:9pt;color:#000066;background-color:#FFFFFF} SELECT.smallSel{    BACKGROUND-COLOR: #ffffff;    COLOR: #000080;    FONT-SIZE: 9pt} .normal{BACKGROUND: #ffffff} .today {font-weight:bold;BACKGROUND: #6699cc} .satday{color:green} .sunday{color:red} .days {font-weight:bold} .Arraw {color:#0000BB; cursor:hand; font-family:Webdings; font-size:9pt}</style>");

getLayer();//显示div
