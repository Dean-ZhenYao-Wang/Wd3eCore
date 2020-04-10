using System.Threading.Tasks;
using GraphQL.Types;
using Microsoft.Extensions.Primitives;

namespace Wd3eCore.Apis.GraphQL
{
    /// <summary>
    /// 该接口的实现可以参与构建用于GraphQL请求的<see cref="ISchema"/>实例。
    /// </summary>
    public interface ISchemaBuilder
    {
        /// <summary>
        /// 更新<paramref name="schema"/>。
        /// </summary>
        /// <param name="schema"><see cref="ISchema"/>要更新的实例。</param>
        /// <returns>当 <see cref="IChangeToken"/>中使用的数据被无效时，一个<see cref="ISchema"/>的实例被无效。
        /// 实例已经改变，如果没有依赖关系，则为<c>null</c>。</returns>
        Task<IChangeToken> BuildAsync(ISchema schema);
    }
}
