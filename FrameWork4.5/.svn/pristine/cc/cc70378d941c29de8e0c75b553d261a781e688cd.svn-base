using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework
{
    public class TbSysDaemonTaskFTaskStatus
    {
        /// <summary>
        /// 空闲
        /// </summary>
        public const string Idle = "idle";
        /// <summary>
        /// 正在处理
        /// </summary>
        public const string Processing = "processing";
    }

    /// <summary>
    /// 后台处理任务(sbt_sys_daemon_task)的表操作用户类
    /// </summary>
    public class TbSysDaemonTask : TbSysDaemonTaskBase
    {
        #region 用户可编辑区域

        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)


        public static void TruncateTable()
        {
            string sSQL;
            sSQL = "truncate table sbt_sys_daemon_task ";
            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 得到任务表的任务数
        /// </summary>
        /// <returns>任务表的任务数</returns>
        public int RecordCount()
        {
            string sSQL;
            int nRecordCount;

            //======= 1. 得到SQL语句 ==============
            sSQL = "select count(*) from sbt_sys_daemon_task \n";

            //======= 2. 运行SQL语句 ========
            nRecordCount = Convert.ToInt32(DataHelper.Instance.ExecuteScalar(sSQL));
            return nRecordCount;
        }

        /// <summary>
        /// 取出最早的一条记录
        /// </summary>
        /// <returns>是否找到最早的记录</returns>
        public bool FetchEarliestRecord()
        {
            string sSQL;
            DataSet dataSet;
            int nRecordCount;

            //======= 1. 得到SQL语句 ==============
            sSQL = "select * from sbt_sys_daemon_task "
                    + " where task_status = " + Quote(TbSysDaemonTaskFTaskStatus.Idle)
                    + " order by plan_time limit 1";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount <= 0)
                return false;

            //========= 3. 更新状态为正在处理 =============
            TbSysDaemonTask tblTask = new TbSysDaemonTask();
            tblTask.AssignByDataRow(dataSet, 0);
            string sUpdateSQL = "update sbt_sys_daemon_task "
                    + " set task_status = " + Quote(TbSysDaemonTaskFTaskStatus.Processing)
                    + " where task_status = " + Quote(TbSysDaemonTaskFTaskStatus.Idle)
                    + " and daemon_task_uid = " + Quote(tblTask.DaemonTaskUid);
            int nAffected = DataHelper.Instance.ExecuteNonQuery(sUpdateSQL);
            if (nAffected <= 0)
                return false;

            AssignByDataRow(dataSet, 0);
            return true;
        }

        #endregion
    }


    /// <summary>
    /// 后台处理任务(sbt_sys_daemon_task)的字段名类
    /// </summary>
    public class TbSysDaemonTaskF
    {
        public const string TableName = "sbt_sys_daemon_task";
        public const string DaemonTaskUid = "daemon_task_uid";
        public const string DaemonType = "daemon_type";
        public const string TaskName = "task_name";
        public const string TaskParameter = "task_parameter";
        public const string PlanTime = "plan_time";
        public const string TaskStatus = "task_status";
        public const string Paused = "paused";
        public const string TaskPriority = "task_priority";
        public const string ActiveIntervalValue = "active_interval_value";
        public const string BatchSize = "batch_size";
        public const string StartTime = "start_time";
        public const string TaskSize = "task_size";
        public const string TaskPos = "task_pos";
        public const string TaskMsg = "task_msg";
        public const string ResultData = "result_data";
        public const string Remarks = "remarks";
    }


    /// <summary>
    /// 后台处理任务(sbt_sys_daemon_task)的表操作基类
    /// </summary>
    public class TbSysDaemonTaskBase : Sigbit.Data.TableBase
    {
        #region 私有变量定义
        protected string _daemonTaskUid = "";
        protected string _daemonType = "";
        protected string _taskName = "";
        protected string _taskParameter = "";
        protected string _planTime = "";
        protected string _taskStatus = "";
        protected string _paused = "";
        protected int _taskPriority;
        protected int _activeIntervalValue;
        protected int _batchSize;
        protected string _startTime = "";
        protected int _taskSize;
        protected int _taskPos;
        protected string _taskMsg = "";
        protected string _resultData = "";
        protected string _remarks = "";
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public TbSysDaemonTaskBase()
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
        /// 当前任务的名称
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
        /// 当前任务的参数
        /// </summary>
        public string TaskParameter
        {
            get
            {
                return _taskParameter;
            }
            set
            {
                _taskParameter = value;
            }
        }

        /// <summary>
        /// 下一次任务的计划运行时间
        /// </summary>
        public string PlanTime
        {
            get
            {
                return _planTime;
            }
            set
            {
                _planTime = value;
            }
        }

        /// <summary>
        /// 当前的任务状态
        /// </summary>
        public string TaskStatus
        {
            get
            {
                return _taskStatus;
            }
            set
            {
                _taskStatus = value;
            }
        }

        /// <summary>
        /// 是否暂停
        /// </summary>
        public string Paused
        {
            get
            {
                return _paused;
            }
            set
            {
                _paused = value;
            }
        }

        /// <summary>
        /// 运行优先级
        /// </summary>
        public int TaskPriority
        {
            get
            {
                return _taskPriority;
            }
            set
            {
                _taskPriority = value;
            }
        }

        /// <summary>
        /// 当激活时的时间间隔
        /// </summary>
        public int ActiveIntervalValue
        {
            get
            {
                return _activeIntervalValue;
            }
            set
            {
                _activeIntervalValue = value;
            }
        }

        /// <summary>
        /// 配置的批次大小
        /// </summary>
        public int BatchSize
        {
            get
            {
                return _batchSize;
            }
            set
            {
                _batchSize = value;
            }
        }

        /// <summary>
        /// 当前任务开始的时间
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
        /// 当前任务的规模
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
        /// 当前任务的进度
        /// </summary>
        public int TaskPos
        {
            get
            {
                return _taskPos;
            }
            set
            {
                _taskPos = value;
            }
        }

        /// <summary>
        /// 当前任务的提示
        /// </summary>
        public string TaskMsg
        {
            get
            {
                return _taskMsg;
            }
            set
            {
                _taskMsg = value;
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
            sb.Append("TaskName: " + this._taskName + "<br>");
            sb.Append("TaskParameter: " + this._taskParameter + "<br>");
            sb.Append("PlanTime: " + this._planTime + "<br>");
            sb.Append("TaskStatus: " + this._taskStatus + "<br>");
            sb.Append("Paused: " + this._paused + "<br>");
            sb.Append("TaskPriority: " + this._taskPriority + "<br>");
            sb.Append("ActiveIntervalValue: " + this._activeIntervalValue + "<br>");
            sb.Append("BatchSize: " + this._batchSize + "<br>");
            sb.Append("StartTime: " + this._startTime + "<br>");
            sb.Append("TaskSize: " + this._taskSize + "<br>");
            sb.Append("TaskPos: " + this._taskPos + "<br>");
            sb.Append("TaskMsg: " + this._taskMsg + "<br>");
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
            _taskName = "";
            _taskParameter = "";
            _planTime = "";
            _taskStatus = "";
            _paused = "";
            _taskPriority = 0;
            _activeIntervalValue = 0;
            _batchSize = 0;
            _startTime = "";
            _taskSize = 0;
            _taskPos = 0;
            _taskMsg = "";
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
            sSQL = "select daemon_type,         task_name,           task_parameter,       \n";
            sSQL += "       plan_time,           task_status,         paused,               \n";
            sSQL += "       task_priority,       active_interval_value,batch_size,           \n";
            sSQL += "       start_time,          task_size,           task_pos,             \n";
            sSQL += "       task_msg,            result_data,         remarks              \n";
            sSQL += "  from sbt_sys_daemon_task    \n";
            sSQL += "  where daemon_task_uid = " + Quote(_daemonTaskUid) + "\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbSysDaemonTask.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _daemonType = DbToString(row["daemon_type"]); DbToInt(row["daemon_type"]);
            _taskName = DbToString(row["task_name"]); DbToInt(row["task_name"]);
            _taskParameter = DbToString(row["task_parameter"]); DbToInt(row["task_parameter"]);
            _planTime = DbToString(row["plan_time"]); DbToInt(row["plan_time"]);
            _taskStatus = DbToString(row["task_status"]); DbToInt(row["task_status"]);
            _paused = DbToString(row["paused"]); DbToInt(row["paused"]);
            _taskPriority = DbToInt(row["task_priority"]);
            _activeIntervalValue = DbToInt(row["active_interval_value"]);
            _batchSize = DbToInt(row["batch_size"]);
            _startTime = DbToString(row["start_time"]); DbToInt(row["start_time"]);
            _taskSize = DbToInt(row["task_size"]);
            _taskPos = DbToInt(row["task_pos"]);
            _taskMsg = DbToString(row["task_msg"]); DbToInt(row["task_msg"]);
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

            sSQL = "insert into sbt_sys_daemon_task \n";
            sSQL += "( daemon_task_uid,  daemon_type,      \n";
            sSQL += "  task_name,        task_parameter,   \n";
            sSQL += "  plan_time,        task_status,      \n";
            sSQL += "  paused,           task_priority,    \n";
            sSQL += "  active_interval_value, batch_size,       \n";
            sSQL += "  start_time,       task_size,        \n";
            sSQL += "  task_pos,         task_msg,         \n";
            sSQL += "  result_data,      remarks           \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_daemonTaskUid) + "," + Quote(_daemonType) + ",\n";
            sSQL += Quote(_taskName) + "," + Quote(_taskParameter) + ",\n";
            sSQL += Quote(_planTime) + "," + Quote(_taskStatus) + ",\n";
            sSQL += Quote(_paused) + "," + _taskPriority.ToString() + ",\n";
            sSQL += _activeIntervalValue.ToString() + "," + _batchSize.ToString() + ",\n";
            sSQL += Quote(_startTime) + "," + _taskSize.ToString() + ",\n";
            sSQL += _taskPos.ToString() + "," + Quote(_taskMsg) + ",\n";
            sSQL += Quote(_resultData) + "," + Quote(_remarks) + ")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 按主键删除一条数据
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL = "delete from sbt_sys_daemon_task \n";
            sSQL += "  where daemon_task_uid = " + Quote(_daemonTaskUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            //if (nRowsAffected != 1)
            //    throw new Exception("TbSysDaemonTask.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL = "update sbt_sys_daemon_task set \n";
            sSQL += " daemon_task_uid = " + Quote(_daemonTaskUid) + ",\n";
            sSQL += " daemon_type = " + Quote(_daemonType) + ",\n";
            sSQL += " task_name = " + Quote(_taskName) + ",\n";
            sSQL += " task_parameter = " + Quote(_taskParameter) + ",\n";
            sSQL += " plan_time = " + Quote(_planTime) + ",\n";
            sSQL += " task_status = " + Quote(_taskStatus) + ",\n";
            sSQL += " paused = " + Quote(_paused) + ",\n";
            sSQL += " task_priority = " + _taskPriority.ToString() + ",\n";
            sSQL += " active_interval_value = " + _activeIntervalValue.ToString() + ",\n";
            sSQL += " batch_size = " + _batchSize.ToString() + ",\n";
            sSQL += " start_time = " + Quote(_startTime) + ",\n";
            sSQL += " task_size = " + _taskSize.ToString() + ",\n";
            sSQL += " task_pos = " + _taskPos.ToString() + ",\n";
            sSQL += " task_msg = " + Quote(_taskMsg) + ",\n";
            sSQL += " result_data = " + Quote(_resultData) + ",\n";
            sSQL += " remarks = " + Quote(_remarks) + "\n";
            sSQL += "  where daemon_task_uid = " + Quote(_daemonTaskUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            //if (nRowsAffected != 1)
            //    throw new Exception("TbSysDaemonTask.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from sbt_sys_daemon_task \n";
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
                throw new Exception("TbSysDaemonTask.FetchBy(...) Error - cannot locate record via PrimaryKey.");
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
        static public TbSysDaemonTask CreateBy(string daemonTaskUid)
        {
            TbSysDaemonTask tbl;
            bool hasData;

            tbl = new TbSysDaemonTask();
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
            TbSysDaemonTask tbl;
            tbl = new TbSysDaemonTask();

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
            _taskName = DbToString(row["task_name"]);
            _taskParameter = DbToString(row["task_parameter"]);
            _planTime = DbToString(row["plan_time"]);
            _taskStatus = DbToString(row["task_status"]);
            _paused = DbToString(row["paused"]);
            _taskPriority = DbToInt(row["task_priority"]);
            _activeIntervalValue = DbToInt(row["active_interval_value"]);
            _batchSize = DbToInt(row["batch_size"]);
            _startTime = DbToString(row["start_time"]);
            _taskSize = DbToInt(row["task_size"]);
            _taskPos = DbToInt(row["task_pos"]);
            _taskMsg = DbToString(row["task_msg"]);
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

            sLine = "task_name\x9" + _taskName;
            writer.WriteLine(sLine);

            sLine = "task_parameter\x9" + _taskParameter;
            writer.WriteLine(sLine);

            sLine = "plan_time\x9" + _planTime;
            writer.WriteLine(sLine);

            sLine = "task_status\x9" + _taskStatus;
            writer.WriteLine(sLine);

            sLine = "paused\x9" + _paused;
            writer.WriteLine(sLine);

            sLine = "task_priority\x9" + _taskPriority.ToString();
            writer.WriteLine(sLine);

            sLine = "active_interval_value\x9" + _activeIntervalValue.ToString();
            writer.WriteLine(sLine);

            sLine = "batch_size\x9" + _batchSize.ToString();
            writer.WriteLine(sLine);

            sLine = "start_time\x9" + _startTime;
            writer.WriteLine(sLine);

            sLine = "task_size\x9" + _taskSize.ToString();
            writer.WriteLine(sLine);

            sLine = "task_pos\x9" + _taskPos.ToString();
            writer.WriteLine(sLine);

            sLine = "task_msg\x9" + _taskMsg;
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
            sLine = "daemon_task_uid\tdaemon_type\ttask_name\t";
            sLine += "task_parameter\tplan_time\ttask_status\t";
            sLine += "paused\ttask_priority\tactive_interval_value\t";
            sLine += "batch_size\tstart_time\ttask_size\t";
            sLine += "task_pos\ttask_msg\tresult_data\t";
            sLine += "remarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL = "select daemon_task_uid, daemon_type,     task_name,       \n";
            sSQL += "       task_parameter,  plan_time,       task_status,     \n";
            sSQL += "       paused,          task_priority,   active_interval_value,\n";
            sSQL += "       batch_size,      start_time,      task_size,       \n";
            sSQL += "       task_pos,        task_msg,        result_data,     \n";
            sSQL += "       remarks         \n";
            sSQL += "  from sbt_sys_daemon_task";

            //======== 4. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. 处理每一行 ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 处理每一列 ========
                for (nCol = 0; nCol < 16; nCol++)
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
                case "task_name":
                    return "varchar";
                case "task_parameter":
                    return "varchar";
                case "plan_time":
                    return "varchar";
                case "task_status":
                    return "varchar";
                case "paused":
                    return "varchar";
                case "task_priority":
                    return "int";
                case "active_interval_value":
                    return "int";
                case "batch_size":
                    return "int";
                case "start_time":
                    return "varchar";
                case "task_size":
                    return "int";
                case "task_pos":
                    return "int";
                case "task_msg":
                    return "varchar";
                case "result_data":
                    return "text";
                case "remarks":
                    return "varchar";
                default:
                    throw new Exception("TbSysDaemonTaskBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
                case "task_name":
                    return "string";
                case "task_parameter":
                    return "string";
                case "plan_time":
                    return "string";
                case "task_status":
                    return "string";
                case "paused":
                    return "string";
                case "task_priority":
                    return "int";
                case "active_interval_value":
                    return "int";
                case "batch_size":
                    return "int";
                case "start_time":
                    return "string";
                case "task_size":
                    return "int";
                case "task_pos":
                    return "int";
                case "task_msg":
                    return "string";
                case "result_data":
                    return "string";
                case "remarks":
                    return "string";
                default:
                    throw new Exception("TbSysDaemonTaskBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL = "select " + toFieldName + "from sbt_sys_daemon_task \n";
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
