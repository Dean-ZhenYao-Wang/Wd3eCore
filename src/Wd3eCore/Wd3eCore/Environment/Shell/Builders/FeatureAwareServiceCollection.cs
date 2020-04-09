using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wd3eCore.Environment.Extensions.Features;

namespace Wd3eCore.Environment.Shell.Builders
{
    /// <summary>
    /// 一个服务集合，用于跟踪每个新增服务的 <see cref="IFeatureInfo"/> 。
    /// </summary>
    public class FeatureAwareServiceCollection : IServiceCollection
    {
        private readonly IServiceCollection _innerServiceCollection;

        private readonly Dictionary<IFeatureInfo, ServiceCollection> _featureServiceCollections = new Dictionary<IFeatureInfo, ServiceCollection>();
        private ServiceCollection _currentFeatureServiceCollection;

        public FeatureAwareServiceCollection(IServiceCollection innerServiceCollection)
        {
            _innerServiceCollection = innerServiceCollection;
        }

        /// <summary>
        /// 按其特征信息分组的服务集合。
        /// </summary>
        public IDictionary<IFeatureInfo, ServiceCollection> FeatureCollections => _featureServiceCollections;

        /// <summary>
        /// 设置添加到此集合中时,将分配给当前服务的特征。
        /// </summary>
        /// <param name="feature">分配给当前服务的特征</param>
        public void SetCurrentFeature(IFeatureInfo feature)
        {
            if (!_featureServiceCollections.TryGetValue(feature, out _currentFeatureServiceCollection))
            {
                _featureServiceCollections.Add(feature, _currentFeatureServiceCollection = new ServiceCollection());
            }
        }

        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return _innerServiceCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ICollection<ServiceDescriptor>.Add(ServiceDescriptor item)
        {
            _innerServiceCollection.Add(item);
            _currentFeatureServiceCollection?.Add(item);
        }

        public void Clear()
        {
            _innerServiceCollection.Clear();
            _featureServiceCollections.Clear();
        }

        public bool Contains(ServiceDescriptor item)
        {
            return _innerServiceCollection.Contains(item);
        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            _innerServiceCollection.CopyTo(array, arrayIndex);
        }

        public bool Remove(ServiceDescriptor item)
        {
            return _innerServiceCollection.Remove(item);
        }

        public int Count => _innerServiceCollection.Count;

        public bool IsReadOnly => _innerServiceCollection.IsReadOnly;

        public int IndexOf(ServiceDescriptor item)
        {
            return _innerServiceCollection.IndexOf(item);
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            _innerServiceCollection.Insert(index, item);
            _currentFeatureServiceCollection?.Add(item);
        }

        public void RemoveAt(int index)
        {
            _innerServiceCollection.RemoveAt(index);
        }

        public ServiceDescriptor this[int index]
        {
            get => _innerServiceCollection[index];
            set => _innerServiceCollection[index] = value;
        }
    }
}
