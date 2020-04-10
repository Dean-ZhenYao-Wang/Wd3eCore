using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wd3eCore.Environment.Cache
{
    public interface ICacheContextManager
    {
        /// <summary>
        /// 通过请求所有<see cref="ICacheContextProvider"/>实现为特定的缓存上下文提供鉴别器。
        /// </summary>
        Task<IEnumerable<CacheContextEntry>> GetDiscriminatorsAsync(IEnumerable<string> contexts);
    }
}
