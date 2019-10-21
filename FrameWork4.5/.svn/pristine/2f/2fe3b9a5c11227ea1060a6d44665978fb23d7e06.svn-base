using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Framework.License
{
    /// <summary>
    /// 授权 - 标识特性
    /// </summary>
    public class SbtLicIDFeaturesConfig
    {
        private static SbtLicIDFeaturesConfig _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static SbtLicIDFeaturesConfig Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new SbtLicIDFeaturesConfig();
                return _thisInstance;
            }
        }

        private string _customerCode = "ITXEKE";
        /// <summary>
        /// 用户标识
        /// </summary>
        public string CustomerCode
        {
            get
            {
                if (_customerCode == "ITXEKE")
                    _customerCode = TbSysParameter.GetParameterValue("sbt_license", "id_features", "customer_code", "");

                return _customerCode;
            }
            set
            {
                _customerCode = value;
                TbSysParameter.SetParameterValue("sbt_license", "id_features", "customer_code", value);
            }
        }

        private string _customerFullName = "ITXEKE";
        /// <summary>
        /// 用户全称
        /// </summary>
        public string CustomerFullName
        {
            get
            {
                if (_customerFullName == "ITXEKE")
                    _customerFullName = TbSysParameter.GetParameterValue("sbt_license", "id_features", "customer_full_name", "");

                return _customerFullName;
            }
            set
            {
                _customerFullName = value;
                TbSysParameter.SetParameterValue("sbt_license", "id_features", "customer_full_name", value);
            }
        }

        private string _customerBriefName = "ITXEKE";
        /// <summary>
        /// 用户简称
        /// </summary>
        public string CustomerBriefName
        {
            get
            {
                if (_customerBriefName == "ITXEKE")
                    _customerBriefName = TbSysParameter.GetParameterValue("sbt_license", "id_features", "customer_brief_name", "");

                return _customerBriefName;
            }
            set
            {
                _customerBriefName = value;
                TbSysParameter.SetParameterValue("sbt_license", "id_features", "customer_brief_name", value);
            }
        }

        private string _systemFullName = "ITXEKE";
        /// <summary>
        /// 系统全称
        /// </summary>
        public string SystemFullName
        {
            get
            {
                if (_systemFullName == "ITXEKE")
                    _systemFullName = TbSysParameter.GetParameterValue("sbt_license", "id_features", "system_full_name", "");

                return _systemFullName;
            }
            set
            {
                _systemFullName = value;
                TbSysParameter.SetParameterValue("sbt_license", "id_features", "system_full_name", value);
            }
        }

        private string _systemBriefName = "ITXEKE";
        /// <summary>
        /// 系统简称
        /// </summary>
        public string SystemBriefName
        {
            get
            {
                if (_systemBriefName == "ITXEKE")
                    _systemBriefName = TbSysParameter.GetParameterValue("sbt_license", "id_features", "system_brief_name", "");

                return _systemBriefName;
            }
            set
            {
                _systemBriefName = value;
                TbSysParameter.SetParameterValue("sbt_license", "id_features", "system_brief_name", value);
            }
        }

    }
}
