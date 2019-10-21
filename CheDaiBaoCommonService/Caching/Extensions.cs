/*----------------------------------------------------------------			
// Copyright (C) 2012 ZhongJin.com
//
// �ļ�����	        CacheExtensions.cs
// �ļ�����������  ʵ��ICacheManager�Ļ�����չ
//
//----------------------------------------------------------------*/

using System;

namespace CheDaiBaoCommonService.Caching
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// �����ȡ
        /// </summary>
        /// <typeparam name="T"> //</typeparam>
        /// <param name="cacheManager"></param>
        /// <param name="key"></param>
        /// <param name="acquire"></param>
        /// <returns></returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, 30, acquire);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager"></param>
        /// <param name="key"></param>
        /// <param name="cacheTime"></param>
        /// <param name="acquire"></param>
        /// <returns></returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire) 
        {
            return acquire();

            //if (cacheManager.IsSet(key))
            //{
            //    return cacheManager.Get<T>(key);
            //}
            //else
            //{
            //    var result = acquire();
            //    //if (result != null)
            //        cacheManager.Set(key, result, cacheTime);
            //    return result;
            //}
        }
    }
}
