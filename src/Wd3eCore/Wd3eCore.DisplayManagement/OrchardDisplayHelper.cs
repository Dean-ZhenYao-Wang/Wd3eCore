using Microsoft.AspNetCore.Http;

namespace Wd3eCore.DisplayManagement.Razor
{
    public interface IWd3eDisplayHelper : IWd3eHelper
    {
        IDisplayHelper DisplayHelper { get; }
    }

    internal class Wd3eDisplayHelper : IWd3eDisplayHelper
    {
        public Wd3eDisplayHelper(HttpContext context, IDisplayHelper displayHelper)
        {
            HttpContext = context;
            DisplayHelper = displayHelper;
        }

        public HttpContext HttpContext { get; set; }
        public IDisplayHelper DisplayHelper { get; set; }
    }
}
