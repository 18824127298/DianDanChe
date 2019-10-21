using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework
{
    /// <summary>
    /// 菜单结构表(sbt_sys_menu)的表操作用户类
    /// </summary>
    public class TbSysMenu : TbSysMenuBase
    {
        #region 用户可编辑区域

        public static DataSet GetDataSetOfAll()
        {
            string sSQL = "select * from sbt_sys_menu order by list_order, menu_name";
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);
            return ds;
        }
        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }


    /// <summary>
    /// 菜单结构表(sbt_sys_menu)的字段名类
    /// </summary>
    public class TbSysMenuF
    {
        public const string TableName = "sbt_sys_menu";
        public const string MenuCode = "menu_code";
        public const string MenuName = "menu_name";
        public const string MenuLink = "menu_link";
        public const string MenuClass = "menu_class";
        public const string MenuStyle = "menu_style";
        public const string MenuIcon = "menu_icon";
        public const string ParentMenuCode = "parent_menu_code";
        public const string LevelNum = "level_num";
        public const string ListOrder = "list_order";
        public const string IsActive = "is_active";
        public const string HasChild = "has_child";
        public const string IsMenuItem = "is_menu_item";
        public const string IsLogItem = "is_log_item";
        public const string IsRightItem = "is_right_item";
        public const string Remarks = "remarks";
    }


    /// <summary>
    /// 菜单结构表(sbt_sys_menu)的表操作基类
    /// </summary>
    public class TbSysMenuBase : Sigbit.Data.TableBase
    {
        #region 私有变量定义
        protected string _menuCode = "";
        protected string _menuName = "";
        protected string _menuLink = "";
        protected string _menuClass = "";
        protected string _menuStyle = "";
        protected string _menuIcon = "";
        protected string _parentMenuCode = "";
        protected int _levelNum;
        protected int _listOrder;
        protected string _isActive = "";
        protected string _hasChild = "";
        protected string _isMenuItem = "";
        protected string _isLogItem = "";
        protected string _isRightItem = "";
        protected string _remarks = "";
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public TbSysMenuBase()
        {
            ResetData();
        }

        #region 属性定义
        /// <summary>
        /// 菜单编码，主键
        /// </summary>
        public string MenuCode
        {
            get
            {
                return _menuCode;
            }
            set
            {
                _menuCode = value;
            }
        }

        /// <summary>
        /// 菜单中文名
        /// </summary>
        public string MenuName
        {
            get
            {
                return _menuName;
            }
            set
            {
                _menuName = value;
            }
        }

        /// <summary>
        /// 页面URL
        /// </summary>
        public string MenuLink
        {
            get
            {
                return _menuLink;
            }
            set
            {
                _menuLink = value;
            }
        }

        /// <summary>
        /// 菜单类别
        /// </summary>
        public string MenuClass
        {
            get
            {
                return _menuClass;
            }
            set
            {
                _menuClass = value;
            }
        }

        /// <summary>
        /// 菜单风格
        /// </summary>
        public string MenuStyle
        {
            get
            {
                return _menuStyle;
            }
            set
            {
                _menuStyle = value;
            }
        }

        /// <summary>
        /// 菜单的图像文件
        /// </summary>
        public string MenuIcon
        {
            get
            {
                return _menuIcon;
            }
            set
            {
                _menuIcon = value;
            }
        }

        /// <summary>
        /// 上级菜单编码
        /// </summary>
        public string ParentMenuCode
        {
            get
            {
                return _parentMenuCode;
            }
            set
            {
                _parentMenuCode = value;
            }
        }

        /// <summary>
        /// 所处位置(层)，从1始
        /// </summary>
        public int LevelNum
        {
            get
            {
                return _levelNum;
            }
            set
            {
                _levelNum = value;
            }
        }

        /// <summary>
        /// 排序索引
        /// </summary>
        public int ListOrder
        {
            get
            {
                return _listOrder;
            }
            set
            {
                _listOrder = value;
            }
        }

        /// <summary>
        /// 是否激活(Y/N)
        /// </summary>
        public string IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
            }
        }

        /// <summary>
        /// 是否有子项(Y/N)
        /// </summary>
        public string HasChild
        {
            get
            {
                return _hasChild;
            }
            set
            {
                _hasChild = value;
            }
        }

        /// <summary>
        /// 是否菜单项
        /// </summary>
        public string IsMenuItem
        {
            get
            {
                return _isMenuItem;
            }
            set
            {
                _isMenuItem = value;
            }
        }

        /// <summary>
        /// 是否日志项
        /// </summary>
        public string IsLogItem
        {
            get
            {
                return _isLogItem;
            }
            set
            {
                _isLogItem = value;
            }
        }

        /// <summary>
        /// 是否权限项
        /// </summary>
        public string IsRightItem
        {
            get
            {
                return _isRightItem;
            }
            set
            {
                _isRightItem = value;
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
            sb.Append("MenuCode: " + this._menuCode + "<br>");
            sb.Append("MenuName: " + this._menuName + "<br>");
            sb.Append("MenuLink: " + this._menuLink + "<br>");
            sb.Append("MenuClass: " + this._menuClass + "<br>");
            sb.Append("MenuStyle: " + this._menuStyle + "<br>");
            sb.Append("MenuIcon: " + this._menuIcon + "<br>");
            sb.Append("ParentMenuCode: " + this._parentMenuCode + "<br>");
            sb.Append("LevelNum: " + this._levelNum + "<br>");
            sb.Append("ListOrder: " + this._listOrder + "<br>");
            sb.Append("IsActive: " + this._isActive + "<br>");
            sb.Append("HasChild: " + this._hasChild + "<br>");
            sb.Append("IsMenuItem: " + this._isMenuItem + "<br>");
            sb.Append("IsLogItem: " + this._isLogItem + "<br>");
            sb.Append("IsRightItem: " + this._isRightItem + "<br>");
            sb.Append("Remarks: " + this._remarks + "<br>");
            return sb.ToString();
        }

        /// <summary>
        /// 清零本记录的数据
        /// </summary>
        public override void ResetData()
        {
            _menuCode = "";
            _menuName = "";
            _menuLink = "";
            _menuClass = "";
            _menuStyle = "";
            _menuIcon = "";
            _parentMenuCode = "";
            _levelNum = 0;
            _listOrder = 0;
            _isActive = "";
            _hasChild = "";
            _isMenuItem = "";
            _isLogItem = "";
            _isRightItem = "";
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
            sSQL = "select menu_name,           menu_link,           menu_class,           \n";
            sSQL += "       menu_style,          menu_icon,           parent_menu_code,     \n";
            sSQL += "       level_num,           list_order,          is_active,            \n";
            sSQL += "       has_child,           is_menu_item,        is_log_item,          \n";
            sSQL += "       is_right_item,       remarks              \n";
            sSQL += "  from sbt_sys_menu    \n";
            sSQL += "  where menu_code = " + Quote(_menuCode) + "\n";

            //======= 2. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbSysMenu.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. 读取记录 ========
            row = dataSet.Tables[0].Rows[0];
            _menuName = DbToString(row["menu_name"]);
            _menuLink = DbToString(row["menu_link"]);
            _menuClass = DbToString(row["menu_class"]);
            _menuStyle = DbToString(row["menu_style"]);
            _menuIcon = DbToString(row["menu_icon"]);
            _parentMenuCode = DbToString(row["parent_menu_code"]);
            _levelNum = DbToInt(row["level_num"]);
            _listOrder = DbToInt(row["list_order"]);
            _isActive = DbToString(row["is_active"]);
            _hasChild = DbToString(row["has_child"]);
            _isMenuItem = DbToString(row["is_menu_item"]);
            _isLogItem = DbToString(row["is_log_item"]);
            _isRightItem = DbToString(row["is_right_item"]);
            _remarks = DbToString(row["remarks"]);
            return true;
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        public override void Insert()
        {
            string sSQL;

            sSQL = "insert into sbt_sys_menu \n";
            sSQL += "( menu_code,        menu_name,        \n";
            sSQL += "  menu_link,        menu_class,       \n";
            sSQL += "  menu_style,       menu_icon,        \n";
            sSQL += "  parent_menu_code, level_num,        \n";
            sSQL += "  list_order,       is_active,        \n";
            sSQL += "  has_child,        is_menu_item,     \n";
            sSQL += "  is_log_item,      is_right_item,    \n";
            sSQL += "  remarks           \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_menuCode) + "," + Quote(_menuName) + ",\n";
            sSQL += Quote(_menuLink) + "," + Quote(_menuClass) + ",\n";
            sSQL += Quote(_menuStyle) + "," + Quote(_menuIcon) + ",\n";
            sSQL += Quote(_parentMenuCode) + "," + _levelNum.ToString() + ",\n";
            sSQL += _listOrder.ToString() + "," + Quote(_isActive) + ",\n";
            sSQL += Quote(_hasChild) + "," + Quote(_isMenuItem) + ",\n";
            sSQL += Quote(_isLogItem) + "," + Quote(_isRightItem) + ",\n";
            sSQL += Quote(_remarks) + ")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 按主键删除一条数据
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL = "delete from sbt_sys_menu \n";
            sSQL += "  where menu_code = " + Quote(_menuCode) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysMenu.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL = "update sbt_sys_menu set \n";
            sSQL += " menu_code = " + Quote(_menuCode) + ",\n";
            sSQL += " menu_name = " + Quote(_menuName) + ",\n";
            sSQL += " menu_link = " + Quote(_menuLink) + ",\n";
            sSQL += " menu_class = " + Quote(_menuClass) + ",\n";
            sSQL += " menu_style = " + Quote(_menuStyle) + ",\n";
            sSQL += " menu_icon = " + Quote(_menuIcon) + ",\n";
            sSQL += " parent_menu_code = " + Quote(_parentMenuCode) + ",\n";
            sSQL += " level_num = " + _levelNum.ToString() + ",\n";
            sSQL += " list_order = " + _listOrder.ToString() + ",\n";
            sSQL += " is_active = " + Quote(_isActive) + ",\n";
            sSQL += " has_child = " + Quote(_hasChild) + ",\n";
            sSQL += " is_menu_item = " + Quote(_isMenuItem) + ",\n";
            sSQL += " is_log_item = " + Quote(_isLogItem) + ",\n";
            sSQL += " is_right_item = " + Quote(_isRightItem) + ",\n";
            sSQL += " remarks = " + Quote(_remarks) + "\n";
            sSQL += "  where menu_code = " + Quote(_menuCode) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            //if (nRowsAffected != 1)
            //    throw new Exception("TbSysMenu.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from sbt_sys_menu \n";
            sSQL += "  where menu_code = " + Quote(_menuCode) + "\n";

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
        public void FetchByE(string menuCode)
        {
            bool hasData;
            hasData = FetchBy(menuCode);
            if (!hasData)
                throw new Exception("TbSysMenu.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string menuCode)
        {
            _menuCode = menuCode;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbSysMenu CreateBy(string menuCode)
        {
            TbSysMenu tbl;
            bool hasData;

            tbl = new TbSysMenu();
            hasData = tbl.FetchBy(menuCode);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string menuCode)
        {
            TbSysMenu tbl;
            tbl = new TbSysMenu();

            tbl.MenuCode = menuCode;

            tbl.Delete();
        }

        #endregion

        #region 文件和访问组件相关的支持函数
        /// <summary>
        /// 通过DataRow进行赋值
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _menuCode = DbToString(row["menu_code"]);
            _menuName = DbToString(row["menu_name"]);
            _menuLink = DbToString(row["menu_link"]);
            _menuClass = DbToString(row["menu_class"]);
            _menuStyle = DbToString(row["menu_style"]);
            _menuIcon = DbToString(row["menu_icon"]);
            _parentMenuCode = DbToString(row["parent_menu_code"]);
            _levelNum = DbToInt(row["level_num"]);
            _listOrder = DbToInt(row["list_order"]);
            _isActive = DbToString(row["is_active"]);
            _hasChild = DbToString(row["has_child"]);
            _isMenuItem = DbToString(row["is_menu_item"]);
            _isLogItem = DbToString(row["is_log_item"]);
            _isRightItem = DbToString(row["is_right_item"]);
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
            sLine = "menu_code\x9" + _menuCode;
            writer.WriteLine(sLine);

            sLine = "menu_name\x9" + _menuName;
            writer.WriteLine(sLine);

            sLine = "menu_link\x9" + _menuLink;
            writer.WriteLine(sLine);

            sLine = "menu_class\x9" + _menuClass;
            writer.WriteLine(sLine);

            sLine = "menu_style\x9" + _menuStyle;
            writer.WriteLine(sLine);

            sLine = "menu_icon\x9" + _menuIcon;
            writer.WriteLine(sLine);

            sLine = "parent_menu_code\x9" + _parentMenuCode;
            writer.WriteLine(sLine);

            sLine = "level_num\x9" + _levelNum.ToString();
            writer.WriteLine(sLine);

            sLine = "list_order\x9" + _listOrder.ToString();
            writer.WriteLine(sLine);

            sLine = "is_active\x9" + _isActive;
            writer.WriteLine(sLine);

            sLine = "has_child\x9" + _hasChild;
            writer.WriteLine(sLine);

            sLine = "is_menu_item\x9" + _isMenuItem;
            writer.WriteLine(sLine);

            sLine = "is_log_item\x9" + _isLogItem;
            writer.WriteLine(sLine);

            sLine = "is_right_item\x9" + _isRightItem;
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
            sLine = "menu_code\tmenu_name\tmenu_link\t";
            sLine += "menu_class\tmenu_style\tmenu_icon\t";
            sLine += "parent_menu_code\tlevel_num\tlist_order\t";
            sLine += "is_active\thas_child\tis_menu_item\t";
            sLine += "is_log_item\tis_right_item\tremarks";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL = "select menu_code,       menu_name,       menu_link,       \n";
            sSQL += "       menu_class,      menu_style,      menu_icon,       \n";
            sSQL += "       parent_menu_code,level_num,       list_order,      \n";
            sSQL += "       is_active,       has_child,       is_menu_item,    \n";
            sSQL += "       is_log_item,     is_right_item,   remarks         \n";
            sSQL += "  from sbt_sys_menu";

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
                case "menu_code":
                    return "varchar";
                case "menu_name":
                    return "varchar";
                case "menu_link":
                    return "varchar";
                case "menu_class":
                    return "varchar";
                case "menu_style":
                    return "varchar";
                case "menu_icon":
                    return "varchar";
                case "parent_menu_code":
                    return "varchar";
                case "level_num":
                    return "int";
                case "list_order":
                    return "int";
                case "is_active":
                    return "varchar";
                case "has_child":
                    return "varchar";
                case "is_menu_item":
                    return "varchar";
                case "is_log_item":
                    return "varchar";
                case "is_right_item":
                    return "varchar";
                case "remarks":
                    return "varchar";
                default:
                    throw new Exception("TbSysMenuBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 获取一个字段的CSharp类型
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "menu_code":
                    return "string";
                case "menu_name":
                    return "string";
                case "menu_link":
                    return "string";
                case "menu_class":
                    return "string";
                case "menu_style":
                    return "string";
                case "menu_icon":
                    return "string";
                case "parent_menu_code":
                    return "string";
                case "level_num":
                    return "int";
                case "list_order":
                    return "int";
                case "is_active":
                    return "string";
                case "has_child":
                    return "string";
                case "is_menu_item":
                    return "string";
                case "is_log_item":
                    return "string";
                case "is_right_item":
                    return "string";
                case "remarks":
                    return "string";
                default:
                    throw new Exception("TbSysMenuBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL = "select " + toFieldName + "from sbt_sys_menu \n";
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
