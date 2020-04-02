using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wd3eCore.ContentManagement.Cache;
using Wd3eCore.ContentManagement.Handlers;
using Wd3eCore.ContentManagement.Metadata;
using Wd3eCore.ContentManagement.Records;
using Wd3eCore.Data.Migration;
using Wd3eCore.Environment.Cache;
using YesSql.Indexes;

namespace Wd3eCore.ContentManagement
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddContentManagement(this IServiceCollection services)
        {
            services.AddScoped<ICacheContextProvider, ContentDefinitionCacheContextProvider>();
            services.TryAddScoped<IContentDefinitionManager, ContentDefinitionManager>();
            services.TryAddScoped<IContentDefinitionStore, DatabaseContentDefinitionStore>();
            services.TryAddScoped<IContentManager, DefaultContentManager>();
            services.TryAddScoped<IContentManagerSession, DefaultContentManagerSession>();
            services.AddSingleton<IIndexProvider, ContentItemIndexProvider>();
            services.AddScoped<IDataMigration, Migrations>();
            services.AddScoped<IContentHandler, UpdateContentsHandler>();
            services.AddScoped<IContentHandler, ContentPartHandlerCoordinator>();
            services.AddSingleton<ITypeActivatorFactory<ContentPart>, ContentPartFactory>();
            services.AddSingleton<ITypeActivatorFactory<ContentField>, ContentFieldFactory>();

            services.AddSingleton<IContentItemIdGenerator, DefaultContentItemIdGenerator>();
            services.AddScoped<IContentAliasManager, ContentAliasManager>();

            services.AddOptions<ContentOptions>();
            services.AddScoped<IContentPartHandlerResolver, ContentPartHandlerResolver>();
            return services;
        }

        public static IServiceCollection AddFileContentDefinitionStore(this IServiceCollection services)
        {
            services.RemoveAll<IContentDefinitionStore>();
            services.AddSingleton<IContentDefinitionStore, FileContentDefinitionStore>();
            services.AddScoped<FileContentDefinitionScopedCache>();

            return services;
        }
    }
}
