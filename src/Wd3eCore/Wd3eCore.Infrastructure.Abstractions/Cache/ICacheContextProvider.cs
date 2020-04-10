using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wd3eCore.Environment.Cache
{
    /// <summary>
    /// 返回一组值，这些值描述上下文的鉴别器，并返回一个值，每当鉴别器的状态发生变化时，该值就会变化，例如时间戳的序列号。
    /// </summary>
    public interface ICacheContextProvider
    {
        Task PopulateContextEntriesAsync(IEnumerable<string> contexts, List<CacheContextEntry> entries);
    }
}
