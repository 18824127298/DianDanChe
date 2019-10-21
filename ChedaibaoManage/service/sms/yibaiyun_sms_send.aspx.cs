using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Yibai.Api.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yibai.Api.Domain;
using Yibai.Api;
using Yibai.Api.Common;


public partial class service_sms_yibaiyun_sms_send : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    
    {
        //string json = ("{" +
        //        "  'apikey':'f75306c4a60147689b9e630aa35db3c3'," + // 修改为您的apikey
        //        "  'submits':[{" +
        //        "    'mobile':'18824127298'," +  // 修改为您要发送的手机号
        //        "    'message':'【杰运好车】您的验证码是123321，请注意查收，谢谢'" + // 修改为您要发送的内容，内容必须和某个模板匹配
        //        "  }]" +
        //        "}"
        //    ).Replace('\'', '"');

        //HttpUtils.PostJson("https://sms.100sms.cn/api/sms/batchSubmit", json);



        TestSmsBatchSubmit();
    }
    public static void TestSmsBatchSubmit()
    {
        try
        {
            List<SmsSubmit> smsSubmits = new List<SmsSubmit>();
            SmsSubmit smsSubmit1 = new SmsSubmit("18824127298", "【杰运好车】您的验证码是123321，请注意查收，谢谢");
            smsSubmits.Add(smsSubmit1);
            String apikey = "f75306c4a60147689b9e630aa35db3c3";//修改为您的apikey
            string sjon = JsonConvert.SerializeObject(smsSubmits).Replace('\"', '\'');
            List<SmsSubmitJson> smsSubmitJsonList = new List<SmsSubmitJson>();
            SmsSubmitJson smsSubmitJson = new SmsSubmitJson(apikey,sjon);
            string sresult = JsonConvert.SerializeObject(smsSubmitJson).Replace('\'','"').Replace("\"[", "[").Replace("]\"","]");

            HttpUtils.PostJson("https://sms.100sms.cn/api/sms/batchSubmit", sresult);
        }
        catch (YibaiApiException e)
        {
            Console.WriteLine("YibaiApiException, code: " + e.code + ", message: " + e.message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            e.ToString();
        }

    }

}

