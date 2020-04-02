using Microsoft.Extensions.FileProviders;
using Wd3eCore.Modules.FileProviders;

namespace Wd3eCore.Modules
{
    /// <summary>
    /// This custom <see cref="IFileProvider"/> implementation provides Di registration identification
    /// for IStaticFileProviders that should be served via UseStaticFiles.
    /// </summary>
    public interface IModuleStaticFileProvider : IStaticFileProvider
    {
    }
}
