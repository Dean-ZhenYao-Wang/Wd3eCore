using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wd3eCore.Environment.Shell.Scope;
using Wd3eCore.Modules;

namespace Wd3eCore.Mvc
{
    public class ShellViewFeatureProvider : IApplicationFeatureProvider<ViewsFeature>, IApplicationFeatureProvider<DevelopmentViewsFeature>
    {
        private readonly IHostEnvironment _hostingEnvironment;
        private readonly IApplicationContext _applicationContext;

        private ApplicationPartManager _applicationPartManager;
        private IEnumerable<IApplicationFeatureProvider<ViewsFeature>> _featureProviders;

        public ShellViewFeatureProvider(IServiceProvider services)
        {
            _hostingEnvironment = services.GetRequiredService<IHostEnvironment>();
            _applicationContext = services.GetRequiredService<IApplicationContext>();
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ViewsFeature feature)
        {
            EnsureScopedServices();

            // Module compiled views are only served if not in dev mode or if the 'refs' folder doesn't exists.
            var refsFolderExists = Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "refs"));

            if (!_hostingEnvironment.IsDevelopment() || !refsFolderExists)
            {
                PopulateFeatureInternal(parts, feature);
            }

            // Apply views feature providers registered at the tenant level.
            foreach (var provider in _featureProviders)
            {
                provider.PopulateFeature(parts, feature);
            }
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, DevelopmentViewsFeature developmentViewsFeature)
        {
            EnsureScopedServices();

            // Module compiled views are only served if not in dev mode or if the 'refs' folder doesn't exists.
            var refsFolderExists = Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "refs"));

            // But in dev mode we still provide all view descriptors.
            if (_hostingEnvironment.IsDevelopment() && refsFolderExists)
            {
                var viewsFeature = new ViewsFeature();
                PopulateFeatureInternal(parts, viewsFeature);

                // Apply views feature providers registered at the tenant level.
                foreach (var provider in _featureProviders)
                {
                    provider.PopulateFeature(parts, viewsFeature);
                }

                foreach (var descriptor in viewsFeature.ViewDescriptors)
                {
                    developmentViewsFeature.ViewDescriptors.Add(descriptor);
                }
            }
        }

        private void PopulateFeatureInternal(IEnumerable<ApplicationPart> parts, ViewsFeature feature)
        {
            // Retrieve mvc views feature providers but not this one.
            var mvcFeatureProviders = _applicationPartManager.FeatureProviders
                .OfType<IApplicationFeatureProvider<ViewsFeature>>()
                .Where(p => p.GetType() != typeof(ShellViewFeatureProvider));

            var modules = _applicationContext.Application.Modules;
            var moduleFeature = new ViewsFeature();

            foreach (var module in modules)
            {
                var precompiledAssemblyPath = Path.Combine(Path.GetDirectoryName(module.Assembly.Location),
                    module.Assembly.GetName().Name + ".Views.dll");

                if (File.Exists(precompiledAssemblyPath))
                {
                    try
                    {
                        var assembly = Assembly.LoadFile(precompiledAssemblyPath);

                        var applicationPart = new ApplicationPart[] { new CompiledRazorAssemblyPart(assembly) };

                        foreach (var provider in mvcFeatureProviders)
                        {
                            provider.PopulateFeature(applicationPart, moduleFeature);
                        }

                        // Razor views are precompiled in the context of their modules, but at runtime
                        // their paths need to be relative to the virtual "Areas/{ModuleId}" folders.
                        // Note: For the app's module this folder is "Areas/{env.ApplicationName}".
                        foreach (var descriptor in moduleFeature.ViewDescriptors)
                        {
                            descriptor.RelativePath = '/' + module.SubPath + descriptor.RelativePath;
                            feature.ViewDescriptors.Add(descriptor);
                        }

                        // For the app's module we still allow to explicitly specify view paths relative to the app content root.
                        // So for the application's module we re-apply the feature providers without updating the relative paths.
                        // Note: This is only needed in prod mode if app's views are precompiled and views files no longer exist.
                        if (module.Name == _hostingEnvironment.ApplicationName)
                        {
                            foreach (var provider in mvcFeatureProviders)
                            {
                                provider.PopulateFeature(applicationPart, moduleFeature);
                            }

                            foreach (var descriptor in moduleFeature.ViewDescriptors)
                            {
                                feature.ViewDescriptors.Add(descriptor);
                            }
                        }

                        moduleFeature.ViewDescriptors.Clear();
                    }
                    catch (FileLoadException)
                    {
                        // Don't throw if assembly cannot be loaded. This can happen if the file is not a managed assembly.
                    }
                }
            }
        }

        private void EnsureScopedServices()
        {
            var services = ShellScope.Services;

            // The scope is null when this code is called through a 'ChangeToken' callback, e.g to recompile razor pages.
            // So, here we resolve and cache tenant level singletons, application singletons can be resolved in the ctor.

            if (services != null && _featureProviders == null)
            {
                lock (this)
                {
                    if (_featureProviders == null)
                    {
                        _applicationPartManager = services.GetRequiredService<ApplicationPartManager>();
                        _featureProviders = services.GetServices<IApplicationFeatureProvider<ViewsFeature>>();
                    }
                }
            }
        }
    }
}
