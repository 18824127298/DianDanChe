using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework
{
    /// <summary>
    /// 后台处理任务日志(sbt_sys_daemon_log)的表操作用户类
    /// </summary>
    public class TbSysDaemonLog : TbSysDaemonLogBase
    {
        #region 用户可编辑区域

        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }


    /// <summary>
    /// 后台处理任务日志(sbt_sys_daemon_log)的字段名类
    /// </summary>
    public class TbSysDaemonLogF
    {
        public const string TableName = "sbt_sys_daemon_log";
        public const string DaemonTaskUid = "daemon_task_uid";
        public const string DaemonType = "daemon_type";
        public const string StartTime = "start_time";
        public const string StopTime = "stop_time";
        public const string TaskName = "task_name";
        public const string TaskSize = "task_size";
        public const string TaskDuration = "task_duration";
        public const string TaskResult = "task_result";
        public const string FailReason = "fail_reason";
        public const string ResultData = "result_data";
        public const string Remarks = "remarks";
    }


    /// <summary>
    /// 后台处理任务日志(sbt_sys_daemon_log)的表操作基类
    /// </summary>
    public class TbSysDaemonLogBase : Sigbit.Data.TableBase
    {
        #region 私有变量定义
        protected string _daemonTaskUid = "";
        protected string _daemonType = "";
        protected string _startTime = "";
        protected string _stopTime = "";
        protected string _taskName = "";
        protected int _taskSize;
        protected int _taskDuration;
        protected string _taskResult = "";
        protected string _failReason = "";
        protected string _resultData = "";
        protected string _remarks = "";
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public TbSysDaemonLogBase()
        {
            ResetData();
        }

        #region 属性定义
        /// <summary>
        /// 后台处理任务标识，主键
        /// </summary>
        public string DaemonTaskUid
        {
            get
            {
                return _daemonTaskUid;
            }
            set
            {
                _daemonTaskUid = value;
            }
        }

        /// <summary>
        /// 后台处理的类型
        /// </summary>
        public string DaemonType
        {
            get
            {
                return _daemonType;
            }
            set
            {
                _daemonType = value;
            }
        }

        /// <summary>
        /// 后台处理开始时间
        /// </summary>
        public string StartTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                _startTime = value;
            }
        }

        /// <summary>
        /// 后台处理结束时间
        /// </summary>
        public string StopTime
        {
            get
            {
                return _stopTime;
            }
            set
            {
                _stopTime = value;
            }
        }

        /// <summary>
        /// 后台处理的任务名
        /// </summary>
        public string TaskName
        {
            get
            {
                return _taskName;
            }
            set
            {
                _taskName = value;
            }
        }

        /// <summary>
        /// 规模大小
        /// </summary>
        public int TaskSize
        {
            get
            {
                return _taskSize;
            }
            set
            {
                _taskSize = value;
            }
        }

        /// <summary>
        /// 一共运行的时长（秒）
        /// </summary>
        public int TaskDuration
        {
            get
            {
                return _taskDuration;
            }
            set
            {
                _taskDuration = value;
            }
        }

        /// <summary>
        /// 运行的结果
        /// </summary>
        public string TaskResult
        {
            get
            {
                return _taskResult;
            }
            set
            {
                _taskResult = value;
            }
        }

        /// <summary>
        /// 失败原因
        /// </summary>
        public string FailReason
        {
            get
            {
                return _failReason;
            }
            set
            {
                _failReason = value;
            }
        }

        /// <summary>
        /// 运行过程中的处理结果
        /// </summary>
        public string ResultData
        {
            get
            {
                return _resultData;
            }
            set
            {
                _resultData = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            get
            {
                return _remarks;
            }
            set
            {
                _remarks = value;
            }
        }
        #endregion

        #region 变量的清零及输出
        /// <summary>
        /// 得到数据的HTML显示文本
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DaemonTaskUid: " + this._daemonTaskUid + "<br>");
            sb.Append("DaemonType: " + this._daemonType + "<br>");
            sb.Append("StartTime: " + this._startTime + "<br>");
            sb.Append("StopTime: " + this._stopTime + "<br>");
            sb.Append("TaskName: " + this._taskName + "<br>");
            sb.Append("TaskSize: " + this._taskSize + "<br>");
            sb.Append("TaskDuration: " + this._taskDuration + "<br>");
            sb.Append("TaskResult: " + this._taskResult + "<br>");
            sb.Append("FailReason: " + this._failReason + "<br>");
            sb.Append("ResultData: " + this._resultData + "<br>");
            sb.Append("Remarks: " + this._remarks + "<br>");
            return sb.ToString();
        }

        /// <summary>
        /// 清零本记录的数据
        /// </summary>
        public override void ResetData()
        {
            _daemonTaskUid = "";
            _daemonType = "";
            _startTime = "";
            _stopTime = "";
            _taskName = "";
            _taskSize = 0;
            _taskDuration = 0;
            _taskResult = "";
            _failReason = "";
            _resultData = "";
            _remarks = "";
        }

        #endregion

        #region 基本的增删改查操作
        /// <summary>
        /// 按主键获取一条数据（如无数据抛例外）
        /// </summary>
        public override void Fetch()
        {
            Fetch(false);
        }

        /// <summary>
        /// 按主键获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public override bool Fetch(bool allowNoData)
        {
            string sSQL;
            int nRecordCount;
            DataSet dataSet;
            DataRow row;

            //======= 1. 得到SQL语句 ==============
            sSQL = "select daemon_type,         start_time,          stop_time,            \n";
            sSQL += "       task_name,           task_size,           task_duration,        \n";
            sSQL += "       task_result,         fail_reason,         result_data,          \n";
            sSQL += "       remarks              \n";
            sSQL += "  from sbt_sys_daemon_log    \n";
            sSQL += "  where daemon_task_uid = " + Quote(_daemonTaskUid) + "\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbSysDaemonLog.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _daemonType = DbToString(row["daemon_type"]); DbToInt(row["daemon_type"]);
            _startTime = DbToString(row["start_time"]); DbToInt(row["start_time"]);
            _stopTime = DbToString(row["stop_time"]); DbToInt(row["stop_time"]);
            _taskName = DbToString(row["task_name"]); DbToInt(row["task_name"]);
            _taskSize = DbToInt(row["task_size"]);
            _taskDuration = DbToInt(row["task_duration"]);
            _taskResult = DbToString(row["task_result"]); DbToInt(row["task_result"]);
            _failReason = DbToString(row["fail_reason"]); DbToInt(row["fail_reason"]);
            _resultData = DbToString(row["result_data"]); DbToInt(row["result_data"]);
            _remarks = DbToString(row["remarks"]); DbToInt(row["remarks"]);
            return true;
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        public override void Insert()
        {
            string sSQL;

            sSQL = "insert into sbt_sys_daemon_log \n";
            sSQL += "( daemon_task_uid,  daemon_type,      \n";
            sSQL += "  start_time,       stop_time,        \n";
            sSQL += "  task_name,        task_size,        \n";
            sSQL += "  task_duration,    task_result,      \n";
            sSQL += "  fail_reason,      result_data,      \n";
            sSQL += "  remarks           \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_daemonTaskUid) + "," + Quote(_daemonType) + ",\n";
            sSQL += Quote(_startTime) + "," + Quote(_stopTime) + ",\n";
            sSQL += Quote(_taskName) + "," + _taskSize.ToString() + ",\n";
            sSQL += _taskDuration.ToString() + "," + Quote(_taskResult) + ",\n";
            sSQL += Quote(_failReason) + "," + Quote(_resultData) + ",\n";
            sSQL += Quote(_remarks) + ")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 按主键删除一条数据
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL = "delete from sbt_sys_daemon_log \n";
            sSQL += "  where daemon_task_uid = " + Quote(_daemonTaskUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysDaemonLog.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL = "update sbt_sys_daemon_log set \n";
            sSQL += " daemon_task_uid = " + Quote(_daemonTaskUid) + ",\n";
            sSQL += " daemon_type = " + Quote(_daemonType) + ",\n";
            sSQL += " start_time = " + Quote(_startTime) + ",\n";
            sSQL += " stop_time = " + Quote(_stopTime) + ",\n";
            sSQL += " task_name = " + Quote(_taskName) + ",\n";
            sSQL += " task_size = " + _taskSize.ToString() + ",\n";
            sSQL += " task_duration = " + _taskDuration.ToString() + ",\n";
            sSQL += " task_result = " + Quote(_taskResult) + ",\n";
            sSQL += " fail_reason = " + Quote(_failReason) + ",\n";
            sSQL += " result_data = " + Quote(_resultData) + ",\n";
            sSQL += " remarks = " + Quote(_remarks) + "\n";
            sSQL += "  where daemon_task_uid = " + Quote(_daemonTaskUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysDaemonLog.Update() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键判断记录是否存在
        /// </summary>
        /// <returns>记录是否存在</returns>
        public override bool RecordExists()
        {
            string sSQL;
            int nRecordCount;

            //======= 1. 得到SQL语句 ==============
            sSQL = "select count(*) from sbt_sys_daemon_log \n";
            sSQL += "  where daemon_task_uid = " + Quote(_daemonTaskUid) + "\n";

            //======= 2. 运行SQL语句 ========
            nRecordCount = (int)DataHelper.Instance.ExecuteScalar(sSQL);
            if (nRecordCount == 1)
                return true;
            else
                return false;
        }

        #endregion

        #region 以主键为参数的操作
        /// <summary>
        /// 以主键为参数获取一条数据（如无数据抛例外）
        /// </summary>
        public void FetchByE(string daemonTaskUid)
        {
            bool hasData;
            hasData = FetchBy(daemonTaskUid);
            if (!hasData)
                throw new Exception("TbSysDaemonLog.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string daemonTaskUid)
        {
            _daemonTaskUid = daemonTaskUid;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbSysDaemonLog CreateBy(string daemonTaskUid)
        {
            TbSysDaemonLog tbl;
            bool hasData;

            tbl = new TbSysDaemonLog();
            hasData = tbl.FetchBy(daemonTaskUid);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string daemonTaskUid)
        {
            TbSysDaemonLog tbl;
            tbl = new TbSysDaemonLog();

            tbl.DaemonTaskUid = daemonTaskUid;

            tbl.Delete();
        }

        #endregion

        #region 文件和访问组件相关的支持函数
        /// <summary>
        /// 通过DataRow进行赋值
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _daemonTaskUid = DbToString(row["daemon_task_uid"]);
            _daemonType = DbToString(row["daemon_type"]);
            _startTime = DbToString(row["start_time"]);
            _stopTime = DbToString(row["stop_time"]);
            _taskName = DbToString(row["task_name"]);
            _taskSize = DbToInt(row["task_size"]);
            _taskDuration = DbToInt(row["task_duration"]);
            _taskResult = DbToString(row["task_result"]);
            _failReason = DbToString(row["fail_reason"]);
            _resultData = DbToString(row["result_data"]);
            _remarks = DbToString(row["remarks"]);
        }

        /// <summary>
        /// 通过DataSet进行赋值
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <param name="rowNum">行号</param>
        public override void AssignByDataRow(DataSet dataSet, int rowNum)
        {
            DataRow row;
            row = dataSet.Tables[0].Rows[rowNum];

            AssignByDataRow(row);
        }

        /// <summary>
        /// 将当前记录的信息保存到文件
        /// </summary>
        /// </param name="fileName">保存到的文件名</param>
        public override void DumpToFile(string fileName)
        {
            //========= 1. 打开文件 ============
            StreamWriter writer;
            string sLine;
            writer = File.CreateText(fileName);

            //========= 2. 写入文件 ============
            sLine = "daemon_task_uid\x9" + _daemonTaskUid;
            writer.WriteLine(sLine);

            sLine = "daemon_type\x9" + _daemonType;
            writer.WriteLine(sLine);

            sLine = "start_time\x9" + _startTime;
            writer.WriteLine(sLine);

            sLine = "stop_time\x9" + _stopTime;
            writer.WriteLine(sLine);

            sLine = "task_name\x9" + _taskName;
            writer.WriteLine(sLine);

            sLine = "task_size\x9" + _taskSize.ToString();
            writer.WriteLine(sLine);

            sLine = "task_duration\x9" + _taskDuration.ToString();
            writer.WriteLine(sLine);

            sLine = "task_result\x9" + _taskResult;
            writer.WriteLine(sLine);

            sLine = "fail_reason\x9" + _failReason;
            writer.WriteLine(sLine);

            sLine = "result_data\x9" + _resultData;
            writer.WriteLine(sLine);

            sLine = "remarks\x9" + _remarks;
            writer.WriteLine(sLine);

            //========= 3. 关闭文件 ============
            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// 将表中的所有记录保存到文件
        /// </summary>
        /// </param name="fileName">保存到的文件名</param>
        public override void DumpAllRecordsToFile(string fileName)
        {
            string sSQL;
            int i, nCol, nRecordCount;
            DataSet dataSet;
            DataRow row;
            string sFieldValue, sLine;
            StreamWriter writer;

            //======== 1. 打开文件 ========
            writer = File.CreateText(fileName);

            //======== 2. 写入第一行（标题行） ========
            sLine = "daemon_task_uid\tdaemon_type\tstart_time\t";
            sLine += "stop_time\ttask_name\ttask_size\t";
            sLine += "task_duration\ttask_result\tfail_reason\t";
            sLine += "result_data\tremarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL = "select daemon_task_uid, daemon_type,     start_time,      \n";
            sSQL += "       stop_time,       task_name,       task_size,       \n";
            sSQL += "       task_duration,   task_result,     fail_reason,     \n";
            sSQL += "       result_data,     remarks         \n";
            sSQL += "  from sbt_sys_daemon_log";

            //======== 4. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. 处理每一行 ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 处理每一列 ========
                for (nCol = 0; nCol < 11; nCol++)
                {
                    if (row[nCol] is DBNull)
                        sFieldValue = "";
                    else
                        sFieldValue = row[nCol].ToString();

                    if (nCol == 0)
                        sLine += sFieldValue;
                    else
                        sLine += "\t" + sFieldValue;
                }

                //======== 5.2 将一行的值写入文件 ========
                writer.WriteLine(sLine);
            }

            //======== 6. 关闭文件 ========
            writer.Flush();
            writer.Close();
        }

        #endregion

        #region 辅助支持函数
        /// <summary>
        /// 获取一个字段的数据库类型
        /// </summary>
        static public string DBTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "daemon_task_uid":
                    return "varchar";
                case "daemon_type":
                    return "varchar";
                case "start_time":
                    return "varchar";
                case "stop_time":
                    return "varchar";
                case "task_name":
                    return "varchar";
                case "task_size":
                    return "int";
                case "task_duration":
                    return "int";
                case "task_result":
                    return "varchar";
                case "fail_reason":
                    return "text";
                case "result_data":
                    return "text";
                case "remarks":
                    return "varchar";
                default:
                    throw new Exception("TbSysDaemonLogBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 获取一个字段的CSharp类型
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "daemon_task_uid":
                    return "string";
                case "daemon_type":
                    return "string";
                case "start_time":
                    return "string";
                case "stop_time":
                    return "string";
                case "task_name":
                    return "string";
                case "task_size":
                    return "int";
                case "task_duration":
                    return "int";
                case "task_result":
                    return "string";
                case "fail_reason":
                    return "string";
                case "result_data":
                    return "string";
                case "remarks":
                    return "string";
                default:
                    throw new Exception("TbSysDaemonLogBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 通过一个字段的值访问得到另一个字段的值
        /// </summary>
        static public string GetFieldValueBy(string fromFieldName, string fromFieldValue, string toFieldName)
        {
            string sSQL;
            int nRecordCount;
            DataSet dataSet;
            DataRow row;

            //======= 1. 得到SQL语句 ==============
            sSQL = "select " + toFieldName + "from sbt_sys_daemon_log \n";
            sSQL += "where " + fromFieldName + " = ";
            if (CSharpTypeOfFieldName(fromFieldName) == "string")
                sSQL += "'" + fromFieldValue + "'";
            else
                sSQL += fromFieldValue;

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount == 0)
                return "";

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            return row[toFieldName].ToString();
        }

        #endregion

    }
}
