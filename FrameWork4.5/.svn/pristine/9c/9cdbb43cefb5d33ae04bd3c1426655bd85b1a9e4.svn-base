using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework.Patch.DBDefine
{
    #region 枚举
    #endregion

    /// <summary>
    /// 应用的各个版本 (sbt_sys_webapp_version)的表操作类
    /// </summary>
    public class TbSysWebappVersion : TbSysWebappVersionBase
    {
        #region 枚举属性
        #endregion

        #region 用户可编辑区域

        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }


    /// <summary>
    /// 应用的各个版本 (sbt_sys_webapp_version)的字段名类
    /// </summary>
    public class TbSysWebappVersionF
    {
        public const string TableName = "sbt_sys_webapp_version";
        public const string VersionUid           = "version_uid";
        public const string WebappId             = "webapp_id";
        public const string VersionNum           = "version_num";
        public const string VersionNumDisp       = "version_num_disp";
        public const string VersionBriefDesc     = "version_brief_desc";
        public const string VersionDetailDesc    = "version_detail_desc";
        public const string LastPatchUid         = "last_patch_uid";
        public const string LastPatchKeyId       = "last_patch_key_id";
        public const string CreateTime           = "create_time";
        public const string Creator              = "creator";
        public const string ModifyTime           = "modify_time";
        public const string Remarks              = "remarks";
    }


    /// <summary>
    /// 应用的各个版本 (sbt_sys_webapp_version)的表操作基类
    /// </summary>
    public class TbSysWebappVersionBase : Sigbit.Data.TableBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TbSysWebappVersionBase()
        {
            ResetData();
        }

        #region 属性定义
        protected string _versionUid = "";
        /// <summary>
        /// 版本标识，主键
        /// </summary>
        public string VersionUid
        {
            get { return _versionUid; }
            set { _versionUid = value; }
        }

        protected string _webappId = "";
        /// <summary>
        /// 软件标识
        /// </summary>
        public string WebappId
        {
            get { return _webappId; }
            set { _webappId = value; }
        }

        protected string _versionNum = "";
        /// <summary>
        /// 规整的版本标识
        /// </summary>
        public string VersionNum
        {
            get { return _versionNum; }
            set { _versionNum = value; }
        }

        protected string _versionNumDisp = "";
        /// <summary>
        /// 显示的版本标识
        /// </summary>
        public string VersionNumDisp
        {
            get { return _versionNumDisp; }
            set { _versionNumDisp = value; }
        }

        protected string _versionBriefDesc = "";
        /// <summary>
        /// 版本简短描述
        /// </summary>
        public string VersionBriefDesc
        {
            get { return _versionBriefDesc; }
            set { _versionBriefDesc = value; }
        }

        protected string _versionDetailDesc = "";
        /// <summary>
        /// 版本详细说明
        /// </summary>
        public string VersionDetailDesc
        {
            get { return _versionDetailDesc; }
            set { _versionDetailDesc = value; }
        }

        protected string _lastPatchUid = "";
        /// <summary>
        /// 最后一个补丁标识
        /// </summary>
        public string LastPatchUid
        {
            get { return _lastPatchUid; }
            set { _lastPatchUid = value; }
        }

        protected string _lastPatchKeyId = "";
        /// <summary>
        /// 最后一个补丁ID
        /// </summary>
        public string LastPatchKeyId
        {
            get { return _lastPatchKeyId; }
            set { _lastPatchKeyId = value; }
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
            sb.Append("VersionUid: " + this._versionUid + "<br>");
            sb.Append("WebappId: " + this._webappId + "<br>");
            sb.Append("VersionNum: " + this._versionNum + "<br>");
            sb.Append("VersionNumDisp: " + this._versionNumDisp + "<br>");
            sb.Append("VersionBriefDesc: " + this._versionBriefDesc + "<br>");
            sb.Append("VersionDetailDesc: " + this._versionDetailDesc + "<br>");
            sb.Append("LastPatchUid: " + this._lastPatchUid + "<br>");
            sb.Append("LastPatchKeyId: " + this._lastPatchKeyId + "<br>");
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
            _versionUid = "";
            _webappId = "";
            _versionNum = "";
            _versionNumDisp = "";
            _versionBriefDesc = "";
            _versionDetailDesc = "";
            _lastPatchUid = "";
            _lastPatchKeyId = "";
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
            sSQL  = "select webapp_id,           version_num,         version_num_disp,     \r\n";
            sSQL += "       version_brief_desc,  version_detail_desc, last_patch_uid,       \r\n";
            sSQL += "       last_patch_key_id,   create_time,         creator,              \r\n";
            sSQL += "       modify_time,         remarks              \r\n";
            sSQL += "  from sbt_sys_webapp_version    \r\n";
            sSQL += "  where version_uid = " + Quote(_versionUid) + "\r\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbSysWebappVersion.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _webappId       = DbToString(row["webapp_id"]);
            _versionNum     = DbToString(row["version_num"]);
            _versionNumDisp = DbToString(row["version_num_disp"]);
            _versionBriefDesc = DbToString(row["version_brief_desc"]);
            _versionDetailDesc = DbToString(row["version_detail_desc"]);
            _lastPatchUid   = DbToString(row["last_patch_uid"]);
            _lastPatchKeyId = DbToString(row["last_patch_key_id"]);
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

            sSQL  = "insert into sbt_sys_webapp_version \r\n";
            sSQL += "( version_uid,      webapp_id,        \r\n";
            sSQL += "  version_num,      version_num_disp, \r\n";
            sSQL += "  version_brief_desc, version_detail_desc, \r\n";
            sSQL += "  last_patch_uid,   last_patch_key_id, \r\n";
            sSQL += "  create_time,      creator,          \r\n";
            sSQL += "  modify_time,      remarks           \r\n";
            sSQL += ") values (          \r\n";
            sSQL += Quote(_versionUid)      +","+ Quote(_webappId)        +",\r\n";
            sSQL += Quote(_versionNum)      +","+ Quote(_versionNumDisp)  +",\r\n";
            sSQL += Quote(_versionBriefDesc)+","+ Quote(_versionDetailDesc)+",\r\n";
            sSQL += Quote(_lastPatchUid)    +","+ Quote(_lastPatchKeyId)  +",\r\n";
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

            sSQL  = "delete from sbt_sys_webapp_version \r\n";
            sSQL += "  where version_uid = " + Quote(_versionUid) + "\r\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysWebappVersion.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL  = "update sbt_sys_webapp_version set \r\n";
            sSQL += " version_uid = " + Quote(_versionUid) + ",\r\n";
            sSQL += " webapp_id = " + Quote(_webappId) + ",\r\n";
            sSQL += " version_num = " + Quote(_versionNum) + ",\r\n";
            sSQL += " version_num_disp = " + Quote(_versionNumDisp) + ",\r\n";
            sSQL += " version_brief_desc = " + Quote(_versionBriefDesc) + ",\r\n";
            sSQL += " version_detail_desc = " + Quote(_versionDetailDesc) + ",\r\n";
            sSQL += " last_patch_uid = " + Quote(_lastPatchUid) + ",\r\n";
            sSQL += " last_patch_key_id = " + Quote(_lastPatchKeyId) + ",\r\n";
            sSQL += " create_time = " + Quote(_createTime) + ",\r\n";
            sSQL += " creator = " + Quote(_creator) + ",\r\n";
            sSQL += " modify_time = " + Quote(_modifyTime) + ",\r\n";
            sSQL += " remarks = " + Quote(_remarks) + "\r\n";
            sSQL += "  where version_uid = " + Quote(_versionUid) + "\r\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysWebappVersion.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from sbt_sys_webapp_version \r\n";
            sSQL += "  where version_uid = " + Quote(_versionUid) + "\r\n";

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
        public void FetchByE(string versionUid)
        {
            bool hasData;
            hasData = FetchBy(versionUid);
            if (!hasData)
                throw new Exception("TbSysWebappVersion.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string versionUid)
        {
            _versionUid = versionUid;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbSysWebappVersion CreateBy(string versionUid)
        {
            TbSysWebappVersion tbl;
            bool hasData;

            tbl = new TbSysWebappVersion();
            hasData = tbl.FetchBy(versionUid);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string versionUid)
        {
            TbSysWebappVersion tbl;
            tbl = new TbSysWebappVersion();

            tbl.VersionUid = versionUid;

            tbl.Delete();
        }
        #endregion

        #region 文件和访问组件相关的支持函数
        /// <summary>
        /// 通过DataRow进行赋值
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _versionUid          = DbToString(row["version_uid"]);
            _webappId            = DbToString(row["webapp_id"]);
            _versionNum          = DbToString(row["version_num"]);
            _versionNumDisp      = DbToString(row["version_num_disp"]);
            _versionBriefDesc    = DbToString(row["version_brief_desc"]);
            _versionDetailDesc   = DbToString(row["version_detail_desc"]);
            _lastPatchUid        = DbToString(row["last_patch_uid"]);
            _lastPatchKeyId      = DbToString(row["last_patch_key_id"]);
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
            sLine = "version_uid\x9" + _versionUid;
            writer.WriteLine(sLine);

            sLine = "webapp_id\x9" + _webappId;
            writer.WriteLine(sLine);

            sLine = "version_num\x9" + _versionNum;
            writer.WriteLine(sLine);

            sLine = "version_num_disp\x9" + _versionNumDisp;
            writer.WriteLine(sLine);

            sLine = "version_brief_desc\x9" + _versionBriefDesc;
            writer.WriteLine(sLine);

            sLine = "version_detail_desc\x9" + _versionDetailDesc;
            writer.WriteLine(sLine);

            sLine = "last_patch_uid\x9" + _lastPatchUid;
            writer.WriteLine(sLine);

            sLine = "last_patch_key_id\x9" + _lastPatchKeyId;
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
            sLine  = "version_uid\twebapp_id\tversion_num\t";
            sLine += "version_num_disp\tversion_brief_desc\tversion_detail_desc\t";
            sLine += "last_patch_uid\tlast_patch_key_id\tcreate_time\t";
            sLine += "creator\tmodify_time\tremarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL  = "select version_uid,     webapp_id,       version_num,     \r\n";
            sSQL += "       version_num_disp,version_brief_desc,version_detail_desc,\r\n";
            sSQL += "       last_patch_uid,  last_patch_key_id,create_time,     \r\n";
            sSQL += "       creator,         modify_time,     remarks         \r\n";
            sSQL += "  from sbt_sys_webapp_version";

            //======== 4. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. 处理每一行 ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 处理每一列 ========
                for (nCol = 0; nCol < 12; nCol++)
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
                case "version_uid":
                    return "varchar";
                case "webapp_id":
                    return "varchar";
                case "version_num":
                    return "varchar";
                case "version_num_disp":
                    return "varchar";
                case "version_brief_desc":
                    return "varchar";
                case "version_detail_desc":
                    return "text";
                case "last_patch_uid":
                    return "varchar";
                case "last_patch_key_id":
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
                    throw new Exception("TbSysWebappVersionBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 获取一个字段的CSharp类型
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "version_uid":
                    return "string";
                case "webapp_id":
                    return "string";
                case "version_num":
                    return "string";
                case "version_num_disp":
                    return "string";
                case "version_brief_desc":
                    return "string";
                case "version_detail_desc":
                    return "string";
                case "last_patch_uid":
                    return "string";
                case "last_patch_key_id":
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
                    throw new Exception("TbSysWebappVersionBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL  = "select " + toFieldName + " from sbt_sys_webapp_version \r\n";
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
