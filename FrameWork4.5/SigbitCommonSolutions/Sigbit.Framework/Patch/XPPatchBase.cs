using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Sigbit.Common;
using System.Diagnostics;

namespace Sigbit.Framework.Patch
{
    public enum XPPatchLevel
    {
        Minor,
        Medium,
        Major
    }

    public class XPPatchBase
    {
        private string _webAppID = "default";
        /// <summary>
        /// 应用标识
        /// </summary>
        public string WebAppID
        {
            get { return _webAppID; }
            set { _webAppID = value; }
        }

        private string _patchKeyID = "";
        /// <summary>
        /// 补丁标识
        /// </summary>
        public string PatchKeyID
        {
            get
            {
                if (_patchKeyID != "")
                    return _patchKeyID;

                Uri visitUri = HttpContext.Current.Request.Url;
                string sUrl = visitUri.AbsoluteUri;

                //=========== 1. 得到url的最后一个目录 ===========
                string sPath = FileUtil.ExtractFilePath(sUrl).TrimEnd('/');
                string sFinalDir = FileUtil.ExtractFileName(sPath);

                _patchKeyID = sFinalDir;

                Debug.Assert(_patchKeyID.Length >= 10);

                return sFinalDir;
            }
        }

        private string _patchName = "";
        /// <summary>
        /// 补丁的名称
        /// </summary>
        public string PatchName
        {
            get { return _patchName; }
            set { _patchName = value; }
        }

        private string _patchDesc = "";
        /// <summary>
        /// 补丁的说明
        /// </summary>
        public string PatchDesc
        {
            get { return _patchDesc; }
            set { _patchDesc = value; }
        }

        private string _patchDistributeTime = "";
        /// <summary>
        /// 补丁的发布日期
        /// </summary>
        public string PatchDistributeTime
        {
            get 
            {
                if (_patchDistributeTime != "")
                    return _patchDistributeTime;

                //=========== 1. 从PatchKeyID取出"_"之前的部分，并判断是否为8位 =======
                int nPosUnderline = this.PatchKeyID.IndexOf("_");
                if (nPosUnderline != 8)
                    _patchDistributeTime = DateTimeUtil.Now;
                else
                {
                    string sDatePart = this.PatchKeyID.Substring(0, 8);
                    string sDistribute14Time = sDatePart + "000000";

                    _patchDistributeTime = DateTimeUtil.FromDateTime14Str(sDistribute14Time);
                }

                return _patchDistributeTime; 
            }
            set { _patchDistributeTime = value; }
        }

        private XPPatchLevel _patchLevel = XPPatchLevel.Minor;
        /// <summary>
        /// 补丁的级别。缺省为小版本+1。
        /// </summary>
        public XPPatchLevel PatchLevel
        {
            get { return _patchLevel; }
            set { _patchLevel = value; }
        }

        private string _patchVersionForce = "";
        /// <summary>
        /// 补丁的强制版本号。如果设定了该版本号，则打完补丁后就是这个版本号了。
        /// </summary>
        public string PatchVersionForce
        {
            get { return _patchVersionForce; }
            set { _patchVersionForce = value; }
        }

        virtual public bool WillDoDispatch(ref string sUpdateDetail)
        {
            return true;
        }

        virtual public bool PatchWork(ref string sUpdateDetail)
        {
            return true;
        }
    }
}
