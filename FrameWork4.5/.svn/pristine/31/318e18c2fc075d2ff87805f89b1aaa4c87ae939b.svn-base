using System;
using System.Collections;

using System.Text;

namespace Sigbit.Common
{
    /// <summary>
    /// 字符串处理的相关函数集
    /// </summary>
    public sealed class StringUtil
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        private StringUtil()
        {
        }

        /// <summary>
        /// 得到一个字符重复多遍后生成的字符串
        /// </summary>
        /// <param name="c">字符</param>
        /// <param name="count">重复次数</param>
        /// <returns>结果字符串</returns>
        public static string RepeatChar(char c, int count)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                sb.Append(c);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 得到一个字符串重复多遍后生成的字符串
        /// </summary>
        /// <param name="c">字符串</param>
        /// <param name="count">重复次数</param>
        /// <returns>结果字符串</returns>
        public static string RepeatChar(string c, int count)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                sb.Append(c);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 得到由空格组成的指定长度的字符串
        /// </summary>
        /// <param name="count">指定长度</param>
        /// <returns>得到的字符串</returns>
        public static string Space(int count)
        {
            return RepeatChar(' ', count);
        }

        /// <summary>
        /// 判断一个字符串是否为数字串
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns>是否数字串</returns>
        public static bool IsNumber(string s)
        {
            try
            {
                Int32.Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region FormatCurrency

        /// <summary>
        /// 对金额进行格式化
        /// </summary>
        /// <param name="amt">金额对象</param>
        /// <param name="thousandSep">是否加千位间隔</param>
        /// <param name="precision">精度</param>
        /// <returns>格式化后的字符串</returns>
        public static string FormatCurrency(object amt, bool thousandSep, int precision)
        {
            double fNum;
            fNum = ConvertUtil.ToFloat(amt);

            string formatStr;
            if (thousandSep)
                formatStr = "#,##0";
            else
                formatStr = "0";

            if (precision > 0)
                formatStr += ".";

            formatStr += RepeatChar('0', precision);

            return fNum.ToString(formatStr);
        }

        /// <summary>
        /// 对金额进行格式化
        /// </summary>
        /// <param name="amt">金额对象</param>
        /// <returns>格式化后的字符串。带千位分隔符，2位精度。</returns>
        public static string FormatCurrency(object amt)
        {
            return FormatCurrency(amt, true, 2);
        }

        /// <summary>
        /// 对金额进行格式化
        /// </summary>
        /// <param name="amt">金额对象</param>
        /// <param name="thousandSep">是否加千位间隔</param>
        /// <returns>格式化后的字符串。2位精度。</returns>
        public static string FormatCurrency(object amt, bool thousandSep)
        {
            return FormatCurrency(amt, thousandSep, 2);
        }

        #endregion FormatCurrency

        #region QuotedToDBStr

        /// <summary>
        /// 对字符串格式化为符合SQL语句的带引号的字符串
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>格式化的字符串</returns>
        /// <remarks>该函数做两件事：
        /// 1. 将字符串内的单引号变为两个单引号；
        /// 2. 前后加上单引号；
        /// </remarks>
        public static string QuotedToDBStr(string str)
        {
            return "'" + str.Replace("'", "''") + "'";
        }

        /// <summary>
        /// 将对象格式化对符合SQL语句的单引号的字符串
        /// </summary>
        /// <param name="str">对象</param>
        /// <param name="defaultStr">缺省字符串</param>
        /// <returns>格式化后的字符串</returns>
        /// <remarks>如缺省的字符串为null值，则返回的字符串为null</remarks>
        public static string QuotedToDBStr(object str, string defaultStr)
        {
            if (str == null || str is System.DBNull)
            {
                if (defaultStr == null)
                {
                    return "null";
                }
                else
                {
                    return QuotedToDBStr((string)defaultStr);
                }
            }
            else
            {
                return QuotedToDBStr((string)str);
            }
        }

        /// <summary>
        /// 将对象格式化对符合SQL语句的单引号的字符串
        /// </summary>
        /// <param name="str">对象</param>
        /// <returns>格式化后的字符串，缺省为""。</returns>
        public static string QuotedToDBStr(object str)
        {
            return QuotedToDBStr(str, "");
        }

        #endregion QuotedToDBStr

        #region BoolToDBStr

        /// <summary>
        /// 将布尔值转换为SQL语句的字符串
        /// </summary>
        /// <param name="flag">布尔值</param>
        /// <returns>得到的字符串，取值非1即0</returns>
        public static string BoolToDBStr(bool flag)
        {
            if (flag)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        /// <summary>
        /// 将对象转换为SQL语句的字符串
        /// </summary>
        /// <param name="objFlag">对象</param>
        /// <returns>得到的字符串，取值非1即0</returns>
        public static string BoolToDBStr(object objFlag)
        {
            bool flag = false;
            try
            {
                flag = Boolean.Parse(objFlag.ToString());
            }
            catch
            {
                flag = false;
            }
            return BoolToDBStr(flag);
        }
        #endregion BoolToDBStr

        /// <summary>
        /// 计数某子串在字符串中产生的次数
        /// </summary>
        /// <param name="sSubStr">待寻找的子串</param>
        /// <param name="sString">包含子串的字符串</param>
        /// <returns>计数得到的次数</returns>
        public static int Occurs(string sSubStr, string sString)
        {
            int nRet = 0;
            bool bFound = true;

            while (bFound)
            {
                int nFindPos = sString.IndexOf(sSubStr);
                if (nFindPos != -1)
                {
                    nRet++;
                    sString = sString.Substring(nFindPos + sSubStr.Length);
                }
                else
                    bFound = false;
            }

            return nRet;
        }

        /// <summary>
        /// 分隔字符串为数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="sSplitStr">分隔符</param>
        /// <returns>经分隔的字符串数组</returns>
        /// <remarks>.net自带的split方法只能用一个字符分隔</remarks>
        public static string[] Split(string str, string sSplitStr)
        {
            int nPos;
            string[] arrStr;
            ArrayList arrList = new ArrayList();
            nPos = -1;
            nPos = str.IndexOf(sSplitStr);
            while (nPos >= 0)
            {
                arrList.Add(str.Substring(0, nPos));
                str = str.Substring(nPos + sSplitStr.Length);
                nPos = str.IndexOf(sSplitStr);
            }
            arrList.Add(str);
            arrStr = new string[arrList.Count];
            arrStr = (string[])arrList.ToArray(Type.GetType("System.String"));
            arrList = null;
            return arrStr;
        }

        /// <summary>
        /// 将文本转换为HTML的格式
        /// </summary>
        /// <param name="sText">待转换的文本</param>
        /// <returns>HTML格式的文本</returns>
        public static string ConvertText2Html(string sText)
        {
            sText = sText.Replace(">", "&gt;");
            sText = sText.Replace("<", "&lt;");
            sText = sText.Replace("\r\n", "<br>");
            sText = sText.Replace("\n\r", "<br>");
            sText = sText.Replace("\r", "<br>");
            sText = sText.Replace("\n", "<br>");
            sText = sText.Replace(" ", "&nbsp;");
            return sText;
        }

        /// <summary>
        /// 将HTML格式转换为普通文本格式
        /// </summary>
        /// <param name="sText">HTML格式字符串</param>
        /// <returns>普通文本格式字符串</returns>
        public static string ConvertHtml2Text(string sText)
        {
            sText = sText.Replace("&nbsp;", " ");
            sText = sText.Replace("<br>", "\n");
            sText = sText.Replace("&lt;", "<");
            sText = sText.Replace("&gt;", ">");

            return sText;
        }

        /// <summary>
        /// 替换Html标签
        /// </summary>
        /// <param name="sHtmlString">转换前的字符串</param>
        /// <returns>去掉<>标签后的字符串</returns>
        public static string ReplaceHtmlTag(string sHtmlString)
        {
            string sText = System.Text.RegularExpressions.Regex.Replace(sHtmlString, "<[^>]+>", "");
            sText = System.Text.RegularExpressions.Regex.Replace(sText, "&[^;]+;", "");

            return sText;
        }

        /// <summary>
        /// 得到字符串的字节长度
        /// </summary>
        /// <param name="sText">字符串</param>
        /// <returns>长度</returns>
        /// <remarks>中文的长度为两个字节，英文为一个字节</remarks>
        public static int GetTextLength(string sText)
        {
            char[] arrText = sText.ToCharArray();
            int length = 0;
            for (int i = 0; i < arrText.Length; i++)
            {
                if (((int)arrText[i]) > 19900)
                    length = length + 2;
                else
                    length = length + 1;
            }
            arrText = null;
            return length;
        }

        /// <summary>
        /// 判断是否为汉字字符
        /// </summary>
        /// <param name="ch">字符</param>
        /// <returns>如果是汉字字符则返回true，否则返回false</returns>
        public static bool IsHZChar(char ch)
        {
            if ((int)ch > 19900)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 判断是否为英文字母
        /// </summary>
        /// <param name="ch">字符</param>
        /// <returns>如果是英文字母则返回true，否则返回false</returns>
        public static bool IsEnglishLetter(char ch)
        {
            if (ch >= 'A' && ch <= 'Z') 
                return true;
            if (ch >= 'a' && ch <= 'z')
                return true;
            return false;
        }

        /// <summary>
        /// 从字符串中解析出参数值
        /// </summary>
        /// <param name="QueryString">查询参数串</param>
        /// <param name="ParameterName">参数名称</param>
        /// <returns>获得的参数值</returns>
        /// <remarks>字符串按urlLink的参数格式编码</remarks>
        public static string GetParameterValue(string QueryString, string ParameterName)
        {
            int nPos = QueryString.IndexOf(ParameterName + "=");
            if (nPos >= 0)
            {
                int nPos2 = QueryString.IndexOf("&", nPos);
                if (nPos2 > 0)
                    return QueryString.Substring(nPos + ParameterName.Length + 1,
                            nPos2 - nPos - ParameterName.Length - 1);
                else
                    return QueryString.Substring(nPos + ParameterName.Length + 1);
            }
            return "";
        }

        /// <summary>
        /// 转化为string 类型
        /// </summary>
        /// <param name="obj">待转化的object类型</param>
        /// <returns>转化结果</returns>
        public static string DbToString(object obj)
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

        #region 缩写
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sSrcString"></param>
        /// <param name="nLength"></param>
        /// <returns></returns>
        public static string ShortenString(string sSrcString, int nLength)
        {
            string sRet = ShortenString(sSrcString, nLength, 'x');
            return sRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sSrcString"></param>
        /// <param name="nLength"></param>
        /// <param name="cFillChar"></param>
        /// <returns></returns>
        public static string ShortenString(string sSrcString, int nLength, char cFillChar)
        {
            string sRegulateString = sSrcString.Replace(" ", "").Replace("_", "-");

            //======== 1. 如果包含分隔线，则取分隔线的第一个字母 ==========
            string sRet = sRegulateString;
            if (sRegulateString.IndexOf("-") != -1)
            {
                sRet = "";

                string[] arrItems = sRegulateString.Split('-');
                for (int i = 0; i < arrItems.Length; i++)
                {
                    string sItem = arrItems[i];

                    if (sItem.Length > 0)
                    {
                        sRet += sItem[0];
                    }
                }
            }

            //============ 2. 取指定长度，不足则填充 ==========
            if (sRet.Length > nLength)
            {
                sRet = sRet.Substring(0, nLength);
            }
            else if (sRet.Length < nLength)
            {
                int nFillLength = nLength - sRet.Length;
                for (int i = 0; i < nFillLength; i++)
                    sRet += cFillChar;
            }

            return sRet;
        }

        #endregion
    }
}


