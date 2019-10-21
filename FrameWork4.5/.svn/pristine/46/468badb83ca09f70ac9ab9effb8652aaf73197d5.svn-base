using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace Sigbit.Common
{
    /// <summary>
    /// 诊断查错日志记录
    /// </summary>
    public class DebugLogger
    {
        /// <summary>
        /// 错误记录
        /// </summary>
        /// <param name="sLogText">错误文本</param>
        public static void LogError(string sLogText)
        {
            if (!SigbitCommonConfig.Instance.IsLogError)
                return;

            LogByDate("error", sLogText);
        }

        /// <summary>
        /// 警告记录
        /// </summary>
        /// <param name="sLogText">警告文本</param>
        public static void LogWarning(string sLogText)
        {
            if (!SigbitCommonConfig.Instance.IsLogWarning)
                return;

            LogByDate("warning", sLogText);
        }

        /// <summary>
        /// 记录文本信息
        /// </summary>
        /// <param name="sMessage">文本信息</param>
        /// <param name="sKey">标识字</param>
        public static void LogDebugMessage(string sMessage, string sKey)
        {
            if (!SigbitCommonConfig.Instance.IsLogDebugMessage)
                return;

            LogByDate(sKey, sMessage);
        }

        /// <summary>
        /// 记录文本信息
        /// </summary>
        /// <param name="sMessage">文本信息</param>
        public static void LogDebugMessage(string sMessage)
        {
            if (!SigbitCommonConfig.Instance.IsLogDebugMessage)
                return;

            LogByDate("", sMessage);
        }

        /// <summary>
        /// 按日期分文件进行记录
        /// </summary>
        /// <param name="sLogType">日志类型</param>
        /// <param name="sLogText">日志文本</param>
        private static void LogByDate(string sLogType, string sLogText)
        {
            if (!SigbitCommonConfig.Instance.DebugFileEnabled)
                return;

            lock (typeof(DebugLogger))
            {
                //========== 1. 得到文件名 ============
                string sLogDirectory = SigbitCommonConfig.Instance.DebugFileDirectory;
                string sLogFileName = sLogDirectory + "\\debuglog_"
                        + DateTimeUtil.Now.Substring(0, 10).Replace("-", "") + ".vlog";

                //======= 2. 打开文件 ==========
                FileStream fs = File.OpenWrite(sLogFileName);
                fs.Seek(0, SeekOrigin.End);

                //========== 3. 写入时间 ==========
                string sMessage = "========== ";
                if (sLogType != "")
                    sMessage += "(" + sLogType + ") ";
                sMessage += DateTimeUtil.Now + " ==========\r\n";
                byte[] bsLog = Encoding.Default.GetBytes(sMessage);
                fs.Write(bsLog, 0, bsLog.Length);

                bsLog = Encoding.Default.GetBytes(sLogText + "\r\n\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                fs.Flush();
                fs.Close();
            }
        }
    }
}
