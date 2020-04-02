using System;
using System.Threading.Tasks;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Environment.Shell.Builders;
using Wd3eCore.Environment.Shell.Descriptor.Models;
using Wd3eCore.Environment.Shell.Models;
using Wd3eCore.Shells.Database.Configuration;

namespace Wd3eCore.Shells.Database.Extensions
{
    public static class DatabaseShellContextFactoryExtensions
    {
        internal static Task<ShellContext> GetDatabaseContextAsync(this IShellContextFactory shellContextFactory, DatabaseShellsStorageOptions options)
        {
            if (options.DatabaseProvider == null)
            {
                throw new ArgumentNullException(nameof(options.DatabaseProvider),
                    "The 'Wd3eCore.Shells.Database' configuration section should define a 'DatabaseProvider'");
            }

            var settings = new ShellSettings()
            {
                Name = ShellHelper.DefaultShellName,
                State = TenantState.Running
            };

            settings["DatabaseProvider"] = options.DatabaseProvider;
            settings["ConnectionString"] = options.ConnectionString;
            settings["TablePrefix"] = options.TablePrefix;

            return shellContextFactory.CreateDescribedContextAsync(settings, new ShellDescriptor());
        }
    }
}
