using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework
{
    /// <summary>
    /// 用户信息安全扩展表(sbt_user_security)的表操作用户类
    /// </summary>
    public class TbUserSecurity : TbUserSecurityBase
    {
        #region 用户可编辑区域

        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }


    /// <summary>
    /// 用户信息安全扩展表(sbt_user_security)的字段名类
    /// </summary>
    public class TbUserSecurityF
    {
        public const string TableName = "sbt_user_security";
        public const string UserUid              = "user_uid";
        public const string LockStatus           = "lock_status";
        public const string LockStatusTime       = "lock_status_time";
        public const string WrongPasswdCnt       = "wrong_passwd_cnt";
        public const string WrongPasswdLastTime  = "wrong_passwd_last_time";
        public const string ModifyTime           = "modify_time";
        public const string Remarks              = "remarks";
    }


    /// <summary>
    /// 用户信息安全扩展表(sbt_user_security)的表操作基类
    /// </summary>
    public class TbUserSecurityBase : Sigbit.Data.TableBase
    {
        #region 私有变量定义
        protected string     _userUid                 = "";
        protected string     _lockStatus              = "";
        protected string     _lockStatusTime          = "";
        protected int        _wrongPasswdCnt         ;
        protected string     _wrongPasswdLastTime     = "";
        protected string     _modifyTime              = "";
        protected string     _remarks                 = "";
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public TbUserSecurityBase()
        {
            ResetData();
        }

        #region 属性定义
        /// <summary>
        /// 用户ID，主键
        /// </summary>
        public string UserUid
        {
            get
            {
                return _userUid;
            }
            set
            {
                _userUid = value;
            }
        }

        /// <summary>
        /// 锁定状态
        /// </summary>
        public string LockStatus
        {
            get
            {
                return _lockStatus;
            }
            set
            {
                _lockStatus = value;
            }
        }

        /// <summary>
        /// 锁定/解锁的时间
        /// </summary>
        public string LockStatusTime
        {
            get
            {
                return _lockStatusTime;
            }
            set
            {
                _lockStatusTime = value;
            }
        }

        /// <summary>
        /// 输错密码的次数
        /// </summary>
        public int WrongPasswdCnt
        {
            get
            {
                return _wrongPasswdCnt;
            }
            set
            {
                _wrongPasswdCnt = value;
            }
        }

        /// <summary>
        /// 上次输错密码的时间
        /// </summary>
        public string WrongPasswdLastTime
        {
            get
            {
                return _wrongPasswdLastTime;
            }
            set
            {
                _wrongPasswdLastTime = value;
            }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string ModifyTime
        {
            get
            {
                return _modifyTime;
            }
            set
            {
                _modifyTime = value;
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
            sb.Append("UserUid: " + this._userUid + "<br>");
            sb.Append("LockStatus: " + this._lockStatus + "<br>");
            sb.Append("LockStatusTime: " + this._lockStatusTime + "<br>");
            sb.Append("WrongPasswdCnt: " + this._wrongPasswdCnt + "<br>");
            sb.Append("WrongPasswdLastTime: " + this._wrongPasswdLastTime + "<br>");
            sb.Append("ModifyTime: " + this._modifyTime + "<br>");
            sb.Append("Remarks: " + this._remarks + "<br>");
            return sb.ToString();
        }

        /// <summary>
        /// 清零本记录的数据
        /// </summary>
        public override void ResetData()
        {
            _userUid = "";
            _lockStatus = "";
            _lockStatusTime = "";
            _wrongPasswdCnt = 0;
            _wrongPasswdLastTime = "";
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
            sSQL  = "select lock_status,         lock_status_time,    wrong_passwd_cnt,     \n";
            sSQL += "       wrong_passwd_last_time,modify_time,         remarks              \n";
            sSQL += "  from sbt_user_security    \n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbUserSecurity.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _lockStatus     = DbToString(row["lock_status"]);
            _lockStatusTime = DbToString(row["lock_status_time"]);
            _wrongPasswdCnt = DbToInt(row["wrong_passwd_cnt"]);
            _wrongPasswdLastTime = DbToString(row["wrong_passwd_last_time"]);
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

            sSQL  = "insert into sbt_user_security \n";
            sSQL += "( user_uid,         lock_status,      \n";
            sSQL += "  lock_status_time, wrong_passwd_cnt, \n";
            sSQL += "  wrong_passwd_last_time, modify_time,      \n";
            sSQL += "  remarks           \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_userUid)         +","+ Quote(_lockStatus)      +",\n";
            sSQL += Quote(_lockStatusTime)  +","+ _wrongPasswdCnt.ToString()+",\n";
            sSQL += Quote(_wrongPasswdLastTime)+","+ Quote(_modifyTime)      +",\n";
            sSQL += Quote(_remarks)         +")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 按主键删除一条数据
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL  = "delete from sbt_user_security \n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbUserSecurity.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL  = "update sbt_user_security set \n";
            sSQL += " user_uid = " + Quote(_userUid) + ",\n";
            sSQL += " lock_status = " + Quote(_lockStatus) + ",\n";
            sSQL += " lock_status_time = " + Quote(_lockStatusTime) + ",\n";
            sSQL += " wrong_passwd_cnt = " + _wrongPasswdCnt.ToString() + ",\n";
            sSQL += " wrong_passwd_last_time = " + Quote(_wrongPasswdLastTime) + ",\n";
            sSQL += " modify_time = " + Quote(_modifyTime) + ",\n";
            sSQL += " remarks = " + Quote(_remarks) + "\n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbUserSecurity.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from sbt_user_security \n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";

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
        public void FetchByE(string userUid)
        {
            bool hasData;
            hasData = FetchBy(userUid);
            if (!hasData)
                throw new Exception("TbUserSecurity.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string userUid)
        {
            _userUid = userUid;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbUserSecurity CreateBy(string userUid)
        {
            TbUserSecurity tbl;
            bool hasData;

            tbl = new TbUserSecurity();
            hasData = tbl.FetchBy(userUid);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string userUid)
        {
            TbUserSecurity tbl;
            tbl = new TbUserSecurity();

            tbl.UserUid = userUid;

            tbl.Delete();
        }

        #endregion

        #region 文件和访问组件相关的支持函数
        /// <summary>
        /// 通过DataRow进行赋值
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _userUid             = DbToString(row["user_uid"]);
            _lockStatus          = DbToString(row["lock_status"]);
            _lockStatusTime      = DbToString(row["lock_status_time"]);
            _wrongPasswdCnt      = DbToInt(row["wrong_passwd_cnt"]);
            _wrongPasswdLastTime = DbToString(row["wrong_passwd_last_time"]);
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
            sLine = "user_uid\x9" + _userUid;
            writer.WriteLine(sLine);

            sLine = "lock_status\x9" + _lockStatus;
            writer.WriteLine(sLine);

            sLine = "lock_status_time\x9" + _lockStatusTime;
            writer.WriteLine(sLine);

            sLine = "wrong_passwd_cnt\x9" + _wrongPasswdCnt.ToString();
            writer.WriteLine(sLine);

            sLine = "wrong_passwd_last_time\x9" + _wrongPasswdLastTime;
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
            sLine  = "user_uid\tlock_status\tlock_status_time\t";
            sLine += "wrong_passwd_cnt\twrong_passwd_last_time\tmodify_time\t";
            sLine += "remarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL  = "select user_uid,        lock_status,     lock_status_time,\n";
            sSQL += "       wrong_passwd_cnt,wrong_passwd_last_time,modify_time,     \n";
            sSQL += "       remarks         \n";
            sSQL += "  from sbt_user_security";

            //======== 4. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. 处理每一行 ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 处理每一列 ========
                for (nCol = 0; nCol < 7; nCol++)
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
                case "user_uid":
                    return "varchar";
                case "lock_status":
                    return "varchar";
                case "lock_status_time":
                    return "varchar";
                case "wrong_passwd_cnt":
                    return "int";
                case "wrong_passwd_last_time":
                    return "varchar";
                case "modify_time":
                    return "varchar";
                case "remarks":
                    return "varchar";
                default:
                    throw new Exception("TbUserSecurityBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 获取一个字段的CSharp类型
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "user_uid":
                    return "string";
                case "lock_status":
                    return "string";
                case "lock_status_time":
                    return "string";
                case "wrong_passwd_cnt":
                    return "int";
                case "wrong_passwd_last_time":
                    return "string";
                case "modify_time":
                    return "string";
                case "remarks":
                    return "string";
                default:
                    throw new Exception("TbUserSecurityBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL  = "select " + toFieldName + " from sbt_user_security \n";
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
