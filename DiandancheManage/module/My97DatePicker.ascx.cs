using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;

using Sigbit.Common;
using System.ComponentModel;

public partial class My97DatePicker : System.Web.UI.UserControl
{


    /// <summary>
    /// 样式表
    /// </summary>
    public string CssClass
    {
        get
        {
            return edtDateTime.CssClass;
        }
        set { edtDateTime.CssClass = value; }
    }

    public Unit Width
    {
        get { return edtDateTime.Width; }
        set { edtDateTime.Width = value; }
    }


    //public CssStyleCollection Style
    //{
    //    get
    //    {
    //        return edtDateTime.Style;
    //    }
    //    set
    //    {
    //        edtDateTime.Style.Value = value.Value;
    //    }

    //}


    private bool _readOnly = false;
    /// <summary>
    /// 是否只读，默认可读
    /// </summary>
    public bool ReadOnly
    {
        get { return _readOnly; }
        set { _readOnly = value; }
    }

    private bool _isShowWeek = false;
    /// <summary>
    /// 是否显示周，默认不显示
    /// </summary>
    [Browsable(true)]
    public bool IsShowWeek
    {
        get { return _isShowWeek; }
        set { _isShowWeek = value; }
    }

    private bool _highLineWeekDay = false;
    /// <summary>
    /// 指定是否高亮周末,默认False
    /// </summary>
    public bool HighLineWeekDay
    {
        get { return _highLineWeekDay; }
        set { _highLineWeekDay = value; }
    }

    private bool _isShowClear = true;
    /// <summary>
    /// 是否显示清空按钮,默认显示
    /// </summary>
    public bool IsShowClear
    {
        get { return _isShowClear; }
        set { _isShowClear = value; }
    }

    private DayOfWeek _firstDayOfWeek = DayOfWeek.Sunday;
    /// <summary>
    /// 星期的第一天，默认星期日
    /// </summary>
    public DayOfWeek FirstDayOfWeek
    {
        get { return _firstDayOfWeek; }
        set { _firstDayOfWeek = value; }
    }


    private bool _doubleCalendar = false;
    /// <summary>
    /// 双月日历显示
    /// </summary>
    public bool DoubleCalendar
    {
        get { return _doubleCalendar; }
        set { _doubleCalendar = value; }
    }

    private string _showDateFmt = "";
    /// <summary>
    /// 日期显示格式,可通过My97DatePicker_ShowDateFmt常量设置
    /// </summary>
    public string ShowDateFmt
    {
        get
        {
            if (_showDateFmt == "")
            {
                _showDateFmt = My97DatePicker_ShowDateFmt.StandDate;
            }
            return _showDateFmt;
        }
        set { _showDateFmt = value; }
    }


    private string _startDateString = "";
    /// <summary>
    /// 起始日期字符串
    /// </summary>
    public string StartDateString
    {
        get { return _startDateString; }
        set { _startDateString = value; }
    }


    private bool _alwaysUseStartDate = false;
    /// <summary>
    /// 是否总是将设置的值作为起始日期,默认False;
    /// </summary>
    public bool AlwaysUseStartDate
    {
        get { return _alwaysUseStartDate; }
        set
        {
            if (value && this.StartDateString == "")
            {
                throw new Exception("设置显示起始日期不允许为空！");
            }
            _alwaysUseStartDate = value;
        }
    }


    private My97DatePicker_ErrDealMode _errDealMode = My97DatePicker_ErrDealMode.JsAlert;
    /// <summary>
    /// 自动纠错模式
    /// </summary>
    public My97DatePicker_ErrDealMode ErrDealMode
    {
        get { return _errDealMode; }
        set { _errDealMode = value; }
    }


    /// <summary>
    /// 最小日期限制
    /// </summary>
    public string MinDate
    {
        get { return ConvertUtil.ToString(ViewState["minDate"]); }
        set { ViewState["minDate"] = value; }
    }


    /// <summary>
    /// 最大日期
    /// </summary>
    public string MaxDate
    {
        get { return ConvertUtil.ToString(ViewState["maxDate"]); }
        set { ViewState["maxDate"] = value; }
    }



    private DateTime _dtTime;
    /// <summary>
    /// 设置或者读取控件时间值
    /// </summary>
    public DateTime DateTime
    {
        get
        {
            string sDate = hfdDateTime.Value;
            if (sDate != "")
            {
                _dtTime = DateTimeUtil.ToDateTime(sDate);
            }
            return _dtTime;
        }
        set
        {
            _dtTime = value;
            edtDateTime.Text = _dtTime.ToString(this.ShowDateFmt);
            hfdDateTime.Value = edtDateTime.Text;
        }
    }

    /// <summary>
    /// 设置或者读取时间字符串
    /// </summary>
    public string DateTimeString
    {
        get
        {
            return hfdDateTime.Value;
        }
        set
        {
            edtDateTime.Text = value;
            hfdDateTime.Value = edtDateTime.Text;
        }
    }

    /// <summary>
    /// 设置或者读取时间字符串(兼容之前的 DatePicker控件 )
    /// </summary>
    public string DateString 
    {
        get
        {
            return DateTimeString;
        }
        set
        {
            DateTimeString = value;
        }
    }


    /// <summary>
    /// 绑定最小时间
    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    public void BindMinDate(My97DatePicker control)
    {
        this.MinDate = "#F{$dp.$D(\\'" + control.hfdDateTime.ClientID + "\\')}";
        edtDateTime.Attributes["onFocus"] = GenWdatePickerString();
    }

    /// <summary>
    /// 最大时间绑定其它控件
    /// </summary>
    /// <param name="control"></param>
    public void BindMaxDate(My97DatePicker control)
    {
        this.MaxDate = "#F{$dp.$D(\\'" + control.hfdDateTime.ClientID + "\\')}";
        edtDateTime.Attributes["onFocus"] = GenWdatePickerString();
    }


    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (edtDateTime.CssClass == "")
        {
            edtDateTime.CssClass = "Wdate";
        }


        //=================1.脚本注册===============
        string sJsPath = GenJSRootPath(this.Parent.Page);
        Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "Setup",
            sJsPath + "module/My97DatePicker/WdatePicker.js");

        //================2.添加控件属性字符串=====
        edtDateTime.Attributes.Add("onFocus", GenWdatePickerString());

    }


    private string GenPropertyString(string sKey, string sValue)
    {
        return string.Format(" {0}=\"{1}\" ", sKey, sValue);
    }

    /// <summary>
    /// 生成控件属性字符串
    /// </summary>
    /// <returns></returns>
    private string GenWdatePickerString()
    {
        StringBuilder sbRet = new StringBuilder();
        sbRet.Append("WdatePicker({");

        if (this.ReadOnly)
        {
            sbRet.Append("readOnly:true,");
        }

        if (this.IsShowWeek)
        {
            sbRet.Append("isShowWeek:true,");
        }

        if (this.HighLineWeekDay)
        {
            sbRet.Append("highLineWeekDay:true,");
        }

        if (this.FirstDayOfWeek != DayOfWeek.Sunday)
        {
            sbRet.Append("firstDayOfWeek:" + ConvertUtil.ToInt(this.FirstDayOfWeek) + ",");
        }

        if (this.DoubleCalendar)
        {
            sbRet.Append("doubleCalendar:true,");
        }

        if (this.ShowDateFmt != "")
        {
            sbRet.Append("dateFmt:'" + this.ShowDateFmt + "',");
        }

        if (this.StartDateString != "")
        {
            sbRet.Append("startDate:'" + this.StartDateString + "',");
        }

        if (this.AlwaysUseStartDate)
        {
            sbRet.Append("alwaysUseStartDate:true,");
        }

        if (this.ErrDealMode != My97DatePicker_ErrDealMode.JsAlert)
        {
            sbRet.Append("errDealMode:" + ConvertUtil.ToInt(this.ErrDealMode) + ",");
        }

        if (this.MinDate != "")
        {
            sbRet.Append("minDate:'" + this.MinDate + "',");
        }

        if (this.MaxDate != "")
        {
            sbRet.Append("maxDate:'" + this.MaxDate + "',");
        }


        sbRet.Append("vel:'" + hfdDateTime.ClientID + "'");


        sbRet.Append("})");
        return sbRet.ToString();
    }


    /// <summary>
    /// 生成JS根路径,示例../../
    /// </summary>
    /// <param name="CurrentPage">当前页</param>
    /// <returns></returns>
    public string GenJSRootPath(Page CurrentPage)
    {
        string sRet = "";
        int nFindCount = StringUtil.Occurs("/", CurrentPage.Page.AppRelativeVirtualPath);
        for (int i = 1; i < nFindCount; i++)
        {
            sRet += "../";
        }
        return sRet;
    }


    //public override string ToString()
    //{
    //    //<input id="d11" type="text" onClick="WdatePicker()"/>
    //    StringBuilder sbRet = new StringBuilder();

    //    string sControlHead = "<input ";
    //    string sControlBottom = " />";

    //    sbRet.Append(sControlHead);
    //    sbRet.Append(GenPropertyString("id", this.ID));

    //    sbRet.Append(GenPropertyString("type", "text"));

    //    if (this.CssClass != "")
    //    {
    //        sbRet.Append(GenPropertyString("class", this.CssClass));
    //    }

    //    if (this.Style != "")
    //    {
    //        sbRet.Append(GenPropertyString("style", this.Style));
    //    }

    //    sbRet.Append(GenPropertyString("onFocus", GenWdatePickerString()));

    //    sbRet.Append(sControlBottom);
    //    return sbRet.ToString();
    //}

}


public class My97DatePicker_ShowDateFmt
{
    /// <summary>
    /// 标准日期格式,示例:2008-03-12 
    /// </summary>
    public const string StandDate = "yyyy-MM-dd";

    /// <summary>
    /// 标准时间,示例:19:20:00
    /// </summary>
    public const string StandTime = "HH:mm:ss";

    /// <summary>
    /// 完整的日期加时间格式,示例:2008-03-12 19:20:00
    /// </summary>
    public const string FullDateTime = "yyyy-MM-dd HH:mm:ss";
}



/// <summary>
/// 自动纠错模式
/// </summary>
public enum My97DatePicker_ErrDealMode
{
    /// <summary>
    /// JS告警提示
    /// </summary>
    JsAlert = 0,
    /// <summary>
    /// 自动恢复上一次正确的值
    /// </summary>
    AutoRestore = 1,
    /// <summary>
    /// 只是作标记,标记类:WdateFmtErr是在skin目录下WdatePicker.css中定义的
    /// </summary>
    JustTag = 2
}


