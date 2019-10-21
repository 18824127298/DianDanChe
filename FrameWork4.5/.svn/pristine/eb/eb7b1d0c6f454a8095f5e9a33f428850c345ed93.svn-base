using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sigbit.Common;
using Sigbit.Framework.Patch.DBDefine;
using System.Web;
using System.Net;

namespace Sigbit.Framework.Patch.ScheduleEngine
{
    public class SbtPatchScheduleEngine
    {
        private static SbtPatchScheduleEngine _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static SbtPatchScheduleEngine Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new SbtPatchScheduleEngine();
                return _thisInstance;
            }
        }

        private bool _hasDonePatchWork = false;

        /// <summary>
        /// 重打补丁的处理
        /// </summary>
        public void DoPatchEngineWork___ForRePatching()
        {
            _hasDonePatchWork = false;
            DoPatchEngineWork();
        }

        /// <summary>
        /// 扫描补丁并开始打补丁工作
        /// </summary>
        public void DoPatchEngineWork()
        {
            //======== 1. 仅在应用启动后，做一次补丁操作 ==========
            if (_hasDonePatchWork)
                return;

            //=========== 2. 真正的补丁工作 =============
            _hasDonePatchWork = true;
            DoPatchEngineWork___Do();
        }

        /// <summary>
        /// 真正的打补丁工作
        /// </summary>
        private void DoPatchEngineWork___Do()
        {
            //========== 1. 得到补丁的根目录 ============
            string sPatchRootDirectory = GetPatchRootDirectory();
            if (!Directory.Exists(sPatchRootDirectory))
                return;

            //======== 2. 得到根目录下的每一个补丁目录 ============
            string[] arrPatchDirectores = Directory.GetDirectories(sPatchRootDirectory);

            //========== 3. 处理每一个目录 ===========
            for (int i = 0; i < arrPatchDirectores.Length; i++)
            {
                string sDirectory = arrPatchDirectores[i];

                string sPatchKeyID = FileUtil.ExtractFileName(sDirectory);

                //====== 4. 如果已经打了补丁，就不需要再打了 =========
                if (HasBeenPatched(sPatchKeyID))
                    continue;

                //========== 4.1 异常处理，判断文件是否存在 =========
                if (!File.Exists(sDirectory + "\\patch.aspx"))
                {
                    SchedulePatchFailProcess(sPatchKeyID, "【补丁调度】补丁入口文件(patch.aspx)不存在");
                    continue;
                }

                //======= 5. 调用页面，进行打补丁操作 ==========
                string sPatchUrl = GetPatchUrl(sPatchKeyID);
                try
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadData(sPatchUrl);
                }
                catch (Exception ex)
                {
                    SchedulePatchFailProcess(sPatchKeyID, "【补丁调度】补丁调用失败，失败信息为-" + ex.Message);
                    continue;
                }
            }
        }

        /// <summary>
        /// 得到补丁的根目录
        /// </summary>
        /// <returns>补丁的根目录</returns>
        private string GetPatchRootDirectory()
        {
            string sRet = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\app_patch";
            return sRet;
        }

        /// <summary>
        /// 得到补丁的页面链接
        /// </summary>
        /// <param name="sPatchKeyID">补丁标识</param>
        /// <returns>页面链接</returns>
        private string GetPatchUrl(string sPatchKeyID)
        {
            string sRelativeUrl = "../../app_patch/" + sPatchKeyID + "/patch.aspx";
            Uri baseUri = HttpContext.Current.Request.Url;
            Uri absoluteUri = new Uri(baseUri, sRelativeUrl);

            string sRet = absoluteUri.AbsoluteUri;
            return sRet;
        }

        /// <summary>
        /// 判断补丁是否已安装
        /// </summary>
        /// <param name="sPatchKeyID">补丁标识</param>
        /// <returns>是否已安装</returns>
        /// <remarks>如果是新补丁、或正在安装过程中，都视为未安装，可以重新安装。</remarks>
        private bool HasBeenPatched(string sPatchKeyID)
        {
            TbSysPatchWork tblPatch = new TbSysPatchWork();
            if (!tblPatch.FetchByPatchKeyID(sPatchKeyID))
                return false;

            if (tblPatch.UpdateStatusE == TbSysPatchWorkEUpdateStatus.Patched)
                return true;
            else
                return false;
        }

        private void SchedulePatchFailProcess(string sPatchKeyID, string sFailDetail)
        {
            //=========== 1. 定位补丁记录，如果不存在，就创建一条 ==========
            TbSysPatchWork tblPatch = new TbSysPatchWork();
            if (!tblPatch.FetchByPatchKeyID(sPatchKeyID))
            {
                tblPatch.PatchUid = Guid.NewGuid().ToString();
                tblPatch.PatchKeyId = sPatchKeyID;
                tblPatch.PatchName = "";
                tblPatch.PatchDescription = "";
                tblPatch.DistributeTime = "";
                tblPatch.UpdateStatusE = TbSysPatchWorkEUpdateStatus.New;
                tblPatch.CreateTime = DateTimeUtil.Now;
                tblPatch.Creator = "SCHEDULE_ENGINE";
                tblPatch.ModifyTime = DateTimeUtil.Now;

                tblPatch.Insert();
            }

            //========== 2. 补丁更新的错误信息记录 ============
            tblPatch.UpdateStatusE = TbSysPatchWorkEUpdateStatus.Patched;

            tblPatch.UpdateResultSkip = "N";
            tblPatch.UpdateResultSucc = "N";
            tblPatch.UpdateResultDetail = sFailDetail;

            tblPatch.EngineCallTimes++;

            tblPatch.EngineLastDetail = sFailDetail;
            tblPatch.EngineLastResult = "【调度】更新失败";
            tblPatch.EngineLastTime = DateTimeUtil.Now;

            tblPatch.ModifyTime = DateTimeUtil.Now;

            tblPatch.Update();
        }
    }
}
