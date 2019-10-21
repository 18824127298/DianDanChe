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
        /// ����һ������
        /// </summary>
        /// <returns>
        /// ��һ��������Ҫ�ȴ���ʱ�������Ժ���Ϊ��λ����
        /// ��Ϊ-1����ʾ��ȱʡ�����á�
        /// </returns>
        public int DoOneTask()
        {
            //======= 1. ȡ������ļ�¼ ============
            TbSysDaemonTask tblTask = new TbSysDaemonTask();
            bool bFindTask = tblTask.FetchEarliestRecord();

            //======== 2. ����Ҳ�������ֱ�ӷ��� ==========
            if (!bFindTask)
                return -1;

            //========= 3. �����¼��ʱ�����ڵ�ǰ��ʱ�䣬���ִ�и����� =======
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

                //========= 4. �����¼��ʱ�����ڵ�ǰ��ʱ�䣬�ж��������� =======
                //=========    �������С��ȱʡʱ�䣬��ȴ����ƻ���ʱ�� =========
                if (nSecondsAfter < SbtFrameworkConfig.Instance.TaskQueryTableTimerInterval)
                    return nSecondsAfter * 1000;
                else
                    return -1;
            }
        }

        /// <summary>
        /// ������Ӧ������
        /// </summary>
        /// <param name="tblTask">�����¼</param>
        private void ProcessTaskRecord(TbSysDaemonTask tblTask)
        {
            string dtLogStartTime = DateTimeUtil.Now;

            //========== 1. �õ���Ӧ��������� ==========
            SbtTaskRuleItem ruleItem = SbtTaskRuleLib.Instance.GetRuleItem(tblTask.DaemonType);

            //========== 2. ���÷���õ���Ӧ������ʵ�� =========
            string sBinDirName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
            sBinDirName += "\\bin";

            Assembly assem = Assembly.LoadFrom(sBinDirName + "\\" + ruleItem.DaemonComponent + ".dll");
            Type type = assem.GetType(ruleItem.DaemonComponent + "." + ruleItem.DaemonClass);

            SbtTaskDaemonBase dae = (SbtTaskDaemonBase)Activator.CreateInstance(type);

            //========== 3. ִ�и�ʵ�� ==============
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
                    sFailReason = "����ִ���쳣���޴�����Ϣ��";
                else
                    sFailReason = e.Message;
            }

            //========= 4. ִ���Ժ��������� =============
            //======== 4.1 ������һ������ ===========
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

            //======== 4.2 ɾ����ִ�е����� ===========
            tblTask.Delete();

            //========== 5. ��¼��������־ ==============
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
