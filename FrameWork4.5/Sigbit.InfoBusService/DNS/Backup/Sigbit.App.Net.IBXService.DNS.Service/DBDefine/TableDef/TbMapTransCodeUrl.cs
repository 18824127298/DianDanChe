using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.App.Net.IBXService.DNS.Service.DBDefine
{
    /// <summary>
    /// 交易码的服务地址 (dns_map_trans_code_url)的表操作用户类
    /// </summary>
    public class TbMapTransCodeUrl : TbMapTransCodeUrlBase
    {
        #region 用户可编辑区域

        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }


    /// <summary>
    /// 交易码的服务地址 (dns_map_trans_code_url)的字段名类
    /// </summary>
    public class TbMapTransCodeUrlF
    {
        public const string TableName = "dns_map_trans_code_url";
        public const string MapUid               = "map_uid";
        public const string ServiceId            = "service_id";
        public const string TransCode            = "trans_code";
        public const string FromSystem           = "from_system";
        public const string FromClientVersion    = "from_client_version";
        public const string FromClientId         = "from_client_id";
        public const string UrlAddressUid        = "url_address_uid";
        public const string CreateTime           = "create_time";
        public const string Creator              = "creator";
        public const string ModifyTime           = "modify_time";
        public const string Remarks              = "remarks";
    }


    /// <summary>
    /// 交易码的服务地址 (dns_map_trans_code_url)的表操作基类
    /// </summary>
    public class TbMapTransCodeUrlBase : Sigbit.Data.TableBase
    {
        #region 私有变量定义
        protected string     _mapUid                  = "";
        protected string     _serviceId               = "";
        protected string     _transCode               = "";
        protected string     _fromSystem              = "";
        protected string     _fromClientVersion       = "";
        protected string     _fromClientId            = "";
        protected string     _urlAddressUid           = "";
        protected string     _createTime              = "";
        protected string     _creator                 = "";
        protected string     _modifyTime              = "";
        protected string     _remarks                 = "";
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public TbMapTransCodeUrlBase()
        {
            ResetData();
        }

        #region 属性定义
        /// <summary>
        /// 对应关系的标识，主键
        /// </summary>
        public string MapUid
        {
            get
            {
                return _mapUid;
            }
            set
            {
                _mapUid = value;
            }
        }

        /// <summary>
        /// 服务标识
        /// </summary>
        public string ServiceId
        {
            get
            {
                return _serviceId;
            }
            set
            {
                _serviceId = value;
            }
        }

        /// <summary>
        /// 交易码
        /// </summary>
        public string TransCode
        {
            get
            {
                return _transCode;
            }
            set
            {
                _transCode = value;
            }
        }

        /// <summary>
        /// 来自系统
        /// </summary>
        public string FromSystem
        {
            get
            {
                return _fromSystem;
            }
            set
            {
                _fromSystem = value;
            }
        }

        /// <summary>
        /// 客户端的版本号
        /// </summary>
        public string FromClientVersion
        {
            get
            {
                return _fromClientVersion;
            }
            set
            {
                _fromClientVersion = value;
            }
        }

        /// <summary>
        /// 客户端的标识
        /// </summary>
        public string FromClientId
        {
            get
            {
                return _fromClientId;
            }
            set
            {
                _fromClientId = value;
            }
        }

        /// <summary>
        /// 服务地址
        /// </summary>
        public string UrlAddressUid
        {
            get
            {
                return _urlAddressUid;
            }
            set
            {
                _urlAddressUid = value;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime
        {
            get
            {
                return _createTime;
            }
            set
            {
                _createTime = value;
            }
        }

        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator
        {
            get
            {
                return _creator;
            }
            set
            {
                _creator = value;
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
            sb.Append("MapUid: " + this._mapUid + "<br>");
            sb.Append("ServiceId: " + this._serviceId + "<br>");
            sb.Append("TransCode: " + this._transCode + "<br>");
            sb.Append("FromSystem: " + this._fromSystem + "<br>");
            sb.Append("FromClientVersion: " + this._fromClientVersion + "<br>");
            sb.Append("FromClientId: " + this._fromClientId + "<br>");
            sb.Append("UrlAddressUid: " + this._urlAddressUid + "<br>");
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
            _mapUid = "";
            _serviceId = "";
            _transCode = "";
            _fromSystem = "";
            _fromClientVersion = "";
            _fromClientId = "";
            _urlAddressUid = "";
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
            sSQL  = "select service_id,          trans_code,          from_system,          \n";
            sSQL += "       from_client_version, from_client_id,      url_address_uid,      \n";
            sSQL += "       create_time,         creator,             modify_time,          \n";
            sSQL += "       remarks              \n";
            sSQL += "  from dns_map_trans_code_url    \n";
            sSQL += "  where map_uid = " + Quote(_mapUid) + "\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbMapTransCodeUrl.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _serviceId      = DbToString(row["service_id"]);
            _transCode      = DbToString(row["trans_code"]);
            _fromSystem     = DbToString(row["from_system"]);
            _fromClientVersion = DbToString(row["from_client_version"]);
            _fromClientId   = DbToString(row["from_client_id"]);
            _urlAddressUid  = DbToString(row["url_address_uid"]);
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

            sSQL  = "insert into dns_map_trans_code_url \n";
            sSQL += "( map_uid,          service_id,       \n";
            sSQL += "  trans_code,       from_system,      \n";
            sSQL += "  from_client_version, from_client_id,   \n";
            sSQL += "  url_address_uid,  create_time,      \n";
            sSQL += "  creator,          modify_time,      \n";
            sSQL += "  remarks           \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_mapUid)          +","+ Quote(_serviceId)       +",\n";
            sSQL += Quote(_transCode)       +","+ Quote(_fromSystem)      +",\n";
            sSQL += Quote(_fromClientVersion)+","+ Quote(_fromClientId)    +",\n";
            sSQL += Quote(_urlAddressUid)   +","+ Quote(_createTime)      +",\n";
            sSQL += Quote(_creator)         +","+ Quote(_modifyTime)      +",\n";
            sSQL += Quote(_remarks)         +")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 按主键删除一条数据
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL  = "delete from dns_map_trans_code_url \n";
            sSQL += "  where map_uid = " + Quote(_mapUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbMapTransCodeUrl.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL  = "update dns_map_trans_code_url set \n";
            sSQL += " map_uid = " + Quote(_mapUid) + ",\n";
            sSQL += " service_id = " + Quote(_serviceId) + ",\n";
            sSQL += " trans_code = " + Quote(_transCode) + ",\n";
            sSQL += " from_system = " + Quote(_fromSystem) + ",\n";
            sSQL += " from_client_version = " + Quote(_fromClientVersion) + ",\n";
            sSQL += " from_client_id = " + Quote(_fromClientId) + ",\n";
            sSQL += " url_address_uid = " + Quote(_urlAddressUid) + ",\n";
            sSQL += " create_time = " + Quote(_createTime) + ",\n";
            sSQL += " creator = " + Quote(_creator) + ",\n";
            sSQL += " modify_time = " + Quote(_modifyTime) + ",\n";
            sSQL += " remarks = " + Quote(_remarks) + "\n";
            sSQL += "  where map_uid = " + Quote(_mapUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbMapTransCodeUrl.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from dns_map_trans_code_url \n";
            sSQL += "  where map_uid = " + Quote(_mapUid) + "\n";

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
        public void FetchByE(string mapUid)
        {
            bool hasData;
            hasData = FetchBy(mapUid);
            if (!hasData)
                throw new Exception("TbMapTransCodeUrl.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string mapUid)
        {
            _mapUid = mapUid;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbMapTransCodeUrl CreateBy(string mapUid)
        {
            TbMapTransCodeUrl tbl;
            bool hasData;

            tbl = new TbMapTransCodeUrl();
            hasData = tbl.FetchBy(mapUid);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string mapUid)
        {
            TbMapTransCodeUrl tbl;
            tbl = new TbMapTransCodeUrl();

            tbl.MapUid = mapUid;

            tbl.Delete();
        }

        #endregion

        #region 文件和访问组件相关的支持函数
        /// <summary>
        /// 通过DataRow进行赋值
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _mapUid              = DbToString(row["map_uid"]);
            _serviceId           = DbToString(row["service_id"]);
            _transCode           = DbToString(row["trans_code"]);
            _fromSystem          = DbToString(row["from_system"]);
            _fromClientVersion   = DbToString(row["from_client_version"]);
            _fromClientId        = DbToString(row["from_client_id"]);
            _urlAddressUid       = DbToString(row["url_address_uid"]);
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
            sLine = "map_uid\x9" + _mapUid;
            writer.WriteLine(sLine);

            sLine = "service_id\x9" + _serviceId;
            writer.WriteLine(sLine);

            sLine = "trans_code\x9" + _transCode;
            writer.WriteLine(sLine);

            sLine = "from_system\x9" + _fromSystem;
            writer.WriteLine(sLine);

            sLine = "from_client_version\x9" + _fromClientVersion;
            writer.WriteLine(sLine);

            sLine = "from_client_id\x9" + _fromClientId;
            writer.WriteLine(sLine);

            sLine = "url_address_uid\x9" + _urlAddressUid;
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
            sLine  = "map_uid\tservice_id\ttrans_code\t";
            sLine += "from_system\tfrom_client_version\tfrom_client_id\t";
            sLine += "url_address_uid\tcreate_time\tcreator\t";
            sLine += "modify_time\tremarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL  = "select map_uid,         service_id,      trans_code,      \n";
            sSQL += "       from_system,     from_client_version,from_client_id,  \n";
            sSQL += "       url_address_uid, create_time,     creator,         \n";
            sSQL += "       modify_time,     remarks         \n";
            sSQL += "  from dns_map_trans_code_url";

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
                case "map_uid":
                    return "varchar";
                case "service_id":
                    return "varchar";
                case "trans_code":
                    return "varchar";
                case "from_system":
                    return "varchar";
                case "from_client_version":
                    return "varchar";
                case "from_client_id":
                    return "varchar";
                case "url_address_uid":
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
                    throw new Exception("TbMapTransCodeUrlBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 获取一个字段的CSharp类型
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "map_uid":
                    return "string";
                case "service_id":
                    return "string";
                case "trans_code":
                    return "string";
                case "from_system":
                    return "string";
                case "from_client_version":
                    return "string";
                case "from_client_id":
                    return "string";
                case "url_address_uid":
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
                    throw new Exception("TbMapTransCodeUrlBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL  = "select " + toFieldName + " from dns_map_trans_code_url \n";
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
