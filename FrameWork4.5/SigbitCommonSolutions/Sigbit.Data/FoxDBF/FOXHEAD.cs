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
        /// ��
        /// </summary>
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        private int _month = 0;
        /// <summary>
        /// ��
        /// </summary>
        public int Month
        {
            get { return _month; }
            set { _month = value; }
        }

        private int _day = 0;
        /// <summary>
        /// ��
        /// </summary>
        public int Day
        {
            get { return _day; }
            set { _day = value; }
        }

        private int _recNum = 0;
        /// <summary>
        /// ��¼��
        /// </summary>
        public int RecNum
        {
            get { return _recNum; }
            set { _recNum = value; }
        }

        private int _recAddr = 0;
        /// <summary>
        /// ��¼��ַ
        /// </summary>
        public int RecAddr
        {
            get { return _recAddr; }
            set { _recAddr = value; }
        }

        private int _recLen = 0;
        /// <summary>
        /// ��¼����
        /// </summary>
        public int RecLen
        {
            get { return _recLen; }
            set { _recLen = value; }
        }

        public byte[] ToBytes()
        {
            FOXBytesBuilder bytesBuilder = new FOXBytesBuilder();

            //======== 1. ��־ =========
            bytesBuilder.AddByteNumber(3);

            //========= 2. �ꡢ�¡��� ========
            bytesBuilder.AddByteNumber(this.Year);
            bytesBuilder.AddByteNumber(this.Month);
            bytesBuilder.AddByteNumber(this.Day);

            //======= 3. ��¼�� ========
            bytesBuilder.AddLongNumber(this.RecNum);

            //======= 4. ��¼��ַ ========
            bytesBuilder.AddShortNumber(this.RecAddr);

            //======== 5. ��¼���� =========
            bytesBuilder.AddShortNumber(this.RecLen);

            //======== 6. ���� ==============
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

            //========= 1. ��־ =============
            byte bySign = FOXUtil.RXNByte(bsHeadInfo, ref nPos);
            if (bySign != 3)
                throw new Exception("Header Sign Error, illegal DBF File");

            //========= 2. �ꡢ�¡��� ========
            this.Year = FOXUtil.RXNByteNumber(bsHeadInfo, ref nPos);
            this.Month = FOXUtil.RXNByteNumber(bsHeadInfo, ref nPos);
            this.Day = FOXUtil.RXNByteNumber(bsHeadInfo, ref nPos);

            //======== 3. ��¼�� ==============
            this.RecNum = FOXUtil.RXNLongNumber(bsHeadInfo, ref nPos);

            //========= 4. ��¼��ַ =========
            this.RecAddr = FOXUtil.RXNShortNumber(bsHeadInfo, ref nPos);

            //======== 5. ��¼���� ============
            this.RecLen = FOXUtil.RXNShortNumber(bsHeadInfo, ref nPos);
        }
    }
}
