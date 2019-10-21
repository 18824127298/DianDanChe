using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Data.FoxDBF;

namespace Sigbit.Data.FoxUtils.Sorting
{
    public class FoxDBFKeyLocatorOnSortingDBF
    {
        private FoxDBFile _inputFoxDBF = null;
        /// <summary>
        /// DBF类
        /// </summary>
        public FoxDBFile InputFoxDBF
        {
            get { return _inputFoxDBF; }
            set { _inputFoxDBF = value; }
        }

        private string[] _inputSearchKeys = new string[0];
        /// <summary>
        /// 多个关键字
        /// </summary>
        public string[] InputSearchKeys
        {
            get { return _inputSearchKeys; }
            set { _inputSearchKeys = value; }
        }

        public string InputSearchSingleKey
        {
            get
            {
                if (this.InputSearchKeys.Length == 1)
                    return this.InputSearchKeys[0];
                else
                    return "";
            }
            set
            {
                this.InputSearchKeys = new string[1];
                this.InputSearchKeys[0] = value;
            }
        }

        private int _outputResultRecNo = 0;
        /// <summary>
        /// 找到了，就返回记录号。否则，输出0。
        /// </summary>
        public int OutputResultRecNo
        {
            get { return _outputResultRecNo; }
            set { _outputResultRecNo = value; }
        }

        private int _outputRangeLittler = 0;
        /// <summary>
        /// 更小的
        /// </summary>
        public int OutputRangeLittler
        {
            get { return _outputRangeLittler; }
            set { _outputRangeLittler = value; }
        }

        private int _outputRangeLarger = 0;
        /// <summary>
        /// 更大的
        /// </summary>
        public int OutputRangeLarger
        {
            get { return _outputRangeLarger; }
            set { _outputRangeLarger = value; }
        }

        public void DoLocate()
        {
            //======== 1. 二分法定位到相应的记录 =============
            int nLow = 1;
            int nHigh = this.InputFoxDBF.RecCount;

            while (nLow <= nHigh)
            {
                //======= 2. 二分之一的记录 ========
                int nMid = (nLow + nHigh) / 2;

                //========= 3. 定位到这条记录，取出数据 =========
                string[] arrKeyData = GetKeyDataOfDBFRecNo(nMid);

                //========= 4. 比较待定位的值和取出的二分之一处的数据 =======
                int nCompareResult = CompareSearchKeyAndMid(this.InputSearchKeys, arrKeyData);

                //========== 5. 如果相同，就返回结果 ==============
                if (nCompareResult == 0)
                {
                    this.OutputResultRecNo = nMid;
                    return;
                }

                //========= 3.5 否则，调整Low、High的值 ==========
                if (nCompareResult > 0)
                    nLow = nMid + 1;
                else
                    nHigh = nMid - 1;
            }

            //========= 4. 找不到，就返回空串 =============
            this.OutputResultRecNo = 0;
            this.OutputRangeLittler = nHigh;
            this.OutputRangeLarger = nLow;
        }

        private string[] GetKeyDataOfDBFRecNo(int nRecNo)
        {
            this.InputFoxDBF.Go(nRecNo);

            string[] arrKeyValue = new string[this.InputSearchKeys.Length];
            for (int i = 0; i < arrKeyValue.Length; i++)
            {
                arrKeyValue[i] = this.InputFoxDBF.GetRecordString(i);
            }

            return arrKeyValue;
        }

        private int CompareSearchKeyAndMid(string[] arrSearchKeys, string[] arrMidValues)
        {
            for (int i = 0; i < arrSearchKeys.Length; i++)
            {
                string sOneKey = arrSearchKeys[i];
                string sOneValue = arrMidValues[i];

                int nSingleCompareResult = sOneKey.CompareTo(sOneValue);
                if (nSingleCompareResult == 0)
                    continue;
                else
                    return nSingleCompareResult;
            }

            return 0;
        }
    }
}
