using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Diagnostics;

namespace Sigbit.Common.WordProcess
{
    #region 类HZStringRegulateMapBase
    /// <summary>
    /// 对应表形式转换的基类
    /// </summary>
    class HZStringRegulateMapBase
    {
        /// <summary>
        /// 查找的字符集
        /// </summary>
        char[] _arrFindChars = null;

        /// <summary>
        /// 替换的字符集
        /// </summary>
        char[] _arrReplaceWithChars = null;

        /// <summary>
        /// 初始化字符集
        /// </summary>
        /// <param name="arrFindChars">查找的字符集</param>
        /// <param name="arrReplaceWithChars">替换的字符集</param>
        public void InitMap(char[] arrFindChars, char[] arrReplaceWithChars)
        {
            _arrFindChars = arrFindChars;
            _arrReplaceWithChars = arrReplaceWithChars;
        }

        private Hashtable _mapHash = null;
        /// <summary>
        /// 用于转换的Hash对应表
        /// </summary>
        public Hashtable MapHash
        {
            get
            {
                if (_mapHash == null)
                {
                    Debug.Assert(_arrFindChars.Length == _arrReplaceWithChars.Length);

                    _mapHash = new Hashtable();
                    for (int i = 0; i < _arrFindChars.Length; i++)
                        _mapHash[_arrFindChars[i]] = _arrReplaceWithChars[i];
                }
                return _mapHash;
            }
        }

        /// <summary>
        /// 得到规整过的字符串
        /// </summary>
        /// <param name="sRawString">原字符串</param>
        /// <returns>规整过的字符串</returns>
        public string RegulateString(string sRawString)
        {
            //========== 1. 判断有无全角字母, 如无全角字母则返回原串 ============
            int nIndexFind = sRawString.IndexOfAny(_arrFindChars);
            if (nIndexFind == -1)
                return sRawString;

            //=========== 2. 处理每一个字符 ==========
            StringBuilder sbRet = new StringBuilder();
            for (int i = 0; i < sRawString.Length; i++)
            {
                char chRaw = sRawString[i];
                object objRegulate = MapHash[chRaw];
                if (objRegulate == null)
                    sbRet.Append(chRaw);
                else
                    sbRet.Append((char)objRegulate);
            }

            return sbRet.ToString();
        }
    }
    #endregion 类HZStringRegulateMapBase

    #region 类HZStringRegulateMapHalfSizeDigit
    /// <summary>
    /// 全角数字到半角的转换
    /// </summary>
    class HZStringRegulateMapHalfSizeDigit : HZStringRegulateMapBase
    {
        private static HZStringRegulateMapHalfSizeDigit _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static HZStringRegulateMapHalfSizeDigit Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new HZStringRegulateMapHalfSizeDigit();

                return _thisInstance;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public HZStringRegulateMapHalfSizeDigit()
        {
            char[] arrFindChars = new char[] 
                { 
                    '１', '２', '３', '４', '５', '６', '７', '８', '９', '０'
                };

            char[] arrReplaceWith = new char[] 
                { 
                    '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
                };

            InitMap(arrFindChars, arrReplaceWith);
        }
    }
    #endregion 类HZStringRegulateMapHalfSizeDigit
    
    #region 类HZStringRegulateMapHalfSizeLetter
    /// <summary>
    /// 全角数字到半角的转换
    /// </summary>
    class HZStringRegulateMapHalfSizeLetter : HZStringRegulateMapBase
    {
        private static HZStringRegulateMapHalfSizeLetter _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static HZStringRegulateMapHalfSizeLetter Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new HZStringRegulateMapHalfSizeLetter();

                return _thisInstance;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public HZStringRegulateMapHalfSizeLetter()
        {
            char[] arrFindChars = new char[] 
                { 
                'ａ', 'ｂ', 'ｃ', 'ｄ', 'ｅ', 'ｆ', 'ｇ', 'ｈ', 'ｉ', 'ｊ',
                'ｋ', 'ｌ', 'ｍ', 'ｎ', 'ｏ', 'ｐ', 'ｑ', 'ｒ', 'ｓ', 'ｔ',
                'ｕ', 'ｖ', 'ｗ', 'ｘ', 'ｙ', 'ｚ',
                'Ａ', 'Ｂ', 'Ｃ', 'Ｄ', 'Ｅ', 'Ｆ', 'Ｇ', 'Ｈ', 'Ｉ', 'Ｊ',
                'Ｋ', 'Ｌ', 'Ｍ', 'Ｎ', 'Ｏ', 'Ｐ', 'Ｑ', 'Ｒ', 'Ｓ', 'Ｔ',
                'Ｕ', 'Ｖ', 'Ｗ', 'Ｘ', 'Ｙ', 'Ｚ'
                };

            char[] arrReplaceWith = new char[] 
                { 
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
                'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
                'u', 'v', 'w', 'x', 'y', 'z',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
                'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
                'U', 'V', 'W', 'X', 'Y', 'Z'
                };

            InitMap(arrFindChars, arrReplaceWith);
        }
    }
    #endregion 类HZStringRegulateMapHalfSizeLetter

    #region 类HZStringRegulateMapHalfSizePuctuation
    /// <summary>
    /// 全角标点到半角的转换
    /// </summary>
    class HZStringRegulateMapHalfSizePuctuation : HZStringRegulateMapBase
    {
        private static HZStringRegulateMapHalfSizePuctuation _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static HZStringRegulateMapHalfSizePuctuation Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new HZStringRegulateMapHalfSizePuctuation();

                return _thisInstance;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public HZStringRegulateMapHalfSizePuctuation()
        {
            char[] arrFindChars = new char[] 
                { 
                '　', 
                '｀', '－', '＝', '＼', '～', '！', '＃', '＄', '％', '×', 
                '＾', '＆', '（', '）', '＿', '＋', '｜', '［', '］', '｛',
                '｝', '；', '＇', '：', '＂', '，', '。', '／', '？', '÷',
                '＜', '＞', '“', '”', '‘', '’', '·', '、', '—', '…',
                '〈', '〉', '＊', '．', '《', '》'
                };

            char[] arrReplaceWith = new char[] 
                { 
                ' ', 
                '`', '-', '=', '\\', '~', '!', '#', '$', '%', '*', 
                '^', '&', '(', ')', '_', '+', '|', '[', ']', '{',
                '}', ';', '\'', ':', '"', ',', '.', '/', '?', '/',
                '<', '>', '"', '"', '\'', '\'', '.', ',', '-', '.',
                '<', '>', '*', '.', '<', '>'
                };

            InitMap(arrFindChars, arrReplaceWith);
        }
    }
    #endregion 类HZStringRegulateMapHalfSizePuctuation

    #region 类HZStringRegulateMapHalfSizeArabicNumber
    /// <summary>
    /// 汉字数字到阿拉伯数字的转换
    /// </summary>
    class HZStringRegulateMapHalfSizeArabicNumber : HZStringRegulateMapBase
    {
        private static HZStringRegulateMapHalfSizeArabicNumber _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static HZStringRegulateMapHalfSizeArabicNumber Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new HZStringRegulateMapHalfSizeArabicNumber();

                return _thisInstance;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public HZStringRegulateMapHalfSizeArabicNumber()
        {
            char[] arrFindChars = new char[] 
                { 
                '一', '二', '三', '四', '五', '六', '七', '八', '九', '零', '两'
                };

            char[] arrReplaceWith = new char[] 
                { 
                '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '2'
                };

            InitMap(arrFindChars, arrReplaceWith);
        }
    }
    #endregion 类HZStringRegulateMapHalfSizeArabicNumber

    #region 类HZStringRegulateMapFullSizeChsWordNumber
    /// <summary>
    /// 阿拉伯数字到汉字数字的转换
    /// </summary>
    class HZStringRegulateMapFullSizeChsWordNumber : HZStringRegulateMapBase
    {
        private static HZStringRegulateMapFullSizeChsWordNumber _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static HZStringRegulateMapFullSizeChsWordNumber Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new HZStringRegulateMapFullSizeChsWordNumber();

                return _thisInstance;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public HZStringRegulateMapFullSizeChsWordNumber()
        {
            char[] arrFindChars = new char[] 
                { 
                '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
                };

            char[] arrReplaceWith = new char[] 
                { 
                '一', '二', '三', '四', '五', '六', '七', '八', '九', '零'
                };

            InitMap(arrFindChars, arrReplaceWith);
        }
    }
    #endregion 类HZStringRegulateMapHalfSizeArabicNumber

    /// <summary>
    /// 汉字字符串规整规则
    /// </summary>
    public enum HZStringRegulateRule
    {
        /// <summary>
        /// 转换至半角数字
        /// </summary>
        ToHalfSizeDigit = 1,
        /// <summary>
        /// 转换至半角字母
        /// </summary>
        ToHalfSizeLetter = 2,
        /// <summary>
        /// 转换至半角标点符号
        /// </summary>
        ToHalfSizePuctuation = 4,
        /// <summary>
        /// 数字、字母和标点符号都转为半角
        /// </summary>
        ToHalfSizeAll = 7,
        /// <summary>
        /// 删不可见字符，如空格、TAB和回车换行等
        /// </summary>
        DeleteSpace = 8,
        /// <summary>
        /// 大写变小写
        /// </summary>
        ToLower = 16,
        /// <summary>
        /// 数字转为阿拉伯数字
        /// </summary>
        ToArabicNumber = 32,
        /// <summary>
        /// 应用所有的转换规则
        /// </summary>
        RegulateAll = 63,
        /// <summary>
        /// 阿拉伯数字转为汉字数字
        /// </summary>
        ToChsWordNumber
    }

    /// <summary>
    /// 汉字字符相关的转换函数
    /// </summary>
    public class HZStringConvert
    {
        static char[] FULL_SIZE_ALPHAS = new char[] 
            { 
                '　', 
                '１', '２', '３', '４', '５', '６', '７', '８', '９', '０',
                'ａ', 'ｂ', 'ｃ', 'ｄ', 'ｅ', 'ｆ', 'ｇ', 'ｈ', 'ｉ', 'ｊ',
                'ｋ', 'ｌ', 'ｍ', 'ｎ', 'ｏ', 'ｐ', 'ｑ', 'ｒ', 'ｓ', 'ｔ',
                'ｕ', 'ｖ', 'ｗ', 'ｘ', 'ｙ', 'ｚ',
                'Ａ', 'Ｂ', 'Ｃ', 'Ｄ', 'Ｅ', 'Ｆ', 'Ｇ', 'Ｈ', 'Ｉ', 'Ｊ',
                'Ｋ', 'Ｌ', 'Ｍ', 'Ｎ', 'Ｏ', 'Ｐ', 'Ｑ', 'Ｒ', 'Ｓ', 'Ｔ',
                'Ｕ', 'Ｖ', 'Ｗ', 'Ｘ', 'Ｙ', 'Ｚ',
                '｀', '－', '＝', '＼', '～', '！', '＃', '＄', '％', '×', 
                '＾', '＆', '（', '）', '＿', '＋', '｜', '［', '］', '｛',
                '｝', '；', '＇', '：', '＂', '，', '。', '／', '？', '÷',
                '＜', '＞', '“', '”', '‘', '’', '·', '、', '—', '…',
                '〈', '〉', '＊', '．', '《', '》'
            };

        static char[] HALF_SIZE_ALPHAS = new char[] 
            { 
                ' ', 
                '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
                'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
                'u', 'v', 'w', 'x', 'y', 'z',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
                'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
                'U', 'V', 'W', 'X', 'Y', 'Z',
                '`', '-', '=', '\\', '~', '!', '#', '$', '%', '*', 
                '^', '&', '(', ')', '_', '+', '|', '[', ']', '{',
                '}', ';', '\'', ':', '"', ',', '.', '/', '?', '/',
                '<', '>', '"', '"', '\'', '\'', '.', ',', '-', '.',
                '<', '>', '*', '.', '<', '>'
            };

        private static Hashtable _staHtFullHalfMap = null;
        /// <summary>
        /// 全角到半角的哈希对应表
        /// </summary>
        private static Hashtable FullHalfAlphaMap
        {
            get
            {
                if (_staHtFullHalfMap == null)
                {
                    Debug.Assert(FULL_SIZE_ALPHAS.Length == HALF_SIZE_ALPHAS.Length);

                    _staHtFullHalfMap = new Hashtable();
                    for (int i = 0; i < FULL_SIZE_ALPHAS.Length; i++)
                        _staHtFullHalfMap[FULL_SIZE_ALPHAS[i]] = HALF_SIZE_ALPHAS[i];
                }
                return _staHtFullHalfMap;
            }
        }

        /// <summary>
        /// 转换到半角字符串
        /// </summary>
        /// <param name="sFullSizeAlphaStr">全角字符串</param>
        /// <returns>半角字符串</returns>
        /// <remarks>
        /// 转换包括全角的英文、数字、标点、空格
        /// </remarks>
        public static string ToHalfSizeAlpha(string sFullSizeAlphaStr)
        {
            //========== 1. 判断有无全角字母, 如无全角字母则返回原串 ============
            int nIndexAlpha = sFullSizeAlphaStr.IndexOfAny(FULL_SIZE_ALPHAS);
            if (nIndexAlpha == -1)
                return sFullSizeAlphaStr;

            //=========== 2. 处理每一个字符 ==========
            StringBuilder sbRet = new StringBuilder();
            for (int i = 0; i < sFullSizeAlphaStr.Length; i++)
            {
                char chFull = sFullSizeAlphaStr[i];
                object objHalf = FullHalfAlphaMap[chFull];
                if (objHalf == null)
                    sbRet.Append(chFull);
                else
                    sbRet.Append((char)objHalf);
            }

            return sbRet.ToString();
        }

        /// <summary>
        /// 转换、规整汉字字符串
        /// </summary>
        /// <param name="sHZString">汉字字符串</param>
        /// <param name="rule">规整规则</param>
        /// <returns>规整后的字符串</returns>
        public static string RegulateHZString(string sHZString, HZStringRegulateRule rule)
        {
            string sRet = sHZString;

            //=========== 1. 半角数字 ===============
            if ((rule & HZStringRegulateRule.ToHalfSizeDigit) 
                    == HZStringRegulateRule.ToHalfSizeDigit)
                sRet = HZStringRegulateMapHalfSizeDigit.Instance.RegulateString(sRet);

            //======== 2. 半角字母 ================
            if ((rule & HZStringRegulateRule.ToHalfSizeLetter)
                    == HZStringRegulateRule.ToHalfSizeLetter)
                sRet = HZStringRegulateMapHalfSizeLetter.Instance.RegulateString(sRet);

            //======== 3. 半角标点符号 ===============
            if ((rule & HZStringRegulateRule.ToHalfSizePuctuation)
                    == HZStringRegulateRule.ToHalfSizePuctuation)
                sRet = HZStringRegulateMapHalfSizePuctuation.Instance.RegulateString(sRet);

            //======== 4. 去空格 =============
            if ((rule & HZStringRegulateRule.DeleteSpace)
                    == HZStringRegulateRule.DeleteSpace)
                sRet = RegulateHZString__DeleteSpace(sRet);

            //========= 5. 大写变小写 =========
            if ((rule & HZStringRegulateRule.ToLower)
                    == HZStringRegulateRule.ToLower)
                sRet = sRet.ToLower();

            //=========== 6. 汉字数字到阿拉伯数字 ===========
            if ((rule & HZStringRegulateRule.ToArabicNumber)
                    == HZStringRegulateRule.ToArabicNumber)
                sRet = RegulateHZString__ToArabicNumber(sRet);

            //========== 7. 阿拉伯数字到汉字数字 ============
            if ((rule & HZStringRegulateRule.ToChsWordNumber)
                    == HZStringRegulateRule.ToChsWordNumber)
                sRet = HZStringRegulateMapFullSizeChsWordNumber.Instance.RegulateString(sRet);

            return sRet;
        }

        /// <summary>
        /// 不显示的字符
        /// </summary>
        static char[] SPACE_CHARS = new char[] 
            { 
                ' ', '\t', '\n', '\r', '　'
            };
        /// <summary>
        /// 删除不显示的字符
        /// </summary>
        /// <param name="sRaw">原始串</param>
        /// <returns>返回串</returns>
        private static string RegulateHZString__DeleteSpace(string sRaw)
        {
            int nIndexSpace = sRaw.IndexOfAny(SPACE_CHARS);
            if (nIndexSpace == -1)
                return sRaw;

            string sRet = sRaw;
            for (int i = 0; i < SPACE_CHARS.Length; i++)
            {
                string sFindStr = "";
                sFindStr += SPACE_CHARS[i];
                sRet = sRet.Replace(sFindStr, "");
            }
            return sRet;
        }

        /// <summary>
        /// 汉字数字到阿拉伯数字
        /// </summary>
        /// <param name="sHZString">原始串</param>
        /// <returns>返回串</returns>
        private static string RegulateHZString__ToArabicNumber(string sHZString)
        {
            const string HZ_NUMBER_STRING = "一二三四五六七八九十零两";
            const string HZ_NUMBER_FULL = "一二三四五六七八九零两点亿万千百十1234567890";

            const string HZ_HUNDREDS = "点亿万千百十";
            bool bMeetHundreds = false;

            //======= 1. 将到汉字数字的起始点 =========
            int nStartHZNumberPos = -1;

            for (int i = 0; i < sHZString.Length; i++)
            {
                char ch = sHZString[i];

                if (ch == '十')
                    bMeetHundreds = true;

                if (HZ_NUMBER_STRING.IndexOf(ch) != -1)
                {
                    nStartHZNumberPos = i;
                    break;
                }
            }

            //========= 2. 如果找不到起始点，就直接返回原串 ==========
            if (nStartHZNumberPos == -1)
                return sHZString;

            //========= 3. 找到终止点 ===========
            int nEndHZNumberPos = nStartHZNumberPos;
            for (int i = nStartHZNumberPos + 1; i < sHZString.Length; i++)
            {
                char ch = sHZString[i];
                if (HZ_NUMBER_FULL.IndexOf(ch) != -1)
                    nEndHZNumberPos = i;
                else
                    break;

                if (HZ_HUNDREDS.IndexOf(ch) != -1)
                    bMeetHundreds = true;
            }

            //======== 4. 得到起终点之间的字符串及替换字符串，并进行替换 =========
            string sWantReplaceStr = sHZString.Substring(nStartHZNumberPos,
                    nEndHZNumberPos - nStartHZNumberPos + 1);

            string sReplaceWithStr;
            if (bMeetHundreds)
            {
                sReplaceWithStr = HZNumberConvert.HZStringToFloat(sWantReplaceStr).ToString();
            }
            else
            {
                sReplaceWithStr = HZStringRegulateMapHalfSizeArabicNumber.Instance
                        .RegulateString(sWantReplaceStr);
            }

            //string sRet = sHZString.Replace(sWantReplaceStr, sReplaceWithStr);
            string sRet = sHZString.Substring(0, nStartHZNumberPos) + sReplaceWithStr + sHZString.Substring(nEndHZNumberPos + 1);

            sRet = RegulateHZString__ToArabicNumber(sRet);

            return sRet;
        }

    }
}
