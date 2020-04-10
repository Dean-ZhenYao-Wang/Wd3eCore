using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Wd3eCore.Environment.Shell.Builders
{
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// 创建一个子容器。
        /// </summary>
        /// <param name="serviceProvider">服务提供商,为其创建一个子容器.</param>
        /// <param name="serviceCollection">克隆服务</param>
        public static IServiceCollection CreateChildContainer(this IServiceProvider serviceProvider, IServiceCollection serviceCollection)
        {
            IServiceCollection clonedCollection = new ServiceCollection();
            var servicesByType = serviceCollection.GroupBy(s => s.ServiceType);

            foreach (var services in servicesByType)
            {
                //防止托管 "IStartupFilter "将中间件重新添加到租户管道中。
                if (services.Key == typeof(IStartupFilter))
                {
                }

                //一般类型的定义 是用来创建其他构造的一般类型。
                else if (services.Key.IsGenericTypeDefinition)
                {
                    //所以，我们只需要传递描述符就可以了。
                    foreach (var service in services)
                    {
                        clonedCollection.Add(service);
                    }
                }

                //如果只有一种类型的服务。
                else if (services.Count() == 1)
                {
                    var service = services.First();

                    if (service.Lifetime == ServiceLifetime.Singleton)
                    {
                        //一个主机单例共享到各个租户容器中，但只有注册的实例不被DI处置，
                        //所以我们检查它是否是一次性的，或者说它使用的工厂可能会返回不同的类型。
                        if (typeof(IDisposable).IsAssignableFrom(service.GetImplementationType()) || service.ImplementationFactory != null)
                        {
                            //如果是一次性的，注册一个实例，我们立即从主容器中解析。
                            clonedCollection.CloneSingleton(service, serviceProvider.GetService(service.ServiceType));
                        }
                        else
                        {
                            // 如果不是一次性的，可以在第一次申请时通过工厂解决单元。
                            clonedCollection.CloneSingleton(service, sp => serviceProvider.GetService(service.ServiceType));

                            // Note:
                            //  大多数情况下，特定类型的单例服务是独一无二的，不是一次性的。
                            //  所以，大多数情况下，通过租户容器的第一次申请，就能解决。
                        }
                    }
                    else
                    {
                        clonedCollection.Add(service);
                    }
                }

                // 如果所有同类型的服务都不是单例服务。
                else if (services.All(s => s.Lifetime != ServiceLifetime.Singleton))
                {
                    // 我们不需要解决。
                    foreach (var service in services)
                    {
                        clonedCollection.Add(service);
                    }
                }

                // 如果所有同类型的服务都是单例服务。
                else if (services.All(s => s.Lifetime == ServiceLifetime.Singleton))
                {
                    //  我们可以从主容器中解析它们。
                    var instances = serviceProvider.GetServices(services.Key);

                    for (var i = 0; i < services.Count(); i++)
                    {
                        clonedCollection.CloneSingleton(services.ElementAt(i), instances.ElementAt(i));
                    }
                }

                //如果单例服务和作用域化服务混合在一起。
                else
                {
                    // 我们需要一个服务的作用域来解决。
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var instances = scope.ServiceProvider.GetServices(services.Key);

                        // 那么，我们只保留单例。
                        for (var i = 0; i < services.Count(); i++)
                        {
                            if (services.ElementAt(i).Lifetime == ServiceLifetime.Singleton)
                            {
                                clonedCollection.CloneSingleton(services.ElementAt(i), instances.ElementAt(i));
                            }
                            else
                            {
                                clonedCollection.Add(services.ElementAt(i));
                            }
                        }
                    }
                }
            }

            return clonedCollection;
        }
    }
}
