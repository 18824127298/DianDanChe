using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using System.Diagnostics;

namespace Sigbit.Common
{
    /// <summary>
    /// ������Ĳ�������
    /// </summary>
    public enum RandGenerateMethod
    {
        /// <summary>
        /// ȱʡ��ʽ
        /// </summary>
        Default,    // ȱʡ��ʽ
        /// <summary>
        /// GUID��ʽ
        /// </summary>
        Guid        // GUID��ʽ
    }

    class CharList
    {
        //��д��ĸ
        public static char[] UpperCharList
                = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 
                    'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 
                    'U', 'V', 'W', 'X', 'Y', 'Z' };
        //��д��ĸ������
        public static char[] UpperNumberCharList
                = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 
                    'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 
                    'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', 
                    '4', '5', '6', '7', '8', '9' };
        //Сд��ĸ
        public static char[] LowerCharList
                = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 
                   'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 
                   'u', 'v', 'w', 'x', 'y', 'z' };
        //Сд��ĸ������
        public static char[] LowerNumberCharList
               = {  'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 
                   'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 
                   'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', 
                   '4', '5', '6', '7', '8', '9' };
        //��д��ĸ��Сд��ĸ
        public static char[] UppwerLowerCharList
              = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 
                  'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 
                  'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
                  'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
                  'o', 'p', 'q', 'r', 's', 't','u', 'v', 'w', 'x', 
                  'y', 'z'};
        //��д��Сд��ĸ������
        public static char[] UppwerLowerNumberCharList
              = {  'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 
                   'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 
                   'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
                   'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
                   'o', 'p', 'q', 'r', 's', 't','u', 'v', 'w', 'x', 
                   'y', 'z', '0', '1', '2', '3','4', '5', '6', '7', 
                   '8', '9'};
        //����
        public static char[] NumberCharList
               = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    }

    /// <summary>
    /// ö�����ͣ������ַ����ķ�ʽ
    /// </summary>
    public enum RandStringType
    {
        /// <summary>
        /// ���ɴ�д��Ӣ���ַ���
        /// </summary>
        Upper,
        /// <summary>
        /// ����Сд��Ӣ���ַ���
        /// </summary>
        Lower,
        /// <summary>
        /// ���ɴ�Сд��ϵ�Ӣ���ַ���
        /// </summary>
        UpperLower,
        /// <summary>
        /// ���ɴ�д�����ֻ�ϵ��ַ���
        /// </summary>
        UpperNumber,
        /// <summary>
        /// ����Сд�����ֻ�ϵ��ַ���
        /// </summary>
        LowerNumber,
        /// <summary>
        /// ���ɴ�Сд�����ֻ�ϵ��ַ���
        /// </summary>
        UpperLowerNumber,
        /// <summary>
        /// ���������ַ���
        /// </summary>
        Number,
        /// <summary>
        /// �����ַ���
        /// </summary>
        Chs,
        /// <summary>
        /// ���ֺ�Ӣ�����ֻ�ϵ��ַ���
        /// </summary>
        ChsEng
    }

    /// <summary>
    /// ����������֡��ַ������б��Ӧ����
    /// </summary>
    public class RandUtil
    {
        private static RandGenerateMethod _randGenerateMethod = RandGenerateMethod.Default;
        /// <summary>
        /// �����������ʽ
        /// </summary>
        public static RandGenerateMethod RandGenerateMethod
        {
            get { return RandUtil._randGenerateMethod; }
            set { RandUtil._randGenerateMethod = value; }
        }

        /// <summary>
        /// ����0~1֮�ڵ������
        /// </summary>
        /// <returns>�����������</returns>
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
        /// ����0��MaxValue-1���������
        /// </summary>
        /// <param name="MaxValue">ָ�������ֵ</param>
        /// <returns>���صõ�һ��С����ָ�����ֵ�ķǸ������</returns>
        public static int NewNumber(int MaxValue)
        {
            int nRand = (int)(NextDouble() * MaxValue);
            return nRand;

            //int nRand = MiscUtil.Random.Next(MaxValue);
            //return nRand;
        }

        /// <summary>
        /// ���ɴ�nFromValue��nToValue��������֣�����nFromValue��nToValue��
        /// </summary>
        /// <param name="nFromValue">��������½���Сֵ</param>
        /// <param name="nToValue">��������Ͻ����ֵ</param>
        /// <returns>���صõ�һ��С����ָ�����ֵ�ķǸ������</returns>
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
        /// �������������
        /// </summary>
        /// <param name="fMaxValue">���ֵ</param>
        /// <returns>��0�����ֵ��һ��������</returns>
        public static double NewFloat(double fMaxValue)
        {
            return NextDouble() * fMaxValue;
            //return MiscUtil.Random.NextDouble() * fMaxValue;
        }

        /// <summary>
        /// �������������
        /// </summary>
        /// <param name="fFromValue">��Сֵ</param>
        /// <param name="fToValue">���ֵ</param>
        /// <returns>������Сֵ�����ֵ֮������������</returns>
        public static double NewFloat(double fFromValue, double fToValue)
        {
            //========== 1. �ҵ���Сֵ�����ֵ =============
            double fMinValue = fFromValue;
            double fMaxValue = fToValue;

            if (fToValue < fFromValue)
            {
                fMinValue = fToValue;
                fMaxValue = fFromValue;
            }

            //========== 2. ����֮���ֵ =============
            double fOffset = fMaxValue - fMinValue;
            //return fMinValue + fOffset * MiscUtil.Random.NextDouble();
            return fMinValue + fOffset * NextDouble();
        }

        /// <summary>
        /// ��ָ����������������ַ�
        /// </summary>
        /// <param name="randType">�ַ���������</param>
        /// <returns>����һ��ָ�����͵�����ַ���</returns>
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
        /// ����ָ�����ȡ�ָ�����͵��ַ���
        /// </summary>
        /// <param name="nLength">��������ַ����ĳ���</param>
        /// <param name="randType">��������ַ���������</param>
        /// <returns>����һ��ָ�����ȵġ�ָ�����͵�����ַ���</returns>
        public static string NewString(int nLength, RandStringType randType)
        {
            string sRet = "";
            for (int i = 0; i < nLength; i++)
                sRet += NewChar(randType);
            return sRet;
        }

        /// <summary>
        /// ����ָ���������䡢ָ�����͵��ַ���
        /// </summary>
        /// <param name="nFromLength">�Ӹó��ȿ�ʼ</param>
        /// <param name="nToLength">���ó��Ƚ���</param>
        /// <param name="randType">��������ַ���������</param>
        /// <returns>����һ��ָ���������䡢ָ�����͵��ַ���</returns>
        public static string NewString(int nFromLength, int nToLength, RandStringType randType)
        {
            int nCurrrentLength = NewNumber(nFromLength, nToLength);
            return NewString(nCurrrentLength, randType);
        }

        /// <summary>
        /// �õ����췶Χ�ڵ����ʱ��
        /// </summary>
        /// <returns>���ص�������ʱ��</returns>
        public static string NewTime()
        {
            DateTime dtToday = DateTime.Today; //��õ�ǰ���ڵ�00:00:00
            int nRandSeconds = NewNumber(24 * 3600);
            TimeSpan ts = new TimeSpan(0, 0, nRandSeconds);
            DateTime dtFinalTime = dtToday + ts;
            string sRet = DateTimeUtil.ToDateTimeStr(dtFinalTime);
            return sRet;
        }

        /// <summary>
        /// �õ�����ʼʱ�䵽��ֹʱ������ʱ��
        /// </summary>
        /// <param name="sFromTime">��ʼʱ��</param>
        /// <param name="sToTime">��ֹʱ��</param>
        /// <returns>�������ʱ��</returns>
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
        /// �õ���ָ��ʱ�俪ʼ��֮��0~nSeconds-1������ʱ�䣨nSeconds����Ϊ������
        /// </summary>
        /// <param name="sFromTime">��ʼʱ��</param>
        /// <param name="nSeconds">֮�������</param>
        /// <returns>�������ʱ��</returns>
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
        /// �õ�����ĺ����ַ�
        /// </summary>
        /// <returns>��������ĺ����ַ�</returns>
        /// <remarks>
        /// gb2312һ���������֯��ʽ�ǣ�
        /// ��һ���ֽ�  176-214  215
        /// �ڶ����ֽ�  161-254  161-249
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
        /// ����ָ�����ȵ�������ִ�
        /// </summary>
        /// <param name="nLength">������ֵĳ���</param>
        /// <returns>����ָ�����ȵ�������ִ�</returns>
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
        /// ����ָ�����������������ִ�
        /// </summary>
        /// <param name="nFromLength">��ʼ����</param>
        /// <param name="nToLength">��ֹ�ĳ���</param>
        /// <returns>����ָ�����ȵ�������ִ�</returns>
        public static string NewChsString(int nFromLength, int nToLength)
        {
            int nCurrrentLength = NewNumber(nFromLength, nToLength);
            return NewChsString(nCurrrentLength);
        }

        /// <summary>
        /// ��һ���ַ���������������ַ�
        /// </summary>
        /// <param name="sSrc">ԭ�ַ���</param>
        /// <param name="nCount">��������ַ�����</param>
        /// <param name="randType">Ŀ���ַ�������</param>
        /// <returns>Ŀ���ַ���</returns>
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
        /// ��һ���ַ���������������ַ����ַ���������ָ���������
        /// </summary>
        /// <param name="sSrc">ԭ�ַ���</param>
        /// <param name="nFromCount">��ʼ����</param>
        /// <param name="nToCount">��ֹ����</param>
        /// <param name="randType">Ŀ���ַ�������</param>
        /// <returns>Ŀ���ַ���</returns>
        public static string AlterString(string sSrc, int nFromCount, int nToCount,
                RandStringType randType)
        {
            int nCount = NewNumber(nFromCount, nToCount);
            return AlterString(sSrc, nCount, randType);
        }


        /// <summary>
        /// �õ�ָ����ģ��ָ������������б�
        /// </summary>
        /// <param name="nSize">��ʾ�Ĺ�ģ</param>
        /// <param name="nRange">���ݵķ�Χ</param>
        /// <param name="dupNumberForbidden">�Ƿ��ֹ�ظ�����</param>
        /// <returns>�����б�</returns>
        public static ArrayList NewNumberList(int nSize, int nRange, bool dupNumberForbidden)
        {
            Debug.Assert(!dupNumberForbidden || (nRange >= nSize));

            //======== 0. ���ӵ����ܲ����������б��ģ�ϴ�ʱ�ĳ�ͻ��� ============
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

            //========= 1. ѭ���õ�ÿһ���� ==========
            for (int i = 0; i < nSize; i++)
            {
                //======== 2. �õ�һ���� ==========
                if (dupNumberForbidden)
                    nRandNum = NewNumberList__GetANotDupNumber(sortedList, nRange);
                else
                    nRandNum = NewNumber(nRange);

                //========= 3. ���ӵ��б��� ==========
                listResult.Add(nRandNum);
                if (dupNumberForbidden)
                    sortedList.Add(nRandNum, null);
            }

            return listResult;
        }

        /// <summary>
        /// �õ����ظ���һ������
        /// </summary>
        /// <param name="list">�б�</param>
        /// <param name="nRange">��Χ</param>
        /// <returns>�õ�������</returns>
        private static int NewNumberList__GetANotDupNumber(SortedList list, int nRange)
        {
            //========== 1. ѭ����ֱ���õ����ظ�����Ϊֹ ========
            while (true)
            {
                //======= 2. ����һ������� =========
                int nRand = NewNumber(nRange);

                //========= 3. �ж��б��������ظ� =========
                bool bFindNum = list.ContainsKey(nRand);

                //========== 4. ����ظ�����������һ�� ========
                if (!bFindNum)
                    return nRand;
            }
        }

        /// <summary>
        /// �õ�һ�������б�
        /// </summary>
        /// <param name="nSize">��С</param>
        /// <returns>�����б�</returns>
        /// <remarks>�൱��NewNumberList(nSize, nSize, true)</remarks>
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
        /// ��һ���б���������
        /// </summary>
        /// <param name="list">�б�</param>
        /// <returns>�������к���б�</returns>
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
        /// �����������
        /// </summary>
        /// <param name="nSeed"></param>
        public static void SetRandSeed(int nSeed)
        {
            MiscUtil.Random = new Random(nSeed);
        }
    }
}