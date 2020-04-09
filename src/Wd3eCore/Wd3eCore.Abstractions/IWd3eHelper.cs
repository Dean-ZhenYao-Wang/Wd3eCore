using Microsoft.AspNetCore.Http;

namespace Wd3eCore
{
    public interface IWd3eHelper
    {
        HttpContext HttpContext { get; }
    }
}
