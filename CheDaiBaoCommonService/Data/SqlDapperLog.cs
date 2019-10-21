using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigbit.Common;
using Sigbit.Data;

namespace CheDaiBaoCommonService.Data
{
    class SqlDapperLog
    {

        public SqlDapperLog()
        {
            this.DataInstanceName = "SqlDapper";
        }


        #region 属性
        /// <summary>
        /// 数据的实例名称
        /// </summary>
        private string _dataInstanceName;

        public string DataInstanceName
        {
            get { return _dataInstanceName; }
            set { _dataInstanceName = value; }
        }

        private bool _isRunningSQL = false;
        /// <summary>
        /// 是否正在运行SQL语句
        /// </summary>
        public bool IsRunningSQL
        {
            get { return _isRunningSQL; }
        }

        private DateTime _lastRunSQLTime = DateTime.Now;
        /// <summary>
        /// 最近一次运行SQL语句的时间
        /// </summary>
        public DateTime LastRunSQLTime
        {
            get { return _lastRunSQLTime; }
            set { _lastRunSQLTime = value; }
        }


        private SQLMaxTimeCostType _nextSQLMaxTimeCostType = SQLMaxTimeCostType.QuickSQL;
        /// <summary>
        /// 下一条SQL语句的运行时长类型
        /// </summary>
        public SQLMaxTimeCostType NextSQLMaxTimeCostType
        {
            get { return _nextSQLMaxTimeCostType; }
            set { _nextSQLMaxTimeCostType = value; }
        }

        private int _nextSQLMaxTimeCost = 0;
        /// <summary>
        /// 下一条SQL语句的运行时长设定（单位：毫秒）。如果设置了该属性，
        /// 则忽略时长类型设定。
        /// </summary>
        public int NextSQLMaxTimeCost
        {
            get { return _nextSQLMaxTimeCost; }
            set { _nextSQLMaxTimeCost = value; }
        }
       

        private Stopwatch _watch = null;

        private string _runSQL = "";
        /// <summary>
        /// 运行的SQL语句
        /// </summary>
        public string RunSQL
        {
            get { return _runSQL; }
            set { _runSQL = value; }
        }

        private object _inputParam = null;
        /// <summary>
        /// 输入参数
        /// </summary>
        public object InputParam
        {
            get { return _inputParam; }
            set { _inputParam = value; }
        }


        #endregion 属性

        public void LogStart()
        {
            //========== 1. 启动计时 ==============

            if (DataHelperConfig.Instance.LogMethod != SQLPerfLogMethod.LogNone)
            {
                _watch = new Stopwatch();
                _watch.Start();
            }
        }

        public void LogEnd()
        {
            //======== 3. 停止计时，并记录日志 ===========
            if (DataHelperConfig.Instance.LogMethod != SQLPerfLogMethod.LogNone)
            {
                _watch.Stop();
                int nEllapsedMS = (int)_watch.ElapsedMilliseconds;

                SQLPerfLogWrite(this.RunSQL, nEllapsedMS);
            }
        }


        #region 日志记录

        /// <summary>
        /// 数据库执行信息写入
        /// </summary>
        /// <param name="sSQL">SQL语句</param>
        /// <param name="sRunLog">执行信息</param>
        private void SQLRunLogWrite(string sSQL, string sRunLog)
        {
            //========= 1. 打开文件 ==========
            string sLogDirectory = DataHelperConfig.Instance.LogDirectory;
            string sLogFileName = sLogDirectory + "\\sqlperf_"
                    + DateTimeUtil.Now.Substring(0, 10).Replace("-", "") + ".vlog";

            lock (typeof(DataHelper))
            {
                FileStream fs = File.OpenWrite(sLogFileName);
                fs.Seek(0, SeekOrigin.End);

                //====== 2. 写入开始计时信息 =============
                string sLine;

                sLine = "==== (" + this.DataInstanceName + ")【运行信息】";
                sLine += DateTime.Now.ToString() + " ";
                sLine += "====\r\n";

                byte[] bsLog = Encoding.Default.GetBytes(sLine);
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 3. 写入SQL语句 ==========
                bsLog = Encoding.Default.GetBytes(sSQL + "\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 4. 写入执行信息 ==========
                bsLog = Encoding.Default.GetBytes("-------- 运行信息 --------\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                bsLog = Encoding.Default.GetBytes(sRunLog + "\r\n\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 5. 关闭文件 ==========
                fs.Flush();
                fs.Close();
            }
        }


        private void SQLErrorLogWrite(string sSQL, string sErrorMessage)
        {
            //========= 1. 打开文件 ==========
            string sLogDirectory = DataHelperConfig.Instance.LogDirectory;
            string sLogFileName = sLogDirectory + "\\sqlperf_"
                    + DateTimeUtil.Now.Substring(0, 10).Replace("-", "") + ".vlog";

            lock (typeof(DataHelper))
            {
                FileStream fs = File.OpenWrite(sLogFileName);
                fs.Seek(0, SeekOrigin.End);

                //====== 2. 写入开始计时信息 =============
                string sLine;

                sLine = "==== (" + this.DataInstanceName + ")【SQL语句运行错误】";
                sLine += DateTime.Now.ToString() + " ";
                sLine += "====\r\n";

                byte[] bsLog = Encoding.Default.GetBytes(sLine);
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 3. 写入SQL语句 ==========
                bsLog = Encoding.Default.GetBytes(sSQL + "\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 4. 写入错误信息 ==========
                bsLog = Encoding.Default.GetBytes("-------- 错误信息 --------\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                bsLog = Encoding.Default.GetBytes(sErrorMessage + "\r\n\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 5. 关闭文件 ==========
                fs.Flush();
                fs.Close();
            }
        }

        /// <summary>
        /// 记录SQL语句
        /// </summary>
        /// <param name="sSQL">SQL语句</param>
        /// <param name="nEllapsedMS">历时</param>
        private void SQLPerfLogWrite(string sSQL, int nEllapsedMS)
        {
            //=========== 0. 判断是否需要进行日志记录 ==========
            Debug.Assert(DataHelperConfig.Instance.LogMethod == SQLPerfLogMethod.LogWanted
                    || DataHelperConfig.Instance.LogMethod == SQLPerfLogMethod.LogAll);
            int nMaxSQLTimeCost = GetMaxSQLTimeCost();

            bool bSQLOverTime = false;
            if (nEllapsedMS > nMaxSQLTimeCost)
                bSQLOverTime = true;

            if (DataHelperConfig.Instance.LogMethod == SQLPerfLogMethod.LogWanted
                    && !bSQLOverTime)
                return;

            //========= 1. 打开文件 ==========
            string sLogDirectory = DataHelperConfig.Instance.LogDirectory;
            string sLogFileName = sLogDirectory + "\\sqlperf_"
                    + DateTimeUtil.Now.Substring(0, 10).Replace("-", "") + ".vlog";

            lock (typeof(DataHelper))
            {
                FileStream fs = File.OpenWrite(sLogFileName);
                fs.Seek(0, SeekOrigin.End);

                //====== 2. 写入开始计时信息 =============
                string sLine;

                if (bSQLOverTime)
                {
                    sLine = "==== 【" + _dataInstanceName + "】【actual " + nEllapsedMS.ToString() + " ms / want ";
                    sLine += nMaxSQLTimeCost.ToString() + " ms】 ";
                    sLine += DateTime.Now.ToString() + " ";
                    sLine += "====\r\n";
                }
                else
                {
                    sLine = "====[" + _dataInstanceName + "][SQL " + nEllapsedMS.ToString().PadLeft(6, '.') + " ms] ";
                    sLine += DateTime.Now.ToString() + " ";
                    sLine += "====\r\n";
                }
                byte[] bsLog = Encoding.Default.GetBytes(sLine);
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 3. 写入SQL语句 ==========
                bsLog = Encoding.Default.GetBytes(sSQL + "\r\n\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 5. 关闭文件 ==========
                fs.Flush();
                fs.Close();
            }
        }

        /// <summary>
        /// 得到SQL语句运行的最大时间
        /// </summary>
        /// <returns>以毫秒为单位的时间值</returns>
        private int GetMaxSQLTimeCost()
        {
            int nRet = -1;
            if (_nextSQLMaxTimeCost > 0)
                nRet = _nextSQLMaxTimeCost;
            else
            {
                switch (_nextSQLMaxTimeCostType)
                {
                    case SQLMaxTimeCostType.QuickSQL:
                        nRet = DataHelperConfig.Instance.LogQuickSQLTime;
                        break;
                    case SQLMaxTimeCostType.NormalSQL:
                        nRet = DataHelperConfig.Instance.LogNormalSQLTime;
                        break;
                    case SQLMaxTimeCostType.SlowSQL:
                        nRet = DataHelperConfig.Instance.LogSlowSQLTime;
                        break;
                    default:
                        throw new Exception("未知的_nextSQLMaxTimeCostType");
                }
            }

            _nextSQLMaxTimeCost = 0;
            _nextSQLMaxTimeCostType = SQLMaxTimeCostType.QuickSQL;
            return nRet;
        }
        #endregion 日志记录

    }
}
