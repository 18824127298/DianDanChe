using System;
using System.Collections.Generic;
using System.Text;
using Sigbit.Common;
using Sigbit.Framework.Patch.DBDefine;
using Sigbit.Framework.Patch.WebAppVersion;

namespace Sigbit.Framework.Patch
{
    public class XPPatchWorkEngine
    {
        private XPPatchBase _inputPatch = null;
        /// <summary>
        /// 待更新的补丁
        /// </summary>
        public XPPatchBase InputPatch
        {
            get { return _inputPatch; }
            set { _inputPatch = value; }
        }

        private string _outputMessage = "";
        /// <summary>
        /// 输出信息
        /// </summary>
        public string OutputMessage
        {
            get { return _outputMessage; }
            set { _outputMessage = value; }
        }

        private TbSysPatchWork _patchWorkRec = null;

        public void DoPatch()
        {
            //========== 1. 判断补丁是否已经安装 ==========
            _patchWorkRec = GetPatchWorkRec();

            if (_patchWorkRec.UpdateStatusE == TbSysPatchWorkEUpdateStatus.Patched)
            {
                this.OutputMessage = "【跳过补丁更新】补丁之前已安装过了。";
                return;
            }

            if (_patchWorkRec.UpdateStatusE == TbSysPatchWorkEUpdateStatus.New)
            {
                _patchWorkRec.UpdateStatusE = TbSysPatchWorkEUpdateStatus.Patching;
                _patchWorkRec.ModifyTime = DateTimeUtil.Now;
                _patchWorkRec.Update();
            }

            //========= 2. 判断能否安装补丁 ==========
            string sWillDetail = "";

            _patchWorkRec.InstallTime = DateTimeUtil.Now;

            if (!this.InputPatch.WillDoDispatch(ref sWillDetail))
            {
                this.OutputMessage = "【跳过补丁更新】" + sWillDetail;

                _patchWorkRec.UpdateStatusE = TbSysPatchWorkEUpdateStatus.Patched;
                _patchWorkRec.UpdateResultSkip = "Y";
                _patchWorkRec.UpdateResultDetail = this.OutputMessage;
                _patchWorkRec.EngineCallTimes++;

                _patchWorkRec.EngineLastDetail = this.OutputMessage;
                _patchWorkRec.EngineLastResult = "跳过补丁更新";
                _patchWorkRec.EngineLastTime = DateTimeUtil.Now;

                _patchWorkRec.ModifyTime = DateTimeUtil.Now;

                _patchWorkRec.Update();

                return;
            }

            //============== 3. 补丁更新过程 ==============
            _patchWorkRec.UpdateBeginTime = DateTimeUtil.NowWithMilliSeconds;

            string sUpdateDetail = "";
            bool bPatchWorkSucc = this.InputPatch.PatchWork(ref sUpdateDetail);

            this.OutputMessage = sUpdateDetail;

            //=========== 4. 更新应用记录 ===============
            if (bPatchWorkSucc)
            {
                ATPatchWebAppVersionUpdater verUpdater = new ATPatchWebAppVersionUpdater();
                verUpdater.InputPatch = this.InputPatch;
                verUpdater.InputPatchWorkRec = _patchWorkRec;

                verUpdater.DoUpdate();

                _patchWorkRec.VesionBeforeUpdate = verUpdater.OutputVesionBeforeUpdate;
                _patchWorkRec.VersionAfterUpdate = verUpdater.OutputVersionAfterUpdate;
            }
            else
            {
                _patchWorkRec.VesionBeforeUpdate = "";
                _patchWorkRec.VersionAfterUpdate = "";
            }

            //============ 5. 更新补丁记录 =================
            _patchWorkRec.UpdateEndTime = DateTimeUtil.NowWithMilliSeconds;
            _patchWorkRec.UpdateDuration = DateTimeUtil.MilliSecondsAfter(_patchWorkRec.UpdateBeginTime, 
                    _patchWorkRec.UpdateEndTime) / 1000.0;
            _patchWorkRec.UpdateStatusE = TbSysPatchWorkEUpdateStatus.Patched;

            _patchWorkRec.UpdateResultSkip = "N";
            _patchWorkRec.UpdateResultSucc = bPatchWorkSucc ? "Y" : "N";
            _patchWorkRec.UpdateResultDetail = this.OutputMessage;


            _patchWorkRec.EngineCallTimes++;

            _patchWorkRec.EngineLastDetail = this.OutputMessage;
            _patchWorkRec.EngineLastResult = bPatchWorkSucc ? "更新成功" : "更新失败";
            _patchWorkRec.EngineLastTime = DateTimeUtil.Now;

            _patchWorkRec.ModifyTime = DateTimeUtil.Now;

            _patchWorkRec.Update();
        }

        /// <summary>
        /// 得到补丁的记录。如果不存在，会自动建一条。
        /// </summary>
        /// <returns>补丁的数据库记录</returns>
        private TbSysPatchWork GetPatchWorkRec()
        {
            TbSysPatchWork tblPatch = new TbSysPatchWork();
            if (tblPatch.FetchByPatchKeyID(this.InputPatch.PatchKeyID))
                return tblPatch;

            tblPatch.PatchUid = Guid.NewGuid().ToString();
            tblPatch.PatchKeyId = this.InputPatch.PatchKeyID;
            tblPatch.PatchName = this.InputPatch.PatchName;
            tblPatch.PatchDescription = this.InputPatch.PatchDesc;
            tblPatch.DistributeTime = this.InputPatch.PatchDistributeTime;
            tblPatch.UpdateStatusE = TbSysPatchWorkEUpdateStatus.New;
            tblPatch.CreateTime = DateTimeUtil.Now;
            tblPatch.Creator = "PATCH_WORK_ENGINE";
            tblPatch.ModifyTime = DateTimeUtil.Now;

            tblPatch.Insert();

            return tblPatch;
        }


    }
}
