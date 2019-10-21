using System;
using System.Collections.Generic;
using System.Text;

using System.Text.RegularExpressions;

namespace Sigbit.Common
{
    /// <summary>
    /// �����������֮���ת��
    /// </summary>
    public class ConvertUtil
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        private ConvertUtil()
        {
        }

        #region NotNullStr

        /// <summary>
        /// ���طǿ�(null)�ַ���
        /// </summary>
        /// <param name="canNullStr">��ת���Ķ���</param>
        /// <param name="defaultStr">ȱʡ�ַ���</param>
        /// <returns>�õ��ַ���</returns>
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
        /// ���طǿ�(null)�ַ���
        /// </summary>
        /// <param name="canNullStr">��ת���Ķ���</param>
        /// <returns>�õ����ַ��������Ϊ����null���򷵻�""</returns>
        public static string NotNullStr(object canNullStr)
        {
            return NotNullStr(canNullStr, "");
        }

        #endregion NotNullStr


        #region ToInt

        /// <summary>
        /// ������ת��Ϊ����
        /// </summary>
        /// <param name="objInt">����</param>
        /// <param name="defaultValue">ȱʡֵ</param>
        /// <returns>�õ�������</returns>
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
        /// ������ת��Ϊ����
        /// </summary>
        /// <param name="objInt">����</param>
        /// <returns>�õ���������ȱʡΪ0</returns>
        public static int ToInt(object objInt)
        {
            return ToInt(objInt, 0);
        }

        #endregion ToInt

        #region ToBool
        /// <summary>
        /// ������ת��Ϊ������
        /// </summary>
        /// <param name="objBool">��ת���Ķ���</param>
        /// <param name="defaultValue">ȱʡֵ</param>
        /// <returns>�õ��Ĳ�����</returns>
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
        /// ������ת��Ϊ������
        /// </summary>
        /// <param name="objBool">��ת���Ķ���</param>
        /// <returns>�õ��Ĳ����͡�ȱʡΪfalse��</returns>
        public static bool ToBool(object objBool)
        {
            return ToBool(objBool, false);
        }

        /// <summary>
        /// ��̬����ת��Ϊ������
        /// </summary>
        /// <param name="bool3State">��̬������</param>
        /// <param name="defaultValue">ȱʡֵ</param>
        /// <returns>�õ��Ĳ�����</returns>
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
        /// ��̬����ת��Ϊ������ȱʡֵΪfalse
        /// </summary>
        /// <param name="bool3State">��̬����ֵ</param>
        /// <returns>�õ��Ĳ���ֵ</returns>
        public static bool ToBool(Bool3State bool3State)
        {
            return ToBool(bool3State, false);
        }
        #endregion

        #region Bool3State
        /// <summary>
        /// ����ֵת����̬����
        /// </summary>
        /// <param name="bBool">����ֵ</param>
        /// <returns>��̬����</returns>
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
        /// ������ת��ΪDecimal����
        /// </summary>
        /// <param name="objDecimal">��ת���Ķ���</param>
        /// <param name="defaultValue">ȱʡֵ</param>
        /// <returns>�õ���Decimal����</returns>
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
        /// ������ת��ΪDecimal����
        /// </summary>
        /// <param name="objDecimal">��ת���Ķ���</param>
        /// <returns>�õ���Decimal���͡�ȱʡΪ0</returns>
        public static decimal ToDecimal(object objDecimal)
        {
            return ToDecimal(objDecimal, 0);
        }

        #endregion ToDecimal

        #region ToFloat

        /// <summary>
        /// ������ת��ΪFloat����
        /// </summary>
        /// <param name="objFloat">��ת���Ķ���</param>
        /// <param name="defaultValue">ȱʡֵ</param>
        /// <returns>�õ���Float����</returns>
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
        /// ������ת��ΪFloat����
        /// </summary>
        /// <param name="objFloat">��ת���Ķ���</param>
        /// <returns>�õ���Float���͡�ȱʡΪ0</returns>
        public static double ToFloat(object objFloat)
        {
            return ToFloat(objFloat, 0);
        }

        #endregion ToFloat

        #region ToString

        /// <summary>
        /// ������ת��Ϊ�ַ�������
        /// </summary>
        /// <param name="objString">��ת��Ϊ����</param>
        /// <param name="defaultValue">ȱʡֵ</param>
        /// <returns>�õ����ַ�������</returns>
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
        /// ������ת��Ϊ�ַ�������
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
        /// ���ַ�������Ϊ����
        /// </summary>
        /// <param name="sString">�ַ���</param>
        /// <param name="fDefault">ȱʡֵ</param>
        /// <returns>����</returns>
        public static double ExtractNumberFromString(string sString, double fDefault)
        {
            //========= 1. ��������֣���ֻȡ���ֲ��ֽ���ת�� =========
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
        /// ȡ������ת��Ϊ���ֵ��ַ���
        /// </summary>
        /// <param name="sRearString">��һλ�����ֻ������ŵ��ַ���</param>
        /// <returns>��ת��Ϊ���ֵ��ַ���</returns>
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
        /// ��ö��ת��Ϊ�ַ���
        /// </summary>
        /// <param name="enumObject">ö��ֵ</param>
        /// <returns>�ַ�����Сд��ÿ���������»��߷ֿ���</returns>
        public static string EnumToString(object enumObject)
        {
            //========= 1. תΪ�ַ��� ===========
            string sRawString = enumObject.ToString();
            string sLowerString = sRawString.ToLower();

            //======= 2. ����ÿһ���ַ� ============
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
        /// ���ַ���ת��Ϊ�ض����͵�ö��ֵ���ַ���Сд��ÿ���������»��߷ֿ���
        /// </summary>
        /// <param name="sString">�ַ���</param>
        /// <param name="enumValue">������ö�ٵ�ĳһ��ֵ�����ڻ�ȡ����</param>
        /// <returns>�ض����͵�ö��ֵ��</returns>
        public static object StringToEnum(string sString, object enumValue)
        {
            //=========== 1. ת���ַ�����תΪ��Сд��������»��ߵ��ַ��� ===========
            bool bMeetUnderLine = false;

            StringBuilder sbWithUpper = new StringBuilder();

            for (int i = 0; i < sString.Length; i++)
            {
                char ch = sString[i];

                //======== 1.1 ��һ���ַ�תΪ��д =============
                if (i == 0)
                {
                    char cUpperFirstLetter = Char.ToUpper(ch);
                    sbWithUpper.Append(cUpperFirstLetter);
                    continue;
                }

                //========= 1.2 �������»��� ============
                if (ch == '_')
                {
                    bMeetUnderLine = true;
                    continue;
                }

                //========= 1.3 �����ַ��Ĵ��� ==============
                char cAppend = ch;
                if (bMeetUnderLine)
                {
                    bMeetUnderLine = false;
                    cAppend = Char.ToUpper(cAppend);
                }

                sbWithUpper.Append(cAppend);
            }

            string sWithUpperString = sbWithUpper.ToString();

            //=========== 2. ת��Ϊ�ض���ö������ ==========
            try
            {
                object oRet = Enum.Parse(enumValue.GetType(), sWithUpperString);
                return oRet;
            }
            catch (Exception ex)
            {
                string sErrorMessage = ex.Message + "(�ַ���תö������ʧ�ܣ�ö������Ϊ" 
                        + enumValue.GetType().ToString() + ")";
                return enumValue;
                //throw new Exception(sErrorMessage);
            }
        }




        #endregion


    }
}
