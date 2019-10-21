using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;
using Sigbit.Data;
using Sigbit.Framework;

namespace Sigbit.App.Net.IBXService.VoiceCOMP.Service.VCOMPService
{
    public class IBMVoiceCOMPConfig
    {
        private static IBMVoiceCOMPConfig _instance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static IBMVoiceCOMPConfig Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new IBMVoiceCOMPConfig();
                return _instance;
            }
        }


        private string _voiceRootPath = "";
        /// <summary>
        /// 
        /// </summary>
        public string VoiceRootPath
        {
            get
            {
                if (_voiceRootPath == "")
                {
                    _voiceRootPath = TbSysParameter.GetParameterValue("voice_compare", "path_config", "voice_root_path");
                }
                return _voiceRootPath;
            }
            set
            {
                _voiceRootPath = value;
                TbSysParameter.SetParameterValue("voice_compare", "path_config", "voice_root_path", _voiceRootPath);
            }
        }

    }
}
