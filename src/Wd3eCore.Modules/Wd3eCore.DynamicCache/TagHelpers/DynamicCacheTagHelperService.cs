using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;

namespace Wd3eCore.DynamicCache.TagHelpers
{
    public class DynamicCacheTagHelperService
    {
        public ConcurrentDictionary<string, Task<IHtmlContent>> Workers = new ConcurrentDictionary<string, Task<IHtmlContent>>();
    }
}
