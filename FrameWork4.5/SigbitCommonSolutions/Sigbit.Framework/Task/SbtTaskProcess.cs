using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;
using Sigbit.Common;

namespace Sigbit.Framework
{
    class SbtTaskProcess
    {
        /// <summary>
        /// 处理一个任务
        /// </summary>
        /// <returns>
        /// 下一个任务需要等待的时间间隔（以毫秒为单位）。
        /// 如为-1，表示按缺省的配置。
        /// </returns>
        public int DoOneTask()
        {
            //======= 1. 取出最早的记录 ============
            TbSysDaemonTask tblTask = new TbSysDaemonTask();
            bool bFindTask = tblTask.FetchEarliestRecord();

            //======== 2. 如果找不到，就直接返回 ==========
            if (!bFindTask)
                return -1;

            //========= 3. 如果记录的时间早于当前的时间，则可执行该任务 =======
            string dtNow = DateTimeUtil.Now;
            int nSecondsAfter = DateTimeUtil.SecondsAfter(dtNow, tblTask.PlanTime);
            if (nSecondsAfter <= 0)
            {
                ProcessTaskRecord(tblTask);
                return 50;
            }
            else
            {
                tblTask.TaskStatus = TbSysDaemonTaskFTaskStatus.Idle;
                tblTask.Update();

                //========= 4. 如果记录的时间晚于当前的时间，判断相差的秒数 =======
                //=========    如果秒数小于缺省时间，则等待到计划的时间 =========
                if (nSecondsAfter < SbtFrameworkConfig.Instance.TaskQueryTableTimerInterval)
                    return nSecondsAfter * 1000;
                else
                    return -1;
            }
        }

        /// <summary>
        /// 处理相应的任务
        /// </summary>
        /// <param name="tblTask">任务记录</param>
        private void ProcessTaskRecord(TbSysDaemonTask tblTask)
        {
            string dtLogStartTime = DateTimeUtil.Now;

            //========== 1. 得到相应的任务规则 ==========
            SbtTaskRuleItem ruleItem = SbtTaskRuleLib.Instance.GetRuleItem(tblTask.DaemonType);

            //========== 2. 利用反射得到相应的运行实例 =========
            string sBinDirName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
            sBinDirName += "\\bin";

            Assembly assem = Assembly.LoadFrom(sBinDirName + "\\" + ruleItem.DaemonComponent + ".dll");
            Type type = assem.GetType(ruleItem.DaemonComponent + "." + ruleItem.DaemonClass);

            SbtTaskDaemonBase dae = (SbtTaskDaemonBase)Activator.CreateInstance(type);

            //========== 3. 执行该实例 ==============
            dae.DaemonType = tblTask.DaemonType;
            dae.TaskName = tblTask.TaskName;
            dae.TaskParameter = tblTask.TaskParameter;
            dae.TaskSize = tblTask.TaskSize;
            dae.TaskPos = tblTask.TaskPos;
            dae.TaskMsg = tblTask.TaskMsg;
            dae.BatchSize = ruleItem.BatchSize;

            string sFailReason = "";
            SbtTaskRunningResult taskResult = SbtTaskRunningResult.Processing;
            try
            {
                taskResult = dae.DoProcess();
            }
            catch (Exception e)
            {
                if (e.Message == "")
                    sFailReason = "任务执行异常（无错误消息）";
                else
                    sFailReason = e.Message;
            }

            //========= 4. 执行以后更新任务表 =============
            //======== 4.1 生成下一个任务 ===========
            if (ruleItem.IntervalType != SbtTaskIntervalType.Once)
            {
                TbSysDaemonTask tblNew;
                if (taskResult == SbtTaskRunningResult.Processing)
                {
                    tblNew = ruleItem.GenerateTbTaskRecord(DateTimeUtil.Now, true);
                }
                else
                {
                    tblNew = ruleItem.GenerateTbTaskRecord(DateTimeUtil.Now, false);
                }
                tblNew.Insert();
            }

            //======== 4.2 删除已执行的任务 ===========
            tblTask.Delete();

            //========== 5. 记录任务处理日志 ==============
            if (taskResult == SbtTaskRunningResult.FinishWithNoLog)
                return;

            if (taskResult == SbtTaskRunningResult.Processing)
                return;

            TbSysDaemonLog tblLog = new TbSysDaemonLog();
            tblLog.DaemonTaskUid = tblTask.DaemonTaskUid;
            tblLog.DaemonType = tblTask.DaemonType;
            tblLog.StartTime = dtLogStartTime;
            tblLog.StopTime = DateTimeUtil.Now;
            tblLog.TaskName = tblTask.TaskName;
            tblLog.TaskSize = tblTask.TaskSize;
            tblLog.TaskDuration = DateTimeUtil.SecondsAfter(tblLog.StartTime, tblLog.StopTime);
            if (sFailReason != "")
            {
                tblLog.TaskResult = "fail";
                tblLog.FailReason = sFailReason;
            }
            else
            {
                tblLog.TaskResult = "success";
                tblLog.FailReason = "";
            }
            tblLog.ResultData = dae.TaskMsg;
            tblLog.Remarks = "";
            tblLog.Insert();
        }
    }
}
