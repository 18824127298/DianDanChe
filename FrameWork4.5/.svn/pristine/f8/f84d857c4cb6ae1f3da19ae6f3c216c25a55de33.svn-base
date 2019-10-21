using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;
using Sigbit.Common;

namespace Sigbit.Framework
{
    /// <summary>
    /// 任务调度处理
    /// </summary>
    /// <remarks>
    /// 本次完成暂未考虑以下内容：
    /// 1. 大任务的分批执行，包括BatchSize, 暂停等；
    /// 2. 任务优先级的判断和处理；
    /// </remarks>
    public class SbtTaskScheduler : IDisposable
    {
        private static SbtTaskScheduler _thisIntance = null;
        /// <summary>
        /// 返回唯一的实例
        /// </summary>
        public static SbtTaskScheduler Instance
        {
            get
            {
                if (_thisIntance == null)
                    _thisIntance = new SbtTaskScheduler();

                return _thisIntance;
            }
        }

        public void Dispose()
        {
            DoApplicationEnd();
        }

        /// <summary>
        /// 线程句柄
        /// </summary>
        private Thread _taskThread = null;

        /// <summary>
        /// 应用启动时的调用处理
        /// </summary>
        public void DoApplicationStart()
        {
            AppPath.GetCurrentVirtualPath();

            if (_taskThread != null)
                return;

            TbSysDaemonTask.TruncateTable();

            ThreadStart threadStart = new ThreadStart(this.TaskThread);
            _taskThread = new Thread(threadStart);
            _taskThread.Start();
        }

        /// <summary>
        /// 应用退出时的调用处理
        /// </summary>
        public void DoApplicationEnd()
        {
            if (_taskThread != null)
            {
                _taskThread.Abort();
                _taskThread.Join();
            }
        }

        /// <summary>
        /// 任务线程的处理函数（主函数）
        /// </summary>
        private void TaskThread()
        {
            //====== 1. 如果任务表中没有数据，则扫描任务安排表，========
            //======    并安排下一个要完成的任务 ============
            TaskThread__Init();

            int nNextSleepDuration = -1;
            //======== 2. 循环执行，逐一处理任务 ===========
            while (true)
            {
                if (nNextSleepDuration < 0)
                    Thread.Sleep(SbtFrameworkConfig.Instance.TaskQueryTableTimerInterval * 1000);
                else
                    Thread.Sleep(nNextSleepDuration);

                    nNextSleepDuration = DoOneTask();
            }
        }

        /// <summary>
        /// 处理一个任务
        /// </summary>
        /// <returns>下一次任务等待的时间间隔</returns>
        private int DoOneTask()
        {
            SbtTaskProcess taskProcess = new SbtTaskProcess();
            return taskProcess.DoOneTask();
        }

        /// <summary>
        /// 线程初始化时所做的工作。主要是如果任务表中没有数据，
        /// 则扫描任务安排表，并安排下一个要完成的任务。用于在仅
        /// 初始化了定义表后，进行任务的首次调度。
        /// </summary>
        /// <remarks>
        /// 以后可能需要做两个工作:
        /// 1. 维护界面中，在新增或调整了一个任务的定义（调度规则）
        /// 后，进行任务的重新安排；
        /// 2. 维护界面中，增加任务的根据规则刷新、查错功能。
        /// </remarks>
        private void TaskThread__Init()
        {
            //===== 1. 获得任务表中的记录数，如有记录，就不做初始化工作 ===
            TbSysDaemonTask tbl = new TbSysDaemonTask();
            int nRecordCount = tbl.RecordCount();

            if (nRecordCount > 0)
                return;

            //============ 2. 增加任务安排 ========
            SbtTaskRuleLib.Instance.InitTaskTableByRule();
        }
    }
}
