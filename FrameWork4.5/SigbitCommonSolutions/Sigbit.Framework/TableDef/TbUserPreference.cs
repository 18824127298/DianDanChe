using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework
{
    /// <summary>
    /// �û�ƫ�����ñ�(sbt_user_preference)�ı�����û���
    /// </summary>
    public class TbUserPreference : TbUserPreferenceBase
    {
        #region �û��ɱ༭����

        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }


    /// <summary>
    /// �û�ƫ�����ñ�(sbt_user_preference)���ֶ�����
    /// </summary>
    public class TbUserPreferenceF
    {
        public const string TableName = "sbt_user_preference";
        public const string UserUid = "user_uid";
        public const string PreferenceClass = "preference_class";
        public const string PreferenceCode = "preference_code";
        public const string ModifyTime = "modify_time";
        public const string Remarks = "remarks";
    }


    /// <summary>
    /// �û�ƫ�����ñ�(sbt_user_preference)�ı��������
    /// </summary>
    public class TbUserPreferenceBase : Sigbit.Data.TableBase
    {
        #region ˽�б�������
        protected string _userUid = "";
        protected string _preferenceClass = "";
        protected string _preferenceCode = "";
        protected string _modifyTime = "";
        protected string _remarks = "";
        #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public TbUserPreferenceBase()
        {
            ResetData();
        }

        #region ���Զ���
        /// <summary>
        /// �û�ID������
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
        /// ƫ�õ����ͣ�����
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
        /// ƫ�õ�ȡֵ����
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
        /// �޸�ʱ��
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
            sb.Append("UserUid: " + this._userUid + "<br>");
            sb.Append("PreferenceClass: " + this._preferenceClass + "<br>");
            sb.Append("PreferenceCode: " + this._preferenceCode + "<br>");
            sb.Append("ModifyTime: " + this._modifyTime + "<br>");
            sb.Append("Remarks: " + this._remarks + "<br>");
            return sb.ToString();
        }

        /// <summary>
        /// ���㱾��¼������
        /// </summary>
        public override void ResetData()
        {
            _userUid = "";
            _preferenceClass = "";
            _preferenceCode = "";
            _modifyTime = "";
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
            sSQL = "select preference_code,     modify_time,         remarks              \n";
            sSQL += "  from sbt_user_preference    \n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";
            sSQL += "    and preference_class = " + Quote(_preferenceClass) + "\n";

            //======= 2. ����SQL��� ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbUserPreference.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. ��ȡ��¼ ========
            row = dataSet.Tables[0].Rows[0];
            _preferenceCode = DbToString(row["preference_code"]);
            _modifyTime = DbToString(row["modify_time"]);
            _remarks = DbToString(row["remarks"]);
            return true;
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public override void Insert()
        {
            string sSQL;

            sSQL = "insert into sbt_user_preference \n";
            sSQL += "( user_uid,         preference_class, \n";
            sSQL += "  preference_code,  modify_time,      \n";
            sSQL += "  remarks           \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_userUid) + "," + Quote(_preferenceClass) + ",\n";
            sSQL += Quote(_preferenceCode) + "," + Quote(_modifyTime) + ",\n";
            sSQL += Quote(_remarks) + ")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// ������ɾ��һ������
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL = "delete from sbt_user_preference \n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";
            sSQL += "    and preference_class = " + Quote(_preferenceClass) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbUserPreference.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// ����������һ������
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL = "update sbt_user_preference set \n";
            sSQL += " user_uid = " + Quote(_userUid) + ",\n";
            sSQL += " preference_class = " + Quote(_preferenceClass) + ",\n";
            sSQL += " preference_code = " + Quote(_preferenceCode) + ",\n";
            sSQL += " modify_time = " + Quote(_modifyTime) + ",\n";
            sSQL += " remarks = " + Quote(_remarks) + "\n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";
            sSQL += "    and preference_class = " + Quote(_preferenceClass) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbUserPreference.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from sbt_user_preference \n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";
            sSQL += "    and preference_class = " + Quote(_preferenceClass) + "\n";

            //======= 2. ����SQL��� ========
            nRecordCount = ConvertUtil.ToInt(DataHelper.Instance.ExecuteScalar(sSQL));
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
        public void FetchByE(string userUid, string preferenceClass)
        {
            bool hasData;
            hasData = FetchBy(userUid, preferenceClass);
            if (!hasData)
                throw new Exception("TbUserPreference.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// ������Ϊ������ȡһ������
        /// </summary>
        /// <returns>�Ƿ���ʵ�����</returns>
        public bool FetchBy(string userUid, string preferenceClass)
        {
            _userUid = userUid;
            _preferenceClass = preferenceClass;

            return Fetch(true);
        }

        /// <summary>
        /// ������Ϊ������ȡ���ݣ����������ʵ��
        /// </summary>
        /// <returns>���ʵ��</returns>
        static public TbUserPreference CreateBy(string userUid, string preferenceClass)
        {
            TbUserPreference tbl;
            bool hasData;

            tbl = new TbUserPreference();
            hasData = tbl.FetchBy(userUid, preferenceClass);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// ������Ϊ����ɾ��һ������
        /// </summary>
        static public void DeleteBy(string userUid, string preferenceClass)
        {
            TbUserPreference tbl;
            tbl = new TbUserPreference();

            tbl.UserUid = userUid;
            tbl.PreferenceClass = preferenceClass;

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
            _preferenceClass = DbToString(row["preference_class"]);
            _preferenceCode = DbToString(row["preference_code"]);
            _modifyTime = DbToString(row["modify_time"]);
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
            sLine = "user_uid\x9" + _userUid;
            writer.WriteLine(sLine);

            sLine = "preference_class\x9" + _preferenceClass;
            writer.WriteLine(sLine);

            sLine = "preference_code\x9" + _preferenceCode;
            writer.WriteLine(sLine);

            sLine = "modify_time\x9" + _modifyTime;
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
            sLine = "user_uid\tpreference_class\tpreference_code\t";
            sLine += "modify_time\tremarks";
            writer.WriteLine(sLine);

            //======== 3. �õ�SQL��� ========
            sSQL = "select user_uid,        preference_class,preference_code, \n";
            sSQL += "       modify_time,     remarks         \n";
            sSQL += "  from sbt_user_preference";

            //======== 4. ����SQL��� ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. ����ÿһ�� ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 ����ÿһ�� ========
                for (nCol = 0; nCol < 5; nCol++)
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
                case "preference_class":
                    return "varchar";
                case "preference_code":
                    return "varchar";
                case "modify_time":
                    return "varchar";
                case "remarks":
                    return "varchar";
                default:
                    throw new Exception("TbUserPreferenceBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
                case "preference_class":
                    return "string";
                case "preference_code":
                    return "string";
                case "modify_time":
                    return "string";
                case "remarks":
                    return "string";
                default:
                    throw new Exception("TbUserPreferenceBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL = "select " + toFieldName + "from sbt_user_preference \n";
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
