using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Diagnostics;

namespace Sigbit.Common.WordProcess
{
    /// <summary>
    /// 去标点的方式
    /// </summary>
    public enum TrimPuctuationMethod
    {
        /// <summary>
        /// 去掉前面的标点
        /// </summary>
        Begin,
        /// <summary>
        /// 去掉后面的标点
        /// </summary>
        End,
        /// <summary>
        /// 两头去标点
        /// </summary>
        Both
    }

    /// <summary>
    /// 汉字字符串的处理类
    /// </summary>
    public class HZStringProc
    {
        /// <summary>
        /// 取出字符串中的全部汉字
        /// </summary>
        /// <param name="sRawStr">字符串</param>
        /// <returns>取出的汉字字符串</returns>
        public static string ObtainPureHZString(string sRawStr)
        {
            return ObtainPureHZString(sRawStr, -1);
        }

        /// <summary>
        /// 取出字符串中的若干汉字
        /// </summary>
        /// <param name="sRawStr">字符串</param>
        /// <param name="nHZCount">汉字的数量，-1表示取出全部</param>
        /// <returns>取出的指定数量的汉字串</returns>
        public static string ObtainPureHZString(string sRawStr, int nHZCount)
        {
            if (nHZCount == 0)
                return "";

            //========= 1. 初始化变量 ==========
            int nStrLen = sRawStr.Length;
            if (nHZCount == -1)
                nHZCount = nStrLen;

            string sRet = "";
            int nHasProcessedHZCount = 0;

            //========= 2. 循环处理每一个字符 ==========
            for (int i = 0; i < nStrLen; i++)
            {
                string sChar = sRawStr.Substring(i, 1);
                if (StringUtil.GetTextLength(sChar) == 2)
                {
                    sRet += sChar;
                    nHasProcessedHZCount++;

                    if (nHasProcessedHZCount >= nHZCount)
                        break;
                }
            }

            return sRet;
        }

        /// <summary>
        /// 右对齐字符串，以空格填充左边
        /// </summary>
        /// <param name="sString">字符串参数</param>
        /// <param name="nWidth">填充的宽度</param>
        /// <returns>对齐后的字符串</returns>
        public static string PadHZLeft(string sString, int nWidth)
        {
            return PadHZLeft(sString, nWidth, ' ');
        }

        /// <summary>
        /// 右对齐字符串，以指定字符填充左边
        /// </summary>
        /// <param name="sString">字符串参数</param>
        /// <param name="nWidth">填充的宽度</param>
        /// <param name="cPaddingChar">填充的字符</param>
        /// <returns>对齐后的字符串</returns>
        public static string PadHZLeft(string sString, int nWidth, char cPaddingChar)
        {
            int nStrLength = Encoding.Default.GetBytes(sString).Length;
            if (nStrLength >= nWidth)
                return sString;

            int nCharCount = sString.Length + nWidth - nStrLength;
            return sString.PadLeft(nCharCount, cPaddingChar);
        }

        /// <summary>
        /// 左对齐字符串，以空格填充右边
        /// </summary>
        /// <param name="sString">字符串参数</param>
        /// <param name="nWidth">填充的宽度</param>
        /// <returns>对齐后的字符串</returns>
        public static string PadHZRight(string sString, int nWidth)
        {
            return PadHZRight(sString, nWidth, ' ');
        }

        /// <summary>
        /// 左对齐字符串，以指定字符填充右边
        /// </summary>
        /// <param name="sString">字符串参数</param>
        /// <param name="nWidth">填充的宽度</param>
        /// <param name="cPaddingChar">填充的字符</param>
        /// <returns>对齐后的字符串</returns>
        public static string PadHZRight(string sString, int nWidth, char cPaddingChar)
        {
            int nStrLength = Encoding.Default.GetBytes(sString).Length;
            if (nStrLength >= nWidth)
                return sString;

            int nCharCount = sString.Length + nWidth - nStrLength;
            return sString.PadRight(nCharCount, cPaddingChar);
        }

        private static Hashtable _staHtPuctuation = null;
        /// <summary>
        /// 全角到半角的哈希对应表
        /// </summary>
        private static Hashtable PuctuationHashTable
        {
            get
            {
                char[] arrPuctuations = new char[] 
                { 
                    '｀', '－', '＝', '＼', '～', '！', '＃', '＄', '％', '×', 
                    '＾', '＆', '（', '）', '＿', '＋', '｜', '［', '］', '｛',
                    '｝', '；', '＇', '：', '＂', '，', '。', '／', '？', '÷',
                    '＜', '＞', '“', '”', '‘', '’', '·', '、', '—', '…',
                    '〈', '〉', '＊', '．', '《', '》',
                    '`', '-', '=', '\\', '~', '!', '#', '$', '%', '*', 
                    '^', '&', '(', ')', '_', '+', '|', '[', ']', '{',
                    '}', ';', '\'', ':', '"', ',', '.', '/', '?', '/',
                    '<', '>', '"', '"', '\'', '\'', '.', ',', '-', '.',
                    '<', '>', '*', '.', '<', '>'
                };

                if (_staHtPuctuation == null)
                {
                    _staHtPuctuation = new Hashtable();
                    for (int i = 0; i < arrPuctuations.Length; i++)
                    {
                        char chPunc = arrPuctuations[i];
                        _staHtPuctuation[chPunc] = chPunc;
                    }
                }
                return _staHtPuctuation;
            }
        }

        /// <summary>
        /// 去掉字符串的标点
        /// </summary>
        /// <param name="sHZString">汉字字符串</param>
        /// <param name="trimMeth">去标点的方式</param>
        /// <returns>去标点的结果</returns>
        public static string TrimPuctuation(string sHZString, TrimPuctuationMethod trimMeth)
        {
            string sRet = sHZString;

            if (trimMeth == TrimPuctuationMethod.Begin || trimMeth == TrimPuctuationMethod.Both)
            {
                while (sRet != "")
                {
                    char ch = sRet[0];
                    if (PuctuationHashTable[ch] != null)
                        sRet = sRet.Substring(1);
                    else
                        break;
                }
            }

            if (trimMeth == TrimPuctuationMethod.End || trimMeth == TrimPuctuationMethod.Both)
            {
                while (sRet != "")
                {
                    char ch = sRet[sRet.Length - 1];
                    if (PuctuationHashTable[ch] != null)
                        sRet = sRet.Substring(0, sRet.Length - 1);
                    else
                        break;
                }
            }
            return sRet;
        }

        /// <summary>
        /// 按字节数截取汉字串
        /// </summary>
        /// <param name="sSrc">源汉字串</param>
        /// <param name="nStartIndex">起始位置</param>
        /// <param name="nBytes">字节数</param>
        /// <returns>子串</returns>
        public static string SubstringBytes(string sSrc, int nStartIndex, int nBytes)
        {
            Debug.Assert(nStartIndex >= 0);
            Debug.Assert(nBytes >= 0);

            int nMinLength = nBytes / 2;
            int nMaxLength = nBytes;

            string sRet = "";

            for (int i = 0; i <= nMaxLength; i++)
            {
                if (i + nStartIndex > sSrc.Length)
                    break;

                string sSub = sSrc.Substring(nStartIndex, i);
                if (HZByteLength(sSub) <= nBytes)
                    sRet = sSub;
            }

            return sRet;
        }

        /// <summary>
        /// 得到汉字串的字节长度
        /// </summary>
        /// <param name="sHZString">汉字串</param>
        /// <returns>字节长度</returns>
        public static int HZByteLength(string sHZString)
        {
            byte[] bsHZ = Encoding.Default.GetBytes(sHZString);
            return bsHZ.Length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sInput"></param>
        /// <returns></returns>
        public static string AddSpaceInTooLongLetterString(string sInput)
        {
            return AddSpaceInTooLongLetterString(sInput, 10);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sInput"></param>
        /// <param name="nMaxLetterLength"></param>
        /// <returns></returns>
        public static string AddSpaceInTooLongLetterString(string sInput, int nMaxLetterLength)
        {
            StringBuilder sbRet = new StringBuilder();

            int nLetterLength = 0;
            for (int i = 0; i < sInput.Length; i++)
            {
                char cChar = sInput[i];
                if ((cChar >= 'A' && cChar <= 'Z') || (cChar >= 'a' && cChar <= 'z')
                        || (cChar >= '0' && cChar <= '9'))
                {
                    nLetterLength++;
                }
                else
                    nLetterLength = 0;

                if (nLetterLength > nMaxLetterLength)
                {
                    sbRet.Append(' ');
                    nLetterLength = 0;
                }

                sbRet.Append(cChar);
            }

            return sbRet.ToString();
        }
    }
}
