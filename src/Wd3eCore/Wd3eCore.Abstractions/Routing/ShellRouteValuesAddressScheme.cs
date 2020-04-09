using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Wd3eCore.Routing
{
    /// <summary>
    /// 允许租户添加自己的“RouteValuesAddress”方案，用于生成链接。
    /// </summary>
    public sealed class ShellRouteValuesAddressScheme : IEndpointAddressScheme<RouteValuesAddress>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEnumerable<IShellRouteValuesAddressScheme> _schemes;

        private bool _defaultSchemeInitialized;
        private IEndpointAddressScheme<RouteValuesAddress> _defaultScheme;

        public ShellRouteValuesAddressScheme(IHttpContextAccessor httpContextAccessor, IEnumerable<IShellRouteValuesAddressScheme> schemes)
        {
            _httpContextAccessor = httpContextAccessor;
            _schemes = schemes;
        }

        public IEnumerable<Endpoint> FindEndpoints(RouteValuesAddress address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            // 运行自定义租户方案。
            foreach (var scheme in _schemes)
            {
                var endpoints = scheme.FindEndpoints(address);

                if (endpoints.Any())
                {
                    return endpoints;
                }
            }

            if (!_defaultSchemeInitialized)
            {
                lock (this)
                {
                    _defaultScheme = _httpContextAccessor.HttpContext?.RequestServices
                        .GetServices<IEndpointAddressScheme<RouteValuesAddress>>()
                        .Where(scheme => scheme.GetType() != GetType())
                        .LastOrDefault();

                    _defaultSchemeInitialized = true;
                }
            }

            // 退回到默认的“RouteValuesAddress”方案。
            return _defaultScheme?.FindEndpoints(address) ?? Enumerable.Empty<Endpoint>();
        }
    }
}
