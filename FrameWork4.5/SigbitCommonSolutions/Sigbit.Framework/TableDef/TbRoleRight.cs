using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework
{
    /// <summary>
    /// 角色权限表(sbt_role_right)的表操作用户类
    /// </summary>
    public class TbRoleRight : TbRoleRightBase
    {
        #region 用户可编辑区域

        public static void ClearAllRightsOfRole(string sRoleUid)
        {
            string sSQL = "delete from sbt_role_right "
                    + " where role_uid = " + StringUtil.QuotedToDBStr(sRoleUid);
            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }


    /// <summary>
    /// 角色权限表(sbt_role_right)的字段名类
    /// </summary>
    public class TbRoleRightF
    {
        public const string TableName = "sbt_role_right";
        public const string RoleUid = "role_uid";
        public const string MenuCode = "menu_code";
    }


    /// <summary>
    /// 角色权限表(sbt_role_right)的表操作基类
    /// </summary>
    public class TbRoleRightBase : Sigbit.Data.TableBase
    {
        #region 私有变量定义
        protected string _roleUid = "";
        protected string _menuCode = "";
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public TbRoleRightBase()
        {
            ResetData();
        }

        #region 属性定义
        /// <summary>
        /// 角色标识，主键
        /// </summary>
        public string RoleUid
        {
            get
            {
                return _roleUid;
            }
            set
            {
                _roleUid = value;
            }
        }

        /// <summary>
        /// 权限，主键
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
        #endregion

        #region 变量的清零及输出
        /// <summary>
        /// 得到数据的HTML显示文本
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("RoleUid: " + this._roleUid + "<br>");
            sb.Append("MenuCode: " + this._menuCode + "<br>");
            return sb.ToString();
        }

        /// <summary>
        /// 清零本记录的数据
        /// </summary>
        public override void ResetData()
        {
            _roleUid = "";
            _menuCode = "";
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
            throw new Exception("TbRoleRight.Fetch() : 没有除主键外的数据，不让Fetch()");
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        public override void Insert()
        {
            string sSQL;

            sSQL = "insert into sbt_role_right \n";
            sSQL += "( role_uid,         menu_code         \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_roleUid) + "," + Quote(_menuCode) + ")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// 按主键删除一条数据
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL = "delete from sbt_role_right \n";
            sSQL += "  where role_uid = " + Quote(_roleUid) + "\n";
            sSQL += "    and menu_code = " + Quote(_menuCode) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbRoleRight.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// 按主键更新一条数据
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL = "update sbt_role_right set \n";
            sSQL += " role_uid = " + Quote(_roleUid) + ",\n";
            sSQL += " menu_code = " + Quote(_menuCode) + "\n";
            sSQL += "  where role_uid = " + Quote(_roleUid) + "\n";
            sSQL += "    and menu_code = " + Quote(_menuCode) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbRoleRight.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from sbt_role_right \n";
            sSQL += "  where role_uid = " + Quote(_roleUid) + "\n";
            sSQL += "    and menu_code = " + Quote(_menuCode) + "\n";

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
        public void FetchByE(string roleUid, string menuCode)
        {
            bool hasData;
            hasData = FetchBy(roleUid, menuCode);
            if (!hasData)
                throw new Exception("TbRoleRight.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// 以主键为参数获取一条数据
        /// </summary>
        /// <returns>是否访问到数据</returns>
        public bool FetchBy(string roleUid, string menuCode)
        {
            _roleUid = roleUid;
            _menuCode = menuCode;

            return Fetch(true);
        }

        /// <summary>
        /// 以主键为参数获取数据，并创建类的实例
        /// </summary>
        /// <returns>类的实例</returns>
        static public TbRoleRight CreateBy(string roleUid, string menuCode)
        {
            TbRoleRight tbl;
            bool hasData;

            tbl = new TbRoleRight();
            hasData = tbl.FetchBy(roleUid, menuCode);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// 以主键为参数删除一条数据
        /// </summary>
        static public void DeleteBy(string roleUid, string menuCode)
        {
            TbRoleRight tbl;
            tbl = new TbRoleRight();

            tbl.RoleUid = roleUid;
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
            _roleUid = DbToString(row["role_uid"]);
            _menuCode = DbToString(row["menu_code"]);
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
            sLine = "role_uid\x9" + _roleUid;
            writer.WriteLine(sLine);

            sLine = "menu_code\x9" + _menuCode;
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
            sLine = "role_uid\tmenu_code";
            writer.WriteLine(sLine);

            //======== 3. 得到SQL语句 ========
            sSQL = "select role_uid,        menu_code       \n";
            sSQL += "  from sbt_role_right";

            //======== 4. 运行SQL语句 ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. 处理每一行 ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 处理每一列 ========
                for (nCol = 0; nCol < 2; nCol++)
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
                case "role_uid":
                    return "varchar";
                case "menu_code":
                    return "varchar";
                default:
                    throw new Exception("TbRoleRightBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// 获取一个字段的CSharp类型
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "role_uid":
                    return "string";
                case "menu_code":
                    return "string";
                default:
                    throw new Exception("TbRoleRightBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL = "select " + toFieldName + " from sbt_role_right \n";
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
