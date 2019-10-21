using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.App.Net.IBXService.VCodeBreak.Service.DBDefine
{
    #region 枚举
    #endregion

    /// <summary>
    /// 验证码场景的定义 (vcb_sys_vcode)的表操作类
    /// </summary>
    public class TbSysVcode : TbSysVcodeBase
    {
        #region 枚举属性
        /// <summary>
        /// 破解算法
        /// </summary>
        public TbSysBreakAlgolEAlgolID AlgolIdE
        {
            get
            {
                TbSysBreakAlgolEAlgolID enumRet =
                        (TbSysBreakAlgolEAlgolID)ConvertUtil.StringToEnum
                        (this.AlgolId, TbSysBreakAlgolEAlgolID.None);
                return enumRet;
            }
            set
            {
                this.AlgolId = ConvertUtil.EnumToString(value);
            }
        }
        #endregion

        #region 用户可编辑区域

        public override void Insert()
        {
            base.Insert();
            QDBVCBreakPools.ResetVCodeUsage();
        }

        public override void Delete()
        {
            base.Delete();
            QDBVCBreakPools.ResetVCodeUsage();
        }

        public override void Update()
        {
            base.Update();
            QDBVCBreakPools.ResetVCodeUsage();
        }

        #endregion
    }


    /// <summary>
    /// 验证码场景的定义 (vcb_sys_vcode)的字段名类
    /// </summary>
    public class TbSysVcodeF
    {
        public const string TableName = "vcb_sys_vcode";
        public const string VcodeId              = "vcode_id";
        public const string VcodeName            = "vcode_name";
        public const string VcodeDesc            = "vcode_desc";
        public const string AlgolId              = "algol_id";
        public const string AlgolParams          = "algol_params";
        public const string CallRate             = "call_rate";
        public const string CallFakeMinSec       = "call_fake_min_sec";
        public const string CallFakeMaxSec       = "call_fake_max_sec";
        public const string CallForceMinSec      = "call_force_min_sec";
        public const string CallForceMaxSec      = "call_force_max_sec";
        public const string ExInfo01             = "ex_info_01";
        public const string ExInfo02             = "ex_info_02";
        public const string ExInfo03             = "ex_info_03";
        public const string ExInfo04             = "ex_info_04";
        public const string CreateTime           = "create_time";
        public const string Creator              = "creator";
        public const string ModifyTime           = "modify_time";
        public const string Remarks              = "remarks";
    }


    /// <summary>
    /// 验证码场景的定义 (vcb_sys_vcode)的表操作基类
    /// </summary>
    public class TbSysVcodeBase : Sigbit.Data.TableBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TbSysVcodeBase()
        {
            ResetData();
        }

        #region 属性定义
        protected string _vcodeId = "";
        /// <summary>
        /// 验证码标识，主键
        /// </summary>
        public string VcodeId
        {
            get { return _vcodeId; }
            set { _vcodeId = value; }
        }

        protected string _vcodeName = "";
        /// <summary>
        /// 验证码名称
        /// </summary>
        public string VcodeName
        {
            get { return _vcodeName; }
            set { _vcodeName = value; }
        }

        protected string _vcodeDesc = "";
        /// <summary>
        /// 验证码描述
        /// </summary>
        public string VcodeDesc
        {
            get { return _vcodeDesc; }
            set { _vcodeDesc = value; }
        }

        protected string _algolId = "";
        /// <summary>
        /// 算法标识
        /// </summary>
        public string AlgolId
        {
            get { return _algolId; }
            set { _algolId = value; }
        }

        protected string _algolParams = "";
        /// <summary>
        /// 算法相关的参数
        /// </summary>
        public string AlgolParams
        {
            get { return _algolParams; }
            set { _algolParams = value; }
        }

        protected double _callRate;
        /// <summary>
        /// 调用引擎的比例
        /// </summary>
        public double CallRate
        {
            get { return _callRate; }
            set { _callRate = value; }
        }

        protected double _callFakeMinSec;
        /// <summary>
        /// 伪装调用的时延范围
        /// </summary>
        public double CallFakeMinSec
        {
            get { return _callFakeMinSec; }
            set { _callFakeMinSec = value; }
        }

        protected double _callFakeMaxSec;
        /// <summary>
        /// 伪装调用的时延范围
        /// </summary>
        public double CallFakeMaxSec
        {
            get { return _callFakeMaxSec; }
            set { _callFakeMaxSec = value; }
        }

        protected double _callForceMinSec;
        /// <summary>
        /// 真实调用的强制时延
        /// </summary>
        public double CallForceMinSec
        {
            get { return _callForceMinSec; }
            set { _callForceMinSec = value; }
        }

        protected double _callForceMaxSec;
        /// <summary>
        /// 真实调用的强制时延
        /// </summary>
        public double CallForceMaxSec
        {
            get { return _callForceMaxSec; }
            set { _callForceMaxSec = value; }
        }

        protected string _exInfo01 = "";
        /// <summary>
        /// 扩展信息
        /// </summary>
        public string ExInfo01
        {
            get { return _exInfo01; }
            set { _exInfo01 = value; }
        }

        protected string _exInfo02 = "";
        /// <summary>
        /// 扩展信息
        /// </summary>
        public string ExInfo02
        {
            get { return _exInfo02; }
            set { _exInfo02 = value; }
        }

        protected string _exInfo03 = "";
        /// <summary>
        /// 扩展信息
        /// </summary>
        public string ExInfo03
        {
            get { return _exInfo03; }
            set { _exInfo03 = value; }
        }

        protected string _exInfo04 = "";
        /// <summary>
        /// 扩展信息
        /// </summary>
        public string ExInfo04
        {
            get { return _exInfo04; }
            set { _exInfo04 = value; }
        }

        protected string _createTime = "";
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        protected string _creator = "";
        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }

        protected string _modifyTime = "";
        /// <summary>
        /// 修改时间
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
            sb.Append("VcodeId: " + this._vcodeId + "<br>");
            sb.Append("VcodeName: " + this._vcodeName + "<br>");
            sb.Append("VcodeDesc: " + this._vcodeDesc + "<br>");
            sb.Append("AlgolId: " + this._algolId + "<br>");
            sb.Append("AlgolParams: " + this._algolParams + "<br>");
            sb.Append("CallRate: " + this._callRate + "<br>");
            sb.Append("CallFakeMinSec: " + this._callFakeMinSec + "<br>");
            sb.Append("CallFakeMaxSec: " + this._callFakeMaxSec + "<br>");
            sb.Append("CallForceMinSec: " + this._callForceMinSec + "<br>");
            sb.Append("CallForceMaxSec: " + this._callForceMaxSec + "<br>");
            sb.Append("ExInfo01: " + this._exInfo01 + "<br>");
            sb.Append("ExInfo02: " + this._exInfo02 + "<br>");
            sb.Append("ExInfo03: " + this._exInfo03 + "<br>");
            sb.Append("ExInfo04: " + this._exInfo04 + "<br>");
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
            _vcodeId = "";
            _vcodeName = "";
            _vcodeDesc = "";
            _algolId = "";
            _algolParams = "";
            _callRate = 0;
            _callFakeMinSec = 0;
            _callFakeMaxSec = 0;
            _callForceMinSec = 0;
            _callForceMaxSec = 0;
            _exInfo01 = "";
            _exInfo02 = "";
            _exInfo03 = "";
            _exInfo04 = "";
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
            sSQL  = "select vcode_name,          vcode_desc,          algol_id,             \r\n";
            sSQL += "       algol_params,        call_rate,           call_fake_min_sec,    \r\n";
            sSQL += "       call_fake_max_sec,   call_force_min_sec,  call_force_max_sec,   \r\n";
            sSQL += "       ex_info_01,          ex_info_02,          ex_info_03,           \r\n";
            sSQL += "       ex_info_04,          create_time,         creator,              \r\n";
            sSQL += "       modify_time,         remarks              \r\n";
            sSQL += "  from vcb_sys_vcode    \r\n";
            sSQL += "  where vcode_id = " + Quote(_vcodeId) + "\r\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbSysVcode.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _vcodeName      = DbToString(row["vcode_name"]);
            _vcodeDesc      = DbToString(row["vcode_desc"]);
            _algolId        = DbToString(row["algol_id"]);
            _algolParams    = DbToString(row["algol_params"]);
            _callRate       = DbToDouble(row["call_rate"]);
            _callFakeMinSec = DbToDouble(row["call_fake_min_sec"]);
            _callFakeMaxSec = DbToDouble(row["call_fake_max_sec"]);
            _callForceMinSec = DbToDouble(row["call_force_min_sec"]);
            _callForceMaxSec = DbToDouble(row["call_force_max_sec"]);
            _exInfo01       = DbToString(row["ex_info_01"]);
            _exInfo02       = DbToString(row["ex_info_02"]);
            _exInfo03       = DbToString(row["ex_info_03"]);
            _exInfo04       = DbToString(row["ex_info_04"]);
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

            sSQL  = "insert into vcb_sys_vcode \r\n";
            sSQL += "( vcode_id,         vcode_name,       \r\n";
            sSQL += "  vcode_desc,       algol_id,         \r\n";
            sSQL += "  algol_params,     call_rate,        \r\n";
            sSQL += "  call_fake_min_sec, call_fake_max_sec, \r\n";
            sSQL += "  call_force_min_sec, call_force_max_sec, \r\n";
            sSQL += "  ex_info_01,       ex_info_02,       \r\n";
            sSQL += "  ex_info_03,       ex_info_04,       \r\n";
            sSQL += "  create_time,      creator,          \r\n";
            sSQL += "  modify_time,      remarks           \r\n";
            sSQL += ") values (          \r\n";
            sSQL += Quote(_vcodeId)         +","+ Quote(_vcodeName)       +",\r\n";
            sSQL += Quote(_vcodeDesc)       +","+ Quote(_algolId)         +",\r\n";
            sSQL += Quote(_algolParams)     +","+ _callRate.ToString()    +",\r\n";
            sSQL += _callFakeMinSec.ToString()+","+ _callFakeMaxSec.ToString()+",\r\n";
            sSQL += _callForceMinSec.ToString()+","+ _callForceMaxSec.ToString()+",\r\n";
            sSQL += Quote(_exInfo01)        +","+ Quote(_exInfo02)        +",\r\n";
            sSQL += Quote(_exInfo03)        +","+ Quote(_exInfo04)        +",\r\n";
            sSQL += Quote(_createTime)      +","+ Quote(_creator)         +",\r\n";
            sSQL += Quote(_modifyTime)      +","+ Quote(_remarks)         +")\r\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 按主键删除一条数据
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL  = "delete from vcb_sys_vcode \r\n";
            sSQL += "  where vcode_id = " + Quote(_vcodeId) + "\r\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysVcode.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL  = "update vcb_sys_vcode set \r\n";
            sSQL += " vcode_id = " + Quote(_vcodeId) + ",\r\n";
            sSQL += " vcode_name = " + Quote(_vcodeName) + ",\r\n";
            sSQL += " vcode_desc = " + Quote(_vcodeDesc) + ",\r\n";
            sSQL += " algol_id = " + Quote(_algolId) + ",\r\n";
            sSQL += " algol_params = " + Quote(_algolParams) + ",\r\n";
            sSQL += " call_rate = " + _callRate.ToString() + ",\r\n";
            sSQL += " call_fake_min_sec = " + _callFakeMinSec.ToString() + ",\r\n";
            sSQL += " call_fake_max_sec = " + _callFakeMaxSec.ToString() + ",\r\n";
            sSQL += " call_force_min_sec = " + _callForceMinSec.ToString() + ",\r\n";
            sSQL += " call_force_max_sec = " + _callForceMaxSec.ToString() + ",\r\n";
            sSQL += " ex_info_01 = " + Quote(_exInfo01) + ",\r\n";
            sSQL += " ex_info_02 = " + Quote(_exInfo02) + ",\r\n";
            sSQL += " ex_info_03 = " + Quote(_exInfo03) + ",\r\n";
            sSQL += " ex_info_04 = " + Quote(_exInfo04) + ",\r\n";
            sSQL += " create_time = " + Quote(_createTime) + ",\r\n";
            sSQL += " creator = " + Quote(_creator) + ",\r\n";
            sSQL += " modify_time = " + Quote(_modifyTime) + ",\r\n";
            sSQL += " remarks = " + Quote(_remarks) + "\r\n";
            sSQL += "  where vcode_id = " + Quote(_vcodeId) + "\r\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysVcode.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from vcb_sys_vcode \r\n";
            sSQL += "  where vcode_id = " + Quote(_vcodeId) + "\r\n";

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
        public void FetchByE(string vcodeId)
        {
            bool hasData;
            hasData = FetchBy(vcodeId);
            if (!hasData)
                throw new Exception("TbSysVcode.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string vcodeId)
        {
            _vcodeId = vcodeId;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbSysVcode CreateBy(string vcodeId)
        {
            TbSysVcode tbl;
            bool hasData;

            tbl = new TbSysVcode();
            hasData = tbl.FetchBy(vcodeId);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string vcodeId)
        {
            TbSysVcode tbl;
            tbl = new TbSysVcode();

            tbl.VcodeId = vcodeId;

            tbl.Delete();
        }
        #endregion

        #region 文件和访问组件相关的支持函数
        /// <summary>
        /// 通过DataRow进行赋值
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _vcodeId             = DbToString(row["vcode_id"]);
            _vcodeName           = DbToString(row["vcode_name"]);
            _vcodeDesc           = DbToString(row["vcode_desc"]);
            _algolId             = DbToString(row["algol_id"]);
            _algolParams         = DbToString(row["algol_params"]);
            _callRate            = DbToDouble(row["call_rate"]);
            _callFakeMinSec      = DbToDouble(row["call_fake_min_sec"]);
            _callFakeMaxSec      = DbToDouble(row["call_fake_max_sec"]);
            _callForceMinSec     = DbToDouble(row["call_force_min_sec"]);
            _callForceMaxSec     = DbToDouble(row["call_force_max_sec"]);
            _exInfo01            = DbToString(row["ex_info_01"]);
            _exInfo02            = DbToString(row["ex_info_02"]);
            _exInfo03            = DbToString(row["ex_info_03"]);
            _exInfo04            = DbToString(row["ex_info_04"]);
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
            sLine = "vcode_id\x9" + _vcodeId;
            writer.WriteLine(sLine);

            sLine = "vcode_name\x9" + _vcodeName;
            writer.WriteLine(sLine);

            sLine = "vcode_desc\x9" + _vcodeDesc;
            writer.WriteLine(sLine);

            sLine = "algol_id\x9" + _algolId;
            writer.WriteLine(sLine);

            sLine = "algol_params\x9" + _algolParams;
            writer.WriteLine(sLine);

            sLine = "call_rate\x9" + _callRate.ToString();
            writer.WriteLine(sLine);

            sLine = "call_fake_min_sec\x9" + _callFakeMinSec.ToString();
            writer.WriteLine(sLine);

            sLine = "call_fake_max_sec\x9" + _callFakeMaxSec.ToString();
            writer.WriteLine(sLine);

            sLine = "call_force_min_sec\x9" + _callForceMinSec.ToString();
            writer.WriteLine(sLine);

            sLine = "call_force_max_sec\x9" + _callForceMaxSec.ToString();
            writer.WriteLine(sLine);

            sLine = "ex_info_01\x9" + _exInfo01;
            writer.WriteLine(sLine);

            sLine = "ex_info_02\x9" + _exInfo02;
            writer.WriteLine(sLine);

            sLine = "ex_info_03\x9" + _exInfo03;
            writer.WriteLine(sLine);

            sLine = "ex_info_04\x9" + _exInfo04;
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
            sLine  = "vcode_id\tvcode_name\tvcode_desc\t";
            sLine += "algol_id\talgol_params\tcall_rate\t";
            sLine += "call_fake_min_sec\tcall_fake_max_sec\tcall_force_min_sec\t";
            sLine += "call_force_max_sec\tex_info_01\tex_info_02\t";
            sLine += "ex_info_03\tex_info_04\tcreate_time\t";
            sLine += "creator\tmodify_time\tremarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL  = "select vcode_id,        vcode_name,      vcode_desc,      \r\n";
            sSQL += "       algol_id,        algol_params,    call_rate,       \r\n";
            sSQL += "       call_fake_min_sec,call_fake_max_sec,call_force_min_sec,\r\n";
            sSQL += "       call_force_max_sec,ex_info_01,      ex_info_02,      \r\n";
            sSQL += "       ex_info_03,      ex_info_04,      create_time,     \r\n";
            sSQL += "       creator,         modify_time,     remarks         \r\n";
            sSQL += "  from vcb_sys_vcode";

            //======== 4. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. 处理每一行 ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 处理每一列 ========
                for (nCol = 0; nCol < 18; nCol++)
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
                case "vcode_id":
                    return "varchar";
                case "vcode_name":
                    return "varchar";
                case "vcode_desc":
                    return "varchar";
                case "algol_id":
                    return "varchar";
                case "algol_params":
                    return "varchar";
                case "call_rate":
                    return "numeric";
                case "call_fake_min_sec":
                    return "numeric";
                case "call_fake_max_sec":
                    return "numeric";
                case "call_force_min_sec":
                    return "numeric";
                case "call_force_max_sec":
                    return "numeric";
                case "ex_info_01":
                    return "varchar";
                case "ex_info_02":
                    return "varchar";
                case "ex_info_03":
                    return "varchar";
                case "ex_info_04":
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
                    throw new Exception("TbSysVcodeBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 获取一个字段的CSharp类型
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "vcode_id":
                    return "string";
                case "vcode_name":
                    return "string";
                case "vcode_desc":
                    return "string";
                case "algol_id":
                    return "string";
                case "algol_params":
                    return "string";
                case "call_rate":
                    return "double";
                case "call_fake_min_sec":
                    return "double";
                case "call_fake_max_sec":
                    return "double";
                case "call_force_min_sec":
                    return "double";
                case "call_force_max_sec":
                    return "double";
                case "ex_info_01":
                    return "string";
                case "ex_info_02":
                    return "string";
                case "ex_info_03":
                    return "string";
                case "ex_info_04":
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
                    throw new Exception("TbSysVcodeBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL  = "select " + toFieldName + " from vcb_sys_vcode \r\n";
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
