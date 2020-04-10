using System;

namespace Wd3eCore.Environment.Shell.Scope
{
    public class ShellScopeServices : IServiceProvider
    {
        private readonly IServiceProvider _services;

        /// <summary>
        /// 使“IServiceProvider”知道当前的“ShellScope”。
        /// </summary>
        public ShellScopeServices(IServiceProvider services) => _services = services;

        private IServiceProvider Services => ShellScope.Services ?? _services;

        public object GetService(Type serviceType) => Services?.GetService(serviceType);
    }
}
