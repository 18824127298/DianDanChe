using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;

namespace CheDaiBaoCommonService.Caching
{
    public interface ICacheDependency
    {
        AggregateCacheDependency GetDependency();
    }
}
