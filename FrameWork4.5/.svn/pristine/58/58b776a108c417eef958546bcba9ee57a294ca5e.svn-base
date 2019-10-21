using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;

namespace Sigbit.Net.WebVisitor
{
    /// <summary>
    /// 网站多线程访问
    /// </summary>
    public class WVWebVisitor
    {
        private static WVWebVisitor _thisInstance = null;
        /// <summary>
        /// 唯一实例。根据应用的情况，应用也可以不用这个唯一实例，或创建新的实例。
        /// </summary>
        public static WVWebVisitor Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new WVWebVisitor();
                return _thisInstance;
            }
        }

        /// <summary>
        /// 访问线程池
        /// </summary>
        WVT__TaskList _visitorTaskList = new WVT__TaskList();

        /// <summary>
        /// 是否已启动
        /// </summary>
        private bool _bVisitorHasStarted = false;

        /// <summary>
        /// 请求队列
        /// </summary>
        private WVT__RequestList _requestList = new WVT__RequestList();

        /// <summary>
        /// 响应队列
        /// </summary>
        private WVT__ResponseList _responseList = new WVT__ResponseList();

        /// <summary>
        /// 启动多线程网页访问
        /// </summary>
        public void StartVisitor()
        {
            if (_bVisitorHasStarted)
                return;
            _bVisitorHasStarted = true;

            _requestList.Clear();
            _responseList.Clear();

            for (int i = 0; i < WVWebVisitorConfig.Instance.VisitThreadCount; i++)
            {
                WVT__Task task = new WVT__Task();
                task.RequestList = _requestList;
                task.ResponseList = _responseList;
                task.VisitorTaskSeq = i;
                task.Start();

                lock (this)
                {
                    _visitorTaskList.AddTask(task);
                }    

                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// 停止多线程网页访问
        /// </summary>
        public void StopVisitor()
        {
            if (!_bVisitorHasStarted)
                return;
            _bVisitorHasStarted = false;

            int nThreadCount = WVWebVisitorConfig.Instance.VisitThreadCount;

            for (int i = nThreadCount - 1; i >= 0; i--)
            {
                WVT__Task task = _visitorTaskList.GetTask(i);

                task.Terminate();
                _visitorTaskList.RemoveAt(i);
            }
            Thread.Sleep(500);
        }

        /// <summary>
        /// 压入新的访问请求
        /// </summary>
        /// <param name="request"></param>
        public void PushRequest(WVWebRequest request)
        {
            _requestList.PushRequest(request);
        }

        /// <summary>
        /// 得到访问结果。需要在外部循环或定时调用，如没有结果则返回null。
        /// </summary>
        /// <returns></returns>
        public WVWebResponse PopResponse()
        {
            WVWebResponse response = _responseList.PopResponse();
            return response;
        }
    }
}
