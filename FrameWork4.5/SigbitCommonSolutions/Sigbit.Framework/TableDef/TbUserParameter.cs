using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework
{
    /// <summary>
    /// (sbt_user_parameter)的表操作用户类
    /// </summary>
    public class TbUserParameter : TbUserParameterBase
    {
        #region 用户可编辑区域

        /// <summary>
        /// 得到系统配置的参数值
        /// </summary>
        /// <param name="sUserUid">用户编号</param>
        /// <param name="sApplicationName">应用程序名</param>
        /// <param name="sParameterClass">参数类型</param>
        /// <param name="sParameterName">参数名</param>
        /// <param name="sDefaultValue">缺省值</param>
        /// <returns>参数值</returns>
        public static string GetParameterValue(string sUserUid, string sApplicationName, string sParameterClass,
                string sParameterName, string sDefaultValue)
        {
            TbUserParameter tblParam = new TbUserParameter();
            tblParam.UserUid = sUserUid;
            tblParam.ApplicationName = sApplicationName;
            tblParam.ParameterClass = sParameterClass;
            tblParam.ParameterName = sParameterName;
            if (!tblParam.Fetch(true))
                return sDefaultValue;

            return tblParam.ParameterValue;
        }

        /// <summary>
        /// 得到系统配置的参数值
        /// </summary>
        /// <param name="sUserUid">用户编号</param>
        /// <param name="sApplicationName">应用程序名</param>
        /// <param name="sParameterClass">参数类型</param>
        /// <param name="sParameterName">参数名</param>
        /// <returns>参数值</returns>
        public static string GetParameterValue(string sUserUid, string sApplicationName, string sParameterClass,
                string sParameterName)
        {
            return GetParameterValue(sUserUid, sApplicationName, sParameterClass, sParameterName, "");
        }

        /// <summary>
        /// 得到系统配置的参数整型值
        /// </summary>
        /// <param name="sUserUid">用户编号</param>
        /// <param name="sApplicationName">应用程序名</param>
        /// <param name="sParameterClass">参数类型</param>
        /// <param name="sParameterName">参数名</param>
        /// <param name="nDefaultValue">缺省整型值</param>
        /// <returns>参数值</returns>
        public static int GetParameterValueInt(string sUserUid, string sApplicationName, string sParameterClass,
                string sParameterName, int nDefaultValue)
        {
            string sParamValue = GetParameterValue(sUserUid, sApplicationName, sParameterClass,
                    sParameterName, nDefaultValue.ToString());
            return ConvertUtil.ToInt(sParamValue, nDefaultValue);
        }

        /// <summary>
        /// 得到系统配置的参数整型值
        /// </summary>
        /// <param name="sUserUid">用户编号</param>
        /// <param name="sApplicationName">应用程序名</param>
        /// <param name="sParameterClass">参数类型</param>
        /// <param name="sParameterName">参数名</param>
        /// <returns>参数值</returns>
        public static int GetParameterValueInt(string sUserUid, string sApplicationName, string sParameterClass,
                string sParameterName)
        {
            return GetParameterValueInt(sUserUid, sApplicationName, sParameterClass, sParameterName, 0);
        }

        /// <summary>
        /// 设置系统配置参数
        /// </summary>
        /// <param name="sUserUid">用户编号</param>
        /// <param name="sApplicationName">应用程序名</param>
        /// <param name="sParameterClass">参数类型</param>
        /// <param name="sParameterName">参数名</param>
        /// <param name="sValue">参数值</param>
        public static void SetParameterValue(string sUserUid, string sApplicationName, string sParameterClass,
                string sParameterName, string sValue)
        {
            TbUserParameter tblParam = new TbUserParameter();
            tblParam.UserUid = sUserUid;
            tblParam.ApplicationName = sApplicationName;
            tblParam.ParameterClass = sParameterClass;
            tblParam.ParameterName = sParameterName;
            if (tblParam.Fetch(true))
            {
                tblParam.ParameterValue = sValue;
                tblParam.Update();
            }
            else
            {
                tblParam.ParameterValue = sValue;
                tblParam.Insert();
            }
        }

        /// <summary>
        /// 设置系统配置参数（整型值）
        /// </summary>
        /// <param name="sUserUid">用户编号</param>
        /// <param name="sApplicationName">应用程序名</param>
        /// <param name="sParameterClass">参数类型</param>
        /// <param name="sParameterName">参数名</param>
        /// <param name="nValue">参数值</param>
        public static void SetParameterValueInt(string sUserUid, string sApplicationName, string sParameterClass,
                string sParameterName, int nValue)
        {
            SetParameterValue(sUserUid, sApplicationName, sParameterClass,
                    sParameterName, nValue.ToString());
        }


        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }


    /// <summary>
    /// (sbt_user_parameter)的字段名类
    /// </summary>
    public class TbUserParameterF
    {
        /// <summary>
        /// 表名
        /// </summary>
        public const string TableName = "sbt_user_parameter";
        /// <summary>
        /// 用户编号
        /// </summary>
        public const string UserUid = "user_uid";
        /// <summary>
        /// 应用名称
        /// </summary>
        public const string ApplicationName = "application_name";
        /// <summary>
        /// 参数分类
        /// </summary>
        public const string ParameterClass = "parameter_class";
        /// <summary>
        /// 参数名称
        /// </summary>
        public const string ParameterName = "parameter_name";
        /// <summary>
        /// 参数值
        /// </summary>
        public const string ParameterValue = "parameter_value";
        /// <summary>
        /// 备注
        /// </summary>
        public const string Remarks = "remarks";
    }


    /// <summary>
    /// (sbt_user_parameter)的表操作基类
    /// </summary>
    public class TbUserParameterBase : Sigbit.Data.TableBase
    {
        #region 私有变量定义
        protected string _userUid = "";
        protected string _applicationName = "";
        protected string _parameterClass = "";
        protected string _parameterName = "";
        protected string _parameterValue = "";
        protected string _remarks = "";
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public TbUserParameterBase()
        {
            ResetData();
        }

        #region 属性定义
        /// <summary>
        /// 用户标识编码，主键
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
        /// 应用程序名，主键
        /// </summary>
        public string ApplicationName
        {
            get
            {
                return _applicationName;
            }
            set
            {
                _applicationName = value;
            }
        }

        /// <summary>
        /// 参数类型，主键
        /// </summary>
        public string ParameterClass
        {
            get
            {
                return _parameterClass;
            }
            set
            {
                _parameterClass = value;
            }
        }

        /// <summary>
        /// 参数名，主键
        /// </summary>
        public string ParameterName
        {
            get
            {
                return _parameterName;
            }
            set
            {
                _parameterName = value;
            }
        }

        /// <summary>
        /// 参数值
        /// </summary>
        public string ParameterValue
        {
            get
            {
                return _parameterValue;
            }
            set
            {
                _parameterValue = value;
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
            sb.Append("ApplicationName: " + this._applicationName + "<br>");
            sb.Append("ParameterClass: " + this._parameterClass + "<br>");
            sb.Append("ParameterName: " + this._parameterName + "<br>");
            sb.Append("ParameterValue: " + this._parameterValue + "<br>");
            sb.Append("Remarks: " + this._remarks + "<br>");
            return sb.ToString();
        }

        /// <summary>
        /// 清零本记录的数据
        /// </summary>
        public override void ResetData()
        {
            _userUid = "";
            _applicationName = "";
            _parameterClass = "";
            _parameterName = "";
            _parameterValue = "";
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
            sSQL = "select parameter_value,     remarks              \n";
            sSQL += "  from sbt_user_parameter    \n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";
            sSQL += "    and application_name = " + Quote(_applicationName) + "\n";
            sSQL += "    and parameter_class = " + Quote(_parameterClass) + "\n";
            sSQL += "    and parameter_name = " + Quote(_parameterName) + "\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbUserParameter.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _parameterValue = DbToString(row["parameter_value"]);
            _remarks = DbToString(row["remarks"]);
            return true;
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        public override void Insert()
        {
            string sSQL;

            sSQL = "insert into sbt_user_parameter \n";
            sSQL += "( user_uid,         application_name, \n";
            sSQL += "  parameter_class,  parameter_name,   \n";
            sSQL += "  parameter_value,  remarks           \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_userUid) + "," + Quote(_applicationName) + ",\n";
            sSQL += Quote(_parameterClass) + "," + Quote(_parameterName) + ",\n";
            sSQL += Quote(_parameterValue) + "," + Quote(_remarks) + ")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 按主键删除一条数据
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL = "delete from sbt_user_parameter \n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";
            sSQL += "    and application_name = " + Quote(_applicationName) + "\n";
            sSQL += "    and parameter_class = " + Quote(_parameterClass) + "\n";
            sSQL += "    and parameter_name = " + Quote(_parameterName) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbUserParameter.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL = "update sbt_user_parameter set \n";
            sSQL += " user_uid = " + Quote(_userUid) + ",\n";
            sSQL += " application_name = " + Quote(_applicationName) + ",\n";
            sSQL += " parameter_class = " + Quote(_parameterClass) + ",\n";
            sSQL += " parameter_name = " + Quote(_parameterName) + ",\n";
            sSQL += " parameter_value = " + Quote(_parameterValue) + ",\n";
            sSQL += " remarks = " + Quote(_remarks) + "\n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";
            sSQL += "    and application_name = " + Quote(_applicationName) + "\n";
            sSQL += "    and parameter_class = " + Quote(_parameterClass) + "\n";
            sSQL += "    and parameter_name = " + Quote(_parameterName) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected > 1)
                throw new Exception("TbUserParameter.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from sbt_user_parameter \n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";
            sSQL += "    and application_name = " + Quote(_applicationName) + "\n";
            sSQL += "    and parameter_class = " + Quote(_parameterClass) + "\n";
            sSQL += "    and parameter_name = " + Quote(_parameterName) + "\n";

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
        public void FetchByE(string userUid, string applicationName, string parameterClass, string parameterName)
        {
            bool hasData;
            hasData = FetchBy(userUid, applicationName, parameterClass, parameterName);
            if (!hasData)
                throw new Exception("TbUserParameter.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string userUid, string applicationName, string parameterClass, string parameterName)
        {
            _userUid = userUid;
            _applicationName = applicationName;
            _parameterClass = parameterClass;
            _parameterName = parameterName;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbUserParameter CreateBy(string userUid, string applicationName, string parameterClass, string parameterName)
        {
            TbUserParameter tbl;
            bool hasData;

            tbl = new TbUserParameter();
            hasData = tbl.FetchBy(userUid, applicationName, parameterClass, parameterName);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string userUid, string applicationName, string parameterClass, string parameterName)
        {
            TbUserParameter tbl;
            tbl = new TbUserParameter();

            tbl.UserUid = userUid;
            tbl.ApplicationName = applicationName;
            tbl.ParameterClass = parameterClass;
            tbl.ParameterName = parameterName;

            tbl.Delete();
        }

        #endregion

        #region 文件和访问组件相关的支持函数
        /// <summary>
        /// 通过DataRow进行赋值
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _userUid = DbToString(row["user_uid"]);
            _applicationName = DbToString(row["application_name"]);
            _parameterClass = DbToString(row["parameter_class"]);
            _parameterName = DbToString(row["parameter_name"]);
            _parameterValue = DbToString(row["parameter_value"]);
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
            sLine = "user_uid\x9" + _userUid;
            writer.WriteLine(sLine);

            sLine = "application_name\x9" + _applicationName;
            writer.WriteLine(sLine);

            sLine = "parameter_class\x9" + _parameterClass;
            writer.WriteLine(sLine);

            sLine = "parameter_name\x9" + _parameterName;
            writer.WriteLine(sLine);

            sLine = "parameter_value\x9" + _parameterValue;
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
            sLine = "user_uid\tapplication_name\tparameter_class\t";
            sLine += "parameter_name\tparameter_value\tremarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL = "select user_uid,        application_name,parameter_class, \n";
            sSQL += "       parameter_name,  parameter_value, remarks         \n";
            sSQL += "  from sbt_user_parameter";

            //======== 4. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. 处理每一行 ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 处理每一列 ========
                for (nCol = 0; nCol < 6; nCol++)
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
                case "application_name":
                    return "varchar";
                case "parameter_class":
                    return "varchar";
                case "parameter_name":
                    return "varchar";
                case "parameter_value":
                    return "varchar";
                case "remarks":
                    return "varchar";
                default:
                    throw new Exception("TbUserParameterBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
                case "application_name":
                    return "string";
                case "parameter_class":
                    return "string";
                case "parameter_name":
                    return "string";
                case "parameter_value":
                    return "string";
                case "remarks":
                    return "string";
                default:
                    throw new Exception("TbUserParameterBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL = "select " + toFieldName + " from sbt_user_parameter \n";
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
