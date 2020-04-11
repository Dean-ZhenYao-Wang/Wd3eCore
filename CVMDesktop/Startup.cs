using System;
using System.Collections.Generic;
using System.Text;
using Wd3eCore.CVMDesktop.Controllers;
using Wd3eCore.CVMDesktop.dbModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Wd3eCore.Data.Migration;
using Wd3eCore.Modules;
using Wd3eCore.Mvc.Core.Utilities;
using YesSql;
using YesSql.Indexes;
using Wd3eCore.Data;
using Wd3eCore.Environment.Shell;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IO;

namespace Wd3eCore.CVMDesktop
{
    public class Startup : StartupBase
    {
        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            var accountControllerName = typeof(HomeController).ControllerName();

            routes.MapAreaControllerRoute(
                name: "CVMDesktop",
                areaName: "Wd3eCore.CVMDesktop",
                pattern: "CVMDesktop",
                defaults: new { controller = accountControllerName, action = nameof(HomeController.Index) }
            );
            base.Configure(app, routes, serviceProvider);
        }
        public override void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IIndexProvider, SwaggerUiProvider>();
            services.AddScoped<IDataMigration, Migratons>();
            services.AddDbContext<CVMDesktop_Context>(options =>
            {
                IServiceProvider serviceProvider = services.BuildServiceProvider();

                var shellSettings = serviceProvider.GetService<ShellSettings>();
                switch (shellSettings["DatabaseProvider"])
                {
                    case "SqlConnection":
                        options.UseSqlServer(shellSettings["ConnectionString"]);
                        break;
                    case "Sqlite":
                        var shellOptions = serviceProvider.GetService<IOptions<ShellOptions>>();
                        var option = shellOptions.Value;
                        var databaseFolder = Path.Combine(option.ShellsApplicationDataPath, option.ShellsContainerName, shellSettings.Name);
                        var databaseFile = Path.Combine(databaseFolder, "yessql.db");
                        Directory.CreateDirectory(databaseFolder);
                        options.UseSqlite($"Data Source={databaseFile};Cache=Shared");
                        break;
                    case "MySql":
                        options.UseMySQL(shellSettings["ConnectionString"]);
                        break;
                    case "Postgres":
                        options.UseNpgsql(shellSettings["ConnectionString"]);
                        break;
                    default:
                        throw new ArgumentException("Unknown database provider: " + shellSettings["DatabaseProvider"]);
                }
            });
        }

    }

}
