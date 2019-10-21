using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Sigbit.App.Net.IBXService.VCodeBreak.YunNan
{
    public class BYNBreakEngine
    {
        private static BYNBreakEngine _instance;
        /// <summary>
        /// 静态实例
        /// </summary>
        public static BYNBreakEngine Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BYNBreakEngine();

                return _instance;
            }
        }

        /// <summary>
        /// 破解验证码
        /// </summary>
        /// <param name="req">破解请求</param>
        /// <returns>破解结果</returns>
        public BYNBreakResult VCodeBreak(BYNBreakRequest req)
        {
            BYNBreakResult result = new BYNBreakResult();
            string sBreakResult = "";
            Bitmap bmpCode=null;

            try
            {
                //====== (1).获取图片文件 ==================
                bmpCode = new Bitmap(req.ImageFileName);
            }
            catch (Exception ex)
            {
                result.BreakResultText = "";
                result.ErrorCode = "Source Image Error!";
                result.ErrorString = "获取验证码图片出错！";
                return result;
            }

            //========= (2).识别验证码 ======================
            sBreakResult = BreakCode_YN.Identify(bmpCode);

            result.BreakResultText = sBreakResult;
            return result;
        }
    }
}
