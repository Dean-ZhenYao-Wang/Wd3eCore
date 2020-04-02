using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Hosting;
using Wd3eCore.Modules.Manifest;

namespace Wd3eCore.Modules
{
    public class AssemblyAttributeModuleNamesProvider : IModuleNamesProvider
    {
        private readonly List<string> _moduleNames;

        public AssemblyAttributeModuleNamesProvider(IHostEnvironment hostingEnvironment)
        {
            var assembly = Assembly.Load(new AssemblyName(hostingEnvironment.ApplicationName));
            _moduleNames = assembly.GetCustomAttributes<ModuleNameAttribute>().Select(m => m.Name).ToList();
        }

        public IEnumerable<string> GetModuleNames()
        {
            return _moduleNames;
        }
    }
}
