using Microsoft.AspNetCore.Routing;

namespace Wd3eCore.Routing
{
    /// <summary>
    /// 标记接口，用于检索用于链接生成的租户 "RouteValuesAddress "方案。
    /// </summary>
    public interface IShellRouteValuesAddressScheme : IEndpointAddressScheme<RouteValuesAddress>
    {
    }
}
