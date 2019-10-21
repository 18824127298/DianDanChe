using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;
using System.Net;
using System.Diagnostics;
using System.IO;

using Sigbit.Common;

namespace Sigbit.Net.WebVisitor
{
    class WVT__Task
    {
        private WVT__RequestList _requestList;
        /// <summary>
        /// 请求队列
        /// </summary>
        public WVT__RequestList RequestList
        {
            get { return _requestList; }
            set { _requestList = value; }
        }

        private WVT__ResponseList _responseList;
        /// <summary>
        /// 响应队列
        /// </summary>
        public WVT__ResponseList ResponseList
        {
            get { return _responseList; }
            set { _responseList = value; }
        }

        private int _visitorTaskSeq = 0;
        /// <summary>
        /// 访问线程的顺序号
        /// </summary>
        public int VisitorTaskSeq
        {
            get { return _visitorTaskSeq; }
            set { _visitorTaskSeq = value; }
        }

        bool _isTerminated = false;
        /// <summary>
        /// 是否已退出
        /// </summary>
        public bool IsTerminated
        {
            get { return _isTerminated; }
        }

        /// <summary>
        /// 退出应用
        /// </summary>
        public void Terminate()
        {
            _isTerminated = true;
        }

        /// <summary>
        /// 启动运行线程
        /// </summary>
        public void Start()
        {
            Thread visitorThread = new Thread(new ThreadStart(this.VisitorThread));
            visitorThread.Start();
        }

        /// <summary>
        /// 客户端处理线程
        /// </summary>
        private void VisitorThread()
        {
            //====== 1. 如果没有退出，则一直进行页面访问 ======
            while (!IsTerminated)
            {
                Thread.Sleep(10 + RandUtil.NewNumber(100));

                FetchAndVisitOnePage();
            }
        }

        /// <summary>
        /// 取出一个页面，并进行访问
        /// </summary>
        private void FetchAndVisitOnePage()
        {
            WVWebRequest request = _requestList.PopRequest();
            if (request == null)
                return;

            WVWebResponse response = ExecuteRequest(request);
            _responseList.PushResponse(response);
        }

        private WVWebResponse ExecuteRequest(WVWebRequest request)
        {
            //========== 0+. 字符集 ====================
            WVPageCharset charset = request.PageCharset;
            if (charset == WVPageCharset.Undefine)
                charset = WVWebVisitorConfig.Instance.DefaultCharset;

            Encoding thisEncoding = null;
            switch (charset)
            {
                case WVPageCharset.GB2312:
                    thisEncoding = Encoding.Default;
                    break;
                default:
                    thisEncoding = Encoding.UTF8;
                    break;
            }

            //============ 1. 准备请求 ============
            WVWebResponse response = new WVWebResponse();
            Stopwatch watch = new Stopwatch();
            watch.Start();

            response.ExecuteThreadNo = this._visitorTaskSeq;
            response.RequestId = request.RequestId;
            response.RequestObjectData = request.RequestObjectData;
            response.BeginVisitTime = DateTime.Now;

            //========== 1. 访问页面 ===========
            WebClient webClient = new WebClient();

            try
            {
                byte[] bsPageContent = webClient.DownloadData(request.VisitUrl);
                response.PageBytes = bsPageContent;
            }
            catch (WebException ex)
            {
                //Stream receviceStream = ex.Response.GetResponseStream();
                //StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding("gb2312"));
                //sHtmlContent = readerOfStream.ReadToEnd();

                //============ 【2013-08-01 再补, zick反馈】 ============
                if (ex.Response == null)
                {
                    watch.Stop();
                    response.TotalVisitMilliseconds = (int)watch.ElapsedMilliseconds;
                    response.PageBytes = new byte[0];
                    response.HasSuccess = false;
                    response.ErrorString = "WebException Error, but ex.Reponse = null. - "
                            + request.VisitUrl;
                    response.EndVisitTime = DateTime.Now;
                    return response;
                }

                //========== 【2013-06-24补】 对于例外情况的记录和处理 =============
                Stream receiveExStream = ex.Response.GetResponseStream();
                int nExRecvLength = (int)receiveExStream.Length;
                byte[] bsExRecvBytes = new byte[nExRecvLength];
                receiveExStream.Read(bsExRecvBytes, 0, nExRecvLength);

                watch.Stop();
                response.TotalVisitMilliseconds = (int)watch.ElapsedMilliseconds;
                response.PageBytes = bsExRecvBytes;
                response.HasSuccess = false;
                response.ErrorString = "页面访问WebEx例外" + request.VisitUrl;
                response.EndVisitTime = DateTime.Now;

                response.PageContentString = thisEncoding.GetString(response.PageBytes);

                return response;
            }
            catch
            {
                watch.Stop();
                response.TotalVisitMilliseconds = (int)watch.ElapsedMilliseconds;
                response.PageBytes = new byte[0];
                response.HasSuccess = false;
                response.ErrorString = "页面访问失败，可能是网页不存在，或地址指定错误 - "
                        + request.VisitUrl;
                response.EndVisitTime = DateTime.Now;
                return response;
            }

            //============== 2. 解析页面 =============

            //========== 2.2 按字符集得到页面字符串 ===========
            try
            {
                response.PageContentString = thisEncoding.GetString(response.PageBytes);
            }
            catch
            {
                watch.Stop();
                response.TotalVisitMilliseconds = (int)watch.ElapsedMilliseconds;
                response.HasSuccess = false;
                response.ErrorString = "页面字符集解析错误，可能是指定了错误的字符集 - "
                        + request.VisitUrl;
                response.EndVisitTime = DateTime.Now;
                return response;
            }

            watch.Stop();
            response.TotalVisitMilliseconds = (int)watch.ElapsedMilliseconds;
            response.HasSuccess = true;
            response.ErrorString = "";
            response.EndVisitTime = DateTime.Now;

            return response;
        }
    }
}
