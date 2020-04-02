using Microsoft.AspNetCore.Http;

namespace Wd3eCore.Modules
{
    public class DefaultWd3eHelper : IWd3eHelper
    {
        public DefaultWd3eHelper(IHttpContextAccessor httpContextAccessor)
        {
            HttpContext = httpContextAccessor.HttpContext;
        }

        public HttpContext HttpContext { get; set; }
    }
}
