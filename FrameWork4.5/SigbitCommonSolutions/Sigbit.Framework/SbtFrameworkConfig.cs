using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Framework
{
    class SbtFrameworkConfig
    {
        private static SbtFrameworkConfig _thisIntance = null;
        /// <summary>
        /// 返回唯一的实例
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
        /// 任务扫表的时间间隔
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
