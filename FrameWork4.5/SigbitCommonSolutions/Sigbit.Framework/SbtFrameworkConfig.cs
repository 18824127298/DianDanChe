using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Framework
{
    class SbtFrameworkConfig
    {
        private static SbtFrameworkConfig _thisIntance = null;
        /// <summary>
        /// ����Ψһ��ʵ��
        /// </summary>
        public static SbtFrameworkConfig Instance
        {
            get
            {
                if (_thisIntance == null)
                    _thisIntance = new SbtFrameworkConfig();

                return _thisIntance;
            }
        }

        /// <summary>
        /// ����ɨ���ʱ����
        /// </summary>
        public int TaskQueryTableTimerInterval
        {
            get
            {
                return 10;
            }
        }
    }
}
