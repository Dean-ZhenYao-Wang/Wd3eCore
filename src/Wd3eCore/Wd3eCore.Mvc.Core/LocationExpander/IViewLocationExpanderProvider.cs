using Microsoft.AspNetCore.Mvc.Razor;

namespace Wd3eCore.Mvc.LocationExpander
{
    public interface IViewLocationExpanderProvider : IViewLocationExpander
    {
        int Priority { get; }
    }
}
