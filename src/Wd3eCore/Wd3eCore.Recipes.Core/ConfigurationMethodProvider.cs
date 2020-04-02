using System;
using System.Collections.Generic;
using Wd3eCore.Environment.Shell.Configuration;
using Wd3eCore.Scripting;

namespace Wd3eCore.Recipes
{
    public class ConfigurationMethodProvider : IGlobalMethodProvider
    {
        private readonly GlobalMethod _globalMethod;

        public ConfigurationMethodProvider(IShellConfiguration configuration)
        {
            _globalMethod = new GlobalMethod
            {
                Name = "configuration",
                Method = serviceprovider => (Func<string, object>)(name => configuration[name])
            };
        }

        public IEnumerable<GlobalMethod> GetMethods()
        {
            yield return _globalMethod;
        }
    }
}
