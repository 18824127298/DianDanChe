using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

namespace Sigbit.Common
{
    /// <summary>
    /// 数组相关的实用例程
    /// </summary>
    public class ArrayUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrList"></param>
        /// <returns></returns>
        public static string[] ToStringArray(ArrayList arrList)
        {
            string[] arrRet = new string[arrList.Count];
            for (int i = 0; i < arrList.Count; i++)
                arrRet[i] = (string)arrList[i];
            return arrRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrString"></param>
        /// <returns></returns>
        public static ArrayList ToArrayList(string[] arrString)
        {
            ArrayList arrRet = new ArrayList();
            for (int i = 0; i < arrString.Length; i++)
                arrRet.Add(arrString[i]);

            return arrRet;
        }

        /// <summary>
        /// 将字符串数组编码至一个字符串中
        /// </summary>
        /// <param name="arrString">字符串数组，需要事先保证字符串中不包含分隔符</param>
        /// <param name="cSplitChar">分隔符</param>
        /// <returns>编码结果</returns>
        public static string MergeStringArrayToString(string[] arrString, char cSplitChar)
        {
            StringBuilder sbRet = new StringBuilder();
            for (int i = 0; i < arrString.Length; i++)
            {
                if (i != 0)
                    sbRet.Append(cSplitChar);
                string sOneString = arrString[i];
                sbRet.Append(sOneString);
            }
            return sbRet.ToString();
        }

        /// <summary>
        /// 将字符串列表编码至一个字符串中
        /// </summary>
        /// <param name="arrList">字符串列表，需要事先保证字符串中不包含分隔符</param>
        /// <param name="cSplitChar">分隔符</param>
        /// <returns>编码结果</returns>
        public static string MergeStringArrayToString(ArrayList arrList, char cSplitChar)
        {
            StringBuilder sbRet = new StringBuilder();
            for (int i = 0; i < arrList.Count; i++)
            {
                if (i != 0)
                    sbRet.Append(cSplitChar);
                string sOneString = (string)arrList[i];
                sbRet.Append(sOneString);
            }
            return sbRet.ToString();
        }

        public static ArrayList SplitStringToArrayList(string sString, char cSplitChar)
        {
            ArrayList arrRet = new ArrayList();
            string[] arrItems = sString.Split(cSplitChar);
            for (int i = 0; i < arrItems.Length; i++)
            {
                string sOneItem = arrItems[i];
                if (sOneItem != "")
                    arrRet.Add(sOneItem);
            }
            return arrRet;
        }

        public static string[] SplitStringToStringArray(string sString, char cSplitChar)
        {
            ArrayList lst = SplitStringToArrayList(sString, cSplitChar);
            string[] arrRet = ArrayUtil.ToStringArray(lst);
            return arrRet;
        }

        /// <summary>
        /// 得到一个整数数组，其中数组的值就是该数组的下标
        /// </summary>
        /// <param name="nNum">数组的大小</param>
        /// <returns>获得的数组</returns>
        public static int[] GetOrderValueIntArray(int nNum)
        {
            int[] arrValue = new int[nNum];
            for (int i = 0; i < nNum; i++)
            {
                arrValue[i] = i;
            }
            return arrValue;
        }

        /// <summary>
        /// 打乱整数数组的值的顺序，得到一个新的乱序了的数组
        /// </summary>
        /// <param name="arrValues">待乱序的数组</param>
        /// <param name="arrOldPosition">乱序后的原来数据的位置</param>
        /// <returns>新的乱序了的数组</returns>
        public static int[] GetMixedIntArrayOrder(int[] arrValues, ref int[] arrOldPosition)
        {
            arrOldPosition = new int[arrValues.Length];
            string strGettedIndex = ",";
            int nGettedCount = 0;
            int[] arrNewValues = new int[arrValues.Length];
            while (nGettedCount < arrValues.Length)
            {
                int thisIndex = MiscUtil.Random.Next(0, arrValues.Length);
                if (strGettedIndex.IndexOf("," + thisIndex.ToString() + ",") == -1)
                {
                    arrNewValues[nGettedCount] = arrValues[thisIndex];
                    arrOldPosition[nGettedCount] = thisIndex;
                    strGettedIndex = strGettedIndex + thisIndex.ToString() + ",";
                    nGettedCount = nGettedCount + 1;
                }
            }
            return arrNewValues;
        }

        /// <summary>
        /// 打乱整数数组的值的顺序，得到一个新的乱序了的数组
        /// </summary>
        /// <param name="arrValues">待乱序的数组</param>
        /// <returns>新的乱序了的数组</returns>
        public static int[] GetMixedIntArrayOrder(int[] arrValues)
        {
            int[] arrOldPosition = new int[arrValues.Length];
            return GetMixedIntArrayOrder(arrValues, ref arrOldPosition);
        }

        /// <summary>
        /// 打乱字符串数组的值的顺序，得到一个新的乱序了的数组
        /// </summary>
        /// <param name="arrValues">待乱序的数组</param>
        /// <param name="arrOldPosition">乱序后的原来数据的位置</param>
        /// <returns>新的乱序了的数组</returns>
        public static string[] GetMixedStringArrayOrder(string[] arrValues, ref int[] arrOldPosition)
        {
            arrOldPosition = new int[arrValues.Length];
            string strGettedIndex = ",";
            int nGettedCount = 0;
            string[] arrNewValues = new string[arrValues.Length];
            while (nGettedCount < arrValues.Length)
            {
                int thisIndex = MiscUtil.Random.Next(0, arrValues.Length);
                if (strGettedIndex.IndexOf("," + thisIndex.ToString() + ",") == -1)
                {
                    arrNewValues[nGettedCount] = arrValues[thisIndex];
                    arrOldPosition[nGettedCount] = thisIndex;
                    strGettedIndex = strGettedIndex + thisIndex.ToString() + ",";
                    nGettedCount = nGettedCount + 1;
                }
            }
            return arrNewValues;
        }

        /// <summary>
        /// 打乱字符串数组的值的顺序，得到一个新的乱序了的数组
        /// </summary>
        /// <param name="arrValues">待乱序的数组</param>
        /// <returns>新的乱序了的数组</returns>
        public static string[] GetMixedStringArrayOrder(string[] arrValues)
        {
            int[] arrOldPosition = new int[arrValues.Length];
            return GetMixedStringArrayOrder(arrValues, ref arrOldPosition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="by"></param>
        /// <param name="nCount"></param>
        /// <returns></returns>
        public static byte[] RepeatByte(byte by, int nCount)
        {
            byte[] bsRet = new byte[nCount];
            for (int i = 0; i < nCount; i++)
                bsRet[i] = by;

            return bsRet;
        }


    }
}
