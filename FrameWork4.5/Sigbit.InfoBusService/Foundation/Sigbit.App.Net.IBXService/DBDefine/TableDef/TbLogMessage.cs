using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.App.Net.IBXService.DBDefine
{
    /// <summary>
    /// 通信消息包日志 (ibx_log_message)的表操作用户类
    /// </summary>
    public class TbLogMessage : TbLogMessageBase
    {
        #region 用户可编辑区域

        public static int ClearLogBeforeTime(string sClearEndTime)
        {
            string sSQL = "delete from " + TbLogMessageF.TableName
                    + " where request_time < " + StringUtil.QuotedToDBStr(sClearEndTime);
            int nRecCountAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);

            return nRecCountAffected;
        }

        #endregion
    }


    /// <summary>
    /// 通信消息包日志 (ibx_log_message)的字段名类
    /// </summary>
    public class TbLogMessageF
    {
        public const string TableName = "ibx_log_message";
        public const string LogUid               = "log_uid";
        public const string TransCodeEng         = "trans_code_eng";
        public const string TransCodeChs         = "trans_code_chs";
        public const string FromApplication      = "from_application";
        public const string ToApplication        = "to_application";
        public const string RequestTime          = "request_time";
        public const string RequestPacket        = "request_packet";
        public const string RequestDesc          = "request_desc";
        public const string FromSystem           = "from_system";
        public const string FromClientId         = "from_client_id";
        public const string FromClientVersion    = "from_client_version";
        public const string FromClientDesc       = "from_client_desc";
        public const string ResponseTime         = "response_time";
        public const string ResponsePacket       = "response_packet";
        public const string ResponseDesc         = "response_desc";
        public const string ErrorCode            = "error_code";
        public const string ErrorString          = "error_string";
        public const string CallDuration         = "call_duration";
        public const string Remarks              = "remarks";
    }


    /// <summary>
    /// 通信消息包日志 (ibx_log_message)的表操作基类
    /// </summary>
    public class TbLogMessageBase : Sigbit.Data.TableBase
    {
        #region 私有变量定义
        protected string     _logUid                  = "";
        protected string     _transCodeEng            = "";
        protected string     _transCodeChs            = "";
        protected string     _fromApplication         = "";
        protected string     _toApplication           = "";
        protected string     _requestTime             = "";
        protected string     _requestPacket           = "";
        protected string     _requestDesc             = "";
        protected string     _fromSystem              = "";
        protected string     _fromClientId            = "";
        protected string     _fromClientVersion       = "";
        protected string     _fromClientDesc          = "";
        protected string     _responseTime            = "";
        protected string     _responsePacket          = "";
        protected string     _responseDesc            = "";
        protected int        _errorCode              ;
        protected string     _errorString             = "";
        protected int        _callDuration           ;
        protected string     _remarks                 = "";
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public TbLogMessageBase()
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
        /// 接口命令字英文
        /// </summary>
        public string TransCodeEng
        {
            get
            {
                return _transCodeEng;
            }
            set
            {
                _transCodeEng = value;
            }
        }

        /// <summary>
        /// 接口命令字中文
        /// </summary>
        public string TransCodeChs
        {
            get
            {
                return _transCodeChs;
            }
            set
            {
                _transCodeChs = value;
            }
        }

        /// <summary>
        /// 主叫应用
        /// </summary>
        public string FromApplication
        {
            get
            {
                return _fromApplication;
            }
            set
            {
                _fromApplication = value;
            }
        }

        /// <summary>
        /// 目标应用
        /// </summary>
        public string ToApplication
        {
            get
            {
                return _toApplication;
            }
            set
            {
                _toApplication = value;
            }
        }

        /// <summary>
        /// 发送时间（带毫秒）
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
        /// 请求的完整包数据
        /// </summary>
        public string RequestPacket
        {
            get
            {
                return _requestPacket;
            }
            set
            {
                _requestPacket = value;
            }
        }

        /// <summary>
        /// 请求数据描述
        /// </summary>
        public string RequestDesc
        {
            get
            {
                return _requestDesc;
            }
            set
            {
                _requestDesc = value;
            }
        }

        /// <summary>
        /// 来自系统
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
        /// 客户端的版本号
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
        /// 客户端的标识
        /// </summary>
        public string FromClientVersion
        {
            get
            {
                return _fromClientVersion;
            }
            set
            {
                _fromClientVersion = value;
            }
        }

        /// <summary>
        /// 客户端描述
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
        /// 响应时间（带毫秒）
        /// </summary>
        public string ResponseTime
        {
            get
            {
                return _responseTime;
            }
            set
            {
                _responseTime = value;
            }
        }

        /// <summary>
        /// 响应的完整包数据
        /// </summary>
        public string ResponsePacket
        {
            get
            {
                return _responsePacket;
            }
            set
            {
                _responsePacket = value;
            }
        }

        /// <summary>
        /// 响应数据描述
        /// </summary>
        public string ResponseDesc
        {
            get
            {
                return _responseDesc;
            }
            set
            {
                _responseDesc = value;
            }
        }

        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
            }
        }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrorString
        {
            get
            {
                return _errorString;
            }
            set
            {
                _errorString = value;
            }
        }

        /// <summary>
        /// 响应时长（毫秒）
        /// </summary>
        public int CallDuration
        {
            get
            {
                return _callDuration;
            }
            set
            {
                _callDuration = value;
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
            sb.Append("TransCodeEng: " + this._transCodeEng + "<br>");
            sb.Append("TransCodeChs: " + this._transCodeChs + "<br>");
            sb.Append("FromApplication: " + this._fromApplication + "<br>");
            sb.Append("ToApplication: " + this._toApplication + "<br>");
            sb.Append("RequestTime: " + this._requestTime + "<br>");
            sb.Append("RequestPacket: " + this._requestPacket + "<br>");
            sb.Append("RequestDesc: " + this._requestDesc + "<br>");
            sb.Append("FromSystem: " + this._fromSystem + "<br>");
            sb.Append("FromClientId: " + this._fromClientId + "<br>");
            sb.Append("FromClientVersion: " + this._fromClientVersion + "<br>");
            sb.Append("FromClientDesc: " + this._fromClientDesc + "<br>");
            sb.Append("ResponseTime: " + this._responseTime + "<br>");
            sb.Append("ResponsePacket: " + this._responsePacket + "<br>");
            sb.Append("ResponseDesc: " + this._responseDesc + "<br>");
            sb.Append("ErrorCode: " + this._errorCode + "<br>");
            sb.Append("ErrorString: " + this._errorString + "<br>");
            sb.Append("CallDuration: " + this._callDuration + "<br>");
            sb.Append("Remarks: " + this._remarks + "<br>");
            return sb.ToString();
        }

        /// <summary>
        /// 清零本记录的数据
        /// </summary>
        public override void ResetData()
        {
            _logUid = "";
            _transCodeEng = "";
            _transCodeChs = "";
            _fromApplication = "";
            _toApplication = "";
            _requestTime = "";
            _requestPacket = "";
            _requestDesc = "";
            _fromSystem = "";
            _fromClientId = "";
            _fromClientVersion = "";
            _fromClientDesc = "";
            _responseTime = "";
            _responsePacket = "";
            _responseDesc = "";
            _errorCode = 0;
            _errorString = "";
            _callDuration = 0;
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
            sSQL  = "select trans_code_eng,      trans_code_chs,      from_application,     \n";
            sSQL += "       to_application,      request_time,        request_packet,       \n";
            sSQL += "       request_desc,        from_system,         from_client_id,       \n";
            sSQL += "       from_client_version, from_client_desc,    response_time,        \n";
            sSQL += "       response_packet,     response_desc,       error_code,           \n";
            sSQL += "       error_string,        call_duration,       remarks              \n";
            sSQL += "  from ibx_log_message    \n";
            sSQL += "  where log_uid = " + Quote(_logUid) + "\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbLogMessage.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _transCodeEng   = DbToString(row["trans_code_eng"]);
            _transCodeChs   = DbToString(row["trans_code_chs"]);
            _fromApplication = DbToString(row["from_application"]);
            _toApplication  = DbToString(row["to_application"]);
            _requestTime    = DbToString(row["request_time"]);
            _requestPacket  = DbToString(row["request_packet"]);
            _requestDesc    = DbToString(row["request_desc"]);
            _fromSystem     = DbToString(row["from_system"]);
            _fromClientId   = DbToString(row["from_client_id"]);
            _fromClientVersion = DbToString(row["from_client_version"]);
            _fromClientDesc = DbToString(row["from_client_desc"]);
            _responseTime   = DbToString(row["response_time"]);
            _responsePacket = DbToString(row["response_packet"]);
            _responseDesc   = DbToString(row["response_desc"]);
            _errorCode      = DbToInt(row["error_code"]);
            _errorString    = DbToString(row["error_string"]);
            _callDuration   = DbToInt(row["call_duration"]);
            _remarks        = DbToString(row["remarks"]);
            return true;
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        public override void Insert()
        {
            string sSQL;

            sSQL  = "insert into ibx_log_message \n";
            sSQL += "( log_uid,          trans_code_eng,   \n";
            sSQL += "  trans_code_chs,   from_application, \n";
            sSQL += "  to_application,   request_time,     \n";
            sSQL += "  request_packet,   request_desc,     \n";
            sSQL += "  from_system,      from_client_id,   \n";
            sSQL += "  from_client_version, from_client_desc, \n";
            sSQL += "  response_time,    response_packet,  \n";
            sSQL += "  response_desc,    error_code,       \n";
            sSQL += "  error_string,     call_duration,    \n";
            sSQL += "  remarks           \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_logUid)          +","+ Quote(_transCodeEng)    +",\n";
            sSQL += Quote(_transCodeChs)    +","+ Quote(_fromApplication) +",\n";
            sSQL += Quote(_toApplication)   +","+ Quote(_requestTime)     +",\n";
            sSQL += Quote(_requestPacket)   +","+ Quote(_requestDesc)     +",\n";
            sSQL += Quote(_fromSystem)      +","+ Quote(_fromClientId)    +",\n";
            sSQL += Quote(_fromClientVersion)+","+ Quote(_fromClientDesc)  +",\n";
            sSQL += Quote(_responseTime)    +","+ Quote(_responsePacket)  +",\n";
            sSQL += Quote(_responseDesc)    +","+ _errorCode.ToString()   +",\n";
            sSQL += Quote(_errorString)     +","+ _callDuration.ToString()+",\n";
            sSQL += Quote(_remarks)         +")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 按主键删除一条数据
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL  = "delete from ibx_log_message \n";
            sSQL += "  where log_uid = " + Quote(_logUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbLogMessage.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL  = "update ibx_log_message set \n";
            sSQL += " log_uid = " + Quote(_logUid) + ",\n";
            sSQL += " trans_code_eng = " + Quote(_transCodeEng) + ",\n";
            sSQL += " trans_code_chs = " + Quote(_transCodeChs) + ",\n";
            sSQL += " from_application = " + Quote(_fromApplication) + ",\n";
            sSQL += " to_application = " + Quote(_toApplication) + ",\n";
            sSQL += " request_time = " + Quote(_requestTime) + ",\n";
            sSQL += " request_packet = " + Quote(_requestPacket) + ",\n";
            sSQL += " request_desc = " + Quote(_requestDesc) + ",\n";
            sSQL += " from_system = " + Quote(_fromSystem) + ",\n";
            sSQL += " from_client_id = " + Quote(_fromClientId) + ",\n";
            sSQL += " from_client_version = " + Quote(_fromClientVersion) + ",\n";
            sSQL += " from_client_desc = " + Quote(_fromClientDesc) + ",\n";
            sSQL += " response_time = " + Quote(_responseTime) + ",\n";
            sSQL += " response_packet = " + Quote(_responsePacket) + ",\n";
            sSQL += " response_desc = " + Quote(_responseDesc) + ",\n";
            sSQL += " error_code = " + _errorCode.ToString() + ",\n";
            sSQL += " error_string = " + Quote(_errorString) + ",\n";
            sSQL += " call_duration = " + _callDuration.ToString() + ",\n";
            sSQL += " remarks = " + Quote(_remarks) + "\n";
            sSQL += "  where log_uid = " + Quote(_logUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbLogMessage.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from ibx_log_message \n";
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
                throw new Exception("TbLogMessage.FetchBy(...) Error - cannot locate record via PrimaryKey.");
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
        static public TbLogMessage CreateBy(string logUid)
        {
            TbLogMessage tbl;
            bool hasData;

            tbl = new TbLogMessage();
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
            TbLogMessage tbl;
            tbl = new TbLogMessage();

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
            _transCodeEng        = DbToString(row["trans_code_eng"]);
            _transCodeChs        = DbToString(row["trans_code_chs"]);
            _fromApplication     = DbToString(row["from_application"]);
            _toApplication       = DbToString(row["to_application"]);
            _requestTime         = DbToString(row["request_time"]);
            _requestPacket       = DbToString(row["request_packet"]);
            _requestDesc         = DbToString(row["request_desc"]);
            _fromSystem          = DbToString(row["from_system"]);
            _fromClientId        = DbToString(row["from_client_id"]);
            _fromClientVersion   = DbToString(row["from_client_version"]);
            _fromClientDesc      = DbToString(row["from_client_desc"]);
            _responseTime        = DbToString(row["response_time"]);
            _responsePacket      = DbToString(row["response_packet"]);
            _responseDesc        = DbToString(row["response_desc"]);
            _errorCode           = DbToInt(row["error_code"]);
            _errorString         = DbToString(row["error_string"]);
            _callDuration        = DbToInt(row["call_duration"]);
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

            sLine = "trans_code_eng\x9" + _transCodeEng;
            writer.WriteLine(sLine);

            sLine = "trans_code_chs\x9" + _transCodeChs;
            writer.WriteLine(sLine);

            sLine = "from_application\x9" + _fromApplication;
            writer.WriteLine(sLine);

            sLine = "to_application\x9" + _toApplication;
            writer.WriteLine(sLine);

            sLine = "request_time\x9" + _requestTime;
            writer.WriteLine(sLine);

            sLine = "request_packet\x9" + _requestPacket;
            writer.WriteLine(sLine);

            sLine = "request_desc\x9" + _requestDesc;
            writer.WriteLine(sLine);

            sLine = "from_system\x9" + _fromSystem;
            writer.WriteLine(sLine);

            sLine = "from_client_id\x9" + _fromClientId;
            writer.WriteLine(sLine);

            sLine = "from_client_version\x9" + _fromClientVersion;
            writer.WriteLine(sLine);

            sLine = "from_client_desc\x9" + _fromClientDesc;
            writer.WriteLine(sLine);

            sLine = "response_time\x9" + _responseTime;
            writer.WriteLine(sLine);

            sLine = "response_packet\x9" + _responsePacket;
            writer.WriteLine(sLine);

            sLine = "response_desc\x9" + _responseDesc;
            writer.WriteLine(sLine);

            sLine = "error_code\x9" + _errorCode.ToString();
            writer.WriteLine(sLine);

            sLine = "error_string\x9" + _errorString;
            writer.WriteLine(sLine);

            sLine = "call_duration\x9" + _callDuration.ToString();
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
            sLine  = "log_uid\ttrans_code_eng\ttrans_code_chs\t";
            sLine += "from_application\tto_application\trequest_time\t";
            sLine += "request_packet\trequest_desc\tfrom_system\t";
            sLine += "from_client_id\tfrom_client_version\tfrom_client_desc\t";
            sLine += "response_time\tresponse_packet\tresponse_desc\t";
            sLine += "error_code\terror_string\tcall_duration\t";
            sLine += "remarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL  = "select log_uid,         trans_code_eng,  trans_code_chs,  \n";
            sSQL += "       from_application,to_application,  request_time,    \n";
            sSQL += "       request_packet,  request_desc,    from_system,     \n";
            sSQL += "       from_client_id,  from_client_version,from_client_desc,\n";
            sSQL += "       response_time,   response_packet, response_desc,   \n";
            sSQL += "       error_code,      error_string,    call_duration,   \n";
            sSQL += "       remarks         \n";
            sSQL += "  from ibx_log_message";

            //======== 4. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. 处理每一行 ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 处理每一列 ========
                for (nCol = 0; nCol < 19; nCol++)
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
                case "trans_code_eng":
                    return "varchar";
                case "trans_code_chs":
                    return "varchar";
                case "from_application":
                    return "varchar";
                case "to_application":
                    return "varchar";
                case "request_time":
                    return "varchar";
                case "request_packet":
                    return "text";
                case "request_desc":
                    return "text";
                case "from_system":
                    return "varchar";
                case "from_client_id":
                    return "varchar";
                case "from_client_version":
                    return "varchar";
                case "from_client_desc":
                    return "varchar";
                case "response_time":
                    return "varchar";
                case "response_packet":
                    return "text";
                case "response_desc":
                    return "text";
                case "error_code":
                    return "int";
                case "error_string":
                    return "varchar";
                case "call_duration":
                    return "int";
                case "remarks":
                    return "varchar";
                default:
                    throw new Exception("TbLogMessageBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
                case "trans_code_eng":
                    return "string";
                case "trans_code_chs":
                    return "string";
                case "from_application":
                    return "string";
                case "to_application":
                    return "string";
                case "request_time":
                    return "string";
                case "request_packet":
                    return "string";
                case "request_desc":
                    return "string";
                case "from_system":
                    return "string";
                case "from_client_id":
                    return "string";
                case "from_client_version":
                    return "string";
                case "from_client_desc":
                    return "string";
                case "response_time":
                    return "string";
                case "response_packet":
                    return "string";
                case "response_desc":
                    return "string";
                case "error_code":
                    return "int";
                case "error_string":
                    return "string";
                case "call_duration":
                    return "int";
                case "remarks":
                    return "string";
                default:
                    throw new Exception("TbLogMessageBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL  = "select " + toFieldName + " from ibx_log_message \n";
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
