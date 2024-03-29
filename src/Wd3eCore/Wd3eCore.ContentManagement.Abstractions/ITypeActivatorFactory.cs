using System;

namespace Wd3eCore.ContentManagement
{
    /// <summary>
    /// Represents a service that can provide an <see cref="ITypeActivator{TInstance}"/> instance for a
    /// <see cref="TInstance"/> name.
    /// </summary>
    public interface ITypeActivatorFactory<TInstance>
    {
        ITypeActivator<TInstance> GetTypeActivator(string partName);
    }

    /// <summary>
    /// Represents the type information for a content element.
    /// </summary>
    public interface ITypeActivator<TInstance>
    {
        /// <summary>
        /// The <see cref="Type"/> of the content element represented by the activator.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Creates an instance of the content element type represented by the activator.
        /// </summary>
        TInstance CreateInstance();
    }
}
