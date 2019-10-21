using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Caching;

namespace CheDaiBaoCommonService.Caching
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class TableDependency : ICacheDependency
    {
        protected char[] configurationSeparator = new char[] {','};
        protected AggregateCacheDependency dependency = new AggregateCacheDependency();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configKey"></param>
        protected TableDependency(string configKey)
        {
            string dbName = ConfigurationManager.AppSettings[""];
            string tableConfig = ConfigurationManager.AppSettings[configKey];
            string[] tables = tableConfig.Split(configurationSeparator);
            foreach (string tableName in tables)
                dependency.Add(new SqlCacheDependency(dbName, tableName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AggregateCacheDependency GetDependency()
        {
            return dependency;
        }
    }
}
