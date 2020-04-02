using System;
using Microsoft.Extensions.DependencyInjection;

namespace Wd3eCore.ContentManagement
{
    public class ContentFieldOptionBuilder
    {
        public ContentFieldOptionBuilder(IServiceCollection services, Type contentFieldType)
        {
            Services = services;
            ContentFieldType = contentFieldType;
        }

        public IServiceCollection Services { get; }
        public Type ContentFieldType { get; }
    }
}
