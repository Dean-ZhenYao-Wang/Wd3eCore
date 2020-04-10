using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Wd3eCore.Apis
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册一个描述输入参数的类型
        /// </summary>
        /// <param name="services"></param>
        public static void AddInputObjectGraphType<TObject, TObjectType>(this IServiceCollection services)
            where TObject : class
            where TObjectType : InputObjectGraphType<TObject>
        {
            // 实例被注册为单例，因为它们的构造函数掌握着配置类型的逻辑，不需要每次都运行
            services.AddSingleton<TObjectType>();
            services.AddSingleton<InputObjectGraphType<TObject>, TObjectType>(s => s.GetRequiredService<TObjectType>());
            services.AddSingleton<IInputObjectGraphType, TObjectType>(s => s.GetRequiredService<TObjectType>());
        }

        /// <summary>
        /// 注册一个描述输出参数的类型
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TInputType"></typeparam>
        /// <param name="services"></param>
        public static void AddObjectGraphType<TInput, TInputType>(this IServiceCollection services)
            where TInput : class
            where TInputType : ObjectGraphType<TInput>
        {
            // 实例被注册为单例，因为它们的构造函数掌握着配置类型的逻辑，不需要每次都运行
            services.AddSingleton<TInputType>();
            services.AddSingleton<ObjectGraphType<TInput>, TInputType>(s => s.GetRequiredService<TInputType>());
            services.AddSingleton<IObjectGraphType, TInputType>(s => s.GetRequiredService<TInputType>());
        }
    }
}
