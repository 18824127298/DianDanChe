using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;
using Sigbit.Framework;
using System.Collections;

namespace Sigbit.Framework.Task
{
    /// <summary>
    /// 抽象类。任务扫描的具体实现，都从这个类派生出来。
    /// </summary>
    public class PrivateTaskScannerBase
    {

        /// 扫描个人的任务信息
        /// </summary>
        /// <param name="scanType">扫描类型</param>
        /// <returns>任务列表</returns>
        public virtual PrivateTaskListBase ScanTask()
        {
            return null;
        }
    }
}
