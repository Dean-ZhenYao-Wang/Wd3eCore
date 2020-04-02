using System;
using Fluid;

namespace Wd3eCore.Liquid
{
    public class LiquidTemplateContext : TemplateContext
    {
        public LiquidTemplateContext(IServiceProvider services)
        {
            Services = services;
        }

        public IServiceProvider Services { get; }

        public bool IsInitialized { get; set; }
    }
}
