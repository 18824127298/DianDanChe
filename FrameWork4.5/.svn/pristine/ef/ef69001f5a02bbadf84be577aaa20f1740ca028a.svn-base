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
    /// 所的后台处理类从此类派生
    /// </summary>
    public abstract class SbtTaskDaemonBase
    {
        /// <summary>
        /// 运行后台处理
        /// </summary>
        /// <returns>后台处理的结果</returns>
        public abstract SbtTaskRunningResult DoProcess();

        private string _daemonType = "";
        /// <summary>
        /// 后台处理的类型
        /// </summary>
        public string DaemonType
        {
            get { return _daemonType; }
            set { _daemonType = value; }
        }

        private string _taskName = "";
        /// <summary>
        /// 当前任务的名称
        /// </summary>
        public string TaskName
        {
            get { return _taskName; }
            set { _taskName = value; }
        }

        private string _taskParameter = "";
        /// <summary>
        /// 当前任务的参数
        /// </summary>
        public string TaskParameter
        {
            get { return _taskParameter; }
            set { _taskParameter = value; }
        }

        private int _taskSize;
        /// <summary>
        /// 当前任务的规模
        /// </summary>
        public int TaskSize
        {
            get { return _taskSize; }
            set { _taskSize = value; }
        }

        private int _taskPos;
        /// <summary>
        /// 当前任务的进度
        /// </summary>
        public int TaskPos
        {
            get { return _taskPos; }
            set { _taskPos = value; }
        }

        private string _taskMsg = "";
        /// <summary>
        /// 当前任务的提示消息
        /// </summary>
        public string TaskMsg
        {
            get { return _taskMsg; }
            set { _taskMsg = value; }
        }

        private int _batchSize = -1;
        /// <summary>
        /// 一个批次处理的规模
        /// </summary>
        public int BatchSize
        {
            get { return _batchSize; }
            set { _batchSize = value; }
        }

    }
}
