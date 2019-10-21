using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.App.Net.IBXService.VoiceQAN.Service.DBDefine
{
    #region 枚举
    /// <summary>
    /// 枚举字段：当前状态
    /// </summary>
    public enum TbLogVoiceQanECurrentStatus
    {
        None,
        [SbtEnumDescString("上传")]
        Upload,
        [SbtEnumDescString("提交请求")]
        Request,
        [SbtEnumDescString("识别中")]
        Qaning,
        [SbtEnumDescString("已识别")]
        Qaned,
        [SbtEnumDescString("结果已取走")]
        ResultFetched
    }
    #endregion

    /// <summary>
    /// 语音识别日志 (vqan_log_voice_qan)的表操作类
    /// </summary>
    public class TbLogVoiceQan : TbLogVoiceQanBase
    {
        #region 枚举属性
        /// <summary>
        /// 当前状态
        /// </summary>
        public TbLogVoiceQanECurrentStatus CurrentStatusE
        {
            get
            {
                TbLogVoiceQanECurrentStatus enumRet = 
                        (TbLogVoiceQanECurrentStatus)ConvertUtil.StringToEnum
                        (this.CurrentStatus, TbLogVoiceQanECurrentStatus.None);
                return enumRet;
            }
            set
            {
                this.CurrentStatus = ConvertUtil.EnumToString(value);
            }
        }
        #endregion

        #region 用户可编辑区域

        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }


    /// <summary>
    /// 语音分析日志 (vqan_log_voice_qan)的字段名类
    /// </summary>
    public class TbLogVoiceQanF
    {
        public const string TableName = "vqan_log_voice_qan";
        public const string LogUid = "log_uid";
        public const string RequestThirdId = "request_third_id";
        public const string VoiceFileLocal = "voice_file_local";
        public const string VoiceFileUpload = "voice_file_upload";
        public const string VoiceFileForQan = "voice_file_for_qan";
        public const string FromSystem = "from_system";
        public const string FromClientId = "from_client_id";
        public const string FromClientDesc = "from_client_desc";
        public const string IsSyncCall = "is_sync_call";
        public const string CurrentStatus = "current_status";
        public const string QualityValue01 = "quality_value_01";
        public const string QualityValue02 = "quality_value_02";
        public const string QualityValue03 = "quality_value_03";
        public const string QualityValue04 = "quality_value_04";
        public const string QualityValue05 = "quality_value_05";
        public const string QualityValue06 = "quality_value_06";
        public const string QualityValue07 = "quality_value_07";
        public const string QualityValue08 = "quality_value_08";
        public const string QualityValue09 = "quality_value_09";
        public const string QualityValue10 = "quality_value_10";
        public const string UploadTime = "upload_time";
        public const string RequestTime = "request_time";
        public const string QanBeginTime = "qan_begin_time";
        public const string QanEndTime = "qan_end_time";
        public const string ResultFetchTime = "result_fetch_time";
        public const string QanDelay = "qan_delay";
        public const string TotalDelay = "total_delay";
        public const string Remarks = "remarks";
    }


    /// <summary>
    /// 语音分析日志 (vqan_log_voice_qan)的表操作基类
    /// </summary>
    public class TbLogVoiceQanBase : Sigbit.Data.TableBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TbLogVoiceQanBase()
        {
            ResetData();
        }

        #region 属性定义
        protected string _logUid = "";
        /// <summary>
        /// 日志标识，主键
        /// </summary>
        public string LogUid
        {
            get { return _logUid; }
            set { _logUid = value; }
        }

        protected string _requestThirdId = "";
        /// <summary>
        /// 第三方的标识数据
        /// </summary>
        public string RequestThirdId
        {
            get { return _requestThirdId; }
            set { _requestThirdId = value; }
        }

        protected string _voiceFileLocal = "";
        /// <summary>
        /// 本地语音文件
        /// </summary>
        public string VoiceFileLocal
        {
            get { return _voiceFileLocal; }
            set { _voiceFileLocal = value; }
        }

        protected string _voiceFileUpload = "";
        /// <summary>
        /// 上传后保存的文件
        /// </summary>
        public string VoiceFileUpload
        {
            get { return _voiceFileUpload; }
            set { _voiceFileUpload = value; }
        }

        protected string _voiceFileForQan = "";
        /// <summary>
        /// 用于识别的文件
        /// </summary>
        public string VoiceFileForQan
        {
            get { return _voiceFileForQan; }
            set { _voiceFileForQan = value; }
        }

        protected string _fromSystem = "";
        /// <summary>
        /// 来自的系统
        /// </summary>
        public string FromSystem
        {
            get { return _fromSystem; }
            set { _fromSystem = value; }
        }

        protected string _fromClientId = "";
        /// <summary>
        /// 来自的客户端
        /// </summary>
        public string FromClientId
        {
            get { return _fromClientId; }
            set { _fromClientId = value; }
        }

        protected string _fromClientDesc = "";
        /// <summary>
        /// 来自的客户端描述
        /// </summary>
        public string FromClientDesc
        {
            get { return _fromClientDesc; }
            set { _fromClientDesc = value; }
        }

        protected string _isSyncCall = "";
        /// <summary>
        /// 是否同步调用
        /// </summary>
        public string IsSyncCall
        {
            get { return _isSyncCall; }
            set { _isSyncCall = value; }
        }

        protected string _currentStatus = "";
        /// <summary>
        /// 当前状态
        /// </summary>
        public string CurrentStatus
        {
            get { return _currentStatus; }
            set { _currentStatus = value; }
        }

        protected double _qualityValue01;
        /// <summary>
        /// 语音质量值
        /// </summary>
        public double QualityValue01
        {
            get { return _qualityValue01; }
            set { _qualityValue01 = value; }
        }

        protected double _qualityValue02;
        /// <summary>
        /// 语音质量值
        /// </summary>
        public double QualityValue02
        {
            get { return _qualityValue02; }
            set { _qualityValue02 = value; }
        }

        protected double _qualityValue03;
        /// <summary>
        /// 语音质量值
        /// </summary>
        public double QualityValue03
        {
            get { return _qualityValue03; }
            set { _qualityValue03 = value; }
        }

        protected double _qualityValue04;
        /// <summary>
        /// 语音质量值
        /// </summary>
        public double QualityValue04
        {
            get { return _qualityValue04; }
            set { _qualityValue04 = value; }
        }

        protected double _qualityValue05;
        /// <summary>
        /// 语音质量值
        /// </summary>
        public double QualityValue05
        {
            get { return _qualityValue05; }
            set { _qualityValue05 = value; }
        }

        protected double _qualityValue06;
        /// <summary>
        /// 语音质量值
        /// </summary>
        public double QualityValue06
        {
            get { return _qualityValue06; }
            set { _qualityValue06 = value; }
        }

        protected double _qualityValue07;
        /// <summary>
        /// 语音质量值
        /// </summary>
        public double QualityValue07
        {
            get { return _qualityValue07; }
            set { _qualityValue07 = value; }
        }

        protected double _qualityValue08;
        /// <summary>
        /// 语音质量值
        /// </summary>
        public double QualityValue08
        {
            get { return _qualityValue08; }
            set { _qualityValue08 = value; }
        }

        protected double _qualityValue09;
        /// <summary>
        /// 语音质量值
        /// </summary>
        public double QualityValue09
        {
            get { return _qualityValue09; }
            set { _qualityValue09 = value; }
        }

        protected double _qualityValue10;
        /// <summary>
        /// 语音质量值
        /// </summary>
        public double QualityValue10
        {
            get { return _qualityValue10; }
            set { _qualityValue10 = value; }
        }

        protected string _uploadTime = "";
        /// <summary>
        /// 上传时间
        /// </summary>
        public string UploadTime
        {
            get { return _uploadTime; }
            set { _uploadTime = value; }
        }

        protected string _requestTime = "";
        /// <summary>
        /// 请求时间
        /// </summary>
        public string RequestTime
        {
            get { return _requestTime; }
            set { _requestTime = value; }
        }

        protected string _qanBeginTime = "";
        /// <summary>
        /// 开始分析时间
        /// </summary>
        public string QanBeginTime
        {
            get { return _qanBeginTime; }
            set { _qanBeginTime = value; }
        }

        protected string _qanEndTime = "";
        /// <summary>
        /// 结束分析时间
        /// </summary>
        public string QanEndTime
        {
            get { return _qanEndTime; }
            set { _qanEndTime = value; }
        }

        protected string _resultFetchTime = "";
        /// <summary>
        /// 取回结果时间
        /// </summary>
        public string ResultFetchTime
        {
            get { return _resultFetchTime; }
            set { _resultFetchTime = value; }
        }

        protected double _qanDelay;
        /// <summary>
        /// 破解时延
        /// </summary>
        public double QanDelay
        {
            get { return _qanDelay; }
            set { _qanDelay = value; }
        }

        protected double _totalDelay;
        /// <summary>
        /// 总时延
        /// </summary>
        public double TotalDelay
        {
            get { return _totalDelay; }
            set { _totalDelay = value; }
        }

        protected string _remarks = "";
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }
        #endregion

        #region 变量的清零及输出
        /// <summary>
        /// 得到数据的HTML显示文本
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("LogUid: " + this._logUid + "<br>");
            sb.Append("RequestThirdId: " + this._requestThirdId + "<br>");
            sb.Append("VoiceFileLocal: " + this._voiceFileLocal + "<br>");
            sb.Append("VoiceFileUpload: " + this._voiceFileUpload + "<br>");
            sb.Append("VoiceFileForQan: " + this._voiceFileForQan + "<br>");
            sb.Append("FromSystem: " + this._fromSystem + "<br>");
            sb.Append("FromClientId: " + this._fromClientId + "<br>");
            sb.Append("FromClientDesc: " + this._fromClientDesc + "<br>");
            sb.Append("IsSyncCall: " + this._isSyncCall + "<br>");
            sb.Append("CurrentStatus: " + this._currentStatus + "<br>");
            sb.Append("QualityValue01: " + this._qualityValue01 + "<br>");
            sb.Append("QualityValue02: " + this._qualityValue02 + "<br>");
            sb.Append("QualityValue03: " + this._qualityValue03 + "<br>");
            sb.Append("QualityValue04: " + this._qualityValue04 + "<br>");
            sb.Append("QualityValue05: " + this._qualityValue05 + "<br>");
            sb.Append("QualityValue06: " + this._qualityValue06 + "<br>");
            sb.Append("QualityValue07: " + this._qualityValue07 + "<br>");
            sb.Append("QualityValue08: " + this._qualityValue08 + "<br>");
            sb.Append("QualityValue09: " + this._qualityValue09 + "<br>");
            sb.Append("QualityValue10: " + this._qualityValue10 + "<br>");
            sb.Append("UploadTime: " + this._uploadTime + "<br>");
            sb.Append("RequestTime: " + this._requestTime + "<br>");
            sb.Append("QanBeginTime: " + this._qanBeginTime + "<br>");
            sb.Append("QanEndTime: " + this._qanEndTime + "<br>");
            sb.Append("ResultFetchTime: " + this._resultFetchTime + "<br>");
            sb.Append("QanDelay: " + this._qanDelay + "<br>");
            sb.Append("TotalDelay: " + this._totalDelay + "<br>");
            sb.Append("Remarks: " + this._remarks + "<br>");
            return sb.ToString();
        }

        /// <summary>
        /// 清零本记录的数据
        /// </summary>
        public override void ResetData()
        {
            _logUid = "";
            _requestThirdId = "";
            _voiceFileLocal = "";
            _voiceFileUpload = "";
            _voiceFileForQan = "";
            _fromSystem = "";
            _fromClientId = "";
            _fromClientDesc = "";
            _isSyncCall = "";
            _currentStatus = "";
            _qualityValue01 = 0;
            _qualityValue02 = 0;
            _qualityValue03 = 0;
            _qualityValue04 = 0;
            _qualityValue05 = 0;
            _qualityValue06 = 0;
            _qualityValue07 = 0;
            _qualityValue08 = 0;
            _qualityValue09 = 0;
            _qualityValue10 = 0;
            _uploadTime = "";
            _requestTime = "";
            _qanBeginTime = "";
            _qanEndTime = "";
            _resultFetchTime = "";
            _qanDelay = 0;
            _totalDelay = 0;
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
            sSQL = "select request_third_id,    voice_file_local,    voice_file_upload,    \r\n";
            sSQL += "       voice_file_for_qan,  from_system,         from_client_id,       \r\n";
            sSQL += "       from_client_desc,    is_sync_call,        current_status,       \r\n";
            sSQL += "       quality_value_01,    quality_value_02,    quality_value_03,     \r\n";
            sSQL += "       quality_value_04,    quality_value_05,    quality_value_06,     \r\n";
            sSQL += "       quality_value_07,    quality_value_08,    quality_value_09,     \r\n";
            sSQL += "       quality_value_10,    upload_time,         request_time,         \r\n";
            sSQL += "       qan_begin_time,      qan_end_time,        result_fetch_time,    \r\n";
            sSQL += "       qan_delay,           total_delay,         remarks              \r\n";
            sSQL += "  from vqan_log_voice_qan    \r\n";
            sSQL += "  where log_uid = " + Quote(_logUid) + "\r\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbLogVoiceQan.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _requestThirdId = DbToString(row["request_third_id"]);
            _voiceFileLocal = DbToString(row["voice_file_local"]);
            _voiceFileUpload = DbToString(row["voice_file_upload"]);
            _voiceFileForQan = DbToString(row["voice_file_for_qan"]);
            _fromSystem = DbToString(row["from_system"]);
            _fromClientId = DbToString(row["from_client_id"]);
            _fromClientDesc = DbToString(row["from_client_desc"]);
            _isSyncCall = DbToString(row["is_sync_call"]);
            _currentStatus = DbToString(row["current_status"]);
            _qualityValue01 = DbToDouble(row["quality_value_01"]);
            _qualityValue02 = DbToDouble(row["quality_value_02"]);
            _qualityValue03 = DbToDouble(row["quality_value_03"]);
            _qualityValue04 = DbToDouble(row["quality_value_04"]);
            _qualityValue05 = DbToDouble(row["quality_value_05"]);
            _qualityValue06 = DbToDouble(row["quality_value_06"]);
            _qualityValue07 = DbToDouble(row["quality_value_07"]);
            _qualityValue08 = DbToDouble(row["quality_value_08"]);
            _qualityValue09 = DbToDouble(row["quality_value_09"]);
            _qualityValue10 = DbToDouble(row["quality_value_10"]);
            _uploadTime = DbToString(row["upload_time"]);
            _requestTime = DbToString(row["request_time"]);
            _qanBeginTime = DbToString(row["qan_begin_time"]);
            _qanEndTime = DbToString(row["qan_end_time"]);
            _resultFetchTime = DbToString(row["result_fetch_time"]);
            _qanDelay = DbToDouble(row["qan_delay"]);
            _totalDelay = DbToDouble(row["total_delay"]);
            _remarks = DbToString(row["remarks"]);
            return true;
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        public override void Insert()
        {
            string sSQL;

            sSQL = "insert into vqan_log_voice_qan \r\n";
            sSQL += "( log_uid,          request_third_id, \r\n";
            sSQL += "  voice_file_local, voice_file_upload, \r\n";
            sSQL += "  voice_file_for_qan, from_system,      \r\n";
            sSQL += "  from_client_id,   from_client_desc, \r\n";
            sSQL += "  is_sync_call,     current_status,   \r\n";
            sSQL += "  quality_value_01, quality_value_02, \r\n";
            sSQL += "  quality_value_03, quality_value_04, \r\n";
            sSQL += "  quality_value_05, quality_value_06, \r\n";
            sSQL += "  quality_value_07, quality_value_08, \r\n";
            sSQL += "  quality_value_09, quality_value_10, \r\n";
            sSQL += "  upload_time,      request_time,     \r\n";
            sSQL += "  qan_begin_time,   qan_end_time,     \r\n";
            sSQL += "  result_fetch_time, qan_delay,        \r\n";
            sSQL += "  total_delay,      remarks           \r\n";
            sSQL += ") values (          \r\n";
            sSQL += Quote(_logUid) + "," + Quote(_requestThirdId) + ",\r\n";
            sSQL += Quote(_voiceFileLocal) + "," + Quote(_voiceFileUpload) + ",\r\n";
            sSQL += Quote(_voiceFileForQan) + "," + Quote(_fromSystem) + ",\r\n";
            sSQL += Quote(_fromClientId) + "," + Quote(_fromClientDesc) + ",\r\n";
            sSQL += Quote(_isSyncCall) + "," + Quote(_currentStatus) + ",\r\n";
            sSQL += _qualityValue01.ToString() + "," + _qualityValue02.ToString() + ",\r\n";
            sSQL += _qualityValue03.ToString() + "," + _qualityValue04.ToString() + ",\r\n";
            sSQL += _qualityValue05.ToString() + "," + _qualityValue06.ToString() + ",\r\n";
            sSQL += _qualityValue07.ToString() + "," + _qualityValue08.ToString() + ",\r\n";
            sSQL += _qualityValue09.ToString() + "," + _qualityValue10.ToString() + ",\r\n";
            sSQL += Quote(_uploadTime) + "," + Quote(_requestTime) + ",\r\n";
            sSQL += Quote(_qanBeginTime) + "," + Quote(_qanEndTime) + ",\r\n";
            sSQL += Quote(_resultFetchTime) + "," + _qanDelay.ToString() + ",\r\n";
            sSQL += _totalDelay.ToString() + "," + Quote(_remarks) + ")\r\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 按主键删除一条数据
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL = "delete from vqan_log_voice_qan \r\n";
            sSQL += "  where log_uid = " + Quote(_logUid) + "\r\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbLogVoiceQan.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL = "update vqan_log_voice_qan set \r\n";
            sSQL += " log_uid = " + Quote(_logUid) + ",\r\n";
            sSQL += " request_third_id = " + Quote(_requestThirdId) + ",\r\n";
            sSQL += " voice_file_local = " + Quote(_voiceFileLocal) + ",\r\n";
            sSQL += " voice_file_upload = " + Quote(_voiceFileUpload) + ",\r\n";
            sSQL += " voice_file_for_qan = " + Quote(_voiceFileForQan) + ",\r\n";
            sSQL += " from_system = " + Quote(_fromSystem) + ",\r\n";
            sSQL += " from_client_id = " + Quote(_fromClientId) + ",\r\n";
            sSQL += " from_client_desc = " + Quote(_fromClientDesc) + ",\r\n";
            sSQL += " is_sync_call = " + Quote(_isSyncCall) + ",\r\n";
            sSQL += " current_status = " + Quote(_currentStatus) + ",\r\n";
            sSQL += " quality_value_01 = " + _qualityValue01.ToString() + ",\r\n";
            sSQL += " quality_value_02 = " + _qualityValue02.ToString() + ",\r\n";
            sSQL += " quality_value_03 = " + _qualityValue03.ToString() + ",\r\n";
            sSQL += " quality_value_04 = " + _qualityValue04.ToString() + ",\r\n";
            sSQL += " quality_value_05 = " + _qualityValue05.ToString() + ",\r\n";
            sSQL += " quality_value_06 = " + _qualityValue06.ToString() + ",\r\n";
            sSQL += " quality_value_07 = " + _qualityValue07.ToString() + ",\r\n";
            sSQL += " quality_value_08 = " + _qualityValue08.ToString() + ",\r\n";
            sSQL += " quality_value_09 = " + _qualityValue09.ToString() + ",\r\n";
            sSQL += " quality_value_10 = " + _qualityValue10.ToString() + ",\r\n";
            sSQL += " upload_time = " + Quote(_uploadTime) + ",\r\n";
            sSQL += " request_time = " + Quote(_requestTime) + ",\r\n";
            sSQL += " qan_begin_time = " + Quote(_qanBeginTime) + ",\r\n";
            sSQL += " qan_end_time = " + Quote(_qanEndTime) + ",\r\n";
            sSQL += " result_fetch_time = " + Quote(_resultFetchTime) + ",\r\n";
            sSQL += " qan_delay = " + _qanDelay.ToString() + ",\r\n";
            sSQL += " total_delay = " + _totalDelay.ToString() + ",\r\n";
            sSQL += " remarks = " + Quote(_remarks) + "\r\n";
            sSQL += "  where log_uid = " + Quote(_logUid) + "\r\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbLogVoiceQan.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from vqan_log_voice_qan \r\n";
            sSQL += "  where log_uid = " + Quote(_logUid) + "\r\n";

            //======= 2. 运行SQL语句 ========
            nRecordCount = Convert.ToInt32(DataHelper.Instance.ExecuteScalar(sSQL));
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
        public void FetchByE(string logUid)
        {
            bool hasData;
            hasData = FetchBy(logUid);
            if (!hasData)
                throw new Exception("TbLogVoiceQan.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string logUid)
        {
            _logUid = logUid;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbLogVoiceQan CreateBy(string logUid)
        {
            TbLogVoiceQan tbl;
            bool hasData;

            tbl = new TbLogVoiceQan();
            hasData = tbl.FetchBy(logUid);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string logUid)
        {
            TbLogVoiceQan tbl;
            tbl = new TbLogVoiceQan();

            tbl.LogUid = logUid;

            tbl.Delete();
        }
        #endregion

        #region 文件和访问组件相关的支持函数
        /// <summary>
        /// 通过DataRow进行赋值
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _logUid = DbToString(row["log_uid"]);
            _requestThirdId = DbToString(row["request_third_id"]);
            _voiceFileLocal = DbToString(row["voice_file_local"]);
            _voiceFileUpload = DbToString(row["voice_file_upload"]);
            _voiceFileForQan = DbToString(row["voice_file_for_qan"]);
            _fromSystem = DbToString(row["from_system"]);
            _fromClientId = DbToString(row["from_client_id"]);
            _fromClientDesc = DbToString(row["from_client_desc"]);
            _isSyncCall = DbToString(row["is_sync_call"]);
            _currentStatus = DbToString(row["current_status"]);
            _qualityValue01 = DbToDouble(row["quality_value_01"]);
            _qualityValue02 = DbToDouble(row["quality_value_02"]);
            _qualityValue03 = DbToDouble(row["quality_value_03"]);
            _qualityValue04 = DbToDouble(row["quality_value_04"]);
            _qualityValue05 = DbToDouble(row["quality_value_05"]);
            _qualityValue06 = DbToDouble(row["quality_value_06"]);
            _qualityValue07 = DbToDouble(row["quality_value_07"]);
            _qualityValue08 = DbToDouble(row["quality_value_08"]);
            _qualityValue09 = DbToDouble(row["quality_value_09"]);
            _qualityValue10 = DbToDouble(row["quality_value_10"]);
            _uploadTime = DbToString(row["upload_time"]);
            _requestTime = DbToString(row["request_time"]);
            _qanBeginTime = DbToString(row["qan_begin_time"]);
            _qanEndTime = DbToString(row["qan_end_time"]);
            _resultFetchTime = DbToString(row["result_fetch_time"]);
            _qanDelay = DbToDouble(row["qan_delay"]);
            _totalDelay = DbToDouble(row["total_delay"]);
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
            sLine = "log_uid\x9" + _logUid;
            writer.WriteLine(sLine);

            sLine = "request_third_id\x9" + _requestThirdId;
            writer.WriteLine(sLine);

            sLine = "voice_file_local\x9" + _voiceFileLocal;
            writer.WriteLine(sLine);

            sLine = "voice_file_upload\x9" + _voiceFileUpload;
            writer.WriteLine(sLine);

            sLine = "voice_file_for_qan\x9" + _voiceFileForQan;
            writer.WriteLine(sLine);

            sLine = "from_system\x9" + _fromSystem;
            writer.WriteLine(sLine);

            sLine = "from_client_id\x9" + _fromClientId;
            writer.WriteLine(sLine);

            sLine = "from_client_desc\x9" + _fromClientDesc;
            writer.WriteLine(sLine);

            sLine = "is_sync_call\x9" + _isSyncCall;
            writer.WriteLine(sLine);

            sLine = "current_status\x9" + _currentStatus;
            writer.WriteLine(sLine);

            sLine = "quality_value_01\x9" + _qualityValue01.ToString();
            writer.WriteLine(sLine);

            sLine = "quality_value_02\x9" + _qualityValue02.ToString();
            writer.WriteLine(sLine);

            sLine = "quality_value_03\x9" + _qualityValue03.ToString();
            writer.WriteLine(sLine);

            sLine = "quality_value_04\x9" + _qualityValue04.ToString();
            writer.WriteLine(sLine);

            sLine = "quality_value_05\x9" + _qualityValue05.ToString();
            writer.WriteLine(sLine);

            sLine = "quality_value_06\x9" + _qualityValue06.ToString();
            writer.WriteLine(sLine);

            sLine = "quality_value_07\x9" + _qualityValue07.ToString();
            writer.WriteLine(sLine);

            sLine = "quality_value_08\x9" + _qualityValue08.ToString();
            writer.WriteLine(sLine);

            sLine = "quality_value_09\x9" + _qualityValue09.ToString();
            writer.WriteLine(sLine);

            sLine = "quality_value_10\x9" + _qualityValue10.ToString();
            writer.WriteLine(sLine);

            sLine = "upload_time\x9" + _uploadTime;
            writer.WriteLine(sLine);

            sLine = "request_time\x9" + _requestTime;
            writer.WriteLine(sLine);

            sLine = "qan_begin_time\x9" + _qanBeginTime;
            writer.WriteLine(sLine);

            sLine = "qan_end_time\x9" + _qanEndTime;
            writer.WriteLine(sLine);

            sLine = "result_fetch_time\x9" + _resultFetchTime;
            writer.WriteLine(sLine);

            sLine = "qan_delay\x9" + _qanDelay.ToString();
            writer.WriteLine(sLine);

            sLine = "total_delay\x9" + _totalDelay.ToString();
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
            sLine = "log_uid\trequest_third_id\tvoice_file_local\t";
            sLine += "voice_file_upload\tvoice_file_for_qan\tfrom_system\t";
            sLine += "from_client_id\tfrom_client_desc\tis_sync_call\t";
            sLine += "current_status\tquality_value_01\tquality_value_02\t";
            sLine += "quality_value_03\tquality_value_04\tquality_value_05\t";
            sLine += "quality_value_06\tquality_value_07\tquality_value_08\t";
            sLine += "quality_value_09\tquality_value_10\tupload_time\t";
            sLine += "request_time\tqan_begin_time\tqan_end_time\t";
            sLine += "result_fetch_time\tqan_delay\ttotal_delay\t";
            sLine += "remarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL = "select log_uid,         request_third_id,voice_file_local,\r\n";
            sSQL += "       voice_file_upload,voice_file_for_qan,from_system,     \r\n";
            sSQL += "       from_client_id,  from_client_desc,is_sync_call,    \r\n";
            sSQL += "       current_status,  quality_value_01,quality_value_02,\r\n";
            sSQL += "       quality_value_03,quality_value_04,quality_value_05,\r\n";
            sSQL += "       quality_value_06,quality_value_07,quality_value_08,\r\n";
            sSQL += "       quality_value_09,quality_value_10,upload_time,     \r\n";
            sSQL += "       request_time,    qan_begin_time,  qan_end_time,    \r\n";
            sSQL += "       result_fetch_time,qan_delay,       total_delay,     \r\n";
            sSQL += "       remarks         \r\n";
            sSQL += "  from vqan_log_voice_qan";

            //======== 4. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. 处理每一行 ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 处理每一列 ========
                for (nCol = 0; nCol < 28; nCol++)
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
                case "log_uid":
                    return "varchar";
                case "request_third_id":
                    return "varchar";
                case "voice_file_local":
                    return "varchar";
                case "voice_file_upload":
                    return "varchar";
                case "voice_file_for_qan":
                    return "varchar";
                case "from_system":
                    return "varchar";
                case "from_client_id":
                    return "varchar";
                case "from_client_desc":
                    return "varchar";
                case "is_sync_call":
                    return "char";
                case "current_status":
                    return "varchar";
                case "quality_value_01":
                    return "numeric";
                case "quality_value_02":
                    return "numeric";
                case "quality_value_03":
                    return "numeric";
                case "quality_value_04":
                    return "numeric";
                case "quality_value_05":
                    return "numeric";
                case "quality_value_06":
                    return "numeric";
                case "quality_value_07":
                    return "numeric";
                case "quality_value_08":
                    return "numeric";
                case "quality_value_09":
                    return "numeric";
                case "quality_value_10":
                    return "numeric";
                case "upload_time":
                    return "varchar";
                case "request_time":
                    return "varchar";
                case "qan_begin_time":
                    return "varchar";
                case "qan_end_time":
                    return "varchar";
                case "result_fetch_time":
                    return "varchar";
                case "qan_delay":
                    return "numeric";
                case "total_delay":
                    return "numeric";
                case "remarks":
                    return "varchar";
                default:
                    throw new Exception("TbLogVoiceQanBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 获取一个字段的CSharp类型
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "log_uid":
                    return "string";
                case "request_third_id":
                    return "string";
                case "voice_file_local":
                    return "string";
                case "voice_file_upload":
                    return "string";
                case "voice_file_for_qan":
                    return "string";
                case "from_system":
                    return "string";
                case "from_client_id":
                    return "string";
                case "from_client_desc":
                    return "string";
                case "is_sync_call":
                    return "string";
                case "current_status":
                    return "string";
                case "quality_value_01":
                    return "double";
                case "quality_value_02":
                    return "double";
                case "quality_value_03":
                    return "double";
                case "quality_value_04":
                    return "double";
                case "quality_value_05":
                    return "double";
                case "quality_value_06":
                    return "double";
                case "quality_value_07":
                    return "double";
                case "quality_value_08":
                    return "double";
                case "quality_value_09":
                    return "double";
                case "quality_value_10":
                    return "double";
                case "upload_time":
                    return "string";
                case "request_time":
                    return "string";
                case "qan_begin_time":
                    return "string";
                case "qan_end_time":
                    return "string";
                case "result_fetch_time":
                    return "string";
                case "qan_delay":
                    return "double";
                case "total_delay":
                    return "double";
                case "remarks":
                    return "string";
                default:
                    throw new Exception("TbLogVoiceQanBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL = "select " + toFieldName + " from vqan_log_voice_qan \r\n";
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
