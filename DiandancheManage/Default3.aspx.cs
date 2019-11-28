using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using Newtonsoft.Json;
using Sigbit.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
//        if (Request.QueryString["Action"] != null)
//        {
//            Response.ContentType = "text/plain";
//            string Action = Request.QueryString["Action"];
//            switch (Action)
//            {
//                case "Turnover":
//                    Response.Write(Turnover());
//                    break;
//                case "Transactions":
//                    Response.Write(Transactions());
//                    break;
//                case "DailyTurnover":
//                    Response.Write(DailyTurnover());
//                    break;
//                case "MonthlyRepayment":
//                    Response.Write(MonthlyRepayment());
//                    break;
//                case "CurrentMonthNumber":
//                    Response.Write(CurrentMonthNumber());
//                    break;
//            }
//            Response.End();
//        }
//        if (!IsPostBack)
//        {
//        }
    }

//    public string Turnover()
//    {
//        LoanApplyService loanApplyService = new LoanApplyService();
//        List<LoanApply> MonthlyVolumeList = loanApplyService.MonthlyVolume();
//        Int32[] lists19 = new Int32[12];
//        foreach (LoanApply loanapply in MonthlyVolumeList)
//        {
//            if (loanapply.syear.Contains("2019"))
//                lists19[loanapply.nmonth.Value - 1] = Convert.ToInt32((loanapply.TotalAmountStage.Value));
//        }
//        Dictionary<string, Int32[]> lt = new Dictionary<string, Int32[]>();
//        lt.Add("lists19", lists19);

//        string imfo = JsonConvert.SerializeObject(lt).ToString();
//        return imfo;
//    }


//    public string Transactions()
//    {
//        LoanApplyService loanApplyService = new LoanApplyService();
//        List<LoanApply> MonthlyTransactionsList = loanApplyService.MonthlyTransactions();
//        Int32[] lists19 = new Int32[12];
//        foreach (LoanApply loanapply in MonthlyTransactionsList)
//        {
//            if (loanapply.syear.Contains("2019"))
//                lists19[loanapply.nmonth.Value - 1] = loanapply.count.Value;
//        }
//        Dictionary<string, Int32[]> lt = new Dictionary<string, Int32[]>();
//        lt.Add("lists19", lists19);

//        string imfo = JsonConvert.SerializeObject(lt).ToString();
//        return imfo;
//    }


//    public string DailyTurnover()
//    {
//        LoanApplyService loanApplyService = new LoanApplyService();
//        List<LoanApply> DailyTurnoverList = loanApplyService.DailyTurnover();
//        DateTime dtmonth = DateTime.Now;
//        Int32[] lists19 = new Int32[dtmonth.Day];
//        foreach (LoanApply loanapply in DailyTurnoverList)
//        {
//            lists19[loanapply.day.Value - 1] = loanapply.count.Value;
//        }
//        Dictionary<string, Int32[]> lt = new Dictionary<string, Int32[]>();
//        lt.Add("lists19", lists19);

//        string imfo = JsonConvert.SerializeObject(lt).ToString();
//        return imfo;
//    }

//    public string MonthlyRepayment()
//    {
//        RechargeService rechargeService = new RechargeService();
//        List<Recharge> MonthlyRepaymentList = rechargeService.MonthlyRepayment();
//        Int32[] lists19 = new Int32[12];
//        foreach (Recharge recharge in MonthlyRepaymentList)
//        {
//            if (recharge.syear.Contains("2019"))
//                lists19[recharge.nmonth.Value - 1] = Convert.ToInt32((recharge.Amount.Value));
//        }
//        Dictionary<string, Int32[]> lt = new Dictionary<string, Int32[]>();
//        lt.Add("lists19", lists19);

//        string imfo = JsonConvert.SerializeObject(lt).ToString();
//        return imfo;
//    }

//    public string CurrentMonthNumber()
//    {
//        string sql = @"select COUNT(*) as value,b.FullName as name  from LoanApply l join Borrower b on l.SalesmanId = b.Id where l.IsValid= 1 
//and (l.RepaymentStatus = 5 or l.RepaymentStatus = 6) and convert(varchar(7),AuditTime ,120) = convert(varchar(7),GETDATE(),120)
//  group by  b.FullName order by value desc";

//        DataSet ds = DataHelper.Instance.ExecuteDataSet(sql);
//        string[] namelists = new string[ds.Tables[0].Rows.Count];

//        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
//        {
//            namelists[i] = ds.Tables[0].Rows[i]["name"].ToString();
//        }
//        Dictionary<string, string> lt = new Dictionary<string, string>();
//        lt.Add("namelists", string.Join(",", namelists));
//        lt.Add("jolists", JsonConvert.SerializeObject(ds.Tables[0]));

//        string imfo = JsonConvert.SerializeObject(lt).ToString();
//        return imfo;
//    }
    protected void btn_dayinji_Click(object sender, EventArgs e)
    {
        //==================方法1.打印订单==================
        //***返回值JSON字符串***
        //成功：{"msg":"ok","ret":0,"data":"xxxxxxx_xxxxxxxx_xxxxxxxx","serverExecutedTime":5}
        //失败：{"msg":"错误描述","ret":非0,"data":"null","serverExecutedTime":5}

        string method1 = print();
        System.Console.WriteLine(method1);
    }

    //**********************请先填打印机编号和KEY，再测试**************************
    public static string USER = "357454@qq.com";  //*必填*：登录管理后台的账号名
    public static string UKEY = "zEfpmArLngwTyzVV";//*必填*: 注册账号后生成的UKEY

    public static string URL = "http://api.feieyun.cn/Api/Open/";//不需要修改


    //方法1
    private string print()
    {
        //标签说明：
        //单标签: 
        //"<BR>"为换行,"<CUT>"为切刀指令(主动切纸,仅限切刀打印机使用才有效果)
        //"<LOGO>"为打印LOGO指令(前提是预先在机器内置LOGO图片),"<PLUGIN>"为钱箱或者外置音响指令
        //成对标签：
        //"<CB></CB>"为居中放大一倍,"<B></B>"为放大一倍,"<C></C>"为居中,<L></L>字体变高一倍
        //<W></W>字体变宽一倍,"<QR></QR>"为二维码,"<BOLD></BOLD>"为字体加粗,"<RIGHT></RIGHT>"为右对齐

        //拼凑订单内容时可参考如下格式
        string orderInfo;
        orderInfo = "<C>广州市天河区加油站</C><BR>";//标题字体如需居中放大,就需要用标签套上
        orderInfo += "<C>（加油站存根）</C><BR>";
        orderInfo += "--------------------------------<BR>";
        orderInfo += "<BOLD>流水号　CYH-2019112012080727053</BOLD><BR>";
        orderInfo += "--------------------------------<BR>";
        orderInfo += "交易时间　2019-11-20 12:08:07<BR>";
        orderInfo += "电话　　　188*****298<BR>";
        orderInfo += "来源　　　车1号<BR>";
        orderInfo += "油枪　　　1号枪<BR>";
        orderInfo += "油号　　　92#<BR>";
        orderInfo += "升数　　　7升<BR>";
        orderInfo += "--------------------------------<BR>";
        orderInfo += "<B>金额：123元</B><BR>";
        orderInfo += "--------------------------------<BR>";

        orderInfo = Uri.EscapeDataString(orderInfo);
        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(URL);
        req.Method = "POST";
        UTF8Encoding encoding = new UTF8Encoding();

        string postData = "sn=" + edtSN.Text;
        postData += ("&content=" + orderInfo);
        postData += ("&times=" + "1");//默认1联

        int itime = DateTimeToStamp(System.DateTime.Now);//时间戳秒数
        string stime = itime.ToString();
        string sig = sha1(USER, UKEY, stime);

        //公共参数
        postData += ("&user=" + USER);
        postData += ("&stime=" + stime);
        postData += ("&sig=" + sig);
        postData += ("&apiname=" + "Open_printMsg");

        byte[] data = encoding.GetBytes(postData);

        req.ContentType = "application/x-www-form-urlencoded";
        req.ContentLength = data.Length;
        Stream resStream = req.GetRequestStream();

        resStream.Write(data, 0, data.Length);
        resStream.Close();

        HttpWebResponse response;
        string strResult;
        try
        {
            response = (HttpWebResponse)req.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            strResult = reader.ReadToEnd();
        }
        catch (WebException ex)
        {
            response = (HttpWebResponse)ex.Response;
            strResult = response.StatusCode.ToString();//错误信息
        }

        response.Close();
        req.Abort();
        //服务器返回的JSON字符串，建议要当做日志记录起来
        return strResult;

    }

    //签名USER,UKEY,STIME
    public string sha1(string user, string ukey, string stime)
    {
        var buffer = Encoding.UTF8.GetBytes(user + ukey + stime);
        var data = SHA1.Create().ComputeHash(buffer);

        var sb = new StringBuilder();
        foreach (var t in data)
        {
            sb.Append(t.ToString("X2"));
        }

        return sb.ToString().ToLower();

    }


    private int DateTimeToStamp(System.DateTime time)
    {
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); return (int)(time - startTime).TotalSeconds;
    }
}