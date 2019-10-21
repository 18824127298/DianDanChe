using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.IBXService.DNS.Service.DBDefine
{
    public class QDBDnsPools
    {
        private static QDBDnsPoolService _poolService = null;
        /// <summary>
        /// 服务
        /// </summary>
        public static QDBDnsPoolService PoolService
        {
            get
            {
                if (_poolService == null)
                    _poolService = new QDBDnsPoolService();
                return _poolService;
            }
        }

        private static QDBDnsPoolTransCode _poolTransCode = null;
        /// <summary>
        /// 交易码
        /// </summary>
        public static QDBDnsPoolTransCode PoolTransCode
        {
            get
            {
                if (_poolTransCode == null)
                    _poolTransCode = new QDBDnsPoolTransCode();
                return _poolTransCode;
            }
        }

        private static QDBDnsPoolUrlAddress _poolUrlAddress = null;
        public static QDBDnsPoolUrlAddress PoolUrlAddress
        {
            get
            {
                if (_poolUrlAddress == null)
                    _poolUrlAddress = new QDBDnsPoolUrlAddress();
                return _poolUrlAddress;
            }
        }

        private static QDBDnsPoolMapTransCodeUrl _poolMapTransCodeUrl = null;
        public static QDBDnsPoolMapTransCodeUrl PoolMapTransCodeUrl
        {
            get
            {
                if (_poolMapTransCodeUrl == null)
                    _poolMapTransCodeUrl = new QDBDnsPoolMapTransCodeUrl();
                return _poolMapTransCodeUrl;
            }
        }

        public static void ResetAll()
        {
            _poolService = null;
            _poolTransCode = null;
            _poolUrlAddress = null;
            _poolMapTransCodeUrl = null;
        }
    }
}
