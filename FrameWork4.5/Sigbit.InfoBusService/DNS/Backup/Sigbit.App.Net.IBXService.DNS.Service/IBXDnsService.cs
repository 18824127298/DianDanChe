using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;
using Sigbit.App.Net.IBXService.Message;
using Sigbit.App.Net.IBXService.Log;
using Sigbit.App.Net.IBXService.DNS.Message;
using Sigbit.App.Net.IBXService.DNS.Service.DBDefine;

namespace Sigbit.App.Net.IBXService.DNS.Service
{
    public class IBXDnsService
    {
        private static IBXDnsService _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static IBXDnsService Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new IBXDnsService();
                return _thisInstance;
            }
        }

        public IBMDnsRESP DealWithRequest(IBMRequestBase reqMess, byte[] bsPacket)
        {
            IBMDnsRESP dnsRESP = new IBMDnsRESP();

            IBXLogMessage ibxLogMessage = new IBXLogMessage();
            ibxLogMessage.RequestTime = DateTime.Now;

            //========== 1. 根据不同的输入消息，得到不同的响应消息 ==============
            string sUrlAddress = GetUrlAddressByTransCode(reqMess.TransCode, reqMess.FromSystem);
            if (sUrlAddress != "")
            {
                dnsRESP.ServiceUrlAddress = sUrlAddress;
            }
            else
            {
                dnsRESP.ErrorCode = 1635;
                dnsRESP.ErrorString = "未知的交易码-" + reqMess.TransCode;
            }

            //=============== 2. 记录消息日志 ==============
            //=========== 2.1 生成日志记录数据 =============
            ibxLogMessage.RequestMessage = reqMess;
            ibxLogMessage.RequestPacket = bsPacket;
            ibxLogMessage.ResponseMessage = dnsRESP;

            //============ 2.2 插入日志记录 ===========
            ibxLogMessage.NoteLog();

            //============== 3. 消息响应 =============
            return dnsRESP;
        }

        private string GetUrlAddressByTransCode(string sTransCode, string sFromSystem)
        {
            TbMapTransCodeUrl tblMap = QDBDnsPools.PoolMapTransCodeUrl.GetMapRecByTransCodeFromSystem(sTransCode, sFromSystem);
            if (tblMap == null)
                return "";

            string sUrlAddressUid = tblMap.UrlAddressUid;

            TbSysUrlAddress tblAddress = QDBDnsPools.PoolUrlAddress.GetUrlAddressRecByUrlAddressUid(sUrlAddressUid);
            if (tblAddress == null)
                return "";

            return tblAddress.UrlAddressLink;
        }
    }
}
