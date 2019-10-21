using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework
{
    /// <summary>
    /// 后台处理类型配置(sbt_sys_daemon_type)的表操作用户类
    /// </summary>
    public class TbSysDaemonType : TbSysDaemonTypeBase
    {
        #region 用户可编辑区域

        public static DataSet GetDataSetOfAllValid()
        {
            string sSQL = "select * from sbt_sys_daemon_type where enabled='Y'";
            return DataHelper.Instance.ExecuteDataSet(sSQL);
        }

        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }


    /// <summary>
    /// 后台处理类型配置(sbt_sys_daemon_type)的字段名类
    /// </summary>
    public class TbSysDaemonTypeF
    {
        public const string TableName = "sbt_sys_daemon_type";
        public const string DaemonType = "daemon_type";
        public const string DaemonTypeName = "daemon_type_name";
        public const string DaemonComponent = "daemon_component";
        public const string DaemonClass = "daemon_class";
        public const string DaemonPriority = "daemon_priority";
        public const string IntervalType = "interval_type";
        public const string IntervalValue = "interval_value";
        public const string ActiveIntervalValue = "active_interval_value";
        public const string Enabled = "enabled";
        public const string BatchSize = "batch_size";
    }


    /// <summary>
    /// 后台处理类型配置(sbt_sys_daemon_type)的表操作基类
    /// </summary>
    public class TbSysDaemonTypeBase : Sigbit.Data.TableBase
    {
        #region 私有变量定义
        protected string _daemonType = "";
        protected string _daemonTypeName = "";
        protected string _daemonComponent = "";
        protected string _daemonClass = "";
        protected int _daemonPriority;
        protected string _intervalType = "";
        protected int _intervalValue;
        protected int _activeIntervalValue;
        protected string _enabled = "";
        protected int _batchSize;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public TbSysDaemonTypeBase()
        {
            ResetData();
        }

        #region 属性定义
        /// <summary>
        /// 后台处理的类型，主键
        /// </summary>
        public string DaemonType
        {
            get
            {
                return _daemonType;
            }
            set
            {
                _daemonType = value;
            }
        }

        /// <summary>
        /// 后台处理类型的名称
        /// </summary>
        public string DaemonTypeName
        {
            get
            {
                return _daemonTypeName;
            }
            set
            {
                _daemonTypeName = value;
            }
        }

        /// <summary>
        /// 实现组件的名称
        /// </summary>
        public string DaemonComponent
        {
            get
            {
                return _daemonComponent;
            }
            set
            {
                _daemonComponent = value;
            }
        }

        /// <summary>
        /// 后台处理的类名
        /// </summary>
        public string DaemonClass
        {
            get
            {
                return _daemonClass;
            }
            set
            {
                _daemonClass = value;
            }
        }

        /// <summary>
        /// 运行优先级，从1至9
        /// </summary>
        public int DaemonPriority
        {
            get
            {
                return _daemonPriority;
            }
            set
            {
                _daemonPriority = value;
            }
        }

        /// <summary>
        /// 后台处理的时间间隔类型
        /// </summary>
        public string IntervalType
        {
            get
            {
                return _intervalType;
            }
            set
            {
                _intervalType = value;
            }
        }

        /// <summary>
        /// 具体的间隔值
        /// </summary>
        public int IntervalValue
        {
            get
            {
                return _intervalValue;
            }
            set
            {
                _intervalValue = value;
            }
        }

        /// <summary>
        /// 当激活时的时间间隔
        /// </summary>
        public int ActiveIntervalValue
        {
            get
            {
                return _activeIntervalValue;
            }
            set
            {
                _activeIntervalValue = value;
            }
        }

        /// <summary>
        /// 是否有效
        /// </summary>
        public string Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
            }
        }

        /// <summary>
        /// 配置的批次大小（可不配）
        /// </summary>
        public int BatchSize
        {
            get
            {
                return _batchSize;
            }
            set
            {
                _batchSize = value;
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
            sb.Append("DaemonType: " + this._daemonType + "<br>");
            sb.Append("DaemonTypeName: " + this._daemonTypeName + "<br>");
            sb.Append("DaemonComponent: " + this._daemonComponent + "<br>");
            sb.Append("DaemonClass: " + this._daemonClass + "<br>");
            sb.Append("DaemonPriority: " + this._daemonPriority + "<br>");
            sb.Append("IntervalType: " + this._intervalType + "<br>");
            sb.Append("IntervalValue: " + this._intervalValue + "<br>");
            sb.Append("ActiveIntervalValue: " + this._activeIntervalValue + "<br>");
            sb.Append("Enabled: " + this._enabled + "<br>");
            sb.Append("BatchSize: " + this._batchSize + "<br>");
            return sb.ToString();
        }

        /// <summary>
        /// 清零本记录的数据
        /// </summary>
        public override void ResetData()
        {
            _daemonType = "";
            _daemonTypeName = "";
            _daemonComponent = "";
            _daemonClass = "";
            _daemonPriority = 0;
            _intervalType = "";
            _intervalValue = 0;
            _activeIntervalValue = 0;
            _enabled = "";
            _batchSize = 0;
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
            sSQL = "select daemon_type_name,    daemon_component,    daemon_class,         \n";
            sSQL += "       daemon_priority,     interval_type,       interval_value,       \n";
            sSQL += "       active_interval_value,enabled,             batch_size           \n";
            sSQL += "  from sbt_sys_daemon_type    \n";
            sSQL += "  where daemon_type = " + Quote(_daemonType) + "\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbSysDaemonType.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _daemonTypeName = DbToString(row["daemon_type_name"]); DbToInt(row["daemon_type_name"]);
            _daemonComponent = DbToString(row["daemon_component"]); DbToInt(row["daemon_component"]);
            _daemonClass = DbToString(row["daemon_class"]); DbToInt(row["daemon_class"]);
            _daemonPriority = DbToInt(row["daemon_priority"]);
            _intervalType = DbToString(row["interval_type"]); DbToInt(row["interval_type"]);
            _intervalValue = DbToInt(row["interval_value"]);
            _activeIntervalValue = DbToInt(row["active_interval_value"]);
            _enabled = DbToString(row["enabled"]); DbToInt(row["enabled"]);
            _batchSize = DbToInt(row["batch_size"]);
            return true;
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        public override void Insert()
        {
            string sSQL;

            sSQL = "insert into sbt_sys_daemon_type \n";
            sSQL += "( daemon_type,      daemon_type_name, \n";
            sSQL += "  daemon_component, daemon_class,     \n";
            sSQL += "  daemon_priority,  interval_type,    \n";
            sSQL += "  interval_value,   active_interval_value, \n";
            sSQL += "  enabled,          batch_size        \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_daemonType) + "," + Quote(_daemonTypeName) + ",\n";
            sSQL += Quote(_daemonComponent) + "," + Quote(_daemonClass) + ",\n";
            sSQL += _daemonPriority.ToString() + "," + Quote(_intervalType) + ",\n";
            sSQL += _intervalValue.ToString() + "," + _activeIntervalValue.ToString() + ",\n";
            sSQL += Quote(_enabled) + "," + _batchSize.ToString() + ")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 按主键删除一条数据
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL = "delete from sbt_sys_daemon_type \n";
            sSQL += "  where daemon_type = " + Quote(_daemonType) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysDaemonType.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL = "update sbt_sys_daemon_type set \n";
            sSQL += " daemon_type = " + Quote(_daemonType) + ",\n";
            sSQL += " daemon_type_name = " + Quote(_daemonTypeName) + ",\n";
            sSQL += " daemon_component = " + Quote(_daemonComponent) + ",\n";
            sSQL += " daemon_class = " + Quote(_daemonClass) + ",\n";
            sSQL += " daemon_priority = " + _daemonPriority.ToString() + ",\n";
            sSQL += " interval_type = " + Quote(_intervalType) + ",\n";
            sSQL += " interval_value = " + _intervalValue.ToString() + ",\n";
            sSQL += " active_interval_value = " + _activeIntervalValue.ToString() + ",\n";
            sSQL += " enabled = " + Quote(_enabled) + ",\n";
            sSQL += " batch_size = " + _batchSize.ToString() + "\n";
            sSQL += "  where daemon_type = " + Quote(_daemonType) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysDaemonType.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from sbt_sys_daemon_type \n";
            sSQL += "  where daemon_type = " + Quote(_daemonType) + "\n";

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
        public void FetchByE(string daemonType)
        {
            bool hasData;
            hasData = FetchBy(daemonType);
            if (!hasData)
                throw new Exception("TbSysDaemonType.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string daemonType)
        {
            _daemonType = daemonType;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbSysDaemonType CreateBy(string daemonType)
        {
            TbSysDaemonType tbl;
            bool hasData;

            tbl = new TbSysDaemonType();
            hasData = tbl.FetchBy(daemonType);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string daemonType)
        {
            TbSysDaemonType tbl;
            tbl = new TbSysDaemonType();

            tbl.DaemonType = daemonType;

            tbl.Delete();
        }

        #endregion

        #region 文件和访问组件相关的支持函数
        /// <summary>
        /// 通过DataRow进行赋值
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _daemonType = DbToString(row["daemon_type"]);
            _daemonTypeName = DbToString(row["daemon_type_name"]);
            _daemonComponent = DbToString(row["daemon_component"]);
            _daemonClass = DbToString(row["daemon_class"]);
            _daemonPriority = DbToInt(row["daemon_priority"]);
            _intervalType = DbToString(row["interval_type"]);
            _intervalValue = DbToInt(row["interval_value"]);
            _activeIntervalValue = DbToInt(row["active_interval_value"]);
            _enabled = DbToString(row["enabled"]);
            _batchSize = DbToInt(row["batch_size"]);
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
            sLine = "daemon_type\x9" + _daemonType;
            writer.WriteLine(sLine);

            sLine = "daemon_type_name\x9" + _daemonTypeName;
            writer.WriteLine(sLine);

            sLine = "daemon_component\x9" + _daemonComponent;
            writer.WriteLine(sLine);

            sLine = "daemon_class\x9" + _daemonClass;
            writer.WriteLine(sLine);

            sLine = "daemon_priority\x9" + _daemonPriority.ToString();
            writer.WriteLine(sLine);

            sLine = "interval_type\x9" + _intervalType;
            writer.WriteLine(sLine);

            sLine = "interval_value\x9" + _intervalValue.ToString();
            writer.WriteLine(sLine);

            sLine = "active_interval_value\x9" + _activeIntervalValue.ToString();
            writer.WriteLine(sLine);

            sLine = "enabled\x9" + _enabled;
            writer.WriteLine(sLine);

            sLine = "batch_size\x9" + _batchSize.ToString();
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
            sLine = "daemon_type\tdaemon_type_name\tdaemon_component\t";
            sLine += "daemon_class\tdaemon_priority\tinterval_type\t";
            sLine += "interval_value\tactive_interval_value\tenabled\t";
            sLine += "batch_size";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL = "select daemon_type,     daemon_type_name,daemon_component,\n";
            sSQL += "       daemon_class,    daemon_priority, interval_type,   \n";
            sSQL += "       interval_value,  active_interval_value,enabled,         \n";
            sSQL += "       batch_size      \n";
            sSQL += "  from sbt_sys_daemon_type";

            //======== 4. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. 处理每一行 ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 处理每一列 ========
                for (nCol = 0; nCol < 10; nCol++)
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
                case "daemon_type":
                    return "varchar";
                case "daemon_type_name":
                    return "varchar";
                case "daemon_component":
                    return "varchar";
                case "daemon_class":
                    return "varchar";
                case "daemon_priority":
                    return "int";
                case "interval_type":
                    return "varchar";
                case "interval_value":
                    return "int";
                case "active_interval_value":
                    return "int";
                case "enabled":
                    return "char";
                case "batch_size":
                    return "int";
                default:
                    throw new Exception("TbSysDaemonTypeBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 获取一个字段的CSharp类型
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "daemon_type":
                    return "string";
                case "daemon_type_name":
                    return "string";
                case "daemon_component":
                    return "string";
                case "daemon_class":
                    return "string";
                case "daemon_priority":
                    return "int";
                case "interval_type":
                    return "string";
                case "interval_value":
                    return "int";
                case "active_interval_value":
                    return "int";
                case "enabled":
                    return "string";
                case "batch_size":
                    return "int";
                default:
                    throw new Exception("TbSysDaemonTypeBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL = "select " + toFieldName + "from sbt_sys_daemon_type \n";
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
