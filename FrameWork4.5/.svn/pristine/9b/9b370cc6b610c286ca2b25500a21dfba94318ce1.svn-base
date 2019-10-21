using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework.Patch.DBDefine
{
    #region 枚举
    /// <summary>
    /// 枚举字段：更新状态
    /// </summary>
    public enum TbSysPatchWorkEUpdateStatus
    {
        None,
        [SbtEnumDescString("新补丁")]
        New,
        [SbtEnumDescString("正在补丁中")]
        Patching,
        [SbtEnumDescString("已更新")]
        Patched
    }
    #endregion

    /// <summary>
    /// 系统补丁(sbt_sys_patch_work)的表操作类
    /// </summary>
    public class TbSysPatchWork : TbSysPatchWorkBase
    {
        #region 枚举属性
        /// <summary>
        /// 更新状态
        /// </summary>
        public TbSysPatchWorkEUpdateStatus UpdateStatusE
        {
            get
            {
                TbSysPatchWorkEUpdateStatus enumRet = 
                        (TbSysPatchWorkEUpdateStatus)ConvertUtil.StringToEnum
                        (this.UpdateStatus, TbSysPatchWorkEUpdateStatus.None);
                return enumRet;
            }
            set
            {
                this.UpdateStatus = ConvertUtil.EnumToString(value);
            }
        }
        #endregion

        #region 用户可编辑区域

        public bool FetchByPatchKeyID(string sPatchKeyID)
        {
            string sSQL = "select * from sbt_sys_patch_work where patch_key_id = " + StringUtil.QuotedToDBStr(sPatchKeyID);
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

            if (ds.Tables[0].Rows.Count == 0)
                return false;

            this.AssignByDataRow(ds, 0);
            return true;
        }

        #endregion
    }


    /// <summary>
    /// 系统补丁(sbt_sys_patch_work)的字段名类
    /// </summary>
    public class TbSysPatchWorkF
    {
        public const string TableName = "sbt_sys_patch_work";
        public const string PatchUid             = "patch_uid";
        public const string PatchKeyId           = "patch_key_id";
        public const string PatchName            = "patch_name";
        public const string PatchDescription     = "patch_description";
        public const string DistributeTime       = "distribute_time";
        public const string InstallTime          = "install_time";
        public const string UpdateBeginTime      = "update_begin_time";
        public const string UpdateEndTime        = "update_end_time";
        public const string UpdateDuration       = "update_duration";
        public const string UpdateStatus         = "update_status";
        public const string UpdateResultSkip     = "update_result_skip";
        public const string UpdateResultSucc     = "update_result_succ";
        public const string UpdateResultDetail   = "update_result_detail";
        public const string VesionBeforeUpdate   = "vesion_before_update";
        public const string VersionAfterUpdate   = "version_after_update";
        public const string EngineCallTimes      = "engine_call_times";
        public const string EngineLastResult     = "engine_last_result";
        public const string EngineLastDetail     = "engine_last_detail";
        public const string EngineLastTime       = "engine_last_time";
        public const string CreateTime           = "create_time";
        public const string Creator              = "creator";
        public const string ModifyTime           = "modify_time";
        public const string Remarks              = "remarks";
    }


    /// <summary>
    /// 系统补丁(sbt_sys_patch_work)的表操作基类
    /// </summary>
    public class TbSysPatchWorkBase : Sigbit.Data.TableBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TbSysPatchWorkBase()
        {
            ResetData();
        }

        #region 属性定义
        protected string _patchUid = "";
        /// <summary>
        /// 补丁标识，主键
        /// </summary>
        public string PatchUid
        {
            get { return _patchUid; }
            set { _patchUid = value; }
        }

        protected string _patchKeyId = "";
        /// <summary>
        /// 补丁的ID
        /// </summary>
        public string PatchKeyId
        {
            get { return _patchKeyId; }
            set { _patchKeyId = value; }
        }

        protected string _patchName = "";
        /// <summary>
        /// 补丁的名称
        /// </summary>
        public string PatchName
        {
            get { return _patchName; }
            set { _patchName = value; }
        }

        protected string _patchDescription = "";
        /// <summary>
        /// 补丁的说明
        /// </summary>
        public string PatchDescription
        {
            get { return _patchDescription; }
            set { _patchDescription = value; }
        }

        protected string _distributeTime = "";
        /// <summary>
        /// 补丁发布时间
        /// </summary>
        public string DistributeTime
        {
            get { return _distributeTime; }
            set { _distributeTime = value; }
        }

        protected string _installTime = "";
        /// <summary>
        /// 补丁安装时间
        /// </summary>
        public string InstallTime
        {
            get { return _installTime; }
            set { _installTime = value; }
        }

        protected string _updateBeginTime = "";
        /// <summary>
        /// 开始更新时间
        /// </summary>
        public string UpdateBeginTime
        {
            get { return _updateBeginTime; }
            set { _updateBeginTime = value; }
        }

        protected string _updateEndTime = "";
        /// <summary>
        /// 结束更新时间
        /// </summary>
        public string UpdateEndTime
        {
            get { return _updateEndTime; }
            set { _updateEndTime = value; }
        }

        protected double _updateDuration;
        /// <summary>
        /// 更新历时
        /// </summary>
        public double UpdateDuration
        {
            get { return _updateDuration; }
            set { _updateDuration = value; }
        }

        protected string _updateStatus = "";
        /// <summary>
        /// 更新状态
        /// </summary>
        public string UpdateStatus
        {
            get { return _updateStatus; }
            set { _updateStatus = value; }
        }

        protected string _updateResultSkip = "";
        /// <summary>
        /// 是否跳过
        /// </summary>
        public string UpdateResultSkip
        {
            get { return _updateResultSkip; }
            set { _updateResultSkip = value; }
        }

        protected string _updateResultSucc = "";
        /// <summary>
        /// 是否成功
        /// </summary>
        public string UpdateResultSucc
        {
            get { return _updateResultSucc; }
            set { _updateResultSucc = value; }
        }

        protected string _updateResultDetail = "";
        /// <summary>
        /// 更新详情
        /// </summary>
        public string UpdateResultDetail
        {
            get { return _updateResultDetail; }
            set { _updateResultDetail = value; }
        }

        protected string _vesionBeforeUpdate = "";
        /// <summary>
        /// 更新前的版本号
        /// </summary>
        public string VesionBeforeUpdate
        {
            get { return _vesionBeforeUpdate; }
            set { _vesionBeforeUpdate = value; }
        }

        protected string _versionAfterUpdate = "";
        /// <summary>
        /// 更新后的版本号
        /// </summary>
        public string VersionAfterUpdate
        {
            get { return _versionAfterUpdate; }
            set { _versionAfterUpdate = value; }
        }

        protected int _engineCallTimes;
        /// <summary>
        /// 引擎的调用次数
        /// </summary>
        public int EngineCallTimes
        {
            get { return _engineCallTimes; }
            set { _engineCallTimes = value; }
        }

        protected string _engineLastResult = "";
        /// <summary>
        /// 引擎上一次的结果
        /// </summary>
        public string EngineLastResult
        {
            get { return _engineLastResult; }
            set { _engineLastResult = value; }
        }

        protected string _engineLastDetail = "";
        /// <summary>
        /// 引擎上一次的详情
        /// </summary>
        public string EngineLastDetail
        {
            get { return _engineLastDetail; }
            set { _engineLastDetail = value; }
        }

        protected string _engineLastTime = "";
        /// <summary>
        /// 引擎上一次的时间
        /// </summary>
        public string EngineLastTime
        {
            get { return _engineLastTime; }
            set { _engineLastTime = value; }
        }

        protected string _createTime = "";
        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        protected string _creator = "";
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }

        protected string _modifyTime = "";
        /// <summary>
        /// 更新日期
        /// </summary>
        public string ModifyTime
        {
            get { return _modifyTime; }
            set { _modifyTime = value; }
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
            sb.Append("PatchUid: " + this._patchUid + "<br>");
            sb.Append("PatchKeyId: " + this._patchKeyId + "<br>");
            sb.Append("PatchName: " + this._patchName + "<br>");
            sb.Append("PatchDescription: " + this._patchDescription + "<br>");
            sb.Append("DistributeTime: " + this._distributeTime + "<br>");
            sb.Append("InstallTime: " + this._installTime + "<br>");
            sb.Append("UpdateBeginTime: " + this._updateBeginTime + "<br>");
            sb.Append("UpdateEndTime: " + this._updateEndTime + "<br>");
            sb.Append("UpdateDuration: " + this._updateDuration + "<br>");
            sb.Append("UpdateStatus: " + this._updateStatus + "<br>");
            sb.Append("UpdateResultSkip: " + this._updateResultSkip + "<br>");
            sb.Append("UpdateResultSucc: " + this._updateResultSucc + "<br>");
            sb.Append("UpdateResultDetail: " + this._updateResultDetail + "<br>");
            sb.Append("VesionBeforeUpdate: " + this._vesionBeforeUpdate + "<br>");
            sb.Append("VersionAfterUpdate: " + this._versionAfterUpdate + "<br>");
            sb.Append("EngineCallTimes: " + this._engineCallTimes + "<br>");
            sb.Append("EngineLastResult: " + this._engineLastResult + "<br>");
            sb.Append("EngineLastDetail: " + this._engineLastDetail + "<br>");
            sb.Append("EngineLastTime: " + this._engineLastTime + "<br>");
            sb.Append("CreateTime: " + this._createTime + "<br>");
            sb.Append("Creator: " + this._creator + "<br>");
            sb.Append("ModifyTime: " + this._modifyTime + "<br>");
            sb.Append("Remarks: " + this._remarks + "<br>");
            return sb.ToString();
        }

        /// <summary>
        /// 清零本记录的数据
        /// </summary>
        public override void ResetData()
        {
            _patchUid = "";
            _patchKeyId = "";
            _patchName = "";
            _patchDescription = "";
            _distributeTime = "";
            _installTime = "";
            _updateBeginTime = "";
            _updateEndTime = "";
            _updateDuration = 0;
            _updateStatus = "";
            _updateResultSkip = "";
            _updateResultSucc = "";
            _updateResultDetail = "";
            _vesionBeforeUpdate = "";
            _versionAfterUpdate = "";
            _engineCallTimes = 0;
            _engineLastResult = "";
            _engineLastDetail = "";
            _engineLastTime = "";
            _createTime = "";
            _creator = "";
            _modifyTime = "";
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
            sSQL  = "select patch_key_id,        patch_name,          patch_description,    \r\n";
            sSQL += "       distribute_time,     install_time,        update_begin_time,    \r\n";
            sSQL += "       update_end_time,     update_duration,     update_status,        \r\n";
            sSQL += "       update_result_skip,  update_result_succ,  update_result_detail, \r\n";
            sSQL += "       vesion_before_update,version_after_update,engine_call_times,    \r\n";
            sSQL += "       engine_last_result,  engine_last_detail,  engine_last_time,     \r\n";
            sSQL += "       create_time,         creator,             modify_time,          \r\n";
            sSQL += "       remarks              \r\n";
            sSQL += "  from sbt_sys_patch_work    \r\n";
            sSQL += "  where patch_uid = " + Quote(_patchUid) + "\r\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbSysPatchWork.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _patchKeyId     = DbToString(row["patch_key_id"]);
            _patchName      = DbToString(row["patch_name"]);
            _patchDescription = DbToString(row["patch_description"]);
            _distributeTime = DbToString(row["distribute_time"]);
            _installTime    = DbToString(row["install_time"]);
            _updateBeginTime = DbToString(row["update_begin_time"]);
            _updateEndTime  = DbToString(row["update_end_time"]);
            _updateDuration = DbToDouble(row["update_duration"]);
            _updateStatus   = DbToString(row["update_status"]);
            _updateResultSkip = DbToString(row["update_result_skip"]);
            _updateResultSucc = DbToString(row["update_result_succ"]);
            _updateResultDetail = DbToString(row["update_result_detail"]);
            _vesionBeforeUpdate = DbToString(row["vesion_before_update"]);
            _versionAfterUpdate = DbToString(row["version_after_update"]);
            _engineCallTimes = DbToInt(row["engine_call_times"]);
            _engineLastResult = DbToString(row["engine_last_result"]);
            _engineLastDetail = DbToString(row["engine_last_detail"]);
            _engineLastTime = DbToString(row["engine_last_time"]);
            _createTime     = DbToString(row["create_time"]);
            _creator        = DbToString(row["creator"]);
            _modifyTime     = DbToString(row["modify_time"]);
            _remarks        = DbToString(row["remarks"]);
            return true;
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        public override void Insert()
        {
            string sSQL;

            sSQL  = "insert into sbt_sys_patch_work \r\n";
            sSQL += "( patch_uid,        patch_key_id,     \r\n";
            sSQL += "  patch_name,       patch_description, \r\n";
            sSQL += "  distribute_time,  install_time,     \r\n";
            sSQL += "  update_begin_time, update_end_time,  \r\n";
            sSQL += "  update_duration,  update_status,    \r\n";
            sSQL += "  update_result_skip, update_result_succ, \r\n";
            sSQL += "  update_result_detail, vesion_before_update, \r\n";
            sSQL += "  version_after_update, engine_call_times, \r\n";
            sSQL += "  engine_last_result, engine_last_detail, \r\n";
            sSQL += "  engine_last_time, create_time,      \r\n";
            sSQL += "  creator,          modify_time,      \r\n";
            sSQL += "  remarks           \r\n";
            sSQL += ") values (          \r\n";
            sSQL += Quote(_patchUid)        +","+ Quote(_patchKeyId)      +",\r\n";
            sSQL += Quote(_patchName)       +","+ Quote(_patchDescription)+",\r\n";
            sSQL += Quote(_distributeTime)  +","+ Quote(_installTime)     +",\r\n";
            sSQL += Quote(_updateBeginTime) +","+ Quote(_updateEndTime)   +",\r\n";
            sSQL += _updateDuration.ToString()+","+ Quote(_updateStatus)    +",\r\n";
            sSQL += Quote(_updateResultSkip)+","+ Quote(_updateResultSucc)+",\r\n";
            sSQL += Quote(_updateResultDetail)+","+ Quote(_vesionBeforeUpdate)+",\r\n";
            sSQL += Quote(_versionAfterUpdate)+","+ _engineCallTimes.ToString()+",\r\n";
            sSQL += Quote(_engineLastResult)+","+ Quote(_engineLastDetail)+",\r\n";
            sSQL += Quote(_engineLastTime)  +","+ Quote(_createTime)      +",\r\n";
            sSQL += Quote(_creator)         +","+ Quote(_modifyTime)      +",\r\n";
            sSQL += Quote(_remarks)         +")\r\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 按主键删除一条数据
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL  = "delete from sbt_sys_patch_work \r\n";
            sSQL += "  where patch_uid = " + Quote(_patchUid) + "\r\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysPatchWork.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL  = "update sbt_sys_patch_work set \r\n";
            sSQL += " patch_uid = " + Quote(_patchUid) + ",\r\n";
            sSQL += " patch_key_id = " + Quote(_patchKeyId) + ",\r\n";
            sSQL += " patch_name = " + Quote(_patchName) + ",\r\n";
            sSQL += " patch_description = " + Quote(_patchDescription) + ",\r\n";
            sSQL += " distribute_time = " + Quote(_distributeTime) + ",\r\n";
            sSQL += " install_time = " + Quote(_installTime) + ",\r\n";
            sSQL += " update_begin_time = " + Quote(_updateBeginTime) + ",\r\n";
            sSQL += " update_end_time = " + Quote(_updateEndTime) + ",\r\n";
            sSQL += " update_duration = " + _updateDuration.ToString() + ",\r\n";
            sSQL += " update_status = " + Quote(_updateStatus) + ",\r\n";
            sSQL += " update_result_skip = " + Quote(_updateResultSkip) + ",\r\n";
            sSQL += " update_result_succ = " + Quote(_updateResultSucc) + ",\r\n";
            sSQL += " update_result_detail = " + Quote(_updateResultDetail) + ",\r\n";
            sSQL += " vesion_before_update = " + Quote(_vesionBeforeUpdate) + ",\r\n";
            sSQL += " version_after_update = " + Quote(_versionAfterUpdate) + ",\r\n";
            sSQL += " engine_call_times = " + _engineCallTimes.ToString() + ",\r\n";
            sSQL += " engine_last_result = " + Quote(_engineLastResult) + ",\r\n";
            sSQL += " engine_last_detail = " + Quote(_engineLastDetail) + ",\r\n";
            sSQL += " engine_last_time = " + Quote(_engineLastTime) + ",\r\n";
            sSQL += " create_time = " + Quote(_createTime) + ",\r\n";
            sSQL += " creator = " + Quote(_creator) + ",\r\n";
            sSQL += " modify_time = " + Quote(_modifyTime) + ",\r\n";
            sSQL += " remarks = " + Quote(_remarks) + "\r\n";
            sSQL += "  where patch_uid = " + Quote(_patchUid) + "\r\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysPatchWork.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from sbt_sys_patch_work \r\n";
            sSQL += "  where patch_uid = " + Quote(_patchUid) + "\r\n";

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
        public void FetchByE(string patchUid)
        {
            bool hasData;
            hasData = FetchBy(patchUid);
            if (!hasData)
                throw new Exception("TbSysPatchWork.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string patchUid)
        {
            _patchUid = patchUid;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbSysPatchWork CreateBy(string patchUid)
        {
            TbSysPatchWork tbl;
            bool hasData;

            tbl = new TbSysPatchWork();
            hasData = tbl.FetchBy(patchUid);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string patchUid)
        {
            TbSysPatchWork tbl;
            tbl = new TbSysPatchWork();

            tbl.PatchUid = patchUid;

            tbl.Delete();
        }
        #endregion

        #region 文件和访问组件相关的支持函数
        /// <summary>
        /// 通过DataRow进行赋值
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _patchUid            = DbToString(row["patch_uid"]);
            _patchKeyId          = DbToString(row["patch_key_id"]);
            _patchName           = DbToString(row["patch_name"]);
            _patchDescription    = DbToString(row["patch_description"]);
            _distributeTime      = DbToString(row["distribute_time"]);
            _installTime         = DbToString(row["install_time"]);
            _updateBeginTime     = DbToString(row["update_begin_time"]);
            _updateEndTime       = DbToString(row["update_end_time"]);
            _updateDuration      = DbToDouble(row["update_duration"]);
            _updateStatus        = DbToString(row["update_status"]);
            _updateResultSkip    = DbToString(row["update_result_skip"]);
            _updateResultSucc    = DbToString(row["update_result_succ"]);
            _updateResultDetail  = DbToString(row["update_result_detail"]);
            _vesionBeforeUpdate  = DbToString(row["vesion_before_update"]);
            _versionAfterUpdate  = DbToString(row["version_after_update"]);
            _engineCallTimes     = DbToInt(row["engine_call_times"]);
            _engineLastResult    = DbToString(row["engine_last_result"]);
            _engineLastDetail    = DbToString(row["engine_last_detail"]);
            _engineLastTime      = DbToString(row["engine_last_time"]);
            _createTime          = DbToString(row["create_time"]);
            _creator             = DbToString(row["creator"]);
            _modifyTime          = DbToString(row["modify_time"]);
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
            sLine = "patch_uid\x9" + _patchUid;
            writer.WriteLine(sLine);

            sLine = "patch_key_id\x9" + _patchKeyId;
            writer.WriteLine(sLine);

            sLine = "patch_name\x9" + _patchName;
            writer.WriteLine(sLine);

            sLine = "patch_description\x9" + _patchDescription;
            writer.WriteLine(sLine);

            sLine = "distribute_time\x9" + _distributeTime;
            writer.WriteLine(sLine);

            sLine = "install_time\x9" + _installTime;
            writer.WriteLine(sLine);

            sLine = "update_begin_time\x9" + _updateBeginTime;
            writer.WriteLine(sLine);

            sLine = "update_end_time\x9" + _updateEndTime;
            writer.WriteLine(sLine);

            sLine = "update_duration\x9" + _updateDuration.ToString();
            writer.WriteLine(sLine);

            sLine = "update_status\x9" + _updateStatus;
            writer.WriteLine(sLine);

            sLine = "update_result_skip\x9" + _updateResultSkip;
            writer.WriteLine(sLine);

            sLine = "update_result_succ\x9" + _updateResultSucc;
            writer.WriteLine(sLine);

            sLine = "update_result_detail\x9" + _updateResultDetail;
            writer.WriteLine(sLine);

            sLine = "vesion_before_update\x9" + _vesionBeforeUpdate;
            writer.WriteLine(sLine);

            sLine = "version_after_update\x9" + _versionAfterUpdate;
            writer.WriteLine(sLine);

            sLine = "engine_call_times\x9" + _engineCallTimes.ToString();
            writer.WriteLine(sLine);

            sLine = "engine_last_result\x9" + _engineLastResult;
            writer.WriteLine(sLine);

            sLine = "engine_last_detail\x9" + _engineLastDetail;
            writer.WriteLine(sLine);

            sLine = "engine_last_time\x9" + _engineLastTime;
            writer.WriteLine(sLine);

            sLine = "create_time\x9" + _createTime;
            writer.WriteLine(sLine);

            sLine = "creator\x9" + _creator;
            writer.WriteLine(sLine);

            sLine = "modify_time\x9" + _modifyTime;
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
            sLine  = "patch_uid\tpatch_key_id\tpatch_name\t";
            sLine += "patch_description\tdistribute_time\tinstall_time\t";
            sLine += "update_begin_time\tupdate_end_time\tupdate_duration\t";
            sLine += "update_status\tupdate_result_skip\tupdate_result_succ\t";
            sLine += "update_result_detail\tvesion_before_update\tversion_after_update\t";
            sLine += "engine_call_times\tengine_last_result\tengine_last_detail\t";
            sLine += "engine_last_time\tcreate_time\tcreator\t";
            sLine += "modify_time\tremarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL  = "select patch_uid,       patch_key_id,    patch_name,      \r\n";
            sSQL += "       patch_description,distribute_time, install_time,    \r\n";
            sSQL += "       update_begin_time,update_end_time, update_duration, \r\n";
            sSQL += "       update_status,   update_result_skip,update_result_succ,\r\n";
            sSQL += "       update_result_detail,vesion_before_update,version_after_update,\r\n";
            sSQL += "       engine_call_times,engine_last_result,engine_last_detail,\r\n";
            sSQL += "       engine_last_time,create_time,     creator,         \r\n";
            sSQL += "       modify_time,     remarks         \r\n";
            sSQL += "  from sbt_sys_patch_work";

            //======== 4. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. 处理每一行 ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 处理每一列 ========
                for (nCol = 0; nCol < 23; nCol++)
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
                case "patch_uid":
                    return "varchar";
                case "patch_key_id":
                    return "varchar";
                case "patch_name":
                    return "varchar";
                case "patch_description":
                    return "text";
                case "distribute_time":
                    return "varchar";
                case "install_time":
                    return "varchar";
                case "update_begin_time":
                    return "varchar";
                case "update_end_time":
                    return "varchar";
                case "update_duration":
                    return "numeric";
                case "update_status":
                    return "varchar";
                case "update_result_skip":
                    return "char";
                case "update_result_succ":
                    return "char";
                case "update_result_detail":
                    return "varchar";
                case "vesion_before_update":
                    return "varchar";
                case "version_after_update":
                    return "varchar";
                case "engine_call_times":
                    return "int";
                case "engine_last_result":
                    return "varchar";
                case "engine_last_detail":
                    return "varchar";
                case "engine_last_time":
                    return "varchar";
                case "create_time":
                    return "varchar";
                case "creator":
                    return "varchar";
                case "modify_time":
                    return "varchar";
                case "remarks":
                    return "varchar";
                default:
                    throw new Exception("TbSysPatchWorkBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 获取一个字段的CSharp类型
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "patch_uid":
                    return "string";
                case "patch_key_id":
                    return "string";
                case "patch_name":
                    return "string";
                case "patch_description":
                    return "string";
                case "distribute_time":
                    return "string";
                case "install_time":
                    return "string";
                case "update_begin_time":
                    return "string";
                case "update_end_time":
                    return "string";
                case "update_duration":
                    return "double";
                case "update_status":
                    return "string";
                case "update_result_skip":
                    return "string";
                case "update_result_succ":
                    return "string";
                case "update_result_detail":
                    return "string";
                case "vesion_before_update":
                    return "string";
                case "version_after_update":
                    return "string";
                case "engine_call_times":
                    return "int";
                case "engine_last_result":
                    return "string";
                case "engine_last_detail":
                    return "string";
                case "engine_last_time":
                    return "string";
                case "create_time":
                    return "string";
                case "creator":
                    return "string";
                case "modify_time":
                    return "string";
                case "remarks":
                    return "string";
                default:
                    throw new Exception("TbSysPatchWorkBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL  = "select " + toFieldName + " from sbt_sys_patch_work \r\n";
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
