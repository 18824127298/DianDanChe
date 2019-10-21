using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework
{
    /// <summary>
    /// (sbt_user_right)�ı�����û���
    /// </summary>
    public class TbUserRight : TbUserRightBase
    {
        #region �û��ɱ༭����

        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }


    /// <summary>
    /// (sbt_user_right)���ֶ�����
    /// </summary>
    public class TbUserRightF
    {
        public const string TableName = "sbt_user_right";
        public const string UserUid = "user_uid";
        public const string MenuCode = "menu_code";
    }


    /// <summary>
    /// (sbt_user_right)�ı��������
    /// </summary>
    public class TbUserRightBase : Sigbit.Data.TableBase
    {
        #region ˽�б�������
        protected string _userUid = "";
        protected string _menuCode = "";
        #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public TbUserRightBase()
        {
            ResetData();
        }

        #region ���Զ���
        /// <summary>
        /// �û���ʶ������
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
        /// Ȩ�ޣ�����
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

        #region ���������㼰���
        /// <summary>
        /// �õ����ݵ�HTML��ʾ�ı�
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UserUid: " + this._userUid + "<br>");
            sb.Append("MenuCode: " + this._menuCode + "<br>");
            return sb.ToString();
        }

        /// <summary>
        /// ���㱾��¼������
        /// </summary>
        public override void ResetData()
        {
            _userUid = "";
            _menuCode = "";
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
            sSQL = "select * \n";
            sSQL += "  from sbt_user_right    \n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";
            sSQL += "    and menu_code = " + Quote(_menuCode) + "\n";

            //======= 2. ����SQL��� ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbUserRight.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. ��ȡ��¼ ========
            row = dataSet.Tables[0].Rows[0];
            return true;
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public override void Insert()
        {
            string sSQL;

            sSQL = "insert into sbt_user_right \n";
            sSQL += "( user_uid,         menu_code         \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_userUid) + "," + Quote(_menuCode) + ")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// ������ɾ��һ������
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL = "delete from sbt_user_right \n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";
            sSQL += "    and menu_code = " + Quote(_menuCode) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbUserRight.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// ����������һ������
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL = "update sbt_user_right set \n";
            sSQL += " user_uid = " + Quote(_userUid) + ",\n";
            sSQL += " menu_code = " + Quote(_menuCode) + "\n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";
            sSQL += "    and menu_code = " + Quote(_menuCode) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbUserRight.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from sbt_user_right \n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";
            sSQL += "    and menu_code = " + Quote(_menuCode) + "\n";

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
        public void FetchByE(string userUid, string menuCode)
        {
            bool hasData;
            hasData = FetchBy(userUid, menuCode);
            if (!hasData)
                throw new Exception("TbUserRight.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// ������Ϊ������ȡһ������
        /// </summary>
        /// <returns>�Ƿ���ʵ�����</returns>
        public bool FetchBy(string userUid, string menuCode)
        {
            _userUid = userUid;
            _menuCode = menuCode;

            return Fetch(true);
        }

        /// <summary>
        /// ������Ϊ������ȡ���ݣ����������ʵ��
        /// </summary>
        /// <returns>���ʵ��</returns>
        static public TbUserRight CreateBy(string userUid, string menuCode)
        {
            TbUserRight tbl;
            bool hasData;

            tbl = new TbUserRight();
            hasData = tbl.FetchBy(userUid, menuCode);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// ������Ϊ����ɾ��һ������
        /// </summary>
        static public void DeleteBy(string userUid, string menuCode)
        {
            TbUserRight tbl;
            tbl = new TbUserRight();

            tbl.UserUid = userUid;
            tbl.MenuCode = menuCode;

            tbl.Delete();
        }

        #endregion

        #region �ļ��ͷ��������ص�֧�ֺ���
        /// <summary>
        /// ͨ��DataRow���и�ֵ
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _userUid = DbToString(row["user_uid"]);
            _menuCode = DbToString(row["menu_code"]);
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
            sLine = "user_uid\x9" + _userUid;
            writer.WriteLine(sLine);

            sLine = "menu_code\x9" + _menuCode;
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
            sLine = "user_uid\tmenu_code";
            writer.WriteLine(sLine);

            //======== 3. �õ�SQL��� ========
            sSQL = "select user_uid,        menu_code       \n";
            sSQL += "  from sbt_user_right";

            //======== 4. ����SQL��� ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. ����ÿһ�� ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 ����ÿһ�� ========
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
                case "user_uid":
                    return "varchar";
                case "menu_code":
                    return "varchar";
                default:
                    throw new Exception("TbUserRightBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// ��ȡһ���ֶε�CSharp����
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "user_uid":
                    return "string";
                case "menu_code":
                    return "string";
                default:
                    throw new Exception("TbUserRightBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL = "select " + toFieldName + "from sbt_user_right \n";
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
