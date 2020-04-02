using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Primitives;
using Wd3eCore.Apis.GraphQL;
using Wd3eCore.Apis.GraphQL.Resolvers;

namespace Wd3eCore.Localization.GraphQL
{
    /// <summary>
    /// Represents a site cultures for Graph QL.
    /// </summary>
    public class SiteCulturesQuery : ISchemaBuilder
    {
        private readonly IStringLocalizer S;

        /// <summary>
        /// Creates a new instance of the <see cref="SiteCulturesQuery"/>.
        /// </summary>
        /// <param name="localizer">The <see cref="IStringLocalizer"/>.</param>
        public SiteCulturesQuery(IStringLocalizer<SiteCulturesQuery> localizer)
        {
            S = localizer;
        }

        /// <inheritdocs/>
        public Task<IChangeToken> BuildAsync(ISchema schema)
        {
            var field = new FieldType
            {
                Name = "SiteCultures",
                Description = S["The active cultures configured for the site."],
                Type = typeof(ListGraphType<CultureQueryObjectType>),
                Resolver = new LockedAsyncFieldResolver<IEnumerable<SiteCulture>>(ResolveAsync)
            };

            schema.Query.AddField(field);

            return Task.FromResult<IChangeToken>(null);
        }

        private async Task<IEnumerable<SiteCulture>> ResolveAsync(ResolveFieldContext resolveContext)
        {
            var localizationService = resolveContext.ResolveServiceProvider().GetService<ILocalizationService>();

            var defaultCulture = await localizationService.GetDefaultCultureAsync();
            var supportedCultures = await localizationService.GetSupportedCulturesAsync();

            var cultures = supportedCultures.Select(culture =>
               new SiteCulture
               {
                   Culture = culture,
                   IsDefault = string.Equals(defaultCulture, culture, StringComparison.OrdinalIgnoreCase)
               }
           );

            return cultures;
        }
    }
}
