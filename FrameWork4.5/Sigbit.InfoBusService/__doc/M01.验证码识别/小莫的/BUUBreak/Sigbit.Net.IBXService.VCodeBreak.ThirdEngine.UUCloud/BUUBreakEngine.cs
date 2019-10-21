using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;
using Sigbit.Common;

namespace Sigbit.Net.IBXService.VCodeBreak.ThirdEngine.UUCloud
{
    public class BUUBreakEngine
    {
        private BUUBreakEngine()
        {
            //string sConfigFileName = AppPath.AppFullPath("config", "Sigbit.Net.IBXService.VCodeBreak.ThirdEngine.UUCloud.config");
            //ConfigBase config = new ConfigBase();
            //config.LoadFromFile(sConfigFileName);
            //string sSoftId = config.GetString("softInfo", "SoftId");
            //string sSoftKey = config.GetString("softInfo", "SoftKey");
            //string sUser = config.GetString("userInfo", "User");
            //string sPassWord = config.GetString("userInfo", "PassWord");

            string sSoftId = ConfigurationManager.AppSettings["SoftId"];
            string sSoftKey = ConfigurationManager.AppSettings["SoftKey"];
            string sUser = ConfigurationManager.AppSettings["User"];
            string sPassWord = ConfigurationManager.AppSettings["PassWord"];


            try
            {
                int nCodeId;

                //=================设置软件的softId和softKey======================
                UUWiseCSWrapper.BUUWrapper.uu_setSoftInfo(int.Parse(sSoftId), sSoftKey);

                //=================登陆
                nCodeId = UUWiseCSWrapper.BUUWrapper.uu_login(sUser, sPassWord);
                if (nCodeId < 5000)
                {//=======登陆结果判断，5000以下的都被用于作为错误标识================

                    throw new Exception("登陆失败:" + nCodeId.ToString());
                }
            }
            catch
            {
            }
        }

        private static BUUBreakEngine _instance;
        /// <summary>
        /// 静态实例
        /// </summary>
        public static BUUBreakEngine Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BUUBreakEngine();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 破解验证码
        /// </summary>
        /// <param name="req">破解请求</param>
        /// <returns>返回结果BUUBreakResult</returns>
        public BUUBreakResult VCodeBreak(BUUBreakRequest req)
        {
            string sImgPath = req.ImageFileName;
            BUUBreakResult result = new BUUBreakResult();
            try
            {
                if (!File.Exists(sImgPath))
                {
                    throw new Exception("Image File Not Exists!");
                }
                int nCodeType = int.Parse(req.UUCodeType);
                StringBuilder sbResult = new StringBuilder();
                int nCodeId = 0;
                nCodeId = UUWiseCSWrapper.BUUWrapper.uu_recognizeByCodeTypeAndPath(sImgPath, nCodeType, sbResult);

                result.BreakResult = sbResult.ToString();
                result.ErrorCode = nCodeId.ToString();
                return result;
            }
            catch (Exception ex)
            {
                result.BreakResult = "";
                result.ErrorCode = "0";
                result.ErrorString = ex.Message;
                return result;
            }
        }

        /// <summary>
        /// 识别错误处理
        /// </summary>
        /// <param name="nCodeId">UU云识别结果返回的ID</param>
        public void VReportError(int nCodeId)
        {
            if (nCodeId != 0)
            {
                UUWiseCSWrapper.BUUWrapper.uu_reportError(nCodeId);
            }
        }

    }
}
