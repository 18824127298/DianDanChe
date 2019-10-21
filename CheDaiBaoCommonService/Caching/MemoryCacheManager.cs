/*----------------------------------------------------------------			
// Copyright (C) 2012 ZhongJin.com
//
// 文件名：	        MemoryCacheManager.cs
// 文件功能描述：  内存缓存处理方法
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

namespace CheDaiBaoCommonService.Caching
{
    /// <summary>
    /// Represents a MemoryCacheCache
    /// </summary>
    public partial class MemoryCacheManager : ICacheManager
    {
        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }
        
        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public T Get<T>(string key)
        {
            lock (this)
            {
                return (T)Cache[key];
            }
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        public void Set(string key, object data, int cacheTime)
        {
            lock (this)
            {
                if (data == null)
                    return;

                var policy = new CacheItemPolicy
                                 {
                                     AbsoluteExpiration = DateTime.Now.AddSeconds(cacheTime)
                                 };
                Cache.Add(new CacheItem(key, data), policy);
            }
        }

        public void Set(string key, object data, string tableName)
        {
            if (data == null)
                return;

            //var dependency =  new SqlCacheDependency("ZhongJinOnline", tableName);
            //var monitor = new SqlChangeMonitor(dependency);
            //var cd=new SqlChangeMonitor(new SqlDependency())
            //var c = new CacheItemPolicy();
            //c.ChangeMonitors.Add(new SqlChangeMonitor());
            //HttpRuntime.Cache.Insert();
            //Cache.Add(new CacheItem(key, data),c);
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public bool IsSet(string key)
        {
            lock (this)
            {
                return (Cache.Contains(key));
            }
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public void Remove(string key)
        {
            lock (this)
            {
                Cache.Remove(key);
            }
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public void RemoveByPattern(string pattern)
        {
            lock (this)
            {
                var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var keysToRemove = (from item in Cache where regex.IsMatch(item.Key) select item.Key).ToList();

                foreach (string key in keysToRemove)
                {
                    Remove(key);
                }
            }
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public void Clear()
        {
            lock (this)
            {
                foreach (var item in Cache)
                    Remove(item.Key);
            }
        }
    }
}