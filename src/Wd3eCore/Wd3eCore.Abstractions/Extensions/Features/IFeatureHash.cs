using System.Threading.Tasks;

namespace Wd3eCore.Environment.Extensions.Features
{
    /// <summary>
    /// 此接口的实现提供对启用特性状态的有效访问，以便提供用于缓存的Hash。
    /// 因为它的状态应该被缓存，所以实例不应该有任何状态，因此被声明为瞬态。
    /// </summary>
    public interface IFeatureHash
    {
        /// <summary>
        /// 返回一个序列号，该序列号表示当前租户的可用特性列表。
        /// </summary>
        /// <returns>
        ///  一个<see cref="int"/>值，该值在每次特性列表更改时都会更改。
        /// </returns>
        Task<int> GetFeatureHashAsync();

        /// <summary>
        /// 返回一个序列号，该序列号表示当前租户的可用特性列表。
        /// </summary>
        /// <returns>
        /// 一个<see cref="int"/>值，该值在每次启用特定特性时都会更改。
        /// </returns>
        Task<int> GetFeatureHashAsync(string featureId);
    }
}
