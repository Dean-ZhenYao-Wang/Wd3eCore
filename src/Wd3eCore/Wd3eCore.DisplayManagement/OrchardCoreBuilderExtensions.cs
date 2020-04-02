using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Wd3eCore.DisplayManagement;
using Wd3eCore.DisplayManagement.Descriptors;
using Wd3eCore.DisplayManagement.Descriptors.ShapeAttributeStrategy;
using Wd3eCore.DisplayManagement.Descriptors.ShapePlacementStrategy;
using Wd3eCore.DisplayManagement.Descriptors.ShapeTemplateStrategy;
using Wd3eCore.DisplayManagement.Events;
using Wd3eCore.DisplayManagement.Extensions;
using Wd3eCore.DisplayManagement.Implementation;
using Wd3eCore.DisplayManagement.Layout;
using Wd3eCore.DisplayManagement.LocationExpander;
using Wd3eCore.DisplayManagement.ModelBinding;
using Wd3eCore.DisplayManagement.Notify;
using Wd3eCore.DisplayManagement.Razor;
using Wd3eCore.DisplayManagement.Shapes;
using Wd3eCore.DisplayManagement.TagHelpers;
using Wd3eCore.DisplayManagement.Theming;
using Wd3eCore.DisplayManagement.Title;
using Wd3eCore.DisplayManagement.Zones;
using Wd3eCore.Environment.Extensions;
using Wd3eCore.Environment.Extensions.Features;
using Wd3eCore.Mvc.LocationExpander;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Wd3eCoreBuilderExtensions
    {
        /// <summary>
        /// Adds host and tenant level services for managing themes.
        /// </summary>
        public static Wd3eCoreBuilder AddTheming(this Wd3eCoreBuilder builder)
        {
            builder.AddThemingHost()
                .ConfigureServices(services =>
                {
                    services.Configure<MvcOptions>((options) =>
                    {
                        options.Filters.Add(typeof(ModelBinderAccessorFilter));
                        options.Filters.Add(typeof(NotifyFilter));
                        options.Filters.Add(typeof(RazorViewActionFilter));
                    });

                    // Used as a service when we create a fake 'ActionContext'.
                    services.AddScoped<IAsyncViewActionFilter, RazorViewActionFilter>();

                    services.AddScoped<IUpdateModelAccessor, LocalModelBinderAccessor>();
                    services.AddScoped<ViewContextAccessor>();

                    services.AddScoped<IShapeTemplateViewEngine, RazorShapeTemplateViewEngine>();
                    services.AddSingleton<IApplicationFeatureProvider<ViewsFeature>, ThemingViewsFeatureProvider>();
                    services.AddScoped<IViewLocationExpanderProvider, ThemeViewLocationExpanderProvider>();

                    services.AddScoped<IShapeTemplateHarvester, BasicShapeTemplateHarvester>();
                    services.AddTransient<IShapeTableManager, DefaultShapeTableManager>();

                    services.AddScoped<IShapeTableProvider, ShapeAttributeBindingStrategy>();
                    services.AddScoped<IShapeTableProvider, ShapePlacementParsingStrategy>();
                    services.AddScoped<IShapeTableProvider, ShapeTemplateBindingStrategy>();

                    services.AddScoped<IPlacementNodeFilterProvider, PathPlacementNodeFilterProvider>();

                    services.TryAddEnumerable(
                        ServiceDescriptor.Transient<IConfigureOptions<ShapeTemplateOptions>, ShapeTemplateOptionsSetup>());
                    services.TryAddSingleton<IShapeTemplateFileProviderAccessor, ShapeTemplateFileProviderAccessor>();

                    services.AddShapeAttributes<CoreShapes>();
                    services.AddScoped<IShapeTableProvider, CoreShapesTableProvider>();
                    services.AddShapeAttributes<ZoneShapes>();

                    services.AddScoped<IHtmlDisplay, DefaultHtmlDisplay>();
                    services.AddScoped<ILayoutAccessor, LayoutAccessor>();
                    services.AddScoped<IThemeManager, ThemeManager>();
                    services.AddScoped<IPageTitleBuilder, PageTitleBuilder>();

                    services.AddScoped<IShapeFactory, DefaultShapeFactory>();
                    services.AddScoped<IDisplayHelper, DisplayHelper>();

                    services.AddScoped<INotifier, Notifier>();

                    services.AddShapeAttributes<DateTimeShapes>();
                    services.AddShapeAttributes<PageTitleShapes>();

                    services.AddTagHelpers<AddAlternateTagHelper>();
                    services.AddTagHelpers<AddClassTagHelper>();
                    services.AddTagHelpers<AddWrapperTagHelper>();
                    services.AddTagHelpers<ClearAlternatesTagHelper>();
                    services.AddTagHelpers<ClearClassesTagHelper>();
                    services.AddTagHelpers<ClearWrappersTagHelper>();
                    services.AddTagHelpers<InputIsDisabledTagHelper>();
                    services.AddTagHelpers<RemoveAlternateTagHelper>();
                    services.AddTagHelpers<RemoveClassTagHelper>();
                    services.AddTagHelpers<RemoveWrapperTagHelper>();
                    services.AddTagHelpers<ShapeMetadataTagHelper>();
                    services.AddTagHelpers<ShapeTagHelper>();
                    services.AddTagHelpers<ValidationMessageTagHelper>();
                    services.AddTagHelpers<ZoneTagHelper>();
                });

            return builder;
        }

        /// <summary>
        /// Adds host level services for managing themes.
        /// </summary>
        public static Wd3eCoreBuilder AddThemingHost(this Wd3eCoreBuilder builder)
        {
            var services = builder.ApplicationServices;

            services.AddSingleton<IExtensionDependencyStrategy, ThemeExtensionDependencyStrategy>();
            services.AddSingleton<IFeatureBuilderEvents, ThemeFeatureBuilderEvents>();

            return builder;
        }
    }
}
