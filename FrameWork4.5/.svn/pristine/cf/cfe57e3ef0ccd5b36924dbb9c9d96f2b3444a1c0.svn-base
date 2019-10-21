using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Collections;

namespace Sigbit.Net.BIPPacket
{
    /// <summary>
    /// BIP工具类
    /// </summary>
    public class BIPUtil
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
                throw new BIPFormatException("RXNByLength() : Unexpected end of string - pos:"
                        + nPos.ToString() + ", len:" + nLength.ToString());
            for (int i = 0; i < nLength; i++)
                bsResult[i] = bsSrc[nPos + i];

            nPos += nLength;
            return bsResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bsSrc"></param>
        /// <param name="nPos"></param>
        /// <param name="nLength"></param>
        /// <returns></returns>
        static public string RXNStringByLength(byte[] bsSrc, ref int nPos, int nLength)
        {
            byte[] bsResult = RXNByLength(bsSrc, ref nPos, nLength);
            string sResult = Encoding.Default.GetString(bsResult);
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

            for (int i = 0; i < nLength; i++)
            {
                byte ch = bsNum[i];
                nResult += ch;
                if (i != nLength - 1)
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
        /// 从字节数组中取出字符串
        /// </summary>
        /// <param name="bsSrc">字节数组</param>
        /// <param name="nPos">当前位置</param>
        /// <returns>取出的字符串</returns>
        public static string RXNString(byte[] bsSrc, ref int nPos)
        {
            string sResultString;
            int nStringLength;

            //========== 1. 取得第一个字符 =========
            byte byteLength = RXNByte(bsSrc, ref nPos);

            //======== 2. 如果第一个字符为'\xFF'，则取后面的四个字符 =======
            if (byteLength == 0xFF)
            {
                //======== 3. 如果后面的四个字符是四个F7，则为空串 =======
                byte[] bsNext = BIPUtil.RXNByLength(bsSrc, ref nPos, 4);
                if (bsNext[0] == 0xF7 && bsNext[1] == 0xF7
                        && bsNext[2] == 0xF7 && bsNext[3] == 0xF7)
                {
                    return "";
                }
                else
                {
                    //========= 4. 取出后面字符代表的字符串长度 ========
                    nPos -= 4;
                    nStringLength = RXNLongNumber(bsSrc, ref nPos);
                }
            }
            else
            {
                //========= 5. 如果第一个字符不是'\xFF'，则表示字符串长度 ====
                nStringLength = (int)byteLength;
            }

            //========== 6. 取出字符串并返回 =========
            sResultString = RXNStringByLength(bsSrc, ref nPos, nStringLength);
            return sResultString;
        }

        /// <summary>
        ///  将一个整数转成四字节的BIP数字表示
        /// </summary>
        /// <param name="nNumber">整数</param>
        /// <returns>BIP数字表示</returns>
        public static byte[] BIPLongNumberBytes(int nNumber)
        {
            byte[] bsNumber = new byte[4];
            for (int i = 3; i >= 0; i--)
            {
                byte ch = (byte)(nNumber % 256);
                bsNumber[i] = ch;
                nNumber >>= 8;
            }
            return bsNumber;
        }

        /// <summary>
        ///  将一个整数转成二字节的BIP数字表示
        /// </summary>
        /// <param name="nNumber">整数</param>
        /// <returns>BIP数字表示</returns>
        public static byte[] BIPShortNumberBytes(int nNumber)
        {
            byte[] bsNumber = new byte[2];
            for (int i = 1; i >= 0; i--)
            {
                byte ch = (byte)(nNumber % 256);
                bsNumber[i] = ch;
                nNumber >>= 8;
            }
            return bsNumber;
        }

        /// <summary>
        ///  将一个整数转成单字节的BIP数字表示
        /// </summary>
        /// <param name="nNumber">整数</param>
        /// <returns>BIP数字表示</returns>
        public static byte[] BIPByteNumberBytes(int nNumber)
        {
            Debug.Assert(nNumber < 255);
            byte ch = (byte)nNumber;
            byte[] bsNumber = new byte[1];
            bsNumber[0] = ch;

            return bsNumber;
        }

        /// <summary>
        /// 将一个字符串转为BIP字符串表示
        /// </summary>
        /// <param name="sString">字符串</param>
        /// <returns>BIP字符串表示</returns>
        public static byte[] BIPStringBytes(string sString)
        {
            byte[] bsString = Encoding.Default.GetBytes(sString);
            byte[] bsResult;
            int nLength = bsString.Length;
            if (nLength == 0)       // 空串
                bsResult = new byte[5] { 0xFF, 0xF7, 0xF7, 0xF7, 0xF7 };
            else if (nLength < 255)
            {
                bsResult = new byte[nLength + 1];
                bsResult[0] = (byte)nLength;
                bsString.CopyTo(bsResult, 1);
            }
            else
            {
                bsResult = new byte[nLength + 5];
                bsResult[0] = 0xFF;
                BIPLongNumberBytes(nLength).CopyTo(bsResult, 1);
                bsString.CopyTo(bsResult, 5);
            }

            return bsResult;
        }
    }

    /// <summary>
    /// BIP字节生成类
    /// </summary>
    public class BIPBytesBuilder : ArrayList
    {
        /// <summary>
        /// 添加字节
        /// </summary>
        /// <param name="ch"></param>
        public void AddByte(byte ch)
        {
            byte[] bsResult = new byte[1];
            bsResult[0] = ch;
            Add(bsResult);
        }

        /// <summary>
        /// 添加长整形
        /// </summary>
        /// <param name="nNumber"></param>
        public void AddLongNumber(int nNumber)
        {
            Add(BIPUtil.BIPLongNumberBytes(nNumber));
        }

        /// <summary>
        /// 添加短整形
        /// </summary>
        /// <param name="nNumber"></param>
        public void AddShortNumber(int nNumber)
        {
            Add(BIPUtil.BIPShortNumberBytes(nNumber));
        }
        /// <summary>
        /// 添加字节
        /// </summary>
        /// <param name="nNumber"></param>
        public void AddByteNumber(int nNumber)
        {
            Add(BIPUtil.BIPByteNumberBytes(nNumber));
        }

        /// <summary>
        /// 添加字符串
        /// </summary>
        /// <param name="sString"></param>
        public void AddPureString(string sString)
        {
            byte[] bsResult = Encoding.Default.GetBytes(sString);
            Add(bsResult);
        }

        /// <summary>
        /// 添加字符串
        /// </summary>
        /// <param name="sString"></param>
        public void AddString(string sString)
        {
            Add(BIPUtil.BIPStringBytes(sString));
        }

        /// <summary>
        /// 到字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            //======== 1. 得到返回的字节数组总长度 =====
            int nTotalLength = 0;
            for (int i = 0; i < Count; i++)
            {
                byte[] item = (byte[])this[i];
                nTotalLength += item.Length;
            }

            int nPos = 0;
            byte[] bsResult = new byte[nTotalLength];

            //======== 2. 得到结果字节数组 =======
            for (int i = 0; i < Count; i++)
            {
                byte[] item = (byte[])this[i];
                item.CopyTo(bsResult, nPos);
                nPos += item.Length;
            }

            return bsResult;
        }

        /// <summary>
        /// 获取输出字节数据
        /// </summary>
        /// <returns></returns>
        public byte[] FetchOutPacketBytes()
        {
            //====== 1. 如果没有内容，则返回null ========
            if (this.Count == 0)
                return null;

            byte[] bsFirstRecord = (byte[])this[0];
            if (bsFirstRecord.Length <= 5)
                throw new BIPFormatException("BIPBytesBuilder::FetchOutPacketBytes() : "
                        + "the packet is too small.");

            //========== 2. 包头标志，固定为"F7" =========
            int nPos = 0;
            byte cPacketHeaderId;
            cPacketHeaderId = BIPUtil.RXNByte(bsFirstRecord, ref nPos);
            if (cPacketHeaderId != 0xF7)
                throw new BIPFormatException("BIPBytesBuilder::FetchOutPacketBytes() : "
                        + "Invalid packet header encountered.");

            //============ 3. 包长度，包的总长度=包长度+6 ========
            int nPacketLength = BIPUtil.RXNLongNumber(bsFirstRecord, ref nPos);
            int nPacketTotalLength = nPacketLength + 6;

            if (nPacketTotalLength > 102400)
                throw new BIPFormatException("BIPBytesBuilder::FetchOutPacketBytes() : "
                        + "the packet length is too long.");

            //========== 4. 如果总长度不会大于希望长度，则返回null(继续等待新包) =======
            byte[] bsTotalBytes = this.ToBytes();
            if (bsTotalBytes.Length < nPacketTotalLength)
                return null;

            //======== 5. 构造返回包 ==========
            byte[] bsResult = new byte[nPacketTotalLength];

            if (bsTotalBytes.Length == nPacketTotalLength)
            {
                bsTotalBytes.CopyTo(bsResult, 0);
                this.Clear();
            }
            else
            {
                Array.Copy(bsTotalBytes, bsResult, nPacketTotalLength);
                byte[] bsLeftBytes = new byte[bsTotalBytes.Length - nPacketTotalLength];
                Array.Copy(bsTotalBytes, nPacketTotalLength, bsLeftBytes, 0,
                    bsTotalBytes.Length - nPacketTotalLength);
                this.Clear();
                Add(bsLeftBytes);
            }

            if (bsResult[bsResult.Length - 1] != 0xFD)
            {
                throw new BIPFormatException("BIPBytesBuilder::FetchOutPacketBytes() : "
                        + "Invalid packet tailer encountered.");
            }
            return bsResult;
        }
    }
}
