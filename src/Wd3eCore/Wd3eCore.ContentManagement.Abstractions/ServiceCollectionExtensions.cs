using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wd3eCore.ContentManagement.Handlers;

namespace Wd3eCore.ContentManagement
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers a content part type. This method may be called multiple times safely,
        /// to reconfigure an existing part.
        /// </summary>
        public static ContentPartOptionBuilder AddContentPart<TContentPart>(this IServiceCollection services)
            where TContentPart : ContentPart
        {
            return services.AddContentPart(typeof(TContentPart));
        }

        /// <summary>
        /// Registers a content part type. This method may be called multiple times safely,
        /// to reconfigure an existing part.
        /// </summary>
        public static ContentPartOptionBuilder AddContentPart(this IServiceCollection services, Type contentPartType)
        {
            services.Configure<ContentOptions>(o => o.GetOrAddContentPart(contentPartType));
            return new ContentPartOptionBuilder(services, contentPartType);
        }

        /// <summary>
        /// Registers a content field type. This method may be called multiple times safely,
        /// to reconfigure an existing part.
        /// </summary>
        public static ContentFieldOptionBuilder AddContentField<TContentField>(this IServiceCollection services)
            where TContentField : ContentField
        {
            return services.AddContentField(typeof(TContentField));
        }

        /// <summary>
        /// Registers a content field type. This method may be called multiple times safely,
        /// to reconfigure an existing part.
        /// </summary>
        public static ContentFieldOptionBuilder AddContentField(this IServiceCollection services, Type contentFieldType)
        {
            services.Configure<ContentOptions>(o => o.GetOrAddContentField(contentFieldType));
            return new ContentFieldOptionBuilder(services, contentFieldType);
        }

        /// <summary>
        /// Register a handler for use with a content part.
        /// </summary>
        /// <typeparam name="TContentPartHandler"></typeparam>
        public static ContentPartOptionBuilder AddHandler<TContentPartHandler>(this ContentPartOptionBuilder builder)
            where TContentPartHandler : class, IContentPartHandler
        {
            return builder.AddHandler(typeof(TContentPartHandler));
        }

        /// <summary>
        /// Register a handler for use with a content part.
        /// </summary>
        public static ContentPartOptionBuilder AddHandler(this ContentPartOptionBuilder builder, Type handlerType)
        {
            builder.Services.TryAddScoped(handlerType);
            builder.Services.Configure<ContentOptions>(o =>
            {
                o.AddHandler(builder.ContentPartType, handlerType);
            });

            return builder;
        }

        /// <summary>
        /// Remove a handler registration from a content part.
        /// </summary>
        /// <typeparam name="TContentPartHandler"></typeparam>
        public static ContentPartOptionBuilder RemoveHandler<TContentPartHandler>(this ContentPartOptionBuilder builder)
            where TContentPartHandler : class, IContentPartHandler
        {
            return builder.RemoveHandler(typeof(TContentPartHandler));
        }

        /// <summary>
        /// Remove a handler registration from a content part.
        /// </summary>
        public static ContentPartOptionBuilder RemoveHandler(this ContentPartOptionBuilder builder, Type handlerType)
        {
            builder.Services.RemoveAll(handlerType);
            builder.Services.Configure<ContentOptions>(o =>
            {
                o.RemoveHandler(builder.ContentPartType, handlerType);
            });

            return builder;
        }
    }
}
