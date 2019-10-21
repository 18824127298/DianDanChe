using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Data.FoxDBF
{
    class FoxDBFConfig
    {
        private static FoxDBFConfig _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static FoxDBFConfig Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new FoxDBFConfig();
                return _thisInstance;
            }
        }

        Encoding _currentEncoding = Encoding.Default;
        /// <summary>
        /// 当前的字符编码
        /// </summary>
        public Encoding CurrentEncoding
        {
            get { return _currentEncoding; }
            set { _currentEncoding = value; }
        }
    }
}
