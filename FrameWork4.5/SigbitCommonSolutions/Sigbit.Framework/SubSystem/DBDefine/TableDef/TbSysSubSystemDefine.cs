using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework.SubSystem.DBDefine
{
    /// <summary>
    /// 子系统定义表(sbt_sys_sub_system_define)的表操作用户类
    /// </summary>
    public class TbSysSubSystemDefine : TbSysSubSystemDefineBase
    {
        #region 用户可编辑区域

        private string _newSubSystemId_ForUpdate = "";
        /// <summary>
        /// 新的系统编码（For Update）
        /// </summary>
        public string NewSubSystemId_ForUpdate
        {
            get { return _newSubSystemId_ForUpdate; }
            set { _newSubSystemId_ForUpdate = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Update()
        {

            string sNewSubSystemId = this.NewSubSystemId_ForUpdate;
            if (sNewSubSystemId == "")
                sNewSubSystemId = this.SubSystemId;

            string sSQL = "";

            sSQL = "update sbt_sys_sub_system_define set \n";
            sSQL += " sub_system_id = " + Quote(sNewSubSystemId) + ",\n";
            sSQL += " sub_system_name = " + Quote(_subSystemName) + ",\n";
            sSQL += " sub_system_color = " + Quote(_subSystemColor) + ",\n";
            sSQL += " full_name = " + Quote(_fullName) + ",\n";
            sSQL += " app_theme = " + Quote(_appTheme) + ",\n";
            sSQL += " homepage_graph = " + Quote(_homepageGraph) + ",\n";
            sSQL += " homepage_caption = " + Quote(_homepageCaption) + ",\n";
            sSQL += " page_title_text = " + Quote(_pageTitleText) + ",\n";
            sSQL += " page_title_image = " + Quote(_pageTitleImage) + ",\n";
            sSQL += " menu_root_text = " + Quote(_menuRootText) + ",\n";
            sSQL += " display_order = " + _displayOrder.ToString() + ",\n";
            sSQL += " create_time = " + Quote(_createTime) + ",\n";
            sSQL += " creator = " + Quote(_creator) + ",\n";
            sSQL += " modify_time = " + Quote(_modifyTime) + ",\n";
            sSQL += " remarks = " + Quote(_remarks) + "\n";
            sSQL += "  where sub_system_id = " + Quote(_subSystemId) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysSubSystemDefine.Update() Error - cannot update data via PrimaryKey.");

     
        }



        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }


    /// <summary>
    /// 子系统定义表(sbt_sys_sub_system_define)的字段名类
    /// </summary>
    public class TbSysSubSystemDefineF
    {
        public const string TableName = "sbt_sys_sub_system_define";
        public const string SubSystemId = "sub_system_id";
        public const string SubSystemName = "sub_system_name";
        public const string SubSystemColor = "sub_system_color";
        public const string FullName = "full_name";
        public const string AppTheme = "app_theme";
        public const string HomepageGraph = "homepage_graph";
        public const string HomepageCaption = "homepage_caption";
        public const string PageTitleText = "page_title_text";
        public const string PageTitleImage = "page_title_image";
        public const string MenuRootText = "menu_root_text";
        public const string DisplayOrder = "display_order";
        public const string CreateTime = "create_time";
        public const string Creator = "creator";
        public const string ModifyTime = "modify_time";
        public const string Remarks = "remarks";
    }


    /// <summary>
    /// 子系统定义表(sbt_sys_sub_system_define)的表操作基类
    /// </summary>
    public class TbSysSubSystemDefineBase : Sigbit.Data.TableBase
    {
        #region 私有变量定义
        protected string _subSystemId = "";
        protected string _subSystemName = "";
        protected string _subSystemColor = "";
        protected string _fullName = "";
        protected string _appTheme = "";
        protected string _homepageGraph = "";
        protected string _homepageCaption = "";
        protected string _pageTitleText = "";
        protected string _pageTitleImage = "";
        protected string _menuRootText = "";
        protected int _displayOrder;
        protected string _createTime = "";
        protected string _creator = "";
        protected string _modifyTime = "";
        protected string _remarks = "";
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public TbSysSubSystemDefineBase()
        {
            ResetData();
        }

        #region 属性定义
        /// <summary>
        /// 子系统标识，主键
        /// </summary>
        public string SubSystemId
        {
            get
            {
                return _subSystemId;
            }
            set
            {
                _subSystemId = value;
            }
        }

        /// <summary>
        /// 子系统名称
        /// </summary>
        public string SubSystemName
        {
            get
            {
                return _subSystemName;
            }
            set
            {
                _subSystemName = value;
            }
        }

        /// <summary>
        /// 代表的颜色值
        /// </summary>
        public string SubSystemColor
        {
            get
            {
                return _subSystemColor;
            }
            set
            {
                _subSystemColor = value;
            }
        }

        /// <summary>
        /// 全名
        /// </summary>
        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                _fullName = value;
            }
        }

        /// <summary>
        /// 主题
        /// </summary>
        public string AppTheme
        {
            get
            {
                return _appTheme;
            }
            set
            {
                _appTheme = value;
            }
        }

        /// <summary>
        /// 首页上的图片名
        /// </summary>
        public string HomepageGraph
        {
            get
            {
                return _homepageGraph;
            }
            set
            {
                _homepageGraph = value;
            }
        }

        /// <summary>
        /// 首页的子系统名称
        /// </summary>
        public string HomepageCaption
        {
            get
            {
                return _homepageCaption;
            }
            set
            {
                _homepageCaption = value;
            }
        }

        /// <summary>
        /// 主页标题上的文字
        /// </summary>
        public string PageTitleText
        {
            get
            {
                return _pageTitleText;
            }
            set
            {
                _pageTitleText = value;
            }
        }

        /// <summary>
        /// 主页标题上的图片文件
        /// </summary>
        public string PageTitleImage
        {
            get
            {
                return _pageTitleImage;
            }
            set
            {
                _pageTitleImage = value;
            }
        }

        /// <summary>
        /// 菜单根菜单的文字
        /// </summary>
        public string MenuRootText
        {
            get
            {
                return _menuRootText;
            }
            set
            {
                _menuRootText = value;
            }
        }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder
        {
            get
            {
                return _displayOrder;
            }
            set
            {
                _displayOrder = value;
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
            sb.Append("SubSystemId: " + this._subSystemId + "<br>");
            sb.Append("SubSystemName: " + this._subSystemName + "<br>");
            sb.Append("SubSystemColor: " + this._subSystemColor + "<br>");
            sb.Append("FullName: " + this._fullName + "<br>");
            sb.Append("AppTheme: " + this._appTheme + "<br>");
            sb.Append("HomepageGraph: " + this._homepageGraph + "<br>");
            sb.Append("HomepageCaption: " + this._homepageCaption + "<br>");
            sb.Append("PageTitleText: " + this._pageTitleText + "<br>");
            sb.Append("PageTitleImage: " + this._pageTitleImage + "<br>");
            sb.Append("MenuRootText: " + this._menuRootText + "<br>");
            sb.Append("DisplayOrder: " + this._displayOrder + "<br>");
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
            _subSystemId = "";
            _subSystemName = "";
            _subSystemColor = "";
            _fullName = "";
            _appTheme = "";
            _homepageGraph = "";
            _homepageCaption = "";
            _pageTitleText = "";
            _pageTitleImage = "";
            _menuRootText = "";
            _displayOrder = 0;
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
            sSQL = "select sub_system_name,     sub_system_color,    full_name,            \n";
            sSQL += "       app_theme,           homepage_graph,      homepage_caption,     \n";
            sSQL += "       page_title_text,     page_title_image,    menu_root_text,       \n";
            sSQL += "       display_order,       create_time,         creator,              \n";
            sSQL += "       modify_time,         remarks              \n";
            sSQL += "  from sbt_sys_sub_system_define    \n";
            sSQL += "  where sub_system_id = " + Quote(_subSystemId) + "\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbSysSubSystemDefine.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _subSystemName = DbToString(row["sub_system_name"]);
            _subSystemColor = DbToString(row["sub_system_color"]);
            _fullName = DbToString(row["full_name"]);
            _appTheme = DbToString(row["app_theme"]);
            _homepageGraph = DbToString(row["homepage_graph"]);
            _homepageCaption = DbToString(row["homepage_caption"]);
            _pageTitleText = DbToString(row["page_title_text"]);
            _pageTitleImage = DbToString(row["page_title_image"]);
            _menuRootText = DbToString(row["menu_root_text"]);
            _displayOrder = DbToInt(row["display_order"]);
            _createTime = DbToString(row["create_time"]);
            _creator = DbToString(row["creator"]);
            _modifyTime = DbToString(row["modify_time"]);
            _remarks = DbToString(row["remarks"]);
            return true;
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        public override void Insert()
        {
            string sSQL;

            sSQL = "insert into sbt_sys_sub_system_define \n";
            sSQL += "( sub_system_id,    sub_system_name,  \n";
            sSQL += "  sub_system_color, full_name,        \n";
            sSQL += "  app_theme,        homepage_graph,   \n";
            sSQL += "  homepage_caption, page_title_text,  \n";
            sSQL += "  page_title_image, menu_root_text,   \n";
            sSQL += "  display_order,    create_time,      \n";
            sSQL += "  creator,          modify_time,      \n";
            sSQL += "  remarks           \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_subSystemId) + "," + Quote(_subSystemName) + ",\n";
            sSQL += Quote(_subSystemColor) + "," + Quote(_fullName) + ",\n";
            sSQL += Quote(_appTheme) + "," + Quote(_homepageGraph) + ",\n";
            sSQL += Quote(_homepageCaption) + "," + Quote(_pageTitleText) + ",\n";
            sSQL += Quote(_pageTitleImage) + "," + Quote(_menuRootText) + ",\n";
            sSQL += _displayOrder.ToString() + "," + Quote(_createTime) + ",\n";
            sSQL += Quote(_creator) + "," + Quote(_modifyTime) + ",\n";
            sSQL += Quote(_remarks) + ")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 按主键删除一条数据
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL = "delete from sbt_sys_sub_system_define \n";
            sSQL += "  where sub_system_id = " + Quote(_subSystemId) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysSubSystemDefine.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL = "update sbt_sys_sub_system_define set \n";
            sSQL += " sub_system_id = " + Quote(_subSystemId) + ",\n";
            sSQL += " sub_system_name = " + Quote(_subSystemName) + ",\n";
            sSQL += " sub_system_color = " + Quote(_subSystemColor) + ",\n";
            sSQL += " full_name = " + Quote(_fullName) + ",\n";
            sSQL += " app_theme = " + Quote(_appTheme) + ",\n";
            sSQL += " homepage_graph = " + Quote(_homepageGraph) + ",\n";
            sSQL += " homepage_caption = " + Quote(_homepageCaption) + ",\n";
            sSQL += " page_title_text = " + Quote(_pageTitleText) + ",\n";
            sSQL += " page_title_image = " + Quote(_pageTitleImage) + ",\n";
            sSQL += " menu_root_text = " + Quote(_menuRootText) + ",\n";
            sSQL += " display_order = " + _displayOrder.ToString() + ",\n";
            sSQL += " create_time = " + Quote(_createTime) + ",\n";
            sSQL += " creator = " + Quote(_creator) + ",\n";
            sSQL += " modify_time = " + Quote(_modifyTime) + ",\n";
            sSQL += " remarks = " + Quote(_remarks) + "\n";
            sSQL += "  where sub_system_id = " + Quote(_subSystemId) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysSubSystemDefine.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from sbt_sys_sub_system_define \n";
            sSQL += "  where sub_system_id = " + Quote(_subSystemId) + "\n";

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
        public void FetchByE(string subSystemId)
        {
            bool hasData;
            hasData = FetchBy(subSystemId);
            if (!hasData)
                throw new Exception("TbSysSubSystemDefine.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string subSystemId)
        {
            _subSystemId = subSystemId;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbSysSubSystemDefine CreateBy(string subSystemId)
        {
            TbSysSubSystemDefine tbl;
            bool hasData;

            tbl = new TbSysSubSystemDefine();
            hasData = tbl.FetchBy(subSystemId);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string subSystemId)
        {
            TbSysSubSystemDefine tbl;
            tbl = new TbSysSubSystemDefine();

            tbl.SubSystemId = subSystemId;

            tbl.Delete();
        }

        #endregion

        #region 文件和访问组件相关的支持函数
        /// <summary>
        /// 通过DataRow进行赋值
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _subSystemId = DbToString(row["sub_system_id"]);
            _subSystemName = DbToString(row["sub_system_name"]);
            _subSystemColor = DbToString(row["sub_system_color"]);
            _fullName = DbToString(row["full_name"]);
            _appTheme = DbToString(row["app_theme"]);
            _homepageGraph = DbToString(row["homepage_graph"]);
            _homepageCaption = DbToString(row["homepage_caption"]);
            _pageTitleText = DbToString(row["page_title_text"]);
            _pageTitleImage = DbToString(row["page_title_image"]);
            _menuRootText = DbToString(row["menu_root_text"]);
            _displayOrder = DbToInt(row["display_order"]);
            _createTime = DbToString(row["create_time"]);
            _creator = DbToString(row["creator"]);
            _modifyTime = DbToString(row["modify_time"]);
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
            sLine = "sub_system_id\x9" + _subSystemId;
            writer.WriteLine(sLine);

            sLine = "sub_system_name\x9" + _subSystemName;
            writer.WriteLine(sLine);

            sLine = "sub_system_color\x9" + _subSystemColor;
            writer.WriteLine(sLine);

            sLine = "full_name\x9" + _fullName;
            writer.WriteLine(sLine);

            sLine = "app_theme\x9" + _appTheme;
            writer.WriteLine(sLine);

            sLine = "homepage_graph\x9" + _homepageGraph;
            writer.WriteLine(sLine);

            sLine = "homepage_caption\x9" + _homepageCaption;
            writer.WriteLine(sLine);

            sLine = "page_title_text\x9" + _pageTitleText;
            writer.WriteLine(sLine);

            sLine = "page_title_image\x9" + _pageTitleImage;
            writer.WriteLine(sLine);

            sLine = "menu_root_text\x9" + _menuRootText;
            writer.WriteLine(sLine);

            sLine = "display_order\x9" + _displayOrder.ToString();
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
            sLine = "sub_system_id\tsub_system_name\tsub_system_color\t";
            sLine += "full_name\tapp_theme\thomepage_graph\t";
            sLine += "homepage_caption\tpage_title_text\tpage_title_image\t";
            sLine += "menu_root_text\tdisplay_order\tcreate_time\t";
            sLine += "creator\tmodify_time\tremarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL = "select sub_system_id,   sub_system_name, sub_system_color,\n";
            sSQL += "       full_name,       app_theme,       homepage_graph,  \n";
            sSQL += "       homepage_caption,page_title_text, page_title_image,\n";
            sSQL += "       menu_root_text,  display_order,   create_time,     \n";
            sSQL += "       creator,         modify_time,     remarks         \n";
            sSQL += "  from sbt_sys_sub_system_define";

            //======== 4. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. 处理每一行 ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 处理每一列 ========
                for (nCol = 0; nCol < 15; nCol++)
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
                case "sub_system_id":
                    return "varchar";
                case "sub_system_name":
                    return "varchar";
                case "sub_system_color":
                    return "varchar";
                case "full_name":
                    return "varchar";
                case "app_theme":
                    return "varchar";
                case "homepage_graph":
                    return "varchar";
                case "homepage_caption":
                    return "varchar";
                case "page_title_text":
                    return "varchar";
                case "page_title_image":
                    return "varchar";
                case "menu_root_text":
                    return "varchar";
                case "display_order":
                    return "int";
                case "create_time":
                    return "varchar";
                case "creator":
                    return "varchar";
                case "modify_time":
                    return "varchar";
                case "remarks":
                    return "varchar";
                default:
                    throw new Exception("TbSysSubSystemDefineBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 获取一个字段的CSharp类型
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "sub_system_id":
                    return "string";
                case "sub_system_name":
                    return "string";
                case "sub_system_color":
                    return "string";
                case "full_name":
                    return "string";
                case "app_theme":
                    return "string";
                case "homepage_graph":
                    return "string";
                case "homepage_caption":
                    return "string";
                case "page_title_text":
                    return "string";
                case "page_title_image":
                    return "string";
                case "menu_root_text":
                    return "string";
                case "display_order":
                    return "int";
                case "create_time":
                    return "string";
                case "creator":
                    return "string";
                case "modify_time":
                    return "string";
                case "remarks":
                    return "string";
                default:
                    throw new Exception("TbSysSubSystemDefineBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL = "select " + toFieldName + " from sbt_sys_sub_system_define \n";
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
