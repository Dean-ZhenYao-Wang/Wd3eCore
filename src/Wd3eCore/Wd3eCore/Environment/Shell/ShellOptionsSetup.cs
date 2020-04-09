using System;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Wd3eCore.Environment.Shell
{
    /// <summary>
    /// 为 <see cref="ShellOptions"/>设置默认选项
    /// </summary>
    public class ShellOptionsSetup : IConfigureOptions<ShellOptions>
    {
        private const string Wd3eAppData = "Wd3e_APP_DATA";
        private const string DefaultAppDataPath = "App_Data";
        private const string DefaultSitesPath = "Sites";

        private readonly IHostEnvironment _hostingEnvironment;

        public ShellOptionsSetup(IHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void Configure(ShellOptions options)
        {
            var appData = System.Environment.GetEnvironmentVariable(Wd3eAppData);

            if (!String.IsNullOrEmpty(appData))
            {
                options.ShellsApplicationDataPath = Path.Combine(_hostingEnvironment.ContentRootPath, appData);
            }
            else
            {
                options.ShellsApplicationDataPath = Path.Combine(_hostingEnvironment.ContentRootPath, DefaultAppDataPath);
            }

            options.ShellsContainerName = DefaultSitesPath;
        }
    }
}
