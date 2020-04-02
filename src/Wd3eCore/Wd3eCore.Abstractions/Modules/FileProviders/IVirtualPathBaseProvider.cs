using Microsoft.AspNetCore.Http;

namespace Wd3eCore.Modules.FileProviders
{
    public interface IVirtualPathBaseProvider
    {
        PathString VirtualPathBase { get; }
    }
}
