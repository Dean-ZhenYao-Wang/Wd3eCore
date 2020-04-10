using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wd3eCore.Environment.Cache.CacheContextProviders
{
    /// <summary>
    /// 将所有上下文值按原样添加到缓存项中。这允许已知的值变化
    /// </summary>
    public class KnownValueCacheContextProvider : ICacheContextProvider
    {
        public Task PopulateContextEntriesAsync(IEnumerable<string> contexts, List<CacheContextEntry> entries)
        {
            entries.Add(new CacheContextEntry("", String.Join(",", contexts)));

            return Task.CompletedTask;
        }
    }
}
