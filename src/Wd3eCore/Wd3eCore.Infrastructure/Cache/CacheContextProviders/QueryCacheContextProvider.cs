using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Wd3eCore.Environment.Cache.CacheContextProviders
{
    public class QueryCacheContextProvider : ICacheContextProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string QueryPrefix = "query:";

        public QueryCacheContextProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task PopulateContextEntriesAsync(IEnumerable<string> contexts, List<CacheContextEntry> entries)
        {
            if (contexts.Any(ctx => String.Equals(ctx, "query", StringComparison.OrdinalIgnoreCase)))
            {
                var httpContext = _httpContextAccessor.HttpContext;
                var query = httpContext.Request.Query;
                var allKeys = query.Keys.OrderBy(x => x).ToArray();
                entries.AddRange(allKeys
                    .Select(x => new CacheContextEntry(
                        key: x.ToLowerInvariant(),
                        value: query[x].ToString().ToLowerInvariant())
                    ));

                // 如果我们跟踪任何查询值，我们不需要查看特定的查询值
                return Task.CompletedTask;
            }

            foreach (var context in contexts.Where(ctx => ctx.StartsWith(QueryPrefix, StringComparison.OrdinalIgnoreCase)))
            {
                var key = context.Substring(QueryPrefix.Length);

                var httpContext = _httpContextAccessor.HttpContext;
                var query = httpContext.Request.Query;
                entries.Add(new CacheContextEntry(
                        key: key.ToLowerInvariant(),
                        value: query[key].ToString().ToLowerInvariant())
                    );
            }

            return Task.CompletedTask;
        }
    }
}
