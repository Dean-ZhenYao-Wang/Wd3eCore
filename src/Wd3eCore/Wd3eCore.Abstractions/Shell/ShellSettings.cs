using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Wd3eCore.Environment.Shell.Configuration;
using Wd3eCore.Environment.Shell.Models;

namespace Wd3eCore.Environment.Shell
{
    /// <summary>
    /// 代表每个租户存储的最小化字段集。
    /// 这个模型从'IShellSettingsManager'中获得，
    /// 默认情况下，它从'App_Data/tenants.json'文件中读取。
    /// </summary>
    public class ShellSettings
    {
        private ShellConfiguration _settings;
        private ShellConfiguration _configuration;

        public ShellSettings()
        {
            _settings = new ShellConfiguration();
            _configuration = new ShellConfiguration();
        }

        public ShellSettings(ShellConfiguration settings, ShellConfiguration configuration)
        {
            _settings = settings;
            _configuration = configuration;
        }

        public ShellSettings(ShellSettings settings)
        {
            _settings = new ShellConfiguration(settings._settings);
            _configuration = new ShellConfiguration(settings.Name, settings._configuration);
            Name = settings.Name;
        }

        public string Description
        {
            get => _settings["Description"];
            set => _settings["Description"] = value;
        }

        public string Name { get; set; }

        public string RequestUrlHost
        {
            get => _settings["RequestUrlHost"];
            set => _settings["RequestUrlHost"] = value;
        }

        public string RequestUrlPrefix
        {
            get => _settings["RequestUrlPrefix"]?.Trim(' ', '/');
            set => _settings["RequestUrlPrefix"] = value;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public TenantState State
        {
            get => _settings.GetValue<TenantState>("State");
            set => _settings["State"] = value.ToString();
        }

        [JsonIgnore]
        public IShellConfiguration ShellConfiguration => _configuration;

        [JsonIgnore]
        public string this[string key]
        {
            get => _configuration[key];
            set => _configuration[key] = value;
        }

        public Task EnsureConfigurationAsync() => _configuration.EnsureConfigurationAsync();
    }
}
