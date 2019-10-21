using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Common.Scopii
{
    public class NTXScopii
    {
        private string _fromPart = "";
        /// <summary>
        /// 起始
        /// </summary>
        public string FromPart
        {
            get { return _fromPart; }
            set { _fromPart = value; }
        }

        private string _toPart = "";
        /// <summary>
        /// 至
        /// </summary>
        public string ToPart
        {
            get { return _toPart; }
            set { _toPart = value; }
        }

        public string ScopiiString
        {
            get
            {
                if (this.FromPart == "" && this.ToPart == "")
                    return "";
                else if (this.FromPart == "")
                    return this.ToPart + "|" + this.ToPart;
                else if (this.ToPart == "")
                    return this.FromPart + "|" + this.FromPart;
                else
                    return this.FromPart + "|" + this.ToPart;
            }
            set
            {
                //======= 1. 找到第一个分隔符 =========
                int nSepPos = value.IndexOf('|');
                if (nSepPos == -1)
                {
                    this.FromPart = value;
                    this.ToPart = value;
                }
                else
                {
                    this.FromPart = value.Substring(0, nSepPos);
                    this.ToPart = value.Substring(nSepPos + 1);
                }
            }
        }

        /// <summary>
        /// 与另一个SCOPII合并
        /// </summary>
        /// <param name="anotherScopii">另一个SCOPII</param>
        /// <remarks>这种“智能”合并方式，要求字符串顺序从小到大排列。</remarks>
        public void MergeWith(NTXScopii anotherScopii)
        {
            //======= 1. 起始 ===========
            if (this.FromPart == "")
                this.FromPart = anotherScopii.FromPart;
            else if (anotherScopii.FromPart != "")
            {
                if (this.FromPart.CompareTo(anotherScopii.FromPart) > 0)
                    this.FromPart = anotherScopii.FromPart;
            }

            //========= 2. 至 ============
            if (this.ToPart == "")
                this.ToPart = anotherScopii.ToPart;
            else if (anotherScopii.ToPart != "")
            {
                if (this.ToPart.CompareTo(anotherScopii.ToPart) < 0)
                    this.ToPart = anotherScopii.ToPart;
            }
        }

        public void MergeOnTail(NTXScopii anotherScopii)
        {
            if (this.FromPart == "")
                this.FromPart = anotherScopii.FromPart;

            if (anotherScopii.ToPart != "")
                this.ToPart = anotherScopii.ToPart;
        }

        public void MergeOnHead(NTXScopii anotherScopii)
        {
            if (anotherScopii.FromPart != "")
                this.FromPart = anotherScopii.FromPart;

            if (this.ToPart == "")
                this.ToPart = anotherScopii.ToPart;
        }
    }
}
