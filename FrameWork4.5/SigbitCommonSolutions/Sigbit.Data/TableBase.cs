using System;
using System.Data;

namespace Sigbit.Data
{
    /// <summary>
    /// �������ݱ�ʵ�������.
    /// </summary>
    public abstract class TableBase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public TableBase()
        {

        }

        /// <summary>
        /// �����м�¼�������ļ���
        /// </summary>
        /// <param name="filename">�ļ���</param>
        public virtual void DumpAllRecordsToFile(string filename) { throw new Exception("TableBaes.DumpAllRecordsToFile(string filename) has not been implemneted!"); }

        /// <summary>
        /// �ѵ�ǰ��¼�������ļ���
        /// </summary>
        /// <param name="filename">�ļ���</param>
        public virtual void DumpToFile(string filename) { throw new Exception("TableBaes.DumpToFile(string filename) has not been implemneted!"); }

        /// <summary>
        /// ���������г�ʼ��ʵ��
        /// </summary>
        /// <param name="row">������</param>
        public virtual void AssignByDataRow(DataRow row) { throw new Exception("TableBaes.AssignByDataRow(DataRow row) has not been implemneted!"); }

        /// <summary>
        /// �������ݼ���ʼ��ʵ��
        /// </summary>
        /// <param name="dataSet">���ݼ�</param>
        /// <param name="rowNum">��������</param>
        public virtual void AssignByDataRow(DataSet dataSet, int rowNum) { throw new Exception("TableBaes.AssignByDataRow(DataSet dataSet,int rowNum) has not been implemneted!"); }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //public virtual bool FetchBy(string userId) { throw new Exception("TableBaes.FetchBy(string userId) has not been implemneted!"); }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="userId"></param>
        //public virtual void FetchByE(string userId) { throw new Exception("TableBaes.FetchByE(string userId) has not been implemneted!"); }

        /// <summary>
        /// ��ʼ��ʵ��
        /// </summary>
        /// <param name="allowNoData">�Ƿ����������</param>
        /// <returns>�Ƿ��ʼ���ɹ�</returns>
        public virtual bool Fetch(bool allowNoData) { throw new Exception("TableBaes.Fetch() has not been implemneted!"); }

        /// <summary>
        /// �������
        /// </summary>
        public virtual void ResetData() { throw new Exception("TableBaes.ResetData() has not been implemneted!"); }

        /// <summary>
        /// �������жϼ�¼�Ƿ����
        /// </summary>
        /// <returns>��¼�Ƿ����</returns>
        public virtual bool RecordExists() { throw new Exception("TableBase.RecordExists() has not been implemented! "); }

        /// <summary>
        /// ��������ȡһ�����ݣ��������������⣩
        /// </summary>
        public virtual void Fetch() { throw new Exception("TableBase.Fetch() has not been implemented! "); }

        /// <summary>
        /// ����һ������
        /// </summary>
        public virtual void Insert() { throw new Exception("TableBase.Insert() has not been implemented! "); }

        /// <summary>
        /// ����һ������
        /// </summary>
        public virtual void Update() { throw new Exception("TableBase.Update() has not been implemented! "); }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public virtual void Delete() { throw new Exception("TableBase.Delete() has not been implemented! "); }

        /// <summary>
        /// ת��Ϊint ����
        /// </summary>
        /// <param name="obj">��ת����object����</param>
        /// <returns>ת�����</returns>
        public virtual int DbToInt(object obj)
        {
            if (obj is DBNull)
                return 0;
            else
            {
                try
                {
                    return Convert.ToInt32(obj);
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// ת��ΪDouble����
        /// </summary>
        /// <param name="obj">��ת����object����</param>
        /// <returns>ת�����</returns>
        public virtual double DbToDouble(object obj)
        {
            if (obj is DBNull)
                return 0;
            else
            {
                try
                {
                    return Convert.ToDouble(obj);
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// ���ַ������ϵ�����
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <returns>���ؽ��</returns>
        /// <remarks>
        /// 20070513:HISTORY:oldix���������'\'�Ĵ���
        /// </remarks>
        public virtual string Quote(string str)
        {
            str = str.Replace("'", "''");
            str = str.Replace("\\", "\\\\");
            str = "'" + str + "'";
            return str;
        }

        /// <summary>
        /// ת��Ϊstring ����
        /// </summary>
        /// <param name="obj">��ת����object����</param>
        /// <returns>ת�����</returns>
        public virtual string DbToString(object obj)
        {
            if (obj is DBNull)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }
    }
    ///// <summary>
    ///// TableBase ��ժҪ˵����
    ///// </summary>
    //public abstract class TableBase
    //{
    //    public TableBase()
    //    {
			
    //    }

    //    public virtual void DumpToFile(string fileName) { throw new Exception("TableBase.DumpToFile() has not been implemented! "); }
    //    public virtual void DumpAllRecordsToFile(string fileName) { throw new Exception("TableBase.DumpAllRecordsToFile() has not been implemented! "); }
    //    public virtual void RestData(){throw new Exception("TableBase.RestData() has not been implemented! ");}
    //    public virtual void AssignByDataSet(){throw new Exception("TableBase.AssignByDataSet() has not been implemented! ");}
    //    public virtual void AssignByDataRow(DataRow row) { throw new Exception("TableBase.AssignByDataRow() has not been implemented! "); }
    //    public virtual void AssignByDataRow(DataSet dataSet, int rowNum) { throw new Exception("TableBase.AssignByDataRow() has not been implemented! "); }
    //    public virtual bool RecordExists() { throw new Exception("TableBase.RecordExists() has not been implemented! "); }
    //    public virtual void Fetch() { throw new Exception("TableBase.Fetch() has not been implemented! "); }
    //    public virtual bool Fetch(bool bAllowNoData) { throw new Exception("TableBase.Fetch(bool) has not been implemented! "); }
    //    public virtual void Insert() { throw new Exception("TableBase.Insert() has not been implemented! "); }
    //    public virtual void Update(){throw new Exception("TableBase.Update() has not been implemented! ");}
    //    public virtual void Delete(){throw new Exception("TableBase.Delete() has not been implemented! ");}

    //    public virtual int DbToInt(object obj)
    //    {
    //        if(obj is DBNull)
    //            return 0;
    //        else
    //        {
    //            return Convert.ToInt32(obj);
    //        }
    //    }
    //}
}
