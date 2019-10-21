using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;
using Sigbit.Common;

namespace Sigbit.App.Net.IBXService.VCodeBreak.ThirdEngine.UUCloud
{
    public class BUUBreakEngine
    {
        private static BUUBreakEngine _instance;
        /// <summary>
        /// 静态实例
        /// </summary>
        public static BUUBreakEngine Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BUUBreakEngine();

                return _instance;
            }
        }

        bool _hasLogin = false;

        private bool LoginToUUCloud(ref string sErrorString)
        {
            if (_hasLogin)
                return true;

            int nSoftId = BUUBreakConfig.Instance.SoftID;
            string sSoftKey = BUUBreakConfig.Instance.SoftKey;
            string sUser = BUUBreakConfig.Instance.UserName;
            string sPassWord = BUUBreakConfig.Instance.UserPassword;

            int nUULoginRet;

            try
            {
                //================= 1. 设置软件的softId和softKey======================
                UUWiseCSWrapper.BUUWrapper.uu_setSoftInfo(nSoftId, sSoftKey);

                //=================2. 登陆 ==================
                nUULoginRet = UUWiseCSWrapper.BUUWrapper.uu_login(sUser, sPassWord);
            }
            catch (Exception ex)
            {
                sErrorString = "登录调用失败, ExceptionMessage:" + ex.Message;
                return false;
            }

            if (nUULoginRet < 5000)
            {
                //======= 3. 登陆结果判断，5000以下的都被用于作为错误标识 ================

                switch (nUULoginRet)
                {
                    case -1:
                        sErrorString = "软件ID或KEY无效或者用户名为空或密码为空(-1)";
                        break;
                    case -2:
                        sErrorString = "用户不存在(-2)";
                        break;
                    case -3:
                        sErrorString = "密码错误(-3)";
                        break;
                    case -4:
                        sErrorString = "账号被锁定(-4)";
                        break;
                    case -5:
                        sErrorString = "非法登录(-5)";
                        break;
                    case -6:
                        sErrorString = "用户点数不足，请及时充值(-6)";
                        break;
                    case -8:
                        sErrorString = "系统维护中(-8)";
                        break;
                    case -1001:
                        sErrorString = "连接失败(-1001)";
                        break;
                    case -1002:
                        sErrorString = "网络传输超时(-1002)";
                        break;
                    case -1003:
                        sErrorString = "文件访问失败(-1003)";
                        break;
                    case -1004:
                        sErrorString = "图像内存流无效(-1004)";
                        break;
                    case -1005:
                        sErrorString = "服务器返回内容错误(-1005)";
                        break;
                    case -1006:
                        sErrorString = "服务器状态错误(-1006)";
                        break;
                    case -1007:
                        sErrorString = "内存分配失败(-1007)";
                        break;
                    case -1008:
                        sErrorString = "没有取到验证码结果，返回此值指示codeID已返回(-1008)";
                        break;
                    case -1009:
                        sErrorString = "此时不允许进行该操作(-1009)";
                        break;
                    case -1010:
                        sErrorString = "图片过大，限制10MB(-1010)";
                        break;
                    default:
                        sErrorString = "登录结果失败，错误码:" + nUULoginRet.ToString();
                        break;
                }
                return false;
            }

            _hasLogin = true;
            return true;
        }

        /// <summary>
        /// 破解验证码
        /// </summary>
        /// <param name="req">破解请求</param>
        /// <returns>返回结果BUUBreakResult</returns>
        public BUUBreakResult VCodeBreak(BUUBreakRequest req)
        {
            BUUBreakResult result = new BUUBreakResult();

            //=========== 1. 登录调用 =============
            string sLoginErrorMessage = "";
            if (!LoginToUUCloud(ref sLoginErrorMessage))
            {
                result.ErrorCode = "100_login_fail";
                result.ErrorString = sLoginErrorMessage;
                return result;
            }

            //============ 2. 判断输入的完整性 ===========
            //========== 2.1 图像文件是否存在 ===========
            if (!File.Exists(req.ImageFileName))
            {
                result.ErrorCode = "101_image_file_not_exist";
                result.ErrorString = "待破解图像文件" + req.ImageFileName + "不存在";
                return result;
            }

            //========= 2.2 代码有效性 ============
            if (req.UUCodeType <= 1000)
            {
                result.ErrorCode = "102_uucode_type_invalid";
                result.ErrorString = "UUCodeType的值指定非法";
                return result;
            }

            //========== 3. 提交请求 =============
            StringBuilder sbResult = new StringBuilder();
            int nUUBreakResultServerUid = 0;

            try
            {
                nUUBreakResultServerUid = UUWiseCSWrapper.BUUWrapper.uu_recognizeByCodeTypeAndPath
                        (req.ImageFileName, req.UUCodeType, sbResult);
            }
            catch (Exception ex)
            {
                result.BreakResultText = "";
                result.ErrorCode = "158_call_fails";
                result.ErrorString = ex.Message;
                return result;
            }

            result.BreakResultText = sbResult.ToString();
            return result;
        }

        /// <summary>
        /// 识别错误处理
        /// </summary>
        /// <param name="nCodeId">UU云识别结果返回的ID</param>
        /// <remarks>
        /// 由外部程序调用。如果最终UU云的识别结果有出入，可以调用这个方法反馈。（可以退钱）
        /// </remarks>
        public void VReportError(int nUUBreakResultServerUid)
        {
            UUWiseCSWrapper.BUUWrapper.uu_reportError(nUUBreakResultServerUid);
        }

    }
}
