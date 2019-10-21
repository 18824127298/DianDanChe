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

using System.ComponentModel;
using System.ComponentModel.Design;
using Sigbit.Common;

public partial class module_Calendar : System.Web.UI.UserControl
{
    protected string _control_real_name = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        _control_real_name = this.ClientID + "_edtDate";
    }

    //[Browsable(true)]
    //public string DateString
    //{
    //    get
    //    {
    //        return edtDate.Text;
    //    }
    //    set
    //    {
    //        edtDate.Text = value;
    //    }
    //}

    /// <summary>
    /// 10位字符串日期格式
    /// </summary>
    [Browsable(true)]
    public string DateString
    {
        get
        {
            return edtDate.Text;
        }
        set
        {
            edtDate.Text = value;
        }
    }

    /// <summary>
    /// 19位字符串日期时间格式
    /// </summary>
    [Browsable(true)]
    public string DateTimeString
    {
        get
        {
            string sDate = edtDate.Text;
            if (sDate == "")
                return "";
            else
                return sDate + " 00:00:00";
        }
        set
        {
            edtDate.Text = value.Substring(0, 10);
        }      
    }

    [Browsable(true)]
    public DateTime DateTime
    {
        get
        {
            string sDate = edtDate.Text;
            if (sDate == "")
                return DateTime.Now;
            else
            {
                DateTime dt = DateTimeUtil.ToDateTime(sDate);
                return dt;
            }
        }
        set
        {
            string sDateTime = DateTimeUtil.ToDateStr(value);
            edtDate.Text = sDateTime;
        }
    }

    /// <summary>
    /// 控件的宽度
    /// </summary>
    [Browsable(true)]
    public int CalendarWidth
    {       
        get
        {

            return ConvertUtil.ToInt(edtDate.Width);
        }
        set
        {
            edtDate.Width = value;
        }
    }

}
