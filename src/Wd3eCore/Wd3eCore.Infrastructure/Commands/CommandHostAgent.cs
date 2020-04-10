using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Environment.Shell.Builders;
using Wd3eCore.Environment.Shell.Models;
using Wd3eCore.Modules;

namespace Wd3eCore.Environment.Commands
{
    public class CommandHostAgent
    {
        private readonly IShellHost _Wd3eHost;
        private readonly IShellSettingsManager _shellSettingsManager;
        private readonly IStringLocalizer S;

        public CommandHostAgent(IShellHost Wd3eHost,
            IShellSettingsManager shellSettingsManager,
            IStringLocalizer localizer)
        {
            _Wd3eHost = Wd3eHost;
            _shellSettingsManager = shellSettingsManager;

            S = localizer;
        }

        public Task<CommandReturnCodes> RunSingleCommandAsync(TextReader input, TextWriter output, string tenant, string[] args, IDictionary<string, string> switches)
        {
            return RunCommandAsync(input, output, tenant, args, switches);
        }

        public async Task<CommandReturnCodes> RunCommandAsync(TextReader input, TextWriter output, string tenant, string[] args, IDictionary<string, string> switches)
        {
            try
            {
                tenant = tenant ?? ShellHelper.DefaultShellName;

                using (var env = await CreateStandaloneEnvironmentAsync(tenant))
                {
                    var commandManager = env.ServiceProvider.GetService<ICommandManager>();

                    var parameters = new CommandParameters
                    {
                        Arguments = args,
                        Switches = switches,
                        Input = input,
                        Output = output
                    };

                    await commandManager.ExecuteAsync(parameters);
                }

                return CommandReturnCodes.Ok;
            }
            catch (Exception ex)
            {
                if (ex.IsFatal())
                {
                    throw;
                }
                if (ex is TargetInvocationException &&
                    ex.InnerException != null)
                {
                    // 如果这是一个来自反射的异常，并且有一个内部异常，也就是实际的异常，则重定向
                    ex = ex.InnerException;
                }
                await OutputExceptionAsync(output, S["Error executing command \"{0}\"", string.Join(" ", args)], ex);
                return CommandReturnCodes.Fail;
            }
        }

        private async Task OutputExceptionAsync(TextWriter output, LocalizedString title, Exception exception)
        {
            // 显示 header
            await output.WriteLineAsync();
            await output.WriteLineAsync(title);

            // 在堆栈中推送异常，这样我们就可以从最内部显示到最外部
            var errors = new Stack<Exception>();
            for (var scan = exception; scan != null; scan = scan.InnerException)
            {
                errors.Push(scan);
            }

            // 显示内部大部分异常细节
            exception = errors.Peek();
            await output.WriteLineAsync("--------------------------------------------------------------------------------");
            await output.WriteLineAsync();
            await output.WriteLineAsync(exception.Message);
            await output.WriteLineAsync();

            if (!(exception.InnerException == null))
            {
                await output.WriteLineAsync(S["Exception Details: {0}: {1}", exception.GetType().FullName, exception.Message]);
                await output.WriteLineAsync();
                await output.WriteLineAsync(S["Stack Trace:"]);
                await output.WriteLineAsync();

                // Display exceptions from inner most to outer most
                foreach (var error in errors)
                {
                    await output.WriteLineAsync(S["[{0}: {1}]", error.GetType().Name, error.Message]);
                    await output.WriteLineAsync(S["{0}", error.StackTrace]);
                    await output.WriteLineAsync();
                }
            }

            // 显示 footer
            await output.WriteLineAsync("--------------------------------------------------------------------------------");
            await output.WriteLineAsync();
        }

        private async Task<ShellContext> CreateStandaloneEnvironmentAsync(string tenant)
        {
            //检索指定租户的设置。
            var settingsList = await _shellSettingsManager.LoadSettingsAsync();
            if (settingsList.Any())
            {
                var settings = settingsList.SingleOrDefault(s => string.Equals(s.Name, tenant, StringComparison.OrdinalIgnoreCase));
                if (settings == null)
                {
                    throw new Exception(S["Tenant {0} does not exist", tenant]);
                }

                return await _Wd3eHost.CreateShellContextAsync(settings);
            }
            else
            {
                // 对于未初始化的站点(还没有默认设置)，我们创建一个默认设置实例。
                var settings = new ShellSettings { Name = ShellHelper.DefaultShellName, State = TenantState.Uninitialized };
                return await _Wd3eHost.CreateShellContextAsync(settings);
            }
        }
    }
}
