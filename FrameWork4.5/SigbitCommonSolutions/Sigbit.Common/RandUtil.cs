using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using System.Diagnostics;

namespace Sigbit.Common
{
    /// <summary>
    /// 随机数的产生方法
    /// </summary>
    public enum RandGenerateMethod
    {
        /// <summary>
        /// 缺省方式
        /// </summary>
        Default,    // 缺省方式
        /// <summary>
        /// GUID方式
        /// </summary>
        Guid        // GUID方式
    }

    class CharList
    {
        //大写字母
        public static char[] UpperCharList
                = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 
                    'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 
                    'U', 'V', 'W', 'X', 'Y', 'Z' };
        //大写字母和数字
        public static char[] UpperNumberCharList
                = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 
                    'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 
                    'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', 
                    '4', '5', '6', '7', '8', '9' };
        //小写字母
        public static char[] LowerCharList
                = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 
                   'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 
                   'u', 'v', 'w', 'x', 'y', 'z' };
        //小写字母和数字
        public static char[] LowerNumberCharList
               = {  'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 
                   'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 
                   'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', 
                   '4', '5', '6', '7', '8', '9' };
        //大写字母和小写字母
        public static char[] UppwerLowerCharList
              = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 
                  'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 
                  'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
                  'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
                  'o', 'p', 'q', 'r', 's', 't','u', 'v', 'w', 'x', 
                  'y', 'z'};
        //大写、小写字母和数字
        public static char[] UppwerLowerNumberCharList
              = {  'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 
                   'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 
                   'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
                   'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
                   'o', 'p', 'q', 'r', 's', 't','u', 'v', 'w', 'x', 
                   'y', 'z', '0', '1', '2', '3','4', '5', '6', '7', 
                   '8', '9'};
        //数字
        public static char[] NumberCharList
               = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    }

    /// <summary>
    /// 枚举类型，生成字符串的方式
    /// </summary>
    public enum RandStringType
    {
        /// <summary>
        /// 生成大写的英文字符串
        /// </summary>
        Upper,
        /// <summary>
        /// 生成小写的英文字符串
        /// </summary>
        Lower,
        /// <summary>
        /// 生成大小写混合的英文字符串
        /// </summary>
        UpperLower,
        /// <summary>
        /// 生成大写与数字混合的字符串
        /// </summary>
        UpperNumber,
        /// <summary>
        /// 生成小写与数字混合的字符串
        /// </summary>
        LowerNumber,
        /// <summary>
        /// 生成大小写与数字混合的字符串
        /// </summary>
        UpperLowerNumber,
        /// <summary>
        /// 生成数字字符串
        /// </summary>
        Number,
        /// <summary>
        /// 汉字字符串
        /// </summary>
        Chs,
        /// <summary>
        /// 汉字和英文数字混合的字符串
        /// </summary>
        ChsEng
    }

    /// <summary>
    /// 生成随机数字、字符串、列表的应用类
    /// </summary>
    public class RandUtil
    {
        private static RandGenerateMethod _randGenerateMethod = RandGenerateMethod.Default;
        /// <summary>
        /// 随机数产生方式
        /// </summary>
        public static RandGenerateMethod RandGenerateMethod
        {
            get { return RandUtil._randGenerateMethod; }
            set { RandUtil._randGenerateMethod = value; }
        }

        /// <summary>
        /// 产生0~1之内的随机数
        /// </summary>
        /// <returns>产生的随机数</returns>
        private static double NextDouble()
        {
            if (RandUtil.RandGenerateMethod == RandGenerateMethod.Guid)
            {
                double fNewRand = Guid.NewGuid().GetHashCode();
                fNewRand -= Int32.MinValue;
                fNewRand /= (double)Int32.MaxValue - Int32.MinValue;
                return fNewRand;
            }
            else
            {
                return MiscUtil.Random.NextDouble();
            }
        }

        /// <summary>
        /// 生成0～MaxValue-1的随机整数
        /// </summary>
        /// <param name="MaxValue">指定的最大值</param>
        /// <returns>返回得到一个小于所指定最大值的非负随机数</returns>
        public static int NewNumber(int MaxValue)
        {
            int nRand = (int)(NextDouble() * MaxValue);
            return nRand;

            //int nRand = MiscUtil.Random.Next(MaxValue);
            //return nRand;
        }

        /// <summary>
        /// 生成从nFromValue到nToValue的随机数字（包括nFromValue和nToValue）
        /// </summary>
        /// <param name="nFromValue">随机数的下界最小值</param>
        /// <param name="nToValue">随机数的上界最大值</param>
        /// <returns>返回得到一个小于所指定最大值的非负随机数</returns>
        public static int NewNumber(int nFromValue, int nToValue)
        {
            int nMinValue = nFromValue > nToValue ? nToValue : nFromValue;
            int nMaxValue = nFromValue > nToValue ? nFromValue : nToValue;

            int nRand = nMinValue + NewNumber(nMaxValue - nMinValue + 1);

            return nRand;

            //if (nFromValue > nToValue)
            //{
            //    int nRand = MiscUtil.Random.Next(nToValue, nFromValue + 1);
            //    return nRand;
            //}
            //else
            //{
            //    int nRand = MiscUtil.Random.Next(nFromValue, nToValue + 1);
            //    return nRand;
            //}
        }

        /// <summary>
        /// 生成随机浮点数
        /// </summary>
        /// <param name="fMaxValue">最大值</param>
        /// <returns>从0至最大值的一个浮点数</returns>
        public static double NewFloat(double fMaxValue)
        {
            return NextDouble() * fMaxValue;
            //return MiscUtil.Random.NextDouble() * fMaxValue;
        }

        /// <summary>
        /// 生成随机浮点数
        /// </summary>
        /// <param name="fFromValue">最小值</param>
        /// <param name="fToValue">最大值</param>
        /// <returns>介于最小值和最大值之间的随机浮点数</returns>
        public static double NewFloat(double fFromValue, double fToValue)
        {
            //========== 1. 找到最小值和最大值 =============
            double fMinValue = fFromValue;
            double fMaxValue = fToValue;

            if (fToValue < fFromValue)
            {
                fMinValue = fToValue;
                fMaxValue = fFromValue;
            }

            //========== 2. 二者之间差值 =============
            double fOffset = fMaxValue - fMinValue;
            //return fMinValue + fOffset * MiscUtil.Random.NextDouble();
            return fMinValue + fOffset * NextDouble();
        }

        /// <summary>
        /// 按指定的类型生成随机字符
        /// </summary>
        /// <param name="randType">字符串的类型</param>
        /// <returns>返回一个指定类型的随机字符串</returns>
        public static char NewChar(RandStringType randType)
        {
            char cRand;
            if (randType == RandStringType.Upper)
            {
                //cRand = CharList.UpperCharList[MiscUtil.Random.Next(26)];
                cRand = CharList.UpperCharList[NewNumber(26)];
            }
            else if (randType == RandStringType.Lower)
            {
                //cRand = CharList.LowerCharList[MiscUtil.Random.Next(26)];
                cRand = CharList.LowerCharList[NewNumber(26)];
            }
            else if (randType == RandStringType.UpperLower)
            {
                //cRand = CharList.UppwerLowerCharList[MiscUtil.Random.Next(52)];
                cRand = CharList.UppwerLowerCharList[NewNumber(52)];
            }
            else if (randType == RandStringType.LowerNumber)
            {
                //cRand = CharList.LowerNumberCharList[MiscUtil.Random.Next(36)];
                cRand = CharList.LowerNumberCharList[NewNumber(36)];
            }
            else if (randType == RandStringType.UpperNumber)
            {
                //cRand = CharList.UpperNumberCharList[MiscUtil.Random.Next(36)];
                cRand = CharList.UpperNumberCharList[NewNumber(36)];
            }
            else if (randType == RandStringType.UpperLowerNumber)
            {
                //cRand = CharList.UppwerLowerNumberCharList[MiscUtil.Random.Next(62)];
                cRand = CharList.UppwerLowerNumberCharList[NewNumber(62)];
            }
            else if (randType == RandStringType.Chs)
            {
                cRand = NewChsChar();
            }
            else if (randType == RandStringType.ChsEng)
            {
                if (NewNumber(100) < 80)
                    cRand = NewChsChar();
                else
                    cRand = NewChar(RandStringType.UpperLowerNumber);
            }
            else
            {
                Debug.Assert(randType == RandStringType.Number);
                //cRand = CharList.NumberCharList[MiscUtil.Random.Next(10)];
                cRand = CharList.NumberCharList[NewNumber(10)];
            }

            return cRand;
        }

        /// <summary>
        /// 生成指定长度、指定类型的字符串
        /// </summary>
        /// <param name="nLength">生成随机字符串的长度</param>
        /// <param name="randType">生成随机字符串的类型</param>
        /// <returns>返回一个指定长度的、指定类型的随机字符串</returns>
        public static string NewString(int nLength, RandStringType randType)
        {
            string sRet = "";
            for (int i = 0; i < nLength; i++)
                sRet += NewChar(randType);
            return sRet;
        }

        /// <summary>
        /// 生成指定长度区间、指定类型的字符串
        /// </summary>
        /// <param name="nFromLength">从该长度开始</param>
        /// <param name="nToLength">到该长度截至</param>
        /// <param name="randType">生成随机字符串的类型</param>
        /// <returns>返回一个指定长度区间、指定类型的字符串</returns>
        public static string NewString(int nFromLength, int nToLength, RandStringType randType)
        {
            int nCurrrentLength = NewNumber(nFromLength, nToLength);
            return NewString(nCurrrentLength, randType);
        }

        /// <summary>
        /// 得到当天范围内的随机时间
        /// </summary>
        /// <returns>返回当天的随机时间</returns>
        public static string NewTime()
        {
            DateTime dtToday = DateTime.Today; //获得当前日期的00:00:00
            int nRandSeconds = NewNumber(24 * 3600);
            TimeSpan ts = new TimeSpan(0, 0, nRandSeconds);
            DateTime dtFinalTime = dtToday + ts;
            string sRet = DateTimeUtil.ToDateTimeStr(dtFinalTime);
            return sRet;
        }

        /// <summary>
        /// 得到从起始时间到终止时间的随机时间
        /// </summary>
        /// <param name="sFromTime">起始时间</param>
        /// <param name="sToTime">终止时间</param>
        /// <returns>返回随机时间</returns>
        public static string NewTime(string sFromTime, string sToTime)
        {
            DateTime dtFromTime = DateTimeUtil.ToDateTime(sFromTime);
            DateTime dtToTime = DateTimeUtil.ToDateTime(sToTime);
            TimeSpan ts = dtToTime - dtFromTime;
            int nRandSecond = NewNumber((int)(ts.TotalSeconds + 1));
            ts = new TimeSpan(0, 0, nRandSecond);
            DateTime dtFinalTime = dtFromTime + ts;
            string sRet = DateTimeUtil.ToDateTimeStr(dtFinalTime);
            return sRet;
        }

        /// <summary>
        /// 得到从指定时间开始，之后0~nSeconds-1秒的随机时间（nSeconds可以为负数）
        /// </summary>
        /// <param name="sFromTime">起始时间</param>
        /// <param name="nSeconds">之后的秒数</param>
        /// <returns>返回随机时间</returns>
        public static string NewTime(string sFromTime, int nSeconds)
        {
            int nRandSeconds = NewNumber(Math.Abs(nSeconds));
            if (nSeconds < 0)
                nRandSeconds = -1 * nRandSeconds;
            TimeSpan ts = new TimeSpan(0, 0, nRandSeconds);
            DateTime dtFinalTime = DateTimeUtil.ToDateTime(sFromTime) + ts;
            string sRes = DateTimeUtil.ToDateTimeStr(dtFinalTime);
            return sRes;

        }

        /// <summary>
        /// 得到随机的汉字字符
        /// </summary>
        /// <returns>返回随机的汉字字符</returns>
        /// <remarks>
        /// gb2312一级简码的组织方式是：
        /// 第一个字节  176-214  215
        /// 第二个字节  161-254  161-249
        /// </remarks>
        public static char NewChsChar()
        {
            int nRand = NewNumber((214 - 176 + 1) * (254 - 161 + 1) + (249 - 161 + 1));
            byte[] bs = new byte[2];
            bs[0] = (byte)(nRand / (254 - 161 + 1) + 176);
            bs[1] = (byte)(nRand % (254 - 161 + 1) + 161);
            return Encoding.Default.GetString(bs)[0];
        }

        /// <summary>
        /// 生成指定长度的随机汉字串
        /// </summary>
        /// <param name="nLength">随机汉字的长度</param>
        /// <returns>返回指定长度的随机汉字串</returns>
        public static string NewChsString(int nLength)
        {
            StringBuilder sb = new StringBuilder();
            string sRet = "";
            for (int i = 0; i < nLength; i++)
                sb.Append(NewChsChar());
            sRet = sb.ToString();
            return sRet;
        }

        /// <summary>
        /// 生成指定长度区间的随机汉字串
        /// </summary>
        /// <param name="nFromLength">起始长度</param>
        /// <param name="nToLength">截止的长度</param>
        /// <returns>返回指定长度的随机汉字串</returns>
        public static string NewChsString(int nFromLength, int nToLength)
        {
            int nCurrrentLength = NewNumber(nFromLength, nToLength);
            return NewChsString(nCurrrentLength);
        }

        /// <summary>
        /// 将一个字符串随机换掉几个字符
        /// </summary>
        /// <param name="sSrc">原字符串</param>
        /// <param name="nCount">随机换的字符个数</param>
        /// <param name="randType">目标字符的类型</param>
        /// <returns>目标字符串</returns>
        public static string AlterString(string sSrc, int nCount, RandStringType randType)
        {
            if (sSrc == null || sSrc == "")
                return sSrc;

            string sRet = sSrc;

            for (int i = 0; i < nCount; i++)
            {
                int nPos = NewNumber(sRet.Length);
                char cReplaceWith = NewChar(randType);
                sRet = sRet.Substring(0, nPos) + cReplaceWith + sRet.Substring(nPos + 1);
            }

            return sRet;
        }

        /// <summary>
        /// 将一个字符串随机换掉几个字符，字符的数量在指定的区间间
        /// </summary>
        /// <param name="sSrc">原字符串</param>
        /// <param name="nFromCount">起始数量</param>
        /// <param name="nToCount">终止数量</param>
        /// <param name="randType">目标字符的类型</param>
        /// <returns>目标字符串</returns>
        public static string AlterString(string sSrc, int nFromCount, int nToCount,
                RandStringType randType)
        {
            int nCount = NewNumber(nFromCount, nToCount);
            return AlterString(sSrc, nCount, randType);
        }


        /// <summary>
        /// 得到指定规模，指定区间的数字列表
        /// </summary>
        /// <param name="nSize">显示的规模</param>
        /// <param name="nRange">数据的范围</param>
        /// <param name="dupNumberForbidden">是否禁止重复数字</param>
        /// <returns>数字列表</returns>
        public static ArrayList NewNumberList(int nSize, int nRange, bool dupNumberForbidden)
        {
            Debug.Assert(!dupNumberForbidden || (nRange >= nSize));

            //======== 0. 增加的性能补丁，避免列表规模较大时的冲突检测 ============
            if (nSize >= 100 && nRange < nSize * 2)
            {
                ArrayList ret = NewNumberList(nRange);
                for (int i = nSize; i < ret.Count; i++)
                    ret.RemoveAt(i);
                return ret;
            }

            ArrayList listResult = new ArrayList();
            SortedList sortedList = null;
            if (dupNumberForbidden)
                sortedList = new SortedList();
            int nRandNum;

            //========= 1. 循环得到每一个数 ==========
            for (int i = 0; i < nSize; i++)
            {
                //======== 2. 得到一个数 ==========
                if (dupNumberForbidden)
                    nRandNum = NewNumberList__GetANotDupNumber(sortedList, nRange);
                else
                    nRandNum = NewNumber(nRange);

                //========= 3. 增加到列表中 ==========
                listResult.Add(nRandNum);
                if (dupNumberForbidden)
                    sortedList.Add(nRandNum, null);
            }

            return listResult;
        }

        /// <summary>
        /// 得到不重复的一个数字
        /// </summary>
        /// <param name="list">列表</param>
        /// <param name="nRange">范围</param>
        /// <returns>得到的数字</returns>
        private static int NewNumberList__GetANotDupNumber(SortedList list, int nRange)
        {
            //========== 1. 循环，直到得到非重复数字为止 ========
            while (true)
            {
                //======= 2. 生成一个随机数 =========
                int nRand = NewNumber(nRange);

                //========= 3. 判断列表中有无重复 =========
                bool bFindNum = list.ContainsKey(nRand);

                //========== 4. 如果重复，就再生成一个 ========
                if (!bFindNum)
                    return nRand;
            }
        }

        /// <summary>
        /// 得到一个数字列表
        /// </summary>
        /// <param name="nSize">大小</param>
        /// <returns>数字列表</returns>
        /// <remarks>相当于NewNumberList(nSize, nSize, true)</remarks>
        public static ArrayList NewNumberList(int nSize)
        {
            ArrayList listResult = new ArrayList();
            for (int i = 0; i < nSize; i++)
            {
                listResult.Add(i);
            }
            listResult = DisorderList(listResult);

            return listResult;
        }

        /// <summary>
        /// 将一个列表乱序排列
        /// </summary>
        /// <param name="list">列表</param>
        /// <returns>乱序排列后的列表</returns>
        public static ArrayList DisorderList(ArrayList list)
        {
            ArrayList listResult = new ArrayList();

            for (int i = 0; i < list.Count; i++)
                listResult.Add(list[i]);

            for (int i = list.Count - 1; i > 0; i--)
            {
                int nRand = NewNumber(i + 1);
                if (nRand != i)
                {
                    object oTemp = listResult[i];
                    listResult[i] = listResult[nRand];
                    listResult[nRand] = oTemp;
                }
            }

            return listResult;
        }

        /// <summary>
        /// 设置随机种子
        /// </summary>
        /// <param name="nSeed"></param>
        public static void SetRandSeed(int nSeed)
        {
            MiscUtil.Random = new Random(nSeed);
        }
    }
}