using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Wd3eCore.Modules;

namespace Wd3eCore.Environment.Cache
{
    public class DefaultTagCache : ITagCache
    {
        private const string CacheKey = nameof(DefaultTagCache);

        private readonly ConcurrentDictionary<string, HashSet<string>> _dictionary;
        private readonly IEnumerable<ITagRemovedEventHandler> _tagRemovedEventHandlers;
        private readonly ILogger<DefaultTagCache> _logger;

        public DefaultTagCache(
            IEnumerable<ITagRemovedEventHandler> tagRemovedEventHandlers,
            IMemoryCache memoryCache,
            ILogger<DefaultTagCache> logger)
        {
            // 我们使用内存缓存作为状态容器，并保持这个类是临时的，因为它依赖于非单例对象

            if (!memoryCache.TryGetValue(CacheKey, out _dictionary))
            {
                _dictionary = new ConcurrentDictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
                memoryCache.Set(CacheKey, _dictionary);
            }

            _tagRemovedEventHandlers = tagRemovedEventHandlers;
            _logger = logger;
        }

        public void Tag(string key, params string[] tags)
        {
            foreach (var tag in tags)
            {
                var set = _dictionary.GetOrAdd(tag, x => new HashSet<string>());

                lock (set)
                {
                    set.Add(key);
                }
            }
        }

        public IEnumerable<string> GetTaggedItems(string tag)
        {
            HashSet<string> set;
            if (_dictionary.TryGetValue(tag, out set))
            {
                lock (set)
                {
                    return set;
                }
            }

            return Enumerable.Empty<string>();
        }

        public Task RemoveTagAsync(string tag)
        {
            HashSet<string> set;

            if (_dictionary.TryRemove(tag, out set))
            {
                return _tagRemovedEventHandlers.InvokeAsync((handler, tag, set) => handler.TagRemovedAsync(tag, set), tag, set, _logger);
            }

            return Task.CompletedTask;
        }
    }
}
