using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.App.Net.IBXService.DNS.Service.DBDefine
{
    /// <summary>
    /// 服务地址 (dns_sys_url_address)的表操作用户类
    /// </summary>
    public class TbSysUrlAddress : TbSysUrlAddressBase
    {
        #region 用户可编辑区域

        public override void Insert()
        {
            base.Insert();
            QDBDnsPools.ResetAll();
        }

        public override void Delete()
        {
            base.Delete();
            QDBDnsPools.ResetAll();
        }

        public override void Update()
        {
            base.Update();
            QDBDnsPools.ResetAll();
        }

        #endregion
    }


    /// <summary>
    /// 服务地址 (dns_sys_url_address)的字段名类
    /// </summary>
    public class TbSysUrlAddressF
    {
        public const string TableName = "dns_sys_url_address";
        public const string UrlAddressUid        = "url_address_uid";
        public const string UrlAddressName       = "url_address_name";
        public const string UrlAddressDesc       = "url_address_desc";
        public const string ServiceId            = "service_id";
        public const string UrlAddressLink       = "url_address_link";
        public const string CreateTime           = "create_time";
        public const string Creator              = "creator";
        public const string ModifyTime           = "modify_time";
        public const string Remarks              = "remarks";
    }


    /// <summary>
    /// 服务地址 (dns_sys_url_address)的表操作基类
    /// </summary>
    public class TbSysUrlAddressBase : Sigbit.Data.TableBase
    {
        #region 私有变量定义
        protected string     _urlAddressUid           = "";
        protected string     _urlAddressName          = "";
        protected string     _urlAddressDesc          = "";
        protected string     _serviceId               = "";
        protected string     _urlAddressLink          = "";
        protected string     _createTime              = "";
        protected string     _creator                 = "";
        protected string     _modifyTime              = "";
        protected string     _remarks                 = "";
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public TbSysUrlAddressBase()
        {
            ResetData();
        }

        #region 属性定义
        /// <summary>
        /// 服务地址标识，主键
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
        /// 服务地址的名称
        /// </summary>
        public string UrlAddressName
        {
            get
            {
                return _urlAddressName;
            }
            set
            {
                _urlAddressName = value;
            }
        }

        /// <summary>
        /// 服务地址描述
        /// </summary>
        public string UrlAddressDesc
        {
            get
            {
                return _urlAddressDesc;
            }
            set
            {
                _urlAddressDesc = value;
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
        /// 具体的URL
        /// </summary>
        public string UrlAddressLink
        {
            get
            {
                return _urlAddressLink;
            }
            set
            {
                _urlAddressLink = value;
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
            sb.Append("UrlAddressUid: " + this._urlAddressUid + "<br>");
            sb.Append("UrlAddressName: " + this._urlAddressName + "<br>");
            sb.Append("UrlAddressDesc: " + this._urlAddressDesc + "<br>");
            sb.Append("ServiceId: " + this._serviceId + "<br>");
            sb.Append("UrlAddressLink: " + this._urlAddressLink + "<br>");
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
            _urlAddressUid = "";
            _urlAddressName = "";
            _urlAddressDesc = "";
            _serviceId = "";
            _urlAddressLink = "";
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
            sSQL  = "select url_address_name,    url_address_desc,    service_id,           \n";
            sSQL += "       url_address_link,    create_time,         creator,              \n";
            sSQL += "       modify_time,         remarks              \n";
            sSQL += "  from dns_sys_url_address    \n";
            sSQL += "  where url_address_uid = " + Quote(_urlAddressUid) + "\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbSysUrlAddress.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _urlAddressName = DbToString(row["url_address_name"]);
            _urlAddressDesc = DbToString(row["url_address_desc"]);
            _serviceId      = DbToString(row["service_id"]);
            _urlAddressLink = DbToString(row["url_address_link"]);
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

            sSQL  = "insert into dns_sys_url_address \n";
            sSQL += "( url_address_uid,  url_address_name, \n";
            sSQL += "  url_address_desc, service_id,       \n";
            sSQL += "  url_address_link, create_time,      \n";
            sSQL += "  creator,          modify_time,      \n";
            sSQL += "  remarks           \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_urlAddressUid)   +","+ Quote(_urlAddressName)  +",\n";
            sSQL += Quote(_urlAddressDesc)  +","+ Quote(_serviceId)       +",\n";
            sSQL += Quote(_urlAddressLink)  +","+ Quote(_createTime)      +",\n";
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

            sSQL  = "delete from dns_sys_url_address \n";
            sSQL += "  where url_address_uid = " + Quote(_urlAddressUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysUrlAddress.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL  = "update dns_sys_url_address set \n";
            sSQL += " url_address_uid = " + Quote(_urlAddressUid) + ",\n";
            sSQL += " url_address_name = " + Quote(_urlAddressName) + ",\n";
            sSQL += " url_address_desc = " + Quote(_urlAddressDesc) + ",\n";
            sSQL += " service_id = " + Quote(_serviceId) + ",\n";
            sSQL += " url_address_link = " + Quote(_urlAddressLink) + ",\n";
            sSQL += " create_time = " + Quote(_createTime) + ",\n";
            sSQL += " creator = " + Quote(_creator) + ",\n";
            sSQL += " modify_time = " + Quote(_modifyTime) + ",\n";
            sSQL += " remarks = " + Quote(_remarks) + "\n";
            sSQL += "  where url_address_uid = " + Quote(_urlAddressUid) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysUrlAddress.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from dns_sys_url_address \n";
            sSQL += "  where url_address_uid = " + Quote(_urlAddressUid) + "\n";

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
        public void FetchByE(string urlAddressUid)
        {
            bool hasData;
            hasData = FetchBy(urlAddressUid);
            if (!hasData)
                throw new Exception("TbSysUrlAddress.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string urlAddressUid)
        {
            _urlAddressUid = urlAddressUid;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbSysUrlAddress CreateBy(string urlAddressUid)
        {
            TbSysUrlAddress tbl;
            bool hasData;

            tbl = new TbSysUrlAddress();
            hasData = tbl.FetchBy(urlAddressUid);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string urlAddressUid)
        {
            TbSysUrlAddress tbl;
            tbl = new TbSysUrlAddress();

            tbl.UrlAddressUid = urlAddressUid;

            tbl.Delete();
        }

        #endregion

        #region 文件和访问组件相关的支持函数
        /// <summary>
        /// 通过DataRow进行赋值
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _urlAddressUid       = DbToString(row["url_address_uid"]);
            _urlAddressName      = DbToString(row["url_address_name"]);
            _urlAddressDesc      = DbToString(row["url_address_desc"]);
            _serviceId           = DbToString(row["service_id"]);
            _urlAddressLink      = DbToString(row["url_address_link"]);
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
            sLine = "url_address_uid\x9" + _urlAddressUid;
            writer.WriteLine(sLine);

            sLine = "url_address_name\x9" + _urlAddressName;
            writer.WriteLine(sLine);

            sLine = "url_address_desc\x9" + _urlAddressDesc;
            writer.WriteLine(sLine);

            sLine = "service_id\x9" + _serviceId;
            writer.WriteLine(sLine);

            sLine = "url_address_link\x9" + _urlAddressLink;
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
            sLine  = "url_address_uid\turl_address_name\turl_address_desc\t";
            sLine += "service_id\turl_address_link\tcreate_time\t";
            sLine += "creator\tmodify_time\tremarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL  = "select url_address_uid, url_address_name,url_address_desc,\n";
            sSQL += "       service_id,      url_address_link,create_time,     \n";
            sSQL += "       creator,         modify_time,     remarks         \n";
            sSQL += "  from dns_sys_url_address";

            //======== 4. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. 处理每一行 ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 处理每一列 ========
                for (nCol = 0; nCol < 9; nCol++)
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
                case "url_address_uid":
                    return "varchar";
                case "url_address_name":
                    return "varchar";
                case "url_address_desc":
                    return "text";
                case "service_id":
                    return "varchar";
                case "url_address_link":
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
                    throw new Exception("TbSysUrlAddressBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 获取一个字段的CSharp类型
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "url_address_uid":
                    return "string";
                case "url_address_name":
                    return "string";
                case "url_address_desc":
                    return "string";
                case "service_id":
                    return "string";
                case "url_address_link":
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
                    throw new Exception("TbSysUrlAddressBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL  = "select " + toFieldName + " from dns_sys_url_address \n";
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
