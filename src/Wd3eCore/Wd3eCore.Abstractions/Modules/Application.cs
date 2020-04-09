using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Hosting;

namespace Wd3eCore.Modules
{
    public class Application
    {
        private readonly Dictionary<string, Module> _modulesByName;
        private readonly List<Module> _modules;

        public const string ModulesPath = "Areas";
        public const string ModulesRoot = ModulesPath + "/";

        public const string ModuleName = "应用程序的主要特征";
        public const string ModuleDescription = "提供在应用程序级别定义的组件。";
        public static readonly string ModulePriority = int.MinValue.ToString();
        public const string ModuleCategory = "Application";

        public const string DefaultFeatureId = "Application.Default";
        public const string DefaultFeatureName = "应用程序的默认功能";
        public const string DefaultFeatureDescription = "向应用程序模块添加默认特性。";

        public Application(IHostEnvironment environment, IEnumerable<Module> modules)
        {
            Name = environment.ApplicationName;
            Path = environment.ContentRootPath;
            Root = Path + '/';
            ModulePath = ModulesRoot + Name;
            ModuleRoot = ModulePath + '/';

            Assembly = Assembly.Load(new AssemblyName(Name));

            _modules = new List<Module>(modules);
            _modulesByName = _modules.ToDictionary(m => m.Name, m => m);
        }

        public string Name { get; }
        public string Path { get; }
        public string Root { get; }
        public string ModulePath { get; }
        public string ModuleRoot { get; }
        public Assembly Assembly { get; }
        public IEnumerable<Module> Modules => _modules;

        public Module GetModule(string name)
        {
            if (!_modulesByName.TryGetValue(name, out var module))
            {
                return new Module(string.Empty);
            }

            return module;
        }
    }
}
