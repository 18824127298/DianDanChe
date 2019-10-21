using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;

namespace Sigbit.Net.WebVisitor
{
    /// <summary>
    /// ��վ���̷߳���
    /// </summary>
    public class WVWebVisitor
    {
        private static WVWebVisitor _thisInstance = null;
        /// <summary>
        /// Ψһʵ��������Ӧ�õ������Ӧ��Ҳ���Բ������Ψһʵ�����򴴽��µ�ʵ����
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
        /// �����̳߳�
        /// </summary>
        WVT__TaskList _visitorTaskList = new WVT__TaskList();

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        private bool _bVisitorHasStarted = false;

        /// <summary>
        /// �������
        /// </summary>
        private WVT__RequestList _requestList = new WVT__RequestList();

        /// <summary>
        /// ��Ӧ����
        /// </summary>
        private WVT__ResponseList _responseList = new WVT__ResponseList();

        /// <summary>
        /// �������߳���ҳ����
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
        /// ֹͣ���߳���ҳ����
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
        /// ѹ���µķ�������
        /// </summary>
        /// <param name="request"></param>
        public void PushRequest(WVWebRequest request)
        {
            _requestList.PushRequest(request);
        }

        /// <summary>
        /// �õ����ʽ������Ҫ���ⲿѭ����ʱ���ã���û�н���򷵻�null��
        /// </summary>
        /// <returns></returns>
        public WVWebResponse PopResponse()
        {
            WVWebResponse response = _responseList.PopResponse();
            return response;
        }
    }
}
