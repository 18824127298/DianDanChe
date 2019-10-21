using System;
using System.Data;
using System.Text;
using System.IO;

using System.Collections;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework
{
    public class TbUserDeptList : ArrayList
    {
        public void AddDept(TbUserDept tblDept)
        {
            Add(tblDept);
        }

        public TbUserDept GetDept(int nIndex)
        {
            return (TbUserDept)this[nIndex];
        }
    }

    /// <summary>
    /// ������Ϣ(sbt_user_dept)�ı�����û���
    /// </summary>
    public class TbUserDept : TbUserDeptBase
    {
        #region �û��ɱ༭����
        /// <summary>
        /// �õ��Ӳ��ŵ��б�
        /// </summary>
        /// <returns>�Ӳ��ŵ��б�</returns>
        public TbUserDeptList GetChildDepts()
        {
            TbUserDeptList childList = new TbUserDeptList();

            string sSQL = "select * from sbt_user_dept "
                    + " where dept_id like " + Quote(this.DeptId + "%")
                    + " and dept_level = " + (this.DeptLevel + 1).ToString()
                    + " order by list_order, dept_name";
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TbUserDept tblDept = new TbUserDept();
                tblDept.AssignByDataRow(ds, i);
                childList.AddDept(tblDept);
            }

            return childList;
        }

        /// <summary>
        /// �õ����ŵĸ�����
        /// </summary>
        /// <param name="sDeptId">�����ű���</param>
        /// <returns>�����ű���</returns>
        public static string GetParentDeptId(string sDeptId)
        {
            if (sDeptId.Length % 3 != 0)
                throw new Exception("���ű���ĳ��ȱ�����3�ı���");

            if (sDeptId == "")
                return "";

            return sDeptId.Substring(0, sDeptId.Length - 3);
        }

        /// <summary>
        /// ���²�����Ϣ��������Ҫ�������е��Ӳ���ȫ��
        /// </summary>
        public override void Update()
        {
            //======== 1. ����ȫ�� ============
            TbUserDept tblOldDept = new TbUserDept();
            tblOldDept.DeptId = this.DeptId;
            tblOldDept.Fetch();

            if (tblOldDept.DeptName != this.DeptName)
            {
                //========== 2. �õ������ŵ�ȫ�� ============
                string sParentDeptId = GetParentDeptId(this.DeptId);
                string sParentFullNamePrefix = "";
                if (sParentDeptId != "")
                {
                    TbUserDept tblParent = new TbUserDept();
                    tblParent.DeptId = sParentDeptId;
                    tblParent.Fetch();

                    sParentFullNamePrefix = tblParent.FullDeptName + "/";
                }

                //========= 3. ���±������µ����в���ȫ�� =========
                //======== 3.1 ��ȡ�������µ����в��� ==========
                string sSQL = "select * from sbt_user_dept "
                        + " where dept_id like " + Quote(this.DeptId + "%")
                        + " and dept_level > " + this.DeptLevel.ToString();
                DataSet dsChilds = DataHelper.Instance.ExecuteDataSet(sSQL);
                for (int i = 0; i < dsChilds.Tables[0].Rows.Count; i++)
                {
                    TbUserDept tblChild = new TbUserDept();
                    tblChild.AssignByDataRow(dsChilds, i);

                    string sNewFullName = GetNewFullName(tblChild.FullDeptName, 
                            sParentFullNamePrefix + this.DeptName);
                    tblChild.FullDeptName = sNewFullName;
                    tblChild.Update();
                }

                //========= 4. ���±����ȫ�� ===========
                this.FullDeptName = sParentFullNamePrefix + this.DeptName;
            }

            //======== 5. ���±��� =============
            base.Update();
            SbtCodeTables.DeptTree.Clear();
        }

        public override void Insert()
        {
            base.Insert();
            SbtCodeTables.DeptTree.Clear();
        }

        public override void Delete()
        {
            base.Delete();
            SbtCodeTables.DeptTree.Clear();
        }

        /// <summary>
        /// �õ��µĲ���ȫ��
        /// </summary>
        /// <param name="sRawFullName">ԭ�е�ȫ��</param>
        /// <param name="sNewParentFullName">�µĸ��ڵ�ȫ��</param>
        /// <returns></returns>
        private string GetNewFullName(string sRawFullName, string sNewParentFullName)
        {
            //======== 1. �õ���ȫ��"/"������ ===========
            int nParentSlashCount = StringUtil.Occurs("/", sNewParentFullName);
            nParentSlashCount++;

            //========= 2. �õ��ӽڵ���Ӧ����������� ============
            string sTailRaw = sRawFullName;
            for (int i = 0; i < nParentSlashCount; i++)
            {
                int nSlashPos = sTailRaw.IndexOf("/");
                if (nSlashPos < 0)
                    return sRawFullName;

                sTailRaw = sTailRaw.Substring(nSlashPos + 1);
            }

            return sNewParentFullName + "/" + sTailRaw;
        }

        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }


    /// <summary>
    /// ������Ϣ(sbt_user_dept)���ֶ�����
    /// </summary>
    public class TbUserDeptF
    {
        public const string TableName = "sbt_user_dept";
        public const string DeptId = "dept_id";
        public const string DeptName = "dept_name";
        public const string ThirdParyCode = "third_pary_code";
        public const string FullDeptName = "full_dept_name";
        public const string DeptLevel = "dept_level";
        public const string HasChild = "has_child";
        public const string CreateTime = "create_time";
        public const string Creator = "creator";
        public const string IsActive = "is_active";
        public const string ListOrder = "list_order";
        public const string Remarks = "remarks";
    }


    /// <summary>
    /// ������Ϣ(sbt_user_dept)�ı��������
    /// </summary>
    public class TbUserDeptBase : Sigbit.Data.TableBase
    {
        #region ˽�б�������
        protected string _deptId = "";
        protected string _deptName = "";
        protected string _thirdParyCode = "";
        protected string _fullDeptName = "";
        protected int _deptLevel;
        protected string _hasChild = "";
        protected string _createTime = "";
        protected string _creator = "";
        protected string _isActive = "";
        protected int _listOrder;
        protected string _remarks = "";
        #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public TbUserDeptBase()
        {
            ResetData();
        }

        #region ���Զ���
        /// <summary>
        /// ����ID������
        /// </summary>
        public string DeptId
        {
            get
            {
                return _deptId;
            }
            set
            {
                _deptId = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string DeptName
        {
            get
            {
                return _deptName;
            }
            set
            {
                _deptName = value;
            }
        }

        /// <summary>
        /// ����������
        /// </summary>
        public string ThirdParyCode
        {
            get
            {
                return _thirdParyCode;
            }
            set
            {
                _thirdParyCode = value;
            }
        }

        /// <summary>
        /// ����ȫ��
        /// </summary>
        public string FullDeptName
        {
            get
            {
                return _fullDeptName;
            }
            set
            {
                _fullDeptName = value;
            }
        }

        /// <summary>
        /// ����λ�ã��㣩
        /// </summary>
        public int DeptLevel
        {
            get
            {
                return _deptLevel;
            }
            set
            {
                _deptLevel = value;
            }
        }

        /// <summary>
        /// �Ƿ�����
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
        /// ����ʱ��
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
        /// ������
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
        /// �Ƿ񼤻�
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
        /// ˳���
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
        /// ��ע
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

        #region ���������㼰���
        /// <summary>
        /// �õ����ݵ�HTML��ʾ�ı�
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DeptId: " + this._deptId + "<br>");
            sb.Append("DeptName: " + this._deptName + "<br>");
            sb.Append("ThirdParyCode: " + this._thirdParyCode + "<br>");
            sb.Append("FullDeptName: " + this._fullDeptName + "<br>");
            sb.Append("DeptLevel: " + this._deptLevel + "<br>");
            sb.Append("HasChild: " + this._hasChild + "<br>");
            sb.Append("CreateTime: " + this._createTime + "<br>");
            sb.Append("Creator: " + this._creator + "<br>");
            sb.Append("IsActive: " + this._isActive + "<br>");
            sb.Append("ListOrder: " + this._listOrder + "<br>");
            sb.Append("Remarks: " + this._remarks + "<br>");
            return sb.ToString();
        }

        /// <summary>
        /// ���㱾��¼������
        /// </summary>
        public override void ResetData()
        {
            _deptId = "";
            _deptName = "";
            _thirdParyCode = "";
            _fullDeptName = "";
            _deptLevel = 0;
            _hasChild = "";
            _createTime = "";
            _creator = "";
            _isActive = "";
            _listOrder = 0;
            _remarks = "";
        }

        #endregion

        #region ��������ɾ�Ĳ����
        /// <summary>
        /// ��������ȡһ�����ݣ��������������⣩
        /// </summary>
        public override void Fetch()
        {
            Fetch(false);
        }

        /// <summary>
        /// ��������ȡһ������
        /// </summary>
        /// <returns>�Ƿ���ʵ�����</returns>
        public override bool Fetch(bool allowNoData)
        {
            string sSQL;
            int nRecordCount;
            DataSet dataSet;
            DataRow row;

            //======= 1. �õ�SQL��� ==============
            sSQL = "select dept_name,           third_pary_code,     full_dept_name,       \n";
            sSQL += "       dept_level,          has_child,           create_time,          \n";
            sSQL += "       creator,             is_active,           list_order,           \n";
            sSQL += "       remarks              \n";
            sSQL += "  from sbt_user_dept    \n";
            sSQL += "  where dept_id = " + Quote(_deptId) + "\n";

            //======= 2. ����SQL��� ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbUserDept.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. ��ȡ��¼ ========
            row = dataSet.Tables[0].Rows[0];
            _deptName = DbToString(row["dept_name"]);
            _thirdParyCode = DbToString(row["third_pary_code"]);
            _fullDeptName = DbToString(row["full_dept_name"]);
            _deptLevel = DbToInt(row["dept_level"]);
            _hasChild = DbToString(row["has_child"]);
            _createTime = DbToString(row["create_time"]);
            _creator = DbToString(row["creator"]);
            _isActive = DbToString(row["is_active"]);
            _listOrder = DbToInt(row["list_order"]);
            _remarks = DbToString(row["remarks"]);
            return true;
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public override void Insert()
        {
            string sSQL;

            sSQL = "insert into sbt_user_dept \n";
            sSQL += "( dept_id,          dept_name,        \n";
            sSQL += "  third_pary_code,  full_dept_name,   \n";
            sSQL += "  dept_level,       has_child,        \n";
            sSQL += "  create_time,      creator,          \n";
            sSQL += "  is_active,        list_order,       \n";
            sSQL += "  remarks           \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_deptId) + "," + Quote(_deptName) + ",\n";
            sSQL += Quote(_thirdParyCode) + "," + Quote(_fullDeptName) + ",\n";
            sSQL += _deptLevel.ToString() + "," + Quote(_hasChild) + ",\n";
            sSQL += Quote(_createTime) + "," + Quote(_creator) + ",\n";
            sSQL += Quote(_isActive) + "," + _listOrder.ToString() + ",\n";
            sSQL += Quote(_remarks) + ")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// ������ɾ��һ������
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL = "delete from sbt_user_dept \n";
            sSQL += "  where dept_id = " + Quote(_deptId) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbUserDept.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// ����������һ������
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL = "update sbt_user_dept set \n";
            sSQL += " dept_id = " + Quote(_deptId) + ",\n";
            sSQL += " dept_name = " + Quote(_deptName) + ",\n";
            sSQL += " third_pary_code = " + Quote(_thirdParyCode) + ",\n";
            sSQL += " full_dept_name = " + Quote(_fullDeptName) + ",\n";
            sSQL += " dept_level = " + _deptLevel.ToString() + ",\n";
            sSQL += " has_child = " + Quote(_hasChild) + ",\n";
            sSQL += " create_time = " + Quote(_createTime) + ",\n";
            sSQL += " creator = " + Quote(_creator) + ",\n";
            sSQL += " is_active = " + Quote(_isActive) + ",\n";
            sSQL += " list_order = " + _listOrder.ToString() + ",\n";
            sSQL += " remarks = " + Quote(_remarks) + "\n";
            sSQL += "  where dept_id = " + Quote(_deptId) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            //if (nRowsAffected != 1)
            //    throw new Exception("TbUserDept.Update() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// �������жϼ�¼�Ƿ����
        /// </summary>
        /// <returns>��¼�Ƿ����</returns>
        public override bool RecordExists()
        {
            string sSQL;
            int nRecordCount;

            //======= 1. �õ�SQL��� ==============
            sSQL = "select count(*) from sbt_user_dept \n";
            sSQL += "  where dept_id = " + Quote(_deptId) + "\n";

            //======= 2. ����SQL��� ========
            nRecordCount = (int)DataHelper.Instance.ExecuteScalar(sSQL);
            if (nRecordCount == 1)
                return true;
            else
                return false;
        }

        #endregion

        #region ������Ϊ�����Ĳ���
        /// <summary>
        /// ������Ϊ������ȡһ�����ݣ��������������⣩
        /// </summary>
        public void FetchByE(string deptId)
        {
            bool hasData;
            hasData = FetchBy(deptId);
            if (!hasData)
                throw new Exception("TbUserDept.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// ������Ϊ������ȡһ������
        /// </summary>
        /// <returns>�Ƿ���ʵ�����</returns>
        public bool FetchBy(string deptId)
        {
            _deptId = deptId;

            return Fetch(true);
        }

        /// <summary>
        /// ������Ϊ������ȡ���ݣ����������ʵ��
        /// </summary>
        /// <returns>���ʵ��</returns>
        static public TbUserDept CreateBy(string deptId)
        {
            TbUserDept tbl;
            bool hasData;

            tbl = new TbUserDept();
            hasData = tbl.FetchBy(deptId);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// ������Ϊ����ɾ��һ������
        /// </summary>
        static public void DeleteBy(string deptId)
        {
            TbUserDept tbl;
            tbl = new TbUserDept();

            tbl.DeptId = deptId;

            tbl.Delete();
        }

        #endregion

        #region �ļ��ͷ��������ص�֧�ֺ���
        /// <summary>
        /// ͨ��DataRow���и�ֵ
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _deptId = DbToString(row["dept_id"]);
            _deptName = DbToString(row["dept_name"]);
            _thirdParyCode = DbToString(row["third_pary_code"]);
            _fullDeptName = DbToString(row["full_dept_name"]);
            _deptLevel = DbToInt(row["dept_level"]);
            _hasChild = DbToString(row["has_child"]);
            _createTime = DbToString(row["create_time"]);
            _creator = DbToString(row["creator"]);
            _isActive = DbToString(row["is_active"]);
            _listOrder = DbToInt(row["list_order"]);
            _remarks = DbToString(row["remarks"]);
        }

        /// <summary>
        /// ͨ��DataSet���и�ֵ
        /// </summary>
        /// <param name="dataSet">���ݼ�</param>
        /// <param name="rowNum">�к�</param>
        public override void AssignByDataRow(DataSet dataSet, int rowNum)
        {
            DataRow row;
            row = dataSet.Tables[0].Rows[rowNum];

            AssignByDataRow(row);
        }

        /// <summary>
        /// ����ǰ��¼����Ϣ���浽�ļ�
        /// </summary>
        /// </param name="fileName">���浽���ļ���</param>
        public override void DumpToFile(string fileName)
        {
            //========= 1. ���ļ� ============
            StreamWriter writer;
            string sLine;
            writer = File.CreateText(fileName);

            //========= 2. д���ļ� ============
            sLine = "dept_id\x9" + _deptId;
            writer.WriteLine(sLine);

            sLine = "dept_name\x9" + _deptName;
            writer.WriteLine(sLine);

            sLine = "third_pary_code\x9" + _thirdParyCode;
            writer.WriteLine(sLine);

            sLine = "full_dept_name\x9" + _fullDeptName;
            writer.WriteLine(sLine);

            sLine = "dept_level\x9" + _deptLevel.ToString();
            writer.WriteLine(sLine);

            sLine = "has_child\x9" + _hasChild;
            writer.WriteLine(sLine);

            sLine = "create_time\x9" + _createTime;
            writer.WriteLine(sLine);

            sLine = "creator\x9" + _creator;
            writer.WriteLine(sLine);

            sLine = "is_active\x9" + _isActive;
            writer.WriteLine(sLine);

            sLine = "list_order\x9" + _listOrder.ToString();
            writer.WriteLine(sLine);

            sLine = "remarks\x9" + _remarks;
            writer.WriteLine(sLine);

            //========= 3. �ر��ļ� ============
            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// �����е����м�¼���浽�ļ�
        /// </summary>
        /// </param name="fileName">���浽���ļ���</param>
        public override void DumpAllRecordsToFile(string fileName)
        {
            string sSQL;
            int i, nCol, nRecordCount;
            DataSet dataSet;
            DataRow row;
            string sFieldValue, sLine;
            StreamWriter writer;

            //======== 1. ���ļ� ========
            writer = File.CreateText(fileName);

            //======== 2. д���һ�У������У� ========
            sLine = "dept_id\tdept_name\tthird_pary_code\t";
            sLine += "full_dept_name\tdept_level\thas_child\t";
            sLine += "create_time\tcreator\tis_active\t";
            sLine += "list_order\tremarks";
            writer.WriteLine(sLine);

            //======== 3. �õ�SQL��� ========
            sSQL = "select dept_id,         dept_name,       third_pary_code, \n";
            sSQL += "       full_dept_name,  dept_level,      has_child,       \n";
            sSQL += "       create_time,     creator,         is_active,       \n";
            sSQL += "       list_order,      remarks         \n";
            sSQL += "  from sbt_user_dept";

            //======== 4. ����SQL��� ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. ����ÿһ�� ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 ����ÿһ�� ========
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

                //======== 5.2 ��һ�е�ֵд���ļ� ========
                writer.WriteLine(sLine);
            }

            //======== 6. �ر��ļ� ========
            writer.Flush();
            writer.Close();
        }

        #endregion

        #region ����֧�ֺ���
        /// <summary>
        /// ��ȡһ���ֶε����ݿ�����
        /// </summary>
        static public string DBTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "dept_id":
                    return "varchar";
                case "dept_name":
                    return "varchar";
                case "third_pary_code":
                    return "varchar";
                case "full_dept_name":
                    return "varchar";
                case "dept_level":
                    return "int";
                case "has_child":
                    return "char";
                case "create_time":
                    return "varchar";
                case "creator":
                    return "varchar";
                case "is_active":
                    return "varchar";
                case "list_order":
                    return "int";
                case "remarks":
                    return "varchar";
                default:
                    throw new Exception("TbUserDeptBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// ��ȡһ���ֶε�CSharp����
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "dept_id":
                    return "string";
                case "dept_name":
                    return "string";
                case "third_pary_code":
                    return "string";
                case "full_dept_name":
                    return "string";
                case "dept_level":
                    return "int";
                case "has_child":
                    return "string";
                case "create_time":
                    return "string";
                case "creator":
                    return "string";
                case "is_active":
                    return "string";
                case "list_order":
                    return "int";
                case "remarks":
                    return "string";
                default:
                    throw new Exception("TbUserDeptBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// ͨ��һ���ֶε�ֵ���ʵõ���һ���ֶε�ֵ
        /// </summary>
        static public string GetFieldValueBy(string fromFieldName, string fromFieldValue, string toFieldName)
        {
            string sSQL;
            int nRecordCount;
            DataSet dataSet;
            DataRow row;

            //======= 1. �õ�SQL��� ==============
            sSQL = "select " + toFieldName + "from sbt_user_dept \n";
            sSQL += "where " + fromFieldName + " = ";
            if (CSharpTypeOfFieldName(fromFieldName) == "string")
                sSQL += "'" + fromFieldValue + "'";
            else
                sSQL += fromFieldValue;

            //======= 2. ����SQL��� ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount == 0)
                return "";

            //======= 3. ��ȡ��¼ ========
            row = dataSet.Tables[0].Rows[0];
            return row[toFieldName].ToString();
        }

        #endregion

    }
}
