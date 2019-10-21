using System;
using System.Data;
using System.Text;
using System.IO;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Framework
{
    /// <summary>
    /// ��̨������������(sbt_sys_daemon_type)�ı�����û���
    /// </summary>
    public class TbSysDaemonType : TbSysDaemonTypeBase
    {
        #region �û��ɱ༭����

        public static DataSet GetDataSetOfAllValid()
        {
            string sSQL = "select * from sbt_sys_daemon_type where enabled='Y'";
            return DataHelper.Instance.ExecuteDataSet(sSQL);
        }

        /// ... Add your code here ... ///
        /// void FetchByXXX(...)
        /// static DataSet GetDataSetByXXX(...)
        /// static void DeleteByXXX(...)

        #endregion
    }


    /// <summary>
    /// ��̨������������(sbt_sys_daemon_type)���ֶ�����
    /// </summary>
    public class TbSysDaemonTypeF
    {
        public const string TableName = "sbt_sys_daemon_type";
        public const string DaemonType = "daemon_type";
        public const string DaemonTypeName = "daemon_type_name";
        public const string DaemonComponent = "daemon_component";
        public const string DaemonClass = "daemon_class";
        public const string DaemonPriority = "daemon_priority";
        public const string IntervalType = "interval_type";
        public const string IntervalValue = "interval_value";
        public const string ActiveIntervalValue = "active_interval_value";
        public const string Enabled = "enabled";
        public const string BatchSize = "batch_size";
    }


    /// <summary>
    /// ��̨������������(sbt_sys_daemon_type)�ı��������
    /// </summary>
    public class TbSysDaemonTypeBase : Sigbit.Data.TableBase
    {
        #region ˽�б�������
        protected string _daemonType = "";
        protected string _daemonTypeName = "";
        protected string _daemonComponent = "";
        protected string _daemonClass = "";
        protected int _daemonPriority;
        protected string _intervalType = "";
        protected int _intervalValue;
        protected int _activeIntervalValue;
        protected string _enabled = "";
        protected int _batchSize;
        #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public TbSysDaemonTypeBase()
        {
            ResetData();
        }

        #region ���Զ���
        /// <summary>
        /// ��̨��������ͣ�����
        /// </summary>
        public string DaemonType
        {
            get
            {
                return _daemonType;
            }
            set
            {
                _daemonType = value;
            }
        }

        /// <summary>
        /// ��̨�������͵�����
        /// </summary>
        public string DaemonTypeName
        {
            get
            {
                return _daemonTypeName;
            }
            set
            {
                _daemonTypeName = value;
            }
        }

        /// <summary>
        /// ʵ�����������
        /// </summary>
        public string DaemonComponent
        {
            get
            {
                return _daemonComponent;
            }
            set
            {
                _daemonComponent = value;
            }
        }

        /// <summary>
        /// ��̨���������
        /// </summary>
        public string DaemonClass
        {
            get
            {
                return _daemonClass;
            }
            set
            {
                _daemonClass = value;
            }
        }

        /// <summary>
        /// �������ȼ�����1��9
        /// </summary>
        public int DaemonPriority
        {
            get
            {
                return _daemonPriority;
            }
            set
            {
                _daemonPriority = value;
            }
        }

        /// <summary>
        /// ��̨�����ʱ��������
        /// </summary>
        public string IntervalType
        {
            get
            {
                return _intervalType;
            }
            set
            {
                _intervalType = value;
            }
        }

        /// <summary>
        /// ����ļ��ֵ
        /// </summary>
        public int IntervalValue
        {
            get
            {
                return _intervalValue;
            }
            set
            {
                _intervalValue = value;
            }
        }

        /// <summary>
        /// ������ʱ��ʱ����
        /// </summary>
        public int ActiveIntervalValue
        {
            get
            {
                return _activeIntervalValue;
            }
            set
            {
                _activeIntervalValue = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        public string Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
            }
        }

        /// <summary>
        /// ���õ����δ�С���ɲ��䣩
        /// </summary>
        public int BatchSize
        {
            get
            {
                return _batchSize;
            }
            set
            {
                _batchSize = value;
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
            sb.Append("DaemonType: " + this._daemonType + "<br>");
            sb.Append("DaemonTypeName: " + this._daemonTypeName + "<br>");
            sb.Append("DaemonComponent: " + this._daemonComponent + "<br>");
            sb.Append("DaemonClass: " + this._daemonClass + "<br>");
            sb.Append("DaemonPriority: " + this._daemonPriority + "<br>");
            sb.Append("IntervalType: " + this._intervalType + "<br>");
            sb.Append("IntervalValue: " + this._intervalValue + "<br>");
            sb.Append("ActiveIntervalValue: " + this._activeIntervalValue + "<br>");
            sb.Append("Enabled: " + this._enabled + "<br>");
            sb.Append("BatchSize: " + this._batchSize + "<br>");
            return sb.ToString();
        }

        /// <summary>
        /// ���㱾��¼������
        /// </summary>
        public override void ResetData()
        {
            _daemonType = "";
            _daemonTypeName = "";
            _daemonComponent = "";
            _daemonClass = "";
            _daemonPriority = 0;
            _intervalType = "";
            _intervalValue = 0;
            _activeIntervalValue = 0;
            _enabled = "";
            _batchSize = 0;
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
            sSQL = "select daemon_type_name,    daemon_component,    daemon_class,         \n";
            sSQL += "       daemon_priority,     interval_type,       interval_value,       \n";
            sSQL += "       active_interval_value,enabled,             batch_size           \n";
            sSQL += "  from sbt_sys_daemon_type    \n";
            sSQL += "  where daemon_type = " + Quote(_daemonType) + "\n";

            //======= 2. ����SQL��� ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;
            if (nRecordCount != 1)
            {
                if (!allowNoData)
                    throw new Exception("TbSysDaemonType.Fetch() Error - cannot locate record via PrimaryKey.");
                else
                    return false;
            }

            //======= 3. ��ȡ��¼ ========
            row = dataSet.Tables[0].Rows[0];
            _daemonTypeName = DbToString(row["daemon_type_name"]); DbToInt(row["daemon_type_name"]);
            _daemonComponent = DbToString(row["daemon_component"]); DbToInt(row["daemon_component"]);
            _daemonClass = DbToString(row["daemon_class"]); DbToInt(row["daemon_class"]);
            _daemonPriority = DbToInt(row["daemon_priority"]);
            _intervalType = DbToString(row["interval_type"]); DbToInt(row["interval_type"]);
            _intervalValue = DbToInt(row["interval_value"]);
            _activeIntervalValue = DbToInt(row["active_interval_value"]);
            _enabled = DbToString(row["enabled"]); DbToInt(row["enabled"]);
            _batchSize = DbToInt(row["batch_size"]);
            return true;
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public override void Insert()
        {
            string sSQL;

            sSQL = "insert into sbt_sys_daemon_type \n";
            sSQL += "( daemon_type,      daemon_type_name, \n";
            sSQL += "  daemon_component, daemon_class,     \n";
            sSQL += "  daemon_priority,  interval_type,    \n";
            sSQL += "  interval_value,   active_interval_value, \n";
            sSQL += "  enabled,          batch_size        \n";
            sSQL += ") values (          \n";
            sSQL += Quote(_daemonType) + "," + Quote(_daemonTypeName) + ",\n";
            sSQL += Quote(_daemonComponent) + "," + Quote(_daemonClass) + ",\n";
            sSQL += _daemonPriority.ToString() + "," + Quote(_intervalType) + ",\n";
            sSQL += _intervalValue.ToString() + "," + _activeIntervalValue.ToString() + ",\n";
            sSQL += Quote(_enabled) + "," + _batchSize.ToString() + ")\n";

            DataHelper.Instance.ExecuteNonQuery(sSQL);
        }

        /// <summary>
        /// ������ɾ��һ������
        /// </summary>
        public override void Delete()
        {
            string sSQL;

            sSQL = "delete from sbt_sys_daemon_type \n";
            sSQL += "  where daemon_type = " + Quote(_daemonType) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysDaemonType.Delete() Error - cannot update data via PrimaryKey.");
        }

        /// <summary>
        /// ����������һ������
        /// </summary>
        public override void Update()
        {
            string sSQL;

            sSQL = "update sbt_sys_daemon_type set \n";
            sSQL += " daemon_type = " + Quote(_daemonType) + ",\n";
            sSQL += " daemon_type_name = " + Quote(_daemonTypeName) + ",\n";
            sSQL += " daemon_component = " + Quote(_daemonComponent) + ",\n";
            sSQL += " daemon_class = " + Quote(_daemonClass) + ",\n";
            sSQL += " daemon_priority = " + _daemonPriority.ToString() + ",\n";
            sSQL += " interval_type = " + Quote(_intervalType) + ",\n";
            sSQL += " interval_value = " + _intervalValue.ToString() + ",\n";
            sSQL += " active_interval_value = " + _activeIntervalValue.ToString() + ",\n";
            sSQL += " enabled = " + Quote(_enabled) + ",\n";
            sSQL += " batch_size = " + _batchSize.ToString() + "\n";
            sSQL += "  where daemon_type = " + Quote(_daemonType) + "\n";

            int nRowsAffected;
            nRowsAffected = DataHelper.Instance.ExecuteNonQuery(sSQL);
            if (nRowsAffected != 1)
                throw new Exception("TbSysDaemonType.Update() Error - cannot update data via PrimaryKey.");
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
            sSQL = "select count(*) from sbt_sys_daemon_type \n";
            sSQL += "  where daemon_type = " + Quote(_daemonType) + "\n";

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
        public void FetchByE(string daemonType)
        {
            bool hasData;
            hasData = FetchBy(daemonType);
            if (!hasData)
                throw new Exception("TbSysDaemonType.FetchBy(...) Error - cannot locate record via PrimaryKey.");
        }

        /// <summary>
        /// ������Ϊ������ȡһ������
        /// </summary>
        /// <returns>�Ƿ���ʵ�����</returns>
        public bool FetchBy(string daemonType)
        {
            _daemonType = daemonType;

            return Fetch(true);
        }

        /// <summary>
        /// ������Ϊ������ȡ���ݣ����������ʵ��
        /// </summary>
        /// <returns>���ʵ��</returns>
        static public TbSysDaemonType CreateBy(string daemonType)
        {
            TbSysDaemonType tbl;
            bool hasData;

            tbl = new TbSysDaemonType();
            hasData = tbl.FetchBy(daemonType);

            if (!hasData)
                return null;
            else
                return tbl;
        }

        /// <summary>
        /// ������Ϊ����ɾ��һ������
        /// </summary>
        static public void DeleteBy(string daemonType)
        {
            TbSysDaemonType tbl;
            tbl = new TbSysDaemonType();

            tbl.DaemonType = daemonType;

            tbl.Delete();
        }

        #endregion

        #region �ļ��ͷ��������ص�֧�ֺ���
        /// <summary>
        /// ͨ��DataRow���и�ֵ
        /// </summary>
        public override void AssignByDataRow(DataRow row)
        {
            _daemonType = DbToString(row["daemon_type"]);
            _daemonTypeName = DbToString(row["daemon_type_name"]);
            _daemonComponent = DbToString(row["daemon_component"]);
            _daemonClass = DbToString(row["daemon_class"]);
            _daemonPriority = DbToInt(row["daemon_priority"]);
            _intervalType = DbToString(row["interval_type"]);
            _intervalValue = DbToInt(row["interval_value"]);
            _activeIntervalValue = DbToInt(row["active_interval_value"]);
            _enabled = DbToString(row["enabled"]);
            _batchSize = DbToInt(row["batch_size"]);
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
            sLine = "daemon_type\x9" + _daemonType;
            writer.WriteLine(sLine);

            sLine = "daemon_type_name\x9" + _daemonTypeName;
            writer.WriteLine(sLine);

            sLine = "daemon_component\x9" + _daemonComponent;
            writer.WriteLine(sLine);

            sLine = "daemon_class\x9" + _daemonClass;
            writer.WriteLine(sLine);

            sLine = "daemon_priority\x9" + _daemonPriority.ToString();
            writer.WriteLine(sLine);

            sLine = "interval_type\x9" + _intervalType;
            writer.WriteLine(sLine);

            sLine = "interval_value\x9" + _intervalValue.ToString();
            writer.WriteLine(sLine);

            sLine = "active_interval_value\x9" + _activeIntervalValue.ToString();
            writer.WriteLine(sLine);

            sLine = "enabled\x9" + _enabled;
            writer.WriteLine(sLine);

            sLine = "batch_size\x9" + _batchSize.ToString();
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
            sLine = "daemon_type\tdaemon_type_name\tdaemon_component\t";
            sLine += "daemon_class\tdaemon_priority\tinterval_type\t";
            sLine += "interval_value\tactive_interval_value\tenabled\t";
            sLine += "batch_size";
            writer.WriteLine(sLine);

            //======== 3. �õ�SQL��� ========
            sSQL = "select daemon_type,     daemon_type_name,daemon_component,\n";
            sSQL += "       daemon_class,    daemon_priority, interval_type,   \n";
            sSQL += "       interval_value,  active_interval_value,enabled,         \n";
            sSQL += "       batch_size      \n";
            sSQL += "  from sbt_sys_daemon_type";

            //======== 4. ����SQL��� ========
            dataSet = DataHelper.Instance.ExecuteDataSet(sSQL);
            nRecordCount = dataSet.Tables[0].Rows.Count;

            //======== 5. ����ÿһ�� ========
            for (i = 0; i < nRecordCount; i++)
            {
                row = dataSet.Tables[0].Rows[i];
                sLine = "";

                //======== 5.1 ����ÿһ�� ========
                for (nCol = 0; nCol < 10; nCol++)
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
                case "daemon_type":
                    return "varchar";
                case "daemon_type_name":
                    return "varchar";
                case "daemon_component":
                    return "varchar";
                case "daemon_class":
                    return "varchar";
                case "daemon_priority":
                    return "int";
                case "interval_type":
                    return "varchar";
                case "interval_value":
                    return "int";
                case "active_interval_value":
                    return "int";
                case "enabled":
                    return "char";
                case "batch_size":
                    return "int";
                default:
                    throw new Exception("TbSysDaemonTypeBase.DBTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
            }
        }

        /// <summary>
        /// ��ȡһ���ֶε�CSharp����
        /// </summary>
        static public string CSharpTypeOfFieldName(string fieldName)
        {
            switch (fieldName)
            {
                case "daemon_type":
                    return "string";
                case "daemon_type_name":
                    return "string";
                case "daemon_component":
                    return "string";
                case "daemon_class":
                    return "string";
                case "daemon_priority":
                    return "int";
                case "interval_type":
                    return "string";
                case "interval_value":
                    return "int";
                case "active_interval_value":
                    return "int";
                case "enabled":
                    return "string";
                case "batch_size":
                    return "int";
                default:
                    throw new Exception("TbSysDaemonTypeBase.CSharpTypeOfFieldName Error : meet unexpected fieldName - " + fieldName);
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
            sSQL = "select " + toFieldName + "from sbt_sys_daemon_type \n";
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
