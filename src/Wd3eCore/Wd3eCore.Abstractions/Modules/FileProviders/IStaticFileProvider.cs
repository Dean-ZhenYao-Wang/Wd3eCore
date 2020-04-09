using Microsoft.Extensions.FileProviders;

namespace Wd3eCore.Modules.FileProviders
{
    /// <summary>
    /// 这个自定义<see cref="IFileProvider"/>实现为istaticfileprovider提供Di注册标识，该标识应该通过UseStaticFiles提供。
    /// </summary>
    public interface IStaticFileProvider : IFileProvider
    {
    }
}
