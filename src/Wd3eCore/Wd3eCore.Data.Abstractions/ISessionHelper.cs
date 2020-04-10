using System;
using System.Threading.Tasks;

namespace Wd3eCore.Data
{
    /// <summary>
    /// 代表一个为<see cref="YesSql.ISession"/>提供帮助方法的契约。
    /// </summary>
    public interface ISessionHelper
    {
        /// <summary>
        /// 载入单个文档（或创建一个新的文档）进行更新，并且不应该被缓存。
        /// 对于完全隔离，它需要与<see cref="GetForCachingAsync"/>配对使用。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory">一个工厂的方法来加载或创建一个文档。</param>
        Task<T> LoadForUpdateAsync<T>(Func<T> factory = null) where T : class, new();

        /// <summary>
        /// 获取单个文档（或创建一个新的文档）进行缓存，该文档不应该被更新。
        /// 要完全隔离，需要与<see cref="LoadForUpdateAsync"/>配对使用。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory">一种获取或创建文档的工厂方法，用于缓存文档。</param>
        /// <returns></returns>
        Task<T> GetForCachingAsync<T>(Func<T> factory = null) where T : class, new();
    }
}
