using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Framework.Patch.DBDefine;
using Sigbit.Common;

namespace Sigbit.Framework.Patch.WebAppVersion
{
    class ATPatchWebAppVersionUpdater
    {
        private XPPatchBase _inputPatch = null;
        /// <summary>
        /// 补丁
        /// </summary>
        public XPPatchBase InputPatch
        {
            get { return _inputPatch; }
            set { _inputPatch = value; }
        }

        private TbSysPatchWork _inputPatchWorkRec = null;
        /// <summary>
        /// 补丁记录
        /// </summary>
        public TbSysPatchWork InputPatchWorkRec
        {
            get { return _inputPatchWorkRec; }
            set { _inputPatchWorkRec = value; }
        }

        private string _outputVesionBeforeUpdate = "";
        /// <summary>
        /// 更新前的版本号
        /// </summary>
        public string OutputVesionBeforeUpdate
        {
            get { return _outputVesionBeforeUpdate; }
            set { _outputVesionBeforeUpdate = value; }
        }

        private string _outputVersionAfterUpdate = "";
        /// <summary>
        /// 更新后的版本号
        /// </summary>
        public string OutputVersionAfterUpdate
        {
            get { return _outputVersionAfterUpdate; }
            set { _outputVersionAfterUpdate = value; }
        }


        public void DoUpdate()
        {
            //============ 1. 得到当前的系统应用 ==========
            TbSysWebapp tblApp = new TbSysWebapp();
            tblApp.WebappId = this.InputPatch.WebAppID;

            if (!tblApp.Fetch(true))
            {
                //========== 1.1 如果不存在，就插入一条记录 ===========
                tblApp.WebappId = this.InputPatch.WebAppID;
                if (tblApp.WebappId == "default")
                    tblApp.WebappName = "缺省应用";
                else
                    tblApp.WebappName = "";
                tblApp.WebappDesc = "";
                tblApp.CurrentVersionUid = "";
                tblApp.CurrentVersionNum = "1.0.0";
                tblApp.LastPatchUid = "";
                tblApp.LastPatchKeyId = "";
                tblApp.CreateTime = DateTimeUtil.Now;
                tblApp.Creator = "APP_VERSION_UPDATER";
                tblApp.ModifyTime = DateTimeUtil.Now;
                tblApp.Remarks = "";

                tblApp.Insert();
            }

            //=========== 2. 得到新的版本号 ===========
            this.OutputVesionBeforeUpdate = tblApp.CurrentVersionNum;

            string sNewVersionNum = GetNewVersionNum(tblApp.CurrentVersionNum);

            this.OutputVersionAfterUpdate = sNewVersionNum;

            //========= 3. 新增版本信息 ==============
            TbSysWebappVersion tblVersion = new TbSysWebappVersion();
            tblVersion.VersionUid = Guid.NewGuid().ToString();
            tblVersion.WebappId = tblApp.WebappId;
            tblVersion.VersionNum = RegulateVersionNum(sNewVersionNum);
            tblVersion.VersionNumDisp = sNewVersionNum;
            tblVersion.VersionBriefDesc = this.InputPatch.PatchName;
            tblVersion.VersionDetailDesc = this.InputPatch.PatchDesc;
            tblVersion.LastPatchUid = this.InputPatchWorkRec.PatchUid;
            tblVersion.LastPatchKeyId = this.InputPatch.PatchKeyID;
            tblVersion.CreateTime = DateTimeUtil.Now;
            tblVersion.Creator = "APP_VERSION_UPDATER";
            tblVersion.ModifyTime = DateTimeUtil.Now;
            tblVersion.Remarks = "";

            tblVersion.Insert();

            //========= 4. 更新系统应用记录 ============
            tblApp.CurrentVersionUid = tblVersion.VersionUid;
            tblApp.CurrentVersionNum = tblVersion.VersionNumDisp;

            tblApp.LastPatchUid = tblVersion.LastPatchUid;
            tblApp.LastPatchKeyId = tblVersion.LastPatchKeyId;

            tblApp.Update();
        }

        private string GetNewVersionNum(string sCurrentVersionNum)
        {
            //========= 1. 如果有强制版本号，就按指定的强制版本号升级 =============
            if (this.InputPatch.PatchVersionForce != "")
                return this.InputPatch.PatchVersionForce;

            //======== 2. 得到本版本的三个数字 ============
            int[] arrVersionPartNum = new int[3];
            string[] arrVersionParts = sCurrentVersionNum.Split('.');

            for (int i = 0; i < 3; i++)
            {
                string sOnePart = "";
                if (i < arrVersionParts.Length)
                    sOnePart = arrVersionParts[i];

                int nVersionPart = ConvertUtil.ToInt(sOnePart);

                arrVersionPartNum[i] = nVersionPart;
            }

            //=========== 3. 得到新的三个数字 ============
            if (this.InputPatch.PatchLevel == XPPatchLevel.Major)
                arrVersionPartNum[0]++;
            else if (this.InputPatch.PatchLevel == XPPatchLevel.Medium)
                arrVersionPartNum[1]++;
            else
                arrVersionPartNum[2]++;

            //============ 4. 溢出处理 =============
            if (arrVersionPartNum[2] > 999)
            {
                arrVersionPartNum[2] = 0;
                arrVersionPartNum[1]++;
            }

            if (arrVersionPartNum[1] > 999)
            {
                arrVersionPartNum[1] = 0;
                arrVersionPartNum[0]++;
            }

            //========== 5. 得到新的版本号 ============
            string sRet = "";
            for (int i = 0; i < 3; i++)
            {
                int nVersionPart = arrVersionPartNum[i];

                if (i != 0)
                    sRet += ".";
                sRet += nVersionPart.ToString();
            }

            return sRet;
        }

        public static string RegulateVersionNum(string sDisplayVersionNum)
        {
            string sRet = "";
            string[] arrVersionParts = sDisplayVersionNum.Split('.');

            for (int i = 0; i < 3; i++)
            {
                string sOnePart = "";
                if (i < arrVersionParts.Length)
                    sOnePart = arrVersionParts[i];

                int nVersionPart = ConvertUtil.ToInt(sOnePart);

                if (i != 0)
                    sRet += ".";

                sRet += nVersionPart.ToString("000");
            }

            return sRet;
        }

    }
}
