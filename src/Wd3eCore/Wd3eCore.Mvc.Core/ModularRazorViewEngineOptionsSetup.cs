using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using Wd3eCore.Mvc.LocationExpander;

namespace Wd3eCore.Mvc
{
    public class ModularRazorViewEngineOptionsSetup : IConfigureOptions<RazorViewEngineOptions>
    {
        public ModularRazorViewEngineOptionsSetup()
        {
        }

        public void Configure(RazorViewEngineOptions options)
        {
            options.ViewLocationExpanders.Add(new CompositeViewLocationExpanderProvider());
        }
    }
}
