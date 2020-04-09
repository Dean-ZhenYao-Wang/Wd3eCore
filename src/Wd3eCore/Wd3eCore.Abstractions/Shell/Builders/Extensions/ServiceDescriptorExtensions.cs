using System;
using Microsoft.Extensions.DependencyInjection;

namespace Wd3eCore.Environment.Shell.Builders
{
    public static class ServiceDescriptorExtensions
    {
        public static Type GetImplementationType(this ServiceDescriptor descriptor)
        {
            if (descriptor is ClonedSingletonDescriptor cloned)
            {
                // 使用被克隆之前的父描述符。
                return cloned.Parent.GetImplementationType();
            }

            if (descriptor.ImplementationType != null)
            {
                return descriptor.ImplementationType;
            }

            if (descriptor.ImplementationInstance != null)
            {
                return descriptor.ImplementationInstance.GetType();
            }

            if (descriptor.ImplementationFactory != null)
            {
                return descriptor.ImplementationFactory.GetType().GenericTypeArguments[1];
            }

            return null;
        }
    }
}
