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
    /// 实现TCP服务的封装，封装了线程池。
    /// </summary>
    public abstract class SBTTcpServer
    {
        /// <summary>
        /// 侦听连接的Socket
        /// </summary>
        private TcpListener _listenSock;

        /// <summary>
        /// 线程池
        /// </summary>
        private ArrayList _taskThreadList = new ArrayList();
        
        string _hostName;
        /// <summary>
        /// 主机名
        /// </summary>
        public string HostName
        {
            get { return _hostName; }
            set { _hostName = value; }
        }

        string _service;
        /// <summary>
        /// 服务名
        /// </summary>
        public string Service
        {
            get { return _service; }
            set { _service = value; }
        }

        int _port;
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        int _queueSize;
        /// <summary>
        /// Listen的队列长度
        /// </summary>
        public int QueueSize
        {
            get { return _queueSize; }
            set { _queueSize = value; }
        }

        int _minThread;
        /// <summary>
        /// 最少线程数
        /// </summary>
        public int MinThread
        {
            get { return _minThread; }
            set { _minThread = value; }
        }

        int _maxThread;
        /// <summary>
        /// 最大线程数
        /// </summary>
        public int MaxThread
        {
            get { return _maxThread; }
            set { _maxThread = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SBTTcpServer()
        {
            _queueSize = 5;
            _minThread = 10;
            _maxThread = 10;
        }

        public abstract SBTTcpTask GetNewTask();

        /// <summary>
        /// 线程的主运行函数，创建线程池启动服务
        /// </summary>
        public void Start()
        {
            SBTTcpTask task;

            //===== 1. 开始侦听 =====
            IPAddress localAddr = IPAddress.Parse(_hostName);
            _listenSock = new TcpListener(localAddr, _port);
            _listenSock.Start(_queueSize);

            //===== 2. 启动线程池 ======
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
        /// 停止服务，停止所有线程
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
