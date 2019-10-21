using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.App.Net.IBXService.Message;

namespace Sigbit.App.Net.IBXService.DNS.Message
{
    public class IBMDnsRESP : IBMResponseBase
    {
        private string _serviceUrlAddress = "";
        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServiceUrlAddress
        {
            get { return _serviceUrlAddress; }
            set { _serviceUrlAddress = value; }
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            this.AddAStringValue("dns.url", this.ServiceUrlAddress);
        }

        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            this.ServiceUrlAddress = GetAStringValue("dns.url");
        }

        public override string GetMessageDescription()
        {
            string sRet = base.GetMessageDescription();

            sRet += "服务地址:" + this.ServiceUrlAddress + ";";

            return sRet;
        }
    }
}
