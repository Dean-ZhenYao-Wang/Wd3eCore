using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YesSql;

namespace Wd3eCore.Data
{
    /// <summary>
    /// 表示为<see cref="ISession"/>提供辅助方法的类。
    /// </summary>
    public class SessionHelper : ISessionHelper
    {
        private readonly ISession _session;

        private readonly Dictionary<Type, object> _loaded = new Dictionary<Type, object>();

        /// <summary>
        /// 创建一个<see cref="SessionHelper"/>的新实例。
        /// </summary>
        /// <param name="session">The <see cref="ISession"/>.</param>
        public SessionHelper(ISession session)
        {
            _session = session;
        }

        /// <inheritdocs/>
        public async Task<T> LoadForUpdateAsync<T>(Func<T> factory = null) where T : class, new()
        {
            if (_loaded.TryGetValue(typeof(T), out var loaded))
            {
                return loaded as T;
            }

            var document = await _session.Query<T>().FirstOrDefaultAsync() ?? factory?.Invoke() ?? new T();

            _loaded[typeof(T)] = document;

            return document;
        }

        /// <inheritdocs/>
        public async Task<T> GetForCachingAsync<T>(Func<T> factory = null) where T : class, new()
        {
            if (_loaded.TryGetValue(typeof(T), out var loaded))
            {
                _session.Detach(loaded);
            }

            var document = await _session.Query<T>().FirstOrDefaultAsync();

            if (document != null)
            {
                _session.Detach(document);
                return document;
            }

            return factory?.Invoke() ?? new T();
        }
    }
}
