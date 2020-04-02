using System;
using System.Collections.Generic;

namespace Wd3eCore.Liquid
{
    public class LiquidOptions
    {
        public Dictionary<string, Type> FilterRegistrations { get; } = new Dictionary<string, Type>();
    }
}
