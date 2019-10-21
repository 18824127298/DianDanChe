using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Data.FoxDBF
{
    class FOXHEAD
    {
        public const int SIZE_OF_FOXHEAD = 32;

        private int _year = 0;
        /// <summary>
        /// 年
        /// </summary>
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        private int _month = 0;
        /// <summary>
        /// 月
        /// </summary>
        public int Month
        {
            get { return _month; }
            set { _month = value; }
        }

        private int _day = 0;
        /// <summary>
        /// 日
        /// </summary>
        public int Day
        {
            get { return _day; }
            set { _day = value; }
        }

        private int _recNum = 0;
        /// <summary>
        /// 记录数
        /// </summary>
        public int RecNum
        {
            get { return _recNum; }
            set { _recNum = value; }
        }

        private int _recAddr = 0;
        /// <summary>
        /// 记录地址
        /// </summary>
        public int RecAddr
        {
            get { return _recAddr; }
            set { _recAddr = value; }
        }

        private int _recLen = 0;
        /// <summary>
        /// 记录长度
        /// </summary>
        public int RecLen
        {
            get { return _recLen; }
            set { _recLen = value; }
        }

        public byte[] ToBytes()
        {
            FOXBytesBuilder bytesBuilder = new FOXBytesBuilder();

            //======== 1. 标志 =========
            bytesBuilder.AddByteNumber(3);

            //========= 2. 年、月、日 ========
            bytesBuilder.AddByteNumber(this.Year);
            bytesBuilder.AddByteNumber(this.Month);
            bytesBuilder.AddByteNumber(this.Day);

            //======= 3. 记录数 ========
            bytesBuilder.AddLongNumber(this.RecNum);

            //======= 4. 记录地址 ========
            bytesBuilder.AddShortNumber(this.RecAddr);

            //======== 5. 记录长度 =========
            bytesBuilder.AddShortNumber(this.RecLen);

            //======== 6. 保留 ==============
            byte[] bsSP = new byte[20];
            for (int i = 0; i < bsSP.Length; i++)
            {
                bsSP[i] = 0;
            }
            bytesBuilder.AddBytes(bsSP);

            return bytesBuilder.ToBytes();
        }

        public void FromBytes(byte[] bsHeadInfo)
        {
            int nPos = 0;

            //========= 1. 标志 =============
            byte bySign = FOXUtil.RXNByte(bsHeadInfo, ref nPos);
            if (bySign != 3)
                throw new Exception("Header Sign Error, illegal DBF File");

            //========= 2. 年、月、日 ========
            this.Year = FOXUtil.RXNByteNumber(bsHeadInfo, ref nPos);
            this.Month = FOXUtil.RXNByteNumber(bsHeadInfo, ref nPos);
            this.Day = FOXUtil.RXNByteNumber(bsHeadInfo, ref nPos);

            //======== 3. 记录数 ==============
            this.RecNum = FOXUtil.RXNLongNumber(bsHeadInfo, ref nPos);

            //========= 4. 记录地址 =========
            this.RecAddr = FOXUtil.RXNShortNumber(bsHeadInfo, ref nPos);

            //======== 5. 记录长度 ============
            this.RecLen = FOXUtil.RXNShortNumber(bsHeadInfo, ref nPos);
        }
    }
}
