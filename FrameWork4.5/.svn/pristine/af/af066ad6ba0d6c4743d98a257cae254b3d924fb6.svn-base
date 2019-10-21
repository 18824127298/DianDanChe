using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Sigbit.Net.SBTTcpServer
{
    public abstract class SBTTcpTask
    {
        private int _taskThreadSeq;
        /// <summary>
        /// 任务顺序号
        /// </summary>
        public int TaskThreadSeq
        {
            get { return _taskThreadSeq; }
            set { _taskThreadSeq = value; }
        }
        
        TcpListener _listenSocket;
        /// <summary>
        /// 监听Socket，用于接受连接
        /// </summary>
        public TcpListener ListenSocket
        {
            get { return _listenSocket; }
            set { _listenSocket = value; }
        }

        TcpClient _tcpClient = null;
        /// <summary>
        /// 提供客户端连接
        /// </summary>
        public TcpClient TcpClient
        {
            get { return _tcpClient; }
            set { _tcpClient = value; }
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
            Thread taskThread = new Thread(new ThreadStart(this.TaskThread));
            taskThread.Start();
        }

        /// <summary>
        /// 客户端处理线程
        /// </summary>
        private void TaskThread()
        {
            //====== 1. 如果没有退出，则一直接收连接，进行数据交换处理 ======
            while (!IsTerminated)
            {
                Thread.Sleep(100);

                //======= 2. 接受连接 ========
                lock (typeof(SBTTcpTask))
                {
                    if (_listenSocket.Pending())
                        _tcpClient = _listenSocket.AcceptTcpClient();
                }

                //======== 3. 数据交换处理 ========
                if (_tcpClient != null)
                {
                    ClientExecute();
                    _tcpClient.Close();
                }
                _tcpClient = null;
            }
        }

        /// <summary>
        /// 数据交换处理，继承类中重载该方法。
        /// </summary>
        public abstract void ClientExecute();
    }
}
