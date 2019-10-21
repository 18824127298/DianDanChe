using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;

namespace Sigbit.Framework
{
    public class SbtAppLogger
    {
        /// <summary>
        /// 记录动作
        /// </summary>
        /// <param name="sActionName">动作名称</param>
        /// <param name="sActionDescription">动作描述</param>
        public static void LogAction(string sActionName, string sActionDescription)
        {
            TbLogOperationAudit tblLog = new TbLogOperationAudit();
            tblLog.LogUid = Guid.NewGuid().ToString();
            tblLog.UserUid = SbtAppContext.CurrentUser.UserUid;
            tblLog.UserName = SbtAppContext.CurrentUser.UserName;
            tblLog.ProcTime = DateTimeUtil.NowWithMilliSeconds;
            tblLog.ProcClassId = SbtAppContext.ActionInfo.ProcClassId;
            tblLog.ProcClassName = SbtAppContext.ActionInfo.ProcClassName; 
            tblLog.ProcSubclassId = SbtAppContext.ActionInfo.ProcSubclassId;
            tblLog.ProcSubclassName = SbtAppContext.ActionInfo.ProcSubclassName;
            tblLog.ActionCode = "";
            tblLog.ActionName = sActionName;
            tblLog.ActionDescription = sActionDescription;
            tblLog.IpAddress = "";
            tblLog.ProcessData = "";
            tblLog.SqlStatement = "";
            tblLog.ModifyTime = DateTimeUtil.Now;
            tblLog.Remarks = "";

            tblLog.Insert();
        }

        public static void LogAction__WithRemarks(string sActionName, string sActionDescription, 
                string sRemarks)
        {
            TbLogOperationAudit tblLog = new TbLogOperationAudit();
            tblLog.LogUid = Guid.NewGuid().ToString();
            tblLog.UserUid = SbtAppContext.CurrentUser.UserUid;
            tblLog.UserName = SbtAppContext.CurrentUser.UserName;
            tblLog.ProcTime = DateTimeUtil.NowWithMilliSeconds;
            tblLog.ProcClassId = SbtAppContext.ActionInfo.ProcClassId;
            tblLog.ProcClassName = SbtAppContext.ActionInfo.ProcClassName;
            tblLog.ProcSubclassId = SbtAppContext.ActionInfo.ProcSubclassId;
            tblLog.ProcSubclassName = SbtAppContext.ActionInfo.ProcSubclassName;
            tblLog.ActionCode = "";
            tblLog.ActionName = sActionName;
            tblLog.ActionDescription = sActionDescription;
            tblLog.IpAddress = "";
            tblLog.ProcessData = "";
            tblLog.SqlStatement = "";
            tblLog.ModifyTime = DateTimeUtil.Now;
            tblLog.Remarks = sRemarks;

            tblLog.Insert();
        }
    }
}
