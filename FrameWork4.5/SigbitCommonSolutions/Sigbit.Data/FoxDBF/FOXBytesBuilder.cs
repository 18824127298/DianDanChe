using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

namespace Sigbit.Data.FoxDBF
{
    class FOXBytesBuilder : ArrayList
    {
        public void AddBytes(byte[] bs)
        {
            Add(bs);
        }

        public void AddBytes(byte by, int nLength)
        {
            byte[] bsBytes = new byte[nLength];
            for (int i = 0; i < nLength; i++)
            {
                bsBytes[i] = by;
            }

            Add(bsBytes);
        }

        public void AddByte(byte ch)
        {
            byte[] bsResult = new byte[1];
            bsResult[0] = ch;
            Add(bsResult);
        }

        public void AddChar(char ch)
        {
            byte[] bsResult = new byte[1];
            bsResult[0] = (byte)ch;
            Add(bsResult);
        }

        public void AddLongNumber(int nNumber)
        {
            Add(FOXUtil.FOXLongNumberBytes(nNumber));
        }

        public void AddShortNumber(int nNumber)
        {
            Add(FOXUtil.FOXShortNumberBytes(nNumber));
        }

        public void AddByteNumber(int nNumber)
        {
            Add(FOXUtil.FOXByteNumberBytes(nNumber));
        }

        public void AddString(string sString)
        {
            Add(FOXUtil.FOXStringBytes(sString));
        }

        public void AddString(string sString, int nLength)
        {
            Add(FOXUtil.FOXStringBytes(sString, nLength));
        }

        public void AddString(string sString, int nLength, char cDefaultChar)
        {
            Add(FOXUtil.FOXStringBytes(sString, nLength, cDefaultChar));
        }

        public void AddString(string sString, int nLength, byte byDefaultByte)
        {
            Add(FOXUtil.FOXStringBytes(sString, nLength, byDefaultByte));
        }

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
    }
}
