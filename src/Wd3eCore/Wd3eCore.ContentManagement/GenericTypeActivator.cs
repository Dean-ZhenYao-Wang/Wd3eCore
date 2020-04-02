using System;

namespace Wd3eCore.ContentManagement
{
    internal class GenericTypeActivator<T, TInstance> : ITypeActivator<TInstance> where T : TInstance, new()
    {
        /// <inheritdoc />
        public Type Type => typeof(T);

        /// <inheritdoc />
        public TInstance CreateInstance()
        {
            return new T();
        }
    }
}
