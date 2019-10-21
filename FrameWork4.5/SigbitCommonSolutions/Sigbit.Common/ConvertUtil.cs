using System;
using System.Collections.Generic;
using System.Text;

using System.Text.RegularExpressions;

namespace Sigbit.Common
{
    /// <summary>
    /// 处理各个类型之间的转换
    /// </summary>
    public class ConvertUtil
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        private ConvertUtil()
        {
        }

        #region NotNullStr

        /// <summary>
        /// 返回非空(null)字符串
        /// </summary>
        /// <param name="canNullStr">待转换的对象</param>
        /// <param name="defaultStr">缺省字符串</param>
        /// <returns>得到字符串</returns>
        public static string NotNullStr(object canNullStr, string defaultStr)
        {
            if (canNullStr == null)
            {
                if (defaultStr != null)
                {
                    return defaultStr;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return Convert.ToString(canNullStr);
            }
        }

        /// <summary>
        /// 返回非空(null)字符串
        /// </summary>
        /// <param name="canNullStr">待转换的对象</param>
        /// <returns>得到的字符串。如果为对象null，则返回""</returns>
        public static string NotNullStr(object canNullStr)
        {
            return NotNullStr(canNullStr, "");
        }

        #endregion NotNullStr


        #region ToInt

        /// <summary>
        /// 将对象转换为整型
        /// </summary>
        /// <param name="objInt">对象</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>得到的整数</returns>
        public static int ToInt(object objInt, int defaultValue)
        {
            if (objInt == null)
            {
                return defaultValue;
            }
            else if (objInt is string)
            {
                string sString = (string)objInt;
                return ToInt__StringOnly(sString, defaultValue);
            }
            else if (objInt is int)
            {
                return (int)objInt;
            }
            else
            {
                try
                {
                    return Convert.ToInt32(objInt);
                }
                catch
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sToConvertString"></param>
        /// <param name="nDefaultValue"></param>
        /// <returns></returns>
        public static int ToInt__StringOnly(string sToConvertString, int nDefaultValue)
        {
            //Regex rex = new Regex(@"^\d+$");
            Regex rex = new Regex(@"^-?\d+$");
            int nResult = nDefaultValue;
            if (rex.IsMatch(sToConvertString))
            {
                nResult = int.Parse(sToConvertString);
                return nResult;
            }
            else
                return nDefaultValue;
        }

        /// <summary>
        /// 将对象转换为整型
        /// </summary>
        /// <param name="objInt">对象</param>
        /// <returns>得到的整数。缺省为0</returns>
        public static int ToInt(object objInt)
        {
            return ToInt(objInt, 0);
        }

        #endregion ToInt

        #region ToBool
        /// <summary>
        /// 将对象转换为布尔型
        /// </summary>
        /// <param name="objBool">待转换的对象</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>得到的布尔型</returns>
        public static bool ToBool(object objBool, bool defaultValue)
        {
            if (objBool == null || objBool == DBNull.Value)
            {
                return defaultValue;
            }
            else
            {
                try
                {
                    return Convert.ToBoolean(objBool);
                }
                catch
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// 将对象转换为布尔型
        /// </summary>
        /// <param name="objBool">待转换的对象</param>
        /// <returns>得到的布尔型。缺省为false。</returns>
        public static bool ToBool(object objBool)
        {
            return ToBool(objBool, false);
        }

        /// <summary>
        /// 三态布尔转换为布尔型
        /// </summary>
        /// <param name="bool3State">三态布尔型</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>得到的布尔型</returns>
        public static bool ToBool(Bool3State bool3State, bool defaultValue)
        {
            if (bool3State == Bool3State.True)
                return true;
            else if (bool3State == Bool3State.False)
                return false;
            else
                return defaultValue;
        }

        /// <summary>
        /// 三态布尔转换为布尔，缺省值为false
        /// </summary>
        /// <param name="bool3State">三态布尔值</param>
        /// <returns>得到的布尔值</returns>
        public static bool ToBool(Bool3State bool3State)
        {
            return ToBool(bool3State, false);
        }
        #endregion

        #region Bool3State
        /// <summary>
        /// 布尔值转至三态布尔
        /// </summary>
        /// <param name="bBool">布尔值</param>
        /// <returns>三态布尔</returns>
        public static Bool3State ToBool3State(bool bBool)
        {
            if (bBool)
                return Bool3State.True;
            else
                return Bool3State.False;
        }
        #endregion Bool3State

        #region ToDecimal

        /// <summary>
        /// 将对象转换为Decimal类型
        /// </summary>
        /// <param name="objDecimal">待转换的对象</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>得到的Decimal类型</returns>
        public static decimal ToDecimal(object objDecimal, decimal defaultValue)
        {
            if (objDecimal == null)
            {
                return defaultValue;
            }
            else
            {
                try
                {
                    return Convert.ToDecimal(objDecimal);
                }
                catch
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// 将对象转换为Decimal类型
        /// </summary>
        /// <param name="objDecimal">待转换的对象</param>
        /// <returns>得到的Decimal类型。缺省为0</returns>
        public static decimal ToDecimal(object objDecimal)
        {
            return ToDecimal(objDecimal, 0);
        }

        #endregion ToDecimal

        #region ToFloat

        /// <summary>
        /// 将对象转换为Float类型
        /// </summary>
        /// <param name="objFloat">待转换的对象</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>得到的Float类型</returns>
        public static double ToFloat(object objFloat, double defaultValue)
        {
            if (objFloat == null)
            {
                return defaultValue;
            }
            else
            {
                try
                {
                    return Convert.ToDouble(objFloat);
                }
                catch
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// 将对象转换为Float类型
        /// </summary>
        /// <param name="objFloat">待转换的对象</param>
        /// <returns>得到的Float类型。缺省为0</returns>
        public static double ToFloat(object objFloat)
        {
            return ToFloat(objFloat, 0);
        }

        #endregion ToFloat

        #region ToString

        /// <summary>
        /// 将对象转换为字符串类型
        /// </summary>
        /// <param name="objString">待转换为对象</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>得到的字符串类型</returns>
        public static string ToString(object objString, string defaultValue)
        {
            if (objString == null || objString == DBNull.Value)
            {
                return defaultValue;
            }
            else
            {
                try
                {
                    return Convert.ToString(objString);
                }
                catch
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// 将对象转换为字符串类型
        /// </summary>
        /// <param name="objString"></param>
        /// <returns></returns>
        public static string ToString(object objString)
        {
            return ToString(objString, "");
        }

        #endregion

        #region ExtractNumberFromString
        /// <summary>
        /// 将字符串解析为数字
        /// </summary>
        /// <param name="sString">字符串</param>
        /// <param name="fDefault">缺省值</param>
        /// <returns>数字</returns>
        public static double ExtractNumberFromString(string sString, double fDefault)
        {
            //========= 1. 如果有数字，则只取数字部分进行转换 =========
            for (int i = 0; i < sString.Length; i++)
            {
                char cChar = sString[i];
                if ((cChar >= '0' && cChar <= '9') || cChar == '-')
                {
                    string sRearString = sString.Substring(i);
                    sRearString = ExtractNumberFromString__CutRearString(sRearString);
                    double fNumber = ConvertUtil.ToFloat(sRearString, fDefault);
                    return fNumber;
                }
            }

            return fDefault;
        }

        /// <summary>
        /// 取出可以转换为数字的字符串
        /// </summary>
        /// <param name="sRearString">第一位是数字或负数符号的字符串</param>
        /// <returns>可转换为数字的字符串</returns>
        private static string ExtractNumberFromString__CutRearString(string sRearString)
        {
            int nFinalPos = 0;
            bool bHasMeetPoint = false;

            for (int i = 1; i < sRearString.Length; i++)
            {
                char ch = sRearString[i];
                if (ch >= '0' && ch <= '9')
                    nFinalPos = i;
                else if (ch == '.')
                {
                    if (bHasMeetPoint)
                        break;
                    else
                    {
                        bHasMeetPoint = true;
                        nFinalPos = i;
                    }
                }
                else
                    break;
            }

            return sRearString.Substring(0, nFinalPos + 1);
        }

        #endregion ExtractNumberFromString

        #region enum

        /// <summary>
        /// 将枚举转换为字符串
        /// </summary>
        /// <param name="enumObject">枚举值</param>
        /// <returns>字符串。小写，每个单词以下划线分开。</returns>
        public static string EnumToString(object enumObject)
        {
            //========= 1. 转为字符串 ===========
            string sRawString = enumObject.ToString();
            string sLowerString = sRawString.ToLower();

            //======= 2. 处理每一个字符 ============
            StringBuilder sbRet = new StringBuilder();
            for (int i = 0; i < sRawString.Length; i++)
            {
                char chRaw = sRawString[i];
                char chLow = sLowerString[i];

                if (i == 0)
                {
                    sbRet.Append(chLow);
                    continue;
                }

                if (chRaw != chLow)
                    sbRet.Append("_");

                sbRet.Append(chLow);
            }

            return sbRet.ToString();
        }

        /// <summary>
        /// 将字符串转换为特定类型的枚举值。字符串小写，每个单词以下划线分开。
        /// </summary>
        /// <param name="sString">字符串</param>
        /// <param name="enumValue">该类型枚举的某一个值，用于获取类型</param>
        /// <returns>特定类型的枚举值。</returns>
        public static object StringToEnum(string sString, object enumValue)
        {
            //=========== 1. 转换字符串，转为大小写间隔的无下划线的字符串 ===========
            bool bMeetUnderLine = false;

            StringBuilder sbWithUpper = new StringBuilder();

            for (int i = 0; i < sString.Length; i++)
            {
                char ch = sString[i];

                //======== 1.1 第一个字符转为大写 =============
                if (i == 0)
                {
                    char cUpperFirstLetter = Char.ToUpper(ch);
                    sbWithUpper.Append(cUpperFirstLetter);
                    continue;
                }

                //========= 1.2 遇到了下划线 ============
                if (ch == '_')
                {
                    bMeetUnderLine = true;
                    continue;
                }

                //========= 1.3 其它字符的处理 ==============
                char cAppend = ch;
                if (bMeetUnderLine)
                {
                    bMeetUnderLine = false;
                    cAppend = Char.ToUpper(cAppend);
                }

                sbWithUpper.Append(cAppend);
            }

            string sWithUpperString = sbWithUpper.ToString();

            //=========== 2. 转换为特定的枚举类型 ==========
            try
            {
                object oRet = Enum.Parse(enumValue.GetType(), sWithUpperString);
                return oRet;
            }
            catch (Exception ex)
            {
                string sErrorMessage = ex.Message + "(字符串转枚举类型失败，枚举类型为" 
                        + enumValue.GetType().ToString() + ")";
                return enumValue;
                //throw new Exception(sErrorMessage);
            }
        }




        #endregion


    }
}
