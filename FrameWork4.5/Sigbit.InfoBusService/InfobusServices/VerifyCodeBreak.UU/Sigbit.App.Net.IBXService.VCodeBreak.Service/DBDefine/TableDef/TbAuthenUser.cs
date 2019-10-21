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
    /// 授权用户(vcb_authen_user)的表操作类
    /// </summary>
    public class TbAuthenUser : TbAuthenUserBase
    {
        #region 枚举属性
        #endregion

        #region 用户可编辑区域

        public override void Insert()
        {
            base.Insert();
            QDBVCBreakPools.ResetAuthenUser();
        }

        public override void Delete()
        {
            base.Delete();
            QDBVCBreakPools.ResetAuthenUser();
        }

        public override void Update()
        {
            base.Update();
            QDBVCBreakPools.ResetAuthenUser();
        }

        #endregion
    }


    /// <summary>
    /// 授权用户(vcb_authen_user)的字段名类
    /// </summary>
    public class TbAuthenUserF
    {
        public const string TableName = "vcb_authen_user";
        public const string AuthenUserUid        = "authen_user_uid";
        public const string AuthenUserName       = "authen_user_name";
        public const string AuthenPassword       = "authen_password";
        public const string LimitPerMinute       = "limit_per_minute";
        public const string LimitPerHour         = "limit_per_hour";
        public const string LimitPerDay          = "limit_per_day";
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
    /// 授权用户(vcb_authen_user)的表操作基类
    /// </summary>
    public class TbAuthenUserBase : Sigbit.Data.TableBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TbAuthenUserBase()
        {
            ResetData();
        }

        #region 属性定义
        protected string _authenUserUid = "";
        /// <summary>
        /// 授权用户，主键
        /// </summary>
        public string AuthenUserUid
        {
            get { return _authenUserUid; }
            set { _authenUserUid = value; }
        }

        protected string _authenUserName = "";
        /// <summary>
        /// 用户名
        /// </summary>
        public string AuthenUserName
        {
            get { return _authenUserName; }
            set { _authenUserName = value; }
        }

        protected string _authenPassword = "";
        /// <summary>
        /// 密码
        /// </summary>
        public string AuthenPassword
        {
            get { return _authenPassword; }
            set { _authenPassword = value; }
        }

        protected int _limitPerMinute;
        /// <summary>
        /// 每分钟的使用限制
        /// </summary>
        public int LimitPerMinute
        {
            get { return _limitPerMinute; }
            set { _limitPerMinute = value; }
        }

        protected int _limitPerHour;
        /// <summary>
        /// 每小时的使用限制
        /// </summary>
        public int LimitPerHour
        {
            get { return _limitPerHour; }
            set { _limitPerHour = value; }
        }

        protected int _limitPerDay;
        /// <summary>
        /// 每天的使用限制
        /// </summary>
        public int LimitPerDay
        {
            get { return _limitPerDay; }
            set { _limitPerDay = value; }
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
            sb.Append("AuthenUserUid: " + this._authenUserUid + "<br>");
            sb.Append("AuthenUserName: " + this._authenUserName + "<br>");
            sb.Append("AuthenPassword: " + this._authenPassword + "<br>");
            sb.Append("LimitPerMinute: " + this._limitPerMinute + "<br>");
            sb.Append("LimitPerHour: " + this._limitPerHour + "<br>");
            sb.Append("LimitPerDay: " + this._limitPerDay + "<br>");
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
            _authenUserUid = "";
            _authenUserName = "";
            _authenPassword = "";
            _limitPerMinute = 0;
            _limitPerHour = 0;
            _limitPerDay = 0;
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
            sSQL  = "select authen_user_name,    authen_password,     limit_per_minute,     \r\n";
            sSQL += "       limit_per_hour,      limit_per_day,       ex_info_01,           \r\n";
            sSQL += "       ex_info_02,          ex_info_03,          ex_info_04,           \r\n";
            sSQL += "       create_time,         creator,             modify_time,          \r\n";
            sSQL += "       remarks              \r\n";
            sSQL += "  from vcb_authen_user    \r\n";
            sSQL += "  where authen_user_uid = " + Quote(_authenUserUid) + "\r\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbAuthenUser.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _authenUserName = DbToString(row["authen_user_name"]);
            _authenPassword = DbToString(row["authen_password"]);
            _limitPerMinute = DbToInt(row["limit_per_minute"]);
            _limitPerHour   = DbToInt(row["limit_per_hour"]);
            _limitPerDay    = DbToInt(row["limit_per_day"]);
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

            sSQL  = "insert into vcb_authen_user \r\n";
            sSQL += "( authen_user_uid,  authen_user_name, \r\n";
            sSQL += "  authen_password,  limit_per_minute, \r\n";
            sSQL += "  limit_per_hour,   limit_per_day,    \r\n";
            sSQL += "  ex_info_01,       ex_info_02,       \r\n";
            sSQL += "  ex_info_03,       ex_info_04,       \r\n";
            sSQL += "  create_time,      creator,          \r\n";
            sSQL += "  modify_time,      remarks           \r\n";
            sSQL += ") values (          \r\n";
            sSQL += Quote(_authenUserUid)   +","+ Quote(_authenUserName)  +",\r\n";
            sSQL += Quote(_authenPassword)  +","+ _limitPerMinute.ToString()+",\r\n";
            sSQL += _limitPerHour.ToString()+","+ _limitPerDay.ToString() +",\r\n";
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

            sSQL  = "delete from vcb_authen_user \r\n";
            sSQL += "  where authen_user_uid = " + Quote(_authenUserUid) + "\r\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbAuthenUser.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL  = "update vcb_authen_user set \r\n";
            sSQL += " authen_user_uid = " + Quote(_authenUserUid) + ",\r\n";
            sSQL += " authen_user_name = " + Quote(_authenUserName) + ",\r\n";
            sSQL += " authen_password = " + Quote(_authenPassword) + ",\r\n";
            sSQL += " limit_per_minute = " + _limitPerMinute.ToString() + ",\r\n";
            sSQL += " limit_per_hour = " + _limitPerHour.ToString() + ",\r\n";
            sSQL += " limit_per_day = " + _limitPerDay.ToString() + ",\r\n";
            sSQL += " ex_info_01 = " + Quote(_exInfo01) + ",\r\n";
            sSQL += " ex_info_02 = " + Quote(_exInfo02) + ",\r\n";
            sSQL += " ex_info_03 = " + Quote(_exInfo03) + ",\r\n";
            sSQL += " ex_info_04 = " + Quote(_exInfo04) + ",\r\n";
            sSQL += " create_time = " + Quote(_createTime) + ",\r\n";
            sSQL += " creator = " + Quote(_creator) + ",\r\n";
            sSQL += " modify_time = " + Quote(_modifyTime) + ",\r\n";
            sSQL += " remarks = " + Quote(_remarks) + "\r\n";
            sSQL += "  where authen_user_uid = " + Quote(_authenUserUid) + "\r\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbAuthenUser.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from vcb_authen_user \r\n";
            sSQL += "  where authen_user_uid = " + Quote(_authenUserUid) + "\r\n";

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
        public void FetchByE(string authenUserUid)
        {
            bool hasData;
            hasData = FetchBy(authenUserUid);
            if (!hasData)
                throw new Exception("TbAuthenUser.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string authenUserUid)
        {
            _authenUserUid = authenUserUid;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbAuthenUser CreateBy(string authenUserUid)
        {
            TbAuthenUser tbl;
            bool hasData;

            tbl = new TbAuthenUser();
            hasData = tbl.FetchBy(authenUserUid);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string authenUserUid)
        {
            TbAuthenUser tbl;
            tbl = new TbAuthenUser();

            tbl.AuthenUserUid = authenUserUid;

            tbl.Delete();
        }
        #endregion

        #region 文件和访问组件相关的支持函数
        /// <summary>
        /// 通过DataRow进行赋值
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _authenUserUid       = DbToString(row["authen_user_uid"]);
            _authenUserName      = DbToString(row["authen_user_name"]);
            _authenPassword      = DbToString(row["authen_password"]);
            _limitPerMinute      = DbToInt(row["limit_per_minute"]);
            _limitPerHour        = DbToInt(row["limit_per_hour"]);
            _limitPerDay         = DbToInt(row["limit_per_day"]);
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
            sLine = "authen_user_uid\x9" + _authenUserUid;
            writer.WriteLine(sLine);

            sLine = "authen_user_name\x9" + _authenUserName;
            writer.WriteLine(sLine);

            sLine = "authen_password\x9" + _authenPassword;
            writer.WriteLine(sLine);

            sLine = "limit_per_minute\x9" + _limitPerMinute.ToString();
            writer.WriteLine(sLine);

            sLine = "limit_per_hour\x9" + _limitPerHour.ToString();
            writer.WriteLine(sLine);

            sLine = "limit_per_day\x9" + _limitPerDay.ToString();
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
            sLine  = "authen_user_uid\tauthen_user_name\tauthen_password\t";
            sLine += "limit_per_minute\tlimit_per_hour\tlimit_per_day\t";
            sLine += "ex_info_01\tex_info_02\tex_info_03\t";
            sLine += "ex_info_04\tcreate_time\tcreator\t";
            sLine += "modify_time\tremarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL  = "select authen_user_uid, authen_user_name,authen_password, \r\n";
            sSQL += "       limit_per_minute,limit_per_hour,  limit_per_day,   \r\n";
            sSQL += "       ex_info_01,      ex_info_02,      ex_info_03,      \r\n";
            sSQL += "       ex_info_04,      create_time,     creator,         \r\n";
            sSQL += "       modify_time,     remarks         \r\n";
            sSQL += "  from vcb_authen_user";

            //======== 4. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. 处理每一行 ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 处理每一列 ========
                for (nCol = 0; nCol < 14; nCol++)
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
                case "authen_user_uid":
                    return "varchar";
                case "authen_user_name":
                    return "varchar";
                case "authen_password":
                    return "varchar";
                case "limit_per_minute":
                    return "int";
                case "limit_per_hour":
                    return "int";
                case "limit_per_day":
                    return "int";
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
                    throw new Exception("TbAuthenUserBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 获取一个字段的CSharp类型
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "authen_user_uid":
                    return "string";
                case "authen_user_name":
                    return "string";
                case "authen_password":
                    return "string";
                case "limit_per_minute":
                    return "int";
                case "limit_per_hour":
                    return "int";
                case "limit_per_day":
                    return "int";
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
                    throw new Exception("TbAuthenUserBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL  = "select " + toFieldName + " from vcb_authen_user \r\n";
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
