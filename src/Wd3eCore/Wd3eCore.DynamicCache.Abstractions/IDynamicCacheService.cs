using System.Threading.Tasks;
using Wd3eCore.Environment.Cache;

namespace Wd3eCore.DynamicCache
{
    public interface IDynamicCacheService
    {
        Task<string> GetCachedValueAsync(CacheContext context);
        Task SetCachedValueAsync(CacheContext context, string value);
    }
}
