using CheDaiBaoCommonService.Service;
using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Interface;
using CheDaiBaoWeChatService.Service;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using Sigbit.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //RechargeService rechargeService = new RechargeService();
        //rechargeService.RechargeCompare("2019070318220558379", "4200000315201907031026636707", "oAh7kw24ZCfQCZyuHia7IOQ0Nd9A", 1, "CITIC_CREDIT]");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        BorrowService borrowService = new BorrowService();
        borrowService.Repayment(181300, 3458, "提还");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        BorrowService borrowService = new BorrowService();
        borrowService.Repayment(384, 2067, "正常还利息");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        BorrowService borrowService = new BorrowService();
        borrowService.Repayment(414, 2067, "逾期");
    }
    protected void BtnWeChat_Click(object sender, EventArgs e)
    {
        WechatPushMessage wechatpushMessage = new WechatPushMessage();
        //wechatpushMessage.PushOverdueMessage();
    }


    protected void Btnjjr_Click(object sender, EventArgs e)
    {
        if (IsHolidayByDate(DateTime.Now).Result == true)
            TextBox1.Text = "节假日";
    }

    /// <summary>
    /// 判断是不是周末/节假日
    /// </summary>
    /// <param name="date">日期</param>
    /// <returns>周末和节假日返回true，工作日返回false</returns>
    public static async Task<bool> IsHolidayByDate(DateTime date)
    {
        var isHoliday = false;
        var webClient = new System.Net.WebClient();
        var PostVars = new System.Collections.Specialized.NameValueCollection
             {
                 { "d", date.ToString("yyyyMMdd") }//参数
             };
        try
        {
            var day = date.DayOfWeek;

            //判断是否为周末
            if (day == DayOfWeek.Sunday || day == DayOfWeek.Saturday)
                return true;

            //0为工作日，1为周末，2为法定节假日
            var byteResult = await webClient.UploadValuesTaskAsync("http://tool.bitefu.net/jiari/", "POST", PostVars);//请求地址,传参方式,参数集合
            var result = Encoding.UTF8.GetString(byteResult);//获取返回值
            if (result == "1" || result == "2")
                isHoliday = true;
        }
        catch
        {
            isHoliday = false;
        }
        return isHoliday;
    }
    protected void BtnPhoto_Click(object sender, EventArgs e)
    {
        FileInfo fileInfo = new FileInfo("D://phonelee126576//phonelee//LanceNetFinance//PhoneLee//CheDaiBaoWeChat//PictureUpload//image//20190620//201906201848491186172.jpg");
        TextBox1.Text = fileInfo.Length.ToString();
    }
    protected void BtnJieQu_Click(object sender, EventArgs e)
    {
        string a = "http://file.api.weixin.qq.com/cgi-bin/media/get?access_token=24_2dA933tl3LE9kgJVw9KBj5x-CrxswKtIk5x-_gHf6PHNTULzIQ7smBwD1FAwDsWexYfSUImkc7gWxatMTy7TZgsaPxfdMgGBYll9WWJengZXs9VC33b_z7fx__7nwXHTfkvfgnsv0-iwwF41OPCaAFAYZM&media_id=aYvZyOYDW7mAtCI9HFOeLGAJs0fRkWhps3X9g51-Fkyi1sh0BF1mq_XMs2SfKxK1";
        int i = a.IndexOf("=");//找a的位置
        int j = a.IndexOf("&");//找b的位置
        a = (a.Substring(i + 1)).Substring(0, j - i - 1);
        TextBox1.Text = a;
    }

    protected void BtnXiaZai_Click(object sender, EventArgs e)
    {
        BusinessFileService businessFileService = new BusinessFileService();
        List<BusinessFile> businessFileList = businessFileService.Search(new BusinessFile() { IsValid = true }).Where(o => o.RelationId == 3).ToList();
        int i = 1;
        foreach (BusinessFile bf in businessFileList)
        {
            string fileName = "ceshi" + i + ".jpg";//客户端保存的文件名
            string filePath = VIVPHOTO(bf.WeChatPath);
            WriteResponse(fileName, GetImageContent(filePath));
            i++;
        }
        Response.End();
    }

    private void WriteResponse(string picName, byte[] content)
    {
        Response.Clear();
        Response.ClearHeaders();
        Response.Buffer = false;
        Response.ContentType = "application/octet-stream";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(picName, Encoding.Default));
        Response.AppendHeader("Content-Length", content.Length.ToString());
        Response.BinaryWrite(content);
        Response.Flush();
    }

    private byte[] GetImageContent(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.AllowAutoRedirect = true;

        WebProxy proxy = new WebProxy();
        proxy.BypassProxyOnLocal = true;
        proxy.UseDefaultCredentials = true;

        request.Proxy = proxy;

        WebResponse response = request.GetResponse();

        using (Stream stream = response.GetResponseStream())
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Byte[] buffer = new Byte[1024];
                int current = 0;
                while ((current = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    ms.Write(buffer, 0, current);
                }
                return ms.ToArray();
            }
        }
    }
    protected void btnBaoCun_Click(object sender, EventArgs e)
    {
        BusinessFileService businessFileService = new BusinessFileService();
        List<BusinessFile> businessFileList = businessFileService.Search(new BusinessFile { IsValid = true }).Where(o => o.FilePath == "").ToList();
        TextBox1.Text = businessFileList.Count.ToString();
        foreach (BusinessFile bf in businessFileList)
        {
            string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";

            WebClient mywebclient = new WebClient();

            string sLuJing = Server.MapPath("image") + "\\" + DateTime.Now.ToString("yyyyMMdd") + zw(bf.FileName);
            if (Directory.Exists(sLuJing) == false)
            {
                Directory.CreateDirectory(sLuJing);
            }
            string savepath = sLuJing + "\\" + sFileName;
            try
            {
                mywebclient.DownloadFile(VIVPHOTO(bf.WeChatPath), savepath);
                bf.FilePath = savepath;
                businessFileService.Update(bf);
            }
            catch (Exception ex)
            {
            }
        }
    }
    public string VIVPHOTO(string spath)
    {
        int i = spath.IndexOf("=");
        int j = spath.IndexOf("&");
        return spath.Replace((spath.Substring(i + 1)).Substring(0, j - i - 1), WeChatBaseRequestService.getApptoken());
    }






    public void ZipFile(string strFile, string strZip)
    {
        var len = strFile.Length;
        var strlen = strFile[len - 1];
        if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
        {
            strFile += Path.DirectorySeparatorChar;
        }
        ZipOutputStream outstream = new ZipOutputStream(File.Create(strZip));
        outstream.SetLevel(6);
        zip(strFile, outstream, strFile);
        outstream.Finish();
        outstream.Close();
    }

    public void zip(string strFile, ZipOutputStream outstream, string staticFile)
    {
        if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
        {
            strFile += Path.DirectorySeparatorChar;
        }
        Crc32 crc = new Crc32();
        //获取指定目录下所有文件和子目录文件名称
        string[] filenames = Directory.GetFileSystemEntries(strFile);
        //遍历文件
        foreach (string file in filenames)
        {
            if (Directory.Exists(file))
            {
                zip(file, outstream, staticFile);
            }
            //否则，直接压缩文件
            else
            {
                //打开文件
                FileStream fs = File.OpenRead(file);
                //定义缓存区对象
                //FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                //StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);

                //StringBuilder sb = new StringBuilder();
                //while (!sr.EndOfStream)
                //{
                //    sb.AppendLine(sr.ReadLine() + "<br>");
                //}
                byte[] buffer = new byte[fs.Length];
                //通过字符流，读取文件
                fs.Read(buffer, 0, buffer.Length);
                //得到目录下的文件（比如:D:\Debug1\test）,test
                string tempfile = file.Substring(staticFile.LastIndexOf("\\") + 1);
                ZipEntry entry = new ZipEntry(tempfile);
                entry.DateTime = DateTime.Now;
                entry.Size = fs.Length;
                fs.Close();
                crc.Reset();
                crc.Update(buffer);
                entry.Crc = crc.Value;
                outstream.PutNextEntry(entry);
                //写文件
                outstream.Write(buffer, 0, buffer.Length);
            }
        }
    }

    protected void btnYaSuo_Click(object sender, EventArgs e)
    {
        string[] strs = new string[2];

        //待压缩文件目录
        strs[0] = "D:\\image\\周澍";
        //压缩后的目标文件
        strs[1] = "D:\\image\\周澍.zip";
        ZipFile(strs[0], strs[1]);


    }
    protected void btnSendSms_Click(object sender, EventArgs e)
    {
        try
        {
            BorrowService borrowService = new BorrowService();
            borrowService.SmsRepaymentReminder(DateTime.Now.Date);
            TextBox1.Text = "已发送";
        }
        catch (Exception ex)
        {

            TextBox1.Text = ex.Message;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        BusinessFileService businessFileService = new BusinessFileService();
        List<BusinessFile> businessFileList = businessFileService.Search(new BusinessFile { IsValid = true }).Where(o => o.RelationId == ConvertUtil.ToInt(TextBox1.Text)).ToList();

        string sLuJing = "";
        string sName = "";
        foreach (BusinessFile bf in businessFileList)
        {
            string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";

            WebClient mywebclient = new WebClient();

            sName = zw(bf.FileName);
            sLuJing = Server.MapPath("image") + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + sName;
            if (Directory.Exists(sLuJing) == false)
            {
                Directory.CreateDirectory(sLuJing);
            }
            string savepath = sLuJing + "\\" + sFileName;

            try
            {
                mywebclient.DownloadFile(VIVPHOTO(bf.WeChatPath), savepath);
                bf.FilePath = savepath;
                businessFileService.Update(bf);

            }
            catch (Exception ex)
            {
            }
        }
        ZipFile(sLuJing, sLuJing + ".zip");


        WriteResponse(PinYinConverter.Get(sName) + ".zip", GetImageContent("http://diandanche1.nat123.net/image/" + DateTime.Now.ToString("yyyyMMdd") + "/" + sName + ".zip"));
    }


    //提取字符串的中文
    public string zw(string szf)
    {
        string x = @"[\u4E00-\u9FFF]+";
        MatchCollection Matches = Regex.Matches
        (szf, x, RegexOptions.IgnoreCase);
        StringBuilder sb = new StringBuilder();
        foreach (Match NextMatch in Matches)
        {
            sb.Append(NextMatch.Value);
        }
        return sb.ToString();
    }

    protected void btnFangKuang_Click(object sender, EventArgs e)
    {
        try
        {
            int nId = ConvertUtil.ToInt(TextBox1.Text);
            LoanApplyService loanapplyService = new LoanApplyService();
            LoanApply loanapply = loanapplyService.GetById(nId);
            loanapply.InterestDate = DateTime.Now.Date.AddDays(-2);
            loanapply.ExpectedRepayment = DateTime.Now.Date.AddDays(-2).AddMonths(ConvertUtil.ToInt(loanapply.Deadline));
            loanapply.IsLending = true;
            loanapply.LendingDate = DateTime.Now.Date;
            loanapply.BatchDate = DateTime.Now.Date.AddDays(-1);
            loanapplyService.Update(loanapply);

            BorrowService borrowService = new BorrowService();
            Borrow borrow = new Borrow();
            int avgPrincipal = Convert.ToInt32(Math.Floor(loanapply.TotalAmountStage.Value / loanapply.Deadline.Value).ToString());
            decimal Interest = loanapply.MonthlyPayment.Value - avgPrincipal;
            for (int i = 0; i < loanapply.Deadline; i++)
            {
                borrow.BorrowerId = loanapply.BorrowerId;
                borrow.LoanApplyId = loanapply.Id;
                borrow.RepaymentDate = DateTime.Now.AddDays(-2).AddMonths(i + 1).Date;
                borrow.Stages = i + 1;
                borrow.TotalPeriod = loanapply.Deadline.Value;
                borrow.OverInterest = 0;
                borrow.OverDay = 0;
                borrow.BreachAmount = 0;
                if (i + 1 == loanapply.Deadline)
                {
                    borrow.Principal = loanapply.TotalAmountStage.Value - avgPrincipal * (loanapply.Deadline - 1);
                    borrow.UnPrincipal = loanapply.TotalAmountStage.Value - avgPrincipal * (loanapply.Deadline - 1);
                    borrow.Interest = loanapply.MonthlyPayment.Value - borrow.Principal;
                    borrow.UnTotalInterest = loanapply.MonthlyPayment.Value - borrow.UnPrincipal;
                }
                else
                {
                    borrow.Principal = avgPrincipal;
                    borrow.UnPrincipal = avgPrincipal;
                    borrow.Interest = Interest;
                    borrow.UnTotalInterest = Interest;
                }
                borrowService.Insert(borrow);
            }
            FundsFlowService fundsflowService = new FundsFlowService();
            fundsflowService.Insert(new FundsFlow()
            {
                Amount = loanapply.TotalAmountStage,
                IncomeGodId = 5,
                FeeType = FeeType.平台打款,
                IsComputing = true,
                PayGodId = 2,
                LoanApplyId = loanapply.Id,
                IsFreeze = false,
                Remark = "打款"
            });

        }
        catch (Exception ex)
        {
        }
    }


    /// <summary>
    /// 下载zip
    /// </summary>
    /// <param name="URL">请求地址</param>
    /// <param name="filename">缓存的路径</param>
    public void DownloadFile(string URL, string filename)
    {
        try
        {
            float percent = 0;
            HttpWebRequest Myrq = (HttpWebRequest)HttpWebRequest.Create(URL);//请求网络资源
            HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();//返回Internet资源的响应
            long totalBytes = myrp.ContentLength;//获取请求返回内容的长度
            Stream st = myrp.GetResponseStream();//读取服务器的响应资源，以IO流的形式进行读写
            Stream so = new FileStream(filename, FileMode.Create);
            long totalDownloadedByte = 0;
            byte[] by = new byte[1024];
            int osize = st.Read(by, 0, (int)by.Length);
            while (osize > 0)
            {
                totalDownloadedByte = osize + totalDownloadedByte;
                so.Write(by, 0, osize);
                osize = st.Read(by, 0, (int)by.Length);//读取当前字节流的总长度
                percent = (float)totalDownloadedByte / (float)totalBytes * 100;
                double perNumber = Math.Round(percent);
            }
            so.Flush();
            so.Close();
            st.Close();
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnPiChuLi_Click(object sender, EventArgs e)
    {
        DateTime dt = DateTime.Now;
        BorrowService borrowService = new BorrowService();
        borrowService.Batch(dt.Date);
    }
    protected void btnHuiKuang_Click(object sender, EventArgs e)
    {
        WechatPushMessage wechatpushMessage = new WechatPushMessage();
        wechatpushMessage.CustomerReminder("oAh7kw24ZCfQCZyuHia7IOQ0Nd9A", "周生", "1元", "2019年10月29日");
    }
    protected void btnResult_Click(object sender, EventArgs e)
    {
        WechatPushMessage wechatpushMessage = new WechatPushMessage();
        wechatpushMessage.NotificationResult("oAh7kw24ZCfQCZyuHia7IOQ0Nd9A", "18824127298", "周生", "通过");
    }
    protected void btnRemind_Click(object sender, EventArgs e)
    {
        WechatPushMessage wechatpushMessage = new WechatPushMessage();
        wechatpushMessage.AuditProcessingReminder("oAh7kw24ZCfQCZyuHia7IOQ0Nd9A", "18824127298", "周生", "2019年10月16日");
    }


    //**********************请先填打印机编号和KEY，再测试**************************
    public static string USER = "357454@qq.com";  //*必填*：登录管理后台的账号名
    public static string UKEY = "zEfpmArLngwTyzVV";//*必填*: 注册账号后生成的UKEY
    public static string SN = "518503379";        //*必填*：打印机编号，必须要在管理后台里手动添加打印机或者通过API添加之后，才能调用API

    public static string URL = "http://api.feieyun.cn/Api/Open/";//不需要修改
    protected void btn_dayinji_Click(object sender, EventArgs e)
    {
        //==================方法1.打印订单==================
        //***返回值JSON字符串***
        //成功：{"msg":"ok","ret":0,"data":"xxxxxxx_xxxxxxxx_xxxxxxxx","serverExecutedTime":5}
        //失败：{"msg":"错误描述","ret":非0,"data":"null","serverExecutedTime":5}

        string method1 = print();
        System.Console.WriteLine(method1);
    }

    //方法1
    private static string print()
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

        string postData = "sn=" + SN;
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
    public static string sha1(string user, string ukey, string stime)
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


    private static int DateTimeToStamp(System.DateTime time)
    {
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); return (int)(time - startTime).TotalSeconds;
    }


}