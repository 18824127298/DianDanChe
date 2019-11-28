using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WxPayApi;

public partial class caiwu_caiwu_zhidan_native_pay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        NativePay nativePay = new NativePay();

        //生成扫码支付模式一url
        string url1 = nativePay.GetPrePayUrl("123456789");

        //生成扫码支付模式二url
        string url2 = nativePay.GetPayUrl("123456789");

        //将url生成二维码图片
        Image1.ImageUrl = "make_image.aspx?data=" + HttpUtility.UrlEncode(url1);
        Image2.ImageUrl = "make_image.aspx?data=" + HttpUtility.UrlEncode(url2); 
    }
}