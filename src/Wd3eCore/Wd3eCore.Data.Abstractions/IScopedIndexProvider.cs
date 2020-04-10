using YesSql.Indexes;

namespace Wd3eCore.Data
{
    /// <summary>
    /// 代表一个契约
    /// 该契约表示需要由DI解析并在<see cref="YesSql.ISession"/>层注册的<see cref="YesSql.ISession"/>的<see cref="IIndexProvider"/>。
    /// </summary>
    public interface IScopedIndexProvider : IIndexProvider
    {
    }
}
