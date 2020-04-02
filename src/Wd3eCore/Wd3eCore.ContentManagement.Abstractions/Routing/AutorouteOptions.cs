using Microsoft.AspNetCore.Routing;

namespace Wd3eCore.ContentManagement.Routing
{
    public class AutorouteOptions
    {
        public RouteValueDictionary GlobalRouteValues { get; set; } = new RouteValueDictionary();
        public string ContentItemIdKey { get; set; } = "";
    }
}
