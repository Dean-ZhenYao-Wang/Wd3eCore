using System;
using Wd3eCore.Environment.Extensions.Features;

namespace Wd3eCore.Environment.Extensions
{
    /// <summary>
    /// 此服务的实现能够提供任何服务的"Feature"。
    /// </summary>
    public interface ITypeFeatureProvider
    {
        IFeatureInfo GetFeatureForDependency(Type dependency);
        void TryAdd(Type type, IFeatureInfo feature);
    }
}
