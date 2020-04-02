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
    public class DatabaseShellsSettingsSources : IShellsSettingsSources
    {
        private readonly DatabaseShellsStorageOptions _options;
        private readonly IShellContextFactory _shellContextFactory;
        private readonly string _tenants;

        public DatabaseShellsSettingsSources(
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

            _tenants = Path.Combine(shellOptions.Value.ShellsApplicationDataPath, "tenants.json");
        }

        public async Task AddSourcesAsync(IConfigurationBuilder builder)
        {
            DatabaseShellsSettings document;

            using var context = await _shellContextFactory.GetDatabaseContextAsync(_options);
            using (var scope = context.ServiceProvider.CreateScope())
            {
                var session = scope.ServiceProvider.GetRequiredService<ISession>();

                document = await session.Query<DatabaseShellsSettings>().FirstOrDefaultAsync();

                if (document == null)
                {
                    document = new DatabaseShellsSettings();

                    if (!_options.MigrateFromFiles || !await TryMigrateFromFileAsync(document))
                    {
                        return;
                    }

                    session.Save(document, checkConcurrency: true);
                }
            }

            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(document.ShellsSettings.ToString(Formatting.None))));
        }

        public async Task SaveAsync(string tenant, IDictionary<string, string> data)
        {
            using var context = await _shellContextFactory.GetDatabaseContextAsync(_options);
            using (var scope = context.ServiceProvider.CreateScope())
            {
                var session = scope.ServiceProvider.GetRequiredService<ISession>();

                var document = await session.Query<DatabaseShellsSettings>().FirstOrDefaultAsync();

                JObject tenantsSettings;
                if (document != null)
                {
                    tenantsSettings = document.ShellsSettings;
                }
                else
                {
                    document = new DatabaseShellsSettings();
                    tenantsSettings = new JObject();
                }

                var settings = tenantsSettings.GetValue(tenant) as JObject ?? new JObject();

                foreach (var key in data.Keys)
                {
                    if (data[key] != null)
                    {
                        settings[key] = data[key];
                    }
                    else
                    {
                        settings.Remove(key);
                    }
                }

                tenantsSettings[tenant] = settings;

                document.ShellsSettings = tenantsSettings;

                session.Save(document, checkConcurrency: true);
            }
        }

        private async Task<bool> TryMigrateFromFileAsync(DatabaseShellsSettings document)
        {
            if (!File.Exists(_tenants))
            {
                return false;
            }

            using (var file = File.OpenText(_tenants))
            {
                var settings = await file.ReadToEndAsync();
                document.ShellsSettings = JObject.Parse(settings);
            }

            return true;
        }
    }
}
