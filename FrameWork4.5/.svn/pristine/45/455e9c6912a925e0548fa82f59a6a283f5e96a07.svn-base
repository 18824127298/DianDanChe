using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework
{
    /// <summary>
    /// 偏好设置(sbt_sys_preference_setting)的表操作用户类
    /// </summary>
    public class TbSysPreferenceSetting : TbSysPreferenceSettingBase
    {
        #region 用户可编辑区域

        public static string GetDefaultPreference(string sPreferenceClass)
        {
            string sSQL = "select preference_code from sbt_sys_preference_setting";
            sSQL += " where preference_class = '" + sPreferenceClass + "' ";
            sSQL += " and is_default = 'Y'";
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);
            if (ds.Tables[0].Rows.Count != 1)
                throw new Exception("TbSysPreferenceSetting.GetDefaultPreference():"
                        + "找不到缺省的偏好配置 - " + sPreferenceClass);
            string sRet = (string)ds.Tables[0].Rows[0][0];
            return sRet;
        }
        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }


    /// <summary>
    /// 偏好设置(sbt_sys_preference_setting)的字段名类
    /// </summary>
    public class TbSysPreferenceSettingF
    {
        public const string TableName = "sbt_sys_preference_setting";
        public const string PreferenceClass = "preference_class";
        public const string PreferenceCode = "preference_code";
        public const string PreferenceName = "preference_name";
        public const string IsDefault = "is_default";
        public const string PreferenceData = "preference_data";
        public const string Remarks = "remarks";
    }


    /// <summary>
    /// 偏好设置(sbt_sys_preference_setting)的表操作基类
    /// </summary>
    public class TbSysPreferenceSettingBase : Sigbit.Data.TableBase
    {
        #region 私有变量定义
        protected string _preferenceClass = "";
        protected string _preferenceCode = "";
        protected string _preferenceName = "";
        protected string _isDefault = "";
        protected string _preferenceData = "";
        protected string _remarks = "";
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public TbSysPreferenceSettingBase()
        {
            ResetData();
        }

        #region 属性定义
        /// <summary>
        /// 偏好的类型，主键
        /// </summary>
        public string PreferenceClass
        {
            get
            {
                return _preferenceClass;
            }
            set
            {
                _preferenceClass = value;
            }
        }

        /// <summary>
        /// 偏好的取值代码，主键
        /// </summary>
        public string PreferenceCode
        {
            get
            {
                return _preferenceCode;
            }
            set
            {
                _preferenceCode = value;
            }
        }

        /// <summary>
        /// 偏好的名称
        /// </summary>
        public string PreferenceName
        {
            get
            {
                return _preferenceName;
            }
            set
            {
                _preferenceName = value;
            }
        }

        /// <summary>
        /// 是否缺省偏好
        /// </summary>
        public string IsDefault
        {
            get
            {
                return _isDefault;
            }
            set
            {
                _isDefault = value;
            }
        }

        /// <summary>
        /// 可能的相关数据
        /// </summary>
        public string PreferenceData
        {
            get
            {
                return _preferenceData;
            }
            set
            {
                _preferenceData = value;
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
            sb.Append("PreferenceClass: " + this._preferenceClass + "<br>");
            sb.Append("PreferenceCode: " + this._preferenceCode + "<br>");
            sb.Append("PreferenceName: " + this._preferenceName + "<br>");
            sb.Append("IsDefault: " + this._isDefault + "<br>");
            sb.Append("PreferenceData: " + this._preferenceData + "<br>");
            sb.Append("Remarks: " + this._remarks + "<br>");
            return sb.ToString();
        }

        /// <summary>
        /// 清零本记录的数据
        /// </summary>
        public override void ResetData()
        {
            _preferenceClass = "";
            _preferenceCode = "";
            _preferenceName = "";
            _isDefault = "";
            _preferenceData = "";
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
            sSQL = "select preference_name,     is_default,          preference_data,      \n";
            sSQL += "       remarks              \n";
            sSQL += "  from sbt_sys_preference_setting    \n";
            sSQL += "  where preference_class = " + Quote(_preferenceClass) + "\n";
            sSQL += "    and preference_code = " + Quote(_preferenceCode) + "\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbSysPreferenceSetting.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _preferenceName = DbToString(row["preference_name"]);
            _isDefault = DbToString(row["is_default"]);
            _preferenceData = DbToString(row["preference_data"]);
            _remarks = DbToString(row["remarks"]);
            return true;
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        public override void Insert()
        {
            string sSQL;

            sSQL = "insert into sbt_sys_preference_setting \n";
            sSQL += "( preference_class, preference_code,  \n";
            sSQL += "  preference_name,  is_default,       \n";
            sSQL += "  preference_data,  remarks           \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_preferenceClass) + "," + Quote(_preferenceCode) + ",\n";
            sSQL += Quote(_preferenceName) + "," + Quote(_isDefault) + ",\n";
            sSQL += Quote(_preferenceData) + "," + Quote(_remarks) + ")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 按主键删除一条数据
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL = "delete from sbt_sys_preference_setting \n";
            sSQL += "  where preference_class = " + Quote(_preferenceClass) + "\n";
            sSQL += "    and preference_code = " + Quote(_preferenceCode) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysPreferenceSetting.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL = "update sbt_sys_preference_setting set \n";
            sSQL += " preference_class = " + Quote(_preferenceClass) + ",\n";
            sSQL += " preference_code = " + Quote(_preferenceCode) + ",\n";
            sSQL += " preference_name = " + Quote(_preferenceName) + ",\n";
            sSQL += " is_default = " + Quote(_isDefault) + ",\n";
            sSQL += " preference_data = " + Quote(_preferenceData) + ",\n";
            sSQL += " remarks = " + Quote(_remarks) + "\n";
            sSQL += "  where preference_class = " + Quote(_preferenceClass) + "\n";
            sSQL += "    and preference_code = " + Quote(_preferenceCode) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysPreferenceSetting.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from sbt_sys_preference_setting \n";
            sSQL += "  where preference_class = " + Quote(_preferenceClass) + "\n";
            sSQL += "    and preference_code = " + Quote(_preferenceCode) + "\n";

            //======= 2. 运行SQL语句 ========
            nRecordCount = (int)DataHelper.Instance.ExecuteScalar(sSQL);
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
        public void FetchByE(string preferenceClass, string preferenceCode)
        {
            bool hasData;
            hasData = FetchBy(preferenceClass, preferenceCode);
            if (!hasData)
                throw new Exception("TbSysPreferenceSetting.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string preferenceClass, string preferenceCode)
        {
            _preferenceClass = preferenceClass;
            _preferenceCode = preferenceCode;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbSysPreferenceSetting CreateBy(string preferenceClass, string preferenceCode)
        {
            TbSysPreferenceSetting tbl;
            bool hasData;

            tbl = new TbSysPreferenceSetting();
            hasData = tbl.FetchBy(preferenceClass, preferenceCode);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string preferenceClass, string preferenceCode)
        {
            TbSysPreferenceSetting tbl;
            tbl = new TbSysPreferenceSetting();

            tbl.PreferenceClass = preferenceClass;
            tbl.PreferenceCode = preferenceCode;

            tbl.Delete();
        }

        #endregion

        #region 文件和访问组件相关的支持函数
        /// <summary>
        /// 通过DataRow进行赋值
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _preferenceClass = DbToString(row["preference_class"]);
            _preferenceCode = DbToString(row["preference_code"]);
            _preferenceName = DbToString(row["preference_name"]);
            _isDefault = DbToString(row["is_default"]);
            _preferenceData = DbToString(row["preference_data"]);
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
            sLine = "preference_class\x9" + _preferenceClass;
            writer.WriteLine(sLine);

            sLine = "preference_code\x9" + _preferenceCode;
            writer.WriteLine(sLine);

            sLine = "preference_name\x9" + _preferenceName;
            writer.WriteLine(sLine);

            sLine = "is_default\x9" + _isDefault;
            writer.WriteLine(sLine);

            sLine = "preference_data\x9" + _preferenceData;
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
            sLine = "preference_class\tpreference_code\tpreference_name\t";
            sLine += "is_default\tpreference_data\tremarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL = "select preference_class,preference_code, preference_name, \n";
            sSQL += "       is_default,      preference_data, remarks         \n";
            sSQL += "  from sbt_sys_preference_setting";

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
                case "preference_class":
                    return "varchar";
                case "preference_code":
                    return "varchar";
                case "preference_name":
                    return "varchar";
                case "is_default":
                    return "varchar";
                case "preference_data":
                    return "varchar";
                case "remarks":
                    return "varchar";
                default:
                    throw new Exception("TbSysPreferenceSettingBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 获取一个字段的CSharp类型
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "preference_class":
                    return "string";
                case "preference_code":
                    return "string";
                case "preference_name":
                    return "string";
                case "is_default":
                    return "string";
                case "preference_data":
                    return "string";
                case "remarks":
                    return "string";
                default:
                    throw new Exception("TbSysPreferenceSettingBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL = "select " + toFieldName + "from sbt_sys_preference_setting \n";
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
