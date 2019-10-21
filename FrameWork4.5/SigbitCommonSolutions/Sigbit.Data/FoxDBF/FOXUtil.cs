using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

namespace Sigbit.Data.FoxDBF
{
    class FOXUtil
    {
        /// <summary>
        /// 从源字节数组中取出指定长度的字节数组
        /// </summary>
        /// <param name="bsSrc">源字节数组</param>
        /// <param name="nPos">当前位置</param>
        /// <param name="nLength">取出的长度</param>
        /// <returns>取出的字节数组</returns>
        static public byte[] RXNByLength(byte[] bsSrc, ref int nPos, int nLength)
        {
            byte[] bsResult = new byte[nLength];
            if (bsSrc.Length < nPos + nLength)
                throw new Exception("RXNByLength() : Unexpected end of string - pos:"
                        + nPos.ToString() + ", len:" + nLength.ToString());
            for (int i = 0; i < nLength; i++)
                bsResult[i] = bsSrc[nPos + i];

            nPos += nLength;
            return bsResult;
        }

        static public string RXNStringByLength(byte[] bsSrc, ref int nPos, int nLength)
        {
            byte[] bsResult = RXNByLength(bsSrc, ref nPos, nLength);
            string sResult = FoxDBFConfig.Instance.CurrentEncoding.GetString(bsResult);
            return sResult;
        }

        static public string RXNStringByLength(byte[] bsSrc, ref int nPos, int nLength, char cDefault)
        {
            byte byDefault = (byte)cDefault;
            return RXNStringByLength(bsSrc, ref nPos, nLength, byDefault);
        }

        static public string RXNStringByLength(byte[] bsSrc, ref int nPos, int nLength, byte byDefault)
        {
            //========== 1. 取出指定长度的字节数组 =========
            byte[] bsResult = RXNByLength(bsSrc, ref nPos, nLength);

            //=========== 2. 找到(后面填充的)缺省字节的位置 ========
            int nDefaultBytePos = -1;
            for (int i = 0; i < bsResult.Length; i++)
            {
                byte byThis = bsResult[i];
                if (byThis == byDefault)
                {
                    nDefaultBytePos = i;
                    break;
                }
            }

            //=========== 3. 得到字符串的字节数组表示 =========
            byte[] bsString;
            if (nDefaultBytePos == -1)
                bsString = bsResult;
            else
            {
                bsString = new byte[nDefaultBytePos];
                Array.Copy(bsResult, bsString, bsString.Length);
            }

            string sResult = FoxDBFConfig.Instance.CurrentEncoding.GetString(bsString);
            return sResult;
        }

        /// <summary>
        /// 取出一个字节
        /// </summary>
        /// <param name="bsSrc">源字节数组</param>
        /// <param name="nPos">当前位置</param>
        /// <returns>取出的字节</returns>
        static public byte RXNByte(byte[] bsSrc, ref int nPos)
        {
            return RXNByLength(bsSrc, ref nPos, 1)[0];
        }

        static public char RXNChar(byte[] bsSrc, ref int nPos)
        {
            byte by = RXNByte(bsSrc, ref nPos);
            char ch = (char)by;
            return ch;
        }

        /// <summary>
        /// 取出一定长度的整型数值
        /// </summary>
        /// <param name="bsSrc">源字节数组</param>
        /// <param name="nPos">当前位置</param>
        /// <param name="nLength">取出长度</param>
        /// <returns>整型数值</returns>
        private static int RXNNumber__Do(byte[] bsSrc, ref int nPos, int nLength)
        {
            byte[] bsNum = RXNByLength(bsSrc, ref nPos, nLength);
            int nResult = 0;

            for (int i = nLength - 1; i >= 0; i--)
            {
                byte ch = bsNum[i];
                nResult += ch;
                if (i != 0)
                    nResult <<= 8;
            }

            return nResult;
        }

        /// <summary>
        /// 取出四字节的长整型
        /// </summary>
        /// <param name="bsSrc">源字节数组</param>
        /// <param name="nPos">当前位置</param>
        /// <returns>取出的长整型</returns>
        public static int RXNLongNumber(byte[] bsSrc, ref int nPos)
        {
            return RXNNumber__Do(bsSrc, ref nPos, 4);
        }

        /// <summary>
        /// 取出二字节的短整型
        /// </summary>
        /// <param name="bsSrc">源字节数组</param>
        /// <param name="nPos">当前位置</param>
        /// <returns>取出的短整型</returns>
        public static int RXNShortNumber(byte[] bsSrc, ref int nPos)
        {
            return RXNNumber__Do(bsSrc, ref nPos, 2);
        }

        /// <summary>
        /// 取出单字节的整型
        /// </summary>
        /// <param name="bsSrc">源字节数组</param>
        /// <param name="nPos">当前位置</param>
        /// <returns>取出的整型</returns>
        public static int RXNByteNumber(byte[] bsSrc, ref int nPos)
        {
            return RXNNumber__Do(bsSrc, ref nPos, 1);
        }

        /// <summary>
        ///  将一个整数转成四字节的FOX数字表示
        /// </summary>
        /// <param name="nNumber">整数</param>
        /// <returns>FOX数字表示</returns>
        public static byte[] FOXLongNumberBytes(int nNumber)
        {
            byte[] bsNumber = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                byte ch = (byte)(nNumber % 256);
                bsNumber[i] = ch;
                nNumber >>= 8;
            }
            return bsNumber;
        }

        /// <summary>
        ///  将一个整数转成二字节的FOX数字表示
        /// </summary>
        /// <param name="nNumber">整数</param>
        /// <returns>FOX数字表示</returns>
        public static byte[] FOXShortNumberBytes(int nNumber)
        {
            byte[] bsNumber = new byte[2];
            for (int i = 0; i < 2; i++)
            {
                byte ch = (byte)(nNumber % 256);
                bsNumber[i] = ch;
                nNumber >>= 8;
            }
            return bsNumber;
        }

        /// <summary>
        ///  将一个整数转成单字节的FOX数字表示
        /// </summary>
        /// <param name="nNumber">整数</param>
        /// <returns>FOX数字表示</returns>
        public static byte[] FOXByteNumberBytes(int nNumber)
        {
            Debug.Assert(nNumber < 255);
            byte ch = (byte)nNumber;
            byte[] bsNumber = new byte[1];
            bsNumber[0] = ch;

            return bsNumber;
        }

        /// <summary>
        /// 将一个字符串转为FOX字符串表示
        /// </summary>
        /// <param name="sString">字符串</param>
        /// <returns>FOX字符串表示</returns>
        public static byte[] FOXStringBytes(string sString)
        {
            byte[] bsString = FoxDBFConfig.Instance.CurrentEncoding.GetBytes(sString);
            return bsString;
        }

        public static byte[] FOXStringBytes(string sString, int nLength)
        {
            return FOXStringBytes(sString, nLength, ' ');
        }

        public static byte[] FOXStringBytes(string sString, int nLength, char cDefaultChar)
        {
            return FOXStringBytes(sString, nLength, (byte)cDefaultChar);
        }

        public static byte[] FOXStringBytes(string sString, int nLength, byte byDefaultChar)
        {
            byte[] bsRet = new byte[nLength];
            byte[] bsString = FoxDBFConfig.Instance.CurrentEncoding.GetBytes(sString);

            int nCopyLength = nLength;
            if (bsString.Length < nLength)
                nCopyLength = bsString.Length;

            Array.Copy(bsString, bsRet, nCopyLength);

            for (int i = nCopyLength; i < nLength; i++)
            {
                bsRet[i] = byDefaultChar;
            }

            return bsRet;
        }
    }
}
