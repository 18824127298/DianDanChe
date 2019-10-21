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
    /// 系统应用(sbt_sys_webapp)的表操作类
    /// </summary>
    public class TbSysWebapp : TbSysWebappBase
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
    /// 系统应用(sbt_sys_webapp)的字段名类
    /// </summary>
    public class TbSysWebappF
    {
        public const string TableName = "sbt_sys_webapp";
        public const string WebappId             = "webapp_id";
        public const string WebappName           = "webapp_name";
        public const string WebappDesc           = "webapp_desc";
        public const string CurrentVersionUid    = "current_version_uid";
        public const string CurrentVersionNum    = "current_version_num";
        public const string LastPatchUid         = "last_patch_uid";
        public const string LastPatchKeyId       = "last_patch_key_id";
        public const string CreateTime           = "create_time";
        public const string Creator              = "creator";
        public const string ModifyTime           = "modify_time";
        public const string Remarks              = "remarks";
    }


    /// <summary>
    /// 系统应用(sbt_sys_webapp)的表操作基类
    /// </summary>
    public class TbSysWebappBase : Sigbit.Data.TableBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TbSysWebappBase()
        {
            ResetData();
        }

        #region 属性定义
        protected string _webappId = "";
        /// <summary>
        /// 应用标识，主键
        /// </summary>
        public string WebappId
        {
            get { return _webappId; }
            set { _webappId = value; }
        }

        protected string _webappName = "";
        /// <summary>
        /// 应用名称
        /// </summary>
        public string WebappName
        {
            get { return _webappName; }
            set { _webappName = value; }
        }

        protected string _webappDesc = "";
        /// <summary>
        /// 应用描述
        /// </summary>
        public string WebappDesc
        {
            get { return _webappDesc; }
            set { _webappDesc = value; }
        }

        protected string _currentVersionUid = "";
        /// <summary>
        /// 当前版本标识
        /// </summary>
        public string CurrentVersionUid
        {
            get { return _currentVersionUid; }
            set { _currentVersionUid = value; }
        }

        protected string _currentVersionNum = "";
        /// <summary>
        /// 当前版本号
        /// </summary>
        public string CurrentVersionNum
        {
            get { return _currentVersionNum; }
            set { _currentVersionNum = value; }
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
            sb.Append("WebappId: " + this._webappId + "<br>");
            sb.Append("WebappName: " + this._webappName + "<br>");
            sb.Append("WebappDesc: " + this._webappDesc + "<br>");
            sb.Append("CurrentVersionUid: " + this._currentVersionUid + "<br>");
            sb.Append("CurrentVersionNum: " + this._currentVersionNum + "<br>");
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
            _webappId = "";
            _webappName = "";
            _webappDesc = "";
            _currentVersionUid = "";
            _currentVersionNum = "";
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
            sSQL  = "select webapp_name,         webapp_desc,         current_version_uid,  \r\n";
            sSQL += "       current_version_num, last_patch_uid,      last_patch_key_id,    \r\n";
            sSQL += "       create_time,         creator,             modify_time,          \r\n";
            sSQL += "       remarks              \r\n";
            sSQL += "  from sbt_sys_webapp    \r\n";
            sSQL += "  where webapp_id = " + Quote(_webappId) + "\r\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbSysWebapp.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _webappName     = DbToString(row["webapp_name"]);
            _webappDesc     = DbToString(row["webapp_desc"]);
            _currentVersionUid = DbToString(row["current_version_uid"]);
            _currentVersionNum = DbToString(row["current_version_num"]);
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

            sSQL  = "insert into sbt_sys_webapp \r\n";
            sSQL += "( webapp_id,        webapp_name,      \r\n";
            sSQL += "  webapp_desc,      current_version_uid, \r\n";
            sSQL += "  current_version_num, last_patch_uid,   \r\n";
            sSQL += "  last_patch_key_id, create_time,      \r\n";
            sSQL += "  creator,          modify_time,      \r\n";
            sSQL += "  remarks           \r\n";
            sSQL += ") values (          \r\n";
            sSQL += Quote(_webappId)        +","+ Quote(_webappName)      +",\r\n";
            sSQL += Quote(_webappDesc)      +","+ Quote(_currentVersionUid)+",\r\n";
            sSQL += Quote(_currentVersionNum)+","+ Quote(_lastPatchUid)    +",\r\n";
            sSQL += Quote(_lastPatchKeyId)  +","+ Quote(_createTime)      +",\r\n";
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

            sSQL  = "delete from sbt_sys_webapp \r\n";
            sSQL += "  where webapp_id = " + Quote(_webappId) + "\r\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysWebapp.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL  = "update sbt_sys_webapp set \r\n";
            sSQL += " webapp_id = " + Quote(_webappId) + ",\r\n";
            sSQL += " webapp_name = " + Quote(_webappName) + ",\r\n";
            sSQL += " webapp_desc = " + Quote(_webappDesc) + ",\r\n";
            sSQL += " current_version_uid = " + Quote(_currentVersionUid) + ",\r\n";
            sSQL += " current_version_num = " + Quote(_currentVersionNum) + ",\r\n";
            sSQL += " last_patch_uid = " + Quote(_lastPatchUid) + ",\r\n";
            sSQL += " last_patch_key_id = " + Quote(_lastPatchKeyId) + ",\r\n";
            sSQL += " create_time = " + Quote(_createTime) + ",\r\n";
            sSQL += " creator = " + Quote(_creator) + ",\r\n";
            sSQL += " modify_time = " + Quote(_modifyTime) + ",\r\n";
            sSQL += " remarks = " + Quote(_remarks) + "\r\n";
            sSQL += "  where webapp_id = " + Quote(_webappId) + "\r\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysWebapp.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from sbt_sys_webapp \r\n";
            sSQL += "  where webapp_id = " + Quote(_webappId) + "\r\n";

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
        public void FetchByE(string webappId)
        {
            bool hasData;
            hasData = FetchBy(webappId);
            if (!hasData)
                throw new Exception("TbSysWebapp.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string webappId)
        {
            _webappId = webappId;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbSysWebapp CreateBy(string webappId)
        {
            TbSysWebapp tbl;
            bool hasData;

            tbl = new TbSysWebapp();
            hasData = tbl.FetchBy(webappId);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string webappId)
        {
            TbSysWebapp tbl;
            tbl = new TbSysWebapp();

            tbl.WebappId = webappId;

            tbl.Delete();
        }
        #endregion

        #region 文件和访问组件相关的支持函数
        /// <summary>
        /// 通过DataRow进行赋值
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _webappId            = DbToString(row["webapp_id"]);
            _webappName          = DbToString(row["webapp_name"]);
            _webappDesc          = DbToString(row["webapp_desc"]);
            _currentVersionUid   = DbToString(row["current_version_uid"]);
            _currentVersionNum   = DbToString(row["current_version_num"]);
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
            sLine = "webapp_id\x9" + _webappId;
            writer.WriteLine(sLine);

            sLine = "webapp_name\x9" + _webappName;
            writer.WriteLine(sLine);

            sLine = "webapp_desc\x9" + _webappDesc;
            writer.WriteLine(sLine);

            sLine = "current_version_uid\x9" + _currentVersionUid;
            writer.WriteLine(sLine);

            sLine = "current_version_num\x9" + _currentVersionNum;
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
            sLine  = "webapp_id\twebapp_name\twebapp_desc\t";
            sLine += "current_version_uid\tcurrent_version_num\tlast_patch_uid\t";
            sLine += "last_patch_key_id\tcreate_time\tcreator\t";
            sLine += "modify_time\tremarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL  = "select webapp_id,       webapp_name,     webapp_desc,     \r\n";
            sSQL += "       current_version_uid,current_version_num,last_patch_uid,  \r\n";
            sSQL += "       last_patch_key_id,create_time,     creator,         \r\n";
            sSQL += "       modify_time,     remarks         \r\n";
            sSQL += "  from sbt_sys_webapp";

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
                case "webapp_id":
                    return "varchar";
                case "webapp_name":
                    return "varchar";
                case "webapp_desc":
                    return "varchar";
                case "current_version_uid":
                    return "varchar";
                case "current_version_num":
                    return "varchar";
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
                    throw new Exception("TbSysWebappBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 获取一个字段的CSharp类型
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "webapp_id":
                    return "string";
                case "webapp_name":
                    return "string";
                case "webapp_desc":
                    return "string";
                case "current_version_uid":
                    return "string";
                case "current_version_num":
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
                    throw new Exception("TbSysWebappBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL  = "select " + toFieldName + " from sbt_sys_webapp \r\n";
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
