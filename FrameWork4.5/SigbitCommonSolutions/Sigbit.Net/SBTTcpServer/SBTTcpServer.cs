using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;

namespace Sigbit.Net.SBTTcpServer
{
    /// <summary>
    /// ʵ��TCP����ķ�װ����װ���̳߳ء�
    /// </summary>
    public abstract class SBTTcpServer
    {
        /// <summary>
        /// �������ӵ�Socket
        /// </summary>
        private TcpListener _listenSock;

        /// <summary>
        /// �̳߳�
        /// </summary>
        private ArrayList _taskThreadList = new ArrayList();
        
        string _hostName;
        /// <summary>
        /// ������
        /// </summary>
        public string HostName
        {
            get { return _hostName; }
            set { _hostName = value; }
        }

        string _service;
        /// <summary>
        /// ������
        /// </summary>
        public string Service
        {
            get { return _service; }
            set { _service = value; }
        }

        int _port;
        /// <summary>
        /// �˿ں�
        /// </summary>
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        int _queueSize;
        /// <summary>
        /// Listen�Ķ��г���
        /// </summary>
        public int QueueSize
        {
            get { return _queueSize; }
            set { _queueSize = value; }
        }

        int _minThread;
        /// <summary>
        /// �����߳���
        /// </summary>
        public int MinThread
        {
            get { return _minThread; }
            set { _minThread = value; }
        }

        int _maxThread;
        /// <summary>
        /// ����߳���
        /// </summary>
        public int MaxThread
        {
            get { return _maxThread; }
            set { _maxThread = value; }
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        public SBTTcpServer()
        {
            _queueSize = 5;
            _minThread = 10;
            _maxThread = 10;
        }

        public abstract SBTTcpTask GetNewTask();

        /// <summary>
        /// �̵߳������к����������̳߳���������
        /// </summary>
        public void Start()
        {
            SBTTcpTask task;

            //===== 1. ��ʼ���� =====
            IPAddress localAddr = IPAddress.Parse(_hostName);
            _listenSock = new TcpListener(localAddr, _port);
            _listenSock.Start(_queueSize);

            //===== 2. �����̳߳� ======
            for (int i = 0; i < _minThread; i++)
            {
                task = GetNewTask();
                task.TaskThreadSeq = i;
                task.ListenSocket = _listenSock;
                task.Start();
                lock (this)
                {
                    _taskThreadList.Add(task);
                }
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// ֹͣ����ֹͣ�����߳�
        /// </summary>
        public void Stop()
        {
            for (int i = _minThread - 1; i >= 0; i--)
            {
                SBTTcpTask task = (SBTTcpTask)_taskThreadList[i];

                task.Terminate();
                _taskThreadList.RemoveAt(i);
            }
            Thread.Sleep(500);
            _listenSock.Stop();
        }
    }
}
