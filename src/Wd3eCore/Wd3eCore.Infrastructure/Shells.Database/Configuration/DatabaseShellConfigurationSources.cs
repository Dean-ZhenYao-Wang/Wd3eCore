using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Environment.Shell.Builders;
using Wd3eCore.Environment.Shell.Configuration;
using Wd3eCore.Shells.Database.Extensions;
using Wd3eCore.Shells.Database.Models;
using YesSql;

namespace Wd3eCore.Shells.Database.Configuration
{
    public class DatabaseShellConfigurationSources : IShellConfigurationSources
    {
        private readonly DatabaseShellsStorageOptions _options;
        private readonly IShellContextFactory _shellContextFactory;
        private readonly string _container;

        public DatabaseShellConfigurationSources(
            Microsoft.Extensions.Configuration.IConfiguration configuration,
            IShellContextFactory shellContextFactory,
            IOptions<ShellOptions> shellOptions)

        {
            _options = configuration
                .GetSection("Wd3eCore")
                .GetSection("Wd3eCore.Shells.Database")
                .Get<DatabaseShellsStorageOptions>()
                ?? new DatabaseShellsStorageOptions();

            _shellContextFactory = shellContextFactory;

            _container = Path.Combine(shellOptions.Value.ShellsApplicationDataPath, shellOptions.Value.ShellsContainerName);
        }

        public async Task AddSourcesAsync(string tenant, IConfigurationBuilder builder)
        {
            JObject configurations;

            using var context = await _shellContextFactory.GetDatabaseContextAsync(_options);
            using (var scope = context.ServiceProvider.CreateScope())
            {
                var session = scope.ServiceProvider.GetRequiredService<ISession>();

                var document = await session.Query<DatabaseShellConfigurations>().FirstOrDefaultAsync();

                if (document != null)
                {
                    configurations = document.ShellConfigurations;
                }
                else
                {
                    document = new DatabaseShellConfigurations();
                    configurations = new JObject();
                }

                if (!configurations.ContainsKey(tenant))
                {
                    if (!_options.MigrateFromFiles || !await TryMigrateFromFileAsync(tenant, configurations))
                    {
                        return;
                    }

                    document.ShellConfigurations = configurations;

                    session.Save(document, checkConcurrency: true);
                }
            }

            var configuration = configurations.GetValue(tenant) as JObject;
            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(configuration.ToString(Formatting.None))));
        }

        public async Task SaveAsync(string tenant, IDictionary<string, string> data)
        {
            using var context = await _shellContextFactory.GetDatabaseContextAsync(_options);
            using (var scope = context.ServiceProvider.CreateScope())
            {
                var session = scope.ServiceProvider.GetRequiredService<ISession>();

                var document = await session.Query<DatabaseShellConfigurations>().FirstOrDefaultAsync();

                JObject configurations;
                if (document != null)
                {
                    configurations = document.ShellConfigurations;
                }
                else
                {
                    document = new DatabaseShellConfigurations();
                    configurations = new JObject();
                }

                var config = configurations.GetValue(tenant) as JObject ?? new JObject();

                foreach (var key in data.Keys)
                {
                    if (data[key] != null)
                    {
                        config[key] = data[key];
                    }
                    else
                    {
                        config.Remove(key);
                    }
                }

                configurations[tenant] = config;

                document.ShellConfigurations = configurations;

                session.Save(document, checkConcurrency: true);
            }
        }

        private async Task<bool> TryMigrateFromFileAsync(string tenant, JObject configurations)
        {
            var tenantFolder = Path.Combine(_container, tenant);
            var appsettings = Path.Combine(tenantFolder, "appsettings.json");

            if (!File.Exists(appsettings))
            {
                return false;
            }

            using (var file = File.OpenText(appsettings))
            {
                var configuration = await file.ReadToEndAsync();

                if (configuration != null)
                {
                    configurations[tenant] = JObject.Parse(configuration);
                }
            }

            return true;
        }
    }
}
