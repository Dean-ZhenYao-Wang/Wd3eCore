using System.Threading.Tasks;
using GraphQL.Types;

namespace Wd3eCore.Apis.GraphQL
{
    /// <summary>
    /// 代表提供在GraphQL请求中使用的<see cref="ISchema"/>实例的服务。
    /// 结果应该在可能的情况下缓存和重用。
    /// </summary>
    public interface ISchemaFactory
    {
        Task<ISchema> GetSchemaAsync();
    }
}
