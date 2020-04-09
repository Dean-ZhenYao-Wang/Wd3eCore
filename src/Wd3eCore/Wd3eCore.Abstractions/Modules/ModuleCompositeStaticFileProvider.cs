using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;
using Wd3eCore.Modules.FileProviders;

namespace Wd3eCore.Modules
{
    /// <summary>
    /// 这个自定义<see cref="IFileProvider"/>实现为IStaticFileProviders提供Di注册标识，该标识应该通过UseStaticFiles提供。
    /// </summary>
    public class ModuleCompositeStaticFileProvider : CompositeFileProvider, IModuleStaticFileProvider
    {
        public ModuleCompositeStaticFileProvider(params IStaticFileProvider[] fileProviders) : base(fileProviders)
        {
        }
        public ModuleCompositeStaticFileProvider(IEnumerable<IStaticFileProvider> fileProviders) : base(fileProviders)
        {
        }
    }
}
