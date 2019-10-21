using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Collections;

namespace Sigbit.Framework
{
    /// <summary>
    /// 页面间传递参数用的类
    /// </summary>
    /// <remarks>
    /// 一般的用法是，在打开新的页面前填入参数，新页面打开后读取参数，
    /// 以后就再也不用了。（因为所有的页面都会共享这个参数表）
    /// </remarks>
    public class SbtPageParameter
    {
        public string[] StringParam = new string[32];
        public int[] IntParam = new int[32];
        public DataSet DataSetParam = null;
        public object[] ObjParam = new object[8];

        #region 自定义参数
        /// <summary>
        /// 自定义参数集
        /// </summary>
        /// <remarks>
        /// 自定义参数是以任意字符串进行标识和存储的集合
        /// </remarks>
        private Hashtable _htCustomParams = new Hashtable();

        /// <summary>
        /// 清除自定义的参数
        /// </summary>
        public void ClearAllCustomParams()
        {
            _htCustomParams.Clear();
        }

        /// <summary>
        /// 清除以特定字符串开头的自定义参数
        /// </summary>
        /// <param name="sHeadString">特定的字符串开头</param>
        public void ClearCustomParamsStartWith(string sHeadString)
        {
            ArrayList arrClearKeyList = new ArrayList();

            //========== 1. 遍历哈希表，找到以特定字符串开头的关键字 ==============
            foreach (DictionaryEntry entry in _htCustomParams)
            {
                string sKey = (string)entry.Key;
                if (sKey.IndexOf(sHeadString) != -1)
                    arrClearKeyList.Add(sKey);
            }

            //=========== 2. 清除这些自定义参数 ==============
            foreach (string sWantClearKey in arrClearKeyList)
                _htCustomParams.Remove(sWantClearKey);
        }

        /// <summary>
        /// 设置自定义参数
        /// </summary>
        /// <param name="sKey">关键字</param>
        /// <param name="objValue">对象值</param>
        public void SetCustomParamObject(string sKey, object objValue)
        {
            _htCustomParams[sKey] = objValue;
        }

        /// <summary>
        /// 设置自定义参数
        /// </summary>
        /// <param name="sKey">关键字</param>
        /// <param name="sValue">字符串值</param>
        public void SetCustomParamString(string sKey, string sValue)
        {
            SetCustomParamObject(sKey, sValue);
        }

        /// <summary>
        /// 增加自定义参数
        /// </summary>
        /// <param name="sKey">关键字</param>
        /// <param name="objValue">对象值</param>
        public void AddCustomParamObject(string sKey, object objValue)
        {
            if (_htCustomParams[sKey] != null)
                throw new Exception("自定义参数集中已有关键字“" + sKey + "”, 增加该自定义值出错");

            _htCustomParams.Add(sKey, objValue);
        }

        /// <summary>
        /// 增加自定义参数
        /// </summary>
        /// <param name="sKey">关键字</param>
        /// <param name="sValue">字符串值</param>
        public void AddCustomParamString(string sKey, string sValue)
        {
            AddCustomParamObject(sKey, sValue);
        }

        /// <summary>
        /// 获取自定义参数，如未设置，返回null值
        /// </summary>
        /// <param name="sKey">关键字</param>
        /// <returns>对象值</returns>
        public object GetCustomParamObject(string sKey)
        {
            return _htCustomParams[sKey];
        }

        /// <summary>
        /// 获取自定义参数，如未设置，返回null值
        /// </summary>
        /// <param name="sKey">关键字</param>
        /// <returns>字符串值</returns>
        public string GetCustomParamString(string sKey)
        {
            object obj = _htCustomParams[sKey];
            if (obj == null)
                return null;

            if (!(obj is string))
                throw new Exception("获取的值不是字符串");

            return (string)obj;
        }

        /// <summary>
        /// 获取自定义参数，如未设置，返回缺省值
        /// </summary>
        /// <param name="sKey">关键字</param>
        /// <param name="sDefaultValue">缺省值</param>
        /// <returns>字符串值</returns>
        public string GetCustomParamStringDefault(string sKey, string sDefaultValue)
        {
            string sRet = GetCustomParamString(sKey);
            if (sRet == null)
                return sDefaultValue;
            else
                return sRet;
        }

        #endregion 自定义参数
    }
}
