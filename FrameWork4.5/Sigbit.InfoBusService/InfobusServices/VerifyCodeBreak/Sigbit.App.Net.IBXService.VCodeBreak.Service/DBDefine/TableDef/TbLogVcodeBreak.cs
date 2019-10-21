using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.App.Net.IBXService.VCodeBreak.Service.DBDefine
{
    /// <summary>
    /// 验证码破解日志 (vcb_log_vcode_break)的表操作用户类
    /// </summary>
    public class TbLogVcodeBreak : TbLogVcodeBreakBase
    {
        #region 用户可编辑区域

        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }


    /// <summary>
    /// 验证码破解日志 (vcb_log_vcode_break)的字段名类
    /// </summary>
    public class TbLogVcodeBreakF
    {
        public const string TableName = "vcb_log_vcode_break";
        public const string LogUid = "log_uid";
        public const string RequestThirdId = "request_third_id";
        public const string ImageFileLocal = "image_file_local";
        public const string ImageFileUpload = "image_file_upload";
        public const string ImageFileForBreak = "image_file_for_break";
        public const string VcodeId = "vcode_id";
        public const string AlgolId = "algol_id";
        public const string FromSystem = "from_system";
        public const string FromClientId = "from_client_id";
        public const string FromClientDesc = "from_client_desc";
        public const string CurrentStatus = "current_status";
        public const string BreakResult = "break_result";
        public const string FailDesc = "fail_desc";
        public const string BreakText = "break_text";
        public const string UploadTime = "upload_time";
        public const string RequestTime = "request_time";
        public const string BreakBeginTime = "break_begin_time";
        public const string BreakEndTime = "break_end_time";
        public const string ResultFetchTime = "result_fetch_time";
        public const string BreakDelay = "break_delay";
        public const string TotalDelay = "total_delay";
        public const string Remarks = "remarks";
    }


    /// <summary>
    /// 验证码破解日志 (vcb_log_vcode_break)的表操作基类
    /// </summary>
    public class TbLogVcodeBreakBase : Sigbit.Data.TableBase
    {
        #region 私有变量定义
        protected string _logUid = "";
        protected string _requestThirdId = "";
        protected string _imageFileLocal = "";
        protected string _imageFileUpload = "";
        protected string _imageFileForBreak = "";
        protected string _vcodeId = "";
        protected string _algolId = "";
        protected string _fromSystem = "";
        protected string _fromClientId = "";
        protected string _fromClientDesc = "";
        protected string _currentStatus = "";
        protected string _breakResult = "";
        protected string _failDesc = "";
        protected string _breakText = "";
        protected string _uploadTime = "";
        protected string _requestTime = "";
        protected string _breakBeginTime = "";
        protected string _breakEndTime = "";
        protected string _resultFetchTime = "";
        protected double _breakDelay;
        protected double _totalDelay;
        protected string _remarks = "";
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public TbLogVcodeBreakBase()
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
        public string RequestThirdId
        {
            get
            {
                return _requestThirdId;
            }
            set
            {
                _requestThirdId = value;
            }
        }

        /// <summary>
        /// 本地图像文件
        /// </summary>
        public string ImageFileLocal
        {
            get
            {
                return _imageFileLocal;
            }
            set
            {
                _imageFileLocal = value;
            }
        }

        /// <summary>
        /// 上传后保存的文件
        /// </summary>
        public string ImageFileUpload
        {
            get
            {
                return _imageFileUpload;
            }
            set
            {
                _imageFileUpload = value;
            }
        }

        /// <summary>
        /// 用于识别的文件
        /// </summary>
        public string ImageFileForBreak
        {
            get
            {
                return _imageFileForBreak;
            }
            set
            {
                _imageFileForBreak = value;
            }
        }

        /// <summary>
        /// 验证码的标识
        /// </summary>
        public string VcodeId
        {
            get
            {
                return _vcodeId;
            }
            set
            {
                _vcodeId = value;
            }
        }

        /// <summary>
        /// 算法的标识
        /// </summary>
        public string AlgolId
        {
            get
            {
                return _algolId;
            }
            set
            {
                _algolId = value;
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
        public string BreakResult
        {
            get
            {
                return _breakResult;
            }
            set
            {
                _breakResult = value;
            }
        }

        /// <summary>
        /// 失败原因
        /// </summary>
        public string FailDesc
        {
            get
            {
                return _failDesc;
            }
            set
            {
                _failDesc = value;
            }
        }

        /// <summary>
        /// 破解的验证码文字
        /// </summary>
        public string BreakText
        {
            get
            {
                return _breakText;
            }
            set
            {
                _breakText = value;
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
        /// 开始破解时间
        /// </summary>
        public string BreakBeginTime
        {
            get
            {
                return _breakBeginTime;
            }
            set
            {
                _breakBeginTime = value;
            }
        }

        /// <summary>
        /// 结束破解时间
        /// </summary>
        public string BreakEndTime
        {
            get
            {
                return _breakEndTime;
            }
            set
            {
                _breakEndTime = value;
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
            sb.Append("RequestThirdId: " + this._requestThirdId + "<br>");
            sb.Append("ImageFileLocal: " + this._imageFileLocal + "<br>");
            sb.Append("ImageFileUpload: " + this._imageFileUpload + "<br>");
            sb.Append("ImageFileForBreak: " + this._imageFileForBreak + "<br>");
            sb.Append("VcodeId: " + this._vcodeId + "<br>");
            sb.Append("AlgolId: " + this._algolId + "<br>");
            sb.Append("FromSystem: " + this._fromSystem + "<br>");
            sb.Append("FromClientId: " + this._fromClientId + "<br>");
            sb.Append("FromClientDesc: " + this._fromClientDesc + "<br>");
            sb.Append("CurrentStatus: " + this._currentStatus + "<br>");
            sb.Append("BreakResult: " + this._breakResult + "<br>");
            sb.Append("FailDesc: " + this._failDesc + "<br>");
            sb.Append("BreakText: " + this._breakText + "<br>");
            sb.Append("UploadTime: " + this._uploadTime + "<br>");
            sb.Append("RequestTime: " + this._requestTime + "<br>");
            sb.Append("BreakBeginTime: " + this._breakBeginTime + "<br>");
            sb.Append("BreakEndTime: " + this._breakEndTime + "<br>");
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
            _requestThirdId = "";
            _imageFileLocal = "";
            _imageFileUpload = "";
            _imageFileForBreak = "";
            _vcodeId = "";
            _algolId = "";
            _fromSystem = "";
            _fromClientId = "";
            _fromClientDesc = "";
            _currentStatus = "";
            _breakResult = "";
            _failDesc = "";
            _breakText = "";
            _uploadTime = "";
            _requestTime = "";
            _breakBeginTime = "";
            _breakEndTime = "";
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
            sSQL = "select request_third_id,    image_file_local,    image_file_upload,    \n";
            sSQL += "       image_file_for_break,vcode_id,            algol_id,             \n";
            sSQL += "       from_system,         from_client_id,      from_client_desc,     \n";
            sSQL += "       current_status,      break_result,        fail_desc,            \n";
            sSQL += "       break_text,          upload_time,         request_time,         \n";
            sSQL += "       break_begin_time,    break_end_time,      result_fetch_time,    \n";
            sSQL += "       break_delay,         total_delay,         remarks              \n";
            sSQL += "  from vcb_log_vcode_break    \n";
            sSQL += "  where log_uid = " + Quote(_logUid) + "\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbLogVcodeBreak.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _requestThirdId = DbToString(row["request_third_id"]);
            _imageFileLocal = DbToString(row["image_file_local"]);
            _imageFileUpload = DbToString(row["image_file_upload"]);
            _imageFileForBreak = DbToString(row["image_file_for_break"]);
            _vcodeId = DbToString(row["vcode_id"]);
            _algolId = DbToString(row["algol_id"]);
            _fromSystem = DbToString(row["from_system"]);
            _fromClientId = DbToString(row["from_client_id"]);
            _fromClientDesc = DbToString(row["from_client_desc"]);
            _currentStatus = DbToString(row["current_status"]);
            _breakResult = DbToString(row["break_result"]);
            _failDesc = DbToString(row["fail_desc"]);
            _breakText = DbToString(row["break_text"]);
            _uploadTime = DbToString(row["upload_time"]);
            _requestTime = DbToString(row["request_time"]);
            _breakBeginTime = DbToString(row["break_begin_time"]);
            _breakEndTime = DbToString(row["break_end_time"]);
            _resultFetchTime = DbToString(row["result_fetch_time"]);
            _breakDelay = DbToDouble(row["break_delay"]);
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

            sSQL = "insert into vcb_log_vcode_break \n";
            sSQL += "( log_uid,          request_third_id, \n";
            sSQL += "  image_file_local, image_file_upload, \n";
            sSQL += "  image_file_for_break, vcode_id,         \n";
            sSQL += "  algol_id,         from_system,      \n";
            sSQL += "  from_client_id,   from_client_desc, \n";
            sSQL += "  current_status,   break_result,     \n";
            sSQL += "  fail_desc,        break_text,       \n";
            sSQL += "  upload_time,      request_time,     \n";
            sSQL += "  break_begin_time, break_end_time,   \n";
            sSQL += "  result_fetch_time, break_delay,      \n";
            sSQL += "  total_delay,      remarks           \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_logUid) + "," + Quote(_requestThirdId) + ",\n";
            sSQL += Quote(_imageFileLocal) + "," + Quote(_imageFileUpload) + ",\n";
            sSQL += Quote(_imageFileForBreak) + "," + Quote(_vcodeId) + ",\n";
            sSQL += Quote(_algolId) + "," + Quote(_fromSystem) + ",\n";
            sSQL += Quote(_fromClientId) + "," + Quote(_fromClientDesc) + ",\n";
            sSQL += Quote(_currentStatus) + "," + Quote(_breakResult) + ",\n";
            sSQL += Quote(_failDesc) + "," + Quote(_breakText) + ",\n";
            sSQL += Quote(_uploadTime) + "," + Quote(_requestTime) + ",\n";
            sSQL += Quote(_breakBeginTime) + "," + Quote(_breakEndTime) + ",\n";
            sSQL += Quote(_resultFetchTime) + "," + _breakDelay.ToString() + ",\n";
            sSQL += _totalDelay.ToString() + "," + Quote(_remarks) + ")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 按主键删除一条数据
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL = "delete from vcb_log_vcode_break \n";
            sSQL += "  where log_uid = " + Quote(_logUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbLogVcodeBreak.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL = "update vcb_log_vcode_break set \n";
            sSQL += " log_uid = " + Quote(_logUid) + ",\n";
            sSQL += " request_third_id = " + Quote(_requestThirdId) + ",\n";
            sSQL += " image_file_local = " + Quote(_imageFileLocal) + ",\n";
            sSQL += " image_file_upload = " + Quote(_imageFileUpload) + ",\n";
            sSQL += " image_file_for_break = " + Quote(_imageFileForBreak) + ",\n";
            sSQL += " vcode_id = " + Quote(_vcodeId) + ",\n";
            sSQL += " algol_id = " + Quote(_algolId) + ",\n";
            sSQL += " from_system = " + Quote(_fromSystem) + ",\n";
            sSQL += " from_client_id = " + Quote(_fromClientId) + ",\n";
            sSQL += " from_client_desc = " + Quote(_fromClientDesc) + ",\n";
            sSQL += " current_status = " + Quote(_currentStatus) + ",\n";
            sSQL += " break_result = " + Quote(_breakResult) + ",\n";
            sSQL += " fail_desc = " + Quote(_failDesc) + ",\n";
            sSQL += " break_text = " + Quote(_breakText) + ",\n";
            sSQL += " upload_time = " + Quote(_uploadTime) + ",\n";
            sSQL += " request_time = " + Quote(_requestTime) + ",\n";
            sSQL += " break_begin_time = " + Quote(_breakBeginTime) + ",\n";
            sSQL += " break_end_time = " + Quote(_breakEndTime) + ",\n";
            sSQL += " result_fetch_time = " + Quote(_resultFetchTime) + ",\n";
            sSQL += " break_delay = " + _breakDelay.ToString() + ",\n";
            sSQL += " total_delay = " + _totalDelay.ToString() + ",\n";
            sSQL += " remarks = " + Quote(_remarks) + "\n";
            sSQL += "  where log_uid = " + Quote(_logUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbLogVcodeBreak.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from vcb_log_vcode_break \n";
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
                throw new Exception("TbLogVcodeBreak.FetchBy(...) Error - cannot locate record via PrimaryKey.");
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
        static public TbLogVcodeBreak CreateBy(string logUid)
        {
            TbLogVcodeBreak tbl;
            bool hasData;

            tbl = new TbLogVcodeBreak();
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
            TbLogVcodeBreak tbl;
            tbl = new TbLogVcodeBreak();

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
            _imageFileLocal = DbToString(row["image_file_local"]);
            _imageFileUpload = DbToString(row["image_file_upload"]);
            _imageFileForBreak = DbToString(row["image_file_for_break"]);
            _vcodeId = DbToString(row["vcode_id"]);
            _algolId = DbToString(row["algol_id"]);
            _fromSystem = DbToString(row["from_system"]);
            _fromClientId = DbToString(row["from_client_id"]);
            _fromClientDesc = DbToString(row["from_client_desc"]);
            _currentStatus = DbToString(row["current_status"]);
            _breakResult = DbToString(row["break_result"]);
            _failDesc = DbToString(row["fail_desc"]);
            _breakText = DbToString(row["break_text"]);
            _uploadTime = DbToString(row["upload_time"]);
            _requestTime = DbToString(row["request_time"]);
            _breakBeginTime = DbToString(row["break_begin_time"]);
            _breakEndTime = DbToString(row["break_end_time"]);
            _resultFetchTime = DbToString(row["result_fetch_time"]);
            _breakDelay = DbToDouble(row["break_delay"]);
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

            sLine = "image_file_local\x9" + _imageFileLocal;
            writer.WriteLine(sLine);

            sLine = "image_file_upload\x9" + _imageFileUpload;
            writer.WriteLine(sLine);

            sLine = "image_file_for_break\x9" + _imageFileForBreak;
            writer.WriteLine(sLine);

            sLine = "vcode_id\x9" + _vcodeId;
            writer.WriteLine(sLine);

            sLine = "algol_id\x9" + _algolId;
            writer.WriteLine(sLine);

            sLine = "from_system\x9" + _fromSystem;
            writer.WriteLine(sLine);

            sLine = "from_client_id\x9" + _fromClientId;
            writer.WriteLine(sLine);

            sLine = "from_client_desc\x9" + _fromClientDesc;
            writer.WriteLine(sLine);

            sLine = "current_status\x9" + _currentStatus;
            writer.WriteLine(sLine);

            sLine = "break_result\x9" + _breakResult;
            writer.WriteLine(sLine);

            sLine = "fail_desc\x9" + _failDesc;
            writer.WriteLine(sLine);

            sLine = "break_text\x9" + _breakText;
            writer.WriteLine(sLine);

            sLine = "upload_time\x9" + _uploadTime;
            writer.WriteLine(sLine);

            sLine = "request_time\x9" + _requestTime;
            writer.WriteLine(sLine);

            sLine = "break_begin_time\x9" + _breakBeginTime;
            writer.WriteLine(sLine);

            sLine = "break_end_time\x9" + _breakEndTime;
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
            sLine = "log_uid\trequest_third_id\timage_file_local\t";
            sLine += "image_file_upload\timage_file_for_break\tvcode_id\t";
            sLine += "algol_id\tfrom_system\tfrom_client_id\t";
            sLine += "from_client_desc\tcurrent_status\tbreak_result\t";
            sLine += "fail_desc\tbreak_text\tupload_time\t";
            sLine += "request_time\tbreak_begin_time\tbreak_end_time\t";
            sLine += "result_fetch_time\tbreak_delay\ttotal_delay\t";
            sLine += "remarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL = "select log_uid,         request_third_id,image_file_local,\n";
            sSQL += "       image_file_upload,image_file_for_break,vcode_id,        \n";
            sSQL += "       algol_id,        from_system,     from_client_id,  \n";
            sSQL += "       from_client_desc,current_status,  break_result,    \n";
            sSQL += "       fail_desc,       break_text,      upload_time,     \n";
            sSQL += "       request_time,    break_begin_time,break_end_time,  \n";
            sSQL += "       result_fetch_time,break_delay,     total_delay,     \n";
            sSQL += "       remarks         \n";
            sSQL += "  from vcb_log_vcode_break";

            //======== 4. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. 处理每一行 ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 处理每一列 ========
                for (nCol = 0; nCol < 22; nCol++)
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
                case "image_file_local":
                    return "varchar";
                case "image_file_upload":
                    return "varchar";
                case "image_file_for_break":
                    return "varchar";
                case "vcode_id":
                    return "varchar";
                case "algol_id":
                    return "varchar";
                case "from_system":
                    return "varchar";
                case "from_client_id":
                    return "varchar";
                case "from_client_desc":
                    return "varchar";
                case "current_status":
                    return "varchar";
                case "break_result":
                    return "varchar";
                case "fail_desc":
                    return "varchar";
                case "break_text":
                    return "varchar";
                case "upload_time":
                    return "varchar";
                case "request_time":
                    return "varchar";
                case "break_begin_time":
                    return "varchar";
                case "break_end_time":
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
                    throw new Exception("TbLogVcodeBreakBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
                case "image_file_local":
                    return "string";
                case "image_file_upload":
                    return "string";
                case "image_file_for_break":
                    return "string";
                case "vcode_id":
                    return "string";
                case "algol_id":
                    return "string";
                case "from_system":
                    return "string";
                case "from_client_id":
                    return "string";
                case "from_client_desc":
                    return "string";
                case "current_status":
                    return "string";
                case "break_result":
                    return "string";
                case "fail_desc":
                    return "string";
                case "break_text":
                    return "string";
                case "upload_time":
                    return "string";
                case "request_time":
                    return "string";
                case "break_begin_time":
                    return "string";
                case "break_end_time":
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
                    throw new Exception("TbLogVcodeBreakBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL = "select " + toFieldName + " from vcb_log_vcode_break \n";
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
