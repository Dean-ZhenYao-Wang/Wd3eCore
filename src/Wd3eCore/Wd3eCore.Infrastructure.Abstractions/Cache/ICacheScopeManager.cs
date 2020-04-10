using System;

namespace Wd3eCore.Environment.Cache
{
    public interface ICacheScopeManager
    {
        void EnterScope(CacheContext context);
        void ExitScope();
        /// <summary>
        /// 将给定的依赖项添加到当前最内层缓存上下文
        /// </summary>
        /// <param name="dependencies">要添加的依赖项</param>
        void AddDependencies(params string[] dependencies);
        /// <summary>
        /// 将给定的上下文添加到当前最内层缓存上下文
        /// </summary>
        /// <param name="contexts">要添加的上下文</param>
        void AddContexts(params string[] contexts);
        void WithExpiryOn(DateTimeOffset expiryOn);
        void WithExpiryAfter(TimeSpan expiryAfter);
        void WithExpirySliding(TimeSpan expirySliding);
    }
}
