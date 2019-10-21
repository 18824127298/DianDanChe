using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.App.Net.IBXService.VoiceReg.Service.DBDefine
{
    /// <summary>
    /// 语音识别日志 (vreg_log_voice_isr)的表操作用户类
    /// </summary>
    public class TbLogVoiceIsr : TbLogVoiceIsrBase
    {
        #region 用户可编辑区域

        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }


    /// <summary>
    /// 语音识别日志 (vreg_log_voice_isr)的字段名类
    /// </summary>
    public class TbLogVoiceIsrF
    {
        public const string TableName = "vreg_log_voice_isr";
        public const string LogUid               = "log_uid";
        public const string ReqeustThirdId       = "reqeust_third_id";
        public const string VoiceFileLocal       = "voice_file_local";
        public const string VoiceFileUpload      = "voice_file_upload";
        public const string VoiceFileForIsr      = "voice_file_for_isr";
        public const string GrammarId            = "grammar_id";
        public const string FromSystem           = "from_system";
        public const string FromClientId         = "from_client_id";
        public const string FromClientDesc       = "from_client_desc";
        public const string CurrentStatus        = "current_status";
        public const string RegResultCode        = "reg_result_code";
        public const string RegFailDesc          = "reg_fail_desc";
        public const string RegResultText        = "reg_result_text";
        public const string UploadTime           = "upload_time";
        public const string RequestTime          = "request_time";
        public const string IsrBeginTime         = "isr_begin_time";
        public const string IsrEndTime           = "isr_end_time";
        public const string ResultFetchTime      = "result_fetch_time";
        public const string BreakDelay           = "break_delay";
        public const string TotalDelay           = "total_delay";
        public const string Remarks              = "remarks";
    }


    /// <summary>
    /// 语音识别日志 (vreg_log_voice_isr)的表操作基类
    /// </summary>
    public class TbLogVoiceIsrBase : Sigbit.Data.TableBase
    {
        #region 私有变量定义
        protected string     _logUid                  = "";
        protected string     _reqeustThirdId          = "";
        protected string     _voiceFileLocal          = "";
        protected string     _voiceFileUpload         = "";
        protected string     _voiceFileForIsr         = "";
        protected string     _grammarId               = "";
        protected string     _fromSystem              = "";
        protected string     _fromClientId            = "";
        protected string     _fromClientDesc          = "";
        protected string     _currentStatus           = "";
        protected string     _regResultCode           = "";
        protected string     _regFailDesc             = "";
        protected string     _regResultText           = "";
        protected string     _uploadTime              = "";
        protected string     _requestTime             = "";
        protected string     _isrBeginTime            = "";
        protected string     _isrEndTime              = "";
        protected string     _resultFetchTime         = "";
        protected double     _breakDelay             ;
        protected double     _totalDelay             ;
        protected string     _remarks                 = "";
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public TbLogVoiceIsrBase()
        {
            ResetData();
        }

        #region 属性定义
        /// <summary>
        /// 日志标识，主键
        /// </summary>
        public string LogUid
        {
            get
            {
                return _logUid;
            }
            set
            {
                _logUid = value;
            }
        }

        /// <summary>
        /// 第三方的标识数据
        /// </summary>
        public string ReqeustThirdId
        {
            get
            {
                return _reqeustThirdId;
            }
            set
            {
                _reqeustThirdId = value;
            }
        }

        /// <summary>
        /// 本地语音文件
        /// </summary>
        public string VoiceFileLocal
        {
            get
            {
                return _voiceFileLocal;
            }
            set
            {
                _voiceFileLocal = value;
            }
        }

        /// <summary>
        /// 上传后保存的文件
        /// </summary>
        public string VoiceFileUpload
        {
            get
            {
                return _voiceFileUpload;
            }
            set
            {
                _voiceFileUpload = value;
            }
        }

        /// <summary>
        /// 用于识别的文件
        /// </summary>
        public string VoiceFileForIsr
        {
            get
            {
                return _voiceFileForIsr;
            }
            set
            {
                _voiceFileForIsr = value;
            }
        }

        /// <summary>
        /// 语法标识
        /// </summary>
        public string GrammarId
        {
            get
            {
                return _grammarId;
            }
            set
            {
                _grammarId = value;
            }
        }

        /// <summary>
        /// 来自的系统
        /// </summary>
        public string FromSystem
        {
            get
            {
                return _fromSystem;
            }
            set
            {
                _fromSystem = value;
            }
        }

        /// <summary>
        /// 来自的客户端
        /// </summary>
        public string FromClientId
        {
            get
            {
                return _fromClientId;
            }
            set
            {
                _fromClientId = value;
            }
        }

        /// <summary>
        /// 来自的客户端描述
        /// </summary>
        public string FromClientDesc
        {
            get
            {
                return _fromClientDesc;
            }
            set
            {
                _fromClientDesc = value;
            }
        }

        /// <summary>
        /// 当前状态
        /// </summary>
        public string CurrentStatus
        {
            get
            {
                return _currentStatus;
            }
            set
            {
                _currentStatus = value;
            }
        }

        /// <summary>
        /// 破解结果
        /// </summary>
        public string RegResultCode
        {
            get
            {
                return _regResultCode;
            }
            set
            {
                _regResultCode = value;
            }
        }

        /// <summary>
        /// 失败原因
        /// </summary>
        public string RegFailDesc
        {
            get
            {
                return _regFailDesc;
            }
            set
            {
                _regFailDesc = value;
            }
        }

        /// <summary>
        /// 破解的验证码文字
        /// </summary>
        public string RegResultText
        {
            get
            {
                return _regResultText;
            }
            set
            {
                _regResultText = value;
            }
        }

        /// <summary>
        /// 上传时间
        /// </summary>
        public string UploadTime
        {
            get
            {
                return _uploadTime;
            }
            set
            {
                _uploadTime = value;
            }
        }

        /// <summary>
        /// 请求时间
        /// </summary>
        public string RequestTime
        {
            get
            {
                return _requestTime;
            }
            set
            {
                _requestTime = value;
            }
        }

        /// <summary>
        /// 开始识别时间
        /// </summary>
        public string IsrBeginTime
        {
            get
            {
                return _isrBeginTime;
            }
            set
            {
                _isrBeginTime = value;
            }
        }

        /// <summary>
        /// 结束识别时间
        /// </summary>
        public string IsrEndTime
        {
            get
            {
                return _isrEndTime;
            }
            set
            {
                _isrEndTime = value;
            }
        }

        /// <summary>
        /// 取回结果时间
        /// </summary>
        public string ResultFetchTime
        {
            get
            {
                return _resultFetchTime;
            }
            set
            {
                _resultFetchTime = value;
            }
        }

        /// <summary>
        /// 破解时延
        /// </summary>
        public double BreakDelay
        {
            get
            {
                return _breakDelay;
            }
            set
            {
                _breakDelay = value;
            }
        }

        /// <summary>
        /// 总时延
        /// </summary>
        public double TotalDelay
        {
            get
            {
                return _totalDelay;
            }
            set
            {
                _totalDelay = value;
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
            sb.Append("LogUid: " + this._logUid + "<br>");
            sb.Append("ReqeustThirdId: " + this._reqeustThirdId + "<br>");
            sb.Append("VoiceFileLocal: " + this._voiceFileLocal + "<br>");
            sb.Append("VoiceFileUpload: " + this._voiceFileUpload + "<br>");
            sb.Append("VoiceFileForIsr: " + this._voiceFileForIsr + "<br>");
            sb.Append("GrammarId: " + this._grammarId + "<br>");
            sb.Append("FromSystem: " + this._fromSystem + "<br>");
            sb.Append("FromClientId: " + this._fromClientId + "<br>");
            sb.Append("FromClientDesc: " + this._fromClientDesc + "<br>");
            sb.Append("CurrentStatus: " + this._currentStatus + "<br>");
            sb.Append("RegResultCode: " + this._regResultCode + "<br>");
            sb.Append("RegFailDesc: " + this._regFailDesc + "<br>");
            sb.Append("RegResultText: " + this._regResultText + "<br>");
            sb.Append("UploadTime: " + this._uploadTime + "<br>");
            sb.Append("RequestTime: " + this._requestTime + "<br>");
            sb.Append("IsrBeginTime: " + this._isrBeginTime + "<br>");
            sb.Append("IsrEndTime: " + this._isrEndTime + "<br>");
            sb.Append("ResultFetchTime: " + this._resultFetchTime + "<br>");
            sb.Append("BreakDelay: " + this._breakDelay + "<br>");
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
            _reqeustThirdId = "";
            _voiceFileLocal = "";
            _voiceFileUpload = "";
            _voiceFileForIsr = "";
            _grammarId = "";
            _fromSystem = "";
            _fromClientId = "";
            _fromClientDesc = "";
            _currentStatus = "";
            _regResultCode = "";
            _regFailDesc = "";
            _regResultText = "";
            _uploadTime = "";
            _requestTime = "";
            _isrBeginTime = "";
            _isrEndTime = "";
            _resultFetchTime = "";
            _breakDelay = 0;
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
            sSQL  = "select reqeust_third_id,    voice_file_local,    voice_file_upload,    \n";
            sSQL += "       voice_file_for_isr,  grammar_id,          from_system,          \n";
            sSQL += "       from_client_id,      from_client_desc,    current_status,       \n";
            sSQL += "       reg_result_code,     reg_fail_desc,       reg_result_text,      \n";
            sSQL += "       upload_time,         request_time,        isr_begin_time,       \n";
            sSQL += "       isr_end_time,        result_fetch_time,   break_delay,          \n";
            sSQL += "       total_delay,         remarks              \n";
            sSQL += "  from vreg_log_voice_isr    \n";
            sSQL += "  where log_uid = " + Quote(_logUid) + "\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbLogVoiceIsr.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _reqeustThirdId = DbToString(row["reqeust_third_id"]);
            _voiceFileLocal = DbToString(row["voice_file_local"]);
            _voiceFileUpload = DbToString(row["voice_file_upload"]);
            _voiceFileForIsr = DbToString(row["voice_file_for_isr"]);
            _grammarId      = DbToString(row["grammar_id"]);
            _fromSystem     = DbToString(row["from_system"]);
            _fromClientId   = DbToString(row["from_client_id"]);
            _fromClientDesc = DbToString(row["from_client_desc"]);
            _currentStatus  = DbToString(row["current_status"]);
            _regResultCode  = DbToString(row["reg_result_code"]);
            _regFailDesc    = DbToString(row["reg_fail_desc"]);
            _regResultText  = DbToString(row["reg_result_text"]);
            _uploadTime     = DbToString(row["upload_time"]);
            _requestTime    = DbToString(row["request_time"]);
            _isrBeginTime   = DbToString(row["isr_begin_time"]);
            _isrEndTime     = DbToString(row["isr_end_time"]);
            _resultFetchTime = DbToString(row["result_fetch_time"]);
            _breakDelay     = DbToDouble(row["break_delay"]);
            _totalDelay     = DbToDouble(row["total_delay"]);
            _remarks        = DbToString(row["remarks"]);
            return true;
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        public override void Insert()
        {
            string sSQL;

            sSQL  = "insert into vreg_log_voice_isr \n";
            sSQL += "( log_uid,          reqeust_third_id, \n";
            sSQL += "  voice_file_local, voice_file_upload, \n";
            sSQL += "  voice_file_for_isr, grammar_id,       \n";
            sSQL += "  from_system,      from_client_id,   \n";
            sSQL += "  from_client_desc, current_status,   \n";
            sSQL += "  reg_result_code,  reg_fail_desc,    \n";
            sSQL += "  reg_result_text,  upload_time,      \n";
            sSQL += "  request_time,     isr_begin_time,   \n";
            sSQL += "  isr_end_time,     result_fetch_time, \n";
            sSQL += "  break_delay,      total_delay,      \n";
            sSQL += "  remarks           \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_logUid)          +","+ Quote(_reqeustThirdId)  +",\n";
            sSQL += Quote(_voiceFileLocal)  +","+ Quote(_voiceFileUpload) +",\n";
            sSQL += Quote(_voiceFileForIsr) +","+ Quote(_grammarId)       +",\n";
            sSQL += Quote(_fromSystem)      +","+ Quote(_fromClientId)    +",\n";
            sSQL += Quote(_fromClientDesc)  +","+ Quote(_currentStatus)   +",\n";
            sSQL += Quote(_regResultCode)   +","+ Quote(_regFailDesc)     +",\n";
            sSQL += Quote(_regResultText)   +","+ Quote(_uploadTime)      +",\n";
            sSQL += Quote(_requestTime)     +","+ Quote(_isrBeginTime)    +",\n";
            sSQL += Quote(_isrEndTime)      +","+ Quote(_resultFetchTime) +",\n";
            sSQL += _breakDelay.ToString()  +","+ _totalDelay.ToString()  +",\n";
            sSQL += Quote(_remarks)         +")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 按主键删除一条数据
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL  = "delete from vreg_log_voice_isr \n";
            sSQL += "  where log_uid = " + Quote(_logUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbLogVoiceIsr.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL  = "update vreg_log_voice_isr set \n";
            sSQL += " log_uid = " + Quote(_logUid) + ",\n";
            sSQL += " reqeust_third_id = " + Quote(_reqeustThirdId) + ",\n";
            sSQL += " voice_file_local = " + Quote(_voiceFileLocal) + ",\n";
            sSQL += " voice_file_upload = " + Quote(_voiceFileUpload) + ",\n";
            sSQL += " voice_file_for_isr = " + Quote(_voiceFileForIsr) + ",\n";
            sSQL += " grammar_id = " + Quote(_grammarId) + ",\n";
            sSQL += " from_system = " + Quote(_fromSystem) + ",\n";
            sSQL += " from_client_id = " + Quote(_fromClientId) + ",\n";
            sSQL += " from_client_desc = " + Quote(_fromClientDesc) + ",\n";
            sSQL += " current_status = " + Quote(_currentStatus) + ",\n";
            sSQL += " reg_result_code = " + Quote(_regResultCode) + ",\n";
            sSQL += " reg_fail_desc = " + Quote(_regFailDesc) + ",\n";
            sSQL += " reg_result_text = " + Quote(_regResultText) + ",\n";
            sSQL += " upload_time = " + Quote(_uploadTime) + ",\n";
            sSQL += " request_time = " + Quote(_requestTime) + ",\n";
            sSQL += " isr_begin_time = " + Quote(_isrBeginTime) + ",\n";
            sSQL += " isr_end_time = " + Quote(_isrEndTime) + ",\n";
            sSQL += " result_fetch_time = " + Quote(_resultFetchTime) + ",\n";
            sSQL += " break_delay = " + _breakDelay.ToString() + ",\n";
            sSQL += " total_delay = " + _totalDelay.ToString() + ",\n";
            sSQL += " remarks = " + Quote(_remarks) + "\n";
            sSQL += "  where log_uid = " + Quote(_logUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbLogVoiceIsr.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from vreg_log_voice_isr \n";
            sSQL += "  where log_uid = " + Quote(_logUid) + "\n";

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
                throw new Exception("TbLogVoiceIsr.FetchBy(...) Error - cannot locate record via PrimaryKey.");
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
        static public TbLogVoiceIsr CreateBy(string logUid)
        {
            TbLogVoiceIsr tbl;
            bool hasData;

            tbl = new TbLogVoiceIsr();
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
            TbLogVoiceIsr tbl;
            tbl = new TbLogVoiceIsr();

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
            _logUid              = DbToString(row["log_uid"]);
            _reqeustThirdId      = DbToString(row["reqeust_third_id"]);
            _voiceFileLocal      = DbToString(row["voice_file_local"]);
            _voiceFileUpload     = DbToString(row["voice_file_upload"]);
            _voiceFileForIsr     = DbToString(row["voice_file_for_isr"]);
            _grammarId           = DbToString(row["grammar_id"]);
            _fromSystem          = DbToString(row["from_system"]);
            _fromClientId        = DbToString(row["from_client_id"]);
            _fromClientDesc      = DbToString(row["from_client_desc"]);
            _currentStatus       = DbToString(row["current_status"]);
            _regResultCode       = DbToString(row["reg_result_code"]);
            _regFailDesc         = DbToString(row["reg_fail_desc"]);
            _regResultText       = DbToString(row["reg_result_text"]);
            _uploadTime          = DbToString(row["upload_time"]);
            _requestTime         = DbToString(row["request_time"]);
            _isrBeginTime        = DbToString(row["isr_begin_time"]);
            _isrEndTime          = DbToString(row["isr_end_time"]);
            _resultFetchTime     = DbToString(row["result_fetch_time"]);
            _breakDelay          = DbToDouble(row["break_delay"]);
            _totalDelay          = DbToDouble(row["total_delay"]);
            _remarks             = DbToString(row["remarks"]);
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

            sLine = "reqeust_third_id\x9" + _reqeustThirdId;
            writer.WriteLine(sLine);

            sLine = "voice_file_local\x9" + _voiceFileLocal;
            writer.WriteLine(sLine);

            sLine = "voice_file_upload\x9" + _voiceFileUpload;
            writer.WriteLine(sLine);

            sLine = "voice_file_for_isr\x9" + _voiceFileForIsr;
            writer.WriteLine(sLine);

            sLine = "grammar_id\x9" + _grammarId;
            writer.WriteLine(sLine);

            sLine = "from_system\x9" + _fromSystem;
            writer.WriteLine(sLine);

            sLine = "from_client_id\x9" + _fromClientId;
            writer.WriteLine(sLine);

            sLine = "from_client_desc\x9" + _fromClientDesc;
            writer.WriteLine(sLine);

            sLine = "current_status\x9" + _currentStatus;
            writer.WriteLine(sLine);

            sLine = "reg_result_code\x9" + _regResultCode;
            writer.WriteLine(sLine);

            sLine = "reg_fail_desc\x9" + _regFailDesc;
            writer.WriteLine(sLine);

            sLine = "reg_result_text\x9" + _regResultText;
            writer.WriteLine(sLine);

            sLine = "upload_time\x9" + _uploadTime;
            writer.WriteLine(sLine);

            sLine = "request_time\x9" + _requestTime;
            writer.WriteLine(sLine);

            sLine = "isr_begin_time\x9" + _isrBeginTime;
            writer.WriteLine(sLine);

            sLine = "isr_end_time\x9" + _isrEndTime;
            writer.WriteLine(sLine);

            sLine = "result_fetch_time\x9" + _resultFetchTime;
            writer.WriteLine(sLine);

            sLine = "break_delay\x9" + _breakDelay.ToString();
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
            sLine  = "log_uid\treqeust_third_id\tvoice_file_local\t";
            sLine += "voice_file_upload\tvoice_file_for_isr\tgrammar_id\t";
            sLine += "from_system\tfrom_client_id\tfrom_client_desc\t";
            sLine += "current_status\treg_result_code\treg_fail_desc\t";
            sLine += "reg_result_text\tupload_time\trequest_time\t";
            sLine += "isr_begin_time\tisr_end_time\tresult_fetch_time\t";
            sLine += "break_delay\ttotal_delay\tremarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL  = "select log_uid,         reqeust_third_id,voice_file_local,\n";
            sSQL += "       voice_file_upload,voice_file_for_isr,grammar_id,      \n";
            sSQL += "       from_system,     from_client_id,  from_client_desc,\n";
            sSQL += "       current_status,  reg_result_code, reg_fail_desc,   \n";
            sSQL += "       reg_result_text, upload_time,     request_time,    \n";
            sSQL += "       isr_begin_time,  isr_end_time,    result_fetch_time,\n";
            sSQL += "       break_delay,     total_delay,     remarks         \n";
            sSQL += "  from vreg_log_voice_isr";

            //======== 4. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. 处理每一行 ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 处理每一列 ========
                for (nCol = 0; nCol < 21; nCol++)
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
                case "reqeust_third_id":
                    return "varchar";
                case "voice_file_local":
                    return "varchar";
                case "voice_file_upload":
                    return "varchar";
                case "voice_file_for_isr":
                    return "varchar";
                case "grammar_id":
                    return "varchar";
                case "from_system":
                    return "varchar";
                case "from_client_id":
                    return "varchar";
                case "from_client_desc":
                    return "varchar";
                case "current_status":
                    return "varchar";
                case "reg_result_code":
                    return "varchar";
                case "reg_fail_desc":
                    return "varchar";
                case "reg_result_text":
                    return "varchar";
                case "upload_time":
                    return "varchar";
                case "request_time":
                    return "varchar";
                case "isr_begin_time":
                    return "varchar";
                case "isr_end_time":
                    return "varchar";
                case "result_fetch_time":
                    return "varchar";
                case "break_delay":
                    return "numeric";
                case "total_delay":
                    return "numeric";
                case "remarks":
                    return "varchar";
                default:
                    throw new Exception("TbLogVoiceIsrBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
                case "reqeust_third_id":
                    return "string";
                case "voice_file_local":
                    return "string";
                case "voice_file_upload":
                    return "string";
                case "voice_file_for_isr":
                    return "string";
                case "grammar_id":
                    return "string";
                case "from_system":
                    return "string";
                case "from_client_id":
                    return "string";
                case "from_client_desc":
                    return "string";
                case "current_status":
                    return "string";
                case "reg_result_code":
                    return "string";
                case "reg_fail_desc":
                    return "string";
                case "reg_result_text":
                    return "string";
                case "upload_time":
                    return "string";
                case "request_time":
                    return "string";
                case "isr_begin_time":
                    return "string";
                case "isr_end_time":
                    return "string";
                case "result_fetch_time":
                    return "string";
                case "break_delay":
                    return "double";
                case "total_delay":
                    return "double";
                case "remarks":
                    return "string";
                default:
                    throw new Exception("TbLogVoiceIsrBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL  = "select " + toFieldName + " from vreg_log_voice_isr \n";
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
