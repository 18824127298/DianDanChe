using System;
using System.Data;

namespace Sigbit.Data
{
    /// <summary>
    /// 所有数据表实体类基类.
    /// </summary>
    public abstract class TableBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TableBase()
        {

        }

        /// <summary>
        /// 把所有记录保存在文件中
        /// </summary>
        /// <param name="filename">文件名</param>
        public virtual void DumpAllRecordsToFile(string filename) { throw new Exception("TableBaes.DumpAllRecordsToFile(string filename) has not been implemneted!"); }

        /// <summary>
        /// 把当前记录保存在文件中
        /// </summary>
        /// <param name="filename">文件名</param>
        public virtual void DumpToFile(string filename) { throw new Exception("TableBaes.DumpToFile(string filename) has not been implemneted!"); }

        /// <summary>
        /// 根据数据行初始化实例
        /// </summary>
        /// <param name="row">数据行</param>
        public virtual void AssignByDataRow(DataRow row) { throw new Exception("TableBaes.AssignByDataRow(DataRow row) has not been implemneted!"); }

        /// <summary>
        /// 根据数据集初始化实例
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <param name="rowNum">数据行数</param>
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
        /// 初始化实例
        /// </summary>
        /// <param name="allowNoData">是否允许空数据</param>
        /// <returns>是否初始化成功</returns>
        public virtual bool Fetch(bool allowNoData) { throw new Exception("TableBaes.Fetch() has not been implemneted!"); }

        /// <summary>
        /// 清空数据
        /// </summary>
        public virtual void ResetData() { throw new Exception("TableBaes.ResetData() has not been implemneted!"); }

        /// <summary>
        /// 按主键判断记录是否存在
        /// </summary>
        /// <returns>记录是否存在</returns>
        public virtual bool RecordExists() { throw new Exception("TableBase.RecordExists() has not been implemented! "); }

        /// <summary>
        /// 按主键获取一条数据（如无数据抛例外）
        /// </summary>
        public virtual void Fetch() { throw new Exception("TableBase.Fetch() has not been implemented! "); }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        public virtual void Insert() { throw new Exception("TableBase.Insert() has not been implemented! "); }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public virtual void Update() { throw new Exception("TableBase.Update() has not been implemented! "); }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public virtual void Delete() { throw new Exception("TableBase.Delete() has not been implemented! "); }

        /// <summary>
        /// 转化为int 类型
        /// </summary>
        /// <param name="obj">待转化的object类型</param>
        /// <returns>转化结果</returns>
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
        /// 转化为Double类型
        /// </summary>
        /// <param name="obj">待转化的object类型</param>
        /// <returns>转化结果</returns>
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
        /// 给字符串加上单引号
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回结果</returns>
        /// <remarks>
        /// 20070513:HISTORY:oldix，插入针对'\'的处理
        /// </remarks>
        public virtual string Quote(string str)
        {
            str = str.Replace("'", "''");
            str = str.Replace("\\", "\\\\");
            str = "'" + str + "'";
            return str;
        }

        /// <summary>
        /// 转化为string 类型
        /// </summary>
        /// <param name="obj">待转化的object类型</param>
        /// <returns>转化结果</returns>
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
    ///// TableBase 的摘要说明。
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
