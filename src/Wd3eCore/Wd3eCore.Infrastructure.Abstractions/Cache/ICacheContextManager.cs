using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wd3eCore.Environment.Cache
{
    public interface ICacheContextManager
    {
        /// <summary>
        /// Provides the discriminator for a specific cache context by requesting all <see cref="ICacheContextProvider"/>
        /// implementations.
        /// </summary>
        Task<IEnumerable<CacheContextEntry>> GetDiscriminatorsAsync(IEnumerable<string> contexts);
    }
}
