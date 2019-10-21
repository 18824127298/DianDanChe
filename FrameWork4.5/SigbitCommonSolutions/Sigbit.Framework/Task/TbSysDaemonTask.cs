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
        /// ����
        /// </summary>
        public const string Idle = "idle";
        /// <summary>
        /// ���ڴ���
        /// </summary>
        public const string Processing = "processing";
    }

    /// <summary>
    /// ��̨��������(sbt_sys_daemon_task)�ı�����û���
    /// </summary>
    public class TbSysDaemonTask : TbSysDaemonTaskBase
    {
        #region �û��ɱ༭����

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
        /// �õ�������������
        /// </summary>
        /// <returns>������������</returns>
        public int RecordCount()
        {
            string sSQL;
            int nRecordCount;

            //======= 1. �õ�SQL��� ==============
            sSQL = "select count(*) from sbt_sys_daemon_task \n";

            //======= 2. ����SQL��� ========
            nRecordCount = Convert.ToInt32(DataHelper.Instance.ExecuteScalar(sSQL));
            return nRecordCount;
        }

        /// <summary>
        /// ȡ�������һ����¼
        /// </summary>
        /// <returns>�Ƿ��ҵ�����ļ�¼</returns>
        public bool FetchEarliestRecord()
        {
            string sSQL;
            DataSet dataSet;
            int nRecordCount;

            //======= 1. �õ�SQL��� ==============
            sSQL = "select * from sbt_sys_daemon_task "
                    + " where task_status = " + Quote(TbSysDaemonTaskFTaskStatus.Idle)
                    + " order by plan_time limit 1";

            //======= 2. ����SQL��� ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount <= 0)
                return false;

            //========= 3. ����״̬Ϊ���ڴ��� =============
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
    /// ��̨��������(sbt_sys_daemon_task)���ֶ�����
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
    /// ��̨��������(sbt_sys_daemon_task)�ı��������
    /// </summary>
    public class TbSysDaemonTaskBase : Sigbit.Data.TableBase
    {
        #region ˽�б�������
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
        /// ���캯��
        /// </summary>
        public TbSysDaemonTaskBase()
        {
            ResetData();
        }

        #region ���Զ���
        /// <summary>
        /// ��̨���������ʶ������
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
        /// ��̨���������
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
        /// ��ǰ���������
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
        /// ��ǰ����Ĳ���
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
        /// ��һ������ļƻ�����ʱ��
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
        /// ��ǰ������״̬
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
        /// �Ƿ���ͣ
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
        /// �������ȼ�
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
        /// ������ʱ��ʱ����
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
        /// ���õ����δ�С
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
        /// ��ǰ����ʼ��ʱ��
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
        /// ��ǰ����Ĺ�ģ
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
        /// ��ǰ����Ľ���
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
        /// ��ǰ�������ʾ
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
        /// ���й����еĴ�����
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
        /// ��ע
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

        #region ���������㼰���
        /// <summary>
        /// �õ����ݵ�HTML��ʾ�ı�
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
        /// ���㱾��¼������
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

        #region ��������ɾ�Ĳ����
        /// <summary>
        /// ��������ȡһ�����ݣ��������������⣩
        /// </summary>
        public override void Fetch()
        {
            Fetch(false);
        }

        /// <summary>
        /// ��������ȡһ������
        /// </summary>
        /// <returns>�Ƿ���ʵ�����</returns>
        public override bool Fetch(bool allowNoData)
        {
            string sSQL;
            int nRecordCount;
            DataSet dataSet;
            DataRow row;

            //======= 1. �õ�SQL��� ==============
            sSQL = "select daemon_type,         task_name,           task_parameter,       \n";
            sSQL += "       plan_time,           task_status,         paused,               \n";
            sSQL += "       task_priority,       active_interval_value,batch_size,           \n";
            sSQL += "       start_time,          task_size,           task_pos,             \n";
            sSQL += "       task_msg,            result_data,         remarks              \n";
            sSQL += "  from sbt_sys_daemon_task    \n";
            sSQL += "  where daemon_task_uid = " + Quote(_daemonTaskUid) + "\n";

            //======= 2. ����SQL��� ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbSysDaemonTask.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. ��ȡ��¼ ========
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
        /// ����һ������
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
        /// ������ɾ��һ������
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
        /// ����������һ������
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
        /// �������жϼ�¼�Ƿ����
        /// </summary>
        /// <returns>��¼�Ƿ����</returns>
        public override bool RecordExists()
        {
            string sSQL;
            int nRecordCount;

            //======= 1. �õ�SQL��� ==============
            sSQL = "select count(*) from sbt_sys_daemon_task \n";
            sSQL += "  where daemon_task_uid = " + Quote(_daemonTaskUid) + "\n";

            //======= 2. ����SQL��� ========
            nRecordCount = (int)DataHelper.Instance.ExecuteScalar(sSQL);
            if (nRecordCount == 1)
                return true;
            else
                return false;
        }

        #endregion

        #region ������Ϊ�����Ĳ���
        /// <summary>
        /// ������Ϊ������ȡһ�����ݣ��������������⣩
        /// </summary>
        public void FetchByE(string daemonTaskUid)
        {
            bool hasData;
            hasData = FetchBy(daemonTaskUid);
            if (!hasData)
                throw new Exception("TbSysDaemonTask.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// ������Ϊ������ȡһ������
        /// </summary>
        /// <returns>�Ƿ���ʵ�����</returns>
        public bool FetchBy(string daemonTaskUid)
        {
            _daemonTaskUid = daemonTaskUid;

            return Fetch(true);
        }

        /// <summary>
        /// ������Ϊ������ȡ���ݣ����������ʵ��
        /// </summary>
        /// <returns>���ʵ��</returns>
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
        /// ������Ϊ����ɾ��һ������
        /// </summary>
        static public void DeleteBy(string daemonTaskUid)
        {
            TbSysDaemonTask tbl;
            tbl = new TbSysDaemonTask();

            tbl.DaemonTaskUid = daemonTaskUid;

            tbl.Delete();
        }

        #endregion

        #region �ļ��ͷ��������ص�֧�ֺ���
        /// <summary>
        /// ͨ��DataRow���и�ֵ
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
        /// ͨ��DataSet���и�ֵ
        /// </summary>
        /// <param name="dataSet">���ݼ�</param>
        /// <param name="rowNum">�к�</param>
        public override void AssignByDataRow(DataSet dataSet, int rowNum)
        {
            DataRow row;
            row = dataSet.Tables[0].Rows[rowNum];

            AssignByDataRow(row);
        }

        /// <summary>
        /// ����ǰ��¼����Ϣ���浽�ļ�
        /// </summary>
        /// </param name="fileName">���浽���ļ���</param>
        public override void DumpToFile(string fileName)
        {
            //========= 1. ���ļ� ============
            StreamWriter writer;
            string sLine;
            writer = File.CreateText(fileName);

            //========= 2. д���ļ� ============
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

            //========= 3. �ر��ļ� ============
            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// �����е����м�¼���浽�ļ�
        /// </summary>
        /// </param name="fileName">���浽���ļ���</param>
        public override void DumpAllRecordsToFile(string fileName)
        {
            string sSQL;
            int i, nCol, nRecordCount;
            DataSet dataSet;
            DataRow row;
            string sFieldValue, sLine;
            StreamWriter writer;

            //======== 1. ���ļ� ========
            writer = File.CreateText(fileName);

            //======== 2. д���һ�У������У� ========
            sLine = "daemon_task_uid\tdaemon_type\ttask_name\t";
            sLine += "task_parameter\tplan_time\ttask_status\t";
            sLine += "paused\ttask_priority\tactive_interval_value\t";
            sLine += "batch_size\tstart_time\ttask_size\t";
            sLine += "task_pos\ttask_msg\tresult_data\t";
            sLine += "remarks";
            writer.WriteLine(sLine);

            //======== 3. �õ�SQL��� ========
            sSQL = "select daemon_task_uid, daemon_type,     task_name,       \n";
            sSQL += "       task_parameter,  plan_time,       task_status,     \n";
            sSQL += "       paused,          task_priority,   active_interval_value,\n";
            sSQL += "       batch_size,      start_time,      task_size,       \n";
            sSQL += "       task_pos,        task_msg,        result_data,     \n";
            sSQL += "       remarks         \n";
            sSQL += "  from sbt_sys_daemon_task";

            //======== 4. ����SQL��� ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. ����ÿһ�� ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 ����ÿһ�� ========
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

                //======== 5.2 ��һ�е�ֵд���ļ� ========
                writer.WriteLine(sLine);
            }

            //======== 6. �ر��ļ� ========
            writer.Flush();
            writer.Close();
        }

        #endregion

        #region ����֧�ֺ���
        /// <summary>
        /// ��ȡһ���ֶε����ݿ�����
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
        /// ��ȡһ���ֶε�CSharp����
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
        /// ͨ��һ���ֶε�ֵ���ʵõ���һ���ֶε�ֵ
        /// </summary>
        static public string GetFieldValueBy(string fromFieldName, string fromFieldValue, string toFieldName)
        {
            string sSQL;
            int nRecordCount;
            DataSet dataSet;
            DataRow row;

            //======= 1. �õ�SQL��� ==============
            sSQL = "select " + toFieldName + "from sbt_sys_daemon_task \n";
            sSQL += "where " + fromFieldName + " = ";
            if (CSharpTypeOfFieldName(fromFieldName) == "string")
                sSQL += "'" + fromFieldValue + "'";
            else
                sSQL += fromFieldValue;

            //======= 2. ����SQL��� ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount == 0)
                return "";

            //======= 3. ��ȡ��¼ ========
            row = dataSet.Tables[0].Rows[0];
            return row[toFieldName].ToString();
        }

        #endregion

    }
}
