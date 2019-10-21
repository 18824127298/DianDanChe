using System;
using System.Collections;

namespace Sigbit.Common
{
    /// <summary>
    /// 对返回值进行封装
    /// </summary>
    public class ReturnValue
    {
        private bool hasError;
        private string errorCode;
        private string message;
        private object returnObject;
        private Hashtable mapData = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReturnValue()
        {
            hasError = false;
            errorCode = "";
            message = "";
            returnObject = null;
        }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="hasError">是否有错误</param>
        /// <param name="errCode">错误代码</param>
        /// <param name="message">错误信息</param>
        /// <param name="retObj">返回类</param>
        public ReturnValue(bool hasError, string errCode, string message, object retObj)
        {
            this.hasError = hasError;
            this.errorCode = errCode;
            this.message = message;
            this.returnObject = retObj;
        }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="hasError">是否有错误</param>
        /// <param name="errCode">错误代码</param>
        /// <param name="message">错误信息</param>
        public ReturnValue(bool hasError, string errCode, string message)
        {
            this.hasError = hasError;
            this.errorCode = errCode;
            this.message = message;
            this.returnObject = null;
        }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="hasError">是否有错误</param>
        /// <param name="message">错误信息</param>
        public ReturnValue(bool hasError, string message)
        {
            this.hasError = hasError;
            this.errorCode = null;
            this.message = message;
            this.returnObject = null;
        }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="errCode">错误代码</param>
        /// <param name="errMessage">错误信息</param>
        public ReturnValue(string errCode, string errMessage)
        {
            this.hasError = true;
            this.errorCode = errCode;
            this.message = errMessage;
            this.returnObject = null;
        }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="errMessage">错误信息</param>
        public ReturnValue(string errMessage)
        {
            this.hasError = false;
            this.errorCode = null;
            this.message = errMessage;
            this.returnObject = null;
        }

        /// <summary>
        /// 是否有错误
        /// </summary>
        public bool HasError
        {
            get
            {
                return hasError;
            }
            set
            {
                hasError = value;
            }
        }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode
        {
            get
            {
                return errorCode;
            }
            set
            {
                errorCode = value;
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }

        /// <summary>
        /// 返回的类
        /// </summary>
        public object ReturnObject
        {
            get
            {
                return returnObject;
            }
            set
            {
                returnObject = value;
            }
        }

        /// <summary>
        /// 得到值对象
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>值对象</returns>
        public object GetValue(string key)
        {
            if (mapData == null)
            {
                return null;
            }
            if (!mapData.ContainsKey(key))
            {
                return null;
            }
            return mapData[key];

        }

        /// <summary>
        /// 得到值字符串
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>值字符串</returns>
        public string GetStringValue(string key, string defaultValue)
        {
            object objValue = GetValue(key);
            if (objValue == null)
            {
                return defaultValue;
            }
            else
            {
                return objValue.ToString();
            }
        }

        /// <summary>
        /// 得到值字符串
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>值字符串。缺省为""</returns>
        public string GetStringValue(string key)
        {
            return GetStringValue(key, "");
        }

        /// <summary>
        /// 得到值整型
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>值整型</returns>
        public int GetIntValue(string key, int defaultValue)
        {
            object objValue = GetValue(key);
            try
            {
                if (objValue == null)
                {
                    return defaultValue;
                }
                return Int32.Parse(objValue.ToString());
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 得到值整型
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>值整型。缺省为0</returns>
        public int GetIntValue(string key)
        {
            return GetIntValue(key, 0);
        }

        /// <summary>
        /// 得到值布尔型
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>值布尔型</returns>
        public bool GetBooleanValue(string key, bool defaultValue)
        {
            object objValue = GetValue(key);
            try
            {
                if (objValue == null)
                {
                    return defaultValue;
                }
                return Boolean.Parse(objValue.ToString());
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 得到值布尔型
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>值布尔型。缺省为false。</returns>
        public bool GetBooleanValue(string key)
        {
            return GetBooleanValue(key, false);
        }

        /// <summary>
        /// 得到值Decimal类型
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>值Decimal类型</returns>
        public decimal GetDecimalValue(string key, decimal defaultValue)
        {
            object objValue = GetValue(key);
            try
            {
                if (objValue == null)
                {
                    return defaultValue;
                }
                return Decimal.Parse(objValue.ToString());
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 得到值Decimal类型
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>值Decimal类型，缺省为0</returns>
        public decimal GetDecimalValue(string key)
        {
            return GetDecimalValue(key, 0);
        }

        /// <summary>
        /// 得到值浮点型
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>值浮点型</returns>
        public double GetFloatValue(string key, double defaultValue)
        {
            object objValue = GetValue(key);
            try
            {
                if (objValue == null)
                {
                    return defaultValue;
                }
                return double.Parse(objValue.ToString());
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 得到值浮点型
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>值浮点型。缺省为0</returns>
        public double GetFloatValue(string key)
        {
            return GetFloatValue(key, 0);
        }

        /// <summary>
        /// 设置一个值
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="valueObj">待设的值</param>
        public void PutValue(string key, object valueObj)
        {
            if (mapData == null)
            {
                mapData = new Hashtable(5);
            }
            mapData[key] = valueObj;
        }
    }
}
