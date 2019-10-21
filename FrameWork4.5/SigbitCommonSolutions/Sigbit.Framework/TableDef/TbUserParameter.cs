using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework
{
    /// <summary>
    /// (sbt_user_parameter)�ı�����û���
    /// </summary>
    public class TbUserParameter : TbUserParameterBase
    {
        #region �û��ɱ༭����

        /// <summary>
        /// �õ�ϵͳ���õĲ���ֵ
        /// </summary>
        /// <param name="sUserUid">�û����</param>
        /// <param name="sApplicationName">Ӧ�ó�����</param>
        /// <param name="sParameterClass">��������</param>
        /// <param name="sParameterName">������</param>
        /// <param name="sDefaultValue">ȱʡֵ</param>
        /// <returns>����ֵ</returns>
        public static string GetParameterValue(string sUserUid, string sApplicationName, string sParameterClass,
                string sParameterName, string sDefaultValue)
        {
            TbUserParameter tblParam = new TbUserParameter();
            tblParam.UserUid = sUserUid;
            tblParam.ApplicationName = sApplicationName;
            tblParam.ParameterClass = sParameterClass;
            tblParam.ParameterName = sParameterName;
            if (!tblParam.Fetch(true))
                return sDefaultValue;

            return tblParam.ParameterValue;
        }

        /// <summary>
        /// �õ�ϵͳ���õĲ���ֵ
        /// </summary>
        /// <param name="sUserUid">�û����</param>
        /// <param name="sApplicationName">Ӧ�ó�����</param>
        /// <param name="sParameterClass">��������</param>
        /// <param name="sParameterName">������</param>
        /// <returns>����ֵ</returns>
        public static string GetParameterValue(string sUserUid, string sApplicationName, string sParameterClass,
                string sParameterName)
        {
            return GetParameterValue(sUserUid, sApplicationName, sParameterClass, sParameterName, "");
        }

        /// <summary>
        /// �õ�ϵͳ���õĲ�������ֵ
        /// </summary>
        /// <param name="sUserUid">�û����</param>
        /// <param name="sApplicationName">Ӧ�ó�����</param>
        /// <param name="sParameterClass">��������</param>
        /// <param name="sParameterName">������</param>
        /// <param name="nDefaultValue">ȱʡ����ֵ</param>
        /// <returns>����ֵ</returns>
        public static int GetParameterValueInt(string sUserUid, string sApplicationName, string sParameterClass,
                string sParameterName, int nDefaultValue)
        {
            string sParamValue = GetParameterValue(sUserUid, sApplicationName, sParameterClass,
                    sParameterName, nDefaultValue.ToString());
            return ConvertUtil.ToInt(sParamValue, nDefaultValue);
        }

        /// <summary>
        /// �õ�ϵͳ���õĲ�������ֵ
        /// </summary>
        /// <param name="sUserUid">�û����</param>
        /// <param name="sApplicationName">Ӧ�ó�����</param>
        /// <param name="sParameterClass">��������</param>
        /// <param name="sParameterName">������</param>
        /// <returns>����ֵ</returns>
        public static int GetParameterValueInt(string sUserUid, string sApplicationName, string sParameterClass,
                string sParameterName)
        {
            return GetParameterValueInt(sUserUid, sApplicationName, sParameterClass, sParameterName, 0);
        }

        /// <summary>
        /// ����ϵͳ���ò���
        /// </summary>
        /// <param name="sUserUid">�û����</param>
        /// <param name="sApplicationName">Ӧ�ó�����</param>
        /// <param name="sParameterClass">��������</param>
        /// <param name="sParameterName">������</param>
        /// <param name="sValue">����ֵ</param>
        public static void SetParameterValue(string sUserUid, string sApplicationName, string sParameterClass,
                string sParameterName, string sValue)
        {
            TbUserParameter tblParam = new TbUserParameter();
            tblParam.UserUid = sUserUid;
            tblParam.ApplicationName = sApplicationName;
            tblParam.ParameterClass = sParameterClass;
            tblParam.ParameterName = sParameterName;
            if (tblParam.Fetch(true))
            {
                tblParam.ParameterValue = sValue;
                tblParam.Update();
            }
            else
            {
                tblParam.ParameterValue = sValue;
                tblParam.Insert();
            }
        }

        /// <summary>
        /// ����ϵͳ���ò���������ֵ��
        /// </summary>
        /// <param name="sUserUid">�û����</param>
        /// <param name="sApplicationName">Ӧ�ó�����</param>
        /// <param name="sParameterClass">��������</param>
        /// <param name="sParameterName">������</param>
        /// <param name="nValue">����ֵ</param>
        public static void SetParameterValueInt(string sUserUid, string sApplicationName, string sParameterClass,
                string sParameterName, int nValue)
        {
            SetParameterValue(sUserUid, sApplicationName, sParameterClass,
                    sParameterName, nValue.ToString());
        }


        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }


    /// <summary>
    /// (sbt_user_parameter)���ֶ�����
    /// </summary>
    public class TbUserParameterF
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string TableName = "sbt_user_parameter";
        /// <summary>
        /// �û����
        /// </summary>
        public const string UserUid = "user_uid";
        /// <summary>
        /// Ӧ������
        /// </summary>
        public const string ApplicationName = "application_name";
        /// <summary>
        /// ��������
        /// </summary>
        public const string ParameterClass = "parameter_class";
        /// <summary>
        /// ��������
        /// </summary>
        public const string ParameterName = "parameter_name";
        /// <summary>
        /// ����ֵ
        /// </summary>
        public const string ParameterValue = "parameter_value";
        /// <summary>
        /// ��ע
        /// </summary>
        public const string Remarks = "remarks";
    }


    /// <summary>
    /// (sbt_user_parameter)�ı��������
    /// </summary>
    public class TbUserParameterBase : Sigbit.Data.TableBase
    {
        #region ˽�б�������
        protected string _userUid = "";
        protected string _applicationName = "";
        protected string _parameterClass = "";
        protected string _parameterName = "";
        protected string _parameterValue = "";
        protected string _remarks = "";
        #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public TbUserParameterBase()
        {
            ResetData();
        }

        #region ���Զ���
        /// <summary>
        /// �û���ʶ���룬����
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
        /// Ӧ�ó�����������
        /// </summary>
        public string ApplicationName
        {
            get
            {
                return _applicationName;
            }
            set
            {
                _applicationName = value;
            }
        }

        /// <summary>
        /// �������ͣ�����
        /// </summary>
        public string ParameterClass
        {
            get
            {
                return _parameterClass;
            }
            set
            {
                _parameterClass = value;
            }
        }

        /// <summary>
        /// ������������
        /// </summary>
        public string ParameterName
        {
            get
            {
                return _parameterName;
            }
            set
            {
                _parameterName = value;
            }
        }

        /// <summary>
        /// ����ֵ
        /// </summary>
        public string ParameterValue
        {
            get
            {
                return _parameterValue;
            }
            set
            {
                _parameterValue = value;
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
            sb.Append("ApplicationName: " + this._applicationName + "<br>");
            sb.Append("ParameterClass: " + this._parameterClass + "<br>");
            sb.Append("ParameterName: " + this._parameterName + "<br>");
            sb.Append("ParameterValue: " + this._parameterValue + "<br>");
            sb.Append("Remarks: " + this._remarks + "<br>");
            return sb.ToString();
        }

        /// <summary>
        /// ���㱾��¼������
        /// </summary>
        public override void ResetData()
        {
            _userUid = "";
            _applicationName = "";
            _parameterClass = "";
            _parameterName = "";
            _parameterValue = "";
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
            sSQL = "select parameter_value,     remarks              \n";
            sSQL += "  from sbt_user_parameter    \n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";
            sSQL += "    and application_name = " + Quote(_applicationName) + "\n";
            sSQL += "    and parameter_class = " + Quote(_parameterClass) + "\n";
            sSQL += "    and parameter_name = " + Quote(_parameterName) + "\n";

            //======= 2. ����SQL��� ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbUserParameter.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. ��ȡ��¼ ========
            row = dataSet.Tables[0].Rows[0];
            _parameterValue = DbToString(row["parameter_value"]);
            _remarks = DbToString(row["remarks"]);
            return true;
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public override void Insert()
        {
            string sSQL;

            sSQL = "insert into sbt_user_parameter \n";
            sSQL += "( user_uid,         application_name, \n";
            sSQL += "  parameter_class,  parameter_name,   \n";
            sSQL += "  parameter_value,  remarks           \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_userUid) + "," + Quote(_applicationName) + ",\n";
            sSQL += Quote(_parameterClass) + "," + Quote(_parameterName) + ",\n";
            sSQL += Quote(_parameterValue) + "," + Quote(_remarks) + ")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// ������ɾ��һ������
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL = "delete from sbt_user_parameter \n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";
            sSQL += "    and application_name = " + Quote(_applicationName) + "\n";
            sSQL += "    and parameter_class = " + Quote(_parameterClass) + "\n";
            sSQL += "    and parameter_name = " + Quote(_parameterName) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbUserParameter.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// ����������һ������
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL = "update sbt_user_parameter set \n";
            sSQL += " user_uid = " + Quote(_userUid) + ",\n";
            sSQL += " application_name = " + Quote(_applicationName) + ",\n";
            sSQL += " parameter_class = " + Quote(_parameterClass) + ",\n";
            sSQL += " parameter_name = " + Quote(_parameterName) + ",\n";
            sSQL += " parameter_value = " + Quote(_parameterValue) + ",\n";
            sSQL += " remarks = " + Quote(_remarks) + "\n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";
            sSQL += "    and application_name = " + Quote(_applicationName) + "\n";
            sSQL += "    and parameter_class = " + Quote(_parameterClass) + "\n";
            sSQL += "    and parameter_name = " + Quote(_parameterName) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected > 1)
                throw new Exception("TbUserParameter.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from sbt_user_parameter \n";
            sSQL += "  where user_uid = " + Quote(_userUid) + "\n";
            sSQL += "    and application_name = " + Quote(_applicationName) + "\n";
            sSQL += "    and parameter_class = " + Quote(_parameterClass) + "\n";
            sSQL += "    and parameter_name = " + Quote(_parameterName) + "\n";

            //======= 2. ����SQL��� ========
            nRecordCount = Convert.ToInt32(DataHelper.Instance.ExecuteScalar(sSQL));
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
        public void FetchByE(string userUid, string applicationName, string parameterClass, string parameterName)
        {
            bool hasData;
            hasData = FetchBy(userUid, applicationName, parameterClass, parameterName);
            if (!hasData)
                throw new Exception("TbUserParameter.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// ������Ϊ������ȡһ������
        /// </summary>
        /// <returns>�Ƿ���ʵ�����</returns>
        public bool FetchBy(string userUid, string applicationName, string parameterClass, string parameterName)
        {
            _userUid = userUid;
            _applicationName = applicationName;
            _parameterClass = parameterClass;
            _parameterName = parameterName;

            return Fetch(true);
        }

        /// <summary>
        /// ������Ϊ������ȡ���ݣ����������ʵ��
        /// </summary>
        /// <returns>���ʵ��</returns>
        static public TbUserParameter CreateBy(string userUid, string applicationName, string parameterClass, string parameterName)
        {
            TbUserParameter tbl;
            bool hasData;

            tbl = new TbUserParameter();
            hasData = tbl.FetchBy(userUid, applicationName, parameterClass, parameterName);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// ������Ϊ����ɾ��һ������
        /// </summary>
        static public void DeleteBy(string userUid, string applicationName, string parameterClass, string parameterName)
        {
            TbUserParameter tbl;
            tbl = new TbUserParameter();

            tbl.UserUid = userUid;
            tbl.ApplicationName = applicationName;
            tbl.ParameterClass = parameterClass;
            tbl.ParameterName = parameterName;

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
            _applicationName = DbToString(row["application_name"]);
            _parameterClass = DbToString(row["parameter_class"]);
            _parameterName = DbToString(row["parameter_name"]);
            _parameterValue = DbToString(row["parameter_value"]);
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

            sLine = "application_name\x9" + _applicationName;
            writer.WriteLine(sLine);

            sLine = "parameter_class\x9" + _parameterClass;
            writer.WriteLine(sLine);

            sLine = "parameter_name\x9" + _parameterName;
            writer.WriteLine(sLine);

            sLine = "parameter_value\x9" + _parameterValue;
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
            sLine = "user_uid\tapplication_name\tparameter_class\t";
            sLine += "parameter_name\tparameter_value\tremarks";
            writer.WriteLine(sLine);

            //======== 3. �õ�SQL��� ========
            sSQL = "select user_uid,        application_name,parameter_class, \n";
            sSQL += "       parameter_name,  parameter_value, remarks         \n";
            sSQL += "  from sbt_user_parameter";

            //======== 4. ����SQL��� ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. ����ÿһ�� ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 ����ÿһ�� ========
                for (nCol = 0; nCol < 6; nCol++)
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
                case "application_name":
                    return "varchar";
                case "parameter_class":
                    return "varchar";
                case "parameter_name":
                    return "varchar";
                case "parameter_value":
                    return "varchar";
                case "remarks":
                    return "varchar";
                default:
                    throw new Exception("TbUserParameterBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
                case "application_name":
                    return "string";
                case "parameter_class":
                    return "string";
                case "parameter_name":
                    return "string";
                case "parameter_value":
                    return "string";
                case "remarks":
                    return "string";
                default:
                    throw new Exception("TbUserParameterBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL = "select " + toFieldName + " from sbt_user_parameter \n";
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
