using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.IBXService.VCodeBreak.Service.DBDefine
{
    public class QDBVCBreakPools
    {
        private static QDBVCBreakPoolAlgol _poolAlgol = null;
        /// <summary>
        /// 破解算法
        /// </summary>
        public static QDBVCBreakPoolAlgol PoolAlgol
        {
            get
            {
                if (_poolAlgol == null)
                    _poolAlgol = new QDBVCBreakPoolAlgol();
                return _poolAlgol;
            }
        }

        private static QDBVCBreakPoolVCodeUsage _poolVCodeUsage = null;
        /// <summary>
        /// 破解算法
        /// </summary>
        public static QDBVCBreakPoolVCodeUsage PoolVCodeUsage
        {
            get
            {
                if (_poolVCodeUsage == null)
                    _poolVCodeUsage = new QDBVCBreakPoolVCodeUsage();
                return _poolVCodeUsage;
            }
        }

        private static QDBVCBreakPoolAuthenUser _poolAuthenUser = null;
        /// <summary>
        /// 破解算法
        /// </summary>
        public static QDBVCBreakPoolAuthenUser PoolAuthenUser
        {
            get
            {
                if (_poolAuthenUser == null)
                    _poolAuthenUser = new QDBVCBreakPoolAuthenUser();
                return _poolAuthenUser;
            }
        }

        public static void ResetAll()
        {
            _poolAlgol = null;
            _poolVCodeUsage = null;
            _poolAuthenUser = null;
        }

        public static void ResetAlgol()
        {
            _poolAlgol = null;
        }

        public static void ResetVCodeUsage()
        {
            _poolVCodeUsage = null;
        }

        public static void ResetAuthenUser()
        {
            _poolAuthenUser = null;
        }
    }
}


