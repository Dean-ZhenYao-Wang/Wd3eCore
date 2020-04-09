using System;
using System.Collections.Concurrent;
using Wd3eCore.Environment.Extensions.Features;

namespace Wd3eCore.Environment.Extensions
{
    public class TypeFeatureProvider : ITypeFeatureProvider
    {
        private readonly ConcurrentDictionary<Type, IFeatureInfo> _features
            = new ConcurrentDictionary<Type, IFeatureInfo>();

        public IFeatureInfo GetFeatureForDependency(Type dependency)
        {
            IFeatureInfo feature = null;

            if (_features.TryGetValue(dependency, out feature))
            {
                return feature;
            }

            throw new InvalidOperationException($"Could not resolve feature for type {dependency.Name}/无法为类型{dependency.Name}解析特性");
        }

        public void TryAdd(Type type, IFeatureInfo feature)
        {
            _features.TryAdd(type, feature);
        }
    }
}
