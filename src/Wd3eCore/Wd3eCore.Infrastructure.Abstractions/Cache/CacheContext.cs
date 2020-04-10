using System;
using System.Collections.Generic;
using System.Linq;

namespace Wd3eCore.Environment.Cache
{
    public class CacheContext
    {
        private HashSet<string> _contexts;
        private HashSet<string> _tags;

        public CacheContext(string cacheId)
        {
            CacheId = cacheId;
        }

        /// <summary>
        /// 定义应该缓存值的绝对时间。
        /// 如果不调用，将使用滑动值。
        /// </summary>
        public CacheContext WithExpiryOn(DateTimeOffset expiry)
        {
            ExpiresOn = expiry;
            return this;
        }

        /// <summary>
        /// 定义应该为其缓存的绝对时间(相对于现在)。
        /// 如果不调用，将使用滑动值。
        /// </summary>
        public CacheContext WithExpiryAfter(TimeSpan duration)
        {
            ExpiresAfter = duration;
            return this;
        }

        /// <summary>
        /// 定义值应该缓存的滑动过期时间。
        /// 如果没有调用，将使用默认的滑动值(除非指定了绝对失效时间)。
        /// </summary>
        public CacheContext WithExpirySliding(TimeSpan window)
        {
            ExpiresSliding = window;
            return this;
        }

        /// <summary>
        /// 定义用于缓存形态的维度。例如，通过使用<code>"user"</code>，每个用户将得到不同的值。
        /// </summary>
        public CacheContext AddContext(params string[] contexts)
        {
            if (_contexts == null)
            {
                _contexts = new HashSet<string>();
            }

            foreach (var context in contexts)
            {
                _contexts.Add(context);
            }

            return this;
        }

        /// <summary>
        /// 删除特定上下文。
        /// </summary>
        public CacheContext RemoveContext(string context)
        {
            if (_contexts != null)
            {
                _contexts.Remove(context);
            }

            return this;
        }

        public CacheContext AddTag(params string[] tags)
        {
            if (_tags == null)
            {
                _tags = new HashSet<string>();
            }

            foreach (var tag in tags)
            {
                _tags.Add(tag);
            }

            return this;
        }

        public CacheContext RemoveTag(string tag)
        {
            if (_tags != null)
            {
                _tags.Remove(tag);
            }

            return this;
        }

        public string CacheId { get; }
        public ICollection<string> Contexts => (ICollection<string>)_contexts ?? Array.Empty<string>();
        public IEnumerable<string> Tags => _tags ?? Enumerable.Empty<string>();
        public DateTimeOffset? ExpiresOn { get; private set; }
        public TimeSpan? ExpiresAfter { get; private set; }
        public TimeSpan? ExpiresSliding { get; private set; }
    }
}
