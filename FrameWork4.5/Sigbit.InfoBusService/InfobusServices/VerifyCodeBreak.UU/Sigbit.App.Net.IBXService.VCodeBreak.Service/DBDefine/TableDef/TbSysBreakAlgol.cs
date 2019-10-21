using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.App.Net.IBXService.VCodeBreak.Service.DBDefine
{
    #region 枚举
    /// <summary>
    /// 枚举字段：算法
    /// </summary>
    public enum TbSysBreakAlgolEAlgolID
    {
        None,
        [SbtEnumDescString("人工破解")]
        Manual,
        [SbtEnumDescString("UU云破解")]
        UUCloud,
        [SbtEnumDescString("随机结果")]
        Random,
        [SbtEnumDescString("云南移动专属破解")]
        YunnanMobile
    }
    #endregion

    /// <summary>
    /// 验证码的破解算法 (vcb_sys_break_algol)的表操作类
    /// </summary>
    public class TbSysBreakAlgol : TbSysBreakAlgolBase
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
            QDBVCBreakPools.ResetAlgol();
        }

        public override void Delete()
        {
            base.Delete();
            QDBVCBreakPools.ResetAlgol();
        }

        public override void Update()
        {
            base.Update();
            QDBVCBreakPools.ResetAlgol();
        }

        #endregion
    }


    /// <summary>
    /// 验证码的破解算法 (vcb_sys_break_algol)的字段名类
    /// </summary>
    public class TbSysBreakAlgolF
    {
        public const string TableName = "vcb_sys_break_algol";
        public const string AlgolId              = "algol_id";
        public const string AlgolName            = "algol_name";
        public const string AlgolDesc            = "algol_desc";
        public const string AlgolData01          = "algol_data_01";
        public const string AlgolData02          = "algol_data_02";
        public const string AlgolData03          = "algol_data_03";
        public const string AlgolData04          = "algol_data_04";
        public const string CreateTime           = "create_time";
        public const string Creator              = "creator";
        public const string ModifyTime           = "modify_time";
        public const string Remarks              = "remarks";
    }


    /// <summary>
    /// 验证码的破解算法 (vcb_sys_break_algol)的表操作基类
    /// </summary>
    public class TbSysBreakAlgolBase : Sigbit.Data.TableBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TbSysBreakAlgolBase()
        {
            ResetData();
        }

        #region 属性定义
        protected string _algolId = "";
        /// <summary>
        /// 算法标识，主键
        /// </summary>
        public string AlgolId
        {
            get { return _algolId; }
            set { _algolId = value; }
        }

        protected string _algolName = "";
        /// <summary>
        /// 算法名称
        /// </summary>
        public string AlgolName
        {
            get { return _algolName; }
            set { _algolName = value; }
        }

        protected string _algolDesc = "";
        /// <summary>
        /// 算法描述
        /// </summary>
        public string AlgolDesc
        {
            get { return _algolDesc; }
            set { _algolDesc = value; }
        }

        protected string _algolData01 = "";
        /// <summary>
        /// 数据一
        /// </summary>
        public string AlgolData01
        {
            get { return _algolData01; }
            set { _algolData01 = value; }
        }

        protected string _algolData02 = "";
        /// <summary>
        /// 数据二
        /// </summary>
        public string AlgolData02
        {
            get { return _algolData02; }
            set { _algolData02 = value; }
        }

        protected string _algolData03 = "";
        /// <summary>
        /// 数据三
        /// </summary>
        public string AlgolData03
        {
            get { return _algolData03; }
            set { _algolData03 = value; }
        }

        protected string _algolData04 = "";
        /// <summary>
        /// 数据四
        /// </summary>
        public string AlgolData04
        {
            get { return _algolData04; }
            set { _algolData04 = value; }
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
            sb.Append("AlgolId: " + this._algolId + "<br>");
            sb.Append("AlgolName: " + this._algolName + "<br>");
            sb.Append("AlgolDesc: " + this._algolDesc + "<br>");
            sb.Append("AlgolData01: " + this._algolData01 + "<br>");
            sb.Append("AlgolData02: " + this._algolData02 + "<br>");
            sb.Append("AlgolData03: " + this._algolData03 + "<br>");
            sb.Append("AlgolData04: " + this._algolData04 + "<br>");
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
            _algolId = "";
            _algolName = "";
            _algolDesc = "";
            _algolData01 = "";
            _algolData02 = "";
            _algolData03 = "";
            _algolData04 = "";
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
            sSQL  = "select algol_name,          algol_desc,          algol_data_01,        \r\n";
            sSQL += "       algol_data_02,       algol_data_03,       algol_data_04,        \r\n";
            sSQL += "       create_time,         creator,             modify_time,          \r\n";
            sSQL += "       remarks              \r\n";
            sSQL += "  from vcb_sys_break_algol    \r\n";
            sSQL += "  where algol_id = " + Quote(_algolId) + "\r\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbSysBreakAlgol.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _algolName      = DbToString(row["algol_name"]);
            _algolDesc      = DbToString(row["algol_desc"]);
            _algolData01    = DbToString(row["algol_data_01"]);
            _algolData02    = DbToString(row["algol_data_02"]);
            _algolData03    = DbToString(row["algol_data_03"]);
            _algolData04    = DbToString(row["algol_data_04"]);
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

            sSQL  = "insert into vcb_sys_break_algol \r\n";
            sSQL += "( algol_id,         algol_name,       \r\n";
            sSQL += "  algol_desc,       algol_data_01,    \r\n";
            sSQL += "  algol_data_02,    algol_data_03,    \r\n";
            sSQL += "  algol_data_04,    create_time,      \r\n";
            sSQL += "  creator,          modify_time,      \r\n";
            sSQL += "  remarks           \r\n";
            sSQL += ") values (          \r\n";
            sSQL += Quote(_algolId)         +","+ Quote(_algolName)       +",\r\n";
            sSQL += Quote(_algolDesc)       +","+ Quote(_algolData01)     +",\r\n";
            sSQL += Quote(_algolData02)     +","+ Quote(_algolData03)     +",\r\n";
            sSQL += Quote(_algolData04)     +","+ Quote(_createTime)      +",\r\n";
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

            sSQL  = "delete from vcb_sys_break_algol \r\n";
            sSQL += "  where algol_id = " + Quote(_algolId) + "\r\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysBreakAlgol.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL  = "update vcb_sys_break_algol set \r\n";
            sSQL += " algol_id = " + Quote(_algolId) + ",\r\n";
            sSQL += " algol_name = " + Quote(_algolName) + ",\r\n";
            sSQL += " algol_desc = " + Quote(_algolDesc) + ",\r\n";
            sSQL += " algol_data_01 = " + Quote(_algolData01) + ",\r\n";
            sSQL += " algol_data_02 = " + Quote(_algolData02) + ",\r\n";
            sSQL += " algol_data_03 = " + Quote(_algolData03) + ",\r\n";
            sSQL += " algol_data_04 = " + Quote(_algolData04) + ",\r\n";
            sSQL += " create_time = " + Quote(_createTime) + ",\r\n";
            sSQL += " creator = " + Quote(_creator) + ",\r\n";
            sSQL += " modify_time = " + Quote(_modifyTime) + ",\r\n";
            sSQL += " remarks = " + Quote(_remarks) + "\r\n";
            sSQL += "  where algol_id = " + Quote(_algolId) + "\r\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysBreakAlgol.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from vcb_sys_break_algol \r\n";
            sSQL += "  where algol_id = " + Quote(_algolId) + "\r\n";

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
        public void FetchByE(string algolId)
        {
            bool hasData;
            hasData = FetchBy(algolId);
            if (!hasData)
                throw new Exception("TbSysBreakAlgol.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string algolId)
        {
            _algolId = algolId;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbSysBreakAlgol CreateBy(string algolId)
        {
            TbSysBreakAlgol tbl;
            bool hasData;

            tbl = new TbSysBreakAlgol();
            hasData = tbl.FetchBy(algolId);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string algolId)
        {
            TbSysBreakAlgol tbl;
            tbl = new TbSysBreakAlgol();

            tbl.AlgolId = algolId;

            tbl.Delete();
        }
        #endregion

        #region 文件和访问组件相关的支持函数
        /// <summary>
        /// 通过DataRow进行赋值
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _algolId             = DbToString(row["algol_id"]);
            _algolName           = DbToString(row["algol_name"]);
            _algolDesc           = DbToString(row["algol_desc"]);
            _algolData01         = DbToString(row["algol_data_01"]);
            _algolData02         = DbToString(row["algol_data_02"]);
            _algolData03         = DbToString(row["algol_data_03"]);
            _algolData04         = DbToString(row["algol_data_04"]);
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
            sLine = "algol_id\x9" + _algolId;
            writer.WriteLine(sLine);

            sLine = "algol_name\x9" + _algolName;
            writer.WriteLine(sLine);

            sLine = "algol_desc\x9" + _algolDesc;
            writer.WriteLine(sLine);

            sLine = "algol_data_01\x9" + _algolData01;
            writer.WriteLine(sLine);

            sLine = "algol_data_02\x9" + _algolData02;
            writer.WriteLine(sLine);

            sLine = "algol_data_03\x9" + _algolData03;
            writer.WriteLine(sLine);

            sLine = "algol_data_04\x9" + _algolData04;
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
            sLine  = "algol_id\talgol_name\talgol_desc\t";
            sLine += "algol_data_01\talgol_data_02\talgol_data_03\t";
            sLine += "algol_data_04\tcreate_time\tcreator\t";
            sLine += "modify_time\tremarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL  = "select algol_id,        algol_name,      algol_desc,      \r\n";
            sSQL += "       algol_data_01,   algol_data_02,   algol_data_03,   \r\n";
            sSQL += "       algol_data_04,   create_time,     creator,         \r\n";
            sSQL += "       modify_time,     remarks         \r\n";
            sSQL += "  from vcb_sys_break_algol";

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
                case "algol_id":
                    return "varchar";
                case "algol_name":
                    return "varchar";
                case "algol_desc":
                    return "text";
                case "algol_data_01":
                    return "varchar";
                case "algol_data_02":
                    return "varchar";
                case "algol_data_03":
                    return "varchar";
                case "algol_data_04":
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
                    throw new Exception("TbSysBreakAlgolBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 获取一个字段的CSharp类型
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "algol_id":
                    return "string";
                case "algol_name":
                    return "string";
                case "algol_desc":
                    return "string";
                case "algol_data_01":
                    return "string";
                case "algol_data_02":
                    return "string";
                case "algol_data_03":
                    return "string";
                case "algol_data_04":
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
                    throw new Exception("TbSysBreakAlgolBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL  = "select " + toFieldName + " from vcb_sys_break_algol \r\n";
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
