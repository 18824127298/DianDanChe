using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Framework
{
    public enum SbtTaskRunningResult
    {
        Finish,
        FinishWithNoLog,
        Processing
    }

    /// <summary>
    /// ���ĺ�̨������Ӵ�������
    /// </summary>
    public abstract class SbtTaskDaemonBase
    {
        /// <summary>
        /// ���к�̨����
        /// </summary>
        /// <returns>��̨����Ľ��</returns>
        public abstract SbtTaskRunningResult DoProcess();

        private string _daemonType = "";
        /// <summary>
        /// ��̨���������
        /// </summary>
        public string DaemonType
        {
            get { return _daemonType; }
            set { _daemonType = value; }
        }

        private string _taskName = "";
        /// <summary>
        /// ��ǰ���������
        /// </summary>
        public string TaskName
        {
            get { return _taskName; }
            set { _taskName = value; }
        }

        private string _taskParameter = "";
        /// <summary>
        /// ��ǰ����Ĳ���
        /// </summary>
        public string TaskParameter
        {
            get { return _taskParameter; }
            set { _taskParameter = value; }
        }

        private int _taskSize;
        /// <summary>
        /// ��ǰ����Ĺ�ģ
        /// </summary>
        public int TaskSize
        {
            get { return _taskSize; }
            set { _taskSize = value; }
        }

        private int _taskPos;
        /// <summary>
        /// ��ǰ����Ľ���
        /// </summary>
        public int TaskPos
        {
            get { return _taskPos; }
            set { _taskPos = value; }
        }

        private string _taskMsg = "";
        /// <summary>
        /// ��ǰ�������ʾ��Ϣ
        /// </summary>
        public string TaskMsg
        {
            get { return _taskMsg; }
            set { _taskMsg = value; }
        }

        private int _batchSize = -1;
        /// <summary>
        /// һ�����δ���Ĺ�ģ
        /// </summary>
        public int BatchSize
        {
            get { return _batchSize; }
            set { _batchSize = value; }
        }

    }
}
