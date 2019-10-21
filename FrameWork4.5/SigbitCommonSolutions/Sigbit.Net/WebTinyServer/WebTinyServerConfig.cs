using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Net.WebTinyServer
{
    class WebTinyServerConfig
    {
        private static WebTinyServerConfig _thisInstance = null;
        /// <summary>
        /// Î¨Ò»ÊµÀý
        /// </summary>
        public static WebTinyServerConfig Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new WebTinyServerConfig();
                return _thisInstance;
            }
        }

        public bool WillSaveLog
        {
            get
            {
                return false;
            }
        }
    }
}
