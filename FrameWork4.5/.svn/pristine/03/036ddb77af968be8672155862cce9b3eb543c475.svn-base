using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

using Sigbit.App.Net.IBXService.Message;

namespace Sigbit.App.Net.IBXService.DNS.Client
{
    class IBXDnsClient_CachePoolItem
    {
        private string _transCode = "";
        /// <summary>
        /// 交易码
        /// </summary>
        public string TransCode
        {
            get { return _transCode; }
            set { _transCode = value; }
        }

        private string _fromSystem = "";
        /// <summary>
        /// 来源系统
        /// </summary>
        public string FromSystem
        {
            get { return _fromSystem; }
            set { _fromSystem = value; }
        }

        private string _fromClientId = "";
        /// <summary>
        /// 来源的客户端标识
        /// </summary>
        public string FromClientId
        {
            get { return _fromClientId; }
            set { _fromClientId = value; }
        }

        private string _fromClientVersion = "";
        /// <summary>
        /// 来源的客户端版本号
        /// </summary>
        public string FromClientVersion
        {
            get { return _fromClientVersion; }
            set { _fromClientVersion = value; }
        }

        private string _serviceUrlAddress = "";
        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServiceUrlAddress
        {
            get { return _serviceUrlAddress; }
            set { _serviceUrlAddress = value; }
        }

        public string ToHashKey()
        {
            string sRet = this.TransCode + "-" + this.FromSystem + "-" + this.FromClientId + "-" + this.FromClientVersion;
            return sRet;
        }

        public void AssignByREQ(IBMRequestBase req)
        {
            this.TransCode = req.TransCode;
            this.FromSystem = req.FromSystem;
            this.FromClientId = req.FromClientId;
            this.FromClientVersion = req.FromClientVersion;
        }
    }

    class IBXDnsClient_CachePool : Hashtable
    {
        private static IBXDnsClient_CachePool _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static IBXDnsClient_CachePool Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new IBXDnsClient_CachePool();
                return _thisInstance;
            }
        }

        private void AddItem(IBXDnsClient_CachePoolItem item)
        {
            this[item.ToHashKey()] = item;
        }

        private IBXDnsClient_CachePoolItem GetItem(IBXDnsClient_CachePoolItem item)
        {
            string sHashKey = item.ToHashKey();
            return (IBXDnsClient_CachePoolItem)this[sHashKey];
        }

        public void AddItem(IBMRequestBase req, string sServiceAddress)
        {
            IBXDnsClient_CachePoolItem item = new IBXDnsClient_CachePoolItem();
            item.AssignByREQ(req);
            item.ServiceUrlAddress = sServiceAddress;

            this.AddItem(item);
        }

        public IBXDnsClient_CachePoolItem GetItem(IBMRequestBase req)
        {
            IBXDnsClient_CachePoolItem item = new IBXDnsClient_CachePoolItem();
            item.AssignByREQ(req);

            return (IBXDnsClient_CachePoolItem)GetItem(item);
        }

        public string GetServiceAddressOfREQ(IBMRequestBase req)
        {
            IBXDnsClient_CachePoolItem itemRet = GetItem(req);
            if (itemRet == null)
                return "";
            else
                return itemRet.ServiceUrlAddress;
        }
    }
}
