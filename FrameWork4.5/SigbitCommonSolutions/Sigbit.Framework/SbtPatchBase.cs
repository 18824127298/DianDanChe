using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Framework
{
    public class SbtPatchBase
    {
        private string _patchName = "";
        /// <summary>
        /// 补丁名称
        /// </summary>
        public string PatchName
        {
            get { return _patchName; }
            set { _patchName = value; }
        }


        private string _patchDistributeTime = "";
        /// <summary>
        /// 补丁的发布时间
        /// </summary>
        public string PatchDistributeTime
        {
            get { return _patchDistributeTime; }
            set { _patchDistributeTime = value; }
        }


        private string _patchVersionForce = "";
        /// <summary>
        /// 补丁的强制版本号
        /// </summary>
        public string PatchVersionForce
        {
            get { return _patchVersionForce; }
            set { _patchVersionForce = value; }
        }

        private string _outputMessage = "";
        /// <summary>
        /// 输出的供显示信息
        /// </summary>
        public string OutputMessage
        {
            get { return _outputMessage; }
            set { _outputMessage = value; }
        }


        public virtual bool WillDoDispatch(ref string sUpdateDetail)
        {
            return true;
        }


        public virtual void PatchWork(ref string sUpdateDetail)
        {
        }


        public void DoPatch()
        {
            //========== 1. 判断补丁是否已经安装 ==========
            //    string sPatchKeyID = GetCurrentPatchKeyID();    // 可以从执行的路径中获取
            //    if (PatchHasUpdate(sPatchKeyID))
            //    {
            //        this.OutputMessage = "【跳过补丁更新】补丁之前已安装过了。";
            //        return;
            //}

            //========= 2. 判断能否安装补丁 ==========
            string sWillDetail = "";
            if (!WillDoDispatch(ref  sWillDetail))
            {
                //    this.OutputMessage = "【跳过补丁更新】" + sWillDetail;
                //    UpdateXXXX();		// 更新相应的数据状态
            }

            //=========== 3. 补丁的过程 ==========
            string sUpdateDetail = "";
            PatchWork(ref sUpdateDetail);

        }
    }
}
