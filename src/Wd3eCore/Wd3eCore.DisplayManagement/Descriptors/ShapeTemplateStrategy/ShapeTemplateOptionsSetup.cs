using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Wd3eCore.Modules;
using Wd3eCore.Mvc;

namespace Wd3eCore.DisplayManagement.Descriptors.ShapeTemplateStrategy
{
    /// <summary>
    /// Sets up default options for <see cref="ShapeTemplateViewOptions"/>.
    /// </summary>
    public class ShapeTemplateOptionsSetup : IConfigureOptions<ShapeTemplateOptions>
    {
        private readonly IHostEnvironment _hostingEnvironment;
        private readonly IApplicationContext _applicationContext;

        /// <summary>
        /// Initializes a new instance of <see cref="ShapeTemplateViewOptions"/>.
        /// </summary>
        /// <param name="hostingEnvironment"><see cref="IHostEnvironment"/> for the application.</param>
        public ShapeTemplateOptionsSetup(IHostEnvironment hostingEnvironment, IApplicationContext applicationContext)
        {
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
            _applicationContext = applicationContext;
        }

        public void Configure(ShapeTemplateOptions options)
        {
            if (_hostingEnvironment.ContentRootFileProvider != null)
            {
                options.FileProviders.Add(_hostingEnvironment.ContentRootFileProvider);
            }

            // To let the application behave as a module, its views are requested under the virtual
            // "Areas" folder, but they are still served from the file system by this custom provider.
            options.FileProviders.Insert(0, new ApplicationViewFileProvider(_applicationContext));
        }
    }
}
