using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.Extensions.DependencyInjection;

namespace Wd3eCore.Mvc
{
    /// <summary>
    /// Shares across tenants the same <see cref="IViewCompiler"/>.
    /// </summary>
    public class SharedViewCompilerProvider : IViewCompilerProvider
    {
        private object _synLock = new object();
        private static IViewCompiler _compiler;
        private readonly IServiceProvider _services;

        public SharedViewCompilerProvider(IServiceProvider services)
        {
            _services = services;
        }

        public IViewCompiler GetCompiler()
        {
            if (_compiler != null)
            {
                return _compiler;
            }

            lock (_synLock)
            {
                _compiler = _services
                    .GetServices<IViewCompilerProvider>()
                    .FirstOrDefault()
                    .GetCompiler();
            }

            return _compiler;
        }
    }
}
