using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

using Sigbit.Common;
using Sigbit.Common.Encrypt;

using Sigbit.App.Net.WeiXinService.Client;
using Sigbit.App.Net.WeiXinService.Message;
using Sigbit.App.Net.WeiXinService.Event;
using Sigbit.App.Net.WeiXinService.JSON;
using Sigbit.App.Net.WeiXinService.User;
using System.Security.Cryptography;

namespace Sigbit.App.Net.WeiXinService
{
    public class WeiXinEngineBase
    {

        #region 通用接收处理

        /// <summary>
        /// 处理请求
        /// </summary>
        public void ProcessRequest()
        {
            HttpRequest Request=HttpContext.Current.Request;

            if (Request.HttpMethod.ToUpper() == "POST")
            {
                PostProcess();
            }
            else
            {
                if (Request.QueryString.AllKeys.Contains("echostr"))
                {
                    Auth();
                }
                else if (Request.QueryString.AllKeys.Contains("code")
                    && Request.QueryString.AllKeys.Contains("state"))
                {
                    //获取用户信息时的回调处理　

                    OAuthCallback();
                }
                else
                {
                    DebugLogger.LogDebugMessage("未处理的URL请求:" + Request.Url.AbsoluteUri);
                }

            }
        }

        private string GetLogFileName(string sPostFix)
        {
            string sFileName = DateTimeUtil.Now.Replace("-", "").Replace(":", "").Replace(" ", "-");
            sFileName += "-" + RandUtil.NewString(3, RandStringType.Lower) + "-" + sPostFix + ".log";

            sFileName = "c:\\temp\\" + sFileName;

            return sFileName;
        }


        /// <summary>
        /// 获取用户信息的回调处理
        /// </summary>
        /// <returns></returns>
        public virtual TWJOAuth2ResultResp OAuthCallback()
        {
            TWJOAuth2ResultResp resp = new TWJOAuth2ResultResp();

            string sCode = HttpContext.Current.Request.QueryString["CODE"];
            string sState = HttpContext.Current.Request.QueryString["state"];

            string sEnState = HttpContext.Current.Request.QueryString["enstate"];

            DebugLogger.LogDebugMessage("收到的回调链接为:" + HttpContext.Current.Request.Url.AbsoluteUri);

            //============== Code和State的校验 ==============

            string sDesState = EncryptUtil.DesDecodeString(sEnState, "ieosid");

            if (sDesState != sState)
            {
                resp.ErrCode = -100;
                resp.ErrMsg = "无效的STATE较验码";
                return resp;
            }


            TWJOAuth2ResultReq req = new TWJOAuth2ResultReq();
            req.AppId = TWXWeiXinClientConfig.Instance.AppID;
            req.Secret = TWXWeiXinClientConfig.Instance.AppSecret;
            req.Code = sCode;
            req.GrantType = TWJOAuth2ResultReqEGrantType.AuthorizationCode;

            resp = (TWJOAuth2ResultResp)TWXWeiXinClient.Instance.DealJsonRequest(req, resp);

            return resp;

        }


        #endregion

        #region GET请求处理

        /// <summary>
        /// 验证并相应服务器的数据
        /// </summary>
        private void Auth()
        {
            string sToken = TWXWeiXinClientConfig.Instance.Token;

            if (sToken == "")
            {
                throw new Exception("未配置微信Token");
            }

            string sEchoString = HttpContext.Current.Request.QueryString["echoStr"];
            string sSignature = HttpContext.Current.Request.QueryString["signature"];
            string sTimeStamp = HttpContext.Current.Request.QueryString["timestamp"];
            string sNonce = HttpContext.Current.Request.QueryString["nonce"];

            if (CheckSignature(sToken, sSignature, sTimeStamp, sNonce))
            {
                if (!string.IsNullOrEmpty(sEchoString))
                {
                    HttpContext.Current.Response.Write(sEchoString);
                    HttpContext.Current.Response.End();
                }
            }
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        private bool CheckSignature(string sToken, string sSignature, string sTimeStamp, string sNonce)
        {
            string[] arrTmp = { sToken, sTimeStamp, sNonce };

            Array.Sort(arrTmp);

            string sTempStr = string.Join("", arrTmp);

            sTempStr = FormsAuthentication.HashPasswordForStoringInConfigFile(sTempStr, "SHA1");
            sTempStr = sTempStr.ToLower();

            //var sha1 = SHA1.Create();
            //var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(sTempStr));
            //StringBuilder sbText = new StringBuilder();
            //foreach (var b in sha1Arr)
            //{
            //    sbText.AppendFormat("{0:x2}", b);
            //}

            //sTempStr = sbText.ToString();

            if (sTempStr == sSignature)
            {
                return true;
            }
            else
            {
                return false;
            }



        }


        #endregion

        #region POST请求处理

        private void PostProcess()
        {
            HttpRequest Request = HttpContext.Current.Request;
            HttpResponse Response = HttpContext.Current.Response;

            try
            {
                Response.Cache.SetNoStore();
                Response.ContentEncoding = System.Text.Encoding.UTF8;

                //=========== 1. 读取请求, 并解析请求 ==========
                byte[] bsInput = new byte[Request.InputStream.Length];

                if (bsInput.Length == 0)
                    return;

                Request.InputStream.Read(bsInput, 0, bsInput.Length);

                FileUtil.WriteBytesToFile(GetLogFileName("recv"), bsInput);

                TWXMessageBaseReq reqMessage = TWXMessageConvert.BytesToReqMsg(bsInput);

                //using (Stream stream = HttpContext.Current.Request.InputStream)
                //{
                //    Byte[] btRecvBytes = new Byte[stream.Length];
                //    stream.Read(btRecvBytes, 0, (Int32)stream.Length);

                //    reqMessage = TWXMessageConvert.BytesToReqMsg(btRecvBytes);

                //}

                if (reqMessage == null)
                    return;

                TWXMessageBaseResp respMessage = null;

                switch (reqMessage.MsgType)
                {
                    case MsgType.Text:
                        respMessage = OnTextMessageReq(reqMessage as TWXTextMessageReq);
                        break;
                    case MsgType.Image:
                        respMessage = OnImageMessageReq(reqMessage as TWXImageMessageReq);
                        break;
                    case MsgType.Voice:
                        respMessage = OnVoiceMessageReq(reqMessage as TWXVoiceMessageReq);
                        break;
                    case MsgType.Video:
                        respMessage = OnVideoMessageReq(reqMessage as TWXVideoMessageReq);
                        break;
                    case MsgType.Shortvideo:
                        respMessage = OnShortvideoMessageReq(reqMessage as TWXShortvideoMessageReq);
                        break;
                    case MsgType.Location:
                        respMessage = OnLocationMesssageReq(reqMessage as TWXLocationMessageReq);
                        break;
                    case MsgType.Link:
                        respMessage = OnLinkMessageReq(reqMessage as TWXLinkMessageReq);
                        break;
                    case MsgType.Event:

                        TWXEventBaseReq reqEvent = reqMessage as TWXEventBaseReq;

                        switch (reqEvent.Event)
                        {
                            case TWXEvent.Subscribe:

                                TWXSubscribeEvent eventSubscribe = reqEvent as TWXSubscribeEvent;

                                if (eventSubscribe.IsQRScene)
                                    respMessage = OnQRSceneSubScribeEventReq(reqEvent as TWXQRsceneSubscribeEvent);
                                else
                                    respMessage = OnSubScribeEventReq(eventSubscribe);
                                break;
                            case TWXEvent.Scan:
                                respMessage = OnScanEventReq(reqEvent as TWXScanEvent);
                                break;
                            case TWXEvent.Unsubscribe:
                                respMessage = OnUnsubScribeEventReq(reqEvent as TWXUnsubscribeEvent);
                                break;
                            case TWXEvent.View:
                                respMessage = OnMenuViewEventReq(reqEvent as TWXViewEvent);
                                break;
                            case TWXEvent.Click:
                                respMessage = OnMenuClickEventReq(reqEvent as TWXClickEvent);
                                break;
                            case TWXEvent.Location:
                                respMessage = OnLocationEventReq(reqEvent as TWXLocationEvent);
                                break;
                        }

                        break;
                    default:
                        break;

                }

                if (respMessage != null)
                {

                    //========= 4. 响应结果 =========

                    string sPageAnswerContent = respMessage.ToString();

                    FileUtil.WriteStringToFile(GetLogFileName("resp"), sPageAnswerContent);

                    Response.Write(sPageAnswerContent);
                }
                else
                {
                    respMessage = OnMessageReq(reqMessage);

                    if (respMessage != null)
                    {
                        string sPageAnswerContent = respMessage.ToString();

                        FileUtil.WriteStringToFile(GetLogFileName("resp"), sPageAnswerContent);

                        Response.Write(sPageAnswerContent);
                    }
                }
            }
            catch (Exception ex)
            {
                string sMessage = ex.Message + "\r\n" + ex.StackTrace;
                FileUtil.WriteStringToFile(GetLogFileName("error"), sMessage);

                throw ex;
            }

            Response.End();
        }

        #endregion

        #region 被动接收消息处理


        public virtual TWXMessageBaseResp OnMessageReq(TWXMessageBaseReq req)
        {
            return null;
        }


        public virtual TWXMessageBaseResp OnTextMessageReq(TWXTextMessageReq req)
        {
            return null;
        }

        public virtual TWXMessageBaseResp OnImageMessageReq(TWXImageMessageReq req)
        {
            return null;
        }


        public virtual TWXMessageBaseResp OnVoiceMessageReq(TWXVoiceMessageReq req)
        {
            return null;
        }


        public virtual TWXMessageBaseResp OnVideoMessageReq(TWXVideoMessageReq req)
        {
            return null;
        }


        public virtual TWXMessageBaseResp OnShortvideoMessageReq(TWXShortvideoMessageReq req)
        {
            return null;
        }

        public virtual TWXMessageBaseResp OnLocationMesssageReq(TWXLocationMessageReq req)
        {
            return null;
        }


        public virtual TWXMessageBaseResp OnLinkMessageReq(TWXLinkMessageReq req)
        {
            return null;
        }



        #endregion

        #region 被动接收事件处理

        /// <summary>
        /// 用户订阅时的事件推送
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual TWXMessageBaseResp OnSubScribeEventReq(TWXSubscribeEvent req)
        {
            return null;
        }

        /// <summary>
        /// 未关注时扫描带参数二维码事件
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual TWXMessageBaseResp OnQRSceneSubScribeEventReq(TWXQRsceneSubscribeEvent req)
        {
            return null;
        }


        /// <summary>
        /// 用户已关注时的事件推送
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual TWXMessageBaseResp OnScanEventReq(TWXScanEvent req)
        {
            return null;
        }


        public virtual TWXMessageBaseResp OnUnsubScribeEventReq(TWXUnsubscribeEvent req)
        {
            return null;
        }


        public virtual TWXMessageBaseResp OnLocationEventReq(TWXLocationEvent req)
        {
            return null;
        }

        public virtual TWXMessageBaseResp OnMenuClickEventReq(TWXClickEvent req)
        {
            return null;
        }


        public virtual TWXMessageBaseResp OnMenuViewEventReq(TWXViewEvent req)
        {
            return null;
        }



        #endregion

        #region 主动请求处理

        /// <summary>
        /// 发送用户认证请求
        /// </summary>
        /// <param name="req"></param>
        public virtual void SendOAuthReq(TWJOAuth2RedirectReq req)
        {
            if (req.AppId == "")
            {
                req.AppId = TWXWeiXinClientConfig.Instance.AppID;
            }

            if (req.State == "")
            {
                req.State = RandUtil.NewString(10, RandStringType.LowerNumber);
            }

            if (req.RedirectUri == "")
            {
                req.RedirectUri = HttpContext.Current.Request.Url.AbsoluteUri;
            }

            if (req.RedirectUri.Contains("?"))
            {
                req.RedirectUri += "&enstate=" + EncryptUtil.DesEncodeString(req.State, "ieosid");
            }
            else
            {
                req.RedirectUri += "?enstate=" + EncryptUtil.DesEncodeString(req.State, "ieosid");
            }


            TWJRespBase resp = new TWJRespBase();

            TWXWeiXinClient.Instance.DealJsonRequest(req, resp);


        }



        #endregion

    }
}
