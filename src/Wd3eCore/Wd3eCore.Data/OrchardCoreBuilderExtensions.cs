using System;
using System.Data;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Options;
using Wd3eCore.Data;
using Wd3eCore.Data.Migration;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Environment.Shell.Models;
using Wd3eCore.Environment.Shell.Scope;
using Wd3eCore.Modules;
using YesSql;
using YesSql.Indexes;
using YesSql.Provider.MySql;
using YesSql.Provider.PostgreSql;
using YesSql.Provider.Sqlite;
using YesSql.Provider.SqlServer;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Provides extension methods for <see cref="Wd3eCoreBuilder"/> to add database access functionality.
    /// </summary>
    public static class Wd3eCoreBuilderExtensions
    {
        /// <summary>
        /// Adds tenant level data access services.
        /// </summary>
        /// <param name="builder">The <see cref="Wd3eCoreBuilder"/>.</param>
        public static Wd3eCoreBuilder AddDataAccess(this Wd3eCoreBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IDataMigrationManager, DataMigrationManager>();
                services.AddScoped<IModularTenantEvents, AutomaticDataMigrations>();

                // Adding supported databases
                services.TryAddDataProvider(name: "Sql Server", value: "SqlConnection", hasConnectionString: true, sampleConnectionString: "Server=localhost;Database=Wd3e;User Id=username;Password=password", hasTablePrefix: true, isDefault: false);
                services.TryAddDataProvider(name: "Sqlite", value: "Sqlite", hasConnectionString: false, hasTablePrefix: false, isDefault: true);
                services.TryAddDataProvider(name: "MySql", value: "MySql", hasConnectionString: true, sampleConnectionString: "Server=localhost;Database=Wd3e;Uid=username;Pwd=password", hasTablePrefix: true, isDefault: false);
                services.TryAddDataProvider(name: "Postgres", value: "Postgres", hasConnectionString: true, sampleConnectionString: "Server=localhost;Port=5432;Database=Wd3e;User Id=username;Password=password", hasTablePrefix: true, isDefault: false);

                // Configuring data access

                services.AddSingleton<IStore>(sp =>
                {
                    var shellSettings = sp.GetService<ShellSettings>();

                    // Before the setup a 'DatabaseProvider' may be configured without a required 'ConnectionString'.
                    if (shellSettings.State == TenantState.Uninitialized || shellSettings["DatabaseProvider"] == null)
                    {
                        return null;
                    }

                    IConfiguration storeConfiguration = new YesSql.Configuration();

                    switch (shellSettings["DatabaseProvider"])
                    {
                        case "SqlConnection":
                            storeConfiguration
                                .UseSqlServer(shellSettings["ConnectionString"], IsolationLevel.ReadUncommitted)
                                .UseBlockIdGenerator();
                            break;
                        case "Sqlite":
                            var shellOptions = sp.GetService<IOptions<ShellOptions>>();
                            var option = shellOptions.Value;
                            var databaseFolder = Path.Combine(option.ShellsApplicationDataPath, option.ShellsContainerName, shellSettings.Name);
                            var databaseFile = Path.Combine(databaseFolder, "yessql.db");
                            Directory.CreateDirectory(databaseFolder);
                            storeConfiguration
                                .UseSqLite($"Data Source={databaseFile};Cache=Shared", IsolationLevel.ReadUncommitted)
                                .UseDefaultIdGenerator();
                            break;
                        case "MySql":
                            storeConfiguration
                                .UseMySql(shellSettings["ConnectionString"], IsolationLevel.ReadUncommitted)
                                .UseBlockIdGenerator();
                            break;
                        case "Postgres":
                            storeConfiguration
                                .UsePostgreSql(shellSettings["ConnectionString"], IsolationLevel.ReadUncommitted)
                                .UseBlockIdGenerator();
                            break;
                        default:
                            throw new ArgumentException("Unknown database provider: " + shellSettings["DatabaseProvider"]);
                    }

                    if (!string.IsNullOrWhiteSpace(shellSettings["TablePrefix"]))
                    {
                        storeConfiguration = storeConfiguration.SetTablePrefix(shellSettings["TablePrefix"] + "_");
                    }

                    var store = StoreFactory.CreateAsync(storeConfiguration).GetAwaiter().GetResult();
                    var indexes = sp.GetServices<IIndexProvider>();

                    store.RegisterIndexes(indexes);

                    return store;
                });

                services.AddScoped(sp =>
                {
                    var store = sp.GetService<IStore>();

                    if (store == null)
                    {
                        return null;
                    }

                    var session = store.CreateSession();

                    var scopedServices = sp.GetServices<IScopedIndexProvider>();

                    session.RegisterIndexes(scopedServices.ToArray());

                    ShellScope.RegisterBeforeDispose(scope =>
                    {
                        return session.CommitAsync();
                    });

                    return session;
                });

                services.AddScoped<ISessionHelper, SessionHelper>();

                services.AddTransient<IDbConnectionAccessor, DbConnectionAccessor>();
            });

            return builder;
        }
    }
}
